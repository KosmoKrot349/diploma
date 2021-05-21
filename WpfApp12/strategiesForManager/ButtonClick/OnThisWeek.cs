using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class OnThisWeek:IButtonClick
    {
        ManagerWindow windowObj;
        object sender;

        public OnThisWeek(ManagerWindow windowObj, object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
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
            Button but = sender as Button;
            if (but.Name == "OnThisWeekGroopSchedule") { ShowLearningSchedule.ShowForGroops(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj); }
            if (but.Name == "OnThisWeekTeacherSchedule") { ShowLearningSchedule.ShowForTeachers(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj); }
            if (but.Name == "OnThisWeekCabinetSchedule") { ShowLearningSchedule.ShowForCabinets(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj); }
        }
    }
}
