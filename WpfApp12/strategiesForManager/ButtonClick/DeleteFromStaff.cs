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
    class DeleteFromStaff:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteFromStaff(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.StaffDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;



            //проверка в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE shtatrasp SET shtatid = array_remove(shtatid, " + arr[0] + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from shtat where shtatid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
            DataGridUpdater.updateStaffDataGrid(windowObj);

            windowObj.StaffDataGrid.SelectedItem = null;

            //штат
            windowObj.shtatDeleteButton.IsEnabled = false;
            windowObj.shtatChangeButton.IsEnabled = false;
        }
    }
}
