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
    class ScheduleForNewWeek:IButtonClick
    {
        ManagerWindow windowObj;
        object sender;

        public ScheduleForNewWeek(ManagerWindow windowObj, object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
        {
            DateIn wind = new DateIn();
            wind.gridToRas.Visibility = Visibility.Visible;
            wind.ShowDialog();
            DateTime dateMonday = wind.getDm();
            if (dateMonday.Day == 1 && dateMonday.Month == 1 && dateMonday.Year == 1) { return; }
            windowObj.dateMonday = dateMonday;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where  date='" + windowObj.dateMonday.ToShortDateString().Replace('.', '-') + "'";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Расписание на эту неделю уже есть"); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            Button but = sender as Button;
            if (but.Name == "ScheduleForNewWeekroopSchedule") ShowLearningSchedule.ShowForGroops(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
            if (but.Name == "ScheduleForNewWeeTeachersSchedule") ShowLearningSchedule.ShowForTeachers(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
            if (but.Name == "ScheduleForNewWeeCabinetsSchedule") ShowLearningSchedule.ShowForCabinets(windowObj.dateMonday, windowObj.dateMonday.AddDays(6),windowObj);
        }
    }
}
