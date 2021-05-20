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
    class GoToChangeProfit:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToChangeProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ProfitDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.ProfitChangeSumm.Text = arr[2].ToString();
            windowObj.ProfitChangeDate.Text = arr[3].ToString().Replace('/', '.');
            windowObj.profitID = (int)arr[0];
            windowObj.ProfitChangeType.Items.Clear();

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int ii = 0;
                if (reader.HasRows)
                {
                    windowObj.ProfitChangeType.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        windowObj.ProfitChangeType.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { windowObj.ProfitChangeType.SelectedIndex = ii; }
                        ii++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }



            windowObj.personForProfit = arr[4].ToString();
            bool a = false, b = false;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select fio from listeners where fio = '" + windowObj.personForProfit + "'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    windowObj.ProfitChangePersonType.SelectedIndex = 1;
                    a = true;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select fio from sotrudniki where fio='" + windowObj.personForProfit + "'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    windowObj.ProfitChangePersonType.SelectedIndex = 0;
                    b = true;


                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (a == false && b == false) { windowObj.ProfitChangePersonType.SelectedIndex = 2; }
            windowObj.ProfitChangePersonName.Text = arr[4].ToString();
            windowObj.HideAll();
            windowObj.ProfitChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
