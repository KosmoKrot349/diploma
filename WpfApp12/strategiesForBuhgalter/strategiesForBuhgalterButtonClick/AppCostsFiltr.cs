using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AppCostsFiltr:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AppCostsFiltr(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.filtr.ApplyRashodyFiltr();
            DataGridUpdater.updateDataGridRashody(windowObj.connectionString, windowObj.filtr.sql, windowObj.RoshodyDataGrid);
        }
    }
}
