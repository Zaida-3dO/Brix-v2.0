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
    public class Move
    {
        public bool EnPassant;
        public bool KCastle;
        public bool QCastle;
        public bool WhiteWins;
        public bool draw;
        public bool Capture;
        public char Promote;
        public bool Checkmate;
        public bool Stalemate;
        public bool Check;
        public Board b4;
        public Tile from;
        public Tile to;
        char Piece;
        public string Notation;
        // public double[] times??
        //Check ambiguity
        public void StoreMove(Tile from,Tile to) {

            if (KCastle) {
                Notation = "O-O";
            } else if (QCastle) {
                Notation = "O-O-O";
            } else {
                if (Piece != 'P') {
                    Notation = Piece.ToString();

                    string ambiguosFile = "";
                    string ambiguosRank = "";
                    foreach (Tile t in to.Piece.PossibleMoves) {
                        if (t.IsEmpty) {
                            if ((t.Piece.Notation == to.Piece.Notation) && (t.Piece.IsWhite == to.Piece.IsWhite)) {
                                if (t.Rank == from.Rank) {
                                    ambiguosFile = from.File.ToString();
                                }
                                if (t.FileDigit == from.FileDigit) {
                                    ambiguosRank = from.Rank + "";
                                }
                            }
                        }
                    }

                    Notation += ambiguosFile + ambiguosRank;
                }
                if (Capture) {
                    if (Piece == 'P') {
                        Notation = from.File.ToString();
                    }
                    Notation += "x";
                }
                Notation += to.TileName;
                if (EnPassant) {
                    Notation += "e.n";
                }
                if (Promote != ' ') {
                    Notation += "="+Promote.ToString();
                }
            }
            if (Checkmate) {
                Notation += "#";
            }else if (Stalemate) {
                Notation += "-";
                //TODO check if = is stalemate
            } else if(Check) {
                Notation += "+";
            }
        }
        public Move(Board b,Tile from,Tile to)
        {
            EnPassant=false;
            KCastle=false;
            QCastle=false;
            WhiteWins=false;
            draw=false;
            Capture=false;
            Checkmate=false;
            Stalemate=false;
            Check=false;
            Notation="";

        b4 = new Board(b);
            this.from = b4.GetTile(from.TileName);
            this.to = b4.GetTile(to.TileName);
            this.Piece=from.Piece.Notation;

        }
        public Move(Board b,bool WhiteWins,bool draw) {
            b4 = new Board(b);
            Promote = ' ';
            if (draw) {
                Notation = "½-½";
            } else {
                if (WhiteWins) {
                    Notation = "1-0";
                } else {
                    Notation = "0-1";
                }
            }
        }
    }
}