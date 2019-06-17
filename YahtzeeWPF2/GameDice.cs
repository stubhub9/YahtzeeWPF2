using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{
    public static class GameDice
    {
        #region Fields
        // Fields
        static List<Die> dice;
        static int [] faceValues;
        static List<bool> filterList;
        static List<List<bool>> filtersList;
        static int maxStraight;
        static List<int []> multiplesList;
        static int sum;
        static int [] valueIndexedMultiples;
        #endregion Fields

        #region Constructor
        // Constructor
        static GameDice ()
        {
            faceValues = new int [ 5 ];

            valueIndexedMultiples = new int [ 7 ];
            Die die;
            //TODO:  override the dice( list <T>) Sort method; maybe the Sum??
            dice = new List<Die> ();
            while ( dice.Count < 5 )
            {
                die = new Die ();
                dice.Add ( die );
            }

            SortDice ();
        }
        //      End of Constructor.
        #endregion Constructor

        #region Properties and Indexers
        // Properties and Indexers   
        public static int [] ValueIndexedMultiples
        {
            get { return valueIndexedMultiples; }
            set { }
        }

        public static int  MaxStraight
        {
            get { return maxStraight; }
            set { }
        }

        public static List < Die > DieList
        {
            get { return dice;  }
            set { }
        }

        public static List < List < bool >> FiltersList
        {
            get { return filtersList; }
            set { }
        }

        public static List<int []> MultiplesList
        {
            get { return multiplesList; }
            set { }
        }

        public static int Sum
        {
            get => sum;
        }
        // End of Properties
        #endregion Properties

        #region Methods
        // Methods

        public static void NewDice ()
        {
            foreach ( var item in dice )
            {
                item.Held = false;
                item.RollDie ();
            }
            SortDice ();
        }


        public static void RollDice ()
        {
            Die _die = new Die ();

            var subset = from i in dice where i.Held == false select i;
            foreach ( var item in subset )
            {
                item.RollDie ();
            }
            SortDice ();
        }


        static void SortDice ()
        {
            filterList = new List < bool > ();
            filtersList = new List < List <bool> >();
            multiplesList = new List<int []> ();
            valueIndexedMultiples = new int [ 7 ];
            maxStraight = 0;
            sum = 0;

            Die _die;
            int _max = 4; 
            bool _unsorted = true;
            int [] _valMult = new int [ 2 ];

            // Sort the dice by face value.
            while ( _unsorted )
            {
                _unsorted = false;
                for ( int i = 0; i < _max; i++ )
                {
                    if ( dice [ i ].FaceValue > dice [ i + 1 ].FaceValue )
                    {
                        _unsorted = true;
                        _die = new Die ();
                        _die = dice [ i ];
                        dice [ i ] = dice [ i + 1 ];
                        dice [ i + 1 ] = _die;
                    }
                }
                _max--;
            }
            // End of sort by face value.

            // Compute the sum of all dice, and populate the valueIndexedMultiples array.
            foreach ( var die in dice )
            {
                sum += die.FaceValue;
                valueIndexedMultiples [ die.FaceValue ]++;
            }

            // Build a filterList for ones through sixes.
            bool _bool = false;
            for ( int _dieVal = 1; _dieVal < 7; _dieVal++ )
            {
                filterList = new List<bool> ();
                foreach ( var die in dice )
                {
                    _bool = ( die.FaceValue == _dieVal ) ? true : false;
                    filterList .Add ( _bool );
                }
                filtersList.Add ( filterList );
            }
            // End of  filterList loop by die values 1 thru 6.

            // Straight filter list.
            int _previousDieValue = -1;
            filterList = new List<bool> ();
            maxStraight = 1;

            foreach ( var die in dice )
            {
                if ( die.FaceValue != _previousDieValue )
                {
                    // Add all non-recurring  dice to straight filter list.
                    filterList.Add ( true );
                    if ( die.FaceValue == _previousDieValue + 1 )
                    {
                        // For each consecutive face value.
                        maxStraight++;
                    }
                    else if ( maxStraight < 4 )
                    {
                        // Because there was a gap in the sequence, and the current sequence was less than a small straight.
                        maxStraight = 1;
                    }
                }
                else
                {
                    // Looking for sequential, not multiples.
                    filterList.Add ( false );
                }
                 _previousDieValue = die.FaceValue;
            }
            filtersList.Add ( filterList );

            // Multiples list, iterate through valueIndexedMultiples seeking doubles or better.
            for ( int _faceVal = 1; _faceVal < 7; _faceVal++ )
            {
                _valMult = new int [ 2 ];
                if ( valueIndexedMultiples [ _faceVal ] >= 2 )
                {
                    _valMult [ 0 ] = _faceVal;
                    _valMult [ 1 ] = valueIndexedMultiples [ _faceVal ];


                    if ( _valMult [ 1 ] >= 3 )
                    {
                        multiplesList.Insert ( 0, _valMult );
                    }
                    else
                    {
                        multiplesList.Add ( _valMult );
                    }
                }

            }
            // End of multiplesList.
        }
        // End of SortDice method.
        #endregion GameDice Methods
    }
    // End  class GameDice



    //  Helper Class

    /// <summary>
    /// Provides base properties and method for each die.
    /// </summary>
    public class Die
    {
        // Fields
        int faceValue = 0;
        static Random randomDieValue = new Random ();

        // Constructor.
        public Die ()
        {
            Held = false;
            //Highlight = 0;
            RollDie ();
        }


        // Properties
        public int FaceValue
        {
            get => faceValue;
            set { }
        }
        public bool Held { get; set; } = false;

        // Method.     

        /// <summary>
        /// Updates to the next random dice face value
        /// </summary>
        public void RollDie ()
        {
            faceValue = randomDieValue.Next ( 1, 7 );
            //faceValue = 6;
        }
    }
    //     End of Die Class
}

