using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.SelectionChanged
{
    class SelectingGroopArrears:ISelectionChanged
    {
        BookkeeperWindow windowObj;

        public SelectingGroopArrears(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void SelectionChanged()
        {
            windowObj.ListenerDebt.Items.Clear();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select distinct fio from listeners where listenerid in (select listenerid from listdolg inner join groups using(grid) where nazvanie ='" + windowObj.DebtPeymentGroops.SelectedItem + "') order by fio";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        windowObj.ListenerDebt.Items.Add(reader1.GetString(0));
                    }
                    windowObj.ListenerDebt.SelectedIndex = 0;
                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
