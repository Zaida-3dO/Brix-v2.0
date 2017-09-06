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
using ChessAPI;
namespace Play_Brix
{
    public class ButtonListener
    {
        Board board;
        Tile clicked;
        Tile stored;
        bool hasStored;
        Mover mover;
        public ButtonListener(Board board)
        {
            mover = new Mover();   
        }
        public void TileClicked(ImageButton TileClicked)
        {
            clicked = TileFrom(TileClicked);
            //Clicked on the color to play
            //and if it's not empty
            if ((!clicked.IsEmpty)&&(board.whiteToPlay == clicked.Piece.IsWhite))
            {
                stored = clicked;
                hasStored = false;
            }else if (hasStored){
                    if (stored.Piece.PossibleMoves.Contains(clicked))
                    {
                        mover.Move(board, stored, clicked);
                        //refresh(Show possible moves of currently .stored)
                        //move board piece from and to
                    }
                    stored = null;
                    hasStored = false;
             }
               
            }
        
        public Tile TileFrom(ImageButton TileClicked)
        {
            return null;
        }
    }
}