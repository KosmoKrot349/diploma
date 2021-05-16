using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class GoToStatsReport:IButtonClick
    {
        BuhgalterWindow windowObj;

        public GoToStatsReport(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataGridUpdater.updateGridStatistica(windowObj.connectionString, windowObj.statGraf);
            windowObj.HideAll();
            windowObj.StatisticaGrid.Visibility = Visibility.Visible;
        }
    }
}
