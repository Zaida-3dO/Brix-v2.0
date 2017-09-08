﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Rook : Piece
    {
        public Rook(bool isWhite) : base(isWhite,'R') { }
        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            return new HashSet<Tile>();
        }


    }
}
