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
    /// <summary>
    /// Builds three lists of controls for MainWindow's scoresheet.
    /// </summary>
    public class ScoresheetBuilderInstance
    {
        #region Fields
        // Fields

        List<Button> buttonColumn;
        List<List<Button>> buttonColumns;

        List<FrameworkElement> elementColumn;
        List<List<FrameworkElement>> elementColumns;

        // Lists of the textblocks; used to provide quick access to .Text, and other properties.
        List<TextBlock> textBlockColumn;
        List<List<TextBlock>> textBlockColumns;
        #endregion Fields


        // Constructor
        public ScoresheetBuilderInstance ()
        {
            BuildColumns ();
        }


        // Properties
        public List<List<FrameworkElement>> ScoresheetElements
        {
            get => elementColumns;
        }


        public List<List<TextBlock>> ScoresheetTextBlocks
        {
            get=> textBlockColumns;
        }


        public List<List<Button>> ScoresheetButtons
        {
            get => buttonColumns;
        }


        /// <summary>
        /// Builds a List of lists.
        /// </summary>
        void BuildColumns ()
        {
            buttonColumns = new List<List<Button>> ();
            elementColumns = new List<List<FrameworkElement>> ();
            textBlockColumns = new List<List<TextBlock>> ();

            for ( int _column = 0; _column < 6; _column++ )
            {
                BuildColumn ( _column );

                buttonColumns.Add ( buttonColumn );
                elementColumns.Add ( elementColumn );
                textBlockColumns.Add ( textBlockColumn );
            }
        }


        /// <summary>
        /// Builds a List of items.
        /// </summary>
        void BuildColumn ( int column )
        {
            // buttonColumn is populated in GetButton method.
            buttonColumn = new List<Button> ();
            elementColumn = new List<FrameworkElement> ();
            // textBlockColumn is populated in GetTextBlock method.
            textBlockColumn = new List<TextBlock> ();

            // Create twenty rows for each column.
            for ( int _row = 0; _row < 20; _row++ )
            {
                if ( ( ( _row >= 1 ) && ( _row <= 6 ) ) || ( ( _row >= 10 ) && ( _row <= 16 ) ) )
                {
                    // Add textblock(s) in a button "container".
                    elementColumn.Add ( GetButton ( column, _row ) );
                }
                else
                {
                    // Add a textblock in a border "container".
                    elementColumn.Add ( GetBorder ( column, _row ) );
                }
            }
        }


        /// <summary>
        /// Supplies thickness params for all borders except the 5OK buttons.
        /// </summary>
        double [] GetBorderStyle ( int column, int row )
        {
            double x1 = 2;
            double x2 = 2;
            double y1 = 2;
            double y2 = 2;

            // Doubling the thickness of the unpaired borders of the outer vertical edges of the scoresheet.
            if ( column == 0 )
                x1 = 4;
            else if ( column == 4 )
                x2 = 4;

            if ( column == 5 )
            // No BorderThickness for  column 5 textboxes in a border.
            {
                x1 = 0;
                x2 = 0;
                y1 = 0;
                y2 = 0;
            }
            // Doubling the thickness of the unpaired borders of the outer horizontal edges of the scoresheet.
            else if ( row == 0 )
                y1 = 4;
            else if ( row == 19 )
                y2 = 4;
            // Special visual for the "Lower Section" scoresheet divider.
            else if ( row == 9 )
            {
                x1 = ( column == 0 ) ? 4 : 0;
                x2 = ( column == 4 ) ? 4 : 0;
            }

            double [] borderParams = { x1, y1, x2, y2 };
            return borderParams;
        }


        /// <summary>
        /// Supply a textbox wrapped in a border.
        /// </summary>
        Border GetBorder ( int column, int row )
        {
            double [] thick = GetBorderStyle ( column, row );
            var _border = new Border ()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
                Child = GetTextBlock ( column, row ),
            };
            return _border;
        }


        /// <summary>
        /// Supply a textbox(s) wrapped in a button.
        /// </summary>
        Button GetButton ( int column, int row )
        {
            double [] thick = GetBorderStyle ( column, row );

            var _name = $"entryC{column}R{row}";
            var _button = new Button ()
            {
                Background = ( column == 5 ) ? Brushes.AliceBlue : Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = ( column == 5 ) ? new Thickness ( 1 )
                    : new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
                Name = $"{_name}",
                Padding = ( column == 5 ) ? new Thickness ( 2 ) : new Thickness ( 0 ),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };
            if ( column == 5 )
            {
                _button.Content = GetTakeScoreContent ( column, row );
                _button.Margin = new Thickness ( 0, 2, 0, 2 );
            }
            else if ( ( row == 16 ) && ( column >= 2 ) )
            {
                _button.Content = Get5OkContent ( column, row );
            }
            else
            {
                _button.Content = GetTextBlock ( column, row );
            }
            buttonColumn.Add ( _button );
            return _button;
        }


        /// <summary>
        /// Builds the content for a five of a kind button.
        /// </summary>
        StackPanel Get5OkContent ( int column, int row )
        {
            var _stackPanel = new StackPanel ()
            {
                Orientation = Orientation.Horizontal,
            };
            for ( int i = 0; i < 4; i++ )
            {
                var _border = new Border ()
                {
                    BorderBrush = Brushes.Black,
                    BorderThickness = ( i != 3 ) ? new Thickness ( 0, 0, 2, 0 ) : new Thickness ( 0, 0, 0, 0 ),
                    Child = GetTextBlock ( column, row ),
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };
                _stackPanel.Children.Add ( _border );
            }

            return _stackPanel;
        }


        /// <summary>
        /// Builds the content for a take score button.
        /// </summary>
        StackPanel GetTakeScoreContent ( int column, int row )
        {
            var _stackPanel = new StackPanel ()
            {
                Orientation = Orientation.Vertical,

            };
            _stackPanel.Children.Add ( GetTextBlock ( column, row ) );

            //TODO:  Make a list of lines, or add another textbox, or ...................................?????????????????????????????????????????????????????????????
            // TakeScore pretty lines
            var solidColorBrush = new SolidColorBrush ()
            {
                Color = Color.FromArgb ( 255, 0, 255, 0 ),
            };
            for ( int lines = 0; lines < 5; lines++ )
            {
                var line = new Line ()
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

            return _stackPanel;
        }


        /// <summary>
        /// Supplies the text string for row header, player name or "unfilled".
        /// </summary>
        string GetText ( int column, int row )
        {
            var _text = "   ";
            if ( column <= 1 )
                _text = GameStrings1.GetHeaderString ( column, row );
            //_text = GameModel.GameStrings.GetHeaderString ( column, row );
            else if ( ( row == 0 ) && ( column <= 4 ) )
                _text = GameStrings1.GetPlayerName ( ( column - 2 ) );
            //_text = GameModel.GameStrings.GetPlayerName ( ( column - 2 ) );

            return _text;
        }


        /// <summary>
        /// Builds a generic textblock.
        /// </summary>
        TextBlock GetTextBlock ( int column, int row )
        {
            // NOTE: Player columns will have 3 more textboxes than other columns.
            var fontWeight = FontWeights.Bold;
            if ( ( column >= 2 ) && ( column <= 4 ) && ( row >= 1 ) )
            {
                fontWeight = FontWeights.ExtraBold;
            }
            var _textBlock = new TextBlock ()
            {
                /*Name = _name,  Don't need a name */
                TextAlignment = TextAlignment.Center,
                /* FontFamily = new FontFamily ( "Ebrima" ),*/
                FontWeight = fontWeight,
                /*Height = 25,
                Width = _width,*/
                Text = GetText ( column, row ),
                Padding = new Thickness ( 3 ),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            if ( column == 5 )
            {
                _textBlock.FontSize = 11;
                //_textBlock.VerticalAlignment = VerticalAlignment.Top;
            }
            else if ( row == 9 )
                _textBlock.FontSize = 14;
            else
                _textBlock.FontSize = 18;

            textBlockColumn.Add ( _textBlock );
            return _textBlock;
        }


    }





    /// <summary>
    /// Work in progress, Refactoring MainWindow!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    /// </summary>
    public class DiceBoxBuilder
    {
        // Fields

        List<List<Button>> diceList;


        // Constructor
        public DiceBoxBuilder ()
        {
            BuildDiceList ();
        }


        // Property
        public List<List<Button>> DiceList
        {
            get
            {
                return diceList;
            }
        }


        // Methods

        void BuildDiceList ()
        {
            diceList = new List<List<Button>> ();
            // Build five dice and add each one to the list.
            for ( int _dieNum = 0; _dieNum < 5; _dieNum++ )
            {
                diceList.Add ( BuildDie ( _dieNum ) );
            }
        }

        // Return a list that provides handles for all the buttons that make up each die.
        List<Button> BuildDie ( int dieNum )
        {
            var _canvas = GetDieCanvas ();
            var _dieButtonList = new List<Button> ();

            // Build dieContainer and three die faces.
            for ( int i = 0; i < 4; i++ )
            {
                var _dieButton = GetDieButton ( dieNum, i );

                if ( i == 0 )
                {
                    _dieButton.Content = _canvas;
                }
                else
                {
                    _canvas.Children.Add ( _dieButton );
                }

                _dieButtonList.Add ( _dieButton );
            }

            return _dieButtonList;
        }


        Button GetDieButton ( int dieNum, int i )
        {
            var name = $"die{ dieNum }face{ i }";
            if ( i == 0 )
            {
                var _dieContainer = new Button
                {
                    Name = name,
                    Background = Brushes.Transparent,
                    BorderBrush = Brushes.Black,
                    BorderThickness = new Thickness ( 1 ),
                    FontSize = 22,
                    Margin = GetMarginThickness ( dieNum, i ),
                    /*Margin = GetMarginThickness ( dieNum, dieElement ),*/
                    Height = 155,
                    Width = 105,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Stretch,
                    VerticalContentAlignment = VerticalAlignment.Stretch,
                };
                return _dieContainer;
            }
            else
            {
                var _dieFace = new Button
                {
                    Name = name,
                    BorderThickness = new Thickness ( 4 ),
                    Background = ( i == 1 ) ? Brushes.LightGoldenrodYellow : Brushes.DarkGray,
                    FontWeight = ( i == 1 ) ? FontWeights.ExtraBold : FontWeights.Medium,
                    Margin = GetMarginThickness ( dieNum, i ),
                    Height = 50,
                    Width = 50,
                    RenderTransform = GetDieFaceTransformGroup ( i ),
                };
                return _dieFace;
            }
        }


        Canvas GetDieCanvas ()
        {
            var _canvas = new Canvas
            {
                /*Background = Brushes.Transparent,*/
                Height = 148,
                Width = 97,
            };
            return _canvas;
        }


        Thickness GetMarginThickness ( int dieNum, int i )
        {
            var _thickness = new Thickness ();

            switch ( i )
            {
                case 0:
                    _thickness = new Thickness ( ( 60.0 + ( dieNum * 130 ) ), 365.0, 0.0, 0.0 );
                    break;
                case 1:
                    _thickness = new Thickness ( 48, 1, 0, 0 );
                    break;
                case 2:
                    _thickness = new Thickness ( 47, 102, 0, 0 );
                    break;
                // Right face.
                default:
                    _thickness = new Thickness ( 47, 144, 0, 0 );
                    break;
            }
            return _thickness;
        }


        TransformGroup GetDieFaceTransformGroup ( int i )
        {
            var _transformGroup = new TransformGroup ();

            switch ( i )
            {
                case 1:
                    _transformGroup.Children.Add ( new RotateTransform ( 45.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( 1.45, 1.45 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( 0, 0 ) );
                    break;
                case 2:
                    _transformGroup.Children.Add ( new RotateTransform ( 100.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( 10.0, 40.0 ) );
                    break;
                default:
                    _transformGroup.Children.Add ( new RotateTransform ( -101.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( -11.0, -39.0 ) );
                    break;
            }
            return _transformGroup;
        }

    }



}
