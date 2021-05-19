using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToChangeCosts:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToChangeCosts(BookkeeperWindow window)
        {
            this.windowObj = window;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.RoshodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                windowObj.RashodyChangeType.Items.Clear();
                if (reader.HasRows)
                {
                    windowObj.RashodyChangeType.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.RashodyChangeType.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { windowObj.RashodyChangeType.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select fio from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                windowObj.RashodyChangeFIO.Items.Clear();
                if (reader.HasRows)
                {
                    windowObj.RashodyChangeFIO.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.RashodyChangeFIO.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { windowObj.RashodyChangeFIO.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            windowObj.costID = (int)arr[0];
            windowObj.RashodyChangeSum.Text = arr[3].ToString();
            windowObj.RashodyChangeDate.Text = arr[4].ToString().Replace('/', '.');
            windowObj.RashodyChangeDesc.Text = arr[5].ToString();
            windowObj.HideAll();
            windowObj.RashodyChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
