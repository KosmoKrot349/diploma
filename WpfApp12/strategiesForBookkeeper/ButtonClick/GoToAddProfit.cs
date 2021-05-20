using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToAddProfit:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToAddProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.ProfitAddSum.Text = "";
            windowObj.ProfitAddDate.Text = DateTime.Now.ToShortDateString();
            windowObj.ProfitAddType.Items.Clear();
            windowObj.ProfitAddPersonNmae.Text = "";
            windowObj.ProfitAddPErsonType.SelectedIndex = 0;


            windowObj.HideAll();
            windowObj.ProfitAddGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.ProfitAddType.Items.Add(reader.GetString(0));
                    }
                    windowObj.ProfitAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
    }
}
