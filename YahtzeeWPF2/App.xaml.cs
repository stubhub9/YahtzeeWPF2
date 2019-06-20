using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace YahtzeeWPF2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public enum RowHighlight
        {
            Filled = 0,
            Open,
            Scratch,
            Points,
            BestChoice,
            // What the player is choosing, OR enforced take 5OK or 5Str. 
            Insist
        }

        
    }
}
