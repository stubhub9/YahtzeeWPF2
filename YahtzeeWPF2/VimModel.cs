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


    public static class VimModel
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
            Player1 = 2,
            Player2 = 3,
            Player3 = 4,
            TakeScore = 5,
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



        #region Methods

        /*NYI:  Button appears when the Commit button should have a second option.
         */
        public static void CommitOptionWasClicked ()
        {
            // NYI
        }

        

        public static void CommitWasClicked ()
        {
            // The model should process the click!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            GameModel1.CommitWasClicked ();

        }


        //NYI:  SEE VisDice.
        //Toggle die.Held
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
        public static void NewGame ()
        {
            GameModel1.NewGame ();

            //NYI:  Vim/ Vis dice needs to be thought out!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            VimDice.NewDice ();
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
        public static void UpdateCommitDetails ()
        {

        }

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
                get => GameStrings1.PlayerNames [(int) PlayerUp];
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
                    _vResult.Column = ( VisColumn ) Enum.Parse ( typeof ( VisColumn ), GameModel1.PlayerUp.ToString () );
                    //    Enum.TryParse ( row.ToString (), false, out visRow );
                    _vResult.Row = (VisRow) Enum.Parse ( typeof ( VisRow ), resultItem.Row.ToString () );
                    _vResult.Value = resultItem.Value.ToString ();
                    _visResults.Add ( _vResult );
                }
                return _visResults;
            }


            public static List <VisGameRow> BuildVisGameRows ()
            {
                var _visGameRows = new List <VisGameRow> ();




                return _visGameRows;
            }
            


            public struct VisScoresheetResult
            {
                public VisColumn Column;
                public VisRow Row;
                public string Value;
            }


            //    enum HighlightStyle   Filled = 0,   Open,    Scratch,    Points,  BestChoice,   Insist
            public struct VisGameRow
            {
                public bool IsVisible;
                public HighlightStyle Highlight;
                public string Text;

            }


        }


        //NYI:  Do I need this??????????????????????????????????????????????????????????????????????????????????????????????????????
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



    }
}
