using System;
using System.Collections.Generic;
using static Hiashatar.PieceColor;
using static Hiashatar.PieceType;

namespace Hiashatar
{
	public class Rule_Piece
	{
		protected int row;
		protected int column;
		protected PieceType pieceType;
		protected PieceColor color;

		public int[] GetPosition() 
		{
			return new int[] {row, column};
		}
		public virtual void Move(int row, int column) 
		{
			this.row = row;
			this.column = column;
		}
		public bool IsCaptured()
		{
			return row == -1;
		}
		public PieceColor GetColor() 
		{
			return color;
		}
		public void Capture() 
		{
			row = -1;
			column = -1;
		}
		public PieceType getType() 
		{
			return pieceType;
		}

		public Rule_Piece() { }
		public Rule_Piece(int row, int column, PieceColor pieceColor)
		{
			this.row = row;
			this.column = column;
			this.color = pieceColor;
		}
		public Rule_Piece(Rule_Piece piece) 
		{
			pieceType = piece.pieceType;
			row = piece.row;
			column = piece.column;
			color = piece.color;
		}
	}
	public class Rule_Terge : Rule_Piece 
	{
		public Rule_Terge(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITETERGE : BLACKTERGE;
		}
		public Rule_Terge(Rule_Terge piece) : base(piece) { }
	}
	public class Rule_Horse : Rule_Piece
	{
		public Rule_Horse(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITEHORSE : BLACKHORSE;
		}
		public Rule_Horse(Rule_Horse piece) : base(piece) { }
	}
	public class Rule_Camel : Rule_Piece
	{
		public Rule_Camel(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITECAMEL : BLACKCAMEL;
		}
		public Rule_Camel(Rule_Camel piece) : base(piece) { }
		public PieceColor GetSquareColor()
		{
			return row % 2 == column % 2 ? BLACK : WHITE;
		}
	}
	public class Rule_Guard : Rule_Piece
	{
		public Rule_Guard(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITEGUARD : BLACKGUARD;
		}
		public Rule_Guard(Rule_Guard piece) : base(piece) { }
		public List<int> GetControlZone()
		{
			List<int> controlZone = new List<int>();
			for (int i = 0; i < 8; i++)
			{
				int newRow = row + Direction.QUEENLIKEROW[i];
				int newCol = column + Direction.QUEENLIKECOL[i];
				if (newRow < 0 || newRow > 9 || newCol < 0 || newCol > 9)
				{
					continue;
				}
				controlZone.Add(newRow * 10 + newCol);
			}
			return controlZone;
		}
	}
	public class Rule_Lion : Rule_Piece
	{
		public Rule_Lion(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITELION : BLACKLION;
		}
		public Rule_Lion(Rule_Lion piece) : base(piece) { }
	}
	public class Rule_Khan : Rule_Piece
	{
		public Rule_Khan(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITEKHAN : BLACKKHAN;
		}
		public Rule_Khan(Rule_Khan piece) : base(piece) { }
	}
	public class Rule_Hound : Rule_Piece
	{
		private bool isPromoted = false;
		private bool canEnPassant = false;

		public bool IsPromoted()
		{
			return isPromoted;
		}

		public bool CanEnPassant()
		{
			return canEnPassant;
		}
		public void ResetEnPassant()
		{
			canEnPassant = false;
		}
		public override void Move(int row, int column)
		{
			if (!isPromoted && ((this.row == 8 && color == WHITE) || (this.row == 1 && color == BLACK)))
			{
				if (((row == 5 || row == 6) && color == WHITE) || ((row == 3 || row == 4) && color == BLACK))
				{
					canEnPassant = true;
				}
			}
			base.Move(row, column);
			if (!isPromoted)
			{
				if (row == 0 || row == 9)
				{
					isPromoted = true;
				}
			}
		}
		public Rule_Hound(int row, int column, PieceColor pieceColor) : base(row, column, pieceColor)
		{
			pieceType = color == WHITE ? WHITEHOUND : BLACKHOUND;        
		}
		public Rule_Hound(Rule_Hound piece) : base(piece) 
		{
			isPromoted = piece.IsPromoted();
			canEnPassant = piece.CanEnPassant();
		}

	}

	public class Rule_Pieces 
	{
		public Rule_Khan khan;
		public Rule_Lion lion;
		public Rule_Terge[] terges = new Rule_Terge[2];
		public Rule_Horse[] horses = new Rule_Horse[2];
		public Rule_Camel[] camels = new Rule_Camel[2];
		public Rule_Guard[] guards = new Rule_Guard[2];
		public Rule_Hound[] hounds = new Rule_Hound[10];

		public List<Rule_Piece> GetAllPieces() 
		{
			List<Rule_Piece> pieces =
			[
				.. terges,
				.. camels,
				..horses,
				..guards,
				khan,
				lion,
				.. hounds,
			];
			return pieces;
		}

		public Rule_Pieces(PieceColor color) 
		{
			int row;
			int houndRow;
			if (color == BLACK)
			{
				row = 0;
				houndRow = 1;
				khan = new Rule_Khan(row, 5, color);
				lion = new Rule_Lion(row, 4, color);
			}
			else
			{
				row = 9;
				houndRow = 8;
				khan = new Rule_Khan(row, 4, color);
				lion = new Rule_Lion(row, 5, color);
			}
			guards[0] = new Rule_Guard(row, 3, color);
			guards[1] = new Rule_Guard(row, 6, color);
			camels[0] = new Rule_Camel(row, 2, color);
			camels[1] = new Rule_Camel(row, 7, color);
			horses[0] = new Rule_Horse(row, 1, color);
			horses[1] = new Rule_Horse(row, 8, color);
			terges[0] = new Rule_Terge(row, 0, color);
			terges[1] = new Rule_Terge(row, 9, color);
			for (int i = 0; i < 10; i++)
			{
				hounds[i] = new Rule_Hound(houndRow, i, color);
			}
		}

		public Rule_Pieces(Rule_Pieces pieces) 
		{
			khan = new Rule_Khan(pieces.khan);
			lion = new Rule_Lion(pieces.lion);
			for (int i = 0; i < 2; i++) 
			{
				terges[i] = new Rule_Terge(pieces.terges[i]);
				horses[i] = new Rule_Horse(pieces.horses[i]);
				camels[i] = new Rule_Camel(pieces.camels[i]);
				guards[i] = new Rule_Guard(pieces.guards[i]);
			}
			for (int i = 0; i < 10; i++) 
			{
				hounds[i] = new Rule_Hound(pieces.hounds[i]);
			}
		}
	}
}
