using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class GoToPreviouslyMonth:IButtonClick
    {
        BuhgalterWindow windowObj;

        public GoToPreviouslyMonth(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.dateNuch = windowObj.dateNuch.AddMonths(-1);
            DataGridUpdater.updateGridNachZp(windowObj.connectionString, windowObj.NachMonthLabel, windowObj.ChbxMas_SotrNuch, windowObj.NachSotrGrid, windowObj.NachDataGrid, windowObj.dateNuch);
        }
    }
}
