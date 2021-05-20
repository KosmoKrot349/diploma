using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToAddCosts:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToAddCosts(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.CostsAddGrid.Visibility = Visibility.Visible;
            windowObj.CostsAddType.Items.Clear();
            windowObj.CostsAddPersonName.Items.Clear();
            windowObj.CostsAddSum.Text = "";
            windowObj.CostsAddDate.Text = DateTime.Now.ToShortDateString();
            windowObj.CostsAddDesc.Text = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.CostsAddType.Items.Add(reader.GetString(0));
                    }
                    windowObj.CostsAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select fio from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.CostsAddPersonName.Items.Add(reader.GetString(0));
                    }
                    windowObj.CostsAddPersonName.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
