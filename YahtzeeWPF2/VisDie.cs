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
    /// The plan is:  VisDie as the control's container.
    /// </summary>
    public class VisDie : Button
    {
        // Constructor

        // Do I need to use "this." ???
        public VisDie ()
            : base ()
        {
            Background = Brushes.Transparent;
            BorderBrush = Brushes.Black;
            BorderThickness = new Thickness ( 1 );
            Content = GetContent ();
            FontSize = 22;
            Height = 155;
            Width = 105;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalContentAlignment = HorizontalAlignment.Stretch;
            VerticalContentAlignment = VerticalAlignment.Stretch;
        }


        #region Properties
        // Properties
        // Control handles.
        Button LeftFace { get; set; }
        Button RightFace { get; set; }
        Button TopFace { get; set; }
        //public Canvas ContentContainer { get; set; }

        // Visual Setters
        public string DieValue
        {
            set
            {
                TopFace.Content = value;
            }
        }
        public string LeftValue
        {
            set
            {
                LeftFace.Content = value;
            }
        }
        public string RightValue
        {
            set
            {
                RightFace.Content = value;
            }
        }

        public Brush BorderHighlight
        {
            set
            {
                LeftFace.BorderBrush = value;
                RightFace.BorderBrush = value;
                TopFace.BorderBrush = value;
            }
        }

        public Point TopLeft
        {
            get; set;
            //set TopLeft => value;
            //set
            //{
                //Canvas.SetLeft = value.X;
                //    // Do I want margin for a canvas.
                //    //this.Margin = new Thickness ( value.X, value.Y, 0.0, 0.0 );
            //}
        }

        #endregion  Properties


        #region Methods
        Canvas GetContent ()
        {
            var _canvas = new Canvas ()
            {
                Height = 148,
                Width = 97,
            };
            GetContents ( ref _canvas );
            return _canvas;
        }


        void GetContents ( ref Canvas canvas )
        {
            for ( int flag = 1; flag < 4; flag++ )
            {
                var _dieFace = new Button
                {
                    BorderThickness = new Thickness ( 4 ),
                    Background = ( flag == 1 ) ? Brushes.LightGoldenrodYellow : Brushes.DarkGray,
                    FontWeight = ( flag == 1 ) ? FontWeights.ExtraBold : FontWeights.Medium,
                    Margin = GetMarginThickness ( flag ),
                    Height = 50,
                    Width = 50,
                    RenderTransform = GetTransformGroup ( flag ),
                };
                canvas.Children.Add ( _dieFace );

                switch ( flag )
                {
                    case 1:
                        TopFace = _dieFace;
                        break;
                    case 2:
                        LeftFace = _dieFace;
                        break;
                    default:
                        RightFace = _dieFace;
                        break;
                }
            }
        }


        Thickness GetMarginThickness ( int face )
        {
            var _thickness = new Thickness ();
            // Where face is 1, 2, or 3.
            switch ( face )
            {
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


        TransformGroup GetTransformGroup ( int face )
        {
            var _transformGroup = new TransformGroup ();
            // Where face is 1, 2, or 3.
            switch ( face )
            {
                // Top face
                case 1:
                    _transformGroup.Children.Add ( new RotateTransform ( 45.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( 1.45, 1.45 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( 0, 0 ) );
                    break;
                // Left face
                case 2:
                    _transformGroup.Children.Add ( new RotateTransform ( 100.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( 10.0, 40.0 ) );
                    break;
                // Right face
                default:
                    _transformGroup.Children.Add ( new RotateTransform ( -101.0 ) );
                    _transformGroup.Children.Add ( new ScaleTransform ( .99, .99 ) );
                    _transformGroup.Children.Add ( new SkewTransform ( -11.0, -39.0 ) );
                    break;
            }
            return _transformGroup;

        }
        #endregion Methods


    }



}
