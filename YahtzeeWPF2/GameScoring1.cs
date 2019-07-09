using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    public static class GameScoring1
    {
        // Fields
        static List<ResultsItem> scoringList;

        // Fields that reference other classes.
        static int clones;
        static int maxStraight;
        static int pairsCount;
        static int? [] scores;
        static int sumOfAllDice;
        static int [] valueIndexedMultiples;

        // Documented Magic Numbers
        const int FullHouseValue = 25;
        const int FourStrValue = 30;
        const int FiveStrValue = 40;
        const int FirstQuint = 50;
        const int BonusQuint = 100;


        // Default Constructor

        // Property
        internal static List<ResultsItem> ScoringList
        {
            get => scoringList;
        }
        


        #region Methods

        // Public Method

        public static void UpdateScoringList ()
        {
            // Set all fields that reference other classes.
            clones = ( pairsCount > 0 ) ? GameDice.PairsOrBetter [ 0 ] [ 1 ] : 0;
            maxStraight = GameDice.MaxStraight;
            pairsCount = GameDice.PairsOrBetter.Count;
            scores = GameModel1.ScoreTable [ ( int ) GameModel1.PlayerUp ];
            sumOfAllDice = GameDice.SumOfAllDice;
            valueIndexedMultiples = GameDice.ValueIndexedMultiples;

            CheckForPointsAvailable ();
        }


        // Private Methods

        static void AddResultsItemToScoringList ( Row row, int value )
        {
            var resultsItem = new ResultsItem
            {
                Row = row,
                IsFilled = ( scores [ ( int ) row ] == null ) ? false : true,
                Value = value,
            };
            scoringList.Add ( resultsItem );
        }


        static void CheckForPointsAvailable ()
        {
            CheckTheAcesThruSixes ();
            CheckForTripsQuadsAndFullHouse ();
            CheckTheStraightsPlusChance ();
            CheckTheQuints ();
        }


        static void CheckTheAcesThruSixes ()
        {

            for ( int _dieFaceValue = 1; _dieFaceValue < 7; _dieFaceValue++ )
            {
                int row = _dieFaceValue - 1;
                AddResultsItemToScoringList ( ( Row ) row, ( _dieFaceValue * valueIndexedMultiples [ _dieFaceValue ] ) );
            }
        }


        static void CheckForTripsQuadsAndFullHouse ()
        {

            // Add three of a kind.
            int _value = ( clones > 3 ) ? sumOfAllDice : 0;
            AddResultsItemToScoringList ( Row.ThreeX, _value );

            // Add four of a kind.
            _value = ( clones > 4 ) ? sumOfAllDice : 0;
            AddResultsItemToScoringList ( Row.FourX, _value );

            // Add full house.
            _value = ( ( clones == 5 )
                || ( ( clones == 3 ) && ( pairsCount == 2 ) ) ) ? FullHouseValue : 0;
            AddResultsItemToScoringList ( Row.FullHouse, _value );
        }


        static void CheckTheStraightsPlusChance ()
        {

            int _value = ( maxStraight >= 4 ) ? FourStrValue : 0;
            AddResultsItemToScoringList ( Row.FourStr, _value );

            _value = ( maxStraight == 5 ) ? FiveStrValue : 0;
            AddResultsItemToScoringList ( Row.FiveStr, _value );

            AddResultsItemToScoringList ( Row.Chance,  sumOfAllDice );
            
        }


        static void CheckTheQuints ()
        {
            int _value = 0;
            int _bonus5X = scores [ ( int ) Row.ScoreFiveX ] ?? 0;
            if ( clones == 5 )
            {
                _value = ( _bonus5X > 0 ) ? BonusQuint : FirstQuint;
            }

            Row _row = Row.FiveX1;
            for ( int i = (int) Row.FiveX1; i <= (int) Row.FiveX4; i++ )
            {
                if ( scores [ i ] == null )
                {
                    // Set row to the first unfilled FiveX row; or leave set at FiveX1 if all FiveX rows are filled.
                    _row = ( Row ) i;
                    break;
                }
            }
            AddResultsItemToScoringList ( _row, _value );
        }

        


        #endregion Methods
        
    }
}
