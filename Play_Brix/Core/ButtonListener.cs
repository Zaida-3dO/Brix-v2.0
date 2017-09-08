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
        View view;
        Tile clicked;
        Tile stored;
        bool hasStored;
        public ButtonListener(View view){
            this.view = view;
        }
        public EventHandler TileClicked(ImageButton tileBtn)
        {
            
            return delegate {
                clicked = view.btnMap[tileBtn];
                //Clicked on the color to play
                //and if it's not empty
                if ((!clicked.IsEmpty)&&(view.board.whiteToPlay == clicked.Piece.IsWhite))
            {
                   
                stored = clicked;
                hasStored = true;
                    view.board.Revalidate(stored);
                }
                else if (hasStored){

             //       if (stored.Piece.PossibleMoves.Contains(clicked)){
                        view.board.Move( stored, clicked);
              //      }
                    stored = null;
                    hasStored = false;
                    view.board.ClearHighlights();
             }//No need for else...trust me
                
                view.Refresh();
            };
        }
        public EventHandler TileClickedTest(Board board,TextView TV)
        {
            return delegate { TV.Text = (board.PrevMoves.Count!=0)?board.PrevMoves.Peek().Notation:"Start of Game"; };
        }

           
    }
}