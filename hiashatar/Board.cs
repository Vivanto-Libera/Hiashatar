using Godot;
using System;
using System.Threading;

public partial class Board : Node2D
{
	Square[] squares = new Square[100];

	private void SetSquares(bool invert = false) 
	{
		for (int i = 0; i < 100; i++)
		{
			squares[i] = GetNode<Square>(i.ToString());
			squares[i].SetRowAndColumn(i / 10, i % 10);
			squares[i].Reset();
		}
		RotationDegrees = invert ? 180f : 0f;
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
