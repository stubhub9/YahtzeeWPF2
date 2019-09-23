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



    public enum VisColumn
    {
        Unselected = -1,
        RowHeader1 = 0,
        RowHeader2 = 1,
        PlayerOne = 2,
        PlayerTwo = 3,
        PlayerThree = 4,
        TakeScore = 5,
        //Unselected = -1,
        //RowHeader1 = 0,
        //RowHeader2 = 1,
        //Player1 = 2,
        //Player2 = 3,
        //Player3 = 4,
        //TakeScore = 5,
    }

    public enum VisRow
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


    public static class VimModel
    {
        // Fields

        // Documented Magic Numbers

        // Maybe these belong in the visual as a style????????????????????????????????????????????????????????????????????????????????????
        const double DieHeldY = 550.0;
        const double DieNotHeldY = 365.0;
        const double DieOffsetX = 60.0;
        const double DieSpaceX = 130.0;


        // Constructor

        static VimModel ()
        {

        }



        // Properties
        


        // Enum





        #region Methods

        /*NYI:  Button appears when the Commit button should have a second option.
         */
        public static void CommitOptionWasClicked ()
        {
            // NYI
        }



        public static void CommitWasClicked ()
        {
            GameModel1.CommitWasClicked ();
            DiceBoxVM.UpdateDiceVisMod ();

        }


        //NYI:  SEE VisDice.
        //Toggle die.Held
        public static void DieWasClicked ( int OrdinalOfDieClicked )
        {

            DiceBoxVM.DieWasClicked (OrdinalOfDieClicked);

            //var _topLeft = new Point ();
            //GameDice.DieStruct _die = GameDice.DieStructs [ thisDie ];
            //_topLeft.X = 60.0 + ( thisDie * 130.0 );
            //_topLeft.Y = ( _die.Held ) ? 365.0 : 550.0;
            //GameDice.DieStructs [ thisDie ].Held = !_die.Held;
            //return _topLeft;

        }


        // New game OnInit or clicked.
        public static void NewGame ()
        {
            GameModel1.NewGame ();
            DiceBoxVM.UpdateDiceVisMod ();
        }


        /*NYI:  Visual should call a dialog/ subwindow; and then 
         * pass changes made to here. 
         */
        public static void OptionsWasClicked ()
        {
            // NYI
        }


        /// <summary>
        ///  Pass the row clicked to the game model.
        /// </summary>
        public static void RowClicked ( string buttonName )
        {
            // Convert button name to Row enum.
            VisRow _visRow = ( VisRow ) Int32.Parse ( buttonName.Remove ( 0, 8 ) );
            Row _row = ( Row ) Enum.Parse ( typeof ( Row ), _visRow.ToString () );

            // Process the clicked row.
            GameModel1.RowClicked ( _row );
        }


        /* Unused:  Game model will update all fields;
         *  OnReturn:  the Visual calls the viewmodel;
         *  which will translate the model's properties to Visual parameters.
         **/
        //public static void UpdateCommitDetails ()
        //{

        //}

        #endregion Methods



        /// <summary>
        ///  Supply Visual parameters on demand.
        /// </summary>
        public static class CommitDetails
        {

            // Commit button parameters.

            // Sets color 
            public static Player PlayerUp
            {
                get => GameModel1.PlayerUp;
            }


            // Set the Commit button texts.
            public static string PlayerName
            {
                //get => PlayerUp.ToString ();
                get => GameStrings1.PlayerNames [ ( int ) PlayerUp ];
            }


            // String for Larger center textbox.
            public static string Action
            {
                get => GameStrings1.GetActionString ();
            }

            // String for lower textbox.
            public static string Description
            {
                get => GameStrings1.GetDescriptionString ();
            }


            // Scoresheet parameters.  
            public static List<VisScoresheetResult> VisScoresheetResults
            {
                get => BuildVisScoresheetResults ();
            }


            // Takescore rows params
            public static List<VisGameRow> VisGameRows
            {
                get => BuildVisGameRows ();
            }




            // Methods

            public static List<VisScoresheetResult> BuildVisScoresheetResults ()
            {
                // ResultsItem Row Row; IsFilled;t Value;
                // VisScoresheetResult   VisColumn Column;     VisRow Row;   string Value;   var _results = GameModel1.;
                var _visResults = new List<VisScoresheetResult> ();
                foreach ( var resultItem in GameModel1.ResultsList )
                {
                    VisScoresheetResult _vResult = new VisScoresheetResult ();
                    //   visColumn = ( VisColumn ) Enum.Parse ( typeof ( VisColumn ), player.ToString () );
                    _vResult.Column = ( VisColumn ) Enum.Parse ( typeof ( VisColumn ), GameModel1.PlayerScored.ToString () );
                    //_vResult.Column = ( VisColumn ) Enum.Parse ( typeof ( VisColumn ), GameModel1.PlayerUp.ToString () );
                    //    Enum.TryParse ( row.ToString (), false, out visRow );
                    _vResult.Row = ( VisRow ) Enum.Parse ( typeof ( VisRow ), resultItem.Row.ToString () );

                    VisRow _row = _vResult.Row;
                    if ( ( _row == VisRow.FiveX1 ) || ( _row == VisRow.FiveX2 ) || ( _row == VisRow.FiveX3 ) || ( _row == VisRow.FiveX4 ) )
                    {
                        _vResult.Value = ( resultItem.Value == 0 ) ? "X" : "V";
                    }
                    else
                        _vResult.Value = resultItem.Value.ToString ();

                    _visResults.Add ( _vResult );
                }
                return _visResults;
            }



            //    public struct ResultsItem     public Row Row;     public bool IsFilled;    public int Value;
            public static List<VisGameRow> BuildVisGameRows ()
            {
                var _visGameRows = new List<VisGameRow> ();
                var _scoring = GameScoring1.ScoringList;
                foreach ( var item in _scoring )
                {
                    var _gameRow = new VisGameRow ();
                    //_vResult.Row = ( VisRow ) Enum.Parse ( typeof ( VisRow ), resultItem.Row.ToString () );
                    _gameRow.VisRow = ( VisRow ) Enum.Parse ( typeof ( VisRow ), item.Row.ToString () );
                    _gameRow.Text = item.Value.ToString ();

                    if ( item.IsFilled )
                    {
                        _gameRow.Highlight = HighlightStyle.Filled;
                    }
                    else if ( item.Value == 0 )
                    {
                        _gameRow.Highlight = ( GameModel1.CurrentDiceRoll == 3 ) ? HighlightStyle.Scratch : HighlightStyle.Open;
                    }
                    else
                    {
                        _gameRow.Highlight = HighlightStyle.Points;
                    }

                    if ( item.Row == GameModel1.RowSelected )
                        _gameRow.Highlight = HighlightStyle.Insist;

                    _visGameRows.Add ( _gameRow );
                }


                return _visGameRows;
            }


            // structs

            public struct VisScoresheetResult
            {
                public VisColumn Column;
                public VisRow Row;
                public string Value;
            }


            public struct VisGameRow
            {
                // Set in visual based on style.     //False if the row is filled.
                //public bool IsVisible;

                // Highlight enum:     Filled = 0,  Open, Scratch, Points
                public HighlightStyle Highlight;

                public string Text;

                //  Using the VisRow enum to identify the scoresheet row, and to convert to and from the GameModel Row enum values.
                public VisRow VisRow;
                // Notes:
                //  public GameRow ()    RowHighlight = HighlightStyle.Filled;      TakeScoreString = "   ";     TakeScoreValue = 0;    TakeScoreVisible = false;
                //    enum HighlightStyle   Filled = 0,   Open,    Scratch,    Points,  BestChoice,   Insist
            }


        }
        


    }
}
