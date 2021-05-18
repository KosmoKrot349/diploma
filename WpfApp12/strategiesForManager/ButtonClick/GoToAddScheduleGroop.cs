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
            int grid = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            //добавление
            windowObj.raspAddSubs.Items.Clear();
            windowObj.raspAddPrep.Items.Clear();
            windowObj.raspAddKab.Items.Clear();
            //вывод предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where grid =" + grid + " )  @> ARRAY[subid]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.raspAddSubs.Items.Add(reader.GetString(0));
                    }
                    windowObj.raspAddSubs.SelectedIndex = 0;
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
                        windowObj.raspAddPrep.Items.Add(reader.GetString(0));
                    }
                    windowObj.raspAddPrep.SelectedIndex = 0;
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
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного кабинета"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.raspAddKab.Items.Add(reader.GetString(0));
                    }
                    windowObj.raspAddKab.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            switch (day + 1)
            {
                case 1: { windowObj.raspAddDayOfWeek.Text = "Понедельник"; } break;
                case 2: { windowObj.raspAddDayOfWeek.Text = "Вторник"; } break;
                case 3: { windowObj.raspAddDayOfWeek.Text = "Среда"; } break;
                case 4: { windowObj.raspAddDayOfWeek.Text = "Четверг"; } break;
                case 5: { windowObj.raspAddDayOfWeek.Text = "Пятница"; } break;
                case 6: { windowObj.raspAddDayOfWeek.Text = "Суббота"; } break;
                case 7: { windowObj.raspAddDayOfWeek.Text = "Воскресенье"; } break;
            }

            windowObj.raspAddDate.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.raspAddLesNum.Text = "" + lesNum;
            windowObj.raspAddGr.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.addRaspGrid.Visibility = Visibility.Visible;
        }
    }
}
