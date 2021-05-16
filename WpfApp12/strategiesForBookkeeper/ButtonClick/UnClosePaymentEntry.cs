using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class UnClosePaymentEntry:IButtonClick
    {
        BuhgalterWindow windowObj;

        public UnClosePaymentEntry(BuhgalterWindow windowObj)
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


                windowObj.updateOplataTable(1);
            }
        }
    }
}
