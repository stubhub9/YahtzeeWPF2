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
    public class GameDie1
    {
        // Fields
        //int faceValue = 0;
        //static Random randomDieValue = new Random ();

        // Constructor
        public GameDie1 ()
        {

        }

        // Properties
        public int FaceValue
        {
            get; set;
            //get => faceValue;
        }

        public bool Held { get; set; } = false;

        // Sorted ID
        public int Ordinal { get; set; }


        // Method   

        /// <summary>
        /// Updates to the next random dice face value
        /// </summary>
        //public void RollDie ()
        //{
        //    faceValue = randomDieValue.Next ( 1, 7 );
        //    if ( GameModel.GameClock.GameRound == 17 )
        //        faceValue = 0;
        //    //faceValue = 6;
        //    //if ( GameModel.GameClock.GameRound > 1 )
        //    //{
        //    //    faceValue = 5;
        //    //}
        //}


    }
}
