using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ScheduleGroopMenu:IMenuClick
    {
        ManagerWindow windowObj;

        public ScheduleGroopMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            DateTime dateNow = DateTime.Now;
            windowObj.dateMonday = new DateTime();
            int dayDifference = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { dayDifference = 0; } break;
                case "Tuesday": { dayDifference = -1; } break;
                case "Wednesday": { dayDifference = -2; } break;
                case "Thursday": { dayDifference = -3; } break;
                case "Friday": { dayDifference = -4; } break;
                case "Saturday": { dayDifference = -5; } break;
                case "Sunday": { dayDifference = -6; } break;
            }
            windowObj.dateMonday = dateNow.AddDays(dayDifference);
            ShowLearningSchedule.ShowForGroops(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
        }
    }
}
