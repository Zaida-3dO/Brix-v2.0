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
        List<Tile> tiles;
        //TODO stack moves
        Dictionary<string, Tile> tileFromName;
        Dictionary<int, Dictionary<int, Tile>> tileFromNum;
        public bool whiteToPlay;
        public bool gameOn;
        //check checkmate stalemate etc
        public string status;
        public HashSet<Piece> CapturedPieces;
        public Board()
        {
            //Initialize
            status = "Start";
            gameOn = true;
            whiteToPlay = true;
            tileFromName = new Dictionary<string, Tile>();
            CapturedPieces = new HashSet<Piece>();
            //Make Tiles with Officials
            Tile a8 = new Tile(8, 1, new Rook(false));
            Tile b8 = new Tile(8, 2, new Knight(false));
            Tile c8 = new Tile(8, 3, true, new Bishop(false));
            Tile d8 = new Tile(8, 4, new Queen(false));
            Tile e8 = new Tile(8, 5, new King(false));
            Tile f8 = new Tile(8, 6, new Bishop(false));
            Tile g8 = new Tile(8, 7, true, new Knight(false));
            Tile h8 = new Tile(8, 8, new Rook(false));
            Tile a1 = new Tile(1, 1, new Rook(true));
            Tile b1 = new Tile(1, 2, new Knight(true));
            Tile c1 = new Tile(1, 3, true, new Bishop(true));
            Tile d1 = new Tile(1, 4, new Queen(true));
            Tile e1 = new Tile(1, 5, new King(true));
            Tile f1 = new Tile(1, 6, new Bishop(true));
            Tile g1 = new Tile(1, 7, true, new Knight(true));
            Tile h1 = new Tile(1, 8, new Rook(true));
            //Add created tiles to List
            tiles = new List<Tile> { a1,b1,c1,d1,e1,f1,g1,h1,a8,b8,c8,d8,e8,f8,g8,h8};
            //Create Tiles with Pawns and add to list
            for (int i = 1; i <= 8; i++)
            {
                Tile bPawn = new Tile(7, i, new Pawn(false));
                Tile wPawn = new Tile(2, i, new Pawn(true));
                tiles.Add(bPawn);tiles.Add(wPawn);
            }
            //Creates and adds Empty Tiles
            for (int i = 3; i <= 6; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    Tile mt = new Tile(i, j);
                    tiles.Add( mt);
                }
            }
            //Add to the Boards dictionaries
            AddToDictionaries();
           
        }
        public Board(List<Tile> tiles, bool whiteToPlay)
        {
            this.whiteToPlay = whiteToPlay;
            CapturedPieces = new HashSet<Piece>();
            this.tiles = tiles;
            AddToDictionaries();
            FillUp();
            Validify();
        }
        public Board(Board oldBoard)
        {
            //TODO DeepCopy
        }
        private void AddToDictionaries()
        {
            tileFromName = new Dictionary<string, Tile>();
            tileFromNum = new Dictionary<int, Dictionary<int, Tile>>();
            foreach(Tile tile in tiles)
            {
                //Add to Name Dictionary
                tileFromName.Add(tile.TileName, tile);
                //Add to Num Dictionary
                if (!tileFromNum.ContainsKey(tile.Rank))
                    tileFromNum.Add(tile.Rank, new Dictionary<int, Tile>());
                tileFromNum[tile.Rank].Add(tile.FileDigit, tile);
            }
        }
        private void FillUp()
        {
            //Fills up with empty tiles
            for (int i = 1; i <= 8; i++)
            {

                for (int j = 1; j <= 8; j++)
                {
                    if (!tileFromNum.ContainsKey(i))
                    {
                        tileFromNum.Add(i, new Dictionary<int, Tile>());
                    }
                    if (!tileFromNum[i].ContainsKey(j))
                    {
                        tileFromNum[j].Add(j, new Tile(i, j));
                    }
                }
            }
        }
        public Tile[] GetTiles()
        {
            return tiles.ToArray();
        }
        public Tile GetTile(string Name)
        {
            return tileFromName[Name];
        }
        /// <summary>
        /// Gets the tile.e.g to get e4  = GetTile(4,5)
        /// </summary>
        /// <param name="rank">The rank.</param>
        /// <param name="file">The file.</param>
        /// <returns></returns>
        public Tile GetTile(int rank,int file)
        {
            return tileFromNum[rank][file];
        }
        private bool enPassantOnBoard;
        private Tile enPassant;
        public void Move(Tile from, Tile to)
        {
            Revalidate();
            Move move = new Move(this, from, to);
            if (from.Piece.PossibleMoves.Contains(to))
            {
                if (to.IsEmpty)
                {
                    if (to.CanCaptureEnpassant&&from.Piece.Notation=='P') {
                        //Capture the enPassant Piece
                        tileFromNum[from.Rank][to.FileDigit].MoveTo();
                        move.EnPassant = true;
                    }
                    if (to.CanCastle && (from.Piece.Notation == 'K'))
                    {
                        //If you can still legally castle and all items between rook and King are empty, then move rook to it's destination
                        if (to.FileDigit == 3 && tileFromNum[from.Rank][2].IsEmpty && tileFromNum[from.Rank][3].IsEmpty && tileFromNum[from.Rank][4].IsEmpty)
                        {
                            tileFromNum[from.Rank][1].MoveTo(tileFromNum[from.Rank][4]);
                            move.QCastle = true;
                        }
                        if (to.FileDigit == 7 && tileFromNum[from.Rank][6].IsEmpty && tileFromNum[from.Rank][7].IsEmpty)
                        {
                            tileFromNum[from.Rank][8].MoveTo(tileFromNum[from.Rank][6]);
                            move.KCastle = true;
                        }
                    }
                    if ((to.Rank == 1 || to.Rank == 8) && (from.Piece.Notation == 'P')){
                        //TODO promoted Piece = delegate to be Passed In(from.piece.iswhite);
                        Piece prom = new Queen(from.Piece.IsWhite);
                        (new Tile(8, 8, prom)).MoveTo(from);
                        move.Promote = from.Piece.Notation;
                    }
                }
                else
                {
                    CapturedPieces.Add(to.Piece);
                    move.Capture = true;
                }
                
                from.MoveTo(to);
                whiteToPlay = !whiteToPlay;
                //Reset and Check enPassant
                #region EnPassant
                    if (enPassantOnBoard)
                        enPassant.CanCaptureEnpassant = false;
                    if ((to.Piece.Notation == 'P')&&((to.Rank==4&&from.Rank==2)||(to.Rank==5&&from.Rank==7)))
                    {
                        enPassant = tileFromNum[(to.Rank + from.Rank) / 2][to.FileDigit];
                        enPassant.CanCaptureEnpassant = true;
                    }
                #endregion
                //Check Castle Rights
                #region Castle
                    if (to.Piece.Notation == 'K')
                    {
                        if (to.Piece.IsWhite)
                        {
                            //White looses both squares castle rights if a white king was moved
                            tileFromNum[1][3].LostCastleRights();
                            tileFromNum[1][7].LostCastleRights();
                        }
                        else
                        {
                            //Black looses both squares castle rights if a black king was moved
                            tileFromNum[8][3].LostCastleRights();
                            tileFromNum[8][7].LostCastleRights();
                        }
                    }
                    else if (to.Piece.Notation == 'R')
                    {
                        //Loose right to castle to the side of a rook that moves. If rook no longer on it's file tho,
                        //Then it's false already
                        if (from.FileDigit == 1)
                        {
                            if (to.Piece.IsWhite) { 
                                tileFromNum[1][3].LostCastleRights();
                            }else{
                                tileFromNum[8][3].LostCastleRights();
                            }
                        }else if (from.FileDigit == 8){
                            if (to.Piece.IsWhite) { 
                                tileFromNum[1][7].LostCastleRights();
                            }else{ 
                                tileFromNum[8][7].LostCastleRights();
                            }
                        }
                    }
                #endregion
                SetStatus(move);
                move.GameOver = !gameOn;
                move.StoreMove();
            }
        }
        public void Validify()
        {
            //TODO ensure board is valid,pawns on edge, Kings on check e.t.c
        }
        public void SetStatus(Move move)
        {
           //Check mate and stale
            //TODO status and gameOn
        }
        public void Revalidate() {
            foreach (Tile t in tiles)
            {
                t.SafetyReset();
            }
                foreach (Tile t in tiles){
                if (!t.IsEmpty) {
                    t.Piece.CalculatePossibleMoves(t,this);
                    foreach(Tile tt in t.Piece.PossibleMoves)
                    {
                        tt.NotSafeFor(!t.Piece.IsWhite);
                    }
                }
            }
        }
        public void Revalidate(Tile t)
        {
            foreach (Tile tt in tiles)
            {
                tt.IsHighlighted = (!t.IsEmpty && t.Piece.PossibleMoves.Contains(tt));
            }
            //Highlight only Tiles that can be reach by this piece
        }
       
    }
}