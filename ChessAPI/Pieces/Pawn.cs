using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Pawn : Piece
    {
            public Pawn(bool isWhite) : base(isWhite,'P') { }

        public override void CalculatePossibleMoves(Tile location, Board board, bool store) { }


    }
}
