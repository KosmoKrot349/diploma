using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.strategiesForMainWindButtonClick
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
            string testConStr = "Server=" + windowObj.connect.Text + ";Port=" + windowObj.dbPortText.Text + ";User Id=postgres;Password=" + windowObj.dbPassText.Text + ";Database=db;";
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
                windowObj.saveSettings();

            }
            if (res == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
