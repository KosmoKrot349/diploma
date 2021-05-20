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
            int cabinetID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lessonNumber = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            //добавление
            windowObj.CabinetScheduleSubject.Items.Clear();
            windowObj.CabinetScheduleTeacher.Items.Clear();
            windowObj.CabinetScheduleGroop.Items.Clear();
            int groopID = -1;
            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime ScheduleDay = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + ScheduleDay.Day + " and EXTRACT(Month FROM date)=" + ScheduleDay.Month + " and EXTRACT(Year FROM date)=" + ScheduleDay.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободных групп"); return; }
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.CabinetScheduleGroop.Items.Add(reader.GetString(0));
                        if (i == 0) { groopID = reader.GetInt32(1); i++; }

                    }
                    windowObj.CabinetScheduleGroop.SelectedIndex = 0;
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
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного преподавателя"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.CabinetScheduleTeacher.Items.Add(reader.GetString(0));
                    }
                    windowObj.CabinetScheduleTeacher.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Суббота"; } break;
                case 7: { windowObj.CabinetScheduleSelectDayOfWeek.Text = "Воскресенье"; } break;
            }

            windowObj.CabinetScheduleDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.CabinetScheduleLessonNumber.Text = "" + lessonNumber;
            windowObj.CabinetScheduleCabinet.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.AddCabinetScheduleGrdi.Visibility = Visibility.Visible;
        }
    }
}
