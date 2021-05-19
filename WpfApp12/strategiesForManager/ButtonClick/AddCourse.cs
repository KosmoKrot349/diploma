using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddCourse:IButtonClick
    {
        ManagerWindow windowObj;

        public AddCourse(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            string subjectsArr = "'{";
            bool b = false;
            for (int i = 0; i < windowObj.checkBoxArr.Length; i++)
            {
                if (windowObj.checkBoxArr[i].IsChecked == true)
                {
                    b = true;
                    subjectsArr += windowObj.checkBoxArr[i].Name.Substring(2) + ",";
                }
                windowObj.checkBoxArr[i].IsChecked = false;
            }
            subjectsArr = subjectsArr.Substring(0, subjectsArr.Length - 1);
            subjectsArr += "}'";
            if (b == false || windowObj.courseTitle.Text == "") { MessageBox.Show("Название курса или предметы не добавлены"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select count(courseid) from courses where title='" + windowObj.courseTitle.Text + "'";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.GetInt32(0) > 1) { MessageBox.Show("Такое название курса уже существует"); return; }
                    }

                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "INSERT INTO courses(title, subs, comment) VALUES('" + windowObj.courseTitle.Text + "', " + subjectsArr + ", '" + windowObj.courseComm.Text + "'); ";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            var mbx = MessageBox.Show("Курс добавлен. \n Продолжить добавление?", "Добавить", MessageBoxButton.YesNo);
            if (mbx == MessageBoxResult.Yes)
            {
                windowObj.courseTitle.Text = "";
                windowObj.courseComm.Text = "";
            }
            else
            {
                windowObj.HideAll();
                windowObj.courseGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateСoursesDataGrid(windowObj);

            }
        }
    }
}
