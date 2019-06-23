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

        public VisDie ()
        {
            var _button = new VisDie ()
            {

            };
            BuildControl ();
        }


        // Properties
        // Control handles.
        public Button LeftFace { get; set; }
        public Button RightFace { get; set; }
        public Button TopFace { get; set; }
        public Canvas ContentContainer { get; set; }

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
                LeftFace.Background = value;
                RightFace.Background = value;
                TopFace.Background = value;
            }
        }

        // Methods
        void BuildControl ()
        {
            BuildContainer ();
            BuildContentContainer ();
            BuildContent ();
        }



        void BuildContainer ()
        { }



        void BuildContentContainer ()
        { }



        void BuildContent ()
        { }



    }



}
