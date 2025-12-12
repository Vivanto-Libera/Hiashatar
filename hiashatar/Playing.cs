using Godot;
using Hiashatar;
using System;
using System.Collections.Generic;

public partial class Playing : Node
{
	[Signal]
	public delegate void SetPiecesAbleEventHandler(int color);
	[Signal]
	public delegate void PieceSelectedEventHandler(int number);
	[Signal]
	public delegate void PieceCapturedEventHandler(int number);
	[Signal]
	public delegate void MovedEventHandler(int move);
	[Signal]
	public delegate void GameOverEventHandler(int winner);

	private GameBoard board = new();
	private GameState state;
	private List<int> legalMoves = new List<int>();
	private int from;
	private int to;
	private int enPassant = -1;

	public void InitialGame(GameState state) 
	{
		this.state = state;
		board = new();
		legalMoves = board.LegalMoves();
		GetNode<Label>("Message").SetDeferred(Label.PropertyName.Visible, true);
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, true);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, true);
		if (state == GameState.LM) 
		{
			GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, false);
			GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
			SetPiecesButton();
		}
	}

	public void OnPiecePressed(int number, int color) 
	{
		if ((PieceColor)color == board.turn) 
		{
			from = number;
			EmitSignal(SignalName.PieceSelected, number);
		}
		else 
		{
			EmitSignal(SignalName.PieceCaptured, number);
		}
	}

	public void OnSquarePressed(int number) 
	{
		to = number;
		board.ApplyMove(Conversion.MoveToIndex(from * 100 + to));
		enPassant = board.GetEnPassant();
		legalMoves = board.LegalMoves();
		EmitSignal(SignalName.Moved, from * 100 + number);
		PieceColor winner = board.IsTerminal();
		if (winner != PieceColor.NOTEND) 
		{
			EmitSignal(SignalName.GameOver, (int)winner);
			return;
		}
		if (state == GameState.LM)
		{
			SetPiecesButton();
		}
	}


	public List<int> GetPieceLegalMoves() 
	{
		List<int> moves = new List<int>();
		foreach (int move in legalMoves) 
		{
			if (from == Conversion.NumToFromAndTo(move)[0]) 
			{
				moves.Add(Conversion.NumToFromAndTo(move)[1]);
			}
		}
		return moves;
	}

	public bool IsSquareEmpty(int number) 
	{
		return board.board[number / 10, number % 10] == PieceType.EMPTY;
	}

	private void SetPiecesButton() 
	{
		SetTurnMessage(board.turn);
		EmitSignal(SignalName.SetPiecesAble, (int)board.turn);
	}
	public int GetEnPassant() 
	{
		return enPassant;
	}

	public void HideAll() 
	{
		GetNode<Label>("Message").SetDeferred(Label.PropertyName.Visible, false);
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
	}

	public void SetTurnMessage(PieceColor turn) 
	{
		if (turn == PieceColor.WHITE) 
		{
			SetMessage("白方回合");
		}
		else 
		{
			SetMessage("黑方回合");
		}
	}
	public void SetGameOverMessage(PieceColor winner) 
	{
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
		switch (winner) 
		{
			case PieceColor.WHITE:
				SetMessage("白方获胜");
				break;
			case PieceColor.BLACK:
				SetMessage("黑方获胜");
				break;
			case PieceColor.DRAW:
				SetMessage("和棋");
				break;
			default:
				SetMessage("游戏结束");
				break;
		}
	}
	private void SetMessage(string message)
	{
		GetNode<Label>("Message").Text = message;

	}

	public PieceColor GetTurn() 
	{
		return board.turn;
	}
	public void OnOverPressed() 
	{
		EmitSignal(SignalName.GameOver, (int)PieceColor.NOTEND);
	}
}
