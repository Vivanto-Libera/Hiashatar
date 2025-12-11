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
			to = number;
			EmitSignal(SignalName.PieceCaptured, number);
		}
	}

	public List<int> GetPieceLegalMoves() 
	{
		List<int> moves = new List<int>();
		foreach (int move in legalMoves) 
		{
			GD.Print(Conversion.NumToFromAndTo(move)[0], Conversion.NumToFromAndTo(move)[1], from);
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
