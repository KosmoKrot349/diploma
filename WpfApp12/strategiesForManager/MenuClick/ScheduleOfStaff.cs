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
        ManagerWindow window;

        public ScheduleOfStaff(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.StaffScheduleGrid.Visibility = Visibility.Visible;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    window.labelArrForStaffSchedule[i, j] = new Label();
                    window.labelArrForStaffSchedule[i, j].Content = "";
                    window.labelArrForStaffSchedule[i, j].FontSize = 16;
                    window.labelArrForStaffSchedule[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    window.labelArrForStaffSchedule[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    window.labelArrForStaffSchedule[i, j].Name = "name_" + i + "_" + j;
                    window.labelArrForStaffSchedule[i, j].BorderThickness = new Thickness(2);
                    window.labelArrForStaffSchedule[i, j].BorderBrush = Brushes.Black;
                    window.labelArrForStaffSchedule[i, j].MouseDown += window.Label_StaffSchedule_MouseDown;
                }

            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(sotrid) from shtat";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.checkBoxArrForStaffSchedule = new CheckBox[reader.GetInt32(0)];
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            DataGridUpdater.updateStaffScheduleGrid(window);
        }
    }
}
