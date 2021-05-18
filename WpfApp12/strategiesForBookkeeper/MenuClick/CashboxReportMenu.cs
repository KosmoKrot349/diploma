using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            windowObj.kassaGrid.Visibility = Visibility.Visible;
            windowObj.PeopleFromCashboxFilter.CreateKassaDAFiltr(windowObj.pD);
            windowObj.ProfitTypesFromCashboxFilter.CreateKassaDBFiltr(windowObj.tD);
            windowObj.StaffFromCashboxFiltr.CreateKassaRAFiltr(windowObj.pR);
            windowObj.CostsTypesFromCashboxFilter.CreateKassaRBFiltr(windowObj.tR);
            windowObj.PeopleFromCashboxFilter.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            windowObj.StaffFromCashboxFiltr.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(windowObj.connectionString, windowObj.KassaDodohGrid, windowObj.KassaRashodGrid, windowObj.kassaTitleLabel, windowObj.KassaItogoDohod, windowObj.KassaItogoRashod, windowObj.kassaAllDohodLabel, windowObj.PeopleFromCashboxFilter.sql, windowObj.StaffFromCashboxFiltr.sql);
        }
    }
}
