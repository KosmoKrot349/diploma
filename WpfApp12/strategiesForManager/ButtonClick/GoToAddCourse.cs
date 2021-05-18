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
            windowObj.courseTitle.Text = "";
            windowObj.courseComm.Text = "";
            windowObj.subsCanvas.Children.Clear();
            windowObj.HideAll();
            windowObj.courseAddGrid.Visibility = Visibility.Visible;
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
                windowObj.chbxMas = new CheckBox[chbxMasLength];
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        windowObj.chbxMas[i] = new CheckBox();
                        windowObj.chbxMas[i].Name = "id" + reader.GetInt32(1).ToString();
                        windowObj.chbxMas[i].Content = reader.GetString(0);

                        windowObj.subsCanvas.Children.Add(windowObj.chbxMas[i]);
                        Canvas.SetLeft(windowObj.chbxMas[i], 1);
                        Canvas.SetTop(windowObj.chbxMas[i], i * 15);
                        i++;

                    }
                }
                windowObj.subsCanvas.Height = i * 15;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
