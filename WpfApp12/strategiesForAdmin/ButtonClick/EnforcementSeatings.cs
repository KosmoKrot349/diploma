using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class EnforcementSeatings:IButtonClick
    {
        private AdminWindow windowObj;

        public EnforcementSeatings(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            SaveDataBaseSettings.Save(windowObj.connect.Text, windowObj.dbPassText.Text, windowObj.dbPortSettings.Text);
            windowObj.connectionString = "Server=" + windowObj.connect.Text + ";Port=" + windowObj.dbPortSettings.Text + ";User Id=postgres;Password=" + windowObj.dbPassText.Text + ";Database=db";

            MessageBox.Show("Настройки сохранены и применены");
        }
    }
}
