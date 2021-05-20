using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class ArrearsMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public ArrearsMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.DebtPaymentGrid.Visibility = Visibility.Visible;

            windowObj.DebtPeymentGroops.Items.Clear();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select nazvanie from groups where grid in (select distinct grid from listdolg) order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.DebtPeymentGroops.Items.Add(reader.GetString(0));
                    }
                    windowObj.DebtPeymentGroops.SelectedIndex = 0;
                }
                if (reader.HasRows == false)
                {
                    windowObj.HideAll();
                    windowObj.LearningAccrualsPaymentGrdi.Visibility = Visibility.Visible;
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
