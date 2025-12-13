using Godot;
using Hiashatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tensorboard;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

namespace Hiashatar
{
	public partial class Agent(HiashatarModel model) : Node
	{
		[Signal]
		public delegate void AiSelectedMoveEventHandler(int moveIndex);

		public float tau = 1;
		public float cPuct = 2;
		public int sims = 100;

		private Thread aiThread;
		private CancellationTokenSource cts;
		private HiashatarModel model = model;
		private GameBoard board;

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

		private void SelectMove(CancellationToken token)
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
		private void EmitMove(int moveIndex)
		{
			EmitSignal(SignalName.AiSelectedMove, moveIndex);
		}
	}
}
