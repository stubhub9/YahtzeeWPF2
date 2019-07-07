using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace YahtzeeWPF2
{


    struct VimDie1
    {
        public string FaceValue;
        //public string LeftFaceValue;
        //public string RightFaceValue;
        public double Left;
        public double Top;
        //    public int Red;
        //    public int Blue;
        //    public int Green;

    }


    static class VimModel
    {
        // Fields
        //static List<VimDie1> visDice;
        static int [] klugeScoresheetUpdate;
        static List<int []> klugeScoresheetUpdates;
        // Documented Magic Numbers

        const double DieHeldY = 550.0;
        const double DieNotHeldY = 365.0;
        const double DieOffsetX = 60.0;
        const double DieSpaceX = 130.0;

        //const double
        //const int


        // Constructor

        static VimModel ()
        {

        }



        // Properties

        static VimDie1 [] Dice
        {
            get => VimDice.dice;
        }



        // Enum

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


        enum VisColumn
        {
            Unselected = -1,
            RowHeader1 = 0,
            RowHeader2 = 1,
            Player1 = 2,
            Player2 = 3,
            Player3 = 4,
            TakeScore = 5,
        }

        enum VisRow
        {
            Unselected = -1,
            UpperHeader = 0,
            Ones, Twos, Threes, Fours, Fives, Sixes,
            Score63, ScoreUpper,
            LowerHeader,
            ThreeX, FourX, FullHouse, FourStr, FiveStr, Chance,
            FiveX1, FiveX2, FiveX3, FiveX4,
            ScoreFiveX, ScoreLower, ScoreTotal
        }



        #region Methods

        /* Button appears when the Commit button should have a second option.
         */
        public static void CommitOptionWasClicked ()
        {
            // NYI
        }

        /* Expose Properties to the Visual
         * Dice params
         * Scoresheet params
         * OnReturn of [STAThread] from models, the Visual will implement the changes.
         */
        public static void CommitWasClicked ()
        {
            // RBC:  Dialog => must choose row to score
            // RBC:  Dialog => are you sure ( accidental double click, poor dice selection, poor game scoring )

            // Return Bad Click:  Must choose a scoring option.
            /* Using Row.TotalScore as undefined flag, */




            // Roll Dice Click
            /* Return updated dice visual
             *  Return updated take score game rows*/

            // Take Score Click
            /* Return Scoresheet update based on List of Result Items
             Return updated dice visual
             *  Return updated take score game rows*/


        }


        // Toggle die.Held
        public static void DieWasClicked ( int DieOrdinalClicked )
        {


            //var _topLeft = new Point ();
            //GameDice.DieStruct _die = GameDice.DieStructs [ thisDie ];
            //_topLeft.X = 60.0 + ( thisDie * 130.0 );
            //_topLeft.Y = ( _die.Held ) ? 365.0 : 550.0;
            //GameDice.DieStructs [ thisDie ].Held = !_die.Held;
            //return _topLeft;

        }


        // New game OnInit or clicked.
        public static void NewGameWasClicked ()
        {
            GameModel1.NewGame ();
            VimDice.NewDice ();
        }


        /* Visual should call a dialog/ subwindow; and then 
         * pass changes made to here. 
         */
        public static void OptionsWasClicked ()
        {
            // NYI
        }


        /* Visual should pass info on row clicked
         * 
         * Update takescore row highlights
         */
        public static void RowClicked ( string buttonName )
        {
            /* Convert button name to enum for row numbers;
             *  If the takescore row shouldn't be clickable; then return  (Was only clickable on roll 3 and visible );
             * Vis then updates the commit visual.
             */
            VisRow _visRow = ( VisRow ) Int32.Parse ( buttonName.Remove ( 0, 8 ) );
            Row _row = ( Row ) Enum.Parse ( typeof ( Row ), _visRow.ToString () );

            GameModel1.RowClicked ( _row );
            if ( GameModel1.RowSelected != Row.Unselected )
            {

            }
            //  If GameModel1.RowSelected remains Unselected:  Ignore
        }


        public static void UpdateCommitDetails ()
        {

        }


        //// Convert RowX between  int row values between GameModel score table and Vis GameSheet.
        //[ValueConversion ( typeof ( Row ), typeof ( VisRow ) )]
        //public class RowVisRowConverter : IValueConverter
        //{
        //    public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        //    {
        //        Row row = ( Row ) value;
        //        VisRow visRow;
        //        Enum.TryParse ( row.ToString (), false, out visRow );
        //        return visRow;
        //    }

        //    public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        //    {
        //        Row row;
        //        VisRow visRow = ( VisRow ) value;
        //        Enum.TryParse ( visRow.ToString (), false, out row );
        //        return visRow;
        //    }
        //}


        //// Convert PlayerX int column values between GameModel score table and Vis GameSheet.
        //[ValueConversion ( typeof ( Player ), typeof ( VisColumn ) )]
        //public class PlayerVisColumnConverter : IValueConverter
        //{
        //    public object Convert ( object value, Type targetType, object parameter, CultureInfo culture )
        //    {
        //        Player player = ( Player ) value;
        //        VisColumn visColumn;
        //        visColumn = ( VisColumn ) Enum.Parse ( typeof ( VisColumn ), player.ToString () );
        //        return visColumn;
        //    }

        //    public object ConvertBack ( object value, Type targetType, object parameter, CultureInfo culture )
        //    {
        //        Player player;
        //        VisColumn visColumn = ( VisColumn ) value;
        //        player = ( Player ) Enum.Parse ( typeof ( Player ), visColumn.ToString () );
        //        return player;
        //    }
        //}



        // Private Methods







        //#endregion Methods


        public static class CommitDetails
        {
            //      Fields
            //      Constructor

            //      Properties

            // Commit button parameters.

            // Sets color 
            public static Player PlayerUp
            {
                get => GameModel1.PlayerUp;
            }

            // Set the Commit button texts.
            public static string PlayerName
            {
                get => PlayerUp.ToString ();
            }

            // Larger center string
            public static string Action { get; set; }
            public static string Description { get; set; }

            // Scoresheet parameters, needs to get built before rolling dice.
            public static List<VisScoresheetResult> VisScoresheetResults { get; set; }




            // Methods

            public static void BuildVisScoresheetResults ()
            {

            }

            // Clear old ResultsList,
            public static void ClearDetails ()
            {
                ResultsList = new List<int []> ();

            }





            public static void ChooseText ()
            {
                int _rollsRemaining = 3 - GameModel1.CurrentDiceRoll;
                Action = GameStrings1.CommitActionStrings [ 1 ];
                Description = $"{ GameStrings1.CommitDescriptionStrings [ 0 ]} { _rollsRemaining } { GameStrings1.CommitDescriptionStrings [ 1 ]}";
            }


            public static void DeclareWinner ()
            {
                PlayerName = GameStrings1.PlayerNames [ ( int ) GameModel1.GetWinner () ];
                Action = GameStrings1.CommitActionStrings [ 3 ];
                Description = GameStrings1.CommitDescriptionStrings [ 3 ];
            }


            public static void NextPlayer ()
            {
                PlayerName = GameStrings1.PlayerNames [ ( int ) GameModel1.PlayerUp ];
                //PlayerColor = GameColors.PlayerColors [ ( GameClock.PlayerUp - 1 ) ];
                // Setting TakeScoreRow to -1 as a flag for unused.
                TakeScoreRow = -1;
            }


            public static void RollText ()
            {
                int _rollsRemaining = 3 - GameClock.DiceRoll;
                Action = GameStrings1.CommitActionStrings [ 0 ];
                Description = $"{ GameStrings1.CommitDescriptionStrings [ 0 ]} { _rollsRemaining } { GameStrings1.CommitDescriptionStrings [ 1 ]}";
            }


            public static void ScoringSelected ( string takeScoreString, int takeScoreRow )
            {
                Action = $"{GameStrings1.CommitActionStrings [ 2 ]}";
                Description = $"{ GameStrings1.CommitDescriptionStrings [ 2 ]} {takeScoreString}";
                CommitDetails.TakeScoreRow = takeScoreRow;
            }


            public static void UpdateResults ( int row, int value )
            {
                int [] _rowValue = new int [ 2 ];
                _rowValue [ 0 ] = row;
                _rowValue [ 1 ] = value;
                ResultsList.Add ( _rowValue );
            }


            #endregion Methods


            public struct VisScoresheetResult
            {
                VisColumn Column;
                VisRow Row;
                string Value;
            }

        }



        /*  public struct DieStruct 
         *  int DieId  int FaceValue  public bool Held */
        // Convert  DieHeld to Y value; DieClicked to toggle held;
        // RowHighlighted to DiceHighlight; RowClicked to DiceHeld and Highlight
        // 5 dice held detection, 5 dice match a row highlight

        private static class VimDice
        {

            // Fields

            public static VimDie1 [] dice;


            // Constructor

            static VimDice ()
            {
                BuildDice ();
            }


            // Property


            // Public Methods

            public static void NewDice ()
            {

            }


            public static void RollDice ()
            {

            }


            // Private Methods

            /// <summary>
            ///REDACT:  VisualBuilder does this???????????????????????????????????????????????????????????
            /// </summary>
            static void BuildDice ()
            {

            }



        }



        // Convert ResultItems to GameRow output to Vis, ?VimDice
        // Row clicked, inferred, recommended adjust highlights
        // Build take score strings, commit accept/ recommended strings
        // dice X groupings, Roll dice new values, dice X new values (slide)
        private static class GameRow
        {
            /* Choose and Build Row Highlight/ List of highlights
             Color, Opacity?, Visibility
             */


            // TakeScore string


        }






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
