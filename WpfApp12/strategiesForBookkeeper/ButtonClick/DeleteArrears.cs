using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class DeleteArrears:IButtonClick
    {
        BookkeeperWindow windowObj;

        public DeleteArrears(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            MessageBoxResult res = MessageBox.Show("Вы согласны удалить эту запись навсегда?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from listdolg where grid in (select grid from groups where nazvanie ='" + windowObj.DebtPeymentGroops.SelectedItem + "') and listenerid in (select listenerid from listeners where fio ='" + windowObj.ListenerDebt.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                windowObj.DebtPeymentGroops.Items.Clear();
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select nazvanie from groups where grid in (select distinct grid from listdolg) order by grid";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            windowObj.DebtPeymentGroops.Items.Add(reader.GetString(0));
                        }
                        windowObj.DebtPeymentGroops.SelectedIndex = 0;
                    }
                    if (reader.HasRows == false)
                    {
                        windowObj.HideAll();
                        windowObj.LearningAccrualsPaymentGrdi.Visibility = Visibility.Visible;
                    }
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }
        }
    }
}
