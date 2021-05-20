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
    class GoToChangeTeacher : IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeTeacher(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.TeachersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.teacherID = Convert.ToInt32(arr[0]);
            windowObj.TeacherChangeName.Text = arr[2].ToString();
            windowObj.TeacherChangeComment.Text = arr[4].ToString();
            string[] dateStart = arr[3].ToString().Split(' ');
            string parsedDateStart = dateStart[0];
            windowObj.TeacherChangeDateStart.Text = parsedDateStart;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from kategorii";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                int itmeIndex = 0;
                windowObj.TeacherChangeCategory.SelectedIndex = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.TeacherChangeCategory.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { windowObj.TeacherChangeCategory.SelectedIndex = itmeIndex; }
                        itmeIndex++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.TeacherChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
