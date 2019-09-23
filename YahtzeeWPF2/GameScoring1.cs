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
        // Used by GameModel.UpdateScores.
        public const int Over63Bonus = 35;


        // Default Constructor

        // Property
        // Index:  0 through 5 contain the sums of all aces through sixes.
        // Index:  6 through 12 contain 3OK, 4OK, Full House, Small Straight, Lg Straight, Chance and 5OK scores.
        // ResultsItem contains Row enum; IsFilled bool referencing the player's scores taken; and the current scoring value for this row.
        internal static List<ResultsItem> ScoringList
        {
            get => scoringList;
        }



        #region Methods

        // Public Method

        public static void UpdateScoringList ()
        {
            scoringList = new List<ResultsItem> ();

            // Set all fields that reference other classes.

            // Are there multiples of none, one or two numbers?
            pairsCount = GameDice.PairsOrBetter.Count;
            // What is the greatest count of multiples of a value.
            clones = ( pairsCount > 0 ) ? GameDice.PairsOrBetter [ 0 ] [ 1 ] : 0;
            //TODO:  Currently doesn't keep track of multiples less than 4. !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            maxStraight = GameDice.MaxStraight;
            // List of the current player's recorded scores.
            scores = GameModel1.ScoreTable [ ( int ) GameModel1.PlayerUp ];
            sumOfAllDice = GameDice.SumOfAllDice;
            // Index one through six contain the multiples for each die value ( 0 - 5 ).
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
            int _value = ( clones > 2 ) ? sumOfAllDice : 0;
            AddResultsItemToScoringList ( Row.ThreeX, _value );

            // Add four of a kind.
            _value = ( clones > 3 ) ? sumOfAllDice : 0;
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

            AddResultsItemToScoringList ( Row.Chance, sumOfAllDice );

        }


        static void CheckTheQuints ()
        {
            int _value = 0;

            //int _bonus5X = scores [ ( int ) Row.ScoreFiveX ] ?? 0;
            int? _bonus5X = scores [ ( int ) Row.ScoreFiveX ] ;
            if ( clones == 5 )
            {
                //_value = ( _bonus5X > 0 ) ? BonusQuint : FirstQuint;
                _value = (( _bonus5X == null ) || ( _bonus5X == 0 ) ) ? FirstQuint : BonusQuint;
            }

            // Check for an open 5X row.
            bool _isFilled = true;
            for ( int i = ( int ) Row.FiveX1; i <= ( int ) Row.FiveX4; i++ )
            {
                if ( scores [ i ] == null )
                {
                    _isFilled = false;
                    break;
                }
            }
            //AddResultsItemToScoringList ( _row, _value );
            var resultsItem = new ResultsItem
            {
                // Set row to FiveX1 to represent whether any FiveX row is open.
                Row = Row.FiveX1,
                IsFilled = _isFilled,
                Value = _value,
            };
            scoringList.Add ( resultsItem );
        }

        public static ResultsItem GetResultsItem ( Row row )
        {
            ResultsItem _resultsItem;
            if ( row == Row.Unselected )
            {
                _resultsItem = new ResultsItem ();
                return _resultsItem;
            }

            int _intRow = ( int ) row;
            if ( _intRow >= 6 )
                _intRow = _intRow - 2;

            _resultsItem = GameScoring1.ScoringList [ _intRow ];
            return _resultsItem;
        }


        #endregion Methods

    }
}
