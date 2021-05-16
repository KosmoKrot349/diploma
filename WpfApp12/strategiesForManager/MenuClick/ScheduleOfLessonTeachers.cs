using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ScheduleOfLessonTeachers:IMenuClick
    {
        DirectorWindow window;

        public ScheduleOfLessonTeachers(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            DateTime dateNow = DateTime.Now;
            window.dateMonday = new DateTime();
            int day_razn = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { day_razn = 0; } break;
                case "Tuesday": { day_razn = -1; } break;
                case "Wednesday": { day_razn = -2; } break;
                case "Thursday": { day_razn = -3; } break;
                case "Friday": { day_razn = -4; } break;
                case "Saturday": { day_razn = -5; } break;
                case "Sunday": { day_razn = -6; } break;
            }
            window.dateMonday = dateNow.AddDays(day_razn);
            ShowLearningSchedule.ShowForTeachers(window.dateMonday, window.dateMonday.AddDays(6),window);
        }
    }
}
