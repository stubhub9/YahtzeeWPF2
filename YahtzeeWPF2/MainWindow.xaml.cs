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

        // The commit button's textblock.
        List<TextBlock> commitTextBlocks;
        

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
            InitializeDiceVisual ();
            GameModel.NewGame ();
            UpdateDiceVisual ();
            UpdateTakeScoresVisual ();
            UpdateCommitVisual ();
        }
        #endregion MainWindow Constructor



        #region Events
        // Events

        private void Die_Click ( object sender, RoutedEventArgs e )
        {
            Button visDie = ( Button ) sender;

            string name = $"{visDie.Name [ 3 ]}";
            int dieNum = int.Parse ( name );

            Duration duration = new Duration ( TimeSpan.FromMilliseconds ( 750 ) );
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
            UpdateCommitVisual ();
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
            UpdateCommitVisual ();
        }
        #endregion Events



        #region Methods
        //      Methods   

        public void NewGame ()
        {
            // Reset all of the Scoresheet items to "   ",
        }


        public void UpdateCommitVisual ()
        {
            SolidColorBrush _playerBrush = new SolidColorBrush ()
            {
                Color = Color.FromArgb ( 255, 255, 0, 0 ),
                //Color = GameModel.CommitDetails.PlayerColor,
            };
            commitTextBlocks [ 0 ].Background = _playerBrush;

            commitTextBlocks [ 0 ].Text = GameModel.CommitDetails.PlayerName;
            commitTextBlocks [ 1 ].Text = GameModel.CommitDetails.Action;
            commitTextBlocks [ 2 ].Text = GameModel.CommitDetails.Description;
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
                if (( _row >= 16 ) && ( _row <= 19 ))
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

        
        void InitializeDiceVisual ()
        {
            Button _playerCommit;
            // Create a die, parent is the container for a canvas containing three button faces.
            Button _parent;
            Button _topFace;
            Button _leftFace;
            Button _rightFace;

            Canvas _canvas;
            Double _dblNum;
            List<Button> _buttons;

            visualDiceList = new List<List<Button>> ();
            // Create the three faces for each die.
            TransformGroup topGroup = new TransformGroup ();
            topGroup.Children.Add ( new RotateTransform ( 45.0 ) );
            topGroup.Children.Add ( new ScaleTransform ( 1.45, 1.45 ) );
            topGroup.Children.Add ( new SkewTransform ( 0, 0 ) );

            TransformGroup leftGroup = new TransformGroup ();
            leftGroup.Children.Add ( new RotateTransform ( 100.0 ) );
            leftGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
            leftGroup.Children.Add ( new SkewTransform ( 10.0, 40.0 ) );

            TransformGroup rightGroup = new TransformGroup ();
            rightGroup.Children.Add ( new RotateTransform ( -101.0 ) );
            rightGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
            rightGroup.Children.Add ( new SkewTransform ( -11.0, -39.0 ) );

            for ( int i = 0; i < 5; i++ )
            {
                _dblNum = 60.0 + ( i * 130 );
                //x= 60 y= 365

                _parent = new Button
                {
                    Name = $"die{ i }face0",
                    Margin = new Thickness ( _dblNum, 365, 0, 0 ),
                    Height = 155,
                    Width = 105,

                    Background = Brushes.Transparent,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness ( 2 ),
                };
                _parent.Click += Die_Click;

                _topFace = new Button
                {
                    Name = $"die{ i }face1",
                    BorderThickness = new Thickness ( 5 ),
                    BorderBrush = Brushes.LightGreen,
                    Margin = new Thickness ( 48, 1, 0, 0 ),
                    Height = 50,
                    Width = 50,
                    Background = Brushes.LightGoldenrodYellow,
                    FontSize = 22,
                    FontWeight = FontWeights.ExtraBold,
                    RenderTransform = topGroup
                };

                _leftFace = new Button
                {
                    Name = $"die{ i }face2",
                    BorderThickness = new Thickness ( 2 ),
                    BorderBrush = Brushes.LightGreen,
                    Margin = new Thickness ( 47, 102, 0, 0 ),
                    Height = 50,
                    Width = 50,
                    Background = Brushes.DarkGray,
                    FontSize = 22,
                    FontWeight = FontWeights.Medium,
                    RenderTransform = leftGroup
                };

                _rightFace = new Button
                {
                    Name = $"die{ i }face2",
                    BorderThickness = new Thickness ( 2 ),
                    BorderBrush = Brushes.LightGreen,
                    Margin = new Thickness ( 47, 144, 0, 0 ),
                    Height = 50,
                    Width = 50,
                    Background = Brushes.DarkGray,
                    FontSize = 22,
                    FontWeight = FontWeights.Medium,
                    RenderTransform = rightGroup
                };

                _canvas = new Canvas
                {
                    Background = Brushes.Transparent,
                    Height = 148,
                    Width = 97,
                };
                _canvas.Children.Add ( _topFace );
                _canvas.Children.Add ( _leftFace );
                _canvas.Children.Add ( _rightFace );
                _parent.Content = _canvas;
                diceBox.Children.Add ( _parent );

                _buttons = new List<Button> ();
                _buttons.Add ( _parent );
                _buttons.Add ( _topFace );
                _buttons.Add ( _leftFace );
                _buttons.Add ( _rightFace );
                visualDiceList.Add ( _buttons );
            }
            // End of Dice creation loop.

            _playerCommit = new Button
            {
                Background = player1Brush,
                Margin = new Thickness ( 10, 10, 0, 0 ),
                Height = 150,
                Width = 730,
                BorderThickness = new Thickness ( 2 ),
                BorderBrush = Brushes.Black,
                FontSize = 22,
                FontWeight = FontWeights.Medium,
            };
            _playerCommit.Click += Commit_Click;
            StackPanel _stackPanel = new StackPanel ();
            commitTextBlocks = new List<TextBlock> ();
            TextBlock _commitTextBlock;
            for ( int i = 1; i < 6; i++ )
            {
                _commitTextBlock = new TextBlock ()
                {
                    Text = " ",
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                _stackPanel.Children.Add ( _commitTextBlock );
                //if (( i % 2 ) != 0  )
                if ( ( i == 1 ) || ( i == 3 ) || ( i == 4 ) )
                {
                    commitTextBlocks.Add ( _commitTextBlock );
                }
                if ( i == 3 )
                {
                    _commitTextBlock.FontSize = 28;
                    _commitTextBlock.FontWeight = FontWeights.Bold;
                }
            }
            _playerCommit.Content = _stackPanel;
            diceBox.Children.Add ( _playerCommit );

            // Need to add _Click for each die (? after they have been added to the visual?).
            //_parent.Click += Die_Click;
        }
        // End of InitializeDiceVisual method.



        void InitializeScoresheetVisual ()
        {
            entries = new List<List<Button>> ();
            postColumn = new List<TextBlock> ();
            posts = new List<List<TextBlock>> ();

            FrameworkElement _element;
            List<List<FrameworkElement>> _elementColumns = new List<List<FrameworkElement>> ();

            ScoresheetBuilderStatic.InitializeScoreSheetVisual2 ( ref _elementColumns, ref posts, ref entries );
            

            foreach ( var _buttonColumn in entries )
            {
                foreach ( var _button in _buttonColumn )
                {
                     _button.Click += TakeScore_Click;
                }
            }

            for ( int _column = 0; _column < 6; _column++ )
            {
                for ( int _row = 0; _row < 20; _row++ )
                {
                    _element = new FrameworkElement ();
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

