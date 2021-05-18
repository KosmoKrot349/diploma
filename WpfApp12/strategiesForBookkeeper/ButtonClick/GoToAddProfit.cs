using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class GoToAddProfit:IButtonClick
    {
        BookkeeperWindow windowObj;

        public GoToAddProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.DohodyAddSum.Text = "";
            windowObj.DohodyAddDate.Text = DateTime.Now.ToShortDateString();
            windowObj.DohodyAddType.Items.Clear();
            windowObj.dohAddKtoVnesTb.Text = "";
            windowObj.dohAddKtoVnesCmF.SelectedIndex = 0;


            windowObj.HideAll();
            windowObj.DohodyrAddGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.DohodyAddType.Items.Add(reader.GetString(0));
                    }
                    windowObj.DohodyAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
    }
}
