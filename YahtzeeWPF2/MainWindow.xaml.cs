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

        Color colorPlayerUp;
        Color colorPlayer1;
        Color colorPlayer2;
        Color colorPlayer3;

        Color colorOpen;
        Color colorScratch;
        Color colorPoints;
        Color colorFilled;
        Color colorInsist;


        Color rowScratch = Colors.LightPink;
        Color rowOdd = Colors.AliceBlue;
        Color rowEven = Colors.Gainsboro;
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


            //GameModel.NewGame ();
            VimModel.NewGame ();

            UpdateDiceVisual ();
            UpdateTakeScoresVisual1 ();
            //UpdateTakeScoresVisual ();
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
            var name = $"{visDie.Name [ 3 ]}";
            int dieNum = int.Parse ( name );
            Point _topLeft = DiceBoxVM.DieWasClicked ( dieNum );
            visDie.Margin = new Thickness ( _topLeft.X, _topLeft.Y, 0, 0 );
        }


        private void Commit_Click ( object sender, RoutedEventArgs e )
        {

            VimModel.CommitWasClicked ();

            // If a score was taken, then update the scoresheet.
            if ( VimModel.CommitDetails.VisScoresheetResults.Count != 0 )
            {
                UpdateScoresheetEntriesVisual1 ();
                //UpdateScoresheetEntriesVisual ();
            }

            UpdateDiceVisual ();

            UpdateTakeScoresVisual1 ();
            //UpdateTakeScoresVisual ();

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

        // Handles clicks on the scoresheet (entry) rows.
        private void TakeScore_Click ( object sender, RoutedEventArgs e )
        {
            Button _button = ( Button ) sender;
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
            Player playerUp = VimModel.CommitDetails.PlayerUp;
            switch ( playerUp )
            {
                case Player.PlayerOne:
                    colorPlayerUp = colorPlayer1;
                    break;
                case Player.PlayerTwo:
                    colorPlayerUp = colorPlayer2;
                    break;
                default:
                    colorPlayerUp = colorPlayer3;
                    break;
            }

            SolidColorBrush _playerBrush = new SolidColorBrush ()
            {
                Color = colorPlayerUp,
            };

            visCommit.PlayerColor = _playerBrush;
            //visCommit.PlayerName = GameModel.CommitDetails.PlayerName;
            visCommit.PlayerName = VimModel.CommitDetails.PlayerName;
            //visCommit.Action = GameModel.CommitDetails.Action;
            visCommit.Action = VimModel.CommitDetails.Action;
            //visCommit.Description = GameModel.CommitDetails.Description;
            visCommit.Description = VimModel.CommitDetails.Description;
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

        

        void UpdateScoresheetEntriesVisual1 ()
        {
            var _results = VimModel.CommitDetails.VisScoresheetResults;
            foreach ( var item in _results )
            {
                posts [ ( int ) item.Column ] [ ( int ) item.Row ].Text = item.Value;
            }


        }


        void UpdateTakeScoresVisual1 ()
        {
            var _gameRows = VimModel.CommitDetails.VisGameRows;
            var _column = VisColumn.TakeScore;
            // Get a list of takescore buttons
            List<Button> _buttons = entries [ ( int ) _column ];
            // Get a list of takescore textblocks.
            List<TextBlock> _textBlocks = posts [ ( int ) _column ];

            //  VisGameRow    HighlightStyle Highlight; Text;  VisRow VisRow;
            for ( int i = 0; i < _gameRows.Count; i++ )
            {
                var _gameRow = _gameRows [ i ];
                var _button = _buttons [ i ];
                var _textBlock = _textBlocks [ ( int ) _gameRow.VisRow ];
                _textBlock.Text = _gameRow.Text;
                _button.Visibility = Visibility.Visible;

                switch ( _gameRow.Highlight )
                {
                    case HighlightStyle.Filled:
                        _button.Visibility = Visibility.Hidden;

                        break;

                    case HighlightStyle.Open:

                        _button.Visibility = Visibility.Hidden;
                        break;

                    case HighlightStyle.Scratch:
                        _button.Background = new SolidColorBrush ( colorScratch );

                        break;

                    case HighlightStyle.Points:
                        _button.Background = new SolidColorBrush ( colorPoints );

                        break;

                    case HighlightStyle.Insist:
                        _button.Background = new SolidColorBrush ( colorInsist );

                        break;


                }
            }



            //TODO:     Should these be declared here or as fields;     OR AT ALL (just fix the old method!)???????????????????????????????????????
            //Color rowScratch = Colors.LightPink;
            //Color rowOdd = Colors.AliceBlue;
            //Color rowEven = Colors.Gainsboro;
        }


        /// <summary>
        /// Logic should be moved to Vim!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        void UpdatePlayerHighlight ()
        {
            int _player = (int) VimModel.CommitDetails.PlayerUp;
            //player1HighlightRect.Fill = Brushes.Transparent;
            //player2HighlightRect.Fill = Brushes.Transparent;
            //player3HighlightRect.Fill = Brushes.Transparent;

            // Set last player's column to gray, by setting all columns to gray-------------------------------- ?Add a last player id to VimModel?
            player1HighlightRect.Fill = Brushes.Gray;
            player2HighlightRect.Fill = Brushes.Gray;
            player3HighlightRect.Fill = Brushes.Gray;
            if ( _player == 0 )
                player1HighlightRect.Fill = Brushes.Goldenrod;
            else if ( _player == 1 )
                player2HighlightRect.Fill = Brushes.Goldenrod;
            else
                player3HighlightRect.Fill = Brushes.Goldenrod;
        }


        void InitialColors ()
        {
            colorPlayer1 = Color.FromRgb ( 250, 0, 0 );
            colorPlayer2 = Color.FromRgb ( 0, 140, 60 );
            colorPlayer3 = Color.FromRgb ( 0, 0, 200 );

            colorFilled = Color.FromRgb ( 80, 80, 80 );
            colorOpen = Color.FromRgb ( 120, 120, 120 );
            colorScratch = Color.FromRgb ( 220, 20, 20 );
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

        

        #endregion MainWindow methods


    }
}

