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
    class DeleteTeacher:IButtonClick
    {
        DirectorWindow windowObj;

        public DeleteTeacher(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.prepDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select prepid from raspisanie where prepid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Преподаватель используется в расписании"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных1"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = ("delete from prep where prepid =" + arr[0]);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных2"); return; }
            DataGridUpdater.updateDataGridPrep(windowObj.connectionString, windowObj.filtr.sql, windowObj.prepDataGrid);

            windowObj.prepDataGrid.SelectedItem = null;

            //преподаватели
            windowObj.prepDeleteButton.IsEnabled = false;
            windowObj.prepChangeButton.IsEnabled = false;
        }
    }
}
