using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class StaffMenu:IMenuClick
    {
        ManagerWindow window;

        public StaffMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.StaffGrid.Visibility = Visibility.Visible;
            window.StaffFilterCMBX.SelectedIndex = 0;
            window.filter.sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateStaffDataGrid(window);
        }
    }
}
