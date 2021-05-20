using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class PaymentMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public PaymentMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.PaymentGrid.Visibility = Visibility.Visible;
            windowObj.Groups.Items.Clear();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select nazvanie from groups where grid in (select distinct grid from listnuch) order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.Groups.Items.Add(reader.GetString(0));
                    }
                    windowObj.Groups.SelectedIndex = 0;
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
