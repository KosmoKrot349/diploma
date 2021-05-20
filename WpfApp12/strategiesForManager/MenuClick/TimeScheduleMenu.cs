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
        ManagerWindow windowObj;

        public TimeScheduleMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.TimeScheduleGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateTimeScheduleDataGrid(windowObj);
        }
    }
}
