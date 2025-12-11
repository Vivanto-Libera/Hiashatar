using Godot;
using Hiashatar;
using System;

public partial class Piece : Node2D
{
	[Signal]
	public delegate void PieceButtonPressedEventHandler(int number, int color);

	protected int row;
	protected int column;
	protected PieceColor color = PieceColor.WHITE;

	public virtual void SetRowAndCol(int newRow, int newCol)
	{
		row = newRow;
		column = newCol;
	}
	public int GetPiecePosition()
	{
		if (row != -1)
		{
			return row * 10 + column;
		}
		else 
		{
			return -1;
		}
	}

	public void SetPiecePosition(Vector2 newPosition)
	{
		Position = newPosition;
	}
	public void MoveToPosition(Vector2 newPosition)
	{
		Tween tween = CreateTween();
		tween.TweenProperty(this, "position", newPosition, 0.5);
		tween.SetEase(Tween.EaseType.Out);
	}

	public void Capture()
	{
		SetRowAndCol(-1, -1);
	}

	public void SetColor(PieceColor newColor)
	{
		color = newColor;
		bool isWhite = color == PieceColor.WHITE;
		GetNode<TextureRect>("White").SetDeferred(TextureRect.PropertyName.Visible, isWhite);
		GetNode<TextureRect>("Black").SetDeferred(TextureRect.PropertyName.Visible, !isWhite);
	}

	public void SetButtonDisable(bool disable) 
	{
		GetNode<Button>("Button").SetDeferred(Button.PropertyName.Disabled, disable);
	}

	public void OnButtonPressed() 
	{
		EmitSignal(SignalName.PieceButtonPressed, GetPiecePosition(), (int)color);
	}

	public virtual void Reset() 
	{
		row = -1;
		column = -1;
	}

	public override void _Ready()
	{
		GetNode<Button>("Button").Pressed += OnButtonPressed;
	}
}
