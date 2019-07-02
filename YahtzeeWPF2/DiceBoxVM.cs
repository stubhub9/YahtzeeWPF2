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
    /// <summary>
    /// Convert GameDice to VisDice values.
    /// Bool Held to Y 
    /// ?Ordinals sorted by face value to X; group clones;  space extra dice over gaps
    /// ?RowDice to Brush ( and X grouping, and Y held/ release.
    /// int face value to string value
    /// </summary>
    public static class DiceBoxVM
    {
        // Constructor
        static DiceBoxVM ()
        {
            VimDice = new VimDie [ 5 ];
            for ( int i = 0; i < 5; i++ )
            {
                VimDice [ i ] = new VimDie ()
                {

                };
            }
        }

        // Properties
        public static VimDie [] VimDice { get; set; }


        // Method

        //REFACTOR:  Ignores VimDie/ VimDice and uses point.
        public static Point DieWasClicked ( int thisDie )
        {
            var _topLeft = new Point ();
            GameDice.DieStruct _die = GameDice.DieStructs [ thisDie ];
            _topLeft.X = 60.0 + ( thisDie * 130.0 );
            _topLeft.Y = ( _die.Held ) ? 365.0 : 550.0;
            GameDice.DieStructs [ thisDie ].Held = !_die.Held;
            return _topLeft;
        }


        public static void UpdateDiceVisMod ()
        {
            for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
            {
                var _gDie = GameDice.DieStructs [ _thisDie ];
                var _vDie = new VimDie ()
                {
                    FaceValue = _gDie.FaceValue.ToString (),
                    Left = ( _thisDie * 130.0 ) + 60.0,
                    Top = ( _gDie.Held ) ? 550.0 : 365.0,
                };
                VimDice [ _thisDie ] = _vDie;
            }
        }

        /// <summary>
        /// Supplies params to the view.
        /// </summary>
        public struct VimDie
        {
            public string FaceValue;
            public string LeftValue;
            public string RightValue;
            //REFACTOR:  replace point TopLeft with Left and Top.
            public Point TopLeft;
            public double Left;
            public double Top;
            //public struct Rbg
            //{
            //    public int Red;
            //    public int Blue;
            //    public int Green;
            //}
        }






    }
}
