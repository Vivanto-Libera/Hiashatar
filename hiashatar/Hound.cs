using Godot;
using static Hiashatar.PieceColor;

public partial class Hound : Piece
{
	bool isPromoted = false;
    public override void SetRowAndCol(int newRow, int newCol)
    {
        base.SetRowAndCol(newRow, newCol);
        if (!isPromoted)
        {
            if (row == 0 || row == 9)
            {
				Promote();
            }
        }
    }
	private void Promote() 
	{
		isPromoted = true;
		bool isWhite = color == WHITE;
		GetNode<TextureRect>("White").SetDeferred(TextureRect.PropertyName.Visible, false);
		GetNode<TextureRect>("Black").SetDeferred(TextureRect.PropertyName.Visible, false);
		GetNode<TextureRect>("WhitePromoted").SetDeferred(TextureRect.PropertyName.Visible, isWhite);
		GetNode<TextureRect>("BlackPromoted").SetDeferred(TextureRect.PropertyName.Visible, !isWhite);
	}
	public override void Reset()
	{
		base.Reset();
		isPromoted = false;
		bool isWhite = color == WHITE;
		GetNode<TextureRect>("White").SetDeferred(TextureRect.PropertyName.Visible, isWhite);
		GetNode<TextureRect>("Black").SetDeferred(TextureRect.PropertyName.Visible, !isWhite);
		GetNode<TextureRect>("WhitePromoted").SetDeferred(TextureRect.PropertyName.Visible, false);
		GetNode<TextureRect>("BlackPromoted").SetDeferred(TextureRect.PropertyName.Visible, false);
	}
}
