using Godot;
using Hiashatar;
using System;

public partial class Main : Node
{
	private Pieces whitePieces = new Pieces();
	private Pieces blackPieces = new Pieces();
	public override void _Ready()
	{
		InitialPieces();
	}

	private void InitialPieces() 
	{
		Node whitePiecesNode = GetNode<Node>("WhitePieces");
		Node blackPiecesNode = GetNode<Node>("BlackPieces");

		whitePieces.terges[0] = whitePiecesNode.GetNode<Piece>("Terge0");
		blackPieces.terges[0] = blackPiecesNode.GetNode<Piece>("Terge0");
		whitePieces.terges[1] = whitePiecesNode.GetNode<Piece>("Terge1");
		blackPieces.terges[1] = blackPiecesNode.GetNode<Piece>("Terge1");

		whitePieces.camels[0] = whitePiecesNode.GetNode<Camel>("Camel0");
		blackPieces.camels[0] = blackPiecesNode.GetNode<Camel>("Camel0");
		whitePieces.camels[1] = whitePiecesNode.GetNode<Camel>("Camel1");
		blackPieces.camels[1] = blackPiecesNode.GetNode<Camel>("Camel1");

		whitePieces.horses[0] = whitePiecesNode.GetNode<Piece>("Horse0");
		blackPieces.horses[0] = blackPiecesNode.GetNode<Piece>("Horse0");
		whitePieces.horses[1] = whitePiecesNode.GetNode<Piece>("Horse1");
		blackPieces.horses[1] = blackPiecesNode.GetNode<Piece>("Horse1");

		whitePieces.guards[0] = whitePiecesNode.GetNode<Guard>("Guard0");
		blackPieces.guards[0] = blackPiecesNode.GetNode<Guard>("Guard0");
		whitePieces.guards[1] = whitePiecesNode.GetNode<Guard>("Guard1");
		blackPieces.guards[1] = blackPiecesNode.GetNode<Guard>("Guard1");

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
	}
}
