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
            int prep = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(windowObj.labelArr[windowObj.iCoordScheduleLabel, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.quanLessonsInDay < windowObj.iCoordScheduleLabel) { day++; } else break;
            }
            windowObj.raspChangeSubsP.Items.Clear();
            windowObj.raspChangeKabP.Items.Clear();
            windowObj.raspChangeGroupP.Items.Clear();

            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime dayRasp = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    windowObj.raspChangeGroupP.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    windowObj.raspChangeGroupP.SelectedIndex = 0;
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    bool b = false;
                    windowObj.raspChangeGroupP.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        windowObj.raspChangeGroupP.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]) { windowObj.raspChangeGroupP.SelectedIndex = i; b = true; }
                    }
                    if (b == false) { windowObj.raspChangeGroupP.SelectedIndex = 0; }
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
                if (reader.HasRows == false)
                {
                    windowObj.raspChangeKabP.SelectedIndex = 0;
                    windowObj.raspChangeKabP.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.raspChangeKabP.SelectedIndex = 0;
                    windowObj.raspChangeKabP.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);

                    while (reader.Read())
                    {
                        windowObj.raspChangeKabP.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]) { windowObj.raspChangeKabP.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            switch (day + 1)
            {
                case 1: { windowObj.raspChangeDayOfWeekP.Text = "Понедельник"; } break;
                case 2: { windowObj.raspChangeDayOfWeekP.Text = "Вторник"; } break;
                case 3: { windowObj.raspChangeDayOfWeekP.Text = "Среда"; } break;
                case 4: { windowObj.raspChangeDayOfWeekP.Text = "Четверг"; } break;
                case 5: { windowObj.raspChangeDayOfWeekP.Text = "Пятница"; } break;
                case 6: { windowObj.raspChangeDayOfWeekP.Text = "Суббота"; } break;
            }
            windowObj.raspChangeDateP.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.raspChangeLesNumP.Text = "" + lesNum;
            windowObj.raspChangePrepP.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.changeRaspGridPrep.Visibility = Visibility.Visible;
        }
    }
}
