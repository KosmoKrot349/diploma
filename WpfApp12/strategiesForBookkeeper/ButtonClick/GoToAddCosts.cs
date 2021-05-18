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
            windowObj.RashodyAddGrid.Visibility = Visibility.Visible;
            windowObj.RashodyAddType.Items.Clear();
            windowObj.RashodyAddFIO.Items.Clear();
            windowObj.RashodyAddSum.Text = "";
            windowObj.RashodyAddDate.Text = DateTime.Now.ToShortDateString();
            windowObj.RashodyAddDesc.Text = "";
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
                        windowObj.RashodyAddType.Items.Add(reader.GetString(0));
                    }
                    windowObj.RashodyAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

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
                        windowObj.RashodyAddFIO.Items.Add(reader.GetString(0));
                    }
                    windowObj.RashodyAddFIO.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
