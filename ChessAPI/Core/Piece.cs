using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public abstract class Piece
    {
        /// <summary>
        /// Gets a value indicating whether this Piece is white.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is white; otherwise, <c>false</c>.
        /// </value>
        public bool IsWhite { get; }
        /// <summary>
        /// Gets the char notation of the piece.
        /// </summary>
        /// <value>
        /// The notation.
        /// </value>
        public char Notation { get; }
        public HashSet<Tile> PossibleMoves { get; }
        public abstract void CalculatePossibleMoves(Tile location, Board board, bool store);

    }
}
