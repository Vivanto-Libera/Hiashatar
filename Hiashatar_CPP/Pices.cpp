#include "Pieces.h"

void Hound::move(int row, int col)
{
	if (canEnPassent())
	{
		EnPassent = false;
	}
	if (!isPromoted && ((this->row == 8 && color == Color::WHITE) || (this->row == 1 && color == Color::BLACK)))
	{
		if (((row == 5 || row == 6) && color == Color::WHITE) || ((row == 3 || row == 4) && color == Color::BLACK))
		{
			EnPassent = true;
		}
	}
	this->row = row;
	this->col = col;
	if (!isPromoted)
	{
		if (row == 0 || row == 9)
		{
			promoted = true;
		}
	}
}