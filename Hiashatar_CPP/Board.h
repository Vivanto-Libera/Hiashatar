//Created by Vivanto(vivanto@qq.com)
//This file is the board of Hiashatar
#ifndef BOARD_H
#define BOARD_H
#include<iostream>
#include<pybind11/pybind11.h>
#include<array>
#include<queue>
#include<map>
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

const std::array<int, 8> QUEENMOVEROW{ -1, -1, 0, 1, 1, 1, 0, -1 };
const std::array<int, 8> QUEENMOVECOL{ 0, 1, 1, 1, 0, -1, -1, -1 };

class Board
{
public:
	//String "abcd" is mean from row a col b to row c col d.
	const static std::map<int, std::string> indextoMove;
	const static std::map<std::string, int> moveToIndex;

	std::array<std::array<Square, 10>, 10> board;
	Color turn;
	Pieces whitePieces = Pieces(Color::WHITE);
	Pieces blackPieces = Pieces(Color::BLACK);

	static std::string moveToString(int move)
	{
		std::string str = "0000";
		for (int i = 3; i >= 0; i--)
		{
			str[i] = move % 10;
			move /= 10;
		}
		return str;
	}
	static std::array<int, 4> stringToMove(std::string str)
	{
		return std::array<int, 4>{(int)(str[0] - '0'), (int)(str[1] - '0'), (int)(str[2] - '0'), (int)(str[3] - '0')};
	}

	Board();
private:
	int noProcess;
	int repeat;
	std::queue<std::array<std::array<Square, 10>, 10>> preBoards;
};
#endif