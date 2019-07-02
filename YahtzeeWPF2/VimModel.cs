﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{

    /// <summary>
    /// Generated by Vim for Vis
    /// </summary>
    enum HighlightStyle
    {
        Filled = 0,
        Open,
        Scratch,
        Points,
        BestChoice,
        // What the player is choosing, OR enforced take 5OK or 5Str. 
        Insist
    }


    static class VimModel
    {
        // Fields

        // Properties


        #region Methods

        /* Button appears when the Commit button should have a second option.
         */
        public static void AntiCommitClicked ()
        { }

        /* R
         * 
         * 
         */
        public static void CommitClicked ()
        {
            

        }


        // Toggle die.Held
        public static void DieClicked ()
        {

        }


        // 
        public static void NewGameClicked ()
        {

        }


        /* Visual should call a dialog/ subwindow; and then 
         * pass changes made to here. 
         */
        public static void OptionsClicked ()
        {

        }


        /* Visual should pass info on row clicked
         * 
         * Update takescore row highlights
         */
        public static void RowClicked ()
        {

        }







        #endregion Methods













        //TODO:  Refactor this into VimModel
        /*  Move GetTakeScoreString into Vim
         
         */

        //static void BuildGameRows ()
        //{
        //    GameRows = new List<GameRow> ();
        //    for ( int _row = 0; _row < 13; _row++ )
        //    {
        //        var _gameRow = new GameRow ();
        //        GameRows.Add ( _gameRow );

        //        if ( !scoringRowsOpen [ _row ] )
        //            continue;

        //        if ( pointsList [ _row ] > 0 )
        //        {
        //            //_gameRow.RowHighlight = GameRow.HighlightStyle.Points;
        //            _gameRow.RowHighlight = HighlightStyle.Points;
        //            _gameRow.TakeScoreValue = pointsList [ _row ];
        //            _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
        //            _gameRow.TakeScoreVisible = true;
        //        }
        //        else if ( GameModel.GameClock.DiceRoll == 3 )
        //        {
        //            //_gameRow.RowHighlight = GameRow.HighlightStyle.Scratch;
        //            _gameRow.RowHighlight = HighlightStyle.Scratch;
        //            _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
        //            _gameRow.TakeScoreVisible = true;
        //        }
        //        else
        //            _gameRow.RowHighlight = HighlightStyle.Open;
        //        //_gameRow.RowHighlight = GameRow.HighlightStyle.Open;
        //    }

        //}






    }
}
