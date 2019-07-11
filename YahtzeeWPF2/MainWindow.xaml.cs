//using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
using System.Windows.Media;
//using System.Windows.Media.Animation;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

namespace YahtzeeWPF2
{

    public partial class MainWindow : Window
    {
        //TODO: Add modal options window, save user settings, high scores and user tendencies, change player names and colors..
        //TODO: Bind XAML Controls (?? to a single ObservableList<string/  text/ content/> public property??).
        //TODO:  Add ToolTips, OnEsc, Tab order,  OnClickAnywhere, Drag and Toss Dice flick, Touch screen/ Tablet
        #region Fields
        // Fields
        VisualCommitAsClass visCommit;

        // The scoresheet's buttons for each grid cell, for rows that can be selected by a player. [TakeScore rows ]
        List<Button> entryColumn;
        List<List<Button>> entries;

        // The scoresheet's textblocks for each grid cell.
        List<TextBlock> postColumn;
        public List<List<TextBlock>> posts;

        public List<List<TextBlock>> posts1;

        // List of five "dice" button lists.=> List of four buttons ( parent - container button holding the top, left and "right die" face buttons).
        List<List<Button>> visualDiceList;


        SolidColorBrush pointsBrush = new SolidColorBrush ( Color.FromArgb ( 255, 0, 255, 0 ) );
        SolidColorBrush player1Brush = new SolidColorBrush ( Color.FromArgb ( 255, 150, 0, 0 ) );

        Color colorPlayer1;
        Color colorPlayer2;
        Color colorPlayer3;

        Color colorOpen;
        Color colorScratch;
        Color colorPoints;
        Color colorFilled;
        Color colorInsist;
        #endregion Fields



        #region MainWindow Constructor
        // Constructor

        public MainWindow ()
        {
            InitializeComponent ();
            NameScope.SetNameScope ( this, new NameScope () );
            InitialColors ();
            InitializeScoresheetVisual ();
            InitializeDiceBox ();
            GameModel.NewGame ();
            UpdateDiceVisual ();
            UpdateTakeScoresVisual ();
            UpdateCommitVisual1 ();


            //KlugeBuildPosts1 ();
        }
        #endregion MainWindow Constructor



        #region Events
        // Events

        /// <summary>
        /// Moves the die across the hold line when clicked  Y/ top; X/ left doesn't change.
        /// FUTURE: X will change for group sliding.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Die_Click ( object sender, RoutedEventArgs e )
        {
            var visDie = ( Button ) sender;
            var name = $"{visDie.Name [ 3 ]}";
            int dieNum = int.Parse ( name );
            Point _topLeft = DiceBoxVM.DieWasClicked ( dieNum );
            visDie.Margin = new Thickness ( _topLeft.X, _topLeft.Y, 0, 0 );
        }


        //private void Commit_Click ( object sender, RoutedEventArgs e )
        //{
        //    GameModel.CommitClickedHandler ();
        //    if ( GameModel.CommitDetails.ResultsList.Count != 0 )
        //    {
        //        UpdateScoresheetEntriesVisual ();
        //    }
        //    UpdateDiceVisual ();
        //    UpdateTakeScoresVisual ();
        //    UpdateCommitVisual1 ();
        //    //UpdateCommitVisual ();
        //}


        private void Commit_Click ( object sender, RoutedEventArgs e )
        {
            VimModel.CommitWasClicked ();
            if ( GameModel.CommitDetails.ResultsList.Count != 0 )
            {
                UpdateScoresheetEntriesVisual ();
            }
            UpdateDiceVisual ();
            UpdateTakeScoresVisual ();
            UpdateCommitVisual1 ();
            //UpdateCommitVisual ();
        }


        private void NewGame_Click ( object sender, RoutedEventArgs e )
        {
            // Reset the visual, then call GameModel.NewGame.
        }


        private void Options_Click ( object sender, RoutedEventArgs e )
        {
            // Call a NYI dialog.
            // Perhaps a subwindow with menu bars for player names, colors, human/ AI controlled, optional rules, statistics.
        }


