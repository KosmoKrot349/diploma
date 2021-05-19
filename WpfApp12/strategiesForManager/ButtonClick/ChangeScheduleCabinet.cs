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
    class ChangeScheduleCabinet:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeScheduleCabinet(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int subjectID = -1;
            int groopID = -1;
            int teacherID = -1;

            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + windowObj.raspChangeSubsK.SelectedItem + "'";
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

            //получение id группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select grid from groups where nazvanie = '" + windowObj.raspChangeGroupK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        groopID = reader.GetInt32(0);
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
                string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + windowObj.raspChangePrepK.SelectedItem + "'";
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

            int cabinetID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int day = 0;

            switch (windowObj.raspChangeDayOfWeekK.Text)
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
                string sql = "UPDATE raspisanie SET subid=" + subjectID + ", grid=" + groopID + ",prepid = " + teacherID + " WHERE cabid=" + cabinetID + " and  lesson_number=" + windowObj.raspChangeLesNumK.Text + " and date='" + windowObj.raspChangeDateK.Text.Replace('.', '-') + "' and day=" + day;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            ShowLearningSchedule.ShowForCabinets(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
        }
    }
}
