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
    class DeleteServiceWork:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteServiceWork(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ObslWorkDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT shtatid  FROM shtat where  obslwork @> ARRAY[" + arr[0] + "]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Эта работа используется сотрудником"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }



            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "DELETE FROM raboty_obsl WHERE rabotyid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateServiceWorksDataGrid(windowObj);
            windowObj.ObslWorkDataGrid.SelectedItem = null;
            windowObj.ObslWorkDeleteButton.IsEnabled = false;
        }
    }
}
