using Godot;
using Hiashatar;
using System;
using System.Collections.Generic;

public partial class Guard : Piece
{
	public List<int> GetControlZone() 
	{
		List<int> controlZone = new List<int>();
		for (int i = 0; i < 8; i++)
		{
			int newRow = row + Direction.QUEENLIKEROW[i];
			int newCol = column + Direction.QUEENLIKECOL[i];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9) 
			{
				break;
			}
			controlZone.Add(newRow * 10 + newCol);
		}
		return controlZone;
	}
}
