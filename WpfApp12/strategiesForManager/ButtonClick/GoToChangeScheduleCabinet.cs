using Npgsql;
using System;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeScheduleCabinet:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeScheduleCabinet(ManagerWindow windowObj)
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
            windowObj.raspChangeSubsK.Items.Clear();
            windowObj.raspChangePrepK.Items.Clear();
            windowObj.raspChangeGroupK.Items.Clear();
            int groopId = -1;

            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                DateTime scheduleDay = windowObj.dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lessonNumber + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + scheduleDay.Day + " and EXTRACT(Month FROM date)=" + scheduleDay.Month + " and EXTRACT(Year FROM date)=" + scheduleDay.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    windowObj.raspChangeGroupK.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                    windowObj.raspChangeGroupK.SelectedIndex = 0;
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    bool b = false;
                    windowObj.raspChangeGroupK.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]);
                    while (reader.Read())
                    {
                        windowObj.raspChangeGroupK.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[2]) { windowObj.raspChangeGroupK.SelectedIndex = i; groopId = reader.GetInt32(1); b = true; }
                    }
                    if (b == false) { windowObj.raspChangeGroupK.SelectedIndex = 0; }
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

                if (reader.HasRows == false)
                {
                    windowObj.raspChangePrepK.SelectedIndex = 0;
                    windowObj.raspChangePrepK.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    windowObj.raspChangePrepK.SelectedIndex = 0;
                    windowObj.raspChangePrepK.Items.Add(windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        windowObj.raspChangePrepK.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == windowObj.labelArr[windowObj.iCoordScheduleLabel, windowObj.jCoordScheduleLabel].Content.ToString().Split('\n')[1]) { windowObj.raspChangePrepK.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            switch (day + 1)
            {
                case 1: { windowObj.raspChangeDayOfWeekK.Text = "Понедельник"; } break;
                case 2: { windowObj.raspChangeDayOfWeekK.Text = "Вторник"; } break;
                case 3: { windowObj.raspChangeDayOfWeekK.Text = "Среда"; } break;
                case 4: { windowObj.raspChangeDayOfWeekK.Text = "Четверг"; } break;
                case 5: { windowObj.raspChangeDayOfWeekK.Text = "Пятница"; } break;
                case 6: { windowObj.raspChangeDayOfWeekK.Text = "Суббота"; } break;
            }
            windowObj.raspChangeDateK.Text = windowObj.dateMonday.AddDays(day).ToShortDateString();
            windowObj.raspChangeLesNumK.Text = "" + lessonNumber;
            windowObj.raspChangeKabK.Text = windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Content.ToString();
            windowObj.HideAll();
            windowObj.changeRaspGridKab.Visibility = Visibility.Visible;
        }
    }
}
