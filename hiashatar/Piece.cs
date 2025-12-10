using Godot;
using Hiashatar;
using System;

public partial class Piece : Node2D
{
	protected int row;
	protected int column;
	protected PieceColor color;

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
	public bool isCaptured()
	{
		return row == -1;
	}

	public void SetColor(PieceColor newColor)
	{
		color = newColor;
		bool isWhite = color == PieceColor.WHITE;
		GetNode<TextureRect>("White").SetDeferred(TextureRect.PropertyName.Visible, isWhite);
		GetNode<TextureRect>("Black").SetDeferred(TextureRect.PropertyName.Visible, !isWhite);
	}
	public PieceColor GetColor() 
	{
		return color;
	}

	public virtual void Reset() 
	{
		row = -1;
		column = -1;
	}
}
