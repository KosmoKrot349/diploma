using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class ToManager: IMenuClick
    {
        BookkeeperWindow windowObj;

        public ToManager(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            int checkerResult = 0;
            if (windowObj.logUser != -1) checkerResult = Checker.ManagerCheck(windowObj.logUser, windowObj.connectionString);

            if (checkerResult == 1 || windowObj.logUser == -1)
            {
                ManagerWindow wind = new ManagerWindow();
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
                            if (dReader.GetInt32(0) == 0) { wind.GoToAdminMenu.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.GoToBookkeeperMenu.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.GoToManagerMenu.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                wind.logUser = windowObj.logUser;
                wind.userName = windowObj.UserName;
                wind.Title = windowObj.UserName + " - Директор";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль директор. Для начала роботы выберите один из пунктов меню.";
                wind.Width = windowObj.Width;
                wind.Height = windowObj.Height;
                wind.Left = windowObj.Left;
                wind.Top = windowObj.Top;
                wind.Show();
                windowObj.Close();
            }
            else { MessageBox.Show("Вы не имете доступа к этой роли"); }
        }
    }
}
