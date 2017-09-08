using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Knight : Piece
    {
        public Knight(bool isWhite) : base(isWhite,'N') { }
        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            return new HashSet<Tile>();
        }


    }
}
