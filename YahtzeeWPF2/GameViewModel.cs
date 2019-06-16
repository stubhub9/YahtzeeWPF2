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
    /// Generate a big, fat user control for the Scoresheet.
    /// </summary>
    class GameViewModel
    {

        // Fields

        static List<TextBlock> postColumn;

        // Lists of buttons and borders; each containing textblock(s).
        static List< Control > controlColumn;
        static List < List < Control >> controlColumns;

        // Lists of the textblocks; used to provide quick access to .Text, and other properties.
        static List< TextBlock > textBlockColumn;
        static List < List < TextBlock >> textBlockColumns;

        // Enums to provide more logic documentation by replacing magic numbers.
        static ScoresheetColumnType columnType;
        static ScoresheetRowType rowType;



        // Enums to replace some magic numbe

        enum ScoresheetRowType
        {
            ColumnHeader,
            Entry,
            Entry5OK,
            Post,
            Divider,
            Nil
        }

        enum ScoresheetColumnType
        {
            RowHeader,
            Player,
            TakeScore
        }

        // Is "ref" necessary ??????????????????????????????????????????????????????????????????????????????????????????????????????????????????????
        static void InitializeScoreSheetVisual2 ( ref List<List<TextBlock>> posts )
        {
            //entryColumn = new List<Button> ();
            //entries = new List<List<Button>> ();
            posts = new List<List<TextBlock>> ();
            InitializeScoreSheetColumns ( ref  posts );
        }


        static void InitializeScoreSheetColumns ( ref List<List<TextBlock>> posts )
        {
            for ( int _col = 0; _col < 6; _col++ )
            {
                if ( _col <= 1 )
                    columnType = ScoresheetColumnType.RowHeader;
                else if ( _col == 5 )
                    columnType = ScoresheetColumnType.TakeScore;
                else
                    columnType = ScoresheetColumnType.Player;

                posts.Add ( BuildScoresheetColumn ( _col, columnType ) );
                //posts.Add ( BuildScoresheetColumn ( _col ) );
            }
        }


        static List<TextBlock> BuildScoresheetColumn ( int column, ScoresheetColumnType columnType )
        //static List<TextBlock> BuildScoresheetColumn ( int column )
        {
            postColumn = new List<TextBlock> ();
            // Create twenty rows for each column.
            for ( int _row = 0; _row < 20; _row++ )
            {
                if ( _row == 0 )
                {
                    rowType = ScoresheetRowType.ColumnHeader;
                }
                else if ( _row == 9 )
                {
                    rowType = ScoresheetRowType.Divider;
                }
                else if ( ( _row == 7 ) || ( _row == 8 ) || ( _row >= 17 ) )
                {
                    rowType = ScoresheetRowType.Post;
                }
                else
                {
                    rowType = ScoresheetRowType.Entry;
                }
            }
            return postColumn;
        }

        static int [] CreateBorderParams ( int column, int row )
        {
            int x1 = 2;
            int x2 = 2;
            int y1 = 2;
            int y2 = 2;
            if ( column == 0 )
                y1 = 4;
            else if ( column == 4 )
                y2 = 4;

            //if ( column == 5 )
            if ( columnType == ScoresheetColumnType.TakeScore )
            {
                x1 = 0;
                x2 = 0;
                y1 = 0;
                y2 = 0;
            }
            //else if ( row == 0 )
            else if ( rowType == ScoresheetRowType.ColumnHeader )
                x1 = 4;
            else if ( row == 19 )
                x2 = 4;
            //else if ( row == 9 )

            else if ( rowType == ScoresheetRowType.Divider )
            {
                if ( column != 0 )
                    x1 = 0;
                else if ( column != 4 )
                    x2 = 0;
            }
            int [] borderParams = { x1, y1, x2, y2 };
            return borderParams;
        }

        Border CreateScoreSheetBorder ( int column, int row )
        {
            int [] thick = CreateBorderParams ( column, row );
            var _border = new Border ()
            {
                BorderThickness = new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ),
                //BorderThickness = ( column < 5 ) ? new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] ) : new Thickness ( 0 ),
            };
            return _border;
        }

        // Check for columnType == takeScore or RowType == 5OK entry.
        Button CreateScoreSheetButton ( int column, int row )
        {
            int [] thick = CreateBorderParams ( column, row );
            var _button = new Button ()
            {
                Background = Brushes.Transparent,
                Name = $"entryC{column}R{row}",
                BorderThickness = new Thickness ( thick [ 0 ], thick [ 1 ], thick [ 2 ], thick [ 3 ] )
            };
            return _button;
        }

        TextBox CreateScoreSheetTextbox ( string text )
        {

            var _textBox = new TextBox ()
            {
                Background = Brushes.Transparent,
                FontSize = ( rowType == ScoresheetRowType.Divider ) ? 16.0 : 18.0,
                FontWeight = FontWeights.Bold,
                Padding = new Thickness ( 3 ),
                Text = text,
                TextAlignment = TextAlignment.Center,
                VerticalAlignment = VerticalAlignment.Stretch,
                Width = ( columnType == ScoresheetColumnType.Player ) ? 40 : 150,
            };
            return _textBox;
        }

































    }
}
