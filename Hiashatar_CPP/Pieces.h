//Created by Vivanto(vivanto@qq.com)
//This file is the pieces of Hiashatar

#ifndef PIECES_H
#define PIECES_H
#include<iostream>
#include"Enum.h"

class Piece
{
public:
	std::array<int, 2> getPosition() const
	{
		return std::array<int, 2>{row, col};
	}
	virtual void move(int row, int col)
	{
		this->row = row;
		this->col = col;
	}
	bool isCaptured() const
	{
		return row == -1;
	}
	Color getColor() const
	{
		return color;
	}
	void capture()
	{
		row = -1;
		col = -1;
	}
	Square getPiece() const
	{
		return piece;
	}

	Piece(int row, int col, Color color) : row(row), col(col), color(color){}
	Piece(){}
protected:
	//When row and col are -1, its mean this piece has been captured.
	int row;
	int col;
	Color color;
	Square piece;
};

class Khan : public Piece
{
public:
	Khan(int row, int col, Color color) : Piece(row, col, color) 
	{
		if (color == WHITE)
		{
			piece = WHITEKHAN;
		}
		else
		{
			piece = BLACKKHAN;
		}
	}
	Khan(){}
};

class Lion : public Piece
{
public:
	Lion(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == WHITE)
		{
			piece = WHITELION;
		}
		else
		{
			piece = BLACKLION;
		}
	}
	Lion(){}
};

class Guard : public Piece
{
public:
	//Guard can protect neighbour's squares.
	std::vector<int> getZone()
	{
		const std::array<int, 8> QUEENMOVEROW{ -1, -1, 0, 1, 1, 1, 0, -1 };
		const std::array<int, 8> QUEENMOVECOL{ 0, 1, 1, 1, 0, -1, -1, -1 };
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
		if (color == WHITE)
		{
			piece = WHITEGUARD;
		}
		else
		{
			piece = BLACKGUARD;
		}
	}
	Guard(){}
};

class Camel : public Piece
{
public:
	Color getSquareColor()
	{
		return squareColor;
	}

	Camel(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == WHITE)
		{
			piece = WHITECAMEL;
			if (col == 2)
			{
				squareColor = BLACK;
			}
			else
			{
				squareColor = WHITE;
			}
		}
		else
		{
			piece = BLACKCAMEL;
			if (col == 2)
			{
				squareColor = WHITE;
			}
			else
			{
				squareColor = BLACK;
			}
		}
	}
	Camel(){}
private:
	Color squareColor;
};

class Horse : public Piece
{
public:
	Horse(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == WHITE)
		{
			piece = WHITEHORSE;
		}
		else
		{
			piece = BLACKHORSE;
		}
	}
	Horse(){}
};

class Terge : public Piece
{
public:
	Terge(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == WHITE)
		{
			piece = WHITETERGE;
		}
		else
		{
			piece = BLACKTERGE;
		}
	}
	Terge(){}
};

class Hound : public Piece
{
public:
	bool isPromoted()
	{
		return promoted;
	}
	bool canEnPassant()
	{
		return EnPassant;
	}
	void resetEnPassant()
	{
		EnPassant = false;
	}
	void move(int row, int col)
	{
		if (!isPromoted() && ((this->row == 8 && color == Color::WHITE) || (this->row == 1 && color == Color::BLACK)))
		{
			if (((row == 5 || row == 6) && color == Color::WHITE) || ((row == 3 || row == 4) && color == Color::BLACK))
			{
				EnPassant = true;
			}
		}
		this->row = row;
		this->col = col;
		if (!isPromoted())
		{
			if (row == 0 || row == 9)
			{
				promoted = true;
			}
		}
	}

	Hound(int row, int col, Color color) : Piece(row, col, color)
	{
		if (color == WHITE)
		{
			piece = WHITEHOUND;
		}
		else
		{
			piece = BLACKHOUND;
		}
	}
	Hound(){}
private:
	bool promoted = false;
	bool EnPassant = false;
};

#endif