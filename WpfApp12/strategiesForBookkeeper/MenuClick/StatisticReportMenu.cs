using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class StatisticReportMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public StatisticReportMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            DataGridUpdater.updateGridStatistica(windowObj.connectionString, windowObj.statGraf);
            windowObj.HideAll();
            windowObj.StatisticaGrid.Visibility = Visibility.Visible;
        }
    }
}
