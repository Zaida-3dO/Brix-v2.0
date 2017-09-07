using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public abstract class Piece
    {
        public Piece(bool isWhite,char notate)
        {
            _isWhite = isWhite;
            _possibleMoves = new HashSet<Tile>();
            _notation = notate;
        }
        /// <summary>
        /// Gets a value indicating whether this Piece is white.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is white; otherwise, <c>false</c>.
        /// </value>
        public bool IsWhite { get { return _isWhite; } }
        protected readonly bool _isWhite;
        /// <summary>
        /// Gets the char notation of the piece.
        /// </summary>
        /// <value>
        /// The notation.
        /// </value>
        public char Notation { get { return _notation; } }
        protected readonly char _notation;
        /// <summary>
        /// Gets the possible place the piece can move to.
        /// </summary>
        /// <value>
        /// The possible moves.
        /// </value>
        //TODO encapsulate
        public HashSet<Tile> PossibleMoves { get { return _possibleMoves; } }
        protected HashSet<Tile> _possibleMoves;
        public abstract void CalculatePossibleMoves(Tile location, Board board);
        public abstract HashSet<Tile> FindPossibleMoves(Tile location, Board board);
    }
}
