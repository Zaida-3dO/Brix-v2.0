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
        Tile WhiteKing;
        Tile BlackKing;
        //TODO unpublic
        public Stack<Move> PrevMoves;
        Stack<Move> PostMoves;
        Dictionary<string, Tile> tileFromName;
        Dictionary<int, Dictionary<int, Tile>> tileFromNum;
        bool enPassantOnBoard;
        Tile enPassant;
        public bool whiteToPlay;
        public bool gameOn;
       
        //check checkmate stalemate etc
        public string status;
        public List<Piece> CapturedPieces { get { return _capturedPieces.ToList(); } }
        HashSet<Piece> _capturedPieces;
        public Board()
        {
            //Initialize
            PrevMoves = new Stack<Move>();
            PostMoves = new Stack<Move>();
            status = "Start";
            gameOn = true;
            whiteToPlay = true;
            tileFromName = new Dictionary<string, Tile>();
            _capturedPieces = new HashSet<Piece>();
            //Make Tiles with Officials
            Tile a8 = new Tile(8, 1, new Rook(false));
            Tile b8 = new Tile(8, 2, new Knight(false));
            Tile c8 = new Tile(8, 3, true, new Bishop(false));
            Tile d8 = new Tile(8, 4, new Queen(false));
            Tile e8 = new Tile(8, 5, new King(false));
            BlackKing = e8;
            Tile f8 = new Tile(8, 6, new Bishop(false));
            Tile g8 = new Tile(8, 7, true, new Knight(false));
            Tile h8 = new Tile(8, 8, new Rook(false));
            Tile a1 = new Tile(1, 1, new Rook(true));
            Tile b1 = new Tile(1, 2, new Knight(true));
            Tile c1 = new Tile(1, 3, true, new Bishop(true));
            Tile d1 = new Tile(1, 4, new Queen(true));
            Tile e1 = new Tile(1, 5, new King(true));
            WhiteKing = e1;
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
            Revalidate();
        }
        public Board(List<Tile> tiles, bool whiteToPlay)
        {
            this.whiteToPlay = whiteToPlay;
            _capturedPieces = new HashSet<Piece>();
            this.tiles = tiles;
            AddToDictionaries();
            FillUp();
            Validify();
            PrevMoves = new Stack<Move>();
            PostMoves = new Stack<Move>();
        }
        public Board(Board oldBoard)
        {
            tiles = new List<Tile>();
            foreach(Tile tile in oldBoard.tiles)
            {
                tiles.Add(new Tile(tile));
            }
            AddToDictionaries();
            PrevMoves = oldBoard.PrevMoves;
            PostMoves = oldBoard.PostMoves;
            enPassantOnBoard = oldBoard.enPassantOnBoard;
            if (enPassantOnBoard) 
                enPassant = tileFromName[oldBoard.enPassant.TileName];
            whiteToPlay = oldBoard.whiteToPlay;
            gameOn = oldBoard.gameOn;
            status = oldBoard.status;
            _capturedPieces = new HashSet<Piece>();
            foreach (Piece p in oldBoard.CapturedPieces) {
                _capturedPieces.Add(p);
            }
        }
        private void AddToDictionaries()
        {
            tileFromName = new Dictionary<string, Tile>();
            tileFromNum = new Dictionary<int, Dictionary<int, Tile>>();
            foreach(Tile tile in tiles)
            {
                //Store Kings Locations
                if (!tile.IsEmpty&&tile.Piece.Notation == 'K')
                {
                    if (tile.Piece.IsWhite){
                        WhiteKing = tile;
                    } else{
                        BlackKing = tile;
                    }
                }
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
                        tiles.Add(new Tile(i, j));
                    }else if (!tileFromNum[i].ContainsKey(j))
                    {
                        tiles.Add(new Tile(i, j));
                    }
                }
            }
            AddToDictionaries();
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
        //TODO resign or draw
        public void Move(Tile from, Tile to)
        {
            Move move = new Move(this, from, to);
            if (from.Piece.PossibleMoves.Contains(to))
            {
                if (to.IsEmpty)
                {
                    if (to.CanCaptureEnpassant&&from.Piece.Notation=='P') {
                        //Capture the enPassant Piece
                        tileFromNum[from.Rank][to.FileDigit].MoveTo();
                        move.EnPassant =move.Capture= true;
                        
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
                }
                else
                {
                    CapturedPieces.Add(to.Piece);
                    move.Capture = true;
                }
                if ((to.Rank == 1 || to.Rank == 8) && (from.Piece.Notation == 'P'))
                {
                    //TODO promoted Piece = delegate to be Passed In(from.piece.iswhite);
                    Piece prom = new Queen(from.Piece.IsWhite);
                    (new Tile(8, 8, prom)).MoveTo(from);
                    move.Promote = from.Piece.Notation;
                }
               
                from.MoveTo(to);
                //Set king location
                if (to.Piece.Notation == 'K')
                {
                    if (to.Piece.IsWhite){
                        WhiteKing = to;
                    }else {
                        BlackKing = to;
                    }
                }
                    whiteToPlay = !whiteToPlay;

               
                //Reset and Check enPassant
                #region EnPassant
                if (enPassantOnBoard)
                {
                    enPassant.CanCaptureEnpassant = false;
                    enPassantOnBoard = false;
                }
                    if ((to.Piece.Notation == 'P')&&((to.Rank==4&&from.Rank==2)||(to.Rank==5&&from.Rank==7)))
                    {
                        enPassant = tileFromNum[(to.Rank + from.Rank) / 2][to.FileDigit];
                        enPassant.CanCaptureEnpassant = true;
                        enPassantOnBoard = true;
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
                if (Check(whiteToPlay)) {
                    move.Check = true;
                }
                if (!gameOn) {
                    if (CheckMate(whiteToPlay)) {
                        move.Checkmate = true;
                    } else {
                        move.Stalemate = true;
                    }
                  
                }
                move.StoreMove(from,to);
                PrevMoves.Push(move);
                Revalidate();
            } 
        }
        public bool Check(bool checkWhite)
        {
            if(WhiteKing == null)
                return false;
            foreach(Tile tile in tiles) {
                if (!tile.IsEmpty && tile.Piece.IsWhite != checkWhite) {
                    foreach(Tile danger in tile.Piece.FindAllPossibleMoves(tile, this)) {
                        if (GetKing(checkWhite) == danger) {
                            return true;
                        }
                    }
                }
            }
            return false;
            
        }
        public bool MoveIsLegal(Tile from,Tile to)
        {
            //Check if castling through Check
            if (from.FileDigit == 5 && from.Piece.Notation == 'K')
            {
                if (to.FileDigit == 3 )
                {
                    if (from.Rank == 1) {
                        if (!(tileFromNum[1][3].IsSafeHere(true)&& tileFromNum[1][4].IsSafeHere(true) && tileFromNum[1][5].IsSafeHere(true)))
                        {
                            return false;
                        }
                    }else if (from.Rank == 8)
                    {
                        if (!(tileFromNum[8][3].IsSafeHere(false) && tileFromNum[8][4].IsSafeHere(false) && tileFromNum[8][5].IsSafeHere(false)))
                        {
                            return false;
                        }
                    }
                }else if (to.FileDigit == 7)
                {
                    if (from.Rank == 1)
                    {
                        if (!(tileFromNum[1][5].IsSafeHere(true) && tileFromNum[1][6].IsSafeHere(true) && tileFromNum[1][7].IsSafeHere(true)))
                        {
                            return false;
                        }
                    }
                    else if (from.Rank == 8)
                    {
                        if (!(tileFromNum[8][5].IsSafeHere(false) && tileFromNum[8][6].IsSafeHere(false) && tileFromNum[8][7].IsSafeHere(false)))
                        {
                            return false;
                        }
                    }
                }


            }
            //Check if move leaves you in check
            Board temp = new Board(this);
            temp.TryMove(temp.tileFromName[from.TileName], temp.tileFromName[to.TileName]);
            return !temp.Check(temp.tileFromName[to.TileName].Piece.IsWhite);
        }
        private void TryMove(Tile from,Tile to) {

            if (to.IsEmpty) {
                //Enpassant capture
                if (to.CanCaptureEnpassant && from.Piece.Notation == 'P')
                    tileFromNum[from.Rank][to.FileDigit].MoveTo();
                //Castle Rook move
                if (to.CanCastle && (from.Piece.Notation == 'K')) {
                    if (to.FileDigit == 3 && tileFromNum[from.Rank][2].IsEmpty && tileFromNum[from.Rank][3].IsEmpty && tileFromNum[from.Rank][4].IsEmpty)
                        tileFromNum[from.Rank][1].MoveTo(tileFromNum[from.Rank][4]);
                    if (to.FileDigit == 7 && tileFromNum[from.Rank][6].IsEmpty && tileFromNum[from.Rank][7].IsEmpty)
                        tileFromNum[from.Rank][8].MoveTo(tileFromNum[from.Rank][6]);
                }
            }
            //promote Piece to queen;
            if ((to.Rank == 1 || to.Rank == 8) && (from.Piece.Notation == 'P'))
                (new Tile(8, 8, new Queen(from.Piece.IsWhite))).MoveTo(from);
            //Make Move
            from.MoveTo(to);
            //Set king location
            if (to.Piece.Notation == 'K') {
                if (to.Piece.IsWhite) {
                    WhiteKing = to;
                } else {
                    BlackKing = to;
                }
            }
            //Change Turn
            whiteToPlay = !whiteToPlay;
            //Reset and Check enPassant
            #region EnPassant
            if (enPassantOnBoard) {
                enPassant.CanCaptureEnpassant = false;
                enPassantOnBoard = false;
            }
            if ((to.Piece.Notation == 'P') && ((to.Rank == 4 && from.Rank == 2) || (to.Rank == 5 && from.Rank == 7))) {
                enPassant = tileFromNum[(to.Rank + from.Rank) / 2][to.FileDigit];
                enPassant.CanCaptureEnpassant = true;
                enPassantOnBoard = true;
            }
            #endregion
            //Check Castle Rights
            #region Castle
            if (to.Piece.Notation == 'K') {
                if (to.Piece.IsWhite) {
                    //White looses both squares castle rights if a white king was moved
                    tileFromNum[1][3].LostCastleRights();
                    tileFromNum[1][7].LostCastleRights();
                } else {
                    //Black looses both squares castle rights if a black king was moved
                    tileFromNum[8][3].LostCastleRights();
                    tileFromNum[8][7].LostCastleRights();
                }
            } else if (to.Piece.Notation == 'R') {
                //Loose right to castle to the side of a rook that moves. If rook no longer on it's file tho,
                //Then it's false already
                if (from.FileDigit == 1) {
                    if (to.Piece.IsWhite) {
                        tileFromNum[1][3].LostCastleRights();
                    } else {
                        tileFromNum[8][3].LostCastleRights();
                    }
                } else if (from.FileDigit == 8) {
                    if (to.Piece.IsWhite) {
                        tileFromNum[1][7].LostCastleRights();
                    } else {
                        tileFromNum[8][7].LostCastleRights();
                    }
                }
            }
            #endregion
        }
        public void Validify()
        {
            bool HasWhiteKing=false;
            bool HasBlackKing=false;
            bool check = false;
            foreach(Tile tile in tiles)
            {
                for(int i = 1; i <= 8; i++) {
                    if ((!tileFromNum[1][i].IsEmpty) && (tileFromNum[1][i].Piece.Notation == 'P')) {
                        throw new Exception("No Pawns on First Rank");
                    }else if ((!tileFromNum[8][i].IsEmpty) && (tileFromNum[8][i].Piece.Notation == 'P')) {
                        throw new Exception("No Pawns on Last Rank");
                    }
                }
                Revalidate();
                if (!tile.IsEmpty&&tile.Piece.Notation=='K')
                {
                    if (tile.Piece.IsWhite){
                       
                        if (HasWhiteKing){
                            throw new Exception("Can't have more than 1 white king");
                        }else {
                            HasWhiteKing = true;
                        }
                        if (!tile.IsSafeHere(true)) {
                            if (check == true) {
                                throw new Exception("Can't have 2 kings on check");
                            } else {
                                check = true;
                                whiteToPlay = true;
                            }
                        }
                    } else {
                        if (HasBlackKing) {
                            throw new Exception("Can't have more than 1 black king");
                        } else {
                            HasBlackKing = true;
                        }
                        if (!tile.IsSafeHere(false)) {
                            if (check == true) {
                                throw new Exception("Can't have 2 kings on check");
                            } else {
                                check = true;
                                whiteToPlay = false;
                            }
                        }
                    }
                }
            }
            if(HasBlackKing!=HasWhiteKing)
                throw new Exception("Must have the same number of Black and white kings");
        }
        public bool CheckMate(bool CheckWhite){
            foreach(Tile tile in tiles) {
                if (!tile.IsEmpty && (tile.Piece.IsWhite == CheckWhite)) {
                    if (tile.Piece.PossibleMoves.Count > 0) {
                        return false;
                        //There is no mate if any of his pieces has a legal move
                    }
                }
            }
            //If we are still here there is no legal move
            //Retrun false if he is safe indicating stale, true indicates Checkmate
            return !GetKing(CheckWhite).IsSafeHere(CheckWhite);
        }
        Tile GetKing(bool ColorisWite) {
            if (ColorisWite)
                return WhiteKing;
            return BlackKing;
        }
        public void Revalidate() {
            //TODO revalidate wherever board changes
            foreach (Tile t in tiles)
            {
                t.SafetyReset();
            }
                foreach (Tile t in tiles){
                if (!t.IsEmpty) {
                    t.Piece.StorePossibleMoves(t,this);
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
        public void ClearHighlights()
        {
            foreach (Tile t in tiles)
            {
                t.IsHighlighted = false;
            }
            //Remove all Highlights
        }

    }
}