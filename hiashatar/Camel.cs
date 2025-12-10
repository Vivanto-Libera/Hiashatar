using Godot;
using Hiashatar;
using System;
using static Hiashatar.PieceColor;

public partial class Camel : Piece
{
	public PieceColor SquareColor() 
	{
		return row % 2 == column % 2 ? BLACK : WHITE;
	}
}
