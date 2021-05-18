using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class CostsMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public CostsMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.RoshodyGrid.Visibility = Visibility.Visible;
            windowObj.FiltrGridRashody.Children.Clear();
            windowObj.FiltrGridRashody.ColumnDefinitions.Clear();
            windowObj.filter.CreateFiltrRashody(windowObj.FiltrGridRashody);
            windowObj.filter.sql = "SELECT rashody.rashid as rashid, typerash.title as title, sotrudniki.fio as fio, rashody.summ as summ , rashody.data as data, rashody.description as description FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            windowObj.DohodyDataGrid.SelectedItem = null;
            windowObj.DohDeleteButton.IsEnabled = false;
            windowObj.DohChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridRashody(windowObj.connectionString, windowObj.filter.sql, windowObj.RoshodyDataGrid);
        }
    }
}
