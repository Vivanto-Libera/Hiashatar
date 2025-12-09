using Godot;
using System;
using System.Data.Common;
using System.Threading;

public partial class Board : Node2D
{
	[Signal]
	public delegate void SquarePressedEventHandler(int number);

	Square[] squares = new Square[100];

	private void SetSquares(bool invert = false) 
	{
		for (int i = 0; i < 100; i++)
		{
			squares[i] = GetNode<Square>(i.ToString());
			squares[i].SetRowAndColumn(i / 10, i % 10);
			squares[i].SquarePressed += OnSquarePressed;
			squares[i].Reset();
		}
		RotationDegrees = invert ? 180f : 0f;
	}

	public void OnSquarePressed(int number) 
	{
		EmitSignal(SignalName.SquarePressed, number);
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
