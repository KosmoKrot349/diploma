using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeScheduleGroop:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeScheduleGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int groopID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lessonNumber = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            windowObj.raspChangeSubs.Items.Clear();
            windowObj.raspChangePrep.Items.Clear();
            windowObj.raspChangeKab.Items.Clear();
            //вывод предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where grid =" + groopID + " )  @> ARRAY[subid]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.raspChangeSubs.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        windowObj.raspChangeSubs.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[0]) { windowObj.raspChangeSubs.SelectedIndex = i; }
                        i++;
                    }

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

                if (reader.HasRows == false)
                {
                    windowObj.raspChangePrep.SelectedIndex = 0;
                    windowObj.raspChangePrep.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.raspChangePrep.SelectedIndex = 0;
                    windowObj.raspChangePrep.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        windowObj.raspChangePrep.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]) { windowObj.raspChangePrep.SelectedIndex = i; }
                        i++;
                    }

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
                if (reader.HasRows == false)
                {
                    windowObj.raspChangeKab.SelectedIndex = 0;
                    windowObj.raspChangeKab.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.raspChangeKab.SelectedIndex = 0;
                    windowObj.raspChangeKab.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                    while (reader.Read())
                    {
                        windowObj.raspChangeKab.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]) { windowObj.raspChangeKab.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { windowObj.raspChangeDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.raspChangeDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.raspChangeDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.raspChangeDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.raspChangeDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.raspChangeDayOfWeek.Text = "Суббота"; } break;
                case 7: { windowObj.raspChangeDayOfWeek.Text = "Воскресенье"; } break;
            }
            windowObj.raspChangeDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.raspChangeLesNum.Text = "" + lessonNumber;
            windowObj.raspChangeGr.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.changeRaspGrid.Visibility = Visibility.Visible;
        }
    }
}
