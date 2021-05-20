using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeCourse:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeCourse(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            string subjectMass = "'{";
            bool b = false;
            for (int i = 0; i < windowObj.checkBoxArr.Length; i++)
            {
                if (windowObj.checkBoxArr[i].IsChecked == true)
                {
                    b = true;
                    subjectMass += windowObj.checkBoxArr[i].Name.Substring(2) + ",";
                }
            }
            subjectMass = subjectMass.Substring(0, subjectMass.Length - 1);
            subjectMass += "}'";
            if (b == false || windowObj.CourseChangeTitle.Text == "") { MessageBox.Show("Название курса или предметы не добавлены"); return; }
            if (windowObj.dontChangeCourseName != windowObj.CourseChangeTitle.Text)
            {
                try
                {
                    NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                    con1.Open();
                    string sql1 = "select count(courseid) from courses where title='" + windowObj.CourseChangeTitle.Text + "'";
                    NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                    NpgsqlDataReader reader = command1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            if (reader.GetInt32(0) > 0) { MessageBox.Show("Такое название курса уже существует"); return; }
                        }

                    }
                    con1.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "UPDATE courses SET title ='" + windowObj.CourseChangeTitle.Text + "', subs =" + subjectMass + ", comment ='" + windowObj.CourseChangeComment.Text + "' WHERE courseid=" + windowObj.courseID;
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.HideAll();
            windowObj.CourcesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateСoursesDataGrid(windowObj);
        }
    }
}
