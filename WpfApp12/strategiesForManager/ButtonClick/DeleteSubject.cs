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
    class DeleteSubject:IButtonClick
    {
        ManagerWindow windowObj;

        public DeleteSubject(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.SubjectsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка предмета в курсах
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from courses WHERE subs @> '{" + arr[0] + "}'; ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string title = "";
                    while (reader.Read())
                    {
                        title += reader.GetString(0) + " ";
                    }
                    MessageBox.Show("Этот предмет не удаётся удалить, он используется в курсах: " + title); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //проверка предмета в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select subid from raspisanie where subid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Предмет используется в расписании"); return; }
                con.Close();
            }

            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //удаление из предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "DELETE FROM subject WHERE subid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateSubjectDataGrid(windowObj);

            windowObj.SubjectsDataGrid.SelectedItem = null;

            //предметы
            windowObj.subDeleteButton.IsEnabled = false;
        }
    }
}
