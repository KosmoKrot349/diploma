using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForBookkeeper.OtherMethods
{
    class updateDefraymentTable
    {
        public static void Update(BookkeeperWindow window, int a)
        {
            if (a == 1)
            {
                int monthQuan = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + window.Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + window.Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payForMonth = reader.GetString(0);
                            string[] payForMonthArr = payForMonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payForMonthArr[i] != "0")
                                {

                                    monthQuan++;

                                }
                            }
                            window.textBoxArrForDefreyment = new TextBox[monthQuan];
                            for (int i = 0; i < monthQuan; i++)
                            {
                                window.textBoxArrForDefreyment[i] = new TextBox();
                                window.textBoxArrForDefreyment[i].PreviewTextInput += window.TextBox_PreviewTextInput;
                            }
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridOpat(window.connectionString, window.MonthOplGrid, window.Groups, window.Listener, window.textBoxArrForDefreyment, window.isClose, window.isStop, window.Closeing, window.Open, window.StopLern, window.RestartLern);
            }


            if (a == 2)
            {

                int monthQuan = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + window.ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + window.GroupsDolg.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payForMonth = reader.GetString(0);
                            string[] payForMonthArr = payForMonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payForMonthArr[i] != "0")
                                {

                                    monthQuan++;

                                }
                            }
                            window.textBoxArrForArrearsDefreyment = new TextBox[monthQuan];
                            for (int i = 0; i < monthQuan; i++)
                            {
                                window.textBoxArrForArrearsDefreyment[i] = new TextBox();
                                window.textBoxArrForArrearsDefreyment[i].PreviewTextInput += window.TextBox_PreviewTextInput;
                            }
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridDolg(window.connectionString, window.MonthOplGridDolg, window.GroupsDolg, window.ListenerDolg, window.textBoxArrForArrearsDefreyment, window.DataPerehoda, window.isStopDolg);
            }

        }
    }
}
