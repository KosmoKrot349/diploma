using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ScheduleOfStaff:IMenuClick
    {
        DirectorWindow window;

        public ScheduleOfStaff(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.ShtatRaspGrid.Visibility = Visibility.Visible;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    window.lbmas_shtatRasp[i, j] = new Label();
                    window.lbmas_shtatRasp[i, j].Content = "";
                    window.lbmas_shtatRasp[i, j].FontSize = 16;
                    window.lbmas_shtatRasp[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    window.lbmas_shtatRasp[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    window.lbmas_shtatRasp[i, j].Name = "name_" + i + "_" + j;
                    window.lbmas_shtatRasp[i, j].BorderThickness = new Thickness(2);
                    window.lbmas_shtatRasp[i, j].BorderBrush = Brushes.Black;
                    window.lbmas_shtatRasp[i, j].MouseDown += window.Label_shtatRasp_MouseDown;
                }

            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(sotrid) from shtat";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.chbxMas_stateRasp = new CheckBox[reader.GetInt32(0)];
                    }
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            DataGridUpdater.updateGridShtatRasp(window.connectionString, window.MonthGrid, window.ShtatRaspSotrpGrid, window.lbmas_shtatRasp, window.chbxMas_stateRasp, window.ShtatRaspMonthYearLabel, window.date);
        }
    }
}
