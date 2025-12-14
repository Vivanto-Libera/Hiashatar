//Created by Vivanto(GitHub:Vivanto-Libera)(E-mail:vivanto@qq.com)

using Godot;
using Hiashatar;
using System.Collections.Generic;
using static Hiashatar.GameState;

public partial class Main : Node
{
	private Pieces whitePieces = new();
	private Pieces blackPieces = new();
	private Marker2D[] markers = new Marker2D[100];
	private Board board;
	private bool inverted = false;
	private bool needFlip = false;  //If its true, in function GetMarkerPositon will give the flipped Marker's position.
	private GameState state = NOTSTARTED;
	private Playing playing;
	private Tutorial tutorial;


	private void OnLMPressed() 
	{
		SetGameState(LM);
	}
	private void OnPlayBotPressed() 
	{
		SetGameState(BOT);
	}
	private void OnTutotialPressed() 
	{
		SetGameState(TUTORIAL);
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

	private async void OnMoved(int number) 
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

	private void OnSettedCapture(int index, bool canCapture) 
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
		playing.ClearPreboards();
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
		OnFlipToggled(playing.GetNode<Button>("Flip").ButtonPressed);
	}

	private void OnUndid(int move) 
	{
		board.MovedPiece(move);
		SetPieceFromBoard(playing.GetBoard());
		FlipBoard();
		SetAllPiecesPosition();
	}

	private void OnFlipToggled(bool toggled) 
	{
		if (state == GameState.BOT && playing.playerColor != PieceColor.BLACK)
		{
			inverted = false;
		}
		else 
		{
			inverted = toggled; 
		}
		FlipBoard();
	}

	private void OnMoriiChosen() 
	{
		playing.isMorii = true;
		SetChoiceEngine(false);
		SetChoiceColor(true);
	}
	private void OnThemeeChosen() 
	{
		playing.isMorii = false;
		SetChoiceEngine(false);
		SetChoiceColor(true);
	}

