using Godot;
using System;
using static Hiashatar.PieceColor;

public partial class Hound : Piece
{
	private bool isPromoted;
	private bool canEnPassant;

	public bool Ispromoted()
	{
		return isPromoted;
	}
	public bool CanEnPassant()
	{
		return canEnPassant;
	}
	public void ResetEnPassant() 
	{
		canEnPassant = false;
	}

	public override void SetRowAndCol(int newRow, int newCol)
	{
		if (!isPromoted && ((row == 8 && color == WHITE) || (row == 1 && color == BLACK)))
		{
			if (((newRow == 5 || newRow == 6) && color == WHITE) || ((newRow == 3 || newRow == 4) && color == BLACK))
			{
				canEnPassant = true;
			}
		}
		base.SetRowAndCol(newRow, newCol);
		if (!isPromoted)
		{
			if (row == 0 || row == 9)
			{
				isPromoted = true;
			}
		}
	}
	public override void Reset()
	{
		base.Reset();
		isPromoted = false;
		canEnPassant = false;
	}
}
