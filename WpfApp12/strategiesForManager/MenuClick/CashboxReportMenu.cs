using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class CashboxReportMenu:IMenuClick
    {
        ManagerWindow window;

        public CashboxReportMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
           window.HideAll();
            window.CashboxReportGrid.Visibility = Visibility.Visible;
            window.PeopleFromCashboxFilter.CreateCashboxProfitPersonFilter(window.PersonProfitFilterGrid);
            window.ProfitTypesFromCashboxFilter.CreateCashboxProfitTypesFilter(window.TypeProfitFilterGrid);
            window.StaffFromCashboxFiltr.CreateCashboxCostsPersonFilter(window.PersonCostsFilterGrid);
            window.CostsTypesFromCashboxFilter.CreateCashboxCostsTypesFilter(window.TypeCostsFilterGrid);
            window.PeopleFromCashboxFilter.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            window.StaffFromCashboxFiltr.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateCashBoxGrid(window.connectionString, window.CashboxProfitGrid, window.CashboxCostsGrid, window.CashboxTitleLabel, window.CashboxTotalProfit, window.CashboxTotalCosts, window.CashboxFinalProfit, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
        }
    }
}
