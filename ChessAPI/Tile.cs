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
        public Tile(int rank, int file)
        {
            if (rank >= 1 && rank <= 8 && file >= 1 && file <= 8)
            {
                this._rank = rank;
                this._fileAsNum = file;
                this._file = Let(file);
                _isEmpty = true;
                _enPassant = false;
                _canCastle = false;
                IsHighlighted = false;
                _tileName = _file + "" + rank;
                _isLightSquare = !((rank + file) % 2 == 0);
            }
            else
            {
                throw new IndexOutOfRangeException("Rank or File has exceeded uper limit..8");
            }

        }
        public Tile(int rank, int file,Piece piece):this(rank,file)
        {
            _piece = piece;
            _isEmpty = false;
        }
     
        public Tile(int rank, int file, bool castle,Piece piece):this(rank,file)
        {
            _canCastle = castle;
            _piece = piece;
            _isEmpty = false;
        }
        public Tile(int rank, int file,bool castle):this(rank,file)
        {
            _canCastle = castle;
        }
        /// <summary>
        /// Gets the piece currently on the Tile.
        /// </summary>
        /// <value>
        /// The piece.
        /// </value>
        public Piece Piece { get { return _piece; } }
        Piece _piece;
        /// <summary>
        /// Gets a value indicating whether this tile is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        public bool IsEmpty { get { return _isEmpty; } }
        bool _isEmpty;
        /// <summary>
        /// Gets a value indicating whether this a player can capture enpassant on this tile.
        /// Exclusive to 3rd and 6th Rank.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can capture enpassant; otherwise, <c>false</c>.
        /// </value>
        public bool CanCaptureEnpassant { get { return _enPassant; } }
        bool _enPassant;
        /// <summary>
        /// Gets a value indicating whether this Tile is highlighted, for display purposes, e.g possible move highlighting.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is highlighted; otherwise, <c>false</c>.
        /// </value>
        public bool IsHighlighted { get; set; }
        /// <summary>
        /// Gets a value indicating whether this instance is light tile.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is light tile; otherwise, <c>false</c>.
        /// </value>
        public bool IsLightTile { get { return _isLightSquare; } }
        private readonly bool _isLightSquare;
        /// <summary>
        /// Gets the name of the tile e.g e4 d5.
        /// </summary>
        /// <value>
        /// The name of the tile.
        /// </value>
        public string TileName { get { return _tileName; } }
        private readonly string _tileName;
        /// <summary>
        /// Gets the file letter, like the 'a' file.
        /// </summary>
        /// <value>
        /// The file letter.
        /// </value>
        public char File { get { return _file; } }
        private readonly char _file;
        /// <summary>
        /// Gets the file as a digit.
        /// </summary>
        /// <value>
        /// The file digit.
        /// </value>
        public int FileDigit { get { return _fileAsNum; } }
        private readonly int _fileAsNum;
        /// <summary>
        /// Gets the rank this tile is in.
        /// </summary>
        /// <value>
        /// The rank.
        /// </value>
        public int Rank { get { return _rank; } }
        private readonly int _rank;
        /// <summary>
        /// Gets a value indicating whether this tile can still be castled on.
        /// Exclusive to c1,g1,c8,g8
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance can castle; otherwise, <c>false</c>.
        /// </value>
        public bool CanCastle { get { return _canCastle; } }
        bool _canCastle;



        // public Tuple<bool,bool> IsHesafe;
        public bool IsWhiteSafeHere { get; set; }
        public bool IsBlackSafeHere { get; set; }
        public static char Let(int num)
        {
            switch (num)
            {
                case 1:
                    return 'a';
                case 2:
                    return 'b';
                case 3:
                    return 'c';
                case 4:
                    return 'd';
                case 5:
                    return 'e';
                case 6:
                    return 'f';
                case 7:
                    return 'g';
                case 8:
                    return 'h';
            }
            throw new IndexOutOfRangeException("File out of range");
        }
        public void MoveTo(Tile to)
        {
            to.Play(this.Piece);
            this._piece = null;
            _isEmpty = true;
            

        }
        private void Play(Piece p)
        {
            this._piece = p;
            _isEmpty = false;
            //TODO Check if Promoting
        }
    }
}