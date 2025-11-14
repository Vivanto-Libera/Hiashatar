//Created by Vivanto(vivanto@qq.com)
//This file is the board of Hiashatar
#ifndef BOARD_H
#define BOARD_H
#include<iostream>
#include<pybind11/pybind11.h>
#include<array>
#include<unordered_set>
#include<cstdint>
#include"Pieces.h"

namespace py = pybind11;

enum Square
{
	EMPTY,
	WHITEKHAN,
	WHITELION,
	WHITEGUARD,
	WHITECAMEL,
	WHITEHORSE,
	WHITETERGE,
	WHITEHOUND,
	BLACKKHAN,
	BLACKLION,
	BLACKGUARD,
	BLACKCAMEL,
	BLACKHORSE,
	BLACKTERGE,
	BLACKHOUND,
};
enum Color
{
	BLACK,
	WHITE,
	DRAW,
	NOTEND
};

struct Pieces
{
	Khan khan;
	Lion lion;
	std::array<Guard, 2> guards;
	std::array<Camel, 2> camels;
	std::array<Horse, 2> horses;
	std::array<Terge, 2> terges;
	std::array<Hound, 10> hounds;
	Pieces(const Color& color)
	{
		int row;
		int houndRow;
		if (color == Color::BLACK)
		{
			row = 0;
			houndRow = 1;
			khan = Khan(row, 5, color);
			lion = Lion(row, 4, color);
		}
		else
		{
			row = 9;
			houndRow = 8;
			khan = Khan(row, 4, color);
			lion = Lion(row, 5, color);
		}
		guards[0] = Guard(row, 3, color);
		guards[1] = Guard(row, 6, color);
		camels[0] = Camel(row, 2, color);
		camels[1] = Camel(row, 7, color);
		horses[0] = Horse(row, 1, color);
		horses[1] = Horse(row, 8, color);
		terges[0] = Terge(row, 0, color);
		terges[1] = Terge(row, 9, color);
		for (int i = 0; i < 10; i++)
		{
			hounds[i] = Hound(houndRow, i, color);
		}
		
	}
};

//Up, Up-Right, Right, Down-Right, Down, Down-left, Left, Up-Left
//Even index is vertical, and odd index is diagonal
const std::array<int, 8> QUEENMOVEROW{ -1, -1, 0, 1, 1, 1, 0, -1 };
const std::array<int, 8> QUEENMOVECOL{ 0, 1, 1, 1, 0, -1, -1, -1 };
//Down-Right, Down-Left, Up-Right, Up-Left, Right-Down, Left-Down, Right-Up, Right-Down
const std::array<int, 8> HORSEMOVEROW{ 2, 2, -2, -2, 1, 1, -1, -1 };
const std::array<int, 8> HORSEMOVECOL{ 1, -1, 1, -1, 2, -2, 2, -2 };
//Move "abcd" is mean from row a col b to row c col d.
const std::array<uint16_t, 3516> indextoMove;

class Board
{
public:
	std::array<std::array<Square, 10>, 10> board;
	Color turn;
	Pieces whitePieces = Pieces(Color::WHITE);
	Pieces blackPieces = Pieces(Color::BLACK);

	std::vector<int> legalMoves();
	bool hasLegalMoves();

	static std::array<int, 4> NumToMove(int num)
	{
		std::array<int, 4> move;
		for(int i = 3; i >= 0; i--)
		{
			move[i] = num % 10;
			num /= 10;
		}
		return move;
	}
	static int moveToNum(std::array<int, 4> move)
	{
		return move[0] * 1000 + move[1] * 100 + move[2] * 10 + move[3];
	}

	Board();
private:
	int noProcess;
	std::vector<std::array<std::array<Square, 10>, 10>> preBoards;

	//If move == -1 is mean do nothing and just judge now is being checked or not
	//And if move != -1 is mean check after this move Khan is being checked or not
	bool isChecked(int move);
	Color colorOfSquare(int row, int col);
	int repeatCount();
};
#endif