using Godot;
using System;
using System.Data.Common;
using System.Threading;

public partial class Board : Node2D
{
	[Signal]
	public delegate void SquarePressedEventHandler(int number);

	private Square[] squares = new Square[100];
	private int selected;

	private void SetSquares(bool invert = false) 
	{
		for (int i = 0; i < 100; i++)
		{
			squares[i] = GetNode<Square>(i.ToString());
			squares[i].SetRowAndColumn(i / 10, i % 10);
			squares[i].SquarePressed += OnSquarePressed;
			squares[i].Reset();
		}
		Invert();
	}

	public void Invert(bool invert = false) 
	{
		RotationDegrees = invert ? 180f : 0f;
	}

	public void OnSquarePressed(int number) 
	{
		EmitSignal(SignalName.SquarePressed, number);
	}

	public void SelectedPiece(int number) 
	{
		squares[selected].SetHighlight(false);
		squares[number].SetHighlight(true);
		selected = number;
	}
	public void SetLegalMove(int index) 
	{
		squares[index].SetLegalMove(true);
	}
	public void ResetLegalMove() 
	{
		foreach (Square square in squares) 
		{
			square.SetLegalMove(false);
		}
	}
	public void SetCapture(int index)
	{
		squares[index].SetCaputure(true);
	}
	public void ResetCapture()
	{
		foreach (Square square in squares)
		{
			square.SetCaputure(false);
		}
	}

	public void Reset() 
	{
		SetSquares();
	}

	public override void _Ready()
	{
		Reset();
	}
}
