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
        DirectorWindow window;

        public CashboxReportMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
           window.HideAll();
            window.kassaGrid.Visibility = Visibility.Visible;
            window.fda.CreateKassaDAFiltr(window.pD);
            window.fdb.CreateKassaDBFiltr(window.tD);
            window.fra.CreateKassaRAFiltr(window.pR);
            window.frb.CreateKassaRBFiltr(window.tR);
            window.fda.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            window.fra.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(window.connectionString, window.KassaDodohGrid, window.KassaRashodGrid, window.kassaTitleLabel, window.KassaItogoDohod, window.KassaItogoRashod, window.kassaAllDohodLabel, window.fda.sql, window.fra.sql);
        }
    }
}
