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
    public class VisDice
    {
        public VisDice ()
        {
            Die0 = new VisDie ();
            Die1 = new VisDie ();
            Die2 = new VisDie ();
            Die3 = new VisDie ();
            Die4 = new VisDie ();
        }



        #region Properties
        public Brush BorderHighlights
        {
            set
            {
                Die0.BorderHighlight = value;
                Die1.BorderHighlight = value;
                Die2.BorderHighlight = value;
                Die3.BorderHighlight = value;
                Die4.BorderHighlight = value;
            }
        }


        public VisDie Die0
        {
            get;
            set;
        }

        public VisDie Die1
        {
            get;
            set;
        }

        public VisDie Die2
        {
            get;
            set;
        }

        public VisDie Die3
        {
            get;
            set;
        }

        public VisDie Die4
        {
            get;
            set;
        }


        #endregion Properties

        public void MoveThisOrThat ()
        {
            // Probably return destinations for animations.
        }










    }



}
