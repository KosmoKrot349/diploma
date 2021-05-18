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
            SaveDataBaseSettings.Save(windowObj.connect.Text, windowObj.dbPassText.Text, windowObj.dbPortText.Text);
            windowObj.connectionString = "Server=" + windowObj.connect.Text + ";Port=" + windowObj.dbPortText.Text + ";User Id=postgres;Password=" + windowObj.dbPassText.Text + ";Database=db";

            MessageBox.Show("Настройки сохранены и применены");
            windowObj.settingGrid.Visibility = Visibility.Collapsed;
            windowObj.autGrid.Visibility = Visibility.Visible;
        }
    }
}
