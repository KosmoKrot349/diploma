using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToNextMonth:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToNextMonth(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.dateAccrual = windowObj.dateAccrual.AddMonths(1);
            DataGridUpdater.updateGridNachZp(windowObj.connectionString, windowObj.NachMonthLabel, windowObj.checkBoxArrStaffForAccrual, windowObj.NachSotrGrid, windowObj.NachDataGrid, windowObj.dateAccrual);
        }
    }
}
