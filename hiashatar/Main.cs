using Godot;
using Hiashatar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using static Hiashatar.GameState;

public partial class Main : Node
{
	private Pieces whitePieces = new();
	private Pieces blackPieces = new();
	private Marker2D[] markers = new Marker2D[100];
	private Board board;
	private bool inverted = false;
	private bool needFlip = false;
	private GameState state = NOTSTARTED;
	private Playing playing;

	private Vector2 GetMarkerPositon(int number) 
	{
		if (number == -1) 
		{
			return GetNode<Node>("Markers").GetNode<Marker2D>("CapturedMarker").Position;
		}
		else 
		{
			return needFlip ? markers[99 - number].Position : markers[number].Position;
		}
	}

	private void SetPiecePosition(Piece piece)  
	{
		piece.SetPiecePosition(GetMarkerPositon(piece.GetPiecePosition()));
	}

	private void SetAllPiecesPosition() 
	{
		List<Piece> pieces = whitePieces.GetAllPieces();
		foreach (Piece piece in pieces)
		{
			SetPiecePosition(piece);
		}
		pieces = blackPieces.GetAllPieces();
		foreach (Piece piece in pieces)
		{
			SetPiecePosition(piece);
		}
	}

	private void ResetPieces() 
	{
		whitePieces.Reset();
		blackPieces.Reset();
		SetAllPiecesPosition();
	}
	private void SetPiecesToBoard() 
	{
		whitePieces.SetToBoard(PieceColor.WHITE);
		blackPieces.SetToBoard(PieceColor.BLACK);
		SetAllPiecesPosition();
	}

	private void InitialPieces() 
	{
		Node whitePiecesNode = GetNode<Node>("WhitePieces");
		Node blackPiecesNode = GetNode<Node>("BlackPieces");

		whitePieces.terges[0] = whitePiecesNode.GetNode<Piece>("Terge0");
		blackPieces.terges[0] = blackPiecesNode.GetNode<Piece>("Terge0");
		whitePieces.terges[1] = whitePiecesNode.GetNode<Piece>("Terge1");
		blackPieces.terges[1] = blackPiecesNode.GetNode<Piece>("Terge1");

		whitePieces.camels[0] = whitePiecesNode.GetNode<Piece>("Camel0");
		blackPieces.camels[0] = blackPiecesNode.GetNode<Piece>("Camel0");
		whitePieces.camels[1] = whitePiecesNode.GetNode<Piece>("Camel1");
		blackPieces.camels[1] = blackPiecesNode.GetNode<Piece>("Camel1");

		whitePieces.horses[0] = whitePiecesNode.GetNode<Piece>("Horse0");
		blackPieces.horses[0] = blackPiecesNode.GetNode<Piece>("Horse0");
		whitePieces.horses[1] = whitePiecesNode.GetNode<Piece>("Horse1");
		blackPieces.horses[1] = blackPiecesNode.GetNode<Piece>("Horse1");

		whitePieces.guards[0] = whitePiecesNode.GetNode<Piece>("Guard0");
		blackPieces.guards[0] = blackPiecesNode.GetNode<Piece>("Guard0");
		whitePieces.guards[1] = whitePiecesNode.GetNode<Piece>("Guard1");
		blackPieces.guards[1] = blackPiecesNode.GetNode<Piece>("Guard1");

		whitePieces.lion = whitePiecesNode.GetNode<Piece>("Lion");
		blackPieces.lion = blackPiecesNode.GetNode<Piece>("Lion");

		whitePieces.khan = whitePiecesNode.GetNode<Piece>("Khan");
		blackPieces.khan = blackPiecesNode.GetNode<Piece>("Khan");

		for (int i = 0; i < 10; i++)
		{
			whitePieces.hounds[i] = whitePiecesNode.GetNode<Hound>("Hound" + i.ToString());
			blackPieces.hounds[i] = blackPiecesNode.GetNode<Hound>("Hound" + i.ToString());
		}

		blackPieces.SetToBlack();

		foreach (Piece piece in whitePieces.GetAllPieces()) 
		{
			piece.PieceButtonPressed += playing.OnPiecePressed;
		}
		foreach (Piece piece in blackPieces.GetAllPieces())
		{
			piece.PieceButtonPressed += playing.OnPiecePressed;
		}
	}