	private void OnExitPressed() 
	{
		board.Reset();
		ResetPieces();
		if (!tutorial.IsInTutorial()) 
		{
			SetGameState(NOTSTARTED);
		}
		else 
		{
			tutorial.SetButtonsVisible(true);
			tutorial.SetState(false);
		}
	}
	private void OnBasicRule() 
	{
		SetPiecesToBoard();
	}
	private void OnHoundPressed() 
	{
		whitePieces.hounds[0].SetRowAndCol(8, 0);
		board.SetLegalMoveWithoutButton(70);
		board.SetLegalMoveWithoutButton(60);
		board.SetLegalMoveWithoutButton(50);
		whitePieces.hounds[1].SetRowAndCol(7, 2);
		board.SetLegalMoveWithoutButton(62);
		whitePieces.hounds[2].SetRowAndCol(8, 4);
		blackPieces.hounds[0].SetRowAndCol(7, 5);
		board.SetCaptureWithoutButton(75);
		whitePieces.hounds[3].SetRowAndCol(4, 6);
		blackPieces.hounds[1].SetRowAndCol(4, 7);
		board.SetLegalMoveWithoutButton(37);
		board.SetHighlight(47);
		board.SetHighlight(17);
		whitePieces.lion.SetRowAndCol(0, 9);
		board.SetHighlight(9);
		board.SetHighlight(19);
		SetAllPiecesPosition();
	}
	private void OnTergePressed() 
	{
		whitePieces.terges[0].SetRowAndCol(5, 4);
		blackPieces.hounds[0].SetRowAndCol(3, 4);
		whitePieces.hounds[0].SetRowAndCol(5, 2);
		board.SetHighlight(54);
		board.SetLegalMoveWithoutButton(53);
		board.SetLegalMoveWithoutButton(44);
		board.SetCaptureWithoutButton(34);
		board.SetLegalMoveWithoutButton(55);
		board.SetLegalMoveWithoutButton(56);
		board.SetLegalMoveWithoutButton(57);
		board.SetLegalMoveWithoutButton(58);
		board.SetLegalMoveWithoutButton(59);
		board.SetLegalMoveWithoutButton(64);
		board.SetLegalMoveWithoutButton(74);
		board.SetLegalMoveWithoutButton(84);
		board.SetLegalMoveWithoutButton(94);
		SetAllPiecesPosition();
	}
	private void OnHorsePressed() 
	{
		whitePieces.horses[0].SetRowAndCol(5, 4);
		board.SetHighlight(54);
		blackPieces.hounds[0].SetRowAndCol(3, 3);
		board.SetCaptureWithoutButton(33);
		board.SetLegalMoveWithoutButton(35);
		board.SetLegalMoveWithoutButton(73);
		board.SetLegalMoveWithoutButton(75);
		board.SetLegalMoveWithoutButton(42);
		board.SetLegalMoveWithoutButton(46);
		board.SetLegalMoveWithoutButton(62);
		board.SetLegalMoveWithoutButton(66);
		SetAllPiecesPosition();
	}
	private void OnCamelPressed() 
	{
		whitePieces.camels[0].SetRowAndCol(5, 4);
		whitePieces.hounds[0].SetRowAndCol(7, 2);
		blackPieces.hounds[0].SetRowAndCol(3, 6);
		board.SetHighlight(54);
		board.SetCaptureWithoutButton(36);
		board.SetLegalMoveWithoutButton(45);
		board.SetLegalMoveWithoutButton(63);
		board.SetLegalMoveWithoutButton(43);
		board.SetLegalMoveWithoutButton(32);
		board.SetLegalMoveWithoutButton(21);
		board.SetLegalMoveWithoutButton(10);
		board.SetLegalMoveWithoutButton(65);
		board.SetLegalMoveWithoutButton(76);
		board.SetLegalMoveWithoutButton(87);
		board.SetLegalMoveWithoutButton(98);
		SetAllPiecesPosition();
	}
	private void OnGuardPressed() 
	{
		whitePieces.guards[0].SetRowAndCol(7, 2);
		blackPieces.hounds[0].SetRowAndCol(6, 1);
		board.SetCaptureWithoutButton(61);
		board.SetLegalMoveWithoutButton(62);
		board.SetLegalMoveWithoutButton(52);
		board.SetLegalMoveWithoutButton(82);
		board.SetLegalMoveWithoutButton(92);
		board.SetLegalMoveWithoutButton(71);
		board.SetLegalMoveWithoutButton(70);
		board.SetLegalMoveWithoutButton(73);
		board.SetLegalMoveWithoutButton(74);
		board.SetLegalMoveWithoutButton(63);
		board.SetLegalMoveWithoutButton(54);
		board.SetLegalMoveWithoutButton(81);
		board.SetLegalMoveWithoutButton(90);
		board.SetLegalMoveWithoutButton(83);
		board.SetLegalMoveWithoutButton(94);
		whitePieces.guards[1].SetRowAndCol(7, 7);
		board.SetHighlight(72);
		board.SetHighlight(78);
		board.SetHighlight(76);
		board.SetHighlight(87);
		board.SetHighlight(67);
		board.SetHighlight(66);
		board.SetHighlight(88);
		board.SetHighlight(68);
		board.SetHighlight(86);
		blackPieces.terges[0].SetRowAndCol(4, 8);
		board.SetLegalMoveWithoutButton(58);
		board.SetLegalMoveWithoutButton(68);
		SetAllPiecesPosition();
	}
	private void OnLionPressed() 
	{
		whitePieces.lion.SetRowAndCol(5, 4);
		whitePieces.hounds[0].SetRowAndCol(7, 2);
		blackPieces.hounds[0].SetRowAndCol(3, 6);
		board.SetHighlight(54);
		board.SetCaptureWithoutButton(36);
		board.SetLegalMoveWithoutButton(45);
		board.SetLegalMoveWithoutButton(63);
		board.SetLegalMoveWithoutButton(43);
		board.SetLegalMoveWithoutButton(32);
		board.SetLegalMoveWithoutButton(21);
		board.SetLegalMoveWithoutButton(10);
		board.SetLegalMoveWithoutButton(65);
		board.SetLegalMoveWithoutButton(76);
		board.SetLegalMoveWithoutButton(87);
		board.SetLegalMoveWithoutButton(98);
		blackPieces.hounds[1].SetRowAndCol(3, 4);
		whitePieces.hounds[1].SetRowAndCol(5, 2);
		board.SetLegalMoveWithoutButton(53);
		board.SetLegalMoveWithoutButton(44);
		board.SetCaptureWithoutButton(34);
		board.SetLegalMoveWithoutButton(55);
		board.SetLegalMoveWithoutButton(56);
		board.SetLegalMoveWithoutButton(57);
		board.SetLegalMoveWithoutButton(58);
		board.SetLegalMoveWithoutButton(59);
		board.SetLegalMoveWithoutButton(64);
		board.SetLegalMoveWithoutButton(74);
		board.SetLegalMoveWithoutButton(84);
		board.SetLegalMoveWithoutButton(94);
		SetAllPiecesPosition();
	}
	private void OnKhanPressed() 
	{
		whitePieces.khan.SetRowAndCol(8, 5);
		board.SetHighlight(85);
		blackPieces.hounds[0].SetRowAndCol(7, 5);
		board.SetCaptureWithoutButton(75);
		board.SetLegalMoveWithoutButton(74);
		board.SetLegalMoveWithoutButton(76);
		board.SetLegalMoveWithoutButton(84);
		board.SetLegalMoveWithoutButton(86);
		board.SetLegalMoveWithoutButton(94);
		board.SetLegalMoveWithoutButton(95);
		board.SetLegalMoveWithoutButton(96);
		SetAllPiecesPosition();
	}

