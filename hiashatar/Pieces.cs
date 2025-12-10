using System;
using static Hiashatar.PieceColor;

namespace Hiashatar
{
    public class Pieces
    {
        public Piece[] terges = new Piece[2];
        public Camel[] camels = new Camel[2];
        public Piece[] horses = new Piece[2];
        public Guard[] guards = new Guard[2];
        public Piece khan = null;
        public Piece lion = null;
        public Hound[] hounds = new Hound[10];

        public void SetToBlack() 
        {
            terges[0].SetColor(BLACK);
            terges[1].SetColor(BLACK);
            camels[0].SetColor(BLACK);
            camels[1].SetColor(BLACK);
            horses[0].SetColor(BLACK);
            horses[1].SetColor(BLACK);
            guards[0].SetColor(BLACK);
            guards[1].SetColor(BLACK);
            khan.SetColor(BLACK);
            lion.SetColor(BLACK);
            foreach (Hound hound in hounds) 
            {
                hound.SetColor(BLACK);
            }
        }

        public Pieces() { }
    }
}
