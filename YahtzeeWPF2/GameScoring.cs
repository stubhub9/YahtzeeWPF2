//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    public static class GameScoring
    {
        // Fields
        static List<bool> scoringRowsOpen;
        static List<int> pointsList;


        // Default Constructor


        // Properties. 

        public static List<GameRow> GameRows { get; set; }


        #region Methods

        // Public Method

        /// <summary>
        /// Main entry for GameScoring.
        /// Called by GameModel after dice are rolled.
        /// </summary>
        public static void UpdateGameRows ()
        {
            CheckIfRowIsOpen ();
            CheckForPointsAvailable ( ref pointsList );
            BuildGameRows ();
        }


        // Private Methods

        static void BuildGameRows ()
        {
            GameRows = new List<GameRow> ();
            for ( int _row = 0; _row < 13; _row++ )
            {
                var _gameRow = new GameRow ();
                GameRows.Add ( _gameRow );

                if ( !scoringRowsOpen [ _row ] )
                    continue;

                if ( pointsList [ _row ] > 0 )
                {
                    //_gameRow.RowHighlight = GameRow.HighlightStyle.Points;
                    _gameRow.RowHighlight = HighlightStyle.Points;
                    _gameRow.TakeScoreValue = pointsList [ _row ];
                    _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
                    _gameRow.TakeScoreVisible = true;
                }
                else if ( GameModel.GameClock.DiceRoll == 3 )
                {
                    //_gameRow.RowHighlight = GameRow.HighlightStyle.Scratch;
                    _gameRow.RowHighlight = HighlightStyle.Scratch;
                    _gameRow.TakeScoreString = GameModel.GameStrings.GetTakeScoreString ( _row, _gameRow.TakeScoreValue );
                    _gameRow.TakeScoreVisible = true;
                }
                else
                    _gameRow.RowHighlight = HighlightStyle.Open;
                //_gameRow.RowHighlight = GameRow.HighlightStyle.Open;
            }

        }


        /// <summary>
        /// Builds a list of the points available, for each row, using the current dice.
        /// </summary>
        /// <param name="pointsList"></param>
        static void CheckForPointsAvailable ( ref List<int> pointsList )
        {
            pointsList = new List<int> ();
            // Using ref pointsList as a parameter for clarity/ documentation.
            CheckTheAcesThruSixes ( ref pointsList );
            CheckThePairsOrBetter ( ref pointsList );
            CheckTheStraightsPlusChance ( ref pointsList );
        }


        /// <summary>
        /// Build the scoringRowsOpen list for this player's column in the ScoreTable.
        /// </summary>
        static void CheckIfRowIsOpen ()
        {
            scoringRowsOpen = new List<bool> ();
            int _column = GameModel.GameClock.PlayerUp - 1;
            for ( int _row = 0; _row < 18; _row++ )
            {
                // Jump from ">63 bonus" to 3OK.
                if ( _row == 6 )
                    _row = 8;
                scoringRowsOpen.Add ( ( GameModel.ScoreTable [ _column, _row ] == null ) ? true : false );
            }
            // If any 5OK entry is open, then flag index 12 to true. 
            if ( ( scoringRowsOpen [ 12 ] ) || ( scoringRowsOpen [ 13 ] ) || ( scoringRowsOpen [ 14 ] ) || ( scoringRowsOpen [ 15 ] ) )
                scoringRowsOpen [ 12 ] = true;
        }


        /// <summary>
        /// Get the points for the Aces through Sixes rows.
        /// </summary>
        /// <param name="pointsList"></param>
        static void CheckTheAcesThruSixes ( ref List<int> pointsList )
        {
            int [] _valueIndexedMultiples = GameDice.ValueIndexedMultiples;

            for ( int _dieFaceValue = 1; _dieFaceValue < 7; _dieFaceValue++ )
            {
                // Count all Aces through Sixes.
                pointsList.Add ( ( _dieFaceValue ) * _valueIndexedMultiples [ ( _dieFaceValue ) ] );
            }
        }


        /// <summary>
        /// Get the points for all of the "of a Kind" rows.
        /// </summary>
        /// <param name="pointsList"></param>
        static void CheckThePairsOrBetter ( ref List<int> pointsList )
        {
            // Score 50 for the first five of a kind or 100 for any additional five of a kind.
            int _fiveOfAKind = ( ( ( GameModel.ScoreTable [ ( GameModel.GameClock.PlayerUp - 1 ), 18 ] == null )
                    || ( GameModel.ScoreTable [ ( GameModel.GameClock.PlayerUp - 1 ), 18 ] == 0 ) ) ? 50 : 100 );
            int _fullHouse = 25;

            List<int []> _pairsOrBetter = GameDice.PairsOrBetter;
            int _sumOfAllDice = GameDice.SumOfAllDice;

            int [] _points = { 0, 0, 0, 0 };

            if ( ( _pairsOrBetter.Count != 0 ) && ( _pairsOrBetter [ 0 ] [ 1 ] >= 3 ) )
            {
                // Score three of a kind.11
                _points [ 0 ] = _sumOfAllDice;

                if ( _pairsOrBetter [ 0 ] [ 1 ] >= 4 )
                {
                    // Score four of a kind.
                    _points [ 1 ] = _sumOfAllDice;

                    if ( _pairsOrBetter [ 0 ] [ 1 ] == 5 )
                    {
                        // Score five of a kind and full house.
                        _points [ 2 ] = _fullHouse;
                        _points [ 3 ] = _fiveOfAKind;
                    }
                }
                else if ( GameDice.PairsOrBetter.Count > 1 )
                    // Score full house for three of a kind and a pair.
                    _points [ 2 ] = _fullHouse;
            }

            foreach ( var value in _points )
            {
                pointsList.Add ( value );
            }
        }


        /// <summary>
        /// Get the points for straights and chance.
        /// </summary>
        /// <param name="pointsList"></param>
        static void CheckTheStraightsPlusChance ( ref List<int> pointsList )
        {
            pointsList.Insert ( ( pointsList.Count - 1 ), ( ( GameDice.MaxStraight >= 4 ) ? 30 : 0 ) );
            pointsList.Insert ( ( pointsList.Count - 1 ), ( ( GameDice.MaxStraight == 5 ) ? 40 : 0 ) );
            pointsList.Insert ( ( pointsList.Count - 1 ), GameDice.SumOfAllDice );
        }



        #endregion Methods


        /// <summary>
        /// Supplies GameModel with info for each scorable row.
        /// </summary>
        public class GameRow
        {
            // No Fields 


            // Constructor  

            public GameRow ()
            {
                RowHighlight = HighlightStyle.Filled;
                TakeScoreString = "   ";
                TakeScoreValue = 0;
                TakeScoreVisible = false;
            }

            // Enum

            //public enum HighlightStyle
            //{
            //    Filled = 0,
            //    Open,
            //    Scratch,
            //    Points,
            //    BestChoice,
            //    // What the player is choosing, OR enforced take 5OK or 5Str. 
            //    Insist
            //}


            // Properties 

            public HighlightStyle RowHighlight
            { get; set; }

            public string TakeScoreString
            { get; set; }

            public int TakeScoreValue
            { get; set; }

            public bool TakeScoreVisible
            { get; set; }
        }

    }
}
