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
    public class Board 
    {
        //public Tile[,] State;
        public List<List<Tile>> tiles;
        public bool whiteToPlay;
        public HashSet<Piece> CapturedPieces;
        public Board(){
            //State = new Tile[8,8];
            Tile a8 = new Tile(1,1,new Rook());
            //TODO setcolor in piece constructor
            //set piece to be set internally by moveto function in tile
            List<Tile> file = new List<Tile>();
            file.Add(a8);
            Tile b8 = new Tile(1, 1, new Knight());
            file.Add(b8);
            //...

        }
        public Board(List<List<Tile>> tiles,bool whiteToPlay)
        {
            
        }
        public Board(Board oldBoard)
        {
            //TODO DeepCopy
        }
    }
}