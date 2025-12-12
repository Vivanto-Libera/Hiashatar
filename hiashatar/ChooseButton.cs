using Godot;
using Hiashatar;
using System;

public partial class ChooseButton : Button
{
	[Signal]
	public delegate void ColorChoseEventHandler(int color);
	//1:White, -1Black, 0 Random;
	[Export]
	public int color;

	public void OnPressed() 
	{
		PieceColor choseColor;
		switch (color) 
		{
			case -1:
				choseColor = PieceColor.BLACK;
				break;
			case 1:
				choseColor = PieceColor.WHITE;
				break;
			default:
				RandomNumberGenerator rng = new RandomNumberGenerator();
				int randNum = rng.RandiRange(0, 1);
				if (randNum == 0) 
				{
					choseColor = PieceColor.WHITE;
				}
				else 
				{
					choseColor = PieceColor.BLACK;
				}
				break;
		}
		EmitSignal(SignalName.ColorChose, (int)choseColor);
	}
}
