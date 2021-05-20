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
    class DeletePosition:IButtonClick
    {
        ManagerWindow windowObj;

        public DeletePosition(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.PositionsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT shtatid FROM shtat where states @> ARRAY[" + arr[0] + "] ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Должность используется сотрудником"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "DELETE FROM states WHERE statesid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updatePositionsDataGrid(windowObj);
            windowObj.PositionsDataGrid.SelectedItem = null;
            windowObj.StateChangeButton.IsEnabled = false;
            windowObj.StateDeleteButton.IsEnabled = false;
        }
    }
}
