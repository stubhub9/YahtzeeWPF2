using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeWPF2
{
    
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
            get =>VimDice.dice;
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
            

            // Return Bad Click  --
            // Dialog => must choose row to score
            //  Dialog => are you sure ( accidental double click, poor dice selection, poor game scoring )


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
        public static void RowClicked ( Row rowClicked )
        {

        }



        // Private Methods








        #endregion Methods



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
