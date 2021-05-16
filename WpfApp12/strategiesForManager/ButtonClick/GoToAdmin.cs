using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAdmin:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToAdmin(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int a = 0;
            if (windowObj.logUser != -1) a = Checker.adminCheck(windowObj.logUser, windowObj.connectionString);
            if (a == 1 || windowObj.logUser == -1)
            {
                AdminWindow wind = new AdminWindow();
                try
                {
                    NpgsqlConnection connection = new NpgsqlConnection(windowObj.connectionString);
                    string sql = "select admin,buhgalter,director from users where uid = " + windowObj.logUser;
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader dReader = command.ExecuteReader();
                    if (dReader.HasRows)
                    {
                        while (dReader.Read())
                        {
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleA.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleA.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleA.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                wind.logUser = windowObj.logUser;
                wind.FIO = windowObj.FIO;
                wind.Title = windowObj.FIO + " - Админ";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль администратор. Для начала роботы выберите один из пунктов меню.";
                wind.Width = windowObj.Width;
                wind.Height = windowObj.Height;
                wind.Left = windowObj.Left;
                wind.Top = windowObj.Top;
                wind.Show();
                windowObj.Close();
            }
            else { MessageBox.Show("Вы не имеете доступа к этой роли"); }
        }
    }
}
