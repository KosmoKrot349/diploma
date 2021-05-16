using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class PrevMonthStaffSchedule:IButtonClick
    {
        DirectorWindow windowObj;

        public PrevMonthStaffSchedule(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.date = windowObj.date.AddMonths(-1);
            windowObj.ShtatRaspSaveBut.IsEnabled = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    windowObj.lbmas_shtatRasp[i, j].Content = "";

                }
            }

            DataGridUpdater.updateGridShtatRasp(windowObj.connectionString, windowObj.MonthGrid, windowObj.ShtatRaspSotrpGrid, windowObj.lbmas_shtatRasp, windowObj.chbxMas_stateRasp, windowObj.ShtatRaspMonthYearLabel, windowObj.date);
        }
    }
}
