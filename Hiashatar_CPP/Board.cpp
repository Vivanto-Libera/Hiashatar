#include"Board.h"

std::vector<int> Board::legalMoves()
{
	std::vector<int> moves;
	Pieces* pieces;
	std::unordered_set<int> guardZone;
	if (turn == WHITE)
	{
		pieces = &whitePieces;
		for (int i = 0; i < 2; i++) 
		{
			if (!blackPieces.guards[i].isCaptured())
			{
				std::vector<int> zone = blackPieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	else
	{
		pieces = &blackPieces;
		for (int i = 0; i < 2; i++)
		{
			if (!whitePieces.guards[i].isCaptured())
			{
				std::vector<int> zone = whitePieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	std::array<int, 2> fromPos;

	//Khan's move
	fromPos = pieces->khan.getPosition();
	for (int i = 0; i < 8; i++)
	{
		int newRow = fromPos[0] + QUEENMOVEROW[i];
		int newCol = fromPos[1] + QUEENMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if (colorOfSquare(newRow, newCol) != turn)
		{
			int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
			if (!isChecked(num))
			{
				moves.emplace_back(num);
			}
		}
	}

	//Lion's move
	for (int i = 0; i < 11; i++)
	{
		if (i == 0 && !pieces->lion.isCaptured())
		{
			fromPos = pieces->lion.getPosition();
		}
		else if (i != 0)
		{
			if (pieces->hounds[i - 1].isPromoted() && !pieces->hounds[i - 1].isCaptured())
			{
				fromPos = pieces->hounds[i - 1].getPosition();
			}
			else
			{
				continue;
			}
		}
		else
		{
			continue;
		}
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (colorOfSquare(newRow, newCol) != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (!isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Guards' move
	for (auto& guard : pieces->guards)
	{
		if (guard.isCaptured())
		{
			continue;
		}
		fromPos = guard.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			for (int k = 1; k < 3; k++)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] == EMPTY)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (!isChecked(num))
					{
						moves.emplace_back(num);
					}
				}
				else if (colorOfSquare(newRow, newCol) != turn)
				{
					if (j % 2 == 1 && k == 1)
					{
						int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
						if (!isChecked(num))
						{
							moves.emplace_back(num);
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Camels' move
	for (auto& camel : pieces->camels)
	{
		if (camel.isCaptured())
		{
			continue;
		}
		fromPos = camel.getPosition();
		for (int j = 1; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (colorOfSquare(newRow, newCol) != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (!isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Terges' move
	for (auto& terge : pieces->terges)
	{
		if (terge.isCaptured())
		{
			continue;
		}
		fromPos = terge.getPosition();
		for (int j = 0; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (colorOfSquare(newRow, newCol) != turn)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (!isChecked(num))
					{
						moves.emplace_back(num);
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Horses' move
	for (auto& horse : pieces->horses)
	{
		if (horse.isCaptured())
		{
			continue;
		}
		fromPos = horse.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			newRow += HORSEMOVEROW[j];
			newCol += HORSEMOVECOL[j];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (!isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
		}
	}

	//Hounds' move
	int houndDiection;
	if (turn == Color::WHITE)
	{
		houndDiection = -1;
	}
	else
	{
		houndDiection = 1;
	}
	for (auto& hound : pieces->hounds)
	{
		if (hound.isCaptured() || hound.isPromoted())
		{
			continue;
		}
		fromPos = hound.getPosition();
		int moveDistance = 1;
		if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
		{
			moveDistance = 3;
		}
		//Move Forward
		for (int i = 1; i <= moveDistance; i++)
		{
			int newRow = fromPos[0] + houndDiection * i;
			if (newRow < 0 || newRow > 9)
			{
				continue;
			}
			if (board[newRow][fromPos[1]] == EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, fromPos[1]});
				if (!isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
			else
			{
				break;
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + fromPos[1]) != guardZone.end())
			{
				break;
			}
		}
		//Capure
		int newRow = fromPos[0] + houndDiection;
		for (int i = -1; i <= 1; i += 2)
		{
			int newCol = fromPos[1] + i;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn && board[newRow][newCol] != EMPTY)
			{
				int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
				if (!isChecked(num))
				{
					moves.emplace_back(num);
				}
			}
			//En passent
			if (board[newRow][newCol] == EMPTY)
			{
				if ((board[fromPos[0]][newCol] == WHITEHOUND && turn == BLACK) || (board[fromPos[0]][newCol] == BLACKHOUND && turn == WHITE))
				{
					if (static_cast<Hound*>(findPiece(fromPos[0], newCol))->canEnPassent())
					{
						int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
						if (!isChecked(num))
						{
							moves.emplace_back(num);
						}
					}
				}
			}
		}
	}

	return moves;
}
bool Board::hasLegalMoves()
{
	Pieces* pieces;
	std::unordered_set<int> guardZone;

	if (turn == WHITE)
	{
		pieces = &whitePieces;
		for (int i = 0; i < 2; i++)
		{
			if (!blackPieces.guards[i].isCaptured())
			{
				std::vector<int> zone = blackPieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	else
	{
		pieces = &blackPieces;
		for (int i = 0; i < 2; i++)
		{
			if (!whitePieces.guards[i].isCaptured())
			{
				std::vector<int> zone = whitePieces.guards[i].getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}
	std::array<int, 2> fromPos;

	//Khan's move
	fromPos = pieces->khan.getPosition();
	for (int i = 0; i < 8; i++)
	{
		int newRow = fromPos[0] + QUEENMOVEROW[i];
		int newCol = fromPos[1] + QUEENMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if (colorOfSquare(newRow, newCol) != turn)
		{
			if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
			{
				return true;
			}
		}
	}

	//Lions' move
	for (int i = 0; i < 11; i++)
	{
		if (i == 0 && !pieces->lion.isCaptured())
		{
			fromPos = pieces->lion.getPosition();
		}
		else if (i != 0)
		{
			if (pieces->hounds[i - 1].isPromoted() && !pieces->hounds[i - 1].isCaptured())
			{
				fromPos = pieces->hounds[i - 1].getPosition();
			}
			else
			{
				continue;
			}
		}
		else
		{
			continue;
		}

		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}

				if (colorOfSquare(newRow, newCol) != turn)
				{
					if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}

				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Guards' move
	for (auto& guard : pieces->guards)
	{
		if (guard.isCaptured())
		{
			continue;
		}
		fromPos = guard.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			for (int k = 1; k < 3; k++)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (board[newRow][newCol] == EMPTY)
				{
					int num = moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol});
					if (!isChecked(num))
					{
						return true;
					}
				}
				else if (colorOfSquare(newRow, newCol) != turn)
				{
					if (j % 2 == 1 && k == 1)
					{
						if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
						{
							return true;
						}
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Camels' move
	for (auto& camel : pieces->camels)
	{
		if (camel.isCaptured())
		{
			continue;
		}
		fromPos = camel.getPosition();
		for (int j = 1; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (colorOfSquare(newRow, newCol) != turn)
				{
					if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Terges' move
	for (auto& terge : pieces->terges)
	{
		if (terge.isCaptured())
		{
			continue;
		}
		fromPos = terge.getPosition();
		for (int j = 0; j < 8; j += 2)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			while (true)
			{
				newRow += QUEENMOVEROW[j];
				newCol += QUEENMOVECOL[j];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					break;
				}
				if (colorOfSquare(newRow, newCol) != turn)
				{
					if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
					{
						return true;
					}
					if (board[newRow][newCol] != EMPTY)
					{
						break;
					}
				}
				else
				{
					break;
				}
				if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
				{
					break;
				}
			}
		}
	}

	//Horses' move
	for (auto& horse : pieces->horses)
	{
		if (horse.isCaptured())
		{
			continue;
		}
		fromPos = horse.getPosition();
		for (int j = 0; j < 8; j++)
		{
			int newRow = fromPos[0];
			int newCol = fromPos[1];
			newRow += HORSEMOVEROW[j];
			newCol += HORSEMOVECOL[j];
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn)
			{
				if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
				{
					return true;
				}
			}
		}
	}

	//Hounds' move
	int houndDiection;
	if (turn == Color::WHITE)
	{
		houndDiection = -1;
	}
	else
	{
		houndDiection = 1;
	}
	for (auto& hound : pieces->hounds)
	{
		if (hound.isCaptured() || hound.isPromoted())
		{
			continue;
		}
		fromPos = hound.getPosition();
		int moveDistance = 1;
		if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
		{
			moveDistance = 3;
		}
		for (int i = 1; i <= moveDistance; i++)
		{
			int newRow = fromPos[0] + houndDiection * i;
			if (newRow < 0 || newRow > 9)
			{
				continue;
			}
			if (board[newRow][fromPos[1]] == EMPTY)
			{
				if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, fromPos[1]})))
				{
					return true;
				}
			}
			else
			{
				break;
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + fromPos[1]) != guardZone.end())
			{
				break;
			}
		}
		int newRow = fromPos[0] + houndDiection;
		for (int i = -1; i <= 1; i += 2)
		{
			int newCol = fromPos[1] + i;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				continue;
			}
			if (colorOfSquare(newRow, newCol) != turn && board[newRow][newCol] != EMPTY)
			{
				if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
				{
					return true;
				}
			}

			//En passent
			if (board[newRow][newCol] == EMPTY)
			{
				if ((board[fromPos[0]][newCol] == WHITEHOUND && turn == BLACK) || (board[fromPos[0]][newCol] == BLACKHOUND && turn == WHITE))
				{
					if (static_cast<Hound*>(findPiece(fromPos[0], newCol))->canEnPassent())
					{
						if (!isChecked(moveToNum(std::array<int, 4>{fromPos[0], fromPos[1], newRow, newCol})))
						{
							return true;
						}
					}
				}
			}
		}
	}
	return false;
}

Color Board::isTerminal()
{
	if (repeatCount() == 3 || noProgress == 50)
	{
		return DRAW;
	}
	if (!hasLegalMoves())
	{
		if(isChecked(-1))
		{
			if (turn == BLACK)
			{
				return WHITE;
			}
			else
			{
				return BLACK;
			}
		}
		else
		{
			return DRAW;
		}
	}

	//Insuficient Material
	if (blackPieces.lion.isCaptured() && whitePieces.lion.isCaptured())
	{
		for (int i = 0; i < 2; i++)
		{
			if (!blackPieces.guards[i].isCaptured() || !whitePieces.guards[i].isCaptured())
			{
				return NOTEND;
			}
			if (!blackPieces.terges[i].isCaptured() || !whitePieces.terges[i].isCaptured())
			{
				return NOTEND;
			}
		}
		for (int i = 0; i < 10; i++)
		{
			if (!blackPieces.hounds[i].isCaptured() || !whitePieces.hounds[i].isCaptured())
			{
				return NOTEND;
			}
		}
		Color camelColor = DRAW;
		int materia = 0;
		for (int i = 0; i < 2; i++)
		{
			if (materia > 1)
			{
				return NOTEND;
			}
			if (!blackPieces.horses[i].isCaptured())
			{
				materia++;
			}
			if (!whitePieces.horses[i].isCaptured())
			{
				materia++;
			}
			if (!blackPieces.camels[i].isCaptured())
			{
				if (camelColor != DRAW)
				{
					if (camelColor != blackPieces.camels[i].getSquareColor())
					{
						return NOTEND;
					}
				}
				else
				{
					camelColor = blackPieces.camels[i].getSquareColor();
					materia++;
				}
			}
			if (!whitePieces.camels[i].isCaptured())
			{
				if (camelColor != DRAW)
				{
					if (camelColor != whitePieces.camels[i].getSquareColor())
					{
						return NOTEND;
					}
				}
				else
				{
					camelColor = whitePieces.camels[i].getSquareColor();
					materia++;
				}
			}
		}
		if (materia > 1)
		{
			return NOTEND;
		}
		return DRAW;
	}
	return NOTEND;
}

void Board::applyMove(int moveIndex)
{
	noProgress++;
	if (preBoards.size() == 6)
	{
		preBoards.erase(preBoards.begin());
	}
	preBoards.emplace_back(board);

	std::array<int, 4> move = NumToMove(indexToMove[moveIndex]);
	if (board[move[2]][move[3]] != EMPTY)
	{
		findPiece(move[2], move[3])->capture();
		noProgress = 0;
	}
	if (board[move[0]][move[1]] == WHITEHOUND || board[move[0]][move[1]] == BLACKHOUND)
	{
		noProgress = 0;
		if (move[3] - move[1] != 0 && board[move[2]][move[3]] == EMPTY)
		{
			findPiece(move[0], move[3])->capture();
			board[move[0]][move[3]] = EMPTY;
		}
	}

	findPiece(move[0], move[1])->move(move[2], move[3]);
	board[move[2]][move[3]] = board[move[0]][move[1]];
	board[move[0]][move[1]] = EMPTY;

	//Promote
	if((board[move[2]][move[3]] == BLACKHOUND || board[move[2]][move[3]] == WHITEHOUND) && (move[2] == 9 || move[2] == 0))
	{
		board[move[2]][move[3]] = turn == WHITE ? WHITELION : BLACKLION;
	}
	turn = turn == WHITE ? BLACK : WHITE;
}

std::vector<std::array<std::array<float, 10>, 10>> Board::neuralworkInput()
{
	std::vector<std::array<std::array<float, 10>, 10>> inputs;
	std::vector<std::array<std::array<float, 10>, 10>> input1 = inputFormBoard(board);
	inputs.insert(inputs.end(), std::make_move_iterator(input1.begin()), std::make_move_iterator(input1.end()));
	for (int i = 5; i >= 0; i--)
	{
		std::vector<std::array<std::array<float, 10>, 10>> input2;
		if ( i > ((int)preBoards.size() - 1))
		{
			input2 = inputEmptyBoard();
		}
		else
		{
			input2 = inputFormBoard(preBoards[i]);
		}
		inputs.insert(inputs.end(), std::make_move_iterator(input2.begin()), std::make_move_iterator(input2.end()));
	}
	int repeat = repeatCount();
	for (int i = 1; i <= 2; i++)
	{
		std::array<std::array<float, 10>, 10> input3{};
		if (i <= repeat)
		{
			for (auto& input : input3)
			{
				input.fill(1);
			}
		}
		inputs.emplace_back(input3);
	}
	std::array<std::array<float, 10>, 10> input4{};
	for (auto& input : input4)
	{
		input.fill( 1.0 * noProgress / 50);
	}
	inputs.emplace_back(input4);
	return inputs;
}

bool Board::isChecked(int move) const
{
	std::array<std::array<Square, 10>, 10> newBoard = board;
	if(move > 0)
	{
		std::array<int, 4> moveArray = NumToMove(move);
		newBoard[moveArray[2]][moveArray[3]] = newBoard[moveArray[0]][moveArray[1]];
		newBoard[moveArray[0]][moveArray[1]] = EMPTY;
		//Check en passent
		if (board[moveArray[0]][moveArray[1]] == WHITEHOUND || board[moveArray[0]][moveArray[1]] == BLACKHOUND)
		{
			if (moveArray[3] - moveArray[1] != 0)
			{
				if (board[moveArray[2]][moveArray[3]] == EMPTY)
				{
					newBoard[moveArray[0]][moveArray[3]] = EMPTY;
				}
			}
		}
	}

	//Find the guard Zone
	std::unordered_set<int> guardZone;
	for (int i = 0; i < 10; i++)
	{
		for (int j = 0; j < 10; j++)
		{
			if ((newBoard[i][j] == WHITEGUARD && turn == WHITE)
				|| (newBoard[i][j] == BLACKGUARD && turn == BLACK))
			{
				std::vector<int> zone = Guard(i, j, turn).getZone();
				guardZone.insert(zone.begin(), zone.end());
			}
		}
	}

	//Find the Khan
	std::array<int, 2> khanPos{ -1, -1 };
	for (int i = 0; i < 10; i++)
	{
		if (khanPos[0] != -1)
		{
			break;
		}
		for (int j = 0; j < 10; j++)
		{
			if ((newBoard[i][j] == WHITEKHAN && turn == WHITE)
				|| (newBoard[i][j] == BLACKKHAN && turn == BLACK))
			{
				khanPos[0] = i;
				khanPos[1] = j;
				break;
			}
		}
	}

	//Check Khan is being chekced or not
	if (turn == WHITE)
	{
		int newRow = khanPos[0] - 1;
		int newCol = khanPos[1] - 1;
		if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == BLACKHOUND)
			{
				return true;
			}
		}
		newCol += 2;
		if(!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == BLACKHOUND)
			{
				return true;
			}
		}
	}
	else
	{
		int newRow = khanPos[0] + 1;
		int newCol = khanPos[1] - 1;
		if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == WHITEHOUND)
			{
				return true;
			}
		}
		newCol += 2;
		if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
		{
			if (newBoard[newRow][newCol] == WHITEHOUND)
			{
				return true;
			}
		}
	}
	for (int i = 0; i < 8; i++)
	{
		int newRow = khanPos[0] + HORSEMOVEROW[i];
		int newCol = khanPos[1] + HORSEMOVECOL[i];
		if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
		{
			continue;
		}
		if ((newBoard[newRow][newCol] == WHITEHORSE && turn == BLACK)
			|| (newBoard[newRow][newCol] == BLACKHORSE && turn == WHITE))
		{
			return true;
		}
	}
	for (int i = 0; i < 8; i++)
	{
		int newRow = khanPos[0];
		int newCol = khanPos[1];
		int distance = 0;

		while (true) 
		{
			newRow += QUEENMOVEROW[i];
			newCol += QUEENMOVECOL[i];
			distance++;
			if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
			{
				break;
			}
			if ((newBoard[newRow][newCol] == WHITELION && turn == BLACK)
				|| (newBoard[newRow][newCol] == BLACKLION && turn == WHITE))
			{
				return true;
			}
			if (distance == 1)
			{
				if ((newBoard[newRow][newCol] == WHITEKHAN && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKKHAN && turn == WHITE))
				{
					return true;
				}
				if (i % 2 == 1)
				{
					if ((newBoard[newRow][newCol] == WHITEGUARD && turn == BLACK)
						|| (newBoard[newRow][newCol] == BLACKGUARD && turn == WHITE))
					{
						return true;
					}
				}
			}
			if (i % 2 == 0)
			{
				if ((newBoard[newRow][newCol] == WHITETERGE && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKTERGE && turn == WHITE))
				{
					return true;
				}
			}
			else
			{
				if ((newBoard[newRow][newCol] == WHITECAMEL && turn == BLACK)
					|| (newBoard[newRow][newCol] == BLACKCAMEL && turn == WHITE))
				{
					return true;
				}
			}
			if (std::find(guardZone.begin(), guardZone.end(), newRow * 10 + newCol) != guardZone.end())
			{
				break;
			}
		}
	}
	return false;
}
Color Board::colorOfSquare(int row, int col)
{
	if (board[row][col] == EMPTY)
	{
		return DRAW;
	}
	switch (board[row][col])
	{
		case WHITEKHAN:
		case WHITELION:
		case WHITEGUARD:
		case WHITECAMEL:
		case WHITEHORSE:
		case WHITETERGE:
		case WHITEHOUND:
			return WHITE;
		default:
			return BLACK;
	}
}
int Board::repeatCount()
{
	int repeats = 0;
	for (int i = preBoards.size() - 1; i >= 0; i--)
	{
		if (i % 2 == 1)
		{
			continue;
		}
		if (preBoards[i] == board)
		{
			repeats++;
		}
	}
	return repeats;
}

Piece* Board::findPiece(int row, int col)
{
	switch (board[row][col])
	{
		case WHITEKHAN:
			return &whitePieces.khan;
		case WHITELION:
			if (whitePieces.lion.getPosition()[0] == row && whitePieces.lion.getPosition()[1] == col)
			{
				return &whitePieces.lion;
			}
			else
			{
				for (auto& hound : whitePieces.hounds)
				{
					if (!hound.isPromoted())
					{
						continue;
					}
					if (hound.getPosition()[0] == row && hound.getPosition()[1] == col)
					{
						return &hound;
					}
				}
			}
			return nullptr;
		case WHITEGUARD:
			for (auto& guard : whitePieces.guards)
			{
				if (guard.getPosition()[0] == row && guard.getPosition()[1] == col)
				{
					return &guard;
				}
			}
			return nullptr;
		case WHITECAMEL:
			for (auto& camel : whitePieces.camels)
			{
				if (camel.getPosition()[0] == row && camel.getPosition()[1] == col)
				{
					return &camel;
				}
			}
			return nullptr;
		case WHITEHORSE:
			for (auto& horse : whitePieces.horses)
			{
				if (horse.getPosition()[0] == row && horse.getPosition()[1] == col)
				{
					return &horse;
				}
			}
			return nullptr;
		case WHITETERGE:
			for (auto& terge : whitePieces.terges)
			{
				if (terge.getPosition()[0] == row && terge.getPosition()[1] == col)
				{
					return &terge;
				}
			}
			return nullptr;
		case WHITEHOUND:
			for (auto& hound : whitePieces.hounds)
			{
				if (hound.isPromoted())
				{
					continue;
				}
				if (hound.getPosition()[0] == row && hound.getPosition()[1] == col)
				{
					return &hound;
				}
			}
			return nullptr;
		case BLACKKHAN:
			return &blackPieces.khan;
		case BLACKLION:
			if (blackPieces.lion.getPosition()[0] == row && blackPieces.lion.getPosition()[1] == col)
			{
				return &blackPieces.lion;
			}
			else
			{
				for (auto& hound : blackPieces.hounds)
				{
					if (!hound.isPromoted())
					{
						continue;
					}
					if (hound.getPosition()[0] == row && hound.getPosition()[1] == col)
					{
						return &hound;
					}
				}
			}
			return nullptr;
		case BLACKGUARD:
			for (auto& guard : blackPieces.guards)
			{
				if (guard.getPosition()[0] == row && guard.getPosition()[1] == col)
				{
					return &guard;
				}
			}
			return nullptr;
		case BLACKCAMEL:
			for (auto& camel : blackPieces.camels)
			{
				if (camel.getPosition()[0] == row && camel.getPosition()[1] == col)
				{
					return &camel;
				}
			}
			return nullptr;
		case BLACKHORSE:
			for (auto& horse : blackPieces.horses)
			{
				if (horse.getPosition()[0] == row && horse.getPosition()[1] == col)
				{
					return &horse;
				}
			}
			return nullptr;
		case BLACKTERGE:
			for (auto& terge : blackPieces.terges)
			{
				if (terge.getPosition()[0] == row && terge.getPosition()[1] == col)
				{
					return &terge;
				}
			}
			return nullptr;
		case BLACKHOUND:
			for (auto& hound : blackPieces.hounds)
			{
				if (hound.isPromoted())
				{
					continue;
				}
				if (hound.getPosition()[0] == row && hound.getPosition()[1] == col)
				{
					return &hound;
				}
			}
			return nullptr;
		default:
			return nullptr;
	}
}

std::vector<std::array<std::array<float, 10>, 10>> Board::inputFormBoard(const std::array<std::array<Square, 10>, 10>& aBoard) const
{
	std::array<std::array<float, 10>, 10> pKhan{};
	std::array<std::array<float, 10>, 10> pLion{};
	std::array<std::array<float, 10>, 10> pGuard{};
	std::array<std::array<float, 10>, 10> pCamel{};
	std::array<std::array<float, 10>, 10> pHorse{};
	std::array<std::array<float, 10>, 10> pTerge{};
	std::array<std::array<float, 10>, 10> pHound{};
	std::array<std::array<float, 10>, 10> oKhan{};
	std::array<std::array<float, 10>, 10> oLion{};
	std::array<std::array<float, 10>, 10> oGuard{};
	std::array<std::array<float, 10>, 10> oCamel{};
	std::array<std::array<float, 10>, 10> oHorse{};
	std::array<std::array<float, 10>, 10> oTerge{};
	std::array<std::array<float, 10>, 10> oHound{};
	for (int i = 0; i < 10; i++)
	{
		for (int j = 0; j < 10; j++)
		{
			switch (aBoard[i][j])
			{
				case WHITEKHAN:
					pKhan[i][j] = turn == WHITE ? 1 : 0;
					oKhan[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITELION:
					pLion[i][j] = turn == WHITE ? 1 : 0;
					oLion[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITEGUARD:
					pGuard[i][j] = turn == WHITE ? 1 : 0;
					oGuard[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITECAMEL:
					pCamel[i][j] = turn == WHITE ? 1 : 0;
					oCamel[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITEHORSE:
					pHorse[i][j] = turn == WHITE ? 1 : 0;
					oHorse[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITETERGE:
					pTerge[i][j] = turn == WHITE ? 1 : 0;
					oTerge[i][j] = turn == WHITE ? 0 : 1;
					break;
				case WHITEHOUND:
					pHound[i][j] = turn == WHITE ? 1 : 0;
					oHound[i][j] = turn == WHITE ? 0 : 1;
					break;
				case BLACKKHAN:
					pKhan[i][j] = turn == BLACK ? 1 : 0;
					oKhan[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKLION:
					pLion[i][j] = turn == BLACK ? 1 : 0;
					oLion[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKGUARD:
					pGuard[i][j] = turn == BLACK ? 1 : 0;
					oGuard[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKCAMEL:
					pCamel[i][j] = turn == BLACK ? 1 : 0;
					oCamel[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKHORSE:
					pHorse[i][j] = turn == BLACK ? 1 : 0;
					oHorse[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKTERGE:
					pTerge[i][j] = turn == BLACK ? 1 : 0;
					oTerge[i][j] = turn == BLACK ? 0 : 1;
					break;
				case BLACKHOUND:
					pHound[i][j] = turn == BLACK ? 1 : 0;
					oHound[i][j] = turn == BLACK ? 0 : 1;
					break;
				default:
					break;
			}
		}
	}
	return std::vector<std::array<std::array<float, 10>, 10>>
			{pKhan, pLion, pGuard, pCamel, pHorse, pTerge, pHound,
			oKhan, oLion, oGuard, oCamel, oHorse, oTerge, oHound};
}

std::vector<std::array<std::array<float, 10>, 10>> Board::inputEmptyBoard() const
{
	std::array<std::array<float, 10>, 10> empty{};
	std::vector<std::array<std::array<float, 10>, 10>> inputs;
	for (int i = 0; i < 14; i++)
	{
		inputs.emplace_back(empty);
	}
	return inputs;
}

Board::Board()
{
	for (int i = 2; i < 8; i++)
	{
		board[i].fill(EMPTY);
	}
	board[0][0] = board[0][9] = BLACKTERGE;
	board[0][1] = board[0][8] = BLACKHORSE;
	board[0][2] = board[0][7] = BLACKCAMEL;
	board[0][3] = board[0][6] = BLACKGUARD;
	board[0][4] = BLACKLION;
	board[0][5] = BLACKKHAN;

	board[9][0] = board[9][9] = WHITETERGE;
	board[9][1] = board[9][8] = WHITEHORSE;
	board[9][2] = board[9][7] = WHITECAMEL;
	board[9][3] = board[9][6] = WHITEGUARD;
	board[9][5] = WHITELION;
	board[9][4] = WHITEKHAN;

	for (int i = 0; i < 10; i++)
	{
		board[1][i] = BLACKHOUND;
		board[8][i] = WHITEHOUND;
	}

	turn = WHITE;
	noProgress = 0;
}

Board::Board(const Board& aBoard)
{
	board = aBoard.board;
	turn = aBoard.turn;
	whitePieces = aBoard.whitePieces;
	blackPieces = aBoard.blackPieces;
	noProgress = aBoard.noProgress;
	preBoards = aBoard.preBoards;
}
