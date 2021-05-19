using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToPreviouslyMonth:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToPreviouslyMonth(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.dateAccrual = windowObj.dateAccrual.AddMonths(-1);
            DataGridUpdater.updateAccrualsSalaryDataGrid(windowObj);
        }
    }
}
