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
            DataRowView DRV = windowObj.CostsDataGrid.SelectedItem as DataRowView;
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
                windowObj.CostsChangeType.Items.Clear();
                if (reader.HasRows)
                {
                    windowObj.CostsChangeType.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.CostsChangeType.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { windowObj.CostsChangeType.SelectedIndex = i; }
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
                windowObj.CostsChangePersonName.Items.Clear();
                if (reader.HasRows)
                {
                    windowObj.CostsChangePersonName.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.CostsChangePersonName.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { windowObj.CostsChangePersonName.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            windowObj.costID = (int)arr[0];
            windowObj.CostsChangeSum.Text = arr[3].ToString();
            windowObj.CostsChangeDate.Text = arr[4].ToString().Replace('/', '.');
            windowObj.CostsChangeComment.Text = arr[5].ToString();
            windowObj.HideAll();
            windowObj.CostsChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
