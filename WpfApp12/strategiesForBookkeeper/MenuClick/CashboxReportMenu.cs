using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class CashboxReportMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public CashboxReportMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.CashboxGrid.Visibility = Visibility.Visible;
            windowObj.PeopleFromCashboxFilter.CreateCashboxProfitPersonFilter(windowObj.ProfitPersonFilter);
            windowObj.ProfitTypesFromCashboxFilter.CreateCashboxProfitTypesFilter(windowObj.ProfitTypesFilter);
            windowObj.StaffFromCashboxFiltr.CreateCashboxCostsPersonFilter(windowObj.CostsPersonFilter);
            windowObj.CostsTypesFromCashboxFilter.CreateCashboxCostsTypesFilter(windowObj.CostsTypeFilter);
            windowObj.PeopleFromCashboxFilter.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            windowObj.StaffFromCashboxFiltr.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateCashBoxGrid(windowObj.connectionString, windowObj.CashboxProfitGrid, windowObj.CashboxCostsGrid, windowObj.CashboxTitleLabel, windowObj.CashboxTotalProfit, windowObj.CashboxTotalCosts, windowObj.CashboxProfit, windowObj.PeopleFromCashboxFilter.sql, windowObj.StaffFromCashboxFiltr.sql);
        }
    }
}
