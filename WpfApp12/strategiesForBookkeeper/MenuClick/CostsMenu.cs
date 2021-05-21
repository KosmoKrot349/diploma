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
            windowObj.CostsGrid.Visibility = Visibility.Visible;
            windowObj.FilterGridRashody.Children.Clear();
            windowObj.FilterGridRashody.ColumnDefinitions.Clear();
            windowObj.filter.CreateCostsFilter(windowObj.FilterGridRashody);
            windowObj.filter.sql = "SELECT rashody.rashid as rashid, typerash.title as title, sotrudniki.fio as fio, rashody.summ as summ , rashody.data as data, rashody.description as description FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            windowObj.ProfitDataGrid.SelectedItem = null;
            windowObj.DeleteProfit.IsEnabled = false;
            windowObj.GoToChangeProfit.IsEnabled = false;
            DataGridUpdater.updateCostsDataGrid(windowObj);
        }
    }
}