	private void SetGameState(GameState newState) 
	{
		state = newState;
		//TODO:Set UI.
		switch (state) 
		{
			case NOTSTARTED:
				SetNotStarted(true);
				playing.HideAll();
				SetChoiceColor(false);
				break;
			case LM:
				SetNotStarted(false);
				SetPiecesToBoard();
				playing.InitialGame(LM);
				SetChoiceColor(false);
				break;
			case BOT:
				SetNotStarted(false);
				SetChoiceColor(true);
				break;

		}
	}
	private void SetNotStarted(bool visible) 
	{
		Node node = GetNode<Node>("NotStarted");
		node.GetNode<Button>("PlayBot").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("PlayPersonLM").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("PlayPersonLAN").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("Tutorial").SetDeferred(Button.PropertyName.Visible, visible);
	}
	private void OnSetPiecesAble(int color) 
	{
		if ((PieceColor)color == PieceColor.DRAW) 
		{
			SetAllPiecesDisable();
			return;
		}
		bool isWhite = (PieceColor)color == PieceColor.WHITE;
		whitePieces.SetAllPiecesButtonDisable(!isWhite);
		blackPieces.SetAllPiecesButtonDisable(isWhite);
	}
	private void SetAllPiecesDisable() 
	{
		whitePieces.SetAllPiecesButtonDisable(true);
		blackPieces.SetAllPiecesButtonDisable(true);
	}

	private void OnPieceSelected(int number) 
	{
		List<int> moves = playing.GetPieceLegalMoves();
		board.SelectedPiece(number);
		board.ResetLegalMove();
		board.ResetCapture();
		foreach (int move in moves) 
		{
			if (playing.IsSquareEmpty(move)) 
			{
				board.SetLegalMove(move);
			}
			else 
			{
				board.SetCapture(move);
			}
		}
	}

	public void OnLMPressed() 
	{
		SetGameState(LM);
	}
	private void OnPlayBotPressed() 
	{
		SetGameState(BOT);
	}
	private void SetChoiceColor(bool visible) 
	{
		Node node = GetNode<Node>("ChooseColor");
		node.GetNode<Button>("ChooseWhite").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("ChooseBlack").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("ChooseRandom").SetDeferred(Button.PropertyName.Visible, visible);
	}
	public async void OnMoved(int number) 
	{
		board.MovedPiece(number);
		int from = Conversion.NumToFromAndTo(number)[0];
		int to = Conversion.NumToFromAndTo(number)[1];
		Piece piece = FindPiece(to);
		if (piece != null)
		{
			piece.Capture();
			piece.SetPiecePosition(GetMarkerPositon(piece.GetPiecePosition()));
		}
		if (playing.GetEnPassant() != -1) 
		{
			piece = FindPiece(playing.GetEnPassant());
			piece.Capture();
			piece.SetPiecePosition(GetMarkerPositon(piece.GetPiecePosition()));
		}
		piece = FindPiece(from);
		piece.SetRowAndCol(to / 10, to % 10);
		await piece.MoveToPosition(GetMarkerPositon(to));
		FlipBoard();
	}

	public void OnSettedCapture(int index, bool canCapture) 
	{
		Piece piece = FindPiece(index);
		if (piece != null)
		{
			piece.SetButtonDisable(!canCapture);
		}
	}

	private async void OnGameOver(int winner) 
	{
		SetAllPiecesDisable();
		playing.SetGameOverMessage((PieceColor)winner);
		await ToSignal(GetTree().CreateTimer(1.5f), Timer.SignalName.Timeout);
		Reset();
	}

	private void OnColorChosen(int color) 
	{
		SetChoiceColor(false);
		SetPiecesToBoard();
		playing.InitialGame(BOT);
		if (state == GameState.BOT) 
		{
			playing.playerColor = (PieceColor)color;
			playing.StartBotGame();
		}
		FlipBoard();
	}

	private void OnFlipToggled(bool toggled) 
	{
		if (state == GameState.BOT && playing.playerColor != PieceColor.BLACK) 
		{
			inverted = false;
			return;
		}
		inverted = toggled;
		FlipBoard();
	}
	private void FlipBoard() 
	{
		if (state == LM)
		{
			if (playing.GetTurn() == PieceColor.BLACK && inverted)
			{
				needFlip = true;
				board.Invert(true);
			}
			else
			{
				needFlip = false;
				board.Invert(false);
			}
			SetAllPiecesPosition();
		}
		else if (state == BOT) 
		{
			if (playing.playerColor == PieceColor.BLACK && inverted)
			{
				needFlip = true;
				board.Invert(true);
			}
			else
			{
				needFlip = false;
				board.Invert(false);
			}
			SetAllPiecesPosition();
		}
	}

	private Piece FindPiece(int index) 
	{
		foreach (Piece piece in whitePieces.GetAllPieces()) 
		{
			if(piece.GetPiecePosition() == index) 
			{
				return piece;
			}
		}
		foreach (Piece piece in blackPieces.GetAllPieces())
		{
			if (piece.GetPiecePosition() == index)
			{
				return piece;
			}
		}
		return null;
	}
	private void Reset() 
	{
		ResetPieces();
		board.Reset();
		SetGameState(NOTSTARTED);
	}
	public override void _Ready()
	{
		playing = GetNode<Playing>("Playing");
		for (int i = 0; i < 100; i++)
		{
			markers[i] = GetNode<Node>("Markers").GetNode<Marker2D>(i.ToString());
		}
		board = GetNode<Board>("Board");
		InitialPieces();
		Reset();
	}
}
