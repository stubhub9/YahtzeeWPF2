using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    //TODO: Break up the UpdateGameRows method into smaller blocks
    /// <summary>
    ///  Track and Update all texts and flags needed by the UI and game.
    /// </summary>
    public static class GameStatus
    {
        //// Fields

        //// Constructor

        //static GameStatus ()
        //{
        //}

        //// Properties ( No param required. )

        //public static List<GameRow> GameRows { get; set; }

        //#region Methods
        //// Method

        //public static void UpdateGameRows ()
        //{
        //    int _rowOffset = 0;
        //    int _score = 0;
        //    int _scoreTableColumn = GameModel.GameClock.PlayerUp - 1;
        //    int _scoreTableRow;

        //    GameRow _gameRow = new GameRow ();
        //    GameRows = new List<GameRow> ();

        //    // Generate thirteen GameRow objects to fill GameRows.
        //    for ( int _takeScoreRow = 0; _takeScoreRow < 16; _takeScoreRow++ )
        //        //for ( int _takeScoreRow = 0; _takeScoreRow < 13; _takeScoreRow++ )
        //        {
        //        _gameRow = new GameRow ();
        //        _score = 0;
                
        //        if ( _takeScoreRow >= 6 )
        //            _rowOffset = 2;
        //        _scoreTableRow = _takeScoreRow + _rowOffset;

        //        // If the row is open, evaluate the row for points.
        //        if ( GameModel.ScoreTable [ _scoreTableColumn, _scoreTableRow ] == null )
        //        {
        //            _gameRow.RowHighlight = GameRow.HighlightStyle.Open;

        //            // Evaluating for Upper Section points.
        //            if ( _takeScoreRow < 6 )
        //            {
        //                _gameRow.RowDiceFilter = GameDice.FiltersList [ _takeScoreRow ];

        //                // Every die matching the row's face value results in points.
        //                if ( GameDice.ValueIndexedMultiples [ _takeScoreRow + 1 ] != 0 )
        //                {
        //                    _score = ( _takeScoreRow + 1 ) * GameDice.ValueIndexedMultiples [ _takeScoreRow + 1 ];
        //                }
        //            }
        //            // End of Upper Section.

        //            // Chance
        //            else if ( _takeScoreRow == 11 )
        //            {
        //                _score = GameDice.Sum;
        //            }

        //            // Straights
        //            else if ( ( _takeScoreRow == 10 ) || ( _takeScoreRow == 9 ) )
        //            {
        //                // If straight is true.
        //                if ( GameDice.MaxStraight >= ( _takeScoreRow - 5 ) )
        //                {
        //                    _score = ( _takeScoreRow == 10 ) ? 40 : 30;
        //                }
        //            }

        //            // If there are multiples ( not null ), and the largest multiple is 3 or more.
        //            else if ( ( GameDice.MultiplesList.Count > 0 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] >= 3 ) )
        //            {
        //                // 3 and 4 of a kind, score is the sum of all face values.
        //                if ( _takeScoreRow == 6 || ( _takeScoreRow == 7 && GameDice.MultiplesList [ 0 ] [ 1 ] >= 4 ) )
        //                {
        //                    _score = GameDice.Sum;
        //                    // GameDice.MultiplesList  returns the largest multiple's ( 3OK + ) face value .
        //                    //gameRow.RowDiceFilter.Add ( GameDice.FiltersList [ GameDice.MultiplesList [ 0 ] [ 0 ]] );
        //                }

        //                // Check for Full House.
        //                else if ( ( _takeScoreRow == 8 ) && (( GameDice.MultiplesList.Count > 1 ) || ( GameDice.MultiplesList [ 0 ] [ 1 ] == 5  ) ))
        //                {
        //                    _score = 25;
        //                }

        //                // Evaluate for 5OK, Bonus 5OK.
        //                else if ( ( _takeScoreRow == 12 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] == 5 ) )
        //                {
        //                    _score = ( GameModel.ScoreTable [ _scoreTableColumn, 18 ] == null ) ? 50 : 100;

        //                }
        //            }

        //            _gameRow.TakeScoreValue = _score;
        //            _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _takeScoreRow, _score );
        //            if ( _score > 0 )
        //            {
        //                _gameRow.RowHighlight = GameRow.HighlightStyle.Points;
        //                _gameRow.TakeScoreVisible = true;
        //            }

        //            if ( ( GameModel.GameClock.DiceRoll == 3 ) && ( _score == 0 ) )
        //            {
        //                _gameRow.RowHighlight = GameRow.HighlightStyle.Scratch;
        //                _gameRow.TakeScoreVisible = true;
        //            }
        //        }
        //        //      End of open entry options,

        //        // Else the row is already filled and unavailable for scoring.
        //        else
        //        {
        //            _gameRow.RowHighlight = GameRow.HighlightStyle.Filled;
        //            _gameRow.TakeScoreVisible = false;
        //        }
        //        GameRows.Add ( _gameRow );
        //    }
        //    //      End of GameRows loop.
        //}
        ////      End of UpdateGameRows method.
        //#endregion GameStatus methods

        ////public static void UpdateGameRows1 ()
        ////{
        ////    int _rowOffset = 0;
        ////    int _score = 0;
        ////    int _scoreTableColumn = GameModel.GameClock.PlayerUp - 1;
        ////    int _scoreTableRow;

        ////    GameRow _gameRow = new GameRow ();
        ////    GameRows = new List<GameRow> ();

        ////    EvaluateAceySixey ();
        ////}

        ////static void EvaluateAceySixey ()
        ////{
        ////    for ( int _row = 0; _row < 6; _row++ )
        ////    {

        ////    }
        ////}



        ////      Helper Classes 

        //#region GameRow class
        //public class GameRow
        //{
        //    //      Fields      ******
            

        //    //      Constructor     **********

        //    public GameRow ()
        //    {
        //        RowDiceFilter = new List<bool> ();
        //        RowHighlight = new HighlightStyle ();
        //        TakeScoreString = "";
        //        TakeScoreVisible = false;
        //    }

        //    //      GameRow Enum

        //    public enum HighlightStyle
        //    {
        //        Filled = 0,
        //        Open,
        //        Scratch,
        //        Points,
        //        BestChoice,
        //        // What the player is choosing, OR enforced take 5OK or 5Str. 
        //        Insist
        //    }

        //    //      GameRow Properties      *******

        //    public List<bool> RowDiceFilter { get; set; }
        //    //public List<List<bool>> RowDiceFilter { get; set; }

        //    public HighlightStyle RowHighlight
        //    { get; set; }

        //    public string TakeScoreString
        //    { get; set; }

        //    public int TakeScoreValue
        //    { get; set; }

        //    public bool TakeScoreVisible
        //    { get; set; }

        //    //      End GameRowProperties
        //}
        //// End GameRow class.
        //#endregion GameRow class
    }
    //      End GameStatus class.

    


}
