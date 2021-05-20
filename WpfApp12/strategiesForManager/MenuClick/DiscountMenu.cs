using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class DiscountMenu:IMenuClick
    {
        ManagerWindow windowObj;

        public DiscountMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.DiscountGrid.Visibility = Visibility.Visible;
            windowObj.GoToAdminMenu.BorderBrush = null;
            windowObj.ScheduleMenu.BorderBrush = null;
            windowObj.EmployeesMenu.BorderBrush = null;
            windowObj.DiscountMenu.BorderBrush = Brushes.DarkRed; ;
            windowObj.LearningMenu.BorderBrush = null;
            windowObj.ReportsMenu.BorderBrush = null;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select skidka from skidki order by kol_month";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        switch (i)
                        {
                            case 0: { windowObj.DiscountFor1Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 1: { windowObj.DiscountFor2Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 2: { windowObj.DiscountFor3Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 3: { windowObj.DiscountFor4Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 4: { windowObj.DiscountFor5Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 5: { windowObj.DiscountFor6Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 6: { windowObj.DiscountFor7Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 7: { windowObj.DiscountFor8Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 8: { windowObj.DiscountFor9Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 9: { windowObj.DiscountFor10Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 10: { windowObj.DiscountFor11Month.Text = reader.GetDouble(0).ToString(); break; }
                            case 11: { windowObj.DiscountFor12Month.Text = reader.GetDouble(0).ToString(); break; }

                        }
                        i++;
                    }

                }
                con.Close();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
