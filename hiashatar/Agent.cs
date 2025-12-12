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
	public partial class Agent : Node
	{
		[Signal]
		public delegate void AiSelectedMoveEventHandler(int moveIndex);
		private Thread aiThread;
		public HiashatarModel model;
		private GameBoard board;
		private void SelectMove()
		{
			MCEdge rootEdge = new MCEdge(null, null);
			rootEdge.N = 1;
			MCNode rootNode = new MCNode(new(board), rootEdge);
			MCTS MctsSearcher = new MCTS(model, 100);
			float[] moveProb = MctsSearcher.Search(rootNode);
			Tensor randMove = multinomial(tensor(moveProb), 1);
			CallDeferred(nameof(EmitMove), randMove.item<long>());
		}
		public void StartThread() 
		{
			aiThread = new Thread(SelectMove);
			aiThread.Start();
		}
		public void SetBoard(GameBoard board) 
		{
			this.board = board;
		}
		private void EmitMove(int moveIndex) 
		{
			EmitSignal(SignalName.AiSelectedMove, moveIndex);
		}
		public Agent(HiashatarModel model)
		{
			this.model = model;
		}

	}
}
