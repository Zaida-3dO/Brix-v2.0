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
    public class View
    {
        public static List<ImageButton> btnList;
        public static void Refresh(Board board)
        {
            foreach (ImageButton btn in btnList)
            {
                Tile square = board.tile[NameOfId(btn.Id)];
                btn.SetImageResource(ImageFor(square));
            }
        }
        static int ImageFor(Tile tile)
        {
            if (tile.IsEmpty)
            {
                //TODO HighLighted
                if (tile.IsHighlighted)
                {
                    return 2130837511;
                }
                return 2130837513;
            }else if (tile.Piece.Notation == 'P'){
                if (tile.Piece.IsWhite)
                {
                    return 2130837517;
                }
                    return 2130837507;
            }
            else if (tile.Piece.Notation == 'R')
            {
                if (tile.Piece.IsWhite)
                {
                    return 2130837519;
                }
                return 2130837509;
            }
            else if (tile.Piece.Notation == 'N')
            {
                if (tile.Piece.IsWhite)
                {
                    return 2130837516;
                }
                return 2130837506;
            }
            else if (tile.Piece.Notation == 'B')
            {
                if (tile.Piece.IsWhite)
                {
                    return 2130837514;
                }
                return 2130837504;
            }
            else if (tile.Piece.Notation == 'Q')
            {
                if (tile.Piece.IsWhite)
                {
                    return 2130837518;
                }
                return 2130837508;
            }
            else if (tile.Piece.Notation == 'K')
            {
                if (tile.Piece.IsWhite)
                {
                    return 2130837515;
                }
                return 2130837505;
            }
            return 2130837512;
        }
        public static string NameOfId(int id)
        {
            int num = id - 2131034113;
            char file = Tile.Let((num % 8) + 1);
            int rank = (8 - (num / 8));
            return file + "" + rank;

        }
        public static void HighlightAll(Board b,Tile t)
        {
            foreach(Tile tt in b.tile.Values.ToArray())
            {
                tt.IsHighlighted = (!t.IsEmpty && t.Piece.PossibleMoves.Contains(tt));
            }
        }
    }
}