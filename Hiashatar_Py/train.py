import mcts
import torch
from Hiashatar import Board
from Hiashatar import Color
from tqdm import tqdm
import numpy as np
from dataset import HiashatarDataset
from hiashatarModel import HiashatarModel
import torch.optim as optim
from torch.utils.data import Dataset, DataLoader
import torch.nn as nn

class ReinfLearn():

    def __init__(self, model):
        self.model = model

    def playGame(self):
        positionsData = []
        probsData = []
        valuesData = []
        g = Board()
        while g.isTerminal() == Color.NOTEND:
            positionsData.append(g.neuralworkInput())
            rootEdge = mcts.Edge(None, None)
            rootEdge.N = 1
            rootNode = mcts.Node(g, rootEdge)
            mctsSearcher = mcts.MCTS(self.model, 10)
            moveProbs = mctsSearcher.search(rootNode)
            outputVec = np.zeros(3516)
            for (move, prob) in moveProbs:
                outputVec[Board.moveToIndex(move)] = prob
            rand_idx = np.random.multinomial(1, outputVec)
            nextMove = np.where(rand_idx==1)[0][0]
            if(g.turn == Color.WHITE):
                valuesData.append([1])
            else:
                valuesData.append([-1])
            probsData.append(outputVec)
            g.applyMove(nextMove)
        else:
            winner = g.isTerminal()
            for i in range(0, len(valuesData)):
                if(winner == Color.BLACK):
                    valuesData[i][0] = valuesData[i][0] * -1.0
                elif winner == Color.WHITE:
                    valuesData[i][0] = valuesData[i][0] * 1.0
                else:
                    valuesData[i][0] = 0.0
        return (positionsData, probsData, valuesData)
    
model = HiashatarModel()
model.load_state_dict(torch.load("new_model.pt"))
policy_loss = nn.CrossEntropyLoss()
value_loss = nn.MSELoss()
optimizer = optim.Adam(model.parameters(), lr=1e-3)
i = 0
while True:
    learner = ReinfLearn(model)
    allPos = np.empty(0)
    allProbs = np.empty(0)
    allValues = np.empty(0)
    for j in tqdm(range(0,1)):
        pos, probs, values = learner.playGame()
        if allPos.size == 0:
            allPos = pos
            allProbs = probs
            allValues = values
        else:
            allPos = np.concatenate([pos, allPos], axis=0)
            allProbs = np.concatenate([probs, allProbs], axis=0)
            allValues = np.concatenate([values, allValues], axis=0)
    allPos = np.array(allPos)
    allProbs = np.array(allProbs)
    allValues = np.array(allValues)
    train_dataset = HiashatarDataset(allPos, allProbs, allValues)
    train_loader = DataLoader(train_dataset, batch_size=128, shuffle=True)
    device= torch.device("cuda" if torch.cuda.is_available() else "cpu")
    model = model.to(device)
    model.train()
    for epoch in range(0,1):
        for batch_pos, batch_probs,batch_val in train_loader:
            batch_pos = batch_pos.to(device)
            batch_probs
            batch_val = batch_val.to(device)
            optimizer.zero_grad()
            pred_policy, pred_value = model(batch_pos)
            loss_policy = policy_loss(pred_policy, batch_probs)
            loss_value = value_loss(pred_value, batch_val)
            loss = loss_policy + loss_value * 0.5
            loss.backward()
            optimizer.step()
    torch.save(model.state_dict(), "model_it"+str(i)+".pt")
    i += 1
