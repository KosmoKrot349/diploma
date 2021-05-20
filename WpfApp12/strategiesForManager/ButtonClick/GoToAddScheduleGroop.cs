using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddScheduleGroop:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddScheduleGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int groopId = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lessonNumber = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            //добавление
            windowObj.GroopScheduleSubjects.Items.Clear();
            windowObj.GroopScheduleTeacher.Items.Clear();
            windowObj.GroopScheduleCabinet.Items.Clear();
            //вывод предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where grid =" + groopId + " )  @> ARRAY[subid]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.GroopScheduleSubjects.Items.Add(reader.GetString(0));
                    }
                    windowObj.GroopScheduleSubjects.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime scheduleDay = windowObj.dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + scheduleDay.Day + " and EXTRACT(Month FROM date)=" + scheduleDay.Month + " and EXTRACT(Year FROM date)=" + scheduleDay.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного преподавателя"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.GroopScheduleTeacher.Items.Add(reader.GetString(0));
                    }
                    windowObj.GroopScheduleTeacher.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного кабинета"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.GroopScheduleCabinet.Items.Add(reader.GetString(0));
                    }
                    windowObj.GroopScheduleCabinet.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            switch (day + 1)
            {
                case 1: { windowObj.GroopScheduleDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.GroopScheduleDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.GroopScheduleDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.GroopScheduleDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.GroopScheduleDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.GroopScheduleDayOfWeek.Text = "Суббота"; } break;
                case 7: { windowObj.GroopScheduleDayOfWeek.Text = "Воскресенье"; } break;
            }

            windowObj.GroopScheduleDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.GroopScheduleLessonNumber.Text = "" + lessonNumber;
            windowObj.GroopScheduleGroop.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.AddGroopScheduleGrid.Visibility = Visibility.Visible;
        }
    }
}
