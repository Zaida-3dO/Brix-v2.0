using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Bishop : Piece
    {
        public Bishop(bool isWhite) : base(isWhite,'B') { }

        public override void CalculatePossibleMoves(Tile location, Board board, bool store) { }


    }
}
