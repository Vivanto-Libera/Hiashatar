using Godot;
using System;

public partial class Piece : Node2D
{
    protected int row;
    protected int column;

    public void SetRowAndCol(int newRow, int newCol)
    {
        row = newRow;
        column = newCol;
    }
    public int GetPiecePosition() 
    {
        return row * 10 + column;
    }

    public void SetPiecePosition(Vector2 newPosition) 
    {
        Position = newPosition;
    }
    public void MoveToPosition(Vector2 newPosition) 
    {
        Tween tween = CreateTween();
        tween.TweenProperty(this, "position", newPosition, 0.5);
        tween.SetEase(Tween.EaseType.Out);
    }

    public void Capture() 
    {
        SetRowAndCol(-1, -1);
    }
    public bool isCaptured()
    {
        return row == -1;
    }
}
