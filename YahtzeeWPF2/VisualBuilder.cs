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



    ///// <summary>
    ///// REDACTED:  Use ScoresheetBuilderInstance ( re: garbage collection ).
    ///// Builds three lists of controls for MainWindow's scoresheet.
    ///// </summary>
    //public static class ScoresheetBuilderStatic
    //{
    //    #region Fields
    //    // Fields

    //    static List<Button> buttonColumn;
    //    static List<List<Button>> buttonColumns;

    //    static List<FrameworkElement> elementColumn;
    //    static List<List<FrameworkElement>> elementColumns;

    //    // Lists of the textblocks; used to provide quick access to .Text, and other properties.
    //    static List<TextBlock> textBlockColumn;
    //    static List<List<TextBlock>> textBlockColumns;
    //    #endregion Fields


    //    // Unused Constructor, for show.
    //    // Unused, because useless without params
    //    static ScoresheetBuilderStatic ()
    //    {
    //    }

    //    /// <summary>
    //    /// Main entry point for this class.
    //    /// </summary>
    //    /// <param name="scoresheetElements"></param>
    //    /// <param name="scoresheetTextBlocks"></param>
    //    /// <param name="scoresheetButtons"></param>
    //    public static void InitializeScoreSheetVisual2 (
    //        ref List<List<FrameworkElement>> scoresheetElements,
    //        ref List<List<TextBlock>> scoresheetTextBlocks,
    //        ref List<List<Button>> scoresheetButtons )

    //    {
    //        BuildColumns ();

    //        // Return three lists to the visual.
    //        // A list of buttons for hit testing.
    //        scoresheetButtons = buttonColumns;
    //        // A list of elements for populating the scoresheet, 
    //        scoresheetElements = elementColumns;
    //        // A list of the textboxes for updating the scoresheet.
    //        scoresheetTextBlocks = textBlockColumns;
    //    }


    //    /// <summary>
    //    /// Builds a List of lists.
    //    /// </summary>
    //    static void BuildColumns ()
    //    {
    //        buttonColumns = new List<List<Button>> ();
    //        elementColumns = new List<List<FrameworkElement>> ();
    //        textBlockColumns = new List<List<TextBlock>> ();

    //        for ( int _column = 0; _column < 6; _column++ )
    //        {
    //            BuildColumn ( _column );

    //            buttonColumns.Add ( buttonColumn );
    //            elementColumns.Add ( elementColumn );
    //            textBlockColumns.Add ( textBlockColumn );
    //        }
    //    }

    //    /// <summary>
    //    /// Builds a List of items.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    static void BuildColumn ( int column )
    //    {
    //        // buttonColumn is populated in GetButton method.
    //        buttonColumn = new List<Button> ();
    //        elementColumn = new List<FrameworkElement> ();
    //        // textBlockColumn is populated in GetTextBlock method.
    //        textBlockColumn = new List<TextBlock> ();

    //        // Create twenty rows for each column.
    //        for ( int _row = 0; _row < 20; _row++ )
    //        {
    //            if ( ( ( _row >= 1 ) && ( _row <= 6 ) ) || ( ( _row >= 10 ) && ( _row <= 16 ) ) )
    //            {
    //                // Add textblock(s) in a button "container".
    //                elementColumn.Add ( GetButton ( column, _row ) );
    //            }
    //            else
    //            {
    //                // Add a textblock in a border "container".
    //                elementColumn.Add ( GetBorder ( column, _row ) );
    //            }
    //        }
    //    }


    //    /// <summary>
    //    /// Supplies thickness params for all borders except the 5OK buttons.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static double [] GetBorderStyle ( int column, int row )
    //    {
    //        double x1 = 2;
    //        double x2 = 2;
    //        double y1 = 2;
    //        double y2 = 2;

    //        // Doubling the thickness of the unpaired borders of the outer vertical edges of the scoresheet.
    //        if ( column == 0 )
    //            x1 = 4;
    //        else if ( column == 4 )
    //            x2 = 4;

    //        if ( column == 5 )
    //        // No BorderThickness for  column 5 textboxes in a border.
    //        {
    //            x1 = 0;
    //            x2 = 0;
    //            y1 = 0;
    //            y2 = 0;
    //        }
    //        // Doubling the thickness of the unpaired borders of the outer horizontal edges of the scoresheet.
    //        else if ( row == 0 )
    //            y1 = 4;
    //        else if ( row == 19 )
    //            y2 = 4;
    //        // Special visual for the "Lower Section" scoresheet divider.
    //        else if ( row == 9 )
    //        {
    //            x1 = ( column == 0 ) ? 4 : 0;
    //            x2 = ( column == 4 ) ? 4 : 0;
    //        }

    //        double [] borderParams = { x1, y1, x2, y2 };
    //        return borderParams;
    //    }




    //    /// <summary>
    //    /// Supply a textbox wrapped in a border.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static Border GetBorder ( int column, int row )
    //    {
    //        double [] thick = GetBorderStyle ( column, row );
    //        Border _border = new Border ()
    //        {
    //            Background = Brushes.Transparent,
    //            BorderBrush = Brushes.Black,
    //            BorderThickness = new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
    //            Child = GetTextBlock ( column, row ),
    //        };
    //        return _border;
    //    }


    //    /// <summary>
    //    /// Supply a textbox(s) wrapped in a button.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static Button GetButton ( int column, int row )
    //    {
    //        double [] thick = GetBorderStyle ( column, row );

    //        string _name = $"entryC{column}R{row}";
    //        Button _button = new Button ()
    //        {
    //            Background = ( column == 5 ) ? Brushes.AliceBlue : Brushes.Transparent,
    //            BorderBrush = Brushes.Black,
    //            BorderThickness = ( column == 5 ) ? new Thickness ( 1 )
    //                : new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
    //            Name = $"{_name}",
    //            Padding = ( column == 5 ) ? new Thickness ( 2 ) : new Thickness ( 0 ),
    //            HorizontalAlignment = HorizontalAlignment.Stretch,
    //            HorizontalContentAlignment = HorizontalAlignment.Stretch,
    //            VerticalAlignment = VerticalAlignment.Stretch,
    //            VerticalContentAlignment = VerticalAlignment.Stretch,
    //        };
    //        if ( column == 5 )
    //        {
    //            _button.Content = GetTakeScoreContent ( column, row );
    //            _button.Margin = new Thickness ( 0, 2, 0, 2 );
    //        }
    //        else if ( ( row == 16 ) && ( column >= 2 ) )
    //        {
    //            _button.Content = Get5OkContent ( column, row );
    //        }
    //        else
    //        {
    //            _button.Content = GetTextBlock ( column, row );
    //        }
    //        buttonColumn.Add ( _button );
    //        return _button;
    //    }


    //    /// <summary>
    //    /// Builds the content for a five of a kind button.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static StackPanel Get5OkContent ( int column, int row )
    //    {
    //        StackPanel _stackPanel = new StackPanel ()
    //        {
    //            Orientation = Orientation.Horizontal,
    //        };
    //        for ( int i = 0; i < 4; i++ )
    //        {
    //            Border _border = new Border ()
    //            {
    //                BorderBrush = Brushes.Black,
    //                BorderThickness = ( i != 3 ) ? new Thickness ( 0, 0, 2, 0 ) : new Thickness ( 0, 0, 0, 0 ),
    //                Child = GetTextBlock ( column, row ),
    //                HorizontalAlignment = HorizontalAlignment.Stretch,
    //            };
    //            _stackPanel.Children.Add ( _border );
    //        }

    //        return _stackPanel;
    //    }


    //    /// <summary>
    //    /// Builds the content for a take score button.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static StackPanel GetTakeScoreContent ( int column, int row )
    //    {
    //        StackPanel _stackPanel = new StackPanel ()
    //        {
    //            Orientation = Orientation.Vertical,

    //        };
    //        _stackPanel.Children.Add ( GetTextBlock ( column, row ) );

    //        //TODO:  Make a list of lines, or add another textbox, or ...................................?????????????????????????????????????????????????????????????
    //        // TakeScore pretty lines
    //        SolidColorBrush solidColorBrush = new SolidColorBrush ()
    //        {
    //            Color = Color.FromArgb ( 255, 0, 255, 0 ),
    //        };
    //        for ( int lines = 0; lines < 5; lines++ )
    //        {
    //            Line line = new Line ()
    //            {
    //                Margin = new Thickness ( 10, 0, 10, 0 ),
    //                X1 = 0,
    //                Y1 = 0,
    //                X2 = 150,
    //                Y2 = 0,
    //                Stretch = Stretch.Uniform,
    //                HorizontalAlignment = HorizontalAlignment.Stretch,
    //                Stroke = ( lines % 2 == 1 ) ? solidColorBrush : Brushes.Green,
    //                StrokeThickness = ( lines % 2 == 1 ) ? 3 : 1,
    //                /*Stroke = gradientBrush,*/
    //                /*StrokeDashArray = {4,2}, */
    //            };
    //            _stackPanel.Children.Add ( line );
    //        }
    //        // End TakeScore lines.

    //        return _stackPanel;
    //    }


    //    /// <summary>
    //    /// Supplies the text string for row header, player name or "unfilled".
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static string GetText ( int column, int row )
    //    {
    //        string _text = "   ";
    //        if ( column <= 1 )
    //            _text = GameModel.GameStrings.GetHeaderString ( column, row );
    //        else if ( ( row == 0 ) && ( column <= 4 ) )
    //            _text = GameModel.GameStrings.GetPlayerName ( ( column - 2 ) );

    //        return _text;
    //    }


    //    /// <summary>
    //    /// Builds a generic textblock.
    //    /// </summary>
    //    /// <param name="column"></param>
    //    /// <param name="row"></param>
    //    /// <returns></returns>
    //    static TextBlock GetTextBlock ( int column, int row )
    //    {
    //        // NOTE: Player columns will have 3 more textboxes than other columns.
    //        FontWeight fontWeight = FontWeights.Bold;
    //        if ( ( column >= 2 ) && ( column <= 4 ) && ( row >= 1 ) )
    //        {
    //            fontWeight = FontWeights.ExtraBold;
    //        }
    //        TextBlock _textBlock = new TextBlock ()
    //        {
    //            /*Name = _name,  Don't need a name */
    //            TextAlignment = TextAlignment.Center,
    //            /* FontFamily = new FontFamily ( "Ebrima" ),*/
    //            FontWeight = fontWeight,
    //            /*Height = 25,
    //            Width = _width,*/
    //            Text = GetText ( column, row ),
    //            Padding = new Thickness ( 3 ),
    //            HorizontalAlignment = HorizontalAlignment.Stretch,
    //            VerticalAlignment = VerticalAlignment.Stretch,
    //        };
    //        if ( column == 5 )
    //        {
    //            _textBlock.FontSize = 11;
    //            //_textBlock.VerticalAlignment = VerticalAlignment.Top;
    //        }
    //        else if ( row == 9 )
    //            _textBlock.FontSize = 14;
    //        else
    //            _textBlock.FontSize = 18;

    //        textBlockColumn.Add ( _textBlock );
    //        return _textBlock;
    //    }


    //}





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


        // On instantiation, build three lists as Properties for the Scoresheet visual.
        public ScoresheetBuilderInstance ()
        {
            BuildColumns ();
        }


        // Properties
        public List<List<FrameworkElement>> ScoresheetElements
        {
            get
            {
                return elementColumns;
            }
        }


        public List<List<TextBlock>> ScoresheetTextBlocks
        {
            get
            {
                return textBlockColumns;
            }
        }


        public List<List<Button>> ScoresheetButtons
        {
            get
            {
                return buttonColumns;
            }
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
        /// <param name="column"></param>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        string GetText ( int column, int row )
        {
            var _text = "   ";
            if ( column <= 1 )
                _text = GameModel.GameStrings.GetHeaderString ( column, row );
            else if ( ( row == 0 ) && ( column <= 4 ) )
                _text = GameModel.GameStrings.GetPlayerName ( ( column - 2 ) );

            return _text;
        }


        /// <summary>
        /// Builds a generic textblock.
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
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

        Double _dblNum;

        // Button with textblocks.
        Button playerCommit;


        // Create a die, parent is the container for a canvas containing three button faces.
        Button _die;
        Button _topFace;
        Button _leftFace;
        Button _rightFace;
        Canvas _canvas;
        List<Button> _dieList;

        List<List<Button>> diceList;

        // Constructor will build the dicebox elements and expose them as properties for the visual.
        public DiceBoxBuilder ()
        {
            BuildDice ();
        }

        // Enums ( for documentation )
        enum DieElements
        {
            DieContainer,
            DieFaceContainer,
            TopFace,
            LeftFace,
            RightFace,
        }

        enum DieMargins
        {
            Left = 60,
            LeftSpacing = 130,
            Top = 365,
        }
        enum Face
        {
            Top,
            Left,
            Right,
        }


        // Properties

        public Button PlayerCommit
        {
            get
            {
                return playerCommit;
            }
        }


        public List<List<Button>> DiceList
        {
            get
            {
                return diceList;
            }
        }

        // Methods


        //void BuildDice ( ref List<List<Button>> diceList )
        void BuildDice ()
        {
            diceList = new List<List<Button>> ();


            for ( int _dieNum = 0; _dieNum < 5; _dieNum++ )
            {
                diceList.Add ( GetDieList ( _dieNum ) );

                // Set margin's left thickness.
                //Double _left = 60.0 + ( _dieNum * 130 );
                //_dblNum = 60.0 + ( i * 130 );
                //x= 60 y= 365
            }


        }


        List<Button> GetDieList ( int dieNum )
        {
            // _dieButtons provides handles for all the buttons that make up each die.
            // _dieList.Add ( _die,  _topFace,  _leftFace,  _rightFace );

            var _dieControls = new List<Button> ();
            var _dieControl = new Button ();
            for ( var dieElements = DieElements.DieContainer; dieElements <= DieElements.RightFace; dieElements++ )
            {

            }

            return _dieControls;
        }

        //Button GetButton (



        void BuildDie ()
        {

            // Create the three faces for each die.
            var topGroup = new TransformGroup ();
            topGroup.Children.Add ( new RotateTransform ( 45.0 ) );
            topGroup.Children.Add ( new ScaleTransform ( 1.45, 1.45 ) );
            topGroup.Children.Add ( new SkewTransform ( 0, 0 ) );

            var leftGroup = new TransformGroup ();
            leftGroup.Children.Add ( new RotateTransform ( 100.0 ) );
            leftGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
            leftGroup.Children.Add ( new SkewTransform ( 10.0, 40.0 ) );

            var rightGroup = new TransformGroup ();
            rightGroup.Children.Add ( new RotateTransform ( -101.0 ) );
            rightGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
            rightGroup.Children.Add ( new SkewTransform ( -11.0, -39.0 ) );


            //for ( int i = 0; i < 5; i++ )
            //{
            int i = 0;  // TEMP PLACEHOLDER  *****************************************************************************************************************************

            _dblNum = 60.0 + ( i * 130 );
            //x= 60 y= 365

            _die = new Button
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
            ////_parent.Click += Die_Click;

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
            _die.Content = _canvas;

            //// Do this in visual.
            ////diceBox.Children.Add ( _parent );

            _dieList = new List<Button> ();
            _dieList.Add ( _die );
            _dieList.Add ( _topFace );
            _dieList.Add ( _leftFace );
            _dieList.Add ( _rightFace );

            ////    visualDiceList.Add ( _buttons );
            ////}
            //// End of Dice creation loop.

        }

    }





}
