using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeScheduleGroops:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeScheduleGroops(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int subjectID = -1;
            int teacherID = -1;
            int cabinetID = -1;

            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + windowObj.raspChangeKab.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cabinetID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + windowObj.raspChangeSubs.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subjectID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id препода
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + windowObj.raspChangePrep.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        teacherID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int groopID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int day = 0;

            switch (windowObj.raspChangeDayOfWeek.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //обновление записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE raspisanie SET subid=" + subjectID + ", prepid=" + teacherID + ",cabid = " + cabinetID + " WHERE grid=" + groopID + " and  lesson_number=" + windowObj.raspChangeLesNum.Text + " and date='" + windowObj.raspChangeDate.Text.Replace('.', '-') + "' and day=" + day;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            ShowLearningSchedule.ShowForGroops(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
        }
    }
}
