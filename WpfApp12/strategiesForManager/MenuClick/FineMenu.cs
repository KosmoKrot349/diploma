using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class FineMenu:IMenuClick
    {
        ManagerWindow window;

        public FineMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select penyaproc from last_pereraschet";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                DateIn wind = new DateIn();
                wind.gridProcentPeni.Visibility = Visibility.Visible;
                wind.constr = window.connectionString;
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wind.PenyaProc.Text = reader.GetDouble(0).ToString();
                    }
                }
                con.Close();
                wind.Show();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
