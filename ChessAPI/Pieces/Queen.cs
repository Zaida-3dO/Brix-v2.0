using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite,'Q') { }
        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            return new HashSet<Tile>();
        }

    }
}