        private void TakeScore_Click ( object sender, RoutedEventArgs e )
        {
            Button _button = ( Button ) sender;
            GameModel.RowClickedHandler ( _button.Name );
            VimModel.RowClicked ( _button.Name );
            //UpdateCommitVisual ();
            UpdateCommitVisual1 ();
        }



        #endregion Events

        


        #region Methods
        //      Methods   

        void InitializeDiceBox ()
        {
            InitializeDiceVisual1 ();

            visCommit = new VisualCommitAsClass ();
            diceBox.Children.Add ( visCommit.CommitContainer );
            visCommit.CommitContainer.Click += Commit_Click;

        }


        public void NewGame ()
        {
            // Reset all of the Scoresheet items to "   ",
        }


        public void UpdateCommitVisual1 ()
        {
            SolidColorBrush _playerBrush = new SolidColorBrush ()
            {
                Color = Color.FromArgb ( 255, 255, 0, 0 ),
            };
            visCommit.PlayerColor = _playerBrush;
            visCommit.PlayerName = GameModel.CommitDetails.PlayerName;
            visCommit.Action = GameModel.CommitDetails.Action;
            visCommit.Description = GameModel.CommitDetails.Description;
        }



        /// <summary>
        /// Updates dice that weren't held, and TakeScore displays.
        /// </summary>
        public void UpdateDiceVisual ()
        {
            for ( int _thisDie = 0; _thisDie < 5; _thisDie++ )
            {
                var _visDie = visualDiceList [ _thisDie ];
                var _vimDie = DiceBoxVM.VimDice [ _thisDie ];
                _visDie [ 0 ].Margin = new Thickness ( _vimDie.Left, _vimDie.Top, 0, 0 );
                _visDie [ 1 ].Content = _vimDie.FaceValue;
            }
        }


        /// <summary>
        /// Updates the entry of the score taken and the column totals. 
        /// </summary>
        void UpdateScoresheetEntriesVisual ()
        {
            
            List<int []> _results = GameModel.CommitDetails.ResultsList;
            int [] _result = _results [ 0 ];
            int _col = _result [ 0 ];
            for ( int i = 1; i < _results.Count; i++ )
            {
                _result = _results [ i ];
                int _row = _result [ 0 ];
                // If _row is 5OK.
                if ( ( _row >= 16 ) && ( _row <= 19 ) )
                {
                    posts [ _col ] [ _row ].Text = ( _result [ 1 ] == 0 ) ? "X" : "V";
                }
                else
                    posts [ _col ] [ _row ].Text = _result [ 1 ].ToString ();
            }
        }


        void UpdateScoresheetEntriesVisual1 ()
        {

        }




        /// <summary>
        ///  Update the TakeScore buttons.
        /// </summary>
        void UpdateTakeScoresVisual ()
        {
            entryColumn = new List<Button> ();
            entryColumn = entries [ 5 ];
            postColumn = new List<TextBlock> ();
            postColumn = posts [ 5 ];
            int _postRow = 0;
            GameScoring.GameRow gamerow;
            TextBlock textBlock;
            Button button;


            for ( int _row = 0; _row < 13; _row++ )
            {
                gamerow = new GameScoring.GameRow ();
                gamerow = GameScoring.GameRows [ _row ];

                button = new Button ();
                button = entryColumn [ _row ];
                button.Visibility = ( gamerow.TakeScoreVisible ) ? Visibility.Visible : Visibility.Hidden;
                if ( gamerow.RowHighlight == HighlightStyle.Scratch )
                {
                    button.Background = Brushes.LightPink;
                }
                else
                    button.Background = ( _row % 2 == 0 ) ? Brushes.AliceBlue : Brushes.Gainsboro;

                _postRow = ( _row < 6 ) ? _row + 1 : _row + 4;
                textBlock = new TextBlock ();
                textBlock = postColumn [ _postRow ];
                textBlock.Text = gamerow.TakeScoreString;
            }
            UpdatePlayerHighlight ();
        }
        // End  UpdateTakeScoresVisual


