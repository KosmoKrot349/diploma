using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddScheduleTeacher:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddScheduleTeacher(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int teacherID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lessonNumber = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            //добавление
            windowObj.TeacherScheduleSelectSubject.Items.Clear();
            windowObj.TeacherScheduleSelectCabinet.Items.Clear();
            windowObj.TeacherScheduleSelectGroop.Items.Clear();
            int grid = -1;
            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободных групп"); return; }
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.TeacherScheduleSelectGroop.Items.Add(reader.GetString(0));
                        if (i == 0) { grid = reader.GetInt32(1); i++; }

                    }
                    windowObj.TeacherScheduleSelectGroop.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime scheduleDay = windowObj.dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + scheduleDay.Day + " and EXTRACT(Month FROM date)=" + scheduleDay.Month + " and EXTRACT(Year FROM date)=" + scheduleDay.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    windowObj.TeacherScheduleSelectCabinet.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        windowObj.TeacherScheduleSelectCabinet.Items.Add(reader.GetString(0));
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { windowObj.TeacherScheduleDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.TeacherScheduleDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.TeacherScheduleDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.TeacherScheduleDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.TeacherScheduleDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.TeacherScheduleDayOfWeek.Text = "Суббота"; } break;
                case 7: { windowObj.TeacherScheduleDayOfWeek.Text = "Воскресенье"; } break;
            }

            windowObj.TeacherScheduleDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.TeacherScheduleSelectLessonNumber.Text = "" + lessonNumber;
            windowObj.TeacherScheduleTeacher.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.AddTeacherScheduleGrid.Visibility = Visibility.Visible;
        }
    }
}
