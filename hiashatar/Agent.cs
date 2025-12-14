using Godot;
using System.Threading;
using static TorchSharp.torch;

namespace Hiashatar
{
	public abstract partial class Agent() : Node 
	{
		[Signal]
		public delegate void AiSelectedMoveEventHandler(int moveIndex);

		protected Thread aiThread;
		protected CancellationTokenSource cts;
		protected GameBoard board;

		public void StartThread()
		{
			cts = new CancellationTokenSource();
			CancellationToken token = cts.Token;
			aiThread = new Thread(() => SelectMove(token));
			aiThread.Start();
		}
		public void StopThread()
		{
			if (aiThread != null && aiThread.IsAlive)
			{
				cts?.Cancel();
				aiThread.Join(1000);
				cts.Dispose();
				cts = null;
			}
		}
		public void SetBoard(GameBoard board)
		{
			this.board = board;
		}

		protected abstract void SelectMove(CancellationToken token);

		protected void EmitMove(int moveIndex)
		{
			EmitSignal(SignalName.AiSelectedMove, moveIndex);
		}
	}

	public partial class Morii() : Agent
	{
		public float tau = 1;
		public float cPuct = 2;
		public int sims = 100;

		private HiashatarModel model = new();

		protected override void SelectMove(CancellationToken token)
		{
			MCEdge rootEdge = new MCEdge(null, null);
			rootEdge.N = 1;
			MCNode rootNode = new MCNode(new(board), rootEdge);
			MCTS mctsSearcher = new MCTS(model);
			mctsSearcher.SetParameters(tau, cPuct, sims);
			float[] moveProb = mctsSearcher.Search(rootNode, token);
			if (token.IsCancellationRequested)
			{
				return;
			}
			Tensor randMove = multinomial(tensor(moveProb), 1);
			CallDeferred(nameof(EmitMove), randMove.item<long>());
		}
	}
	public partial class Themee : Agent 
	{
		public int depth = 3;

		protected override void SelectMove(CancellationToken token)
		{
			ABPruning ab = new ABPruning(depth);
			if (token.IsCancellationRequested)
			{
				return;
			}
			int selectedMove = ab.SelectMove(board, token);
			if (token.IsCancellationRequested)
			{
				return;
			}
			CallDeferred(nameof(EmitMove), selectedMove);
		}
	}

}
