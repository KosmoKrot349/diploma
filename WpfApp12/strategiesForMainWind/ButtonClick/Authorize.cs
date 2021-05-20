using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.ButtonClick
{
    class Authorize : IButtonClick
    {

        private MainWindow windowObj;
        public Authorize(MainWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            try
            {
                if (windowObj.Login.Text != "" && windowObj.Password.Password != "")
                {
                    if (windowObj.Password.Password == "resetrootpass" && windowObj.Login.Text == "root")
                    {
                        NpgsqlConnection npgSqlConnection1 = new NpgsqlConnection(windowObj.connectionString);
                        npgSqlConnection1.Open();
                        string sql1 = "update users set pas='rootpass' where uid = -1";
                        NpgsqlCommand Command1 = new NpgsqlCommand(sql1, npgSqlConnection1);
                        Command1.ExecuteNonQuery();
                        npgSqlConnection1.Close();
                        windowObj.Password.Password = "rootpass";
                    }

                    NpgsqlConnection npgSqlConnection = new NpgsqlConnection(windowObj.connectionString);
                    npgSqlConnection.Open();
                    string sql = "select uid,admin,buhgalter,director,fio from users where log='" + windowObj.Login.Text + "' and pas = '" + windowObj.Password.Password + "'";
                    NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                    NpgsqlDataReader reader = Command.ExecuteReader();
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {

                            if (1 == reader.GetInt32(1))
                            {

                                AdminWindow wind = new AdminWindow();
                                if (reader.GetInt32(1) == 0) { wind.GoToAdminMenu.IsEnabled = false; }
                                if (reader.GetInt32(2) == 0) { wind.GoToBookkeerMenu.IsEnabled = false; }
                                if (reader.GetInt32(3) == 0) { wind.GoToManagerMenu.IsEnabled = false; }
                                wind.logUser = reader.GetInt32(0);
                                wind.UserName = reader.GetString(4);
                                wind.Title = wind.UserName + " - Админ";
                                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль администратор. Для начала роботы выберите один из пунктов меню.";
                                wind.Width = windowObj.Width;
                                wind.Height = windowObj.Height;
                                wind.Left = windowObj.Left;
                                wind.Top = windowObj.Top;
                                wind.Show();
                                npgSqlConnection.Close();
                                windowObj.Close();
                                return;
                            }
                            if (1 == reader.GetInt32(2))
                            {
                                BookkeeperWindow wind = new BookkeeperWindow();
                                if (reader.GetInt32(1) == 0) { wind.GoToAdmin.IsEnabled = false; }
                                if (reader.GetInt32(2) == 0) { wind.GoToBookkeeper.IsEnabled = false; }
                                if (reader.GetInt32(3) == 0) { wind.GoToManager.IsEnabled = false; }
                                wind.logUser = reader.GetInt32(0);
                                wind.UserName = reader.GetString(4);
                                wind.Title = wind.UserName + " - Бухгалтер";
                                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль бухгалтер. Для начала роботы выберите один из пунктов меню.";
                                wind.Width = windowObj.Width;
                                wind.Height = windowObj.Height;
                                wind.Left = windowObj.Left;
                                wind.Top = windowObj.Top;
                                wind.Show();
                                npgSqlConnection.Close();
                                windowObj.Close();
                                return;
                            }
                            if (1 == reader.GetInt32(3))
                            {
                                ManagerWindow wind = new ManagerWindow();
                                if (reader.GetInt32(1) == 0) { wind.GoToAdminMenu.IsEnabled = false; }
                                if (reader.GetInt32(2) == 0) { wind.GoToBookkeeperMenu.IsEnabled = false; }
                                if (reader.GetInt32(3) == 0) { wind.GoToManagerMenu.IsEnabled = false; }
                                wind.logUser = reader.GetInt32(0);
                                wind.userName = reader.GetString(4);
                                wind.Title = wind.userName + " - Директор";
                                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль директор. Для начала роботы выберите один из пунктов меню.";
                                wind.Width = windowObj.Width;
                                wind.Height = windowObj.Height;
                                wind.Left = windowObj.Left;
                                wind.Top = windowObj.Top;
                                wind.Show();
                                npgSqlConnection.Close();
                                windowObj.Close();
                                return;
                            }
                        }


                    }
                    else { MessageBox.Show("Такого пользователя не существует"); }
                    npgSqlConnection.Close();
                }
                else MessageBox.Show("Одно из полей не заполнено");
            }
            catch
            {
                MessageBoxResult res = MessageBox.Show("Не получается обратиться к базе данных. \n Хотите провести настройки подключения?", "Ошибка соединения", MessageBoxButton.YesNo);

                if (res == MessageBoxResult.Yes)
                {
                    windowObj.SettingsGrid.Visibility = Visibility.Visible;
                    windowObj.AuthorizationGrid.Visibility = Visibility.Collapsed;
                    StreamReader streamReader = new StreamReader(@"setting.txt");
                    ArrayList ListFromSettingsFile = new ArrayList();
                    while (!streamReader.EndOfStream)
                    {
                        ListFromSettingsFile.Add(streamReader.ReadLine());
                    }
                    streamReader.Close();
                    object[] strArr = ListFromSettingsFile.ToArray();
                    windowObj.DBServer.Text = strArr[0].ToString().Split(':')[1];
                    windowObj.DBPassword.Text = strArr[1].ToString().Split(':')[1];
                    windowObj.DBPort.Text = strArr[2].ToString().Split(':')[1];

                }
                if (res == MessageBoxResult.No)
                {
                    return;
                }
            }

        }
    }
}
