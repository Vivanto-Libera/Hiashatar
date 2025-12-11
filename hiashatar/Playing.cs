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

	private GameBoard board = new();
	private GameState state;
	private List<int> legalMoves = new List<int>();
	private int from;
	private int to;

	public void InitialGame(GameState state) 
	{
		this.state = state;
		board = new();
		legalMoves = board.LegalMoves();
		if (state == GameState.LM) 
		{
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
		legalMoves = board.LegalMoves();
		SetPiecesButton();
		EmitSignal(SignalName.Moved, from * 100 + number);
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
		EmitSignal(SignalName.SetPiecesAble, (int)board.turn);
	}
}
