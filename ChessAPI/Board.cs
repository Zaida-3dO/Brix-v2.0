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
        public Dictionary<string,Tile> tile;
        public bool whiteToPlay;
        public HashSet<Piece> CapturedPieces;
        public Board(){
            //State = new Tile[8,8];
            Tile a8 = new Tile(8,1,new Rook(false));
            Tile b8 = new Tile(8, 2, new Knight(false));
            //TODO setcolor in piec2 constructor
            //set piece to be set internally by moveto function in tile
            List<Tile> file = new List<Tile>
            {
                a8,
                b8
              //  new Tile(1, 1, new Knight(true))
            };
            tile.Add(a8.TileName, a8);
            tile.Add(b8.TileName, b8);
            //...



            //Make em
            Tile c8 = new Tile(8, 3, new Bishop(false));
            Tile d8 = new Tile(8, 4, new Queen(false));
            Tile e8 = new Tile(8, 5, new King(false));
            Tile f8 = new Tile(8, 6, new Bishop(false));
            Tile g8 = new Tile(8, 7, new Knight(false));
            Tile h8 = new Tile(8, 8, new Rook(false));
            tile.Add(c8.TileName, c8);
            tile.Add(d8.TileName, d8);
            tile.Add(e8.TileName, e8);
            tile.Add(f8.TileName, f8);
            tile.Add(g8.TileName, g8);
            tile.Add(h8.TileName, h8);
            for (int i = 1; i <= 8; i++)
            {
                Tile bPawns = new Tile(7, i, new Pawn(false));
                Tile wPawns = new Tile(2, i, new Pawn(true));
                tile.Add(bPawns.TileName, bPawns);
                tile.Add(wPawns.TileName, wPawns);
            }
            for(int i = 3; i <= 6; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Tile mt = new Tile(i, j);
                    tile.Add(mt.TileName, mt);
                }
            }
            Tile a1 = new Tile(1, 1, new Rook(true));
            Tile b1 = new Tile(1, 2, new Knight(true));
            Tile c1 = new Tile(1, 3, new Bishop(true));
            Tile d1 = new Tile(1, 4, new Queen(true));
            Tile e1 = new Tile(1, 5, new King(true));
            Tile f1 = new Tile(1, 6, new Bishop(true));
            Tile g1 = new Tile(1, 7, new Knight(true));
            Tile h1 = new Tile(1, 8, new Rook(true));
            tile.Add(a1.TileName, a1);
            tile.Add(b1.TileName, b1);
            tile.Add(c1.TileName, c1);
            tile.Add(d1.TileName, d1);
            tile.Add(e1.TileName, e1);
            tile.Add(f1.TileName, f1);
            tile.Add(g1.TileName, g1);
            tile.Add(h1.TileName, h1);
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