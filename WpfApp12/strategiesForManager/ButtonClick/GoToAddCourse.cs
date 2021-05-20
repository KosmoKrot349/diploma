using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddCourse:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddCourse(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.CourseAddTitle.Text = "";
            windowObj.CourseAddComm.Text = "";
            windowObj.CourseAddSubjects.Children.Clear();
            windowObj.HideAll();
            windowObj.CourseAddGrid.Visibility = Visibility.Visible;
            int chbxMasLength = 0;
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select count(title) from subject ";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        chbxMasLength = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title,subid from subject";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                windowObj.checkBoxArr = new CheckBox[chbxMasLength];
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        windowObj.checkBoxArr[i] = new CheckBox();
                        windowObj.checkBoxArr[i].Name = "id" + reader.GetInt32(1).ToString();
                        windowObj.checkBoxArr[i].Content = reader.GetString(0);

                        windowObj.CourseAddSubjects.Children.Add(windowObj.checkBoxArr[i]);
                        Canvas.SetLeft(windowObj.checkBoxArr[i], 1);
                        Canvas.SetTop(windowObj.checkBoxArr[i], i * 15);
                        i++;

                    }
                }
                windowObj.CourseAddSubjects.Height = i * 15;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
