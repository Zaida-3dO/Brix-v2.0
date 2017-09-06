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
        public ButtonListener(Board board)
        {
            this.board = board;
        }
        public EventHandler TileClicked(string TileClicked,TextView TV)
        {
            
            return delegate {
                clicked = board.tile[TileClicked];
                View.HighlightAll(board, clicked);
                //Clicked on the color to play
                //and if it's not empty
                if ((!clicked.IsEmpty)&&(board.whiteToPlay == clicked.Piece.IsWhite))
            {
                stored = clicked;
                hasStored = true;
                    TV.Text = "Stored "+stored.Piece.Notation+" in " + stored.TileName;

            }else if (hasStored){
                    TV.Text = "Trying to move " + stored.Piece.Notation + " from " + stored.TileName + " to " + clicked.TileName+"Switching turn";
            //        board.whiteToPlay = !board.whiteToPlay;

             //       if (stored.Piece.PossibleMoves.Contains(clicked)){
                        board.Move( stored, clicked);
              //      }
                    stored = null;
                    hasStored = false;
             }
                View.Refresh(board);
                board.Revalidate();
            };
        }
        public EventHandler TileClickedTest(ImageButton TileClicked,TextView TV)
        {
            return delegate { TV.Text= TileClicked.Id.ToString(); };
        }

           
    }
}