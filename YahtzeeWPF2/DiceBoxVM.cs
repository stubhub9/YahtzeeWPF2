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
    /// Ordinals sorted by face value to X; group clones;  space extra dice over gaps
    /// RowDice to Brush ( and X grouping, and Y held/ release.
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

        // Ignores VimDie/ VimDice
        public static Point DieWasClicked ( int thisDie )
        {
            var _topLeft = new Point ();
            GameDice1.DieStruct _die = GameDice1.DieStructs [ thisDie ];
            _topLeft.X = 60.0 + ( thisDie * 130.0 );
            //GameDice1.DieStructs [ thisDie ].Held = !_die.Held;
            //_topLeft.Y = ( _die.Held ) ? 550.0 : 365.0;
            _topLeft.Y = ( _die.Held ) ? 365.0 : 550.0;
            GameDice1.DieStructs [ thisDie ].Held = !_die.Held;
            return _topLeft;
        }



        public static void UpdateDiceVisMod ()
        {
            for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
            {
                var _gDie = GameDice1.DieStructs [ _thisDie ];
                var _vDie = new VimDie ()
                {
                    FaceValue = _gDie.FaceValue.ToString (),
                    Left = ( _thisDie * 130.0 ) + 60.0,
                    Top = ( _gDie.Held ) ? 550.0 : 365.0,
                };
                VimDice [ _thisDie ] = _vDie;


                //VimDice [ _thisDie ].FaceValue = _gDie.FaceValue.ToString ();
                //VimDice [ _thisDie ].Left = ( _thisDie * 130.0 ) + 60.0;
                //VimDice [ _thisDie ].Top = ( _gDie.Held ) ? 550.0 : 365.0;
                //VimDice [ _thisDie ].TopLeft.X = ( _thisDie * 130.0 ) + 60.0;
                //VimDice [ _thisDie ].TopLeft.Y = ( _gDie.Held ) ? 550.0 : 365.0;

            }


            //public static Point [] UpdateDiceVisual ()
            //{
            //    var _points = new Point [ 5 ];
            //    for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
            //    {
            //        var _die = GameDice1.DieStructs [ _thisDie ];


            //    }
            //Die _die;
            //Button _visDie;
            //double y1 = 365;
            //double y2 = 550;
            //double x;
            //for ( int dieNum = 0; dieNum < 5; dieNum++ )
            //{
            //    _die = new Die ();
            //    _die = GameDice.DieList [ dieNum ];
            //    _visDie = new Button ();
            //    _visDie = visualDiceList [ dieNum ] [ 1 ];
            //    _visDie.Content = _die.FaceValue;
            //    x = 60 + ( dieNum * 130 );

            //    visualDiceList [ dieNum ] [ 0 ].Margin = ( _die.Held ) ? new Thickness ( x, y2, 0.0, 0.0 ) : new Thickness ( x, y1, 0.0, 0.0 );
            //}

        }








        public struct VimDie
        {
            public string FaceValue;
            public string LeftValue;
            public string RightValue;
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
