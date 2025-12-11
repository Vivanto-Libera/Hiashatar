using Godot;
using Hiashatar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tensorboard;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;

namespace Hiashatar
{
    public class Agent
    {
        public HiashatarModel model;
        public int SelectMove(GameBoard board)
        {
            MCEdge rootEdge = new MCEdge(null, null);
            rootEdge.N = 1;
            MCNode rootNode = new MCNode(board, rootEdge);
            MCTS MctsSearcher = new MCTS(model, 200);
            float[] moveProb = MctsSearcher.Search(rootNode);
            Tensor randIdx = multinomial(moveProb, 1);
            return Array.IndexOf([.. randIdx.data<int>()], 1);
        }
        public Agent(HiashatarModel model)
        {
            this.model = model;
        }
    }
}
