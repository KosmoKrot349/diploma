using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ListenerMenu:IMenuClick
    {
        ManagerWindow windowObj;

        public ListenerMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.ListenerGrid.Visibility = Visibility.Visible;
            windowObj.FiltrGridGroups.Children.Clear();
            windowObj.FiltrGridGroups.ColumnDefinitions.Clear();
            windowObj.filter.CreateListenersFilter(windowObj.FiltrGridGroups);
            windowObj.filter.sql = "SELECT listenerid,  fio,  phones, comment,array_length(grid, 1) as grid FROM listeners order by listenerid";
            DataGridUpdater.updateListenerDataGrid(windowObj);
        }
    }
}
