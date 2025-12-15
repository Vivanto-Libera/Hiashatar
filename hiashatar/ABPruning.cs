using Godot;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading;
using static Tensorboard.CostGraphDef.Types;

namespace Hiashatar
{
    public class ABPruning(int depth)
    {
        private int depth = depth;
        private readonly int[,] HOUNDWHITE = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                         { 30, 30, 35, 45, 50, 50, 45, 35, 30, 30 },
                                                         { 20, 20, 25, 35, 40, 40, 35, 25, 20, 20 },
                                                         { 15, 15, 20, 28, 32, 32, 28, 20, 15, 15 },
                                                         { 10, 10, 15, 22, 25, 25, 22, 15, 10, 10 },
                                                         { 8, 8, 12, 18, 22, 22, 18, 12, 8, 8 },
                                                         { 5, 5, 8, 12, 15, 15, 12, 8, 5, 5 },
                                                         { 3, 3, 5, 8, 10, 10, 8, 5, 3, 3 },
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                        };
        private readonly int[,] HOUNDBLACK = new int[,] {{ 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                         { 3, 3, 5, 8, 10, 10, 8, 5, 3, 3 },
                                                         { 5, 5, 8, 12, 15, 15, 12, 8, 5, 5 },
                                                         { 8, 8, 12, 18, 22, 22, 18, 12, 8, 8 },
                                                         { 10, 10, 15, 22, 25, 25, 22, 15, 10, 10 },
                                                         { 15, 15, 20, 28, 32, 32, 28, 20, 15, 15 },
                                                         { 20, 20, 25, 35, 40, 40, 35, 25, 20, 20 },
                                                         { 30, 30, 35, 45, 50, 50, 45, 35, 30, 30 },
                                                         { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                                                        };
        private readonly int[,] HORSE = new int[,] { { -50, -45, -40, -40, -20, -20, -30, -40, -45, -50 },
                                                     { -45, -30, -15, -5, 0, 0, -5, -15, -30, -45 },
                                                     { -40, -15, 5, 15, 20, 20, 15, 5, -15, -40 },
                                                     { -30, -5, 15, 25, 30, 30, 25, 15, -5, -30 },
                                                     { -20, 0, 20, 30, 35, 35, 30, 20, 0, -20 },
                                                     { -20, 0, 20, 30, 35, 35, 30, 20, 0, -20 },
                                                     { -30, -5, 15, 25, 30, 30, 25, 15, -5, -30 },
                                                     { -40, -15, 5, 15, 20, 20, 15, 5, -15, -40 },
                                                     { -45, -30, -15, -5, 0, 0, -5, -15, -30, -45 },
                                                     { -50, -45, -40, -40, -20, -20, -30, -40, -45, -50 }
                                                   };
        private readonly int[,] CAMEL = new int[,] { { -20, -15, -10, -5, -5, -5, -5, -10, -15, -20},
                                                     { -15, 0, 5, 10, 15, 15, 10, 5, 0, -15 },
                                                     { -10, 5, 15, 20, 25, 25, 20, 15, 5, -10 },
                                                     { -5, 10, 20, 30, 35, 35, 30, 20, 10, -5 },
                                                     { -5, 15, 25, 35, 40, 40, 35, 25, 15, -5 },
                                                     { -5, 15, 25, 35, 40, 40, 35, 25, 15, -5 },
                                                     { -5, 10, 20, 30, 35, 35, 30, 20, 10, -5 },
                                                     { -10, 5, 15, 20, 25, 25, 20, 15, 5, -10 },
                                                     { -15, 0, 5, 10, 15, 15, 10, 5, 0, -15 },
                                                     { -20, -15, -10, -5, -5, -5, -5, -10, -15, -20}
                                                   };
        private readonly int[,] LION = new int[,] {{ -25, -20, -15, -15, -15, -15, -15, -15, -20, -25},
                                                   { -20, -10, -5, -5, -5, -5, -5, -5, -10, -20},
                                                   { -15, -5, 0, 0, 0, 0, 0, 0, -5, -15 },
                                                   { -15, -5, 0, 5, 8, 8, 5, 0, -5, -15 },
                                                   { -15, -5, 0, 8, 12, 12, 8, 0, -5, -15},
                                                   { -15, -5, 0, 8, 12, 12, 8, 0, -5, -15},
                                                   { -15, -5, 0, 5, 8, 8, 5, 0, -5, -15 },
                                                   { -15, -5, 0, 0, 0, 0, 0, 0, -5, -15 },
                                                   { -20, -10, -5, -5, -5, -5, -5, -5, -10, -20},
                                                   { -25, -20, -15, -15, -15, -15, -15, -15, -20, -25}
                                                  };
        private readonly int[,] KHANOPEN = new int[,] { { 30, 25, 20, 10, 0, 0, 10, 20, 25, 30 },
                                                        { 20, 15, 10, 5, -5, -5, 5, 10, 15, 20},
                                                        { 10, 5, 0, -5, -10, -10, -5, 0, 5, 10 },
                                                        { 0, -5, -10, -15, -20, -20, -15, -10, -5, 0 },
                                                        { -5, -10, -15, -20, -25, -25, -20, -15, -10, -5 },
                                                        { -5, -10, -15, -20, -25, -25, -20, -15, -10, -5 },
                                                        { 0, -5, -10, -15, -20, -20, -15, -10, -5, 0 },
                                                        { 10, 5, 0, -5, -10, -10, -5, 0, 5, 10 },
                                                        { 20, 15, 10, 5, -5, -5, 5, 10, 15, 20},
                                                        { 30, 25, 20, 10, 0, 0, 10, 20, 25, 30 }
                                                      };
        private readonly int[,] KHANMIDDLE = new int[,] { { 25, 20, 15, 5, -10, -10, 5, 15, 20, 25 },
                                                        { 15, 10, 5, 0, -15, -15, 0, 5, 10, 15},
                                                        { 5, 0, -5, 0, -15, -15, 0, -5, 0, 5 },
                                                        { -5, -10, -15, -20, -25, -25, -20, -15, 0, -5 },
                                                        { -10, -15, -20, -25, -30, -30, -25, -20, -15, -10 },
                                                        { -10, -15, -20, -25, -30, -30, -25, -20, -15, -10 },
                                                        { -5, -10, -15, -20, -25, -25, -20, -15, 0, -5 },
                                                        { 5, 0, -5, 0, -15, -15, 0, -5, 0, 5 },
                                                        { 15, 10, 5, 0, -15, -15, 0, 5, 10, 15},
                                                        { 25, 20, 15, 5, -10, -10, 5, 15, 20, 25 }
                                                      };
        private readonly int[,] KHANEND = new int[,] { { -15, -10, -5, 0, 5, 5, 0, -5, -10, -15 },
                                                        { -10, -5, 0, 5, 10, 10, 5, 0, 5, -10},
                                                        { -5, 0, 5, 10, 15, 15, 10, 5, 0, -5 },
                                                        { 0, 5, 10, 15, 20, 20, 15, 10, 5, 0 },
                                                        { 5, 10, 15, 20, 25, 25, 20, 15, 10, 5 },
                                                        { 5, 10, 15, 20, 25, 25, 20, 15, 10, 5 },
                                                        { 0, 5, 10, 15, 20, 20, 15, 10, 5, 0 },
                                                        { -5, 0, 5, 10, 15, 15, 10, 5, 0, -5 },
                                                        { -10, -5, 0, 5, 10, 10, 5, 0, 5, -10},
                                                        { -15, -10, -5, 0, 5, 5, 0, -5, -10, -15 }
                                                      };

        private int selectedMove = 0;
        private PieceColor botColor;

        public int SelectMove(GameBoard board, CancellationToken token) 
        {
            botColor = board.turn;
            MaxValue(-1000000000, 1000000000, board, 0, token);
            if (token.IsCancellationRequested)
            {
                return 0;
            }
            return selectedMove;
        }

        private int Evaluate(GameBoard board) 
        {
            int score = 0;
            int materials = 0;
            //Find Open Line
            bool[] isOpen = { true, true, true, true, true, true, true, true, true, true };

            for (int i = 0; i < 10; i++)
            {
                if (!board.whitePieces.hounds[i].IsCaptured())
                {
                    if (board.whitePieces.hounds[i].IsPromoted())
                    {
                        int[] position = board.whitePieces.hounds[i].GetPosition();
                        score += LION[position[0], position[1]];
                        score += 1000;
                    }
                    else
                    {
                        score += 100;
                        materials += 100;
                        int[] position = board.whitePieces.hounds[i].GetPosition();
                        isOpen[position[1]] = false;
                        score += HOUNDWHITE[position[0], position[1]];
                        if (position[1] == 0 || position[1] == 9)
                        {
                            score -= 25;
                        }

                        //Passed Hound
                        bool passed = true;
                        for (int j = 0; j < 10; j++)
                        {
                            if (board.blackPieces.hounds[j].IsCaptured() || board.blackPieces.hounds[j].IsPromoted())
                            {
                                continue;
                            }
                            int[] houndPosition = board.blackPieces.hounds[j].GetPosition();
                            if (houndPosition[1] == position[1] || houndPosition[1] == position[1] - 1 || houndPosition[1] == position[1] + 1)
                            {
                                if (houndPosition[0] < position[0])
                                {
                                    passed = false;
                                    break;
                                }
                            }
                        }
                        if (passed)
                        {
                            score += 40;
                        }

                        bool isolated = true;
                        bool doubled = false;
                        for (int j = 0; j < 10; j++)
                        {
                            if (board.whitePieces.hounds[j].GetPosition()[1] == position[1] - 1 || board.whitePieces.hounds[j].GetPosition()[1] == position[1] + 1)
                            {
                                isolated = false;
                            }
                            if (board.whitePieces.hounds[j].GetPosition()[1] == position[1])
                            {
                                doubled = true;
                            }
                            if (doubled && !isolated)
                            {
                                break;
                            }
                        }
                        if (doubled)
                        {
                            score -= 10;
                        }
                        if (isolated)
                        {
                            score -= 20;
                        }
                    }
                }
                if (!board.blackPieces.hounds[i].IsCaptured())
                {
                    if (board.blackPieces.hounds[i].IsPromoted())
                    {
                        int[] position = board.blackPieces.hounds[i].GetPosition();
                        score -= LION[position[0], position[1]];
                        score -= 1000;
                    }
                    else
                    {
                        score -= 100;
                        materials += 100;
                        int[] position = board.blackPieces.hounds[i].GetPosition();
                        isOpen[position[1]] = false;
                        score -= HOUNDBLACK[position[0], position[1]];
                        if (position[1] == 0 || position[1] == 9)
                        {
                            score += 25;
                        }

                        bool passed = true;
                        for (int j = 0; j < 10; j++)
                        {
                            if (board.whitePieces.hounds[j].IsCaptured() || board.whitePieces.hounds[j].IsPromoted())
                            {
                                continue;
                            }
                            int[] houndPosition = board.whitePieces.hounds[j].GetPosition();
                            if (houndPosition[1] == position[1] || houndPosition[1] == position[1] - 1 || houndPosition[1] == position[1] + 1)
                            {
                                if (houndPosition[0] > position[0])
                                {
                                    passed = false;
                                    break;
                                }
                            }
                        }
                        if (passed)
                        {
                            score -= 40;
                        }

                        bool isolated = true;
                        bool doubled = false;
                        for (int j = 0; j < 10; j++)
                        {
                            if (board.blackPieces.hounds[j].GetPosition()[1] == position[1] - 1 || board.blackPieces.hounds[j].GetPosition()[1] == position[1] + 1)
                            {
                                isolated = false;
                            }
                            if (board.blackPieces.hounds[j].GetPosition()[1] == position[1])
                            {
                                doubled = true;
                            }
                            if (doubled && !isolated)
                            {
                                break;
                            }
                        }
                        if (doubled)
                        {
                            score += 10;
                        }
                        if (isolated)
                        {
                            score += 20;
                        }
                    }
                }
            }


            if (!board.whitePieces.lion.IsCaptured()) 
            {
                int[] position = board.whitePieces.lion.GetPosition();
                if (isOpen[position[1]]) 
                {
                    score += 15;
                }
                score += LION[position[0], position[1]];
                score += 1000;
                materials += 1000;
            }
            if (!board.blackPieces.lion.IsCaptured())
            {
                int[] position = board.blackPieces.lion.GetPosition();
                if (isOpen[position[1]])
                {
                    score -= 15;
                }
                score -= LION[position[0], position[1]];
                score -= 1000;
                materials += 1000;
            }
            for(int i = 0; i < 10; i++) 
            {
                if (board.whitePieces.hounds[i].IsPromoted() && !board.whitePieces.hounds[i].IsCaptured()) 
                {
                    if (isOpen[board.whitePieces.hounds[i].GetPosition()[1]]) 
                    {
                        score += 15;
                    }
                }
                if (board.blackPieces.hounds[i].IsPromoted() && !board.blackPieces.hounds[i].IsCaptured())
                {
                    if (isOpen[board.blackPieces.hounds[i].GetPosition()[1]])
                    {
                        score -= 15;
                    }
                }
            }
            int whiteCamelCount = 0;
            int blackCamelCount = 0;
            for (int i = 0; i < 2; i++) 
            {
                if (!board.whitePieces.terges[i].IsCaptured())
                {
                    int tergeRow = board.whitePieces.terges[i].GetPosition()[0];
                    switch (tergeRow) 
                    {
                        case 0:
                            score += 20;
                            break;
                        case 1:
                            score += 30;
                            break;
                        case 2:
                            score += 25;
                            break;
                        case 3:
                            score += 20;
                            break;
                        case 4:
                            score += 15;
                            break;
                        case 5:
                            score += 10;
                            break;
                        case 6:
                            score += 5;
                            break;
                        case 7:
                            break;
                        case 8:
                            break;
                        case 9:
                            score -= 5;
                            break;
                    }
                    if (isOpen[board.whitePieces.terges[i].GetPosition()[1]])
                    {
                        score += 25;
                    }
                    score += 525;
                    materials += 525;
                }
                if (!board.blackPieces.terges[i].IsCaptured())
                {
                    int tergeRow = board.blackPieces.terges[i].GetPosition()[0];
                    switch (tergeRow)
                    {
                        case 9:
                            score -= 20;
                            break;
                        case 8:
                            score -= 30;
                            break;
                        case 7:
                            score -= 25;
                            break;
                        case 6:
                            score -= 20;
                            break;
                        case 5:
                            score -= 15;
                            break;
                        case 4:
                            score -= 10;
                            break;
                        case 3:
                            score -= 5;
                            break;
                        case 2:
                            break;
                        case 1:
                            break;
                        case 0:
                            score += 5;
                            break;
                    }
                    if (isOpen[board.blackPieces.terges[i].GetPosition()[1]])
                    {
                        score -= 25;
                    }
                    score -= 525;
                    materials += 525;
                }
                if (!board.whitePieces.horses[i].IsCaptured())
                {
                    int[] position = board.whitePieces.horses[i].GetPosition();
                    score += HORSE[position[0], position[1]];
                    score += 350;
                    materials += 350;
                }
                if (!board.blackPieces.horses[i].IsCaptured())
                {
                    int[] position = board.blackPieces.horses[i].GetPosition();
                    score -= HORSE[position[0], position[1]];
                    score -= 350;
                    materials += 350;
                }
                if (!board.whitePieces.camels[i].IsCaptured())
                {
                    int[] position = board.whitePieces.camels[i].GetPosition();
                    score += CAMEL[position[0], position[1]];
                    score += 350;
                    materials += 350;
                    whiteCamelCount++;
                }
                if (!board.blackPieces.camels[i].IsCaptured())
                {
                    int[] position = board.blackPieces.camels[i].GetPosition();
                    score -= CAMEL[position[0], position[1]];
                    score -= 350;
                    materials += 350;
                    blackCamelCount++;
                }
                if (!board.whitePieces.guards[i].IsCaptured())
                {
                    int[] position = board.whitePieces.guards[i].GetPosition();
                    if (position[0] == 0 || position[0] == 9) 
                    {
                        score -= 20;
                    }
                    if (position[1] == 0 || position[1] == 9)
                    {
                        score -= 20;
                    }
                    int[] khanPosition = board.whitePieces.khan.GetPosition();
                    int distance = Math.Abs(position[0] - khanPosition[0]) + Math.Abs(position[1] - khanPosition[1]);
                    score -= 10 * distance;
                    score += 725;
                    materials += 725;
                }
                if (!board.blackPieces.guards[i].IsCaptured())
                {
                    int[] position = board.blackPieces.guards[i].GetPosition();
                    if (position[0] == 0 || position[0] == 9)
                    {
                        score += 20;
                    }
                    if (position[1] == 0 || position[1] == 9)
                    {
                        score += 20;
                    }
                    int[] khanPosition = board.blackPieces.khan.GetPosition();
                    int distance = Math.Abs(position[0] - khanPosition[0]) + Math.Abs(position[1] - khanPosition[1]);
                    score += 10 * distance;
                    score -= 725;
                }
            }
            if (whiteCamelCount == 2) 
            {
                score += 50;
            }
            if (blackCamelCount == 2) 
            {
                score -= 50;
            }

            if (!board.whitePieces.terges[0].IsCaptured() && !board.whitePieces.terges[1].IsCaptured()) 
            {
                int[] terge = board.whitePieces.terges[0].GetPosition();
                for (int j = 0; j < 8; j += 2)
                {
                    int newRow = terge[0];
                    int newCol = terge[1];
                    while (true)
                    {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                        {
                            break;
                        }
                        if (board.board[newRow, newCol] == PieceType.EMPTY)
                        {
                        score += 2;
                        }
                        else if (board.board[newRow, newCol] == PieceType.WHITETERGE)
                        {
                            score += 15;
                            break; ;
                        }
                        else 
                        {
                            break;
                        }
                    }
                }
            }
            if (!board.blackPieces.terges[0].IsCaptured() && !board.blackPieces.terges[1].IsCaptured())
            {
                int[] terge = board.blackPieces.terges[0].GetPosition();
                for (int j = 0; j < 8; j += 2)
                {
                    int newRow = terge[0];
                    int newCol = terge[1];
                    while (true)
                    {
                        newRow += Direction.QUEENLIKEROW[j];
                        newCol += Direction.QUEENLIKECOL[j];
                        if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                        {
                            break;
                        }
                        if (board.board[newRow, newCol] == PieceType.EMPTY)
                        {
                            score -= 2;
                        }
                        else if (board.board[newRow, newCol] == PieceType.BLACKTERGE)
                        {
                            score -= 15;
                            break; ;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }

            for (int i = 0; i < 8; i++)
            {
                int newRow = board.whitePieces.lion.GetPosition()[0];
                int newCol = board.whitePieces.lion.GetPosition()[1];
                while (true)
                {
                    newRow += Direction.QUEENLIKEROW[i];
                    newCol += Direction.QUEENLIKECOL[i];
                    if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                    {
                        break;
                    }
                    if (board.board[newRow, newCol] == PieceType.EMPTY) 
                    {
                        score += 3;
                    }
                    else if (board.board[newRow, newCol] == PieceType.WHITETERGE && i % 2 == 0) 
                    {
                        score += 30;
                        break;
                    }
                    else if (board.board[newRow, newCol] == PieceType.WHITECAMEL && i % 2 == 1)
                    {
                        score += 25;
                        break;
                    }
                    else if (board.board[newRow, newCol] == PieceType.WHITELION)
                    {
                        score += 40;
                        break;
                    }
                    else 
                    {
                        break;
                    }
                }
            }
            for (int i = 0; i < 8; i++)
            {
                int newRow = board.blackPieces.lion.GetPosition()[0];
                int newCol = board.blackPieces.lion.GetPosition()[1];
                while (true)
                {
                    newRow += Direction.QUEENLIKEROW[i];
                    newCol += Direction.QUEENLIKECOL[i];
                    if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
                    {
                        break;
                    }
                    if (board.board[newRow, newCol] == PieceType.EMPTY)
                    {
                        score -= 3;
                    }
                    else if (board.board[newRow, newCol] == PieceType.BLACKTERGE && i % 2 == 0)
                    {
                        score -= 30;
                        break;
                    }
                    else if (board.board[newRow, newCol] == PieceType.BLACKCAMEL && i % 2 == 1)
                    {
                        score -= 25;
                        break;
                    }
                    else if (board.board[newRow, newCol] == PieceType.BLACKLION)
                    {
                        score -= 40;
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            if (materials > 7000) 
            {
                score += KHANOPEN[board.whitePieces.khan.GetPosition()[0], board.whitePieces.khan.GetPosition()[1]];
                score -= KHANOPEN[board.blackPieces.khan.GetPosition()[0], board.blackPieces.khan.GetPosition()[1]];
            }
            else if(materials > 4000) 
            {
                score += KHANMIDDLE[board.whitePieces.khan.GetPosition()[0], board.whitePieces.khan.GetPosition()[1]];
                score -= KHANMIDDLE[board.blackPieces.khan.GetPosition()[0], board.blackPieces.khan.GetPosition()[1]];
            }
            else 
            {
                score += KHANEND[board.whitePieces.khan.GetPosition()[0], board.whitePieces.khan.GetPosition()[1]];
                score -= KHANEND[board.blackPieces.khan.GetPosition()[0], board.blackPieces.khan.GetPosition()[1]];
            }
            return score;
        }

        private int MaxValue(int alpha, int beta, GameBoard board, int dep, CancellationToken token) 
        {
            if (token.IsCancellationRequested)
            {
                return 0;
            }
            PieceColor winner = board.IsTerminal();
            if (winner != PieceColor.NOTEND) 
            {
                switch (winner) 
                {
                    case PieceColor.WHITE:
                        return botColor == PieceColor.WHITE ? 100000000 : -100000000;
                    case PieceColor.BLACK:
                        return botColor == PieceColor.BLACK ? 100000000 : -100000000;
                    default:
                        return 0;
                }
            }
            if (dep == depth) 
            {
                int value = Evaluate(board);
                return botColor == PieceColor.WHITE ? value : -value;
            }
            List<int> legalMoves = board.LegalMoves();
            int maxValue = -1000000000;
            foreach (int move in legalMoves) 
            {
                int moveIndex = Conversion.MoveToIndex(move);
                if (token.IsCancellationRequested)
                {
                    return 0;
                }
                GameBoard newBoard = new(board);
                newBoard.ApplyMove(moveIndex);
                int newValue = MinValue(alpha, beta, newBoard, dep + 1, token);
                if (dep == 0)
                {
                    if (newValue > maxValue)
                    {
                        selectedMove = moveIndex;
                    }
                    /*else if(newValue == maxValue) 
                    {
                        if (GD.Randi() % 2 == 0) 
                        {
                            selectedMove = moveIndex;
                        }
                    }*/
                }
                if (newValue >= beta)
                {
                    return newValue;
                }
                maxValue = newValue > maxValue ? newValue : maxValue;
                alpha = maxValue > alpha ? maxValue : alpha;
            }

            return maxValue;
        }
        private int MinValue(int alpha, int beta, GameBoard board, int dep, CancellationToken token) 
        {
            if (token.IsCancellationRequested)
            {
                return 0;
            }
            PieceColor winner = board.IsTerminal();
            if (winner != PieceColor.NOTEND)
            {
                switch (winner)
                {
                    case PieceColor.WHITE:
                        return botColor == PieceColor.WHITE ? 100000000 : -100000000;
                    case PieceColor.BLACK:
                        return botColor == PieceColor.BLACK ? 100000000 : -100000000;
                    default:
                        return 0;
                }
            }
            if (dep == depth)
            {
                int value = Evaluate(board);
                return botColor == PieceColor.WHITE ? value : -value;
            }
            List<int> legalMoves = board.LegalMoves();
            int minValue = 1000000000;
            foreach (int move in legalMoves)
            {
                int moveIndex = Conversion.MoveToIndex(move);
                if (token.IsCancellationRequested)
                {
                    return 0;
                }
                GameBoard newBoard = new(board);
                newBoard.ApplyMove(moveIndex);
                int newValue = MaxValue(alpha, beta, newBoard, dep + 1, token);
                if (newValue <= alpha)
                {
                    return newValue;
                }
                minValue = newValue < minValue ? newValue : minValue;
                beta = minValue < beta ? minValue : beta;
            }

            return minValue;
        }
    }
}
