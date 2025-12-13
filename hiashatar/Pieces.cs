using System;
using System.Collections.Generic;
using static Hiashatar.PieceColor;

namespace Hiashatar
{
    public class Pieces
    {
        public Piece[] terges = new Piece[2];
        public Piece[] camels = new Piece[2];
        public Piece[] horses = new Piece[2];
        public Piece[] guards = new Piece[2];
        public Piece khan = null;
        public Piece lion = null;
        public Hound[] hounds = new Hound[10];

        public List<Piece> GetAllPieces()
        {
            List<Piece> pieces =
            [
                .. terges,
                .. camels,
                ..horses,
                ..guards,
                khan,
                lion,
                .. hounds,
            ];
            return pieces;
        }

        public void SetToBlack()
        {
            List<Piece> pieces = GetAllPieces();
            foreach (Piece piece in pieces)
            {
                piece.SetColor(BLACK);
            }
        }

        public void Reset() 
        {
            List<Piece> pieces = GetAllPieces();
            foreach (Piece piece in pieces) 
            {
                piece.Reset();
            }
        }
        public void SetToBoard(PieceColor color) 
        {
            if (color == BLACK) 
            {
                terges[0].SetRowAndCol(0, 0);
                terges[1].SetRowAndCol(0, 9);
                horses[0].SetRowAndCol(0, 1);
                horses[1].SetRowAndCol(0, 8);
                camels[0].SetRowAndCol(0, 2);
                camels[1].SetRowAndCol(0, 7);
                guards[0].SetRowAndCol(0, 3);
                guards[1].SetRowAndCol(0, 6);
                khan.SetRowAndCol(0, 5);
                lion.SetRowAndCol(0, 4);
                for(int i = 0; i < 10; i++) 
                {
                    hounds[i].SetRowAndCol(1, i);
                }
            }
            else 
            {
                terges[0].SetRowAndCol(9, 0);
                terges[1].SetRowAndCol(9, 9);
                horses[0].SetRowAndCol(9, 1);
                horses[1].SetRowAndCol(9, 8);
                camels[0].SetRowAndCol(9, 2);
                camels[1].SetRowAndCol(9, 7);
                guards[0].SetRowAndCol(9, 3);
                guards[1].SetRowAndCol(9, 6);
                khan.SetRowAndCol(9, 4);
                lion.SetRowAndCol(9, 5);
                for (int i = 0; i < 10; i++)
                {
                    hounds[i].SetRowAndCol(8, i);
                }
            }
        }

        public void SetAllPiecesButtonDisable(bool disable) 
        {
            List<Piece> pieces = GetAllPieces();
            foreach (Piece piece in pieces) 
            {
                piece.SetButtonDisable(disable);
            }
        }

        public Pieces() { }
    }
}
