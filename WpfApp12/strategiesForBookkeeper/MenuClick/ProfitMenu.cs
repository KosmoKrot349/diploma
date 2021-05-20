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
            windowObj.ProfitGrid.Visibility = Visibility.Visible;
            windowObj.FilterGridDohody.Children.Clear();
            windowObj.FilterGridDohody.ColumnDefinitions.Clear();
            windowObj.filter.CreateProfitFilter(windowObj.FilterGridDohody);
            windowObj.filter.sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) ";
            DataGridUpdater.updateProfitDataGrid(windowObj);
        }
    }
}
