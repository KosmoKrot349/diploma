using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp12.strategiesForManager.OtherMethods;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class PreviouslyWeek:IButtonClick
    {
        ManagerWindow windowObj;
        object sender;

        public PreviouslyWeek(ManagerWindow windowObj,object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where day = 1 and date<'" + windowObj.dateMonday.ToShortDateString().Replace('.', '-') + "' order by  date desc limit 1";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("Старее расписания нет"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.dateMonday = reader.GetDateTime(0);
                    }
                }

                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.GroopsScheduleDateLabel.Content = "Расписание на " + windowObj.dateMonday.ToShortDateString() + " - " + windowObj.dateMonday.AddDays(6).ToShortDateString();
            Button but = sender as Button;
            if (but.Name == "PreviouslyWeekGroopSchedule") ShowLearningSchedule.ShowForGroops(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
            if (but.Name == "PreviouslyWeekTeacherSchedule") ShowLearningSchedule.ShowForTeachers(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
            if (but.Name == "PreviouslyWeekCabinetSchedule") ShowLearningSchedule.ShowForCabinets(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);

            for (int i = 0; i < (windowObj.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < windowObj.quanGroops + 2; j++)
                {
                    if (i != 0 && j != 1)
                        windowObj.labelArr[i, j].MouseDown += windowObj.Label_MouseDown;
                }

            }
        }
    }
}
