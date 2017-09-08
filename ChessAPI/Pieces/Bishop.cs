using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite,'B') { }

        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            return new HashSet<Tile>();
        }
    }
}
