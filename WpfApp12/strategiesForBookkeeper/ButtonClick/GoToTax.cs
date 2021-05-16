using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class GoToTax:IButtonClick
    {
        BuhgalterWindow windowObj;

        public GoToTax(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.MenuRolesB.BorderBrush = null;
            windowObj.Dohody.BorderBrush = null;
            windowObj.Rashody.BorderBrush = null;
            windowObj.Nalogi.BorderBrush = Brushes.DarkRed;
            windowObj.otchetMenu.BorderBrush = null;
            windowObj.HideAll();
            windowObj.NalogiGrid.Visibility = Visibility.Visible;
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
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
