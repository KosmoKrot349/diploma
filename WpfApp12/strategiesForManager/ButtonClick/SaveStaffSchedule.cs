using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class SaveStaffSchedule:IButtonClick
    {
        ManagerWindow windowObj;

        public SaveStaffSchedule(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
          
                int day = 0;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (windowObj.labelArrForStaffSchedule[i, j].Background == Brushes.Aqua) { day = Convert.ToInt32(windowObj.labelArrForStaffSchedule[i, j].Content.ToString()); windowObj.labelArrForStaffSchedule[i, j].Background = Brushes.White; }
                    }
                }
                DateTime dateToAdd = new DateTime(windowObj.date.Year, windowObj.date.Month, day);

                bool b = false;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select shraspid from shtatrasp where date= '" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows) b = true;
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                string employeesArr = "'{-1,";
                for (int i = 0; i < windowObj.checkBoxArrForStaffSchedule.Length; i++)
                {
                    if (windowObj.checkBoxArrForStaffSchedule[i].IsChecked == true)
                    {
                        employeesArr += windowObj.checkBoxArrForStaffSchedule[i].Name.Split('_')[1] + ",";
                    }
                }
                employeesArr = employeesArr.Substring(0, employeesArr.Length - 1) + "}'";

                if (b == false)
                {
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "INSERT INTO shtatrasp(shtatid, date)VALUES (" + employeesArr + ", '" + dateToAdd.ToShortDateString().Replace('.', '-') + "')";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                }
                else
                {

                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "update shtatrasp set shtatid=" + employeesArr + " where date ='" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                }
                DataGridUpdater.updateStaffScheduleGrid(windowObj);
            windowObj.SaveStaffSchedule.IsEnabled = false;
         

        }
    }
}
