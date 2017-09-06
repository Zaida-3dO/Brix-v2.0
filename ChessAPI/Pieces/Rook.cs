using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base(isWhite,'R') { }
        public override void CalculatePossibleMoves(Tile location, Board board, bool store) { }


    }
}