       /// <summary>
       /// Logic should be moved to Vim!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
       /// </summary>
        void UpdatePlayerHighlight ()
        {
            int _player = GameModel.GameClock.PlayerUp;
            //player1HighlightRect.Fill = Brushes.Transparent;
            //player2HighlightRect.Fill = Brushes.Transparent;
            //player3HighlightRect.Fill = Brushes.Transparent;
            player1HighlightRect.Fill = Brushes.Gray;
            player2HighlightRect.Fill = Brushes.Gray;
            player3HighlightRect.Fill = Brushes.Gray;
            if ( _player == 1 )
                player1HighlightRect.Fill = Brushes.Goldenrod;
            else if ( _player == 2 )
                player2HighlightRect.Fill = Brushes.Goldenrod;
            else
                player3HighlightRect.Fill = Brushes.Gold;
        }


        void InitialColors ()
        {
            colorPlayer1 = Color.FromRgb ( 80, 0, 0 );
            colorPlayer2 = Color.FromRgb ( 0, 140, 0 );
            colorPlayer3 = Color.FromRgb ( 0, 0, 110 );

            colorFilled = Color.FromRgb ( 80, 80, 80 );
            colorOpen = Color.FromRgb ( 120, 120, 120 );
            colorScratch = Color.FromRgb ( 100, 20, 20 );
             colorPoints = Color.FromRgb ( 200, 180, 20 );
            colorInsist = Color.FromRgb ( 220, 200, 40 );
        }

        void InitializeDiceVisual1 ()
        {
            var builder = new DiceBoxBuilder ();
            visualDiceList = builder.DiceList;
            foreach ( var die in visualDiceList )
            {
                diceBox.Children.Add ( die [ 0 ] );
                die [ 0 ].Click += Die_Click;
            }
        }




        void InitializeScoresheetVisual ()
        {
            // Using new to avoid potential conflicts.
            entries = new List<List<Button>> ();
            postColumn = new List<TextBlock> ();
            posts = new List<List<TextBlock>> ();

            var build = new ScoresheetBuilderInstance ();
            List<List<FrameworkElement>> _elementColumns = build.ScoresheetElements;

            entries = build.ScoresheetButtons;
            posts = build.ScoresheetTextBlocks;

            foreach ( var _buttonColumn in entries )
            {
                foreach ( var _button in _buttonColumn )
                {
                    //TODO:  Do I need to use a weak reference for buttons where Visibility can be false?????????????????????????????????????????????????????????????????????????????????????
                    _button.Click += TakeScore_Click;
                }
            }

            for ( int _column = 0; _column < 6; _column++ )
            {
                for ( int _row = 0; _row < 20; _row++ )
                {
                    var _element = new FrameworkElement ();
                    _element = _elementColumns [ _column ] [ _row ];
                    Scoresheet.Children.Add ( _element );
                    Grid.SetColumn ( _element, _column );
                    Grid.SetRow ( _element, _row );
                }
            }
        }


        /// <summary>
        /// Does this change Posts because of reference??????????????????????????????????????????????????????????????????????????????????????????
        /// </summary>
        //void KlugeBuildPosts1 ()
        //{
        //    posts1 = new List<List<TextBlock>> ();
        //    for ( int _col = 2; _col < 5; _col++ )
        //    {
        //        var column = new List<TextBlock> ();
        //        column =    posts [ _col ];
        //        column.RemoveAt ( 9 );
        //        column.RemoveAt ( 0 );
        //        posts1.Add ( column );
        //        if ( posts [0].Count != posts [_col].Count )
        //        {
        //            throw new System.Exception ( "ERROR:  Its a reference thing; add the items I want.  Or............" );
        //        }
        //    }
        //}

        #endregion MainWindow methods


    }
}

