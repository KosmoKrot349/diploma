using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class EmployeeToTeacher:IButtonClick
    {
        ManagerWindow windowObj;

        public EmployeeToTeacher(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.dateStart.Text == "") { MessageBox.Show("Дата начала работы не выбрана"); return; }
            int categoryID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select kategid from kategorii where title='" + windowObj.kategCMB.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoryID = reader.GetInt32(0);
                    }

                }
                else { con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO prep(kategid, date_start, sotrid) VALUES(" + categoryID + ", '" + windowObj.dateStart.Text + "', " + windowObj.employeeID + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBox.Show("Сотрудник определён как преподаватель");
            DataGridUpdater.updateEmploeesDataGrid(windowObj);
            windowObj.HideAll();
            windowObj.allSotrGrid.Visibility = Visibility.Visible;
        }
    }
}
