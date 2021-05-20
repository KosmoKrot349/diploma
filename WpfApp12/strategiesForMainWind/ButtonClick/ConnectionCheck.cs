using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.ButtonClick
{
    class ConnectionCheck : IButtonClick
    {
        MainWindow windowObj;

        public ConnectionCheck(MainWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            string testConStr = "Server=" + windowObj.DBServer.Text + ";Port=" + windowObj.DBPort.Text + ";User Id=postgres;Password=" + windowObj.DBPassword.Text + ";Database=db;";
            NpgsqlConnection testcon = new NpgsqlConnection(testConStr);
            try
            {
                testcon.Open();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
            testcon.Close();
            MessageBoxResult res = MessageBox.Show("Подключение по данным параметрам прошло успешно. \nСохранить параметры?", "Сохранение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                SaveDataBaseSettings.Save(windowObj.DBServer.Text, windowObj.DBPassword.Text, windowObj.DBPort.Text);
                windowObj.connectionString = "Server=" + windowObj.DBServer.Text + ";Port=" + windowObj.DBPort.Text + ";User Id=postgres;Password=" + windowObj.DBPassword.Text + ";Database=db";

                MessageBox.Show("Настройки сохранены и применены");
                windowObj.SettingsGrid.Visibility = Visibility.Collapsed;
                windowObj.AuthorizationGrid.Visibility = Visibility.Visible;
            }
            if (res == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
