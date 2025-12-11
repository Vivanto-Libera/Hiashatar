using Godot;
using System;
using System.Collections.Generic;
using TorchSharp;

namespace Hiashatar
{
	public struct EdgeNode
	{
		public MCEdge edge;
		public MCNode node;
		public EdgeNode(MCEdge edge, MCNode node)
		{
			this.edge = edge;
			this.node = node;
		}
	}
	public class MCEdge
	{
		public int? move;
		public int N = 0;
		public float W = 0;
		public float Q = 0;
		public float P = 0;
#nullable enable
		public MCNode? parentNode;

		public MCEdge(int? move, MCNode? parentNode)
		{
			this.move = move;
			this.parentNode = parentNode;
		}
	}
	public class MCNode
	{
		public GameBoard board;
		public MCEdge parentEdge = new(null, null);
		public List<EdgeNode> childEdgeNodes = [];
		public float Expand(HiashatarModel model)
		{
			List<int> moves = board.LegalMoves();
			foreach (int move in moves)
			{
				GameBoard childBoard = new(board);
				childBoard.ApplyMove(Conversion.MoveToIndex(move));
				MCEdge childEdge = new(move, this);
				MCNode childNode = new(childBoard, childEdge);
				childEdgeNodes.Add(new EdgeNode(childEdge, childNode));
			}
			var (prob, value) = model.Predict(board.ModelInput());
			float probSum = 0;
			foreach (EdgeNode edgeNode in childEdgeNodes)
			{
				edgeNode.edge.P = prob[0, Conversion.MoveToIndex(edgeNode.edge.move.Value)].item<float>();
				probSum += edgeNode.edge.P;
			}
			foreach (EdgeNode edgeNode in childEdgeNodes)
			{
				edgeNode.edge.P /= probSum;
			}
			return value[0, 0].item<float>();
		}
		public bool IsLeaf()
		{
			return childEdgeNodes.Count == 0;
		}
		public MCNode(GameBoard board, MCEdge parentEdge)
		{
			this.board = board;
			this.parentEdge = parentEdge;
		}
	}
	public class MCTS
	{
		public HiashatarModel model;
#nullable enable
		public MCNode? rootNode = null;
		public float tau = 1;
		public float cPuct = 2;
		int times;
		public MCTS(HiashatarModel model, int times)
		{
			this.model = model;
			this.times = times;
		}
		public float UctValues(MCEdge edge, int ParentN)
		{
			return cPuct * edge.P * ((float)Math.Sqrt(ParentN) / (1 + edge.N));
		}
		public MCNode Select(MCNode node)
		{
			if (node.IsLeaf())
			{
				return node;
			}
			else
			{
				MCNode? maxUctChild = null;
				float maxUctValue = -100000000000;
				foreach (EdgeNode edgeNode in node.childEdgeNodes)
				{
					float uctValues = UctValues(edgeNode.edge, edgeNode.edge.parentNode.parentEdge.N);
					float val = edgeNode.edge.Q;
					if (edgeNode.edge.parentNode.board.turn == PieceColor.BLACK)
					{
						val = -val;
					}
					float uctValChild = val + uctValues;
					if (uctValChild > maxUctValue)
					{
						maxUctValue = uctValChild;
						maxUctChild = edgeNode.node;
					}
				}
				List<MCNode> allBestChild = new List<MCNode>();
				return Select(maxUctChild);
			}
		}
		public void BackUp(float value, MCEdge edge)
		{
			edge.N += 1;
			edge.W += value;
			edge.Q = edge.W / edge.N;
			if (edge.parentNode != null)
			{
				if (edge.parentNode.parentEdge != null)
				{
					BackUp(value, edge.parentNode.parentEdge);
				}
			}
		}
		public void ExpandAndEvaluate(MCNode node)
		{
			PieceColor winner = node.board.IsTerminal();
			float v = 0;
			if (winner != PieceColor.NOTEND)
			{
				if (winner == PieceColor.WHITE)
				{
					v = 1;
				}
				else if (winner == PieceColor.BLACK)
				{
					v = -1;
				}
				BackUp(v, node.parentEdge);
				return;
			}
			v = node.Expand(model);
			BackUp(v, node.parentEdge);
		}
		public float[] Search(MCNode rootNode)
		{
			this.rootNode = rootNode;
			this.rootNode.Expand(model);
			for (int i = 0; i < times; i++)
			{
				MCNode selectedNode = Select(rootNode);
				ExpandAndEvaluate(selectedNode);
			}
			int NSum = 0;
			float[] moveProbs = new float[3516];
			foreach (EdgeNode edgeNode in rootNode.childEdgeNodes)
			{
				NSum += edgeNode.edge.N;
			}
			foreach (EdgeNode edgeNode in rootNode.childEdgeNodes)
			{
				float prob = ((float)Math.Pow(edgeNode.edge.N, (1 / tau))) / (float)Math.Pow(NSum, (1 / tau));
				moveProbs[Conversion.MoveToIndex(edgeNode.edge.move.Value)] = prob;
			}
			return moveProbs;
		}
	}
}
