using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToBuhgalter:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToBuhgalter(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int b = 0;
            if (windowObj.logUser != -1) b = Checker.buhgCheck(windowObj.logUser, windowObj.connectionString);
            if (b == 1 || windowObj.logUser == -1)
            {
                BookkeeperWindow wind = new BookkeeperWindow();
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
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleB.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleB.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleB.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                wind.logUser = windowObj.logUser;
                wind.FIO = windowObj.FIO;
                wind.Title = windowObj.FIO + " - Бухгалтер";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль бухгалтер. Для начала роботы выберите один из пунктов меню.";
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
