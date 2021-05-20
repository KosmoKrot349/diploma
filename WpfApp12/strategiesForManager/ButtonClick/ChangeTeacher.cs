using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeTeacher:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeTeacher(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.TeacherChangeName.Text == "" || windowObj.TeacherChangeDateStart.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            int categoryID = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = ("select kategid from kategorii where title = '" + windowObj.TeacherChangeCategory.SelectedValue + "'");
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        categoryID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            string[] date = windowObj.TeacherAddDateStart.Text.Split('.');

            int employeeID = 0;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select sotrid from prep where prepid = " + windowObj.teacherID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employeeID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE prep SET  kategid =" + categoryID + ", date_start ='" + windowObj.TeacherChangeDateStart.Text + "' WHERE prepid = " + windowObj.teacherID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = ("UPDATE sotrudniki SET  fio ='" + windowObj.TeacherChangeName.Text + "', comment ='" + windowObj.TeacherChangeComment.Text + "' WHERE sotrid = " + employeeID + ";");
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.TeachersGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateTeachersDataGrid(windowObj);
        }
    }
}
