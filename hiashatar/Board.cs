using Godot;
using Hiashatar;
using System;
using System.Data.Common;
using System.Reflection;
using System.Threading;

public partial class Board : Node2D
{
	[Signal]
	public delegate void SquarePressedEventHandler(int number);
	[Signal]
	public delegate void SettedCaptureEventHandler(int number, bool set);

	private Square[] squares = new Square[100];
	private int selected;
	private int from;
	private int to;

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
		if (selected != -1)
		{
			squares[selected].SetHighlight(false);
		}
		squares[number].SetHighlight(true);
		selected = number;
	}
	public void SetLegalMove(int index) 
	{
		squares[index].SetLegalMove(true);
	}
	public void SetCapture(int index)
	{
		squares[index].SetCaputure(true);
		EmitSignal(SignalName.SettedCapture, index, true);
	}

    public void MovedPiece(int number)
    {
        ResetLegalMove();
        ResetCapture();
        ResetHighlight();
        if (number != -1)
        {
            from = Conversion.NumToFromAndTo(number)[0];
            to = Conversion.NumToFromAndTo(number)[1];
            squares[from].SetHighlight(true);
            squares[to].SetHighlight(true);
        }
        selected = -1;
    }

    public void ResetLegalMove()
    {
        foreach (Square square in squares)
        {
            square.SetLegalMove(false);
        }
    }
    public void ResetCapture()
	{
		foreach (Square square in squares)
		{
			if (square.CanCapture())
			{
				square.SetCaputure(false);
				EmitSignal(SignalName.SettedCapture, square.GetRow() * 10 + square.GetColumn(), false);
			}
			
		}
	}
	public void ResetHighlight() 
	{
		foreach (Square square in squares)
		{
			square.SetHighlight(false);
		}
	}

	public void Reset() 
	{
		SetSquares();
	}

    private void SetSquares(bool invert = false)
    {
        for (int i = 0; i < 100; i++)
        {
            squares[i] = GetNode<Square>(i.ToString());
            squares[i].SetRowAndColumn(i / 10, i % 10);
            squares[i].Reset();
        }
        Invert();
    }
    public override void _Ready()
	{
		Reset();
		for (int i = 0; i < 100; i++)
		{
			squares[i].SquarePressed += OnSquarePressed;
		}
	}
}
