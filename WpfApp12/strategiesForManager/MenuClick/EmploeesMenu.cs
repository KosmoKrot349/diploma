using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class EmploeesMenu:IMenuClick
    {
        DirectorWindow windowObj;

        public EmploeesMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.allSotrGrid.Visibility = Visibility.Visible;
            windowObj.sqlAllSotr = "SELECT * FROM sotrudniki";
            DataGridUpdater.updateDataGridSotr(windowObj.connectionString, windowObj.sqlAllSotr, windowObj.allSotrDataGrid);
        }
    }
}
