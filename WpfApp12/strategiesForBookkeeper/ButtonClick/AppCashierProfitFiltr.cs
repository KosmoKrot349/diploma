using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AppCashierProfitFiltr:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AppCashierProfitFiltr(BuhgalterWindow window)
        {
            this.windowObj = window;
        }

        public void ButtonClick()
        {
            windowObj.fda.ApplyDohFiltr(windowObj.fdb);
            DataGridUpdater.updateGridKassa(windowObj.connectionString, windowObj.KassaDodohGrid, windowObj.KassaRashodGrid, windowObj.kassaTitleLabel, windowObj.KassaItogoDohod, windowObj.KassaItogoRashod, windowObj.kassaAllDohodLabel, windowObj.fda.sql, windowObj.fra.sql);
        }
    }
}
