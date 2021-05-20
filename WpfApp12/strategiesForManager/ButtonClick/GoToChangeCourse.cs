using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeCourse:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeCourse(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.CourcesDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            windowObj.CourseChangeSubjects.Children.Clear();
            windowObj.HideAll();
            windowObj.CourseChangeGrid.Visibility = Visibility.Visible;
            object[] arr = DR.ItemArray;
            windowObj.courseID = Convert.ToInt32(arr[0]);
            windowObj.CourseChangeTitle.Text = arr[1].ToString();
            windowObj.dontChangeCourseName = arr[1].ToString();
            windowObj.CourseChangeComment.Text = arr[3].ToString();
            object[] subjectsArr = arr[2].ToString().Replace(" ", "").Split(',');
            ArrayList list = new ArrayList(subjectsArr);
            int checkBoxArrLength = 0;
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
                        checkBoxArrLength = reader1.GetInt32(0);
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
                windowObj.checkBoxArr = new CheckBox[checkBoxArrLength];
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        windowObj.checkBoxArr[i] = new CheckBox();
                        windowObj.checkBoxArr[i].Name = "id" + reader.GetInt32(1).ToString();
                        windowObj.checkBoxArr[i].Content = reader.GetString(0);
                        if (list.IndexOf(reader.GetString(0)) != -1) { windowObj.checkBoxArr[i].IsChecked = true; }
                        windowObj.CourseChangeSubjects.Children.Add(windowObj.checkBoxArr[i]);
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
