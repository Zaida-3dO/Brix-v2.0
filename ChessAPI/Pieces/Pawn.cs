using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI
{
    public class Pawn : Piece
    {
            public Pawn(bool isWhite) : base(isWhite,'P') { }
        

        public override void CalculatePossibleMoves(Tile location, Board board)
        {
            _possibleMoves = new HashSet<Tile>();
            if (IsWhite)
            {
                //Moving Forward
                Tile directlyAbove = board.GetTile(location.Rank + 1, location.FileDigit);
                if (directlyAbove.IsEmpty)
                {
                    _possibleMoves.Add(directlyAbove);
                    if (location.Rank == 2)
                    {
                        Tile twoAbove = board.GetTile(location.Rank + 2, location.FileDigit);
                        if (twoAbove.IsEmpty)
                        {
                            _possibleMoves.Add(twoAbove);
                        }
                    }
                }
                //Capturing
                if (location.FileDigit > 1)
                {
                    Tile topLeft = board.GetTile(location.Rank + 1, location.FileDigit - 1);
                    if ((!topLeft.IsEmpty) && (!topLeft.Piece.IsWhite)){
                        _possibleMoves.Add(topLeft);
                    }
                }
                if (location.FileDigit < 8)
                {
                    Tile topRight = board.GetTile(location.Rank + 1, location.FileDigit + 1);
                    if ((!topRight.IsEmpty) && (!topRight.Piece.IsWhite))
                    {
                        _possibleMoves.Add(topRight);
                    }
                }
            }
            else
            {
                //Handle Black Pieces
                //Moving Forward
                Tile directlyBelow = board.GetTile(location.Rank - 1, location.FileDigit);
                if (directlyBelow.IsEmpty)
                {
                    _possibleMoves.Add(directlyBelow);
                    if (location.Rank == 7)
                    {
                        Tile twoBelow = board.GetTile(location.Rank - 2, location.FileDigit);
                        if (twoBelow.IsEmpty)
                        {
                            _possibleMoves.Add(twoBelow);
                        }
                    }
                }
                //Capturing
                if (location.FileDigit > 1)
                {
                    Tile buttomLeft = board.GetTile(location.Rank - 1, location.FileDigit - 1);
                    if ((!buttomLeft.IsEmpty) && (buttomLeft.Piece.IsWhite))
                    {
                        _possibleMoves.Add(buttomLeft);
                    }
                }
                if (location.FileDigit < 8)
                {
                    Tile buttomRight = board.GetTile(location.Rank - 1, location.FileDigit + 1);
                    if ((!buttomRight.IsEmpty) && (buttomRight.Piece.IsWhite))
                    {
                        _possibleMoves.Add(buttomRight);
                    }
                }
            }
        }

        public override HashSet<Tile> FindPossibleMoves(Tile location, Board board)
        {
            throw new NotImplementedException();
        }
    }
}
