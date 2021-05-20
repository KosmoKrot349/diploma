using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class Register : IButtonClick
    {
        private AdminWindow windowObj;

        public Register(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            if (windowObj.NameRegistration.Text == "" || windowObj.LoginRegistration.Text == "" || windowObj.PasswordRegistration.Password == "" || windowObj.RepeatPasswordRegistration.Password == "") { MessageBox.Show("Некоторые поля не заполнены, регистрация невозможна"); return; }
            if (windowObj.PasswordRegistration.Password != windowObj.RepeatPasswordRegistration.Password) { MessageBox.Show("Пароли не совподают"); return; }
            if (windowObj.LoginRegistration.Text == "root") { MessageBox.Show("Пользователь root уже существует"); return; }
            try
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(windowObj.connectionString);
                npgSqlConnection.Open();
                string sql = "select log from users where log='" + windowObj.LoginRegistration + "'";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                NpgsqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Пользовтель с таким логином уже существует"); return; }
                npgSqlConnection.Close();
                npgSqlConnection.Open();
                sql = "insert into users (fio, log, pas, admin, buhgalter, director) values('" + windowObj.NameRegistration.Text + "','" + windowObj.LoginRegistration.Text + "','" + windowObj.PasswordRegistration.Password + "'," + windowObj.adminRole + "," + windowObj.bookkeeperRole + "," + windowObj.managerRole + ")";
                Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult but = MessageBox.Show("Пользователь добавлен.\nПродолжить добавление?", "Добавление", MessageBoxButton.YesNo);

            if (but == MessageBoxResult.Yes)
            {
                windowObj.NameRegistration.Text = "";
                windowObj.LoginRegistration.Text = "";
                windowObj.PasswordRegistration.Password = "";
                windowObj.RepeatPasswordRegistration.Password = "";
                windowObj.isAdminRegistration.IsChecked = false;
                windowObj.isAdminRegistration.IsChecked = false;
                windowObj.isManagerRegistration.IsChecked = false;
            }
            else
            {
                windowObj.hideAll();
                windowObj.UsersGrid.Visibility = Visibility.Visible;
                windowObj.usersDGrid.SelectedItem = null;

                windowObj.changeUser.IsEnabled = false;
                windowObj.dellUser.IsEnabled = false;
                DataGridUpdater.updateUsersDataGrid(windowObj);
            }
        }
    }
}
