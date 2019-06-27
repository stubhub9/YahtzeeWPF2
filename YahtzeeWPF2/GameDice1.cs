﻿using System;
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



//namespace DiceBoxWpf
namespace YahtzeeWPF2
{
    public static class GameDice1
    {
        // Fields
        static Random randomDieValue = new Random ();

        // Constructor
        static GameDice1 ()
        {
            BuildGameDice ();
        }


        // Properties
        //TODO: Used by Vim and this class.
        // Five data models of a die.
        public static DieStruct [] DieStructs
        { get; set; }

        //NYI:  To be used by VimDice
        // Bool matrix of which dies should be held for clones ( one thru six ) and straights [7, 5].
        // Ones through sixes, plus straight = 7.
        public static bool [] [] FiltersMatrix
        { get; set; }

        // Used by GameScoring, and maybe Vim AI
        public static int MaxStraight
        { get; set; } = 0;

        // Used by GameScoring
        public static int [] [] PairsList
        { get; set; }

        // Used by GameScoring
        public static int SumOfAllDice
        { get; set; }

        /// <summary>
        /// Used by GameScoring
        /// int [7] { index zero is unused during gameplay, indexes 1- 6 contain how many dice have that face value. }
        /// </summary>
        public static int [] ValueIndexedMultiples
        { get; set; }



        // Methods

        /// <summary>
        /// Only called by constructor.
        /// </summary>
        static void BuildGameDice ()
        {
            DieStructs = new DieStruct [ 5 ];
            for ( int i = 0; i < 5; i++ )
            {
                DieStructs [ i ] = new DieStruct ();
            }
            NewDice ();
        }


        /// <summary>
        /// Called for each new player.
        /// </summary>
        public static void NewDice ()
        {
            for ( int i = 0; i < 5; i++ )
            {
                DieStructs [ i ].DieId = i;
                DieStructs [ i ].Held = false;
            }
            RollDice ();
        }


        public static void RollDice ()
        {
            //// LINQ -- Query expression.       
            //var subset = from i in DieStructs where i.Held == false select i;
            //foreach ( var item in subset )
            //{
            //    //ERROR:        Not allowed to change members of search items.  !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!         I backdoor did it, but might be a bad idea/ habit.
            //    //item.FaceValue = RollDie ();
            //    // Had to add a method to the struct, that does the same thing.
            //    item.RollDie ();
            //}

            for ( int i = 0; i < 5; i++ )
            {
                if ( !DieStructs [ i ].Held )
                    DieStructs [ i ].FaceValue = RollDie ();
            }

            SortDice ();
            UpdateSumAndValueIndexedMultiples ();
            UpdatePairsList ();
            UpdateMaxStraightAndStraightFilterList ();
        }


        /// <summary>
        /// Returns a new die face value.
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


        /// <summary>
        /// Old version swapped the dies in Dice from low to high.
        /// New version: same, but add a Die ID for each die. 
        /// </summary>
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


        /// <summary>
        /// List of dice to be held or tossed, based on scoring options.
        /// </summary>
        static void UpdateFiltersMatrix ()
        {
            // Ones through sixes, plus straight = 7.
            FiltersMatrix = new bool [ 7 ] [];
            // Build a filterList for ones through sixes.
            bool _bool = false;
            for ( int _thisValue = 1; _thisValue < 7; _thisValue++ )
            {
                var _filter = new bool [ 5 ];
                for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
                {
                    _bool = ( DieStructs [ _thisDie ].FaceValue == _thisValue ) ? true : false;
                    _filter [ _thisDie ] = _bool;
                }
                FiltersMatrix [ _thisValue ] = _filter;
            }
            // End of  filterList loop by die values 1 thru 6.

            // Build a filterList for straights:  occurs in UpdateMaxStraight ().

            /* Let VimDice: 
            * trim ones or sixes from straight list, 
            * choose a filterList for three or more of a kind,
            * Build a filterList for full house. ( VimDice ),
            * Build a filterList for chance??  (?All sixes or all >3?). */
        }


        /// <summary>
        /// Used by GameScoring for three of a kind, full house checks.
        /// </summary>
        static void UpdatePairsList ()
        {
            // Multiples list, iterate through valueIndexedMultiples recording doubles or better.
            PairsList = new int [ 2 ] [];
            // For each die face value ( one through six ), check for two of a kind or better.
            for ( int _faceVal = 1; _faceVal < 7; _faceVal++ )
            {
                var _valueMultiple = new int [ 2 ];
                if ( ValueIndexedMultiples [ _faceVal ] >= 2 )
                {
                    _valueMultiple [ 0 ] = _faceVal;
                    _valueMultiple [ 1 ] = ValueIndexedMultiples [ _faceVal ];

                    // If the lower value is three of a kind, ADD the higher face value pair as the second entry.
                    if ( _valueMultiple [ 1 ] < PairsList [ 0 ] [ 1 ] )
                    {
                        PairsList [ 1 ] = _valueMultiple;
                    }
                    else
                    {
                        // Higher face values are more important than a pair of lower face values.
                        PairsList [ 1 ] = PairsList [ 0 ];
                        PairsList [ 0 ] = _valueMultiple;
                    }
                }
            }
        }


        static void UpdateMaxStraightAndStraightFilterList ()
        {
            int _previousDieValue = -1;
            var _dieFilter = new bool [ 5 ];
            int _maxStraight = 1;

            for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
            {
                var _die = DieStructs [ _thisDie ];
                if ( _die.FaceValue != _previousDieValue )
                {
                    // Add all non-recurring  dice to straight filter list.
                    _dieFilter [ _thisDie ] = true;
                    if ( _die.FaceValue == ( _previousDieValue + 1 ) )
                    {
                        // For each consecutive face value.
                        _maxStraight++;
                    }
                    else if ( _maxStraight < 4 )
                    {
                        // Because there was a gap in the sequence, and the current sequence was less than a small straight.
                        _maxStraight = 1;
                    }
                }
                else
                {
                    // Looking for sequential, not multiples.
                    _dieFilter [ _thisDie ] = false;
                }
                _previousDieValue = _die.FaceValue;
            }
            // The straight list is stored last.
            FiltersMatrix [ 6 ] = _dieFilter;
        }


        /// <summary>
        /// 
        /// </summary>
        static void UpdateSumAndValueIndexedMultiples ()
        {
            SumOfAllDice = 0;
            ValueIndexedMultiples = new int [ 7 ];
            FiltersMatrix = new bool [ 7 ] [];
            // Compute the sum of all dice, and populate the valueIndexedMultiples array.
            foreach ( var _die in DieStructs )
            {
                SumOfAllDice += _die.FaceValue;
                ValueIndexedMultiples [ _die.FaceValue ]++;
            }

        }







        // Struct

        public struct DieStruct
        {
            // Fields
            public bool Held;
            public int FaceValue;
            public int DieId;

            // Method

            // Moved to GameDice!
            //public void RollDie ()
            //{
            //    FaceValue = GameDice1.RollDie ();
            //}
        }

    }
}
