using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.LabelMousDown
{
    class LabelClickFromGroopsSchedule:IMousDown
    {
        DirectorWindow window;
        object sender;

        public LabelClickFromGroopsSchedule(DirectorWindow window, object sender)
        {
            this.window = window;
            this.sender = sender;
        }

        public void MousDown()
        {
            Label l = sender as Label;
            window.iRaspLebale = Convert.ToInt32(l.Name.Split('_')[1]);
            window.jRaspLebale = Convert.ToInt32(l.Name.Split('_')[2]);
            for (int i = 0; i < (window.m * 7) + 1; i++)
            {
                for (int j = 1; j < window.n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.lbmas[i, j].Background = Brushes.White;
                }
            }
            window.lbmas[window.iRaspLebale, window.jRaspLebale].Background = Brushes.Aqua;
            window.DeleteRaspBut.IsEnabled = false;
            window.ChangeRaspBut.IsEnabled = false;
            window.AddRaspBut.IsEnabled = false;
            window.DeleteRaspButС.IsEnabled = false;
            window.ChangeRaspButС.IsEnabled = false;
            window.AddRaspButС.IsEnabled = false;
            window.DeleteRaspButP.IsEnabled = false;
            window.ChangeRaspButP.IsEnabled = false;
            window.AddRaspButP.IsEnabled = false;
            if (window.lbmas[window.iRaspLebale, window.jRaspLebale].Content.ToString() == "") { window.AddRaspBut.IsEnabled = true; window.AddRaspButС.IsEnabled = true; window.AddRaspButP.IsEnabled = true; }
            if (window.lbmas[window.iRaspLebale, window.jRaspLebale].Content.ToString() != "")
            {
                window.DeleteRaspBut.IsEnabled = true;
                window.ChangeRaspBut.IsEnabled = true;

                window.DeleteRaspButС.IsEnabled = true;
                window.ChangeRaspButС.IsEnabled = true;

                window.DeleteRaspButP.IsEnabled = true;
                window.ChangeRaspButP.IsEnabled = true;
            }
        }
    }
}
