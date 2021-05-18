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
    class DeleteEmployee:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteEmployee(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.allSotrDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка препода в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select raspisanie.prepid from sotrudniki inner join prep using(sotrid) inner join raspisanie using(prepid) where sotrid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Преподаватель используется в расписании"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            MessageBoxResult res = MessageBox.Show("Данные об этом сотруднике будут удалены из штатного расписания, преподавателей, штата и таблицы сотрудников.\n Так же будет удалена информация о начислениях и расходах.\n Удалить?", "Предупреждение", MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                //проверка в  х зп


                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from nachisl where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteReader();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                //проверка в расходах
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from rashody where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) { MessageBox.Show("Запись в расходах связана с этим сотрудником"); return; }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                //удаление из штатного расписания
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "UPDATE shtatrasp SET shtatid=array_remove(shtatid," + arr[0] + ")";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                //удаление из преподавателей
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from prep where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                //удаление из штата
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from shtat where shtatid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from sotrudniki where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            DataGridUpdater.updateDataGridSotr(windowObj.connectionString, windowObj.sqlForAllEmployees, windowObj.allSotrDataGrid);

            windowObj.allSotrDataGrid.SelectedItem = null;
            //все сотрудники
            windowObj.allSotrDeleteButton.IsEnabled = false;
            windowObj.allSotrToPrepBtton.IsEnabled = false;
            windowObj.allSotrToShtatBtton.IsEnabled = false;
        }
    }
}
