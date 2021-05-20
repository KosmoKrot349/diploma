using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class TaxMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public TaxMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.RolesMenu.BorderBrush = null;
            windowObj.ProfitMenu.BorderBrush = null;
            windowObj.CostsMenu.BorderBrush = null;
            windowObj.TaxesMenu.BorderBrush = Brushes.DarkRed;
            windowObj.ReportsMenu.BorderBrush = null;
            windowObj.HideAll();
            windowObj.TaxesGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select * from nalogi";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.FondSotsStrah.Text = reader.GetDouble(0).ToString();
                        windowObj.VoenSbor.Text = reader.GetDouble(1).ToString();
                        windowObj.NDFL.Text = reader.GetDouble(2).ToString();

                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
