using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class TimeScheduleMenu:IMenuClick
    {
        DirectorWindow windowObj;

        public TimeScheduleMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.zvonkiGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridZvonki(windowObj.connectionString, windowObj.zvonkiDataGrid);
        }
    }
}
