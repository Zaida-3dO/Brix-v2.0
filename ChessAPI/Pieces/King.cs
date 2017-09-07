using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class King : Piece
    {
        public King(bool isWhite) : base(isWhite,'K') { }
        public override void CalculatePossibleMoves(Tile location, Board board) { }

        public override HashSet<Tile> FindPossibleMoves(Tile location, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
