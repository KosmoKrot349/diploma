using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ScheduleForNewWeek:IButtonClick
    {
        DirectorWindow windowObj;
        object sender;

        public ScheduleForNewWeek(DirectorWindow windowObj, object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
        {
            DateIn wind = new DateIn();
            wind.gridToRas.Visibility = Visibility.Visible;
            wind.ShowDialog();
            DateTime dm = wind.getDm();
            if (dm.Day == 1 && dm.Month == 1 && dm.Year == 1) { return; }
            windowObj.dateMonday = dm;
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
            if (but.Name == "NewRaspBut") windowObj.showRaspG(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
            if (but.Name == "NewRaspButP") windowObj.showRaspP(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
            if (but.Name == "NewRaspButС") windowObj.showRaspС(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
        }
    }
}
