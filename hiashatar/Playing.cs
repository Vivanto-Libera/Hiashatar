using Godot;
using Hiashatar;
using System.Collections.Generic;

public partial class Playing : Node
{
	public struct PreBoard 
	{
		public GameBoard board;
		public int move;
		public PreBoard(GameBoard board, int move) 
		{
			this.board = new(board);
			this.move = move;
		}
	}

	[Signal]
	public delegate void SetPiecesAbleEventHandler(int color);
	[Signal]
	public delegate void PieceSelectedEventHandler(int number);
	[Signal]
	public delegate void PieceCapturedEventHandler(int number);
	[Signal]
	public delegate void MovedEventHandler(int move);
	[Signal]
	public delegate void GameOverEventHandler(int winner);
	[Signal]
	public delegate void UndidEventHandler(int move);

	public PieceColor playerColor;

	private GameBoard board = new();
	private GameState state;
	private List<int> legalMoves = [];
	private int from;
	private int to;
	private int enPassant = -1;
	private Agent agent = new(new HiashatarModel());
	private BotSetDialog dialog;
	private Stack<PreBoard> preBoards = new();
	private PreBoard lastBoard;

	public void InitialGame(GameState state) 
	{
		this.state = state;
		board = new();
		legalMoves = board.LegalMoves();
		GetNode<Label>("Message").SetDeferred(Label.PropertyName.Visible, true);
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, true);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, true);
		preBoards.Clear();
		if (state == GameState.LM) 
		{
			lastBoard = new(board, -1);
			GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, true);
			GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
			SetPiecesButton();
		}
		else if (state == GameState.BOT) 
		{
			GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, true);
			GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, true);
		}
	}

	
	public void OnPiecePressed(int number, int color) 
	{
		if ((PieceColor)color == board.turn) 
		{
			from = number;
			EmitSignal(SignalName.PieceSelected, number);
		}
		else 
		{
			EmitSignal(SignalName.PieceCaptured, number);
		}
	}

	public void OnSquarePressed(int number) 
	{
		to = number;
		board.ApplyMove(Conversion.MoveToIndex(from * 100 + to));
		if (state == GameState.LM) 
		{
			preBoards.Push(lastBoard);
			lastBoard = new(board, from * 100 + to);
		}
		enPassant = board.GetEnPassant();
		legalMoves = board.LegalMoves();
		EmitSignal(SignalName.Moved, from * 100 + number);
		PieceColor winner = board.IsTerminal();
		if (winner != PieceColor.NOTEND) 
		{
			EmitSignal(SignalName.GameOver, (int)winner);
			return;
		}
		if (state == GameState.LM)
		{
			SetPiecesButton();
		}
		if (state == GameState.BOT) 
		{
			preBoards.Push(lastBoard);
			AIMove();
		}
	}

	public List<int> GetPieceLegalMoves() 
	{
		List<int> moves = new List<int>();
		foreach (int move in legalMoves) 
		{
			if (from == Conversion.NumToFromAndTo(move)[0]) 
			{
				moves.Add(Conversion.NumToFromAndTo(move)[1]);
			}
		}
		return moves;
	}

	public bool IsSquareEmpty(int number) 
	{
		return board.board[number / 10, number % 10] == PieceType.EMPTY;
	}
	public int GetEnPassant() 
	{
		return enPassant;
	}

	public void StartBotGame() 
	{
		SetTurnMessage(PieceColor.WHITE);
		if (playerColor == PieceColor.WHITE) 
		{
			lastBoard = new(board, -1);
			SetPiecesButton();
		}
		else 
		{
			AIMove();
		}
	}

	public void HideAll() 
	{
		GetNode<Label>("Message").SetDeferred(Label.PropertyName.Visible, false);
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
	}

	public void SetTurnMessage(PieceColor turn) 
	{
		if (state == GameState.LM)
		{
			if (turn == PieceColor.WHITE)
			{
				SetMessage("白方回合");
			}
			else
			{
				SetMessage("黑方回合");
			}
		}
		else if(state == GameState.BOT) 
		{
			if (turn == playerColor)
			{
				SetMessage("玩家回合");
			}
			else
			{
				SetMessage("AI回合");
			}
		}
	}
	public void SetGameOverMessage(PieceColor winner) 
	{
		GetNode<Button>("Over").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Flip").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("Undo").SetDeferred(Button.PropertyName.Visible, false);
		GetNode<Button>("BotSet").SetDeferred(Button.PropertyName.Visible, false);
		switch (winner) 
		{
			case PieceColor.WHITE:
				SetMessage("白方获胜");
				break;
			case PieceColor.BLACK:
				SetMessage("黑方获胜");
				break;
			case PieceColor.DRAW:
				SetMessage("和棋");
				break;
			default:
				SetMessage("游戏结束");
				break;
		}
	}

	public PieceColor GetTurn() 
	{
		return board.turn;
	}
	public GameBoard GetBoard() 
	{
		return board;
	}
	public void ClearPreboards() 
	{
		preBoards.Clear();
	}

	private void SetPiecesButton()
	{
		SetTurnMessage(board.turn);
		EmitSignal(SignalName.SetPiecesAble, (int)board.turn);
	}

	private void AIMove()
	{
		agent.SetBoard(new GameBoard(board));
		SetTurnMessage(board.turn);
		EmitSignal(SignalName.SetPiecesAble, (int)PieceColor.DRAW);
		agent.StartThread();
	}
	private void OnAiSelectedMove(int moveIndex)
	{
		board.ApplyMove(moveIndex);
		enPassant = board.GetEnPassant();
		legalMoves = board.LegalMoves();
		lastBoard = new(board, Conversion.IndexTransToMove(moveIndex));
		EmitSignal(SignalName.Moved, Conversion.IndexTransToMove(moveIndex));
		PieceColor winner = board.IsTerminal();
		if (winner != PieceColor.NOTEND)
		{
			EmitSignal(SignalName.GameOver, (int)winner);
		}
		else
		{
			SetPiecesButton();
		}
	}

	private void OnUndoPressed()
	{
		if (preBoards.Count == 0)
		{
			return;
		}
		agent.StopThread();
		lastBoard = preBoards.Pop();
		board = new(lastBoard.board);
		enPassant = board.GetEnPassant();
		legalMoves = board.LegalMoves();
		SetTurnMessage(board.turn);
		EmitSignal(SignalName.Undid, lastBoard.move);
		EmitSignal(SignalName.SetPiecesAble, (int)board.turn);
	}
	private void OnOverPressed()
	{
		if (state == GameState.BOT)
		{
			agent.StopThread();
		}
		EmitSignal(SignalName.GameOver, (int)PieceColor.NOTEND);
	}
	private void OnBotSetPressed()
	{
		dialog.SetValues(agent.sims, agent.tau, agent.cPuct);
		dialog.Show();
	}
	private void OnSetSaved(int sims, float tau, float cpuct)
	{
		agent.sims = sims;
		agent.tau = tau;
		agent.cPuct = cpuct;
	}

	private void SetMessage(string message)
	{
		GetNode<Label>("Message").Text = message;

	}

	public override void _Ready()
	{
		agent.AiSelectedMove += OnAiSelectedMove;
		dialog = GetNode<BotSetDialog>("BotSetDialog");
	}
}
