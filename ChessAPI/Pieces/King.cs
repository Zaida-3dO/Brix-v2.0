using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class King : Piece
    {
        public King(bool isWhite) : base(isWhite,'K') { }
        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            return new HashSet<Tile>();
        }
    }
}
