using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class NextMonthStaffSchedule:IButtonClick
    {
        ManagerWindow windowObj;

        public NextMonthStaffSchedule(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.date = windowObj.date.AddMonths(1);
            windowObj.SaveStaffSchedule.IsEnabled = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    windowObj.labelArrForStaffSchedule[i, j].Content = "";

                }
            }
            DataGridUpdater.updateStaffScheduleGrid(windowObj);
        }
    }
}
