using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForAdmin
{
    class DelUser : IButtonClick
    {
        private AdminWindow windowObj;

        public DelUser(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            DataRowView DRV = windowObj.usersDGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (Convert.ToInt32(arr[0]) == windowObj.logUser) { MessageBox.Show("Вы не можете самого себя"); return; }
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(windowObj.connectionString);
            try
            {
                npgSqlConnection.Open();
                string sql = "DELETE FROM users WHERE uid =" + arr[0];
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.usersDGrid.SelectedItem = null;
            windowObj.changeUser.IsEnabled = false;
            windowObj.dellUser.IsEnabled = false;
            DataGridUpdater.updateDataGridUsers(windowObj.connectionString, windowObj.filtr.sql, windowObj.usersDGrid);
        }
    }
}
