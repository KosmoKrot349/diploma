using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.ButtonClick
{
    class EnforcementSeatings : IButtonClick
    {
        private MainWindow windowObj;

        public EnforcementSeatings(MainWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            SaveDataBaseSettings.Save(windowObj.DBServer.Text, windowObj.DBPassword.Text, windowObj.DBPort.Text);
            windowObj.connectionString = "Server=" + windowObj.DBServer.Text + ";Port=" + windowObj.DBPort.Text + ";User Id=postgres;Password=" + windowObj.DBPassword.Text + ";Database=db";

            MessageBox.Show("Настройки сохранены и применены");
            windowObj.SettingsGrid.Visibility = Visibility.Collapsed;
            windowObj.AuthorizationGrid.Visibility = Visibility.Visible;
        }
    }
}
