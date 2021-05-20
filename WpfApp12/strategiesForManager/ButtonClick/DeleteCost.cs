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
    class DeleteCost:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteCost(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.CostsTypeDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (Convert.ToInt32(arr[0]) >= 1 && Convert.ToInt32(arr[0]) <= 4) { MessageBox.Show("Невозможно удалить этот тип расхода"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT rashid   FROM rashody where typeid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Этот тип используется в таблице расходов"); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "DELETE FROM typerash WHERE typeid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateTypeCostsDataGrid(windowObj);
            windowObj.CostsTypeDataGrid.SelectedItem = null;
            windowObj.TypeRashDeleteButton.IsEnabled = false;
        }
    }
}
