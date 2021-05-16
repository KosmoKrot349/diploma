using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AppCashierCostsFiltr:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AppCashierCostsFiltr(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.fra.ApplyRashFiltr(windowObj.frb);
            DataGridUpdater.updateGridKassa(windowObj.connectionString, windowObj.KassaDodohGrid, windowObj.KassaRashodGrid, windowObj.kassaTitleLabel, windowObj.KassaItogoDohod, windowObj.KassaItogoRashod, windowObj.kassaAllDohodLabel, windowObj.fda.sql, windowObj.fra.sql);
        }
    }
}
