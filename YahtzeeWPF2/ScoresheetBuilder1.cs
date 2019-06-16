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
    public static class ScoresheetBuilder1
    {
        // Fields

        //static int [] borderStyle = { 2, 2, 2, 2 };
        
        // Lists of buttons and borders; each containing textblock(s).
        //static List<Control> controlColumn;
        //static List<List<Control>> controlColumns;

        static List<FrameworkElement> elementColumn;
        static List<List<FrameworkElement>> elementColumns;

        // Lists of the textblocks; used to provide quick access to .Text, and other properties.
        static List<TextBlock> textBlockColumn;
        static List<List<TextBlock>> textBlockColumns;



        // Constructor 
        // Unused, because useless without params
        static ScoresheetBuilder1 ()
        {
        }

        // Is "ref" necessary ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
        /// <summary>
        /// Main entry point for this class.
        /// </summary>
        /// <param name="scoresheetElements"></param>
        /// <param name="scoresheetTextBlocks"></param>
        public static void InitializeScoreSheetVisual2 ( ref List<List<FrameworkElement>> scoresheetElements, ref List<List<TextBlock>> scoresheetTextBlocks )
        {
            //scoresheetControls = new List<List<Control>> ();
            //scoresheetTextBlocks = new List<List<TextBlock>> ();

            BuildColumns ();

            scoresheetElements = elementColumns;
            scoresheetTextBlocks = textBlockColumns;
        }



        static void BuildColumns ()
        {
            elementColumns = new List<List<FrameworkElement>> ();
            textBlockColumns = new List<List<TextBlock>> ();

            for ( int _column = 0; _column < 6; _column++ )
            {

                BuildColumn ( _column );
                elementColumns.Add ( elementColumn );
                textBlockColumns.Add ( textBlockColumn );

            }
        }



        static void BuildColumn ( int column )
        {
            elementColumn = new List<FrameworkElement> ();
            textBlockColumn = new List<TextBlock> ();

            // Create twenty rows for each column.
            for ( int _row = 0; _row < 20; _row++ )
            {
                if ((( _row >= 1 ) && ( _row <= 6 )) || (( _row >= 10) && ( _row <= 16 )))
                {
                    elementColumn.Add ( GetButton ( column, _row ));
                }
                else
                {
                    elementColumn.Add ( GetBorder ( column, _row ) );
                }
            }
        }


        /// <summary>
        /// Used to create even borders for the scoresheet
        /// </summary>
        /// <param name="column"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        static double [] GetBorderStyle ( int column, int row )
        {
            double x1 = 2;
            double x2 = 2;
            double y1 = 2;
            double y2 = 2;

            // Doubling the thickness of the unpaired borders of the outer edge of the scoresheet.
            if ( column == 0 )
                x1 = 4;
            else if ( column == 4 )
                x2 = 4;

            if ( column == 5 )
                // No BorderThickness for  column 5 textboxes in a border.
                //if ( columnType == ScoresheetColumnType.TakeScore )
            {
                x1 = 0;
                x2 = 0;
                y1 = 0;
                y2 = 0;
            }
            else if ( row == 0 )
            //else if ( rowType == ScoresheetRowType.ColumnHeader )
                y1 = 4;
            else if ( row == 19 )
                y2 = 4;
            else if ( row == 9 )
            //else if ( rowType == ScoresheetRowType.Divider )
            {
                x1 = ( column == 0 ) ? 4 : 0;
                x2 = ( column == 4 ) ? 4 : 0;
            }

            double [] borderParams = { x1, y1, x2, y2 };
            return borderParams;
        }



        static Border GetBorder ( int column, int row )
        {
            double [] thick = GetBorderStyle ( column, row );
            Border _border = new Border ()
            {
                Background = Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = new Thickness ( thick [0], thick [1], thick [2], thick [3] ),
                Child = GetTextBlock ( column, row ),
            };
            return _border;
        }


        static Button GetButton ( int column, int row )
        {
            double [] thick = GetBorderStyle ( column, row );

            string _name = $"entryC{column}R{row}";
            Button _button = new Button ()
            {
                Background = ( column == 5 ) ? Brushes.AliceBlue : Brushes.Transparent,
                BorderBrush = Brushes.Black,
                BorderThickness = (column == 5) ? new Thickness (1)
                    : new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
                Name = $"{_name}",
                Padding = ( column == 5 ) ? new Thickness ( 2 ) : new Thickness (0),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };
            if ( column == 5 )
            {
                _button.Content = GetTakeScoreContent ( column, row );
            }
            else if (( row == 16 ) && ( column >= 2 ))
            {
                _button.Content = Get5OkContent ( column, row );
            }
            else
            {
                _button.Content = GetTextBlock ( column, row );
            }
            return _button;
        }


        static StackPanel Get5OkContent ( int column, int row )
        {
            //_entry.Content = _stackPanel;
            //_stackPanel.Children.Add ( _post );
            //_post.FontSize = 12;
            //_post.Text = _text = $"{headerLabels [ ( 42 ) ]}";
            StackPanel _stackPanel = new StackPanel ()
            {
                Orientation = Orientation.Horizontal,
            };
            //_stackPanel.Children.Add ( GetTextBlock ( column, row ));
            for ( int i = 0; i < 4; i++ )
            {
                Border _border = new Border ()
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



        static StackPanel GetTakeScoreContent ( int column, int row )
        {
            StackPanel _stackPanel = new StackPanel ()
            {
                Orientation = Orientation.Vertical,

            }; 
            _stackPanel.Children.Add ( GetTextBlock ( column, row ));

            //TODO:  Make a list of lines, or add another textbox, or ...................................?????????????????????????????????????????????????????????????
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

            return _stackPanel;
        }


        static string GetText ( int column, int row )
        {
            string _text = "   ";
            if ( column <= 1 )
                _text = GameModel.GameStrings.GetHeaderString ( column, row );
            else if ( ( row == 0 ) && ( column <= 4 ) )
                _text = GameModel.GameStrings.GetPlayerName (( column - 2 ));

            return _text;
        }


        static TextBlock GetTextBlock ( int column, int row )
        {
            // NOTE: Player columns will have 3 more textboxes than other columns.
            FontWeight fontWeight = FontWeights.Bold;
            if ((column >= 2 ) && (column <= 4) && ( row >= 1 ))
            {
                fontWeight = FontWeights.ExtraBold;
            }
            TextBlock _textBlock = new TextBlock ()
            {
                /*Name = _name,  Don't need a name */
                TextAlignment = TextAlignment.Center,
                /* FontFamily = new FontFamily ( "Ebrima" ),*/
                FontWeight = fontWeight,
                /*FontSize = ( row != 9 ) ? 18.0 : 16.0,*/
                /*Height = 25,
                Width = _width,*/
                Text = GetText ( column, row ),
                Padding = new Thickness ( 3 ),
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
            };
            if ( column == 5 )
                _textBlock.FontSize = 12;
            else if ( row == 9 )
                _textBlock.FontSize = 14;
            //else if ( ( row == 16 ) && ( column >= 2 ) )
            //    _textBlock.FontSize = 18;
            else
                _textBlock.FontSize = 18;

            textBlockColumn.Add ( _textBlock );
            return _textBlock;
        }























    }
}
