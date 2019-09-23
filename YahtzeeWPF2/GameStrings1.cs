using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    public static class GameStrings1
    {
        //      Fields
        static string [] headerLabels;
        static string [] scoringStrings;
        static string [] rowHeader1Labels;
        static string [] rowHeader2Labels;


        //      Constructor
        static GameStrings1 ()
        {
            CommitActionStrings = new string [] { ">>>  Press to Roll Dice.  You have ", " rolls left.  <<<", "<<<<<<<  Choose a scoring option",
                    ">>>  Press to Accept  <<<",  "^^^^  HAS WON THE GAME (no ties)  ^^^^",  /*"Press to start a new game."*/ };
            //CommitActionStrings = new string [] { ">>>  Press to Roll Dice  <<<", "<<<<<<<  Choose a scoring option",
            //        ">>>  Press to Accept  <<<",  "^^^^  HAS WON THE GAME (no ties)  ^^^^",  /*"Press to start a new game."*/ };

            CommitDescriptionStrings = new string [] { "Rolling for", "Take", "for", "points.", "No scoring option selected", "Press the New Game button or Quit" };

            //TODO: headerLabels will only be used as row header texts for the first two columns, and for string building elsewhere.
            headerLabels = new string [] { "Upper Section", "Aces", "Deuces", "Threes", "Fours", "Fives", "Sixes", "> 63 Bonus", "Upper Score", "Lower Section",
                "3 o’Kind", "4 o’Kind", "Full House", "Small Straight", "Large Straight","Chance", "5 o’Kind", "5OK Bonus",
                "Lower Total", "Grand Total", "Points Scored", "Add Aces", "Add Deuces", "Add Threes", "Add Fours",
                "Add Fives", "Add Sixes", "Score 35", "===>", "   ", "Add All Dice", "Add All Dice", "Score 25", "Score 30", "Score 40", "Add All Dice", "+ For Each 5OK",
                "+ ADD 50/100", "===>", "===>",  "            ", "You have a XX% chance ", "0" };
            // Default player names provided.
            PlayerNames = new string [] { "Player 1", "Player 2", "Player 3" };
            scoringStrings = new string [] { "Scratch", "Take", "for", "points." };

            rowHeader1Labels = new string [] { "Aces", "Deuces", "Threes", "Fours", "Fives", "Sixes", "> 63 Bonus", "Upper Score",
                "3 o’Kind", "4 o’Kind", "Full House", "Small Straight", "Large Straight","Chance", "5 o’Kind", "5OK Bonus", "Lower Total", "Grand Total" };
            rowHeader2Labels = new string [] {  "Add Aces", "Add Deuces", "Add Threes", "Add Fours", "Add Fives", "Add Sixes", "Score 35", "===>",
                "Add All Dice", "Add All Dice", "Score 25", "Score 30", "Score 40", "Add All Dice", "+ For Each 5OK", "+ ADD 50/100", "===>", "===>" };
        }


        // Properties 
        // Vim uses GetActionStrings
        public static string [] CommitActionStrings { get; set; }

        public static string [] CommitDescriptionStrings { get; set; }

        public static string [] PlayerNames { get; set; }


        #region GameStrings Methods


        public static string GetTakeScoreString1 ( ResultsItem item )
        {
            string _string = "";
            if ( item.Value > 0 )
            {
                // eg:  Aces for XX points
                _string = $"{ rowHeader1Labels [ ( int ) item.Row ]} { scoringStrings [ 2 ]} { item.Value.ToString ()} { scoringStrings [ 3 ]}";
            }
            else
            {
                // eg:  Scratch Aces
                _string = $"{ scoringStrings [ 0 ]} {  rowHeader1Labels [ ( int ) item.Row ]}.";
            }

            return _string;
        }
        

        // ?????????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
        public static string GetHeaderString1 ( VisColumn column, VisRow row )
        {
            string text = headerLabels [ ( ( ( ( int ) column ) * 20 ) + ( int ) row ) ];
            return text;
        }
        /// <summary>
        ///  Used by VisualBuilderClasses/ ScoresheetBuilderInstance.GetText ()
        /// </summary>
        public static string GetHeaderString ( int column, int row )
        {
            string text = headerLabels [ ( ( column * 20 ) + row ) ];
            return text;
        }




        /// <summary>
        ///  Used by VisualBuilderClasses/ ScoresheetBuilderInstance.GetText ()
        /// </summary>
        public static string GetPlayerName ( int playerIndex )
        {
            string text = PlayerNames [ playerIndex ];
            return text;
        }


        public static string GetActionString ()
        {
            // CommitActionStrings = new string [] { ">>>  Press to Roll Dice.  You have ", " rolls left.  <<<", "<<<<<<<  Choose a scoring option",
            //      ">>>  Press to Accept  <<<",  "^^^^  HAS WON THE GAME (no ties)  ^^^^",  /*"Press to start a new game."*/ };

            int _roll = GameModel1.CurrentDiceRoll;
            int _rollsLeft = 3 - _roll;
            Row row = GameModel1.RowSelected;
            string _text1 = "  ";

            if ( GameModel1.GameRound >= 17 )
            {
                // Has won the game.
                _text1 = CommitActionStrings [ 4 ];
            }
            else if ( _roll < 3 )
            {
                // Press to roll dice.  You have 2/1 rolls left.
                _text1 = $"{CommitActionStrings [ 0 ]}{_rollsLeft}{CommitActionStrings [ 1 ]} ";
            }
            else if ( row == Row.Unselected )
            {
                //   Choose a scoring option.
                _text1 = $"{CommitActionStrings [ 2 ]}";
            }
            else
            {
                //  Press to Accept.
                _text1 = $"{CommitActionStrings [ 3 ]}";
            }
            return _text1;
        }



        public static string GetDescriptionString ()
        {
            //   CommitDescriptionStrings = new string [] { "Rolling for", "Take", "for", "points.", "Press the New Game button or Quit" };
            int _roll = GameModel1.CurrentDiceRoll;
            //int _rollsLeft = 3 - _roll;
            Row _row = GameModel1.RowSelected;
            string _text = "  ";
            string _rowText = " ";

            // If Game is over =>     Press the New Game button or Quit
            if ( GameModel1.GameRound >= 17 )
            {
                _text = CommitDescriptionStrings [ 5 ];
            }
            else
            {
                if ( _row == Row.Unselected )
                    // "No scoring option selected"
                    _text = $"{CommitDescriptionStrings [ 4 ]}";
                else
                {
                    //int _intRow = ( int ) _row;
                    //if ( _intRow >= 6 )
                    //    _intRow = _intRow - 2;
                    //ResultsItem _resultsItem = GameScoring1.ScoringList [ _intRow ];
                    ResultsItem _resultsItem = GameScoring1.GetResultsItem ( _row );
                    string _points = _resultsItem.Value.ToString ();
                    _rowText = rowHeader1Labels [ (int) _row ];
                    if ( _roll < 3 )
                        _text = $"{CommitDescriptionStrings [ 0 ]} {_rowText}.";
                    else
                        _text = $"{CommitDescriptionStrings [ 1 ]} {_rowText} {CommitDescriptionStrings [ 2 ]} {_points} {CommitDescriptionStrings [ 3 ]}";

                }
            }
            return _text;
        }

        

        #endregion GameStrings Methods
    }
}
