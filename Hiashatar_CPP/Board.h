//Created by Vivanto(vivanto@qq.com)
//This file is the board of Hiashatar
#ifndef BOARD_H
#define BOARD_H
#include<iostream>
#include<pybind11/pybind11.h>
#include<array>
#include<queue>
#include<map>
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

class Board
{
public:
	static std::map<int, std::string> indextoMove;
	static std::map<std::string, int> moveToIndex;

	std::array<std::array<Square, 10>, 10> board;
	Color turn;

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