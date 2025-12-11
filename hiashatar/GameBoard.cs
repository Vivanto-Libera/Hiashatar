using Godot;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static Godot.OpenXRInterface;
using static Hiashatar.PieceColor;
using static Hiashatar.PieceType;
using static TorchSharp.torch;

namespace Hiashatar
{
    public class GameBoard
    {
        public PieceType[,] board = new PieceType[10, 10];
        public PieceColor turn;
        public Rule_Pieces whitePieces = new Rule_Pieces(WHITE);
        public Rule_Pieces blackPieces = new Rule_Pieces(BLACK);
        private int noProgress = 0;
        private List<PieceType[,]> preBoards = new List<PieceType[,]>();

        public List<int> LegalMoves()
        {
            List<int> moves = new List<int>();
            Rule_Pieces pieces = turn == WHITE ? whitePieces : blackPieces;
            Rule_Pieces oppsitePieces = turn == WHITE ? blackPieces : whitePieces;

            //Get the control Zone.
            HashSet<int> guardZone = new HashSet<int>();
            for (int i = 0; i < 2; i++)
            {
                if (!oppsitePieces.guards[i].IsCaptured())
                {
                    List<int> zone = whitePieces.guards[i].GetControlZone();
                    guardZone.UnionWith(zone);
                }
            }

            int[] fromPos = new int[2];

            //Khan's move
            fromPos = pieces.khan.GetPosition();
            for (int i = 0; i < 8; i++)
            {
                int newRow = fromPos[0] + Direction.QUEENLIKEROW[i];
                int newCol = fromPos[1] + Direction.QUEENLIKECOL[i];
                if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                {
                    continue;
                }
                if (ColorOfSquare(newRow, newCol) != turn)
                {
                    int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
                    if (!IsChecked(num))
                    {
                        moves.Add(num);
                    }
                }
            }
            
            //Lion's move
	        for (int i = 0; i < 11; i++)
	        {
		        if (i == 0 && !pieces.lion.IsCaptured())
		        {
			        fromPos = pieces.lion.GetPosition();
		        }
		        else if (i != 0)
		        {
			        if (pieces.hounds[i - 1].IsPromoted() && !pieces.hounds[i - 1].IsCaptured())
			        {
				        fromPos = pieces.hounds[i - 1].GetPosition();
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
				        newRow += Direction.QUEENLIKEROW[i];
				        newCol += Direction.QUEENLIKECOL[i];
				        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        moves.Add(num);
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Guards' move
	        foreach (Rule_Guard guard in pieces.guards)
	        {
		        if (guard.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = guard.GetPosition();
		        for (int j = 0; j < 8; j++)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        for (int k = 1; k < 3; k++)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (board[newRow, newCol] == EMPTY)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        moves.Add(num);
					        }
				        }
				        else if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        if (j % 2 == 1 && k == 1)
					        {
						        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
						        if (!IsChecked(num))
						        {
							        moves.Add(num);
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
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Camels' move
	        foreach (Rule_Camel camel in pieces.camels)
	        {
		        if (camel.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = camel.GetPosition();
		        for (int j = 1; j < 8; j += 2)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        while (true)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        moves.Add(num);
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Terges' move
	        foreach (Rule_Terge terge in pieces.terges)
	        {
		        if (terge.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = terge.GetPosition();
		        for (int j = 0; j < 8; j += 2)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        while (true)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        moves.Add(num);
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Horses' move
            foreach (Rule_Horse horse in pieces.horses)
            {
                if (horse.IsCaptured())
                {
                    continue;
                }
                fromPos = horse.GetPosition();
                for (int j = 0; j < 8; j++)
                {
                    int newRow = fromPos[0];
                    int newCol = fromPos[1];
                    newRow += Direction.HORSEMOVEROW[j];
                    newCol += Direction.HORSEMOVECOL[j];
                    if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                    {
                        continue;
                    }
                    if (ColorOfSquare(newRow, newCol) != turn)
                    {
                        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
                        if (!IsChecked(num))
                        {
                            moves.Add(num);
                        }
                    }
                }
            }

            //Hounds' move
			int houndDiection;
			if (turn == WHITE)
			{
				houndDiection = -1;
			}
			else
			{
				houndDiection = 1;
			}
			foreach (Rule_Hound hound in pieces.hounds)
			{
				if (hound.IsCaptured() || hound.IsPromoted())
				{
					continue;
				}
				fromPos = hound.GetPosition();
				int moveDistance = 1;
				if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
				{
					moveDistance = 3;
				}
				//Move Forward
				for (int i = 1; i <= moveDistance; i++)
				{
                    if (fromPos[0] + houndDiection * i < 0 || fromPos[0] + houndDiection * i > 9)
					{
						continue;
					}
					if (board[fromPos[0] + houndDiection * i, fromPos[1]] == EMPTY)
					{
						int num = Conversion.MoveToNum([fromPos[0], fromPos[1], (fromPos[0] + houndDiection * i), fromPos[1]]);
						if (!IsChecked(num))
						{
							moves.Add(num);
						}
					}
					else
					{
						break;
					}
					if (guardZone.Contains((fromPos[0] + houndDiection * i) * 10 + fromPos[1]))
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
					if (ColorOfSquare(newRow, newCol) != turn && board[newRow, newCol] != EMPTY)
					{
						int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
						if (!IsChecked(num))
						{
							moves.Add(num);
						}
					}
					//En passant
					if (board[newRow, newCol] == EMPTY)
					{
						if ((board[fromPos[0], newCol] == WHITEHOUND && turn == BLACK) || (board[fromPos[0], newCol] == BLACKHOUND && turn == WHITE))
						{
							if (((Rule_Hound)FindPiece(fromPos[0], newCol)).CanEnPassant())
							{
								int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
								if (!IsChecked(num))
								{
									moves.Add(num);
								}
							}
						}
					}
				}
			}

			return moves;
        }

        public bool HasLegalMoves() 
        {
            Rule_Pieces pieces = turn == WHITE ? whitePieces : blackPieces;
            Rule_Pieces oppsitePieces = turn == WHITE ? blackPieces : whitePieces;

            //Get the control Zone.
            HashSet<int> guardZone = new HashSet<int>();
            for (int i = 0; i < 2; i++)
            {
                if (!oppsitePieces.guards[i].IsCaptured())
                {
                    List<int> zone = whitePieces.guards[i].GetControlZone();
                    guardZone.UnionWith(zone);
                }
            }

            int[] fromPos = new int[2];

            //Khan's move
            fromPos = pieces.khan.GetPosition();
            for (int i = 0; i < 8; i++)
            {
                int newRow = fromPos[0] + Direction.QUEENLIKEROW[i];
                int newCol = fromPos[1] + Direction.QUEENLIKECOL[i];
                if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                {
                    continue;
                }
                if (ColorOfSquare(newRow, newCol) != turn)
                {
                    int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
                    if (!IsChecked(num))
                    {
                        return true;
                    }
                }
            }
            
            //Lion's move
	        for (int i = 0; i < 11; i++)
	        {
		        if (i == 0 && !pieces.lion.IsCaptured())
		        {
			        fromPos = pieces.lion.GetPosition();
		        }
		        else if (i != 0)
		        {
			        if (pieces.hounds[i - 1].IsPromoted() && !pieces.hounds[i - 1].IsCaptured())
			        {
				        fromPos = pieces.hounds[i - 1].GetPosition();
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
				        newRow += Direction.QUEENLIKEROW[i];
				        newCol += Direction.QUEENLIKECOL[i];
				        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        return true;
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Guards' move
	        foreach (Rule_Guard guard in pieces.guards)
	        {
		        if (guard.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = guard.GetPosition();
		        for (int j = 0; j < 8; j++)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        for (int k = 1; k < 3; k++)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (board[newRow, newCol] == EMPTY)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        return true;
					        }
				        }
				        else if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        if (j % 2 == 1 && k == 1)
					        {
						        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
						        if (!IsChecked(num))
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
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Camels' move
	        foreach (Rule_Camel camel in pieces.camels)
	        {
		        if (camel.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = camel.GetPosition();
		        for (int j = 1; j < 8; j += 2)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        while (true)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        return true;
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Terges' move
	        foreach (Rule_Terge terge in pieces.terges)
	        {
		        if (terge.IsCaptured())
		        {
			        continue;
		        }
		        fromPos = terge.GetPosition();
		        for (int j = 0; j < 8; j += 2)
		        {
			        int newRow = fromPos[0];
			        int newCol = fromPos[1];
			        while (true)
			        {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				        {
					        break;
				        }
				        if (ColorOfSquare(newRow, newCol) != turn)
				        {
					        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
					        if (!IsChecked(num))
					        {
						        return true;
					        }
					        if (board[newRow, newCol] != EMPTY)
					        {
						        break;
					        }
				        }
				        else
				        {
					        break;
				        }
				        if (guardZone.Contains(newRow * 10 + newCol))
				        {
					        break;
				        }
			        }
		        }
	        }

            //Horses' move
            foreach (Rule_Horse horse in pieces.horses)
            {
                if (horse.IsCaptured())
                {
                    continue;
                }
                fromPos = horse.GetPosition();
                for (int j = 0; j < 8; j++)
                {
                    int newRow = fromPos[0];
                    int newCol = fromPos[1];
                    newRow += Direction.HORSEMOVEROW[j];
                    newCol += Direction.HORSEMOVECOL[j];
                    if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                    {
                        continue;
                    }
                    if (ColorOfSquare(newRow, newCol) != turn)
                    {
                        int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
                        if (!IsChecked(num))
                        {
                            return true;
                        }
                    }
                }
            }

            //Hounds' move
			int houndDiection;
			if (turn == WHITE)
			{
				houndDiection = -1;
			}
			else
			{
				houndDiection = 1;
			}
			foreach (Rule_Hound hound in pieces.hounds)
			{
				if (hound.IsCaptured() || hound.IsPromoted())
				{
					continue;
				}
				fromPos = hound.GetPosition();
				int moveDistance = 1;
				if ((fromPos[0] == 8 && turn == WHITE) || (fromPos[0] == 1 && turn == BLACK))
				{
					moveDistance = 3;
				}
				//Move Forward
				for (int i = 1; i <= moveDistance; i++)
				{
                    if (fromPos[0] + houndDiection * i < 0 || fromPos[0] + houndDiection * i > 9)
					{
						continue;
					}
					if (board[fromPos[0] + houndDiection * i, fromPos[1]] == EMPTY)
					{
						int num = Conversion.MoveToNum([fromPos[0], fromPos[1], (fromPos[0] + houndDiection * i), fromPos[1]]);
						if (!IsChecked(num))
						{
							return true;
						}
					}
					else
					{
						break;
					}
					if (guardZone.Contains((fromPos[0] + houndDiection * i) * 10 + fromPos[1]))
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
					if (ColorOfSquare(newRow, newCol) != turn && board[newRow, newCol] != EMPTY)
					{
						int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
						if (!IsChecked(num))
						{
							return true;
						}
					}
					//En passant
					if (board[newRow, newCol] == EMPTY)
					{
						if ((board[fromPos[0], newCol] == WHITEHOUND && turn == BLACK) || (board[fromPos[0], newCol] == BLACKHOUND && turn == WHITE))
						{
							if (((Rule_Hound)FindPiece(fromPos[0], newCol)).CanEnPassant())
							{
								int num = Conversion.MoveToNum([fromPos[0], fromPos[1], newRow, newCol]);
								if (!IsChecked(num))
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

        public void ApplyMove(int moveIndex) 
        {
            noProgress++;
            if (preBoards.Count == 8)
            {
                preBoards.RemoveAt(0);
            }
            preBoards.Add(board);

            int[] move = Conversion.NumToMove(Conversion.indexToMove[moveIndex]);
            if (board[move[2], move[3]] != EMPTY)
            {
                FindPiece(move[2], move[3]).Capture();
                noProgress = 0;
            }
            if (board[move[0], move[1]] == WHITEHOUND || board[move[0], move[1]] == BLACKHOUND)
            {
                noProgress = 0;
                if (move[3] - move[1] != 0 && board[move[2], move[3]] == EMPTY)
                {
                    FindPiece(move[0], move[3]).Capture();
                    board[move[0], move[3]] = EMPTY;
                }
            }

            FindPiece(move[0], move[1]).Move(move[2], move[3]);
            board[move[2], move[3]] = board[move[0], move[1]];
            board[move[0], move[1]] = EMPTY;

            //Promote
            if ((board[move[2], move[3]] == BLACKHOUND || board[move[2], move[3]] == WHITEHOUND) && (move[2] == 9 || move[2] == 0))
            {
                board[move[2], move[3]] = turn == WHITE ? WHITELION : BLACKLION;
            }

            //Reset En Passant Right
            Rule_Pieces pieces = turn == WHITE ? blackPieces : whitePieces;
            for (int i = 0; i < 10; i++)
            {
                pieces.hounds[i].ResetEnPassant();
            }

            turn = turn == WHITE ? BLACK : WHITE;
        }

		public PieceColor IsTerminal() 
		{
            if (RepeatCount() == 2 || noProgress == 50)
            {
                return DRAW;
            }
            if (!HasLegalMoves())
            {
                if (IsChecked(-1))
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
            if (blackPieces.lion.IsCaptured() && whitePieces.lion.IsCaptured())
            {
                for (int i = 0; i < 2; i++)
                {
                    if (!blackPieces.terges[i].IsCaptured() || !whitePieces.terges[i].IsCaptured())
                    {
                        return NOTEND;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    if (!blackPieces.hounds[i].IsCaptured() || !whitePieces.hounds[i].IsCaptured())
                    {
                        return NOTEND;
                    }
                }
                PieceColor camelColor = DRAW;
                int materia = 0;
                for (int i = 0; i < 2; i++)
                {
                    if (materia > 1)
                    {
                        return NOTEND;
                    }
                    if (!blackPieces.horses[i].IsCaptured())
                    {
                        materia++;
                    }
                    if (!whitePieces.horses[i].IsCaptured())
                    {
                        materia++;
                    }
                    if (!blackPieces.guards[i].IsCaptured())
                    {
                        materia++;
                    }
                    if (!whitePieces.guards[i].IsCaptured())
                    {
                        materia++;
                    }
                    if (!blackPieces.camels[i].IsCaptured())
                    {
                        if (camelColor != DRAW)
                        {
                            if (camelColor != blackPieces.camels[i].GetSquareColor())
                            {
                                return NOTEND;
                            }
                        }
                        else
                        {
                            camelColor = blackPieces.camels[i].GetSquareColor();
                            materia++;
                        }
                    }
                    if (!whitePieces.camels[i].IsCaptured())
                    {
                        if (camelColor != DRAW)
                        {
                            if (camelColor != whitePieces.camels[i].GetSquareColor())
                            {
                                return NOTEND;
                            }
                        }
                        else
                        {
                            camelColor = whitePieces.camels[i].GetSquareColor();
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

        public Tensor ModelInput() 
        {
            Tensor input = zeros([1, 16, 10, 10]);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    switch (board[i, j])
                    {
                        case WHITEKHAN:
                            input[0, 0, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 7, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITELION:
                            input[0, 1, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 8, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITEGUARD:
                            input[0, 2, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 9, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITECAMEL:
                            input[0, 3, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 10, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITEHORSE:
                            input[0, 4, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 11, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITETERGE:
                            input[0, 5, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 12, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case WHITEHOUND:
                            input[0, 6, i, j] = turn == WHITE ? 1 : 0;
                            input[0, 13, i, j] = turn == WHITE ? 0 : 1;
                            break;
                        case BLACKKHAN:
                            input[0, 0, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 7, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKLION:
                            input[0, 1, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 8, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKGUARD:
                            input[0, 2, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 9, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKCAMEL:
                            input[0, 3, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 10, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKHORSE:
                            input[0, 4, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 11, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKTERGE:
                            input[0, 5, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 12, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        case BLACKHOUND:
                            input[0, 6, i, j] = turn == BLACK ? 1 : 0;
                            input[0, 13, i, j] = turn == BLACK ? 0 : 1;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (RepeatCount() == 1) 
            {
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < 10; j++) 
                    {
                        input[0, 14, i, j] = 1;
                    }
                }
            }

            float np = (float)1.0 * noProgress / 50;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    input[0, 15, i, j] = np;
                }
            }
            return input;
        }

        private int RepeatCount() 
        {
            int repeats = 0;
            int preCount = preBoards.Count;
            if (preCount - 4 >= 0)
            {
                if (preBoards[preCount - 4] == board)
                {
                    repeats++;
                }
                else
                {
                    return repeats;
                }
            }
            if (preCount - 8 >= 0)
            {
                if (preBoards[preCount - 8] == board)
                {
                    repeats++;
                }
                else
                {
                    return repeats;
                }
            }
            return repeats;
        }
        private bool IsChecked(int move)
        {
            PieceType[,] newBoard = new PieceType[10, 10];
            Array.Copy(board, newBoard, board.Length);
            if (move > 0)
            {
                int[] moveArray = Conversion.NumToMove(move);
                newBoard[moveArray[2], moveArray[3]] = newBoard[moveArray[0], moveArray[1]];
                newBoard[moveArray[0], moveArray[1]] = EMPTY;
                //Check en passant
                if (board[moveArray[0], moveArray[1]] == WHITEHOUND || board[moveArray[0], moveArray[1]] == BLACKHOUND)
                {
                    if (moveArray[3] - moveArray[1] != 0)
                    {
                        if (board[moveArray[2], moveArray[3]] == EMPTY)
                        {
                            newBoard[moveArray[0], moveArray[3]] = EMPTY;
                        }
                    }
                }
            }

            //Find the guard Zone
            HashSet<int> guardZone = new HashSet<int>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((newBoard[i, j] == WHITEGUARD && turn == WHITE)
                        || (newBoard[i, j] == BLACKGUARD && turn == BLACK))
                    {
                        List<int> zone = new Rule_Guard(i, j, turn).GetControlZone();
                        guardZone.UnionWith(zone);
                    }
                }

            }

            //Find the Khan
            int[] khanPos = { -1, -1 };
            for (int i = 0; i < 10; i++)
            {
                if (khanPos[0] != -1)
                {
                    break;
                }
                for (int j = 0; j < 10; j++)
                {
                    if ((newBoard[i, j] == WHITEKHAN && turn == WHITE)
                        || (newBoard[i, j] == BLACKKHAN && turn == BLACK))
                    {
                        khanPos[0] = i;
                        khanPos[1] = j;
                        break;
                    }
                }
            }
            //Check Khan is being chekced or not

            int newRow = khanPos[0] + (turn == WHITE ? -1 : 1);
            int newCol = khanPos[1] - 1;
            if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
            {
                if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKHOUND : WHITEHOUND))
                {
                    return true;
                }
            }
            newCol += 2;
            if (!(newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9))
            {
                if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKHOUND : WHITEHOUND))
                {
                    return true;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                newRow = khanPos[0] + Direction.HORSEMOVEROW[i];
                newCol = khanPos[1] + Direction.HORSEMOVECOL[i];
                if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                {
                    continue;
                }
                if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKHORSE : WHITEHORSE))
                {
                    return true;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                newRow = khanPos[0];
                newCol = khanPos[1];
                int distance = 0;

                while (true)
                {
                    newRow += Direction.QUEENLIKEROW[i];
                    newCol += Direction.QUEENLIKECOL[i];
                    distance++;
                    if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                    {
                        break;
                    }
                    if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKLION : WHITELION))
                    {
                        return true;
                    }
                    if (distance == 1)
                    {
                        if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKKHAN : WHITEKHAN))
                        {
                            return true;
                        }
                        if (i % 2 == 1)
                        {
                            if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKGUARD : WHITEGUARD))
                            {
                                return true;
                            }
                        }
                    }
                    if (i % 2 == 0)
                    {
                        if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKTERGE : WHITETERGE))
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (newBoard[newRow, newCol] == (turn == WHITE ? BLACKCAMEL : WHITECAMEL))
                        {
                            return true;
                        }
                    }
                    if (newBoard[newRow, newCol] != EMPTY)
                    {
                        break;
                    }
                    if (guardZone.Contains(newRow * 10 + newCol))
                    {
                        break;
                    }
                }
            }
            return false;
        }

        private PieceColor ColorOfSquare(int row, int col)
        {
            if (board[row, col] == EMPTY)
            {
                return DRAW;
            }
            switch (board[row, col])
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

		private Rule_Piece FindPiece(int row, int col) 
		{
            switch (board[row, col])
            {
                case WHITEKHAN:
                    return whitePieces.khan;
                case WHITELION:
                    if (whitePieces.lion.GetPosition()[0] == row && whitePieces.lion.GetPosition()[1] == col)
                    {
                        return whitePieces.lion;
                    }
                    else
                    {
                        foreach (Rule_Hound hound in whitePieces.hounds)
                        {
                            if (!hound.IsPromoted())
                            {
                                continue;
                            }
                            if (hound.GetPosition()[0] == row && hound.GetPosition()[1] == col)
                            {
                                return hound;
                            }
                        }
                    }
                    return null;
                case WHITEGUARD:
                    foreach (Rule_Guard guard in whitePieces.guards)
                    {
                        if (guard.GetPosition()[0] == row && guard.GetPosition()[1] == col)
                        {
                            return guard;
                        }
                    }
                    return null;
                case WHITECAMEL:
                    foreach (Rule_Camel camel in whitePieces.camels)
                    {
                        if (camel.GetPosition()[0] == row && camel.GetPosition()[1] == col)
                        {
                            return camel;
                        }
                    }
                    return null;
                case WHITEHORSE:
                    foreach (Rule_Horse horse in whitePieces.horses)
                    {
                        if (horse.GetPosition()[0] == row && horse.GetPosition()[1] == col)
                        {
                            return horse;
                        }
                    }
                    return null;
                case WHITETERGE:
                    foreach (Rule_Terge terge in whitePieces.terges)
                    {
                        if (terge.GetPosition()[0] == row && terge.GetPosition()[1] == col)
                        {
                            return terge;
                        }
                    }
                    return null;
                case WHITEHOUND:
                    foreach (Rule_Hound hound in whitePieces.hounds)
                    {
                        if (hound.IsPromoted())
                        {
                            continue;
                        }
                        if (hound.GetPosition()[0] == row && hound.GetPosition()[1] == col)
                        {
                            return hound;
                        }
                    }
                    return null;
                case BLACKKHAN:
                    return blackPieces.khan;
                case BLACKLION:
                    if (blackPieces.lion.GetPosition()[0] == row && blackPieces.lion.GetPosition()[1] == col)
                    {
                        return blackPieces.lion;
                    }
                    else
                    {
                        foreach (Rule_Hound hound in blackPieces.hounds)
                        {
                            if (!hound.IsPromoted())
                            {
                                continue;
                            }
                            if (hound.GetPosition()[0] == row && hound.GetPosition()[1] == col)
                            {
                                return hound;
                            }
                        }
                    }
                    return null;
                case BLACKGUARD:
                    foreach (Rule_Guard guard in blackPieces.guards)
                    {
                        if (guard.GetPosition()[0] == row && guard.GetPosition()[1] == col)
                        {
                            return guard;
                        }
                    }
                    return null;
                case BLACKCAMEL:
                    foreach (Rule_Camel camel in blackPieces.camels)
                    {
                        if (camel.GetPosition()[0] == row && camel.GetPosition()[1] == col)
                        {
                            return camel;
                        }
                    }
                    return null;
                case BLACKHORSE:
                    foreach (Rule_Horse horse in blackPieces.horses)
                    {
                        if (horse.GetPosition()[0] == row && horse.GetPosition()[1] == col)
                        {
                            return horse;
                        }
                    }
                    return null;
                case BLACKTERGE:
                    foreach (Rule_Terge terge in blackPieces.terges)
                    {
                        if (terge.GetPosition()[0] == row && terge.GetPosition()[1] == col)
                        {
                            return terge;
                        }
                    }
                    return null;
                case BLACKHOUND:
                    foreach (Rule_Hound hound in blackPieces.hounds)
                    {
                        if (hound.IsPromoted())
                        {
                            continue;
                        }
                        if (hound.GetPosition()[0] == row && hound.GetPosition()[1] == col)
                        {
                            return hound;
                        }
                    }
                    return null;
                default:
                    return null;
            }
        }

        public GameBoard() 
        {
            for (int i = 2; i < 8; i++)
            {
                for (int j = 0; j < 10; j++) 
                {
                    board[i, j] = EMPTY;
                }
            }
            board[0, 0] = board[0, 9] = BLACKTERGE;
            board[0, 1] = board[0, 8] = BLACKHORSE;
            board[0, 2] = board[0, 7] = BLACKCAMEL;
            board[0, 3] = board[0, 6] = BLACKGUARD;
            board[0, 4] = BLACKLION;
            board[0, 5] = BLACKKHAN;

            board[9, 0] = board[9, 9] = WHITETERGE;
            board[9, 1] = board[9, 8] = WHITEHORSE;
            board[9, 2] = board[9, 7] = WHITECAMEL;
            board[9, 3] = board[9, 6] = WHITEGUARD;
            board[9, 5] = WHITELION;
            board[9, 4] = WHITEKHAN;

            for (int i = 0; i < 10; i++)
            {
                board[1, i] = BLACKHOUND;
                board[8, i] = WHITEHOUND;
            }

            turn = WHITE;
            noProgress = 0;
        }
        public GameBoard(GameBoard aBoard) 
        {
            Array.Copy(aBoard.board, board, board.Length);
            preBoards = [.. aBoard.preBoards];
            whitePieces = new Rule_Pieces(aBoard.whitePieces);
            blackPieces = new Rule_Pieces(aBoard.blackPieces);
            noProgress = aBoard.noProgress;
            turn = aBoard.turn;
        }
    }
}
