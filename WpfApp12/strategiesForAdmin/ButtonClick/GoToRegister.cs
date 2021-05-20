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

            admWind.NameRegistration.Text = "";
            admWind.LoginRegistration.Text = "";
            admWind.PasswordRegistration.Password = "";
            admWind.RepeatPasswordRegistration.Password = "";
            admWind.isAdminRegistration.IsChecked = false;
            admWind.isAdminRegistration.IsChecked = false;
            admWind.isManagerRegistration.IsChecked = false;
            admWind.hideAll();
            admWind.RegistrationGrid.Visibility = Visibility.Visible;
        }
    }
}
