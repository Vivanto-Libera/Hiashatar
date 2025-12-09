using Godot;
using Hiashatar;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Horse:Piece
{
    public override void SetColor(PieceColor newColor)
    {
        base.SetColor(newColor);
        bool isWhite = color == PieceColor.WHITE;
        GetNode<TextureRect>("WhiteHorse").SetDeferred(TextureRect.PropertyName.Visible, isWhite);
        GetNode<TextureRect>("BlackHorse").SetDeferred(TextureRect.PropertyName.Visible, !isWhite);
    }
}
