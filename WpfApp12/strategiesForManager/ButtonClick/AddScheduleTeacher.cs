using Npgsql;
using System;
using System.Windows;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddScheduleTeacher:IButtonClick
    {
        ManagerWindow windowObj;

        public AddScheduleTeacher(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int subjectID = -1;
            int groopID = -1;
            int cabinetID = -1;
            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + windowObj.TeacherScheduleSelectCabinet.SelectedItem + "'";
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
                string sql = "select subid from subject  where title = '" + windowObj.TeacherScheduleSelectSubject.SelectedItem + "'";
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
                string sql = "select grid from groups where nazvanie = '" + windowObj.TeacherScheduleSelectGroop.SelectedItem + "'";
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
            int teacherID = Convert.ToInt32(windowObj.labelArr[0, windowObj.jCoordScheduleLabel].Name.Split('_')[1]);
            int day = 0;

            switch (windowObj.TeacherScheduleDayOfWeek.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //вставка записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO raspisanie(grid, lesson_number, subid, prepid, date, day,cabid) VALUES(" + groopID + ", " + windowObj.TeacherScheduleSelectLessonNumber.Text + ", " + subjectID + ", " + teacherID + ", '" + windowObj.TeacherScheduleDate.Text.Replace('.', '-') + "', " + day + "," + cabinetID + "); ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            ShowLearningSchedule.ShowForTeachers(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
        }
    }
}
