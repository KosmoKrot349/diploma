using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class ProfitMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public ProfitMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.DohodyrGrid.Visibility = Visibility.Visible;
            windowObj.FiltrGridDohody.Children.Clear();
            windowObj.FiltrGridDohody.ColumnDefinitions.Clear();
            windowObj.filter.CreateProfitFilter(windowObj.FiltrGridDohody);
            windowObj.filter.sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) ";
            DataGridUpdater.updateProfitDataGrid(windowObj);
        }
    }
}
