using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{

    enum Player
    {
        PlayerOne, PlayerTwo, PlayerThree,
    }

    enum Roll
    { Third, Second, First }

    internal enum Row
    {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        Score63, ScoreUpper,
        ThreeX, FourX, FullHouse, FourStr, FiveStr, Chance,
        FiveX1, FiveX2, FiveX3, FiveX4,
        ScoreFiveX, ScoreLower, ScoreTotal
    }


    struct GameClock
    {
        public Player PlayerUp;
        public Roll DiceThrown;
        public int Round;
    }


     internal struct ResultsItem
    {
        public Row Row;
        public bool IsFilled;
        public int Value;
    }


    static class GameModel1
    {
        // Fields

        // Default Constructor


        // Properties

        /// <summary>
        /// How many times the dice have been rolled, this turn.
        /// Value 1 to 3
        /// </summary>
        public static int CurrentDiceRoll
        {
            get => GameClock.diceRoll;
        }

        /// <summary>
        ///  The player with the dice.
        /// </summary>
        public static Player PlayerUp
        {
            get => GameClock.player;
        }

        /// <summary>
        /// How many entries have been filled by end of turn.
        /// </summary>
        static int GameRound
        {
            get => GameClock.gameRound;
        }


        public static int? [] [] ScoreTable
        {
            get => GameScores.scoreTable;
        }



        #region Methods

        // Public Methods

        /* Reset GameClock and ScoreTable
         * Call for new dice and dice evaluation.
         */
        public static void NewGame ()
        {
            GameClock.NewGame ();
            GameScores.NewGame ();
            GameDice.NewDice ();
            GameScoring1.UpdateScoringList ();
        }


        public static void NextPlayer ()
        {
            GameClock.NextPlayer ();
            GameDice.NewDice ();
            GameScoring1.UpdateScoringList ();
        }

        /* 
         * Record score in scoresheet, 
         * return scoring results to Vim for Vis,
         * RowEnum and value for each score sheet entry changed or updated.
         * ie The score taken and all Totals and Bonuses that were updated.
         * */
        public static List < ResultsItem > RecordScore ( ResultsItem scoringItem )
        {
            List<ResultsItem> ResultsList = GameScores.RecordScore ( scoringItem );
            return ResultsList;
        }


        /* Advance game clock;
         * 
         * Call for roll unheld dice.
         * */
        public static void RollDice ()
        {
            if ( CurrentDiceRoll >= 3 )
                throw new Exception ( $"ERROR:  Attempted dice roll when current dice roll is {CurrentDiceRoll}" );

            GameClock.NextDiceRoll ();
            GameDice.RollDice ();
            GameScoring1.UpdateScoringList ();
        }

        #endregion Methods



        private static class GameClock
        {
            public static int gameRound;
            public static int diceRoll;
            public static Player player;

            public static int GameRound
            {
                get => gameRound;
            }


            static void GameOver ()
            {
                //NYI
            }


            static void MustTakeScore ()
            {
                //NYI
            }


            public static void NewGame ()
            {
                gameRound = 1;
                diceRoll = 1;
                player = Player.PlayerOne;
            }


            public static void NextPlayer ()
            {
                diceRoll = 1;
                if ( player == Player.PlayerThree )
                {
                    player = Player.PlayerOne;
                    gameRound++;

                    if ( gameRound > 17 )
                        throw new Exception ( $"GameRound {gameRound} is greater than max of 17." );

                    //TODO:  Perform this check elsewhere??????????????????????????????????????????????????????????????????????????????????????????????????????????
                    if ( gameRound == 17 )
                        GameOver ();
                }
                else
                    player = ( Player ) ( ( ( int ) player ) + 1 );
            }


            public static void NextDiceRoll ()
            {
                diceRoll++;

                if ( diceRoll > 3 )
                {
                    throw new Exception ( $"DiceRoll = {diceRoll} is greater than max of 3." );
                }

                //TODO:  Perform this check elsewhere????????????????????????????????????????????????????????????????????????????????????????????????????????????
                if ( diceRoll == 3 )
                {
                    MustTakeScore ();
                }
            }

        }



        private static class GameScores
        {
            // Field

            // Three player columns with a row for each Row enum; 21 total.
            public static int? [] [] scoreTable;

            // Public Methods


            public static void BuildScoringItemsList ()
            {
                // Get this players scores.
                int? [] scores = scoreTable [ ( int ) PlayerUp ];
                var scoringItems = new List<ResultsItem> ();
                
            }


            public static void NewGame ()
            {
                scoreTable = new int? [ 3 ] [];
                for ( int i = 0; i < 3; i++ )
                {
                    scoreTable [ i ] = new int? [ 21 ];
                    //NOTE:  Initializing the "Score" rows to zero would eliminate many checks for null.
                }
            }

            /// <summary>
            /// Updates the score table, 
            /// Returns all entries that have changed for the Vis
            /// </summary>
            public static List<ResultsItem> RecordScore ( ResultsItem item )
            {
                // Get this players scores.
                int? [] scores = scoreTable [ ( int ) PlayerUp ];
                var resultsList = new List<ResultsItem> ();

                if ( scores [ ( int ) item.Row ] != null )
                {
                    throw new Exception ( "ERROR:  Only unfilled rows can be chosen." );
                }

                // Update the player chosen entry.
                AddToScores ( item.Row, item.Value, ref scores, ref resultsList );

                UpdatePostings ( item.Row, item.Value, ref scores, ref resultsList );

                scoreTable [ ( int ) PlayerUp ] = scores;
                return resultsList;
            }


            // Private Methods

            // Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            static void AddToScores ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            {
                AddToScoreTable ( row, value, ref scores );
                AddToScoringItemList ( row, value, ref resultsList );
            }


            // Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            static void AddToScoreTable ( Row row, int value, ref int? [] scores )
            {
                if ( scores [ ( int ) row ] == null )
                {
                    // Update the player's selected entry or an initial "Score" posting.
                    scores [ ( int ) row ] = value;
                }
                else
                {
                    // Update a "Score" posting.
                    scores [ ( int ) row ] += value;
                }
            }


            // Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            static void AddToScoringItemList ( Row row, int value, ref List<ResultsItem> resultsList )
            {
                var thisItem = new ResultsItem
                {
                    Row = row,
                    Value = value,
                };
                resultsList.Add ( thisItem );
            }


            static void UpdatePostings ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            {
                if ( ( int ) row <= ( int ) Row.Sixes )
                {
                    // Update the upper section postings.
                    UpdateUpperSection ( value, ref scores, ref resultsList );
                }

                else
                // Update the lower section postings.
                {
                    UpdateLowerSection ( row, value, ref scores, ref resultsList );
                }
            }


            static void UpdateUpperSection ( int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            {
                // If the upper total is over 63 and the bonus63 has not been claimed previously.
                if ( ( ( value + ( scores [ ( int ) Row.ScoreUpper ] ?? 0 ) ) > 63 )
                    && ( scores [ ( int ) Row.Score63 ] == null ) )
                {
                    // Score the upper bonus with this Documented Magic Number.
                    int valueScore63 = 35;
                    AddToScores ( Row.Score63, valueScore63, ref scores, ref resultsList );
                    value += valueScore63;
                }

                AddToScores ( Row.ScoreUpper, value, ref scores, ref resultsList );
                AddToScores ( Row.ScoreTotal, value, ref scores, ref resultsList );
            }


            static void UpdateLowerSection ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            {
                // Update the lower section.
                if ( ( ( int ) row >= ( int ) Row.FiveX1 ) && ( ( int ) row <= ( int ) Row.FiveX4 ) && ( value != 0 ) )
                {
                    // If a five of a kind was chosen, but not scratched.
                    AddToScores ( Row.ScoreFiveX, value, ref scores, ref resultsList );
                }

                AddToScores ( Row.ScoreLower, value, ref scores, ref resultsList );
                AddToScores ( Row.ScoreTotal, value, ref scores, ref resultsList );
            }

            //  VVVV  End of GameScores class.    VVVV
        }





    }
}
