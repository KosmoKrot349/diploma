using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AppProfitFiltr:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AppProfitFiltr(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.filtr.ApplyDohodyFiltr();
            DataGridUpdater.updateDataGridDohody(windowObj.connectionString, windowObj.filtr.sql, windowObj.RoshodyDataGrid);
        }
    }
}
