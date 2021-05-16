using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class GoToCashierReport:IButtonClick
    {
        BuhgalterWindow windowObj;

        public GoToCashierReport(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.kassaGrid.Visibility = Visibility.Visible;
            windowObj.fda.CreateKassaDAFiltr(windowObj.pD);
            windowObj.fdb.CreateKassaDBFiltr(windowObj.tD);
            windowObj.fra.CreateKassaRAFiltr(windowObj.pR);
            windowObj.frb.CreateKassaRBFiltr(windowObj.tR);
            windowObj.fda.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            windowObj.fra.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(windowObj.connectionString, windowObj.KassaDodohGrid, windowObj.KassaRashodGrid, windowObj.kassaTitleLabel, windowObj.KassaItogoDohod, windowObj.KassaItogoRashod, windowObj.kassaAllDohodLabel, windowObj.fda.sql, windowObj.fra.sql);
        }
    }
}
