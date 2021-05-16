using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForAdmin
{
    class GoToRegister : IButtonClick
    {

        AdminWindow admWind;

        public GoToRegister(AdminWindow admWind)
        {
            this.admWind = admWind;
        }

        public void buttonClick()
        {

            admWind.fio.Text = "";
            admWind.log_reg.Text = "";
            admWind.pas_reg.Password = "";
            admWind.rePas.Password = "";
            admWind.adm.IsChecked = false;
            admWind.bh.IsChecked = false;
            admWind.dr.IsChecked = false;
            admWind.hideAll();
            admWind.regGrid.Visibility = Visibility.Visible;
        }
    }
}
