using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class StatisticMenu:IMenuClick
    {
        ManagerWindow window;

        public StatisticMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            DataGridUpdater.updateStatisticGrid(window.connectionString, window.StatisticPlot);
            window.HideAll();
            window.StatisticGrid.Visibility = Visibility.Visible;
        }
    }
}
