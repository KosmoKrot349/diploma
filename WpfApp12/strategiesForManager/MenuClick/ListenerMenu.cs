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
            windowObj.FilterGridGroups.Children.Clear();
            windowObj.FilterGridGroups.ColumnDefinitions.Clear();
            windowObj.filter.CreateListenersFilter(windowObj.FilterGridGroups);
            windowObj.filter.sql = "SELECT listenerid,  fio,  phones, comment,array_length(grid, 1) as grid FROM listeners order by listenerid";
            DataGridUpdater.updateListenerDataGrid(windowObj);
        }
    }
}
