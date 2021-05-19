using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ScheduleOfLessonCabinet:IMenuClick
    {
        ManagerWindow window;

        public ScheduleOfLessonCabinet(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            DateTime dateNow = DateTime.Now;
            window.dateMonday = new DateTime();
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
            window.dateMonday = dateNow.AddDays(dayDifference);
            ShowLearningSchedule.ShowForCabinets(window.dateMonday, window.dateMonday.AddDays(6),window);
        }
    }
}
