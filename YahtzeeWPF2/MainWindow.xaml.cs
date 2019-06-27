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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        // List of five "dice" button lists.=> List of four buttons ( parent - container button holding the top, left and "right die" face buttons).
        List<List<Button>> visualDiceList;


        SolidColorBrush pointsBrush = new SolidColorBrush ( Color.FromArgb ( 255, 0, 255, 0 ) );
        SolidColorBrush player1Brush = new SolidColorBrush ( Color.FromArgb ( 255, 150, 0, 0 ) );
        //LinearGradientBrush linearGradientBrush;
        #endregion Fields



        #region MainWindow Constructor
        // Constructor

        public MainWindow ()
        {
            InitializeComponent ();
            NameScope.SetNameScope ( this, new NameScope () );
            InitializeScoresheetVisual ();
            InitializeDiceBox ();
            GameModel.NewGame ();
            UpdateDiceVisual ();
            UpdateTakeScoresVisual ();
            UpdateCommitVisual1 ();
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
            string name = $"{visDie.Name [ 3 ]}";
            int dieNum = int.Parse ( name );
            //BEGIN:  GameDice1 kluge!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Point _topLeft = VimDice.DieWasClicked ( dieNum );



            
            Die _die = GameDice.DieList [ dieNum ];
            double x = 60 + ( dieNum * 130 );
            double y1 = 365;
            double y2 = 550;

            if ( _die.Held )
            {
                visDie.Margin = new Thickness ( x, y1, 0.0, 0.0 );
            }
            else
            {
                visDie.Margin = new Thickness ( x, y2, 0.0, 0.0 );
            }
            _die.Held = !_die.Held;

        }
        // End of Die_Click


        private void Commit_Click ( object sender, RoutedEventArgs e )
        {
            GameModel.CommitClickedHandler ();
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
            //visCommit.PlayerNameTxtBlk.Background = _playerBrush;
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
            Die _die;
            Button _visDie;
            double y1 = 365;
            double y2 = 550;
            double x;
            for ( int dieNum = 0; dieNum < 5; dieNum++ )
            {
                _die = new Die ();
                _die = GameDice.DieList [ dieNum ];
                _visDie = new Button ();
                _visDie = visualDiceList [ dieNum ] [ 1 ];
                _visDie.Content = _die.FaceValue;
                x = 60 + ( dieNum * 130 );

                visualDiceList [ dieNum ] [ 0 ].Margin = ( _die.Held ) ? new Thickness ( x, y2, 0.0, 0.0 ) : new Thickness ( x, y1, 0.0, 0.0 );
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
            //GameStatus.GameRow gamerow;
            TextBlock textBlock;
            Button button;


            for ( int _row = 0; _row < 13; _row++ )
            {
                gamerow = new GameScoring.GameRow ();
                gamerow = GameScoring.GameRows [ _row ];
                //gamerow = new GameStatus.GameRow ();
                //gamerow = GameStatus.GameRows [ _row ];

                button = new Button ();
                button = entryColumn [ _row ];
                button.Visibility = ( gamerow.TakeScoreVisible ) ? Visibility.Visible : Visibility.Hidden;
                if ( gamerow.RowHighlight == GameScoring.GameRow.HighlightStyle.Scratch )
                //if ( gamerow.RowHighlight == GameStatus.GameRow.HighlightStyle.Scratch )
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



        void UpdatePlayerHighlight ()
        {
            int _player = GameModel.GameClock.PlayerUp;
            player1HighlightRect.Fill = Brushes.Transparent;
            player2HighlightRect.Fill = Brushes.Transparent;
            player3HighlightRect.Fill = Brushes.Transparent;
            if ( _player == 1 )
                player1HighlightRect.Fill = Brushes.Goldenrod;
            else if ( _player == 2 )
                player2HighlightRect.Fill = Brushes.Goldenrod;
            else
                player3HighlightRect.Fill = Brushes.Goldenrod;
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

        #endregion MainWindow methods


    }
}

