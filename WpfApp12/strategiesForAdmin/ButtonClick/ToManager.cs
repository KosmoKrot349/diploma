using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class ToManager : IButtonClick
    {
        private AdminWindow windowObj;

        public ToManager(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            int d = 0;
            if (windowObj.logUser != -1) d = Checker.dirCheck(windowObj.logUser, windowObj.connectionString);

            if (d == 1 || windowObj.logUser == -1)
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
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleD.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleD.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleD.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                wind.logUser = windowObj.logUser;
                wind.FIO = windowObj.FIO;
                wind.Title = windowObj.FIO + " - Директор";
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
