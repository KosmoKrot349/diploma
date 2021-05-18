using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.strategiesForBookkeeper.OtherMethods;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class UnClosePaymentEntry:IButtonClick
    {
        BookkeeperWindow windowObj;

        public UnClosePaymentEntry(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите восстановить выплату для этой записи и сделать ее активной?", "Предупреждение", MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == res)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "UPDATE listnuch SET isclose=0 WHERE listenerid = (select listenerid from listeners where fio='" + windowObj.Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + windowObj.Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                MessageBox.Show("Запись успешно восстановлена");


                updateDefraymentTable.Update(windowObj,1);
            }
        }
    }
}
