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
            window.kassaGrid.Visibility = Visibility.Visible;
            window.PeopleFromCashboxFilter.CreateKassaDAFiltr(window.pD);
            window.ProfitTypesFromCashboxFilter.CreateKassaDBFiltr(window.tD);
            window.StaffFromCashboxFiltr.CreateKassaRAFiltr(window.pR);
            window.CostsTypesFromCashboxFilter.CreateKassaRBFiltr(window.tR);
            window.PeopleFromCashboxFilter.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            window.StaffFromCashboxFiltr.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(window.connectionString, window.KassaDodohGrid, window.KassaRashodGrid, window.kassaTitleLabel, window.KassaItogoDohod, window.KassaItogoRashod, window.kassaAllDohodLabel, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
        }
    }
}
