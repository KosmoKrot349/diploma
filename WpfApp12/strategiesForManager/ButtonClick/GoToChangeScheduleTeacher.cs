using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeScheduleTeacher:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeScheduleTeacher(ManagerWindow windowObj)
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
            windowObj.TeacherScheduleChangeSubject.Items.Clear();
            windowObj.TeacherScheduleChangeCabinet.Items.Clear();
            windowObj.TeacherScheduleChangeGroop.Items.Clear();

            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie from groups where grid not in (select grid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    windowObj.TeacherScheduleChangeGroop.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    windowObj.TeacherScheduleChangeGroop.SelectedIndex = 0;
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    bool b = false;
                    windowObj.TeacherScheduleChangeGroop.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        windowObj.TeacherScheduleChangeGroop.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]) { windowObj.TeacherScheduleChangeGroop.SelectedIndex = i; b = true; }
                    }
                    if (b == false) { windowObj.TeacherScheduleChangeGroop.SelectedIndex = 0; }
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
                if (reader.HasRows == false)
                {
                    windowObj.TeacherScheduleChangeCabinet.SelectedIndex = 0;
                    windowObj.TeacherScheduleChangeCabinet.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.TeacherScheduleChangeCabinet.SelectedIndex = 0;
                    windowObj.TeacherScheduleChangeCabinet.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);

                    while (reader.Read())
                    {
                        windowObj.TeacherScheduleChangeCabinet.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]) { windowObj.TeacherScheduleChangeCabinet.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            switch (day + 1)
            {
                case 1: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.TeacherScheduleChangeDayOfWeek.Text = "Суббота"; } break;
            }
            windowObj.TeacherScheduleChangeDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.TeacherScheduleChangeLessonNumber.Text = "" + lessonNumber;
            windowObj.TeacherScheduleChangeTeacher.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.ChangeTeacherScheduleGrid.Visibility = Visibility.Visible;
        }
    }
}
