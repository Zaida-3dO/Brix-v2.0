using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ChessAPI
{
    public class Tile
    {
        public Tile(int rank, int file,Piece piece)
        {

        }
        public Tile(int rank, int file)
        {

        }
        public Tile(int rank, int file, bool castle,Piece piece)
        {

        }
        public Tile(int rank, int file,bool castle)
        {

        }
        public Piece Piece { get; }
        public bool IsEmpty { get; }
        /// <summary>
        /// Gets a value indicating whether this a player can capture enpassant on this tile.
        /// Exclusive to 3rd and 6th Rank.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can capture enpassant; otherwise, <c>false</c>.
        /// </value>
        public bool CanCaptureEnpassant { get; }
        public bool IsHighlighted { get; }
        public bool IsLightTile { get; }
        /// <summary>
        /// Gets the name of the tile e.g e4 d5.
        /// </summary>
        /// <value>
        /// The name of the tile.
        /// </value>
        public string TileName { get; }
        public char File { get; }
        public int FileDigit { get; }
        public int Rank { get; }
        /// <summary>
        /// Gets a value indicating whether this tile can still be castled on.
        /// Exclusive to c1,g1,c8,g8
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can castle; otherwise, <c>false</c>.
        /// </value>
        public bool CanCastle { get; }

       // public Tuple<bool,bool> IsHesafe;
       public bool IsWhiteSafeHere { get; }
        public bool IsBlackSafeHere { get; }
    }
}