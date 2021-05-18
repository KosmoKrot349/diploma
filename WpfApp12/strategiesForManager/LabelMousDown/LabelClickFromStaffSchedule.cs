using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.LabelMousDown
{
    class LabelClickFromStaffSchedule:IMousDown
    {
        ManagerWindow window;
        object sender;

        public LabelClickFromStaffSchedule(ManagerWindow window,object sender)
        {
            this.window = window;
            this.sender = sender;
        }

        public void MousDown()
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    window.labelArrForStaffSchedule[i, j].Background = Brushes.White;
                }
            }

            Label lb = sender as Label;
            int index_i = Convert.ToInt32(lb.Name.Split('_')[1]);
            int index_j = Convert.ToInt32(lb.Name.Split('_')[2]);
            if (index_i != 0 && lb.Content.ToString() != "")
            {
                window.labelArrForStaffSchedule[index_i, index_j].Background = Brushes.Aqua;
                window.ShtatRaspSaveBut.IsEnabled = true;
                for (int j = 0; j < window.checkBoxArrForStaffSchedule.Length; j++)
                {
                    window.checkBoxArrForStaffSchedule[j].IsChecked = false;

                }
                try
                {
                    DateTime dateToSelect = new DateTime(window.date.Year, window.date.Month, Convert.ToInt32(lb.Content.ToString()));
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();
                    string sql = "select array_to_string(shtatid,'_') from shtatrasp where date='" + dateToSelect.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] strSotrid = reader.GetString(0).Split('_');
                            for (int i = 0; i < strSotrid.Length; i++)
                            {
                                for (int j = 0; j < window.checkBoxArrForStaffSchedule.Length; j++)
                                {
                                    if (strSotrid[i] == window.checkBoxArrForStaffSchedule[j].Name.Split('_')[1]) { window.checkBoxArrForStaffSchedule[j].IsChecked = true; }

                                }
                            }
                        }

                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
        }
    }
}
