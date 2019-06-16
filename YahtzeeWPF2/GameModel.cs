using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace YahtzeeWPF2
{
    public static class GameModel
    {
        #region Fields
        //      Fields.

        //static Button button = new Button ();
        static List<List<string>> playerEntryAndPostStrings;
        static List<int [,]> results;
        static int [,] result;
        static int? [,] scoreTable;
        #endregion Fields

        #region Constructor
        // Static constructor 
        static GameModel ()
        {
            scoreTable = new int? [ 3, 21 ];
        }
        #endregion Constructor
        
        

        #region Properties
        // Properties ( or Indexer?)

        public static int? [,] ScoreTable
        {
            get
            { return scoreTable; }
            set
            { }
        }
        #endregion Properties

        #region GameModel  Methods
        //      Methods

        public static void NewGame ()
        {
            scoreTable = new int? [ 3, 21 ];
            GameClock.NewGame ();
            GameDice.NewDice ();
            GameStatus.UpdateGameRows ();
            CommitDetails.NextPlayer ();
            CommitDetails.RollText ();
            
        }


        public static void CommitClickedHandler ()
        {
            // Results will be null unless scoring happenned.
            CommitDetails.ResultsList = new List<int []> ();
            if ( GameClock.DiceRoll == 3 )
            {
                // If a scoresheet row has not been chosen => return.
                if ( CommitDetails.TakeScoreRow < 0 )
                {
                    return;
                }
                RecordScore ();
                GameClock.NextPlayer ();
                GameDice.NewDice ();
            }
            else
            {
                GameClock.NextRoll ();
                GameDice.RollDice ();
            }
            GameStatus.UpdateGameRows ();

            if ( GameClock.DiceRoll < 3 )
            {
                CommitDetails.RollText ();
            }
            else
                CommitDetails.ChooseText ();

            // ?????????If a  "can't do better than 5OK/ 5Str, ..." then a score needs to/ could be taken.  TRIGGERED BY GameStatus  ????????
        }

        static void RecordScore ()
        { 
            GameStatus.GameRow _gamerow = GameStatus.GameRows [ CommitDetails.TakeScoreRow ];
            int _row = ConvertTakeScoreRowToScoreTableRow ( CommitDetails.TakeScoreRow );
            int _col = GameClock.PlayerUp - 1;
            int _scoreDelta = _gamerow.TakeScoreValue;
            // The first item in CommitDetails sets the posts column.
            CommitDetails.UpdateResults ( ConvertPlayerUpToScoresheetColumn (), 0 );

            // Update the entry selected.
            scoreTable [ _col, _row ] =_scoreDelta;
            CommitDetails.UpdateResults ( ConvertScoreTableRowToPostRow ( _row ), _scoreDelta );

            if ( _row < 6 )
            {
                // If the entry was in the upper section, update  the Upper Total and upper bonus.
                // Check for upper section >63 bonus for 35 points.
                if (( scoreTable [ _col, 6 ] != 35 ) && (( scoreTable [ _col, 7 ] + _scoreDelta ) > 63 ))
                {
                    _scoreDelta += 35;
                    scoreTable [ _col, 6 ] = 35;
                    CommitDetails.UpdateResults ( ConvertScoreTableRowToPostRow ( 6 ), 35 );
                }
                scoreTable [ _col, 7 ] = _scoreDelta + ( scoreTable [ _col, 7 ] ?? 0 );
                CommitDetails.UpdateResults ( ConvertScoreTableRowToPostRow ( 7 ), ( int ) scoreTable [ _col, 7 ] );
            }
            else
            {
                // Update lower section total.
                scoreTable [ _col, 19 ] = _scoreDelta + ( scoreTable [ _col, 19 ] ?? 0 );
                CommitDetails.UpdateResults ( ConvertScoreTableRowToPostRow ( 19 ), ( int ) scoreTable [ _col, 19 ] );
            }
            // Update grand total.
            scoreTable [ _col, 20 ] = _scoreDelta + ( scoreTable [ _col, 20 ] ?? 0 );
            CommitDetails.UpdateResults ( ConvertScoreTableRowToPostRow ( 20 ), ( int ) scoreTable [ _col, 20 ] );
        }


        public static void RowClickedHandler ( int visualScoresheetRow )
        {
            int _takeScoreRow = VisualScoresheetToTakeScoreRowConverter ( visualScoresheetRow );
            GameStatus.GameRow _gamerow = GameStatus.GameRows [ _takeScoreRow ];
            // If TakeScore button is not visible then return.
            if ( !_gamerow.TakeScoreVisible )
            {
                return;
            }

            // Scoring:  If dice roll is 3; 
            if ( GameClock.DiceRoll == 3 )
            {
                CommitDetails.ScoringSelected ( _gamerow.TakeScoreString, _takeScoreRow );
            }
            //TODO: Implement dice filter; check if taking score early is warranted.>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        }
        // End RowClickedHandler.

        //public static List < int [,]> Scoring ()
        //{

        //}
        
        static int VisualScoresheetToTakeScoreRowConverter ( int visualScoresheetRow )
        {
             int _visRow = visualScoresheetRow;
            int _takeScoreRow = ( _visRow < 7 ) ? _visRow - 1 : _visRow - 4;
            //_postRow = ( _row < 6 ) ? _row + 1 : _row + 4;
            return _takeScoreRow;
        }

        static int ConvertPlayerUpToScoresheetColumn ( )
        {
            int _column = GameClock.PlayerUp + 1;
            return _column;
        }

        static int ConvertScoreTableRowToPostRow ( int row )
        {
            // Upper section _row = row +1.
            int _postRow = row + 1;
            if ( row >= 8 )
            {
                _postRow = row + 2;
            }
            //// 3OK thru Chance _row = row +2.  >>  _row = ( row >= 8 ) && ( row <= 13) => row+2 
            //if (( row >= 8) && ( row <= 13 ))
            //{
            //    _postRow = row + 2;
            //}
            //// 5OK scoreTable rows 14 thru 17 = scoreSheet row 16. ( 0 = X, >0 = +, (?row 14 gets filled last?)).
            //if (( row >= 14 ) && ( row <= 17 ))
            //{
            //    _postRow = 16;
            //}
            //// 5OK score row 18 = scoreSheet row 19.  >>> _row = ( row >= 18)  => row -1
            //if ( row >= 18 )
            //{
            //    _postRow = row - 1;
            //}
            
            return _postRow;
        }

        static int ConvertTakeScoreRowToScoreTableRow ( int takeScoreRow)
        {
            int _row = ( takeScoreRow < 6 )? takeScoreRow : (takeScoreRow + 2);
            return _row;
        }


        #endregion GameModel Methods





        #region GameClockClass
        /// <summary>
        /// Tracks:   GameRound, PlayerUp and DiceRoll.
        /// </summary>
        public static class GameClock
        {
            /// <summary>
            /// Integer 1 to 3, advances PlayerUp
            /// </summary>
            public static int DiceRoll { get; set; }
            /// <summary>
            /// Game over after 16 rounds
            /// </summary>
            public static int GameRound { get; set; }
            /// <summary>
            ///  The player with the dice
            /// </summary>
            public static int PlayerUp { get; set; }

            static GameClock ()
            {
                //NewGame ();
            }

            /// <summary>
            /// Resets to first player up.
            /// </summary>
            public static void NewGame ()
            {
                DiceRoll = 1;
                GameRound = 1;
                PlayerUp = 1;
            }

            /// <summary>
            ///  Call after Player rolls dice.
            /// </summary>
            public static void NextRoll ()
            {
                    DiceRoll++;
            }

            /// <summary>
            /// Call when player takes score and next player rolls.
            /// </summary>
            public static void NextPlayer ()
            {
                if ( PlayerUp < 3 )
                {
                    PlayerUp++;
                    DiceRoll = 1;
                }
                else
                {
                    GameRound++;
                    PlayerUp = 1;
                    DiceRoll = 1;
                }
                CommitDetails.NextPlayer ();
            }
        }
        //      End GameClock class.        *****             End GameClock class.        *****             End GameClock class.        ***** 
        #endregion GameClockClass

        //#region GameColors class
        //    /// <summary>
        //    /// Type Initialization Exception  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //    /// </summary>
        //public static class GameColors
        //{
        //    static GameColors ()
        //    {
        //        Color _color1 = Color.FromArgb ( 255, 0, 255, 0 );
        //        Color _color2 = Color.FromArgb ( 255, 0, 0, 200 );
        //        Color _color3 = Color.FromArgb ( 255, 150, 0, 0 );
        //        PlayerColors.Add ( _color1 );
        //        PlayerColors.Add ( _color2 );
        //        PlayerColors.Add ( _color3 );
        //    }

        //    public static List <Color> PlayerColors  { get; set; }
        //}
        //#endregion GameColors class



        #region GameStrings Class
        //      GameStrings class.      ********              GameStrings class.      ********              GameStrings class.      ********        
        public static class GameStrings
        {
            #region Fields
            //      Fields
            //static string [] commitStrings;
            static string [] headerLabels;
            //static string [] playerNames;
            static string [] scoringStrings;
            #endregion Fields

            #region GameStrings  Constructor
            //      Constructor
            static GameStrings ()
            {
                CommitActionStrings = new string [] { ">>>  Press to Roll Dice  <<<", "<<<<<<<  Choose a scoring option", ">>>  Press to Accept  <<<", "Press to start a new game." };
                CommitDescriptionStrings = new string [] { "You have", "rolls left.", "Take", "has won the game." };
                //TODO: headerLabels will only be used as row header texts for the first two columns, and for string building elsewhere.
                headerLabels = new string [] { "Upper Section", "Aces", "Deuces", "Threes", "Fours", "Fives", "Sixes", "> 63 Bonus", "Upper Score", "Lower Section",
                "3 o’Kind", "4 o’Kind", "Full House", "Small Straight", "Large Straight","Chance", "5 o’Kind", "5OK Bonus",
                /* "Bonus", */"Lower Total", "Grand Total", "Points Scored", "Add Aces", "Add Deuces", "Add Threes", "Add Fours",
                "Add Fives", "Add Sixes", "Score 35", "===>", "   ", "Add All Dice", "Add All Dice", "Score 25", "Score 30", "Score 40", "Add All Dice", /*"Score 50", */"+ For Each 5OK",
                "+ ADD 50/100", "===>", "===>", /*"Player",*/ "            ", "You have a XX% chance ", "0" };
                // Default player names provided.
                PlayerNames = new string [] { "Player 1", "Player 2", "Player 3" };
                scoringStrings = new string [] { "Scratch", "Take", "for", "points." };
            }
            // End  GameStrings Constructor
            #endregion GameStrings  Constructor
            
            // Indexers 
            public static string [] CommitActionStrings { get; set; }

            public static string [] CommitDescriptionStrings { get; set; }

            public static string [] PlayerNames { get; set; }


            #region GameStrings Methods
            // Methods

            public static string GetTakeScoreString ( int takeScoreRow, int score )
            {
                int _row = ( takeScoreRow < 6 ) ? ( takeScoreRow + 1 ) : ( takeScoreRow + 4 );
                string _string = "";
                if ( score > 0 )
                {
                    _string = $"{ headerLabels [ _row ]} { scoringStrings [ 2 ]} { score.ToString ()} { scoringStrings [ 3 ]}";
                }
                else
                {
                    _string = $"{ scoringStrings [ 0 ]} { headerLabels [ _row ]}.";
                }

                return _string;
            }

            public static string GetCommitString ()
            {
                string _string = "";

                return _string;
            }


            public static string GetHeaderString  ( int column, int row )
            {
                string text = headerLabels [ ( ( column * 20 ) + row ) ];
                return text;
            }


            public static string GetPlayerName ( int column )
            {
                string text = PlayerNames [ column ];
                return text;
            }


            #endregion GameStrings Methods
        }
        // End GameStrings class
        #endregion GameStrings Class



        
        public static class CommitDetails
        {
            //      Fields
            //      Constructor
            static CommitDetails ()
            { }
            //      Properties
            // Commit button parameters.
            public static string PlayerName { get; set; }
            public static string Action { get; set; }
            public static string Description { get; set; }
            public static Color PlayerColor { get; set; }
            // Scoresheet parameters.
            public static List<int []> ResultsList { get; set; }
            public static int TakeScoreRow { get; set; }

            //      Methods
            // 
            public static void NextPlayer ()
            {
                PlayerName = GameStrings.PlayerNames [ ( GameClock.PlayerUp - 1 ) ];
                //PlayerColor = GameColors.PlayerColors [ ( GameClock.PlayerUp - 1 ) ];
                // Setting TakeScoreRow to -1 as a flag for unused.
                TakeScoreRow = -1;
            }

            public static void UpdateResults ( int row, int value )
            {
                int [] _rowValue = new int [ 2 ];
                _rowValue [ 0 ] = row;
                _rowValue [ 1 ] = value;
                ResultsList.Add ( _rowValue );
            }

            public static void RollText ()
            {
                int _rollsRemaining = 3 - GameClock.DiceRoll;
                Action = GameStrings.CommitActionStrings [ 0 ];
                Description = $"{ GameStrings.CommitDescriptionStrings [ 0 ]} { _rollsRemaining } { GameStrings.CommitDescriptionStrings [ 1 ]}";
            }

            public static void ChooseText ()
            {
                int _rollsRemaining = 3 - GameClock.DiceRoll;
                Action = GameStrings.CommitActionStrings [ 1 ];
                Description = $"{ GameStrings.CommitDescriptionStrings [ 0 ]} { _rollsRemaining } { GameStrings.CommitDescriptionStrings [ 1 ]}";
            }

            public static void ScoringSelected ( string takeScoreString, int takeScoreRow )
            {
                //CommitScore = true;
                Action = $"{GameStrings.CommitActionStrings [ 2 ]}";
                Description = $"{ GameStrings.CommitDescriptionStrings [ 2 ]} {takeScoreString}";
                CommitDetails.TakeScoreRow = takeScoreRow;
            }
        }
        // End CommitDetails.
    }

}
