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
        public bool Capture;
        public char Promote;
        public bool Checkmate;
        public bool Stalemate;
        public bool Check;
        // public double[] times
        //1-0 or 1/2-1/2 when game ends
        public bool GameOver;
        //Check ambiguity
        //Move notation, Board b4 and afta
        public void StoreMove() { }
        public Move(Board b,Tile from,Tile to)
        {

        }
    }
}