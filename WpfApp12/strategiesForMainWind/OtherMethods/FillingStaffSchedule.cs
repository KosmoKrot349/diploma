using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.OtherMethods
{
    class FillingStaffSchedule
    {
        public static void Fill(MainWindow window)
        {
            DateTime DateForChangeStaffSchedule = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime DateForCheckStaffSchedule = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            bool b = false;

            while (DateForCheckStaffSchedule.Month == DateTime.Now.Month)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();

                    string sql = "select shraspid from shtatrasp where date='" + DateForCheckStaffSchedule.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows == false)
                    {
                        b = true;
                        break;
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                DateForCheckStaffSchedule = DateForCheckStaffSchedule.AddDays(1);
            }


            if (b == true)
            {
                MessageBoxResult res2 = MessageBox.Show("Есть возможность автоматически заполнить\nштатное расписание для этого месяца. Заполнить?", "Предупреждение", MessageBoxButton.YesNo);

                if (res2 == MessageBoxResult.Yes)
                {
                    while (DateForChangeStaffSchedule.Month == DateTime.Now.Month)
                    {
                        try
                        {
                            NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                            con.Open();

                            string sql = "select shraspid from shtatrasp where date='" + DateForChangeStaffSchedule.ToShortDateString().Replace('.', '-') + "'";
                            NpgsqlCommand com = new NpgsqlCommand(sql, con);
                            NpgsqlDataReader reader = com.ExecuteReader();
                            if (reader.HasRows == false)
                            {
                                string staffIDStringArr = "'{-1,";
                                //получение всех id штатных сотрудников
                                try
                                {
                                    NpgsqlConnection con2 = new NpgsqlConnection(window.connectionString);
                                    con2.Open();

                                    string sql2 = "select sotrid from shtat";
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                    NpgsqlDataReader reader2 = com2.ExecuteReader();
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            staffIDStringArr += reader2.GetInt32(0) + ",";
                                        }
                                    }
                                    con2.Close();

                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                staffIDStringArr = staffIDStringArr.Substring(0, staffIDStringArr.Length - 1) + "}'";

                                //добавление записи в штатное расписание
                                try
                                {
                                    NpgsqlConnection con2 = new NpgsqlConnection(window.connectionString);
                                    con2.Open();

                                    string sql2 = "INSERT INTO shtatrasp(shtatid, date) VALUES (" + staffIDStringArr + ", '" + DateForChangeStaffSchedule.ToShortDateString().Replace('.', '-') + "')";
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                    NpgsqlDataReader reader2 = com2.ExecuteReader();
                                    con2.Close();

                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                            }
                            con.Close();

                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        DateForChangeStaffSchedule = DateForChangeStaffSchedule.AddDays(1);
                    }
                }
            }

        }
    }
}
