using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Queen : Piece
    {
        public Queen(bool isWhite) : base(isWhite,'Q') { }
        public override void CalculatePossibleMoves(Tile location, Board board, bool store) { }


    }
}
