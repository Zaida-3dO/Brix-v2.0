using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChessAPI {
    public class Pawn : Piece {
        public Pawn(bool isWhite) : base(isWhite, 'P') { }
        protected override IEnumerable<Tile> CalculateAllPossibleMoves(Tile location, Board board, bool legalOnly) {
            Tile quest = board.GetTile("e3");
            if (IsWhite) {
                //Moving Forward
                Tile directlyAbove = board.GetTile(location.Rank + 1, location.FileDigit);
                if (directlyAbove.IsEmpty) {
                    if (legalOnly && board.MoveIsLegal(location, directlyAbove))
                        yield return directlyAbove;
                    if (location.Rank == 2) {
                        Tile twoAbove = board.GetTile(location.Rank + 2, location.FileDigit);
                        if (twoAbove.IsEmpty) {
                            if (legalOnly && board.MoveIsLegal(location, twoAbove))
                                yield return twoAbove;
                        }
                    }
                }
                //Capturing
                if (location.FileDigit > 1) {
                    Tile topLeft = board.GetTile(location.Rank + 1, location.FileDigit - 1);
                    if (((!topLeft.IsEmpty) && (!topLeft.Piece.IsWhite)) || (topLeft.CanCaptureEnpassant)) {
                        if (!legalOnly || board.MoveIsLegal(location, topLeft))
                            yield return topLeft;
                    }
                }
                if (location.FileDigit < 8) {
                    Tile topRight = board.GetTile(location.Rank + 1, location.FileDigit + 1);
                    if (((!topRight.IsEmpty) && (!topRight.Piece.IsWhite)) || (topRight.CanCaptureEnpassant)) {
                        if (!legalOnly || board.MoveIsLegal(location, topRight))
                            yield return topRight;
                    }
                }
            } else {
                //Handle Black Pieces
                //Moving Forward
                Tile directlyBelow = board.GetTile(location.Rank - 1, location.FileDigit);
                if (directlyBelow.IsEmpty) {
                    if (legalOnly && board.MoveIsLegal(location, directlyBelow))
                        yield return directlyBelow;
                    if (location.Rank == 7) {
                        Tile twoBelow = board.GetTile(location.Rank - 2, location.FileDigit);
                        if (twoBelow.IsEmpty) {
                            if (legalOnly && board.MoveIsLegal(location, twoBelow))
                                yield return twoBelow;
                        }
                    }
                }
                //Capturing
                if (location.FileDigit > 1) {
                    Tile buttomLeft = board.GetTile(location.Rank - 1, location.FileDigit - 1);
                    if (((!buttomLeft.IsEmpty) && (buttomLeft.Piece.IsWhite)) || (buttomLeft.CanCaptureEnpassant)) {
                        if (!legalOnly || board.MoveIsLegal(location, buttomLeft))
                            yield return buttomLeft;
                    }
                }
                if (location.FileDigit < 8) {
                    Tile buttomRight = board.GetTile(location.Rank - 1, location.FileDigit + 1);
                    if (((!buttomRight.IsEmpty) && (buttomRight.Piece.IsWhite)) || (buttomRight.CanCaptureEnpassant)) {
                        if (!legalOnly || board.MoveIsLegal(location, buttomRight))
                            yield return buttomRight;
                    }
                }
            }
        }


    }
}