	private Vector2 GetMarkerPositon(int number)
	{
		//When number == -1, is mean move the piece off the board. So return CapturedMarker's position
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
	private void SetPiecesToBoard()
	{
		whitePieces.SetToBoard(PieceColor.WHITE);
		blackPieces.SetToBoard(PieceColor.BLACK);
		SetAllPiecesPosition();
	}
	private void SetAllPiecesDisable()
	{
		whitePieces.SetAllPiecesButtonDisable(true);
		blackPieces.SetAllPiecesButtonDisable(true);
	}
	private void SetPieceFromBoard(GameBoard board)
	{
		List<Piece> white = whitePieces.GetAllPieces();
		List<Piece> black = blackPieces.GetAllPieces();
		List<Rule_Piece> Rule_white = board.whitePieces.GetAllPieces();
		List<Rule_Piece> Rule_black = board.blackPieces.GetAllPieces();
		for (int i = 0; i < 20; i++)
		{
			white[i].SetRowAndCol(Rule_white[i].GetPosition()[0], Rule_white[i].GetPosition()[1]);
			black[i].SetRowAndCol(Rule_black[i].GetPosition()[0], Rule_black[i].GetPosition()[1]);
		}
	}
	private void SetGameState(GameState newState)
	{
		state = newState;
		switch (state)
		{
			case NOTSTARTED:
				SetNotStarted(true);
				playing.HideAll();
				tutorial.SetButtonsVisible(false);
				SetChoiceColor(false);
				SetChoiceEngine(false);
				break;
			case LM:
				SetNotStarted(false);
				SetPiecesToBoard();
				playing.InitialGame(LM);
				break;
			case BOT:
				SetNotStarted(false);
				SetChoiceEngine(true);
				break;
			case TUTORIAL:
				SetNotStarted(false);
				tutorial.SetButtonsVisible(true);
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
	private void SetChoiceColor(bool visible)
	{
		Node node = GetNode<Node>("ChooseColor");
		node.GetNode<Button>("ChooseWhite").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("ChooseBlack").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("ChooseRandom").SetDeferred(Button.PropertyName.Visible, visible);
	}
	private void SetChoiceEngine(bool visible) 
	{
		Node node = GetNode<Node>("ChooseEngine");
		node.GetNode<Button>("ChooseMorii").SetDeferred(Button.PropertyName.Visible, visible);
		node.GetNode<Button>("ChooseThemee").SetDeferred(Button.PropertyName.Visible, visible);
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

	private void Reset() 
	{
		ResetPieces();
		board.Reset();
		SetGameState(NOTSTARTED);
	}
	private void ResetPieces()
	{
		whitePieces.Reset();
		blackPieces.Reset();
		SetAllPiecesPosition();
	}

	public override void _Ready()
	{
		playing = GetNode<Playing>("Playing");
		tutorial = GetNode<Tutorial>("Tutorial");
		for (int i = 0; i < 100; i++)
		{
			markers[i] = GetNode<Node>("Markers").GetNode<Marker2D>(i.ToString());
		}
		board = GetNode<Board>("Board");
		InitialPieces();
		Reset();
	}
}
