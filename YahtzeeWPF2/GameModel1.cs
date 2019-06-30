using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YahtzeeWPF2
{

    enum Player
    { One, Two, Three }

    enum Roll
    {  One, Two, Three }

    enum Row
    {
        Ones, Twos, Threes, Fours, Fives, Sixes,
        Score63, ScoreUpper,
        ThreeX, FourX, FullHouse, FourStr, FiveStr, Chance, FiveX,
        ScoreFiveX, ScoreLower, ScoreTotal
    }

    static class GameModel1
    {
        // Fields
        

        // Properties

            static int DiceRoll
        {
            get => GameClock.diceRoll;
        }
        

        static Player PlayerUp
        {
            get => GameClock.player;
        }


        static int GameRound
        {
            get => GameClock.gameRound;
        }







        private static class GameClock
        {
            public static int gameRound;
            public static int diceRoll;
            public static Player player;

            public static int GameRound
            {
                get => gameRound;
            }


            static void GameOver ()
            {
                //NYI
            }


            static void MustTakeScore ()
            {
                //NYI
            }


            static void NewGame ()
            {
                gameRound = 1;
                diceRoll = 1;
                player = Player.One;
            }


            static void NextPlayer ()
            {
                diceRoll = 1;
                if ( player == Player.Three )
                {
                    player = Player.One;
                    gameRound++;

                    if ( gameRound > 17 )
                        throw new Exception ( $"GameRound {gameRound} is greater than max of 17." );

                    //TODO:  Perform this check elsewhere??????????????????????????????????????????????????????????????????????????????????????????????????????????
                    if ( gameRound == 17 )
                        GameOver ();
                }
                else
                    player = ( Player ) ( ( ( int ) player ) + 1 );
            }


            static void NextDiceRoll ()
            {
                diceRoll++;

                if ( diceRoll > 3 )
                {
                    throw new Exception ( $"DiceRoll = {diceRoll} is greater than max of 3." );
                }

                //TODO:  Perform this check elsewhere????????????????????????????????????????????????????????????????????????????????????????????????????????????
                if ( diceRoll == 3  )
                {
                    MustTakeScore ();
                }
            }

        }


        private static class GameScores
        {

            static int? [,] scoreTable;
        }



    }
}
