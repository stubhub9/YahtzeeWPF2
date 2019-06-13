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


        string [] commitStrings;
        string [] headerLabels;
        string [] playerNames;
        string [] scoringStrings;

        SolidColorBrush pointsBrush = new SolidColorBrush ( Color.FromArgb ( 255, 0, 255, 0 ) );
        SolidColorBrush player1Brush = new SolidColorBrush ( Color.FromArgb ( 255, 150, 0, 0 ) );
        LinearGradientBrush linearGradientBrush;
        #endregion Fields



        #region MainWindow Constructor
        // Initialize Main Window     *****      Initialize Main Window     *****      Initialize Main Window     *****      Initialize Main Window     *****      Initialize Main Window     *****     
        public MainWindow ()
        {
            InitializeComponent ();
            NameScope.SetNameScope ( this, new NameScope () );
            Init1SetFields ();
            InitializeScoreSheetVisual ();
            InitializeDiceVisual ();
            // StartNewGame 
            GameModel.NewGame ();
            UpdateDiceVisual ();
            UpdateTakeScoresVisual ();
            UpdateCommitVisual ();
        }
        #endregion MainWindow Constructor



        #region Events
        //      Events      ******            Events      ******            Events      ******      

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
                UpdateScoresheetVisual ();
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
            int _clickedRow = ConvertNameToRow ( _button.Name );
            GameModel.RowClickedHandler ( _clickedRow );
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

        void UpdateScoresheetVisual ()
        {
            List<int []> _results = GameModel.CommitDetails.ResultsList;
            int [] _result = _results [ 0 ];
            int _col = _result [ 0 ];
            for ( int i = 1; i < _results.Count; i++ )
            {
                _result = _results [ i ];
                posts [ _col ] [ _result [ 0 ] ].Text = _result [ 1 ].ToString ();
            }
        }

        /// <summary>
        ///  Display the TakeScore info from GameStatus
        /// </summary>
        void UpdateTakeScoresVisual ()
        {
            entryColumn = new List<Button> ();
            entryColumn = entries [ 5 ];
            postColumn = new List<TextBlock> ();
            postColumn = posts [ 5 ];
            int _postRow = 0;
            GameStatus.GameRow gamerow;
            TextBlock textBlock;
            Button button;


            for ( int _row = 0; _row < 13; _row++ )
            {
                gamerow = new GameStatus.GameRow ();
                gamerow = GameStatus.GameRows [ _row ];

                button = new Button ();
                button = entryColumn [ _row ];
                button.Visibility = ( gamerow.TakeScoreVisible ) ? Visibility.Visible : Visibility.Hidden;

                _postRow = ( _row < 6 ) ? _row + 1 : _row + 4;
                textBlock = new TextBlock ();
                textBlock = postColumn [ _postRow ];
                textBlock.Text = gamerow.TakeScoreString;
            }
        }
        // End  UpdateTakeScoresVisual

        /// <summary>
        /// Commits player score, Updates counters for next turn(?), round and player and calls for new dice. 
        /// </summary>
        public void NextPlayer ()
        {

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

                //this.RegisterName ( _parent.Name, _parent );
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
        }
        // End of InitializeDiceVisual method.

        // Standard, three players on one scoresheet.
        void InitializeScoreSheetVisual ()
        {
            Border _border;
            Button _entry;
            //  Except for LowerTable label. 
            TextBlock _post;
            string _name;
            string _text;
            double _width;

            // The buttons will coordinate by row  for highlight animations, enabled, 
            entryColumn = new List<Button> ();
            entries = new List<List<Button>> ();
            postColumn = new List<TextBlock> ();
            posts = new List<List<TextBlock>> ();

            // Two row header columns,  plus three player columns, plus Take Score Buttons.
            for ( int _col = 0; _col < 6; _col++ )
            {
                entryColumn = new List<Button> ();

                for ( int _row = 0; _row < 20; _row++ )
                {
                    _width = ( ( _col >= 2 ) && ( _col <= 4 ) ) ? 40.0 : 150.0;

                    _text = GetHeaderString ( _col, _row );


                    _name = $"postC{_col}R{_row}";
                    // Create a TextBlock for every cell.
                    _post = new TextBlock ()
                    {
                        Name = _name,
                        TextAlignment = TextAlignment.Center,
                        /* FontFamily = new FontFamily ( "Ebrima" ),*/
                        FontWeight = FontWeights.Bold,
                        FontSize = ( _row != 9 ) ? 18.0 : 16.0,
                        Text = _text,
                        /*Height = 25,
                        Width = _width,*/
                        Padding = new Thickness ( 3 ),
                        VerticalAlignment = VerticalAlignment.Stretch,
                        /*TextWrapping = TextWrapping.WrapWithOverflow*/
                    };


                    _post.Background = Brushes.Transparent;
                    postColumn.Add ( _post );

                    // For Post rows:  Add TextBlock with Border  to grid cell.
                    if ( ( _row == 0 ) || ( ( _row >= 7 ) && ( _row <= 9 ) ) || ( _row >= 17 ) )
                    {
                        _border = new Border ()
                        {
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness ( 2 ),
                            Child = _post,
                        };
                        if ( _col == 4 )
                            _border.BorderThickness = new Thickness ( 2, 2, 4, 2 );
                        else if ( _col == 5 )
                            _border.BorderThickness = new Thickness ( 0 );
                        else if ( _col == 0 )
                            _border.BorderThickness = new Thickness ( 4, 2, 2, 2 );

                        Scoresheet.Children.Add ( _border );

                        Grid.SetColumn ( _border, _col );
                        Grid.SetRow ( _border, _row );
                        // Do I need this????????????????????????????????????????????????????????????????????????????????????????????????????????????????
                        this.RegisterName ( _post.Name, _post );
                    }
                    // End Post textbox.


                    // Begin  textbox in Button for entry rows.
                    else
                    {
                        // For Entry rows:  Add Button with a TextBlock and Border  to a Scoresheet grid cell.
                        _name = $"entryC{_col}R{_row}";
                        _entry = new Button ()
                        {
                            Name = $"{_name}",
                            HorizontalAlignment = HorizontalAlignment.Stretch,
                            VerticalAlignment = VerticalAlignment.Stretch,
                            HorizontalContentAlignment = HorizontalAlignment.Stretch,
                            VerticalContentAlignment = VerticalAlignment.Stretch,
                            BorderBrush = Brushes.Black,
                            BorderThickness = new Thickness ( 2 ),
                        };

                        _entry.Click += TakeScore_Click;
                        _entry.Background = Brushes.Transparent;
                        //_entry.BorderThickness = GetEntryBorderThickness ( _col );

                        // Set BorderThickness for different columns.
                        if ( _col == 4 )
                            _entry.BorderThickness = new Thickness ( 2, 2, 4, 2 );
                        else if ( _col == 0 )
                            _entry.BorderThickness = new Thickness ( 4, 2, 2, 2 );
                        else if ( _col == 5 )
                        {
                            _entry.BorderThickness = new Thickness ( 0 );
                            _entry.Background = ( _row % 2 == 0 ) ? Brushes.AliceBlue : Brushes.Gainsboro;
                        }

                        if ( _col != 5 )
                        {
                            if ( ( _row != 16 ) || ( _col <= 1 ) )
                            // Most buttons are finished here.
                            {
                                _entry.Content = _post;
                            }
                            // Begin 5OK buttons.
                            else
                            {
                                Grid buttonGrid = new Grid ();
                                //_post.Opacity = 0;
                                //buttonGrid.Children.Add ( _post );
                                StackPanel stackPanel = new StackPanel ()
                                {
                                    Orientation = Orientation.Horizontal,
                                };
                                buttonGrid.Children.Add ( stackPanel );


                                _entry.Content = buttonGrid;
                            }

                        }
                        // End _col != 5.

                        // Begin TakeScore buttons.
                        else
                        {
                            //_entry.Click += TakeScore_Click ;
                            StackPanel _stackPanel = new StackPanel ();
                            _entry.Content = _stackPanel;
                            _stackPanel.Children.Add ( _post );
                            _post.FontSize = 12;
                            _post.Text = _text = $"{headerLabels [ ( 42 ) ]}";

                            // TakeScore pretty lines
                            SolidColorBrush solidColorBrush = new SolidColorBrush ()
                            {
                                Color = Color.FromArgb ( 255, 0, 255, 0 ),
                            };
                            for ( int lines = 0; lines < 5; lines++ )
                            {
                                Line line = new Line ()
                                {
                                    Margin = new Thickness ( 10, 0, 10, 0 ),
                                    X1 = 0,
                                    Y1 = 0,
                                    X2 = 150,
                                    Y2 = 0,
                                    Stretch = Stretch.Uniform,
                                    HorizontalAlignment = HorizontalAlignment.Stretch,
                                    Stroke = ( lines % 2 == 1 ) ? solidColorBrush : Brushes.Green,
                                    StrokeThickness = ( lines % 2 == 1 ) ? 3 : 1,
                                    /*Stroke = gradientBrush,*/
                                    /*StrokeDashArray = {4,2}, */
                                };
                                _stackPanel.Children.Add ( line );
                            }
                            // End TakeScore lines.
                        }

                        Scoresheet.Children.Add ( _entry );

                        Grid.SetColumn ( _entry, _col );
                        Grid.SetRow ( _entry, _row );

                        entryColumn.Add ( _entry );

                        // Do I need this?? (dice didn't?).     *********************************************************************************  ( cuz I set NameScope? )
                        //this.RegisterName ( _entry.Name, _entry );
                    }
                    // End of Entry-buttons.
                }
                // End of _row loop

                entries.Add ( entryColumn );
                //entryColumn.Clear ();

                posts.Add ( postColumn );
                postColumn = new List<TextBlock> ();
            }
            // End _col loop.
        }
        // End of  InitializeScoreSheetVisual method.


        /// <summary>
        /// Moving this to GameModel
        /// </summary>
        void Init1SetFields ()
        {
            commitStrings = new string [] { "Roll Dice", "Choose a scoring option" };
            //TODO: headerLabels will only be used as row header texts for the first two columns, and for string building elsewhere.
            headerLabels = new string [] { "Upper Section", "Aces", "Deuces", "Threes", "Fours", "Fives", "Sixes", "Upper Score", "> 63 Bonus", "Lower Section",
                "3 o’Kind", "4 o’Kind", "Full House", "Small Straight", "Large Straight","Chance", "5 o’Kind", "5OK Bonus",
                /* "Bonus", */"Lower Total", "Grand Total", "Points Scored", "Add Aces", "Add Deuces", "Add Threes", "Add Fours",
                "Add Fives", "Add Sixes", "===>", "Score 35", "   ", "Add All Dice", "Add All Dice", "Score 25", "Score 30", "Score 40", "Add All Dice", /*"Score 50", */"+ For Each 5OK",
                "+ ADD 50/100", "===>", "===>", /*"Player",*/ "            ", "You have a XX% chance ", "0" };
            // Default player names provided.
            playerNames = new string [] { "Player 1", "Player 2", "Player 3" };
            scoringStrings = new string [] { "Scratch", "Take", "for", "points." };
        }


        string GetHeaderString ( int column, int row )
        {
            string text;
            // 20 rows * 6 columns
            // The first 40 strings are used for row header texts of the first two columns.
            // The first 20 strings in the array can be used for string-building the TakeScore text.
            int _index = ( column * 20 ) + row;
            if ( _index < 40 )
            {
                text = headerLabels [ _index ];
            }
            else if ( ( row == 0 ) && ( column < 5 ) )
            {
                text = playerNames [ column - 2 ];
            }
            else
            {
                text = headerLabels [ 40 ];
            }

            return text;
        }

        string GetScoringString ( GameStatus.GameRow.HighlightStyle highlightStyle, int row, string score )
        {
            string text;

            if ( highlightStyle == GameStatus.GameRow.HighlightStyle.Points )
            {
                // "Take" "Aces" "for" !value! "points".
                text = $"{scoringStrings [ 1 ]} { headerLabels [ row ]} {scoringStrings [ 2 ]} {score} {scoringStrings [ 3 ]}.";
            }
            else
                // "Scratch" "Aces".
                text = $"{scoringStrings [ 0 ]} { headerLabels [ row ]}.";

            return text;
        }


        //TODO:  Insert logic to convert row #?
        private int ConvertNameToRow ( string Name )
        {
            int result = 0;
            string rowString;
            // Could remove all letters and the first number.
            ////  Name = $"entryC{_col}R{_row}";
            //char [] trimChars = { 'e', 'n', 't', 'r', 'y', 'C', 'R' };
            //colAndRowString = Name.Trim ( trimChars );
            //result [ 0 ] = Int32.Parse ( $"{colAndRowString [ 0 ]}" );
            rowString = Name.Remove ( 0, 8 );
            result = Int32.Parse ( $"{rowString}" );
            return result;
        }
        #endregion MainWindow methods

    }
}

