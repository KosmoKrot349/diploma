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
            windowObj.skidkiGrid.Visibility = Visibility.Visible;
            windowObj.MenuRolesD.BorderBrush = null;
            windowObj.raspMenu.BorderBrush = null;
            windowObj.sotrMenu.BorderBrush = null;
            windowObj.skidki.BorderBrush = Brushes.DarkRed; ;
            windowObj.obuchMenu.BorderBrush = null;
            windowObj.MenuOtchety.BorderBrush = null;
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
                            case 0: { windowObj.sk1.Text = reader.GetDouble(0).ToString(); break; }
                            case 1: { windowObj.sk2.Text = reader.GetDouble(0).ToString(); break; }
                            case 2: { windowObj.sk3.Text = reader.GetDouble(0).ToString(); break; }
                            case 3: { windowObj.sk4.Text = reader.GetDouble(0).ToString(); break; }
                            case 4: { windowObj.sk5.Text = reader.GetDouble(0).ToString(); break; }
                            case 5: { windowObj.sk6.Text = reader.GetDouble(0).ToString(); break; }
                            case 6: { windowObj.sk7.Text = reader.GetDouble(0).ToString(); break; }
                            case 7: { windowObj.sk8.Text = reader.GetDouble(0).ToString(); break; }
                            case 8: { windowObj.sk9.Text = reader.GetDouble(0).ToString(); break; }
                            case 9: { windowObj.sk10.Text = reader.GetDouble(0).ToString(); break; }
                            case 10: { windowObj.sk11.Text = reader.GetDouble(0).ToString(); break; }
                            case 11: { windowObj.sk12.Text = reader.GetDouble(0).ToString(); break; }

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
