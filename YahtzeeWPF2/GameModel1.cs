using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{

    public enum Player
    {
        //Player1, Player2, Player3,
        PlayerOne, PlayerTwo, PlayerThree,
    }

    enum Roll
    { Third, Second, First }

    public enum Row
    {
        Unselected = -1,
        Ones = 0,
        Twos, Threes, Fours, Fives, Sixes,
        Score63, ScoreUpper,
        ThreeX, FourX, FullHouse, FourStr, FiveStr, Chance,
        FiveX1, FiveX2, FiveX3, FiveX4,
        ScoreFiveX, ScoreLower, ScoreTotal
    }


    //struct GameClock
    //{
    //    public Player PlayerUp;
    //    public Roll DiceThrown;
    //    public int Round;
    //}


    public struct ResultsItem
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

        #region Properties

        /// <summary>
        /// How many times the dice have been rolled, this turn.
        /// Value 1 to 3
        ///  Should I use the Roll enum where the value equals the rolls remaining????????????????????????????????????????????????
        /// </summary>
        public static int CurrentDiceRoll
        {
            get => GameClock.diceRoll;
        }

        /// <summary>
        /// How many entries have been filled by end of turn.
        /// </summary>
        public static int GameRound
        {
            get => GameClock.gameRound;
        }

        /// <summary>
        ///  The player with the dice.
        /// </summary>
        public static Player PlayerUp
        {
            get => GameClock.player;
        }


        // Sets the player column for Visual Scoresheet updates
        public static Player PlayerScored
        {
            get; set;
        }


        public static Row RowSelected
        { get; set; }


        public static int? [] [] ScoreTable
        {
            get => GameModelUpdateScores.scoreTable;
        }


        public static List<ResultsItem> ResultsList
        { get; set; }



        #endregion Properties

        #region Methods

        // Public Methods

        public static void CommitWasClicked ()
        {
            // Is game over?
            if ( GameRound == 17 )
            {
                // Game is already over.
                //PlayerUp is set to winner at turn 17.
                return;
            }
            // 
            if ( CurrentDiceRoll == 3 )
            {
                // Must take score.
                if ( RowSelected == Row.Unselected )
                {
                    // Player needs to choose a row to continue.
                    return;
                }
                else
                {
                    // Take score.
                    //ResultsItem resultsItem = GameScoring1.GetResultsItem ( RowSelected );
                    //ResultsItem resultsItem = GameScoring1.ScoringList [ ( int ) RowSelected ];
                    //GameModelUpdateScores.RecordScore ( resultsItem );
                    GameModelUpdateScores.RecordScore ();
                    GameClock.NextPlayer ();
                    GameDice.NewDice ();
                    GameScoring1.UpdateScoringList ();

                }

            }

            else
            {
                // Roll dice
                RollDice ();
            }

        }


        /* Reset GameClock and ScoreTable
         * Call for new dice and dice evaluation.
         */
        public static void NewGame ()
        {
            GameClock.NewGame ();
            GameModelUpdateScores.NewGame ();
            GameDice.NewDice ();
            GameScoring1.UpdateScoringList ();
            RowSelected = Row.Unselected;
            ResultsList = new List<ResultsItem> ();
        }


        //?????????  IS THIS EVER CALLED ???????????????????????????????????????
        ////public static void NextPlayer ()
        ////{
        ////    GameClock.NextPlayer ();
        ////    GameDice.NewDice ();
        ////    GameScoring1.UpdateScoringList ();
        ////    RowSelected = Row.Unselected;
        ////}

        /* 
         * Record score in scoresheet, 
         * return scoring results to Vim for Vis,
         * RowEnum and value for each score sheet entry changed or updated.
         * ie The score taken and all Totals and Bonuses that were updated.
         * */
        //public static List<ResultsItem> RecordScore ( ResultsItem scoringItem )


        // ???  Unused ?????????????????????????????????????????????????????
        ////public static void  RecordScore ( ResultsItem scoringItem )
        ////{
        ////    GameScores.RecordScore ( scoringItem );
        ////    //List<ResultsItem> ResultsList = GameScores.RecordScore ( scoringItem );
        ////}



        static void RollDice ()
        {
            if ( CurrentDiceRoll >= 3 )
                throw new Exception ( $"ERROR:  Attempted dice roll when current dice roll is {CurrentDiceRoll}" );

            GameClock.NextDiceRoll ();
            GameDice.RollDice ();
            GameScoring1.UpdateScoringList ();
            // On scoring ResultsList and NewDice; else Roll dice and clear list;
            ResultsList.Clear ();
        }





        public static void RowClicked ( Row rowClicked )
        {
            int? [] _scores = ScoreTable [ ( int ) PlayerUp ];

            // Check to see if the row clicked is an unfilled row.
            if ( rowClicked == Row.FiveX1 )
            {
                // The button FiveX1 shows data for 5X1 through 5X4.
                if ( ( _scores [ ( int ) Row.FiveX1 ] == null ) || ( _scores [ ( int ) Row.FiveX2 ] == null )
                    || ( _scores [ ( int ) Row.FiveX3 ] == null ) || ( _scores [ ( int ) Row.FiveX4 ] == null ) )
                {
                    RowSelected = rowClicked;
                }
            }

            else if ( _scores [ ( int ) rowClicked ] == null )
            {
                RowSelected = rowClicked;
            }
        }

        #endregion Methods





        private static class GameClock
        {
            public static int gameRound;
            public static int diceRoll;
            public static Player player;

            //public static int GameRound
            //{
            //    get => gameRound;
            //}


            static void GameOver ()
            {
                player = GetWinner ();
            }


            public static Player GetWinner ()
            //public static int GetWinner ()
            {

                if ( ( ScoreTable [ 0 ] [ 20 ] > ScoreTable [ 1 ] [ 20 ] ) && ( ScoreTable [ 0 ] [ 20 ] > ScoreTable [ 2 ] [ 20 ] ) )
                    return Player.PlayerOne;

                else if ( ScoreTable [ 1 ] [ 20 ] > ScoreTable [ 2 ] [ 20 ] )
                    return Player.PlayerTwo;

                else
                    return Player.PlayerThree;
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
                    {
                        diceRoll = 0;
                        GameOver ();
                    }
                }
                else
                {

                    player = ( Player ) ( ( ( int ) player ) + 1 );
                }
                RowSelected = Row.Unselected;
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



        private static class GameModelUpdateScores
        {
            // Field

            // Used to set the GameModel ResultsList property
            //static List<ResultsItem> resultsList;

            // Three player columns with a row for each Row enum; 21 total; source of the GameModel.ScoreTable property.
            public static int? [] [] scoreTable;

            // Public Methods




            // Called by GameModel.NewGame ().
            public static void NewGame ()
            {
                // The following statement is invalid.
                //scoreTable = new int? [ 3 ] [ 21 ];

                scoreTable = new int? [ 3 ] [];
                for ( int i = 0; i < 3; i++ )
                {
                    scoreTable [ i ] = new int? [ 21 ];
                }
            }

            /// <summary>
            /// Updates the score table, 
            /// Returns all entries that have changed for the Vis
            /// </summary>
            //public static void RecordScore ( ResultsItem item )
            //{
            //    // Get this players scores.
            //    int? [] scores = scoreTable [ ( int ) PlayerUp ];

            //    // Used to set the GameModel ResultsList property.
            //    var _resultsList = new List<ResultsItem> ();

            //    //SetFiveXRow ( item );

            //    if ( item.Row == Row.FiveX1 )
            //    {
            //        if ( item.Value == 0 )
            //        {
            //            for ( int i = ( int ) Row.FiveX4; i >= ( int ) Row.FiveX1; i-- )
            //            {
            //                if ( scores [ i ] == null )
            //                {
            //                    item.Row = ( Row ) i;
            //                    break;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for ( int i = ( int ) Row.FiveX1; i <= ( int ) Row.FiveX4; i++ )
            //            {
            //                if ( scores [ i ] == null )
            //                {
            //                    item.Row = ( Row ) i;
            //                    break;
            //                }
            //            }

            //        }
            //    }

            //    PlayerScored = PlayerUp;
            //    // Update the player chosen entry.
            //    AddToScores ( item.Row, item.Value, ref scores, ref resultsList );

            //    UpdatePostings ( item.Row, item.Value, ref scores, ref resultsList );

            //    scoreTable [ ( int ) PlayerUp ] = scores;
            //    ResultsList = resultsList;
            //}



            public static void RecordScore ()
            {
                var _row = RowSelected;
                int? [] _scores = scoreTable [ ( int ) PlayerUp ];
                var _scoresUpdate = new List<ResultsItem> ();
                var _scoresItem = GameScoring1.GetResultsItem ( _row );
                var _postItem = new ResultsItem ();
                //var _scoresItem = new ResultsItem ();
                PlayerScored = PlayerUp;

                // GameScoring1 and MainWindow both use the FiveX1 row to represent the Five of a kind block; for scores available or button clicked.
                if ( _row == Row.FiveX1 )
                {
                    // Add the five of a kind score.
                    _scoresItem.Row = SetFiveXRow ();
                    _scoresUpdate.Add ( _scoresItem );
                    _scores [ ( int ) _scoresItem.Row ] = _scoresItem.Value;
                    //_scores [ ( int ) Row.ScoreFiveX ] = ( _scores [ ( int ) Row.ScoreFiveX ] != null ) ?
                    //    _scores [ ( int ) Row.ScoreFiveX ] + _scoresItem.Value : _scoresItem.Value;

                    // Add the five of a kind total.
                    _scores [ ( int ) Row.ScoreFiveX ] = ( _scores [ ( int ) Row.ScoreFiveX ] ?? 0 ) + _scoresItem.Value;
                    _postItem.Row = Row.ScoreFiveX;
                    _postItem.Value = ( int ) _scores [ ( int ) Row.ScoreFiveX ];
                    _scoresUpdate.Add ( _postItem );
                }
                else
                {
                    _scoresUpdate.Add ( _scoresItem );
                    _scores [ ( int ) _scoresItem.Row ] = _scoresItem.Value;
                }
                
                if ( ( int ) _row <= ( int ) Row.Sixes )
                {

                    _scores [ ( int ) Row.ScoreUpper ] = ( _scores [ ( int ) Row.ScoreUpper ] ?? 0 ) + _scoresItem.Value;
                    if (( _scores [ (int) Row.ScoreUpper] > 63 ) && ( _scores [ (int) Row.Score63 ] == null ))
                    {
                        _scores [ ( int ) Row.Score63 ] = GameScoring1.Over63Bonus;
                        _postItem = new ResultsItem ()
                        {
                            Row = Row.Score63,
                            Value = GameScoring1.Over63Bonus,
                        } ;
                        _scoresUpdate.Add ( _postItem );
                        _scores [ ( int ) Row.ScoreUpper ] += GameScoring1.Over63Bonus;
                    }
                    
                    _postItem = new ResultsItem ()
                    {
                        Row = Row.ScoreUpper,
                        Value = (int) _scores [ ( int ) Row.ScoreUpper ],
                    };
                    _scoresUpdate.Add ( _postItem );
                }
                else
                {
                    // Update LowerTotal
                    _scores [ ( int ) Row.ScoreLower ] = ( _scores [ ( int ) Row.ScoreLower ] ?? 0 ) + _scoresItem.Value;
                    _postItem = new ResultsItem ()
                    {
                        Row = Row.ScoreLower,
                        Value = ( int ) _scores [ ( int ) Row.ScoreLower ],
                    };
                    _scoresUpdate.Add ( _postItem );
                }

                _scores [ ( int ) Row.ScoreTotal ] = ( _scores [ ( int ) Row.ScoreTotal ] ?? 0 ) + _scoresItem.Value;
                _postItem = new ResultsItem ()
                {
                    Row = Row.ScoreTotal,
                    Value = ( int ) _scores [ ( int ) Row.ScoreTotal ],
                };
                _scoresUpdate.Add ( _postItem );

                ResultsList = _scoresUpdate;
            }


            // Sets the correct row for the scoreTable and the 5X button's textblock.
            static Row SetFiveXRow ()
            {
                var _scores = scoreTable [ ( int ) PlayerUp ];
                var _row = Row.Unselected;
                // GameScoring1 and MainWindow both use the FiveX1 row to represent the Five of a kind block; for scores available or button clicked.
                // ie:  var _item = GameScoring1.GetResultsItem ( Row.FiveX1 );
                var _item = GameScoring1.GetResultsItem ( RowSelected );
                // Get this players scores.

                // Scratching progresses from FiveX4 to FiveX1.
                if ( _item.Value == 0 )
                {
                    for ( int i = ( int ) Row.FiveX4; i >= ( int ) Row.FiveX1; i-- )
                    {
                        if ( _scores [ i ] == null )
                        {
                            _row = ( Row ) i;
                            break;
                        }
                    }
                }
                else
                // Scoring a 5 of a kind progresses from FiveX1 to FiveX4.
                {
                    for ( int i = ( int ) Row.FiveX1; i <= ( int ) Row.FiveX4; i++ )
                    {
                        if ( _scores [ i ] == null )
                        {
                            _row = ( Row ) i;
                            break;
                        }
                    }
                }

                return _row;
            }


            // Private Methods



            //// Not used ????????????????????????????????????????????
            //static void BuildScoringItemsList ()
            //{
            //    // Get this players scores.
            //    int? [] scores = scoreTable [ ( int ) PlayerUp ];
            //    var scoringItems = new List<ResultsItem> ();

            //}

            //// Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            //static void AddToScores ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            //{
            //    AddToScoreTable ( row, value, ref scores );
            //    AddToScoringItemList ( row, value, ref resultsList );
            //}


            //// Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            //static void AddToScoreTable ( Row row, int value, ref int? [] scores )
            //{
            //    if ( scores [ ( int ) row ] == null )
            //    {
            //        // Update the player's selected entry or an initial "Score" posting.
            //        scores [ ( int ) row ] = value;
            //    }
            //    else
            //    {
            //        // Update a "Score" posting.
            //        scores [ ( int ) row ] += value;
            //    }
            //}


            //// Do I need to ref the list ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????
            //static void AddToScoringItemList ( Row row, int value, ref List<ResultsItem> resultsList )
            //{
            //    var thisItem = new ResultsItem
            //    {
            //        Row = row,
            //        Value = value,
            //    };
            //    resultsList.Add ( thisItem );
            //}




            //static void UpdatePostings ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            //{
            //            if ((int ) row <= (int ) Row.Sixes )
            //        {
            //            // Update the upper section postings.
            //            UpdateUpperSection ( value, ref scores, ref resultsList );
            //        }

            //            else
            //            // Update the lower section postings.
            //            {
            //                UpdateLowerSection ( row, value, ref scores, ref resultsList );
            //    }
            //}


            //static void UpdateUpperSection ( int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            //{
            //    // If the upper total is over 63 and the bonus63 has not been claimed previously.
            //    if ( ( ( value + ( scores [ ( int ) Row.ScoreUpper ] ?? 0 ) ) > 63 )
            //        && ( scores [ ( int ) Row.Score63 ] == null ) )
            //    {
            //        // Score the upper bonus with this Documented Magic Number.
            //        int valueScore63 = 35;
            //        AddToScores ( Row.Score63, valueScore63, ref scores, ref resultsList );
            //        value += valueScore63;
            //    }

            //    AddToScores ( Row.ScoreUpper, value, ref scores, ref resultsList );
            //    AddToScores ( Row.ScoreTotal, value, ref scores, ref resultsList );
            //}


            //static void UpdateLowerSection ( Row row, int value, ref int? [] scores, ref List<ResultsItem> resultsList )
            //{
            //    // Update the lower section.
            //    if ( ( ( int ) row >= ( int ) Row.FiveX1 ) && ( ( int ) row <= ( int ) Row.FiveX4 ) && ( value != 0 ) )
            //    {
            //        // If a five of a kind was chosen, but not scratched.
            //        AddToScores ( Row.ScoreFiveX, value, ref scores, ref resultsList );
            //    }

            //    AddToScores ( Row.ScoreLower, value, ref scores, ref resultsList );
            //    AddToScores ( Row.ScoreTotal, value, ref scores, ref resultsList );
            //}

            //  VVVV  End of GameScores class.    VVVV
        }





    }
}
