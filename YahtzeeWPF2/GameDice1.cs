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



namespace DiceBoxWpf
//namespace YahtzeeWPF2
{
    public static class GameDice1
    {
        // Fields
        //static int faceValue = 0;
        //static int [] faceValues;
        //static bool [] filterList;
        //static int maxStraight;
        //static List<int []> pairsList;
        //static int sum;
        //static int [] valueIndexedMultiples;
        static Random randomDieValue = new Random ();

        // Constructor
        static GameDice1 ()
        {
            BuildGameDice ();
        }


        // Properties
        // Five data models of a die.
        public static DieStruct [] DieStructs
        { get; set; }

        // Bool matrix of which dies should be held for clones ( one thru six ) and straights [7, 5].
        public static bool [] [] FiltersMatrix
        { get; set; }

        public static int MaxStraight
        { get; set; } = 0;

        public static int [][] PairsList
        { get; set; }

        public static int Sum
        { get; set; }

        /// <summary>
        /// int [7] { index zero is unused during gameplay, indexes 1- 6 contain how many dice have that face value. }
        /// </summary>
        public static int [] ValueIndexedMultiples
        { get; set; }



        // Methods

        static void BuildGameDice ()
        {
            DieStructs = new DieStruct [ 5 ];
            for ( int i = 0; i < 5; i++ )
            {
                DieStructs [ i ] = new DieStruct ();
            }
            NewDice ();
        }


        public static void NewDice ()
        {
            for ( int i = 0; i < 5; i++ )
            {
                DieStructs [ i ].Ordinal = i;
                DieStructs [ i ].Held = false;
                //DieStructs [ i ].FaceValue = RollDie (); 
            }
            RollDice ();
        }


        public static void RollDice ()
        {
            // LINQ -- Query expression.       
            var subset = from i in DieStructs where i.Held == false select i;
            foreach ( var item in subset )
            {
                //        Not allowed to change members of search items.  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!         I backdoor did it, but might be a bad idea/ habit.
                //item.FaceValue = RollDie ();
                // Had to add a method to the struct, that does the same thing.
                item.RollDie ();
            }
            SortDice ();
            UpdateSumAndValueIndexedMultiples ();
            UpdateStraights ();
        }


        /// <summary>
        /// Updates to the next random dice face value
        /// </summary>
        static int RollDie ()
        {
            int _faceValue = randomDieValue.Next ( 1, 7 );
            //if ( GameModel.GameClock.GameRound == 17 )
            //    faceValue = 0;
            //faceValue = 6;
            //if ( GameModel.GameClock.GameRound > 1 )
            //{
            //    faceValue = 5;
            //}
            return _faceValue;
        }


        static void SortDice ()
        {
            int _max = 4;
            bool _unsorted = true;
            var _swapStruct = new DieStruct ();
            // Sort the dice by face value.
            while ( _unsorted )
            {
                _unsorted = false;
                for ( int i = 0; i < _max; i++ )
                {
                    if ( DieStructs [ i ].FaceValue > DieStructs [ i + 1 ].FaceValue )
                    {
                        _unsorted = true;
                        _swapStruct = DieStructs [ i ];
                        DieStructs [ i ] = DieStructs [ i + 1 ];
                        DieStructs [ i + 1 ] = _swapStruct;
                    }
                }
                _max--;
            }
        }


        static void UpdateStraights ()
        {
            // Multiples list, iterate through valueIndexedMultiples seeking doubles or better.
            PairsList = new int [ 2 ] [  ];
            for ( int _faceVal = 1; _faceVal < 7; _faceVal++ )
            {
                var _valMult = new int [ 2 ];
                if ( ValueIndexedMultiples [ _faceVal ] >= 2 )
                {
                    _valMult [ 0 ] = _faceVal;
                    _valMult [ 1 ] = ValueIndexedMultiples [ _faceVal ];
                    if ( _valMult [ 1 ] < PairsList [ 0 ] [1 ] )
                    {
                        PairsList [ 1 ] = _valMult;
                    }
                    else
                    {
                        PairsList [ 1 ] = PairsList [ 0 ];
                        PairsList [ 0 ] = _valMult;
                    }
                }
            }
        }


        static void UpdateSumAndValueIndexedMultiples ()
        {
            Sum = 0;
            ValueIndexedMultiples = new int [ 7 ];
            FiltersMatrix = new bool [ 7 ] [];
            // Compute the sum of all dice, and populate the valueIndexedMultiples array.
            foreach ( var _die in DieStructs )
            {
                Sum += _die.FaceValue;
                ValueIndexedMultiples [ _die.FaceValue ]++;
            }

            // Build a filterList for ones through sixes.
            bool _bool = false;
            for ( int _dieVal = 1; _dieVal < 7; _dieVal++ )
            {
                var _filter = new bool [ 5 ];
                for ( int i = 0; i < 5; i++ )
                {
                    _bool = ( DieStructs [ i ].FaceValue == _dieVal ) ? true : false;
                    _filter [ _dieVal ] = _bool;
                }
                FiltersMatrix [ _dieVal ] = _filter;
            }
            // End of  filterList loop by die values 1 thru 6.
        }


        // Struct

        public struct DieStruct
        {
            // Fields
            public bool Held;
            public int FaceValue;
            public int Ordinal;

            // Method
            public void RollDie ()
            {
                FaceValue = GameDice1.RollDie ();
            }
        }

    }
}
