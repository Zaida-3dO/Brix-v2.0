using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite,'Q') { }
        public override void CalculatePossibleMoves(Tile location, Board board) { }

        public override HashSet<Tile> FindPossibleMoves(Tile location, Board board)
        {
            throw new NotImplementedException();
        }


    }
}
