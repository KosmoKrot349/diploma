using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class AccrualsMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public AccrualsMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.GlNachGrid.Visibility = Visibility.Visible;
            windowObj.dateAccrual = DateTime.Now;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select count(sotrid) from sotrudniki where sotrid in (select sotrid from shtat) or sotrid in (select sotrid from prep)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.checkBoxArrStaffForAccrual = new CheckBox[reader.GetInt32(0)];
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            DataGridUpdater.updateGridNachZp(windowObj.connectionString, windowObj.NachMonthLabel, windowObj.checkBoxArrStaffForAccrual, windowObj.NachSotrGrid, windowObj.NachDataGrid, windowObj.dateAccrual);
        }
    }
}
