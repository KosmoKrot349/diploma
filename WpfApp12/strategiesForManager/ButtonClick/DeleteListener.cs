using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class DeleteListener:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteListener(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ListenersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            //проверка на долги 
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select listdolgid from listdolg where listenerid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Вы не можете удалить этого слушателя, так как у него есть долги"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //проверка закрыта ли оплата у этого слушателя
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select grid from listnuch where isclose=0 and listenerid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Вы не можете удалить этого слушателя, так как у него есть не закрытые оплаты"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удлаение из таблицы
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "delete from listeners  where listenerid =" + arr[0];
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "delete from listnuch  where listenerid =" + arr[0];
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            windowObj.ListenersDataGrid.SelectedItem = null;

            //слушатели
            windowObj.listenerDeleteButton.IsEnabled = false;
            windowObj.listenerChangeButton.IsEnabled = false;
            DataGridUpdater.updateListenerDataGrid(windowObj);
        }
    }
}
