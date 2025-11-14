//Created by Vivanto(vivanto@qq.com)
//This file is the pieces of Hiashatar

#ifndef PIECES_H
#define PIECES_H
#include<iostream>
#include "Board.h"

class Piece
{
public:
	std::array<int, 2> getPosition()
	{
		return std::array<int, 2>{row, col};
	}
	virtual void move(int row, int col)
	{
		this->row = row;
		this->col = col;
	}
	bool isCaptured()
	{
		return row == -1;
	}
	Color getColor()
	{
		return color;
	}
	void capture()
	{
		row = -1;
		col = -1;
	}
	Square getPiece()
	{
		return piece;
	}

	Piece(int row, int col, Color color) : row(row), col(col), color(color){}
protected:
	//When row and col are -1, its mean this piece has been captured.
	int row;
	int col;
	Color color;
	Square piece;
};

class Khan : Piece
{
public:
	Khan(int row, int col, Color color) : Piece(row, col, color) 
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITEKHAN;
		}
		else
		{
			piece = Square::BLACKKHAN;
		}
	}
};

class Lion : Piece
{
public:
	Lion(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITELION;
		}
		else
		{
			piece = Square::BLACKLION;
		}
	}
};

class Guard : Piece
{
public:
	//Guard can protect neighbour's squares.
	std::vector<int> getZone()
	{
		std::vector<int> zone;
		for (int i = 0; i < 8; i++)
		{
			int newRow = row + QUEENMOVEROW[i];
			int newCol = col + QUEENMOVECOL[i];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			zone.emplace_back(newRow * 10 + newCol);
		}
		return zone;
	}

	Guard(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITEGUARD;
		}
		else
		{
			piece = Square::BLACKGUARD;
		}
	}
};

class Camel : Piece
{
public:
	Color getSquareColor()
	{
		return squareColor;
	}

	Camel(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITECAMEL;
			if (col == 2)
			{
				squareColor = Color::BLACK;
			}
			else
			{
				squareColor = Color::WHITE;
			}
		}
		else
		{
			piece = Square::BLACKCAMEL;
			if (col == 2)
			{
				squareColor = Color::WHITE;
			}
			else
			{
				squareColor = Color::BLACK;
			}
		}
	}
private:
	Color squareColor;
};

class Horse : Piece
{
public:
	Horse(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITEHORSE;
		}
		else
		{
			piece = Square::BLACKHORSE;
		}
	}
};

class Terge : Piece
{
public:
	Terge(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITETERGE;
		}
		else
		{
			piece = Square::BLACKTERGE;
		}
	}
};

class Hound : Piece
{
public:
	bool isPromoted()
	{
		return promoted;
	}
	bool canEnPassent()
	{
		return EnPassent;
	}
	void move(int row, int col) override;

	Hound(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == Color::WHITE)
		{
			piece = Square::WHITEHOUND;
		}
		else
		{
			piece = Square::BLACKHOUND;
		}
	}
private:
	bool promoted = false;
	bool EnPassent = false;
};

#endif