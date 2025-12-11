using System;
using System.Collections.Generic;
using System.Linq;
using static TorchSharp.torch;
using TorchSharp.Modules;
using TorchSharp;

namespace Hiashatar
{
    public class HiashatarModel
    {
        jit.ScriptModule model;
        public ValueTuple<Tensor, Tensor> Predict(Tensor input)
        {
            return (ValueTuple<Tensor, Tensor>)model.forward(input);
        }
        public HiashatarModel()
        {
            model = jit.load("Mandal.pt");
            model.eval();
        }
    }
}
