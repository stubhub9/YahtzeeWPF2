using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    public static class GameScoring
    {
        //App.RowHighlight
        // Fields
        static List <bool>  scoringRowsOpen;
        static List<int> pointsList;

        // Property  ( No param required. )

        public static List<GameRow> GameRows { get; set; }

        // Methods

        /// <summary>
        /// Main entry for GameScoring.
        /// Called by GameModel after dice are rolled.
        /// </summary>
        public static void UpdateGameRows ()
        {
            CheckIfRowIsOpen ();
            CheckForPoints ();
            BuildGameRows ();

        }

        static void BuildGameRows ()
        {
            GameRows = new List<GameRow> ();
            GameRow _gameRow; 
            for ( int _row = 0; _row < 13; _row++ )
            {
                _gameRow = new GameRow ();
                GameRows.Add ( _gameRow );

                if ( !scoringRowsOpen [ _row ] )
                    continue;
                if ( pointsList [ _row ] > 0 )
                {
                    _gameRow.RowHighlight = GameRow.HighlightStyle.Points;
                    _gameRow.TakeScoreValue = pointsList [ _row ];
                    _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
                    _gameRow.TakeScoreVisible = true;
                }
                else if ( GameModel.GameClock.DiceRoll == 3 )
                {
                    _gameRow.RowHighlight = GameRow.HighlightStyle.Scratch;
                    _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
                    _gameRow.TakeScoreVisible = true;
                }
                else
                    _gameRow.RowHighlight = GameRow.HighlightStyle.Open;
            }

        }


        static void CheckIfRowIsOpen ()
        {
            scoringRowsOpen = new List<bool> ();
            int _column = GameModel.GameClock.PlayerUp - 1;
            for ( int _row = 0; _row < 18; _row++ )
            {
                // Jump from ">63 bonus" to 3OK.
                if ( _row == 6 )
                    _row = 8;
                scoringRowsOpen.Add  (( GameModel.ScoreTable [ _column, _row ] == null ) ? true : false);
            }
            // If any  5OK entry is open, then flag index 12 to true. 
            if ( ( scoringRowsOpen [ 12 ] ) || ( scoringRowsOpen [ 13 ] ) || ( scoringRowsOpen [ 14 ] ) || ( scoringRowsOpen [ 15 ] ) )
                scoringRowsOpen [ 12 ] = true;
        }


        static void CheckForPoints (  )
        {
            int _sum = GameDice.Sum;
            pointsList = new List<int> ();
            for ( int _row = 1; _row < 7; _row++ )
            {
                // Count all Aces through Sixes.
                pointsList.Add ( ( _row ) * GameDice.ValueIndexedMultiples [ ( _row ) ] );
            }

            // Check for 3OK, 4OK.
            pointsList.Add ( ( ( GameDice.MultiplesList.Count > 0 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] >= 3 ) ) ? _sum : 0 );
            pointsList.Add ( ( ( GameDice.MultiplesList.Count > 0 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] >= 4 ) ) ? _sum : 0 );
            // Score Full House for 5OK or 3OK and a pair.
            pointsList.Add (( ( ( GameDice.MultiplesList.Count > 0 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] >= 5 ) )
                || ( ( GameDice.MultiplesList.Count > 1 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] == 3 ) ))
                ? 25 : 0 );
            pointsList.Add ( ( GameDice.MaxStraight == 4 ) ? 30 : 0 );
            pointsList.Add ( ( GameDice.MaxStraight == 5 ) ? 40 : 0 );
            pointsList.Add ( _sum );
            // Check for 5OK.
            if ( ( GameDice.MultiplesList.Count > 0 ) && ( GameDice.MultiplesList [ 0 ] [ 1 ] == 5 ) )
            {
                // Check for Bonus 5OK.
                pointsList.Add ( ( GameModel.ScoreTable [ ( GameModel.GameClock.PlayerUp - 1 ), 18 ] != null ) ? 100 : 50 );
            }
            else
                pointsList.Add ( 0 );
        }












        public class GameRow
        {
            //      Fields      ******


            //      Constructor     **********

            public GameRow ()
            {
                //RowDiceFilter = new List<bool> ();
                RowHighlight = HighlightStyle.Filled;
                TakeScoreString = "   ";
                TakeScoreValue = 0;
                TakeScoreVisible = false;
            }

            //      GameRow Enum

            public enum HighlightStyle
            {
                Filled = 0,
                Open,
                Scratch,
                Points,
                BestChoice,
                // What the player is choosing, OR enforced take 5OK or 5Str. 
                Insist
            }

            //      GameRow Properties      *******

            //public List<bool> RowDiceFilter { get; set; }
            //public List<List<bool>> RowDiceFilter { get; set; }

            public HighlightStyle RowHighlight
            { get; set; }

            public string TakeScoreString
            { get; set; }

            public int TakeScoreValue
            { get; set; }

            public bool TakeScoreVisible
            { get; set; }

            //      End GameRowProperties
        }


    }
}
