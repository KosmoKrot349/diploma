﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class OnThisWeek:IButtonClick
    {
        DirectorWindow windowObj;
        object sender;

        public OnThisWeek(DirectorWindow windowObj, object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
        {
            DateTime dateNow = DateTime.Now;
            windowObj.dateMonday = new DateTime();
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

            windowObj.dateMonday = dateNow.AddDays(day_razn);
            Button but = sender as Button;
            if (but.Name == "NuwRaspBut") { windowObj.showRaspG(windowObj.dateMonday, windowObj.dateMonday.AddDays(6)); }
            if (but.Name == "NuwRaspButP") { windowObj.showRaspP(windowObj.dateMonday, windowObj.dateMonday.AddDays(6)); }
            if (but.Name == "NuwRaspButС") { windowObj.showRaspС(windowObj.dateMonday, windowObj.dateMonday.AddDays(6)); }
        }
    }
}