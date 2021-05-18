using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddScheduleCabinet:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddScheduleCabinet(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int kab = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            //добавление
            windowObj.raspAddSubsK.Items.Clear();
            windowObj.raspAddPrepK.Items.Clear();
            windowObj.raspAddGroupK.Items.Clear();
            int grid = -1;
            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободных групп"); return; }
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.raspAddGroupK.Items.Add(reader.GetString(0));
                        if (i == 0) { grid = reader.GetInt32(1); i++; }

                    }
                    windowObj.raspAddGroupK.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного преподавателя"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.raspAddPrepK.Items.Add(reader.GetString(0));
                    }
                    windowObj.raspAddPrepK.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { windowObj.raspAddDayOfWeekK.Text = "Понедельник"; } break;
                case 2: { windowObj.raspAddDayOfWeekK.Text = "Вторник"; } break;
                case 3: { windowObj.raspAddDayOfWeekK.Text = "Среда"; } break;
                case 4: { windowObj.raspAddDayOfWeekK.Text = "Четверг"; } break;
                case 5: { windowObj.raspAddDayOfWeekK.Text = "Пятница"; } break;
                case 6: { windowObj.raspAddDayOfWeekK.Text = "Суббота"; } break;
                case 7: { windowObj.raspAddDayOfWeekK.Text = "Воскресенье"; } break;
            }

            windowObj.raspAddDateK.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.raspAddLesNumK.Text = "" + lesNum;
            windowObj.raspAddKabK.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.addRaspGridKab.Visibility = Visibility.Visible;
        }
    }
}
