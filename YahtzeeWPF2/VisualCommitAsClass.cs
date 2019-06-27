using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;



namespace YahtzeeWPF2
{
    /// <summary>
    /// Commit button for the dice box; if it inherited from button as the CommitContainer.
    /// Implement IBuiltMyOwnControl
    /// </summary>
    //public class VisualCommitButton : Button
    public class VisualCommitAsClass 
    {

        // Constructor
        public VisualCommitAsClass ()
        {
            BuildControl ();
        }


        #region Properties
        // Properties
        // Handles for animations.
        public Button CommitContainer { get; set; }

        public Grid ContentGrid { get; set; }

        public TextBlock PlayerNameTxtBlk { get; set; }

        public TextBlock ActionTxtBlk { get; set; }

        public TextBlock DescriptionTxtBlk { get; set; }

        // Visual Setters
        public Brush PlayerColor
        {
            set
            {
                PlayerNameTxtBlk.Background = value;
            }
        }

        public string PlayerName
        {
            set
            {
                PlayerNameTxtBlk.Text = value;
            }
        }

        public string Action
        {
            set
            {
                ActionTxtBlk.Text = value;
            }
        }

        public string Description
        {
            set
            {
                DescriptionTxtBlk.Text = value;
            }
        }
        #endregion Properties


        #region Methods
        // Methods
        void BuildControl ()
        {
            BuildContainer ();
            BuildContentContainer ();
            BuildContent ();
        }


        void BuildContainer ()
        {
            var _button = new Button
            {
                Background = Brushes.Beige,
                Margin = new Thickness ( 10, 10, 0, 0 ),
                Height = 150,
                Width = 730,
                BorderThickness = new Thickness ( 2 ),
                BorderBrush = Brushes.Black,
                FontSize = 22,
                FontWeight = FontWeights.Medium,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Stretch,
                VerticalContentAlignment = VerticalAlignment.Stretch,
            };
            CommitContainer = _button;
        }


        /// <summary>
        /// Maybe I can use loops to build the grid definitions.
        /// </summary>
        /// <returns></returns>
        void BuildContentContainer ()
        {
            var _grid = new Grid ();
            {

            };
            var colSpacer = new ColumnDefinition ()
            {
                Width = new GridLength ( 1, GridUnitType.Star ),
            };
            var colSpacer1 = new ColumnDefinition ()
            {
                Width = new GridLength ( 1, GridUnitType.Star ),
            };
            var colContent = new ColumnDefinition ()
            {
                Width = new GridLength ( 40, GridUnitType.Star ),
            };

            _grid.ColumnDefinitions.Add ( colSpacer );
            _grid.ColumnDefinitions.Add ( colContent );
            _grid.ColumnDefinitions.Add ( colSpacer1 );

            var rowspacerOuter = new RowDefinition ()
            {
                Height = new GridLength ( 2, GridUnitType.Star ),
            };
            var rowspacerOuter1 = new RowDefinition ()
            {
                Height = new GridLength ( 2, GridUnitType.Star ),
            };
            var rowspacerInner = new RowDefinition ()
            {
                Height = new GridLength ( 1, GridUnitType.Star ),
            };
            var rowspacerInner1 = new RowDefinition ()
            {
                Height = new GridLength ( 1, GridUnitType.Star ),
            };
            var rowcontent = new RowDefinition ()
            {
                Height = new GridLength ( 10, GridUnitType.Star ),
            };
            var rowcontent1 = new RowDefinition ()
            {
                Height = new GridLength ( 10, GridUnitType.Star ),
            };
            var rowcontent2 = new RowDefinition ()
            {
                Height = new GridLength ( 10, GridUnitType.Star ),
            };
            _grid.RowDefinitions.Add ( rowspacerOuter );
            _grid.RowDefinitions.Add ( rowcontent );
            _grid.RowDefinitions.Add ( rowspacerInner );
            _grid.RowDefinitions.Add ( rowcontent1 );
            _grid.RowDefinitions.Add ( rowspacerInner1 );
            _grid.RowDefinitions.Add ( rowcontent2 );
            _grid.RowDefinitions.Add ( rowspacerOuter1 );

            ContentGrid = _grid;
            CommitContainer.Content = ContentGrid;
        }


        void BuildContent ()
        {
            for ( int flag = 0; flag < 3; flag++ )
            {
                var _textBlock = new TextBlock ()
                {
                    Text = "Play Game   ",
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                //_grid.Children.Add ( _textBlock );
                Grid.SetColumn ( _textBlock, 1 );
                Grid.SetRow ( _textBlock, ( 1 + ( flag * 2 ) ) );
                switch ( flag )
                {
                    case 0:
                        PlayerNameTxtBlk = _textBlock;
                        _textBlock.Background = Brushes.GreenYellow;
                        break;
                    case 1:
                        ActionTxtBlk = _textBlock;
                        _textBlock.FontSize = 28;
                        _textBlock.FontWeight = FontWeights.Bold;
                        break;
                    default:
                        DescriptionTxtBlk = _textBlock;
                        break;
                }
                ContentGrid.Children.Add ( _textBlock );
            }
        }
        #endregion Methods

    }

}
