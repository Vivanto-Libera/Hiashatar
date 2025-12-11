using Godot;
using System;

public partial class Square : Node2D
{
	[Signal]
	public delegate void SquarePressedEventHandler(int number);

	private int row;
	private int column;

	public int GetRow() 
	{
		return row;
	}
	public int GetColumn() 
	{
		return column;
	}
	public void SetRowAndColumn(int newRow, int newColumn) 
	{
		row = newRow;
		column = newColumn;

		Color color;
		if (row % 2 == column % 2) 
		{
			color = new Color(0, 102 / 255f, 179 / 255f);
		}
		else 
		{
			color = new Color(230 / 255f, 240 / 255f, 250 / 255f);
		}
		GetNode<ColorRect>("BackGround").Color = color;
	}

	public void SetLegalMove(bool visible) 
	{
		GetNode<TextureRect>("LegalMove").SetDeferred(TextureRect.PropertyName.Visible, visible);
		GetNode<Button>("Button").SetDeferred(Button.PropertyName.Disabled, !visible);
	}
	public void SetCaputure(bool visible) 
	{
		GetNode<TextureRect>("Capture").SetDeferred(TextureRect.PropertyName.Visible, visible);
		GetNode<Button>("Button").SetDeferred(Button.PropertyName.Disabled, true);
	}

	public void SetHighlight(bool visible) 
	{
		GetNode<ColorRect>("Highlight").SetDeferred(ColorRect.PropertyName.Visible, visible);
	}

	private void OnButtonPressed() 
	{
		EmitSignal(SignalName.SquarePressed, row * 10 + column);
	}

	public void Reset() 
	{
		SetLegalMove(false);
		SetCaputure(false);
		SetHighlight(false);
	}
}
