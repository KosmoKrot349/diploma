using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin.OtherMethods
{
    class HideAll
    {
        public static void Hide(AdminWindow window)
        {
            window.helloGrdi.Visibility = Visibility.Collapsed;
            window.UsersGrid.Visibility = Visibility.Collapsed;
            window.RegistrationGrid.Visibility = Visibility.Collapsed;
            window.CreateBackUpGrid.Visibility = Visibility.Collapsed;
            window.RestoreBackUpGrid.Visibility = Visibility.Collapsed;
            window.ChangeUserGrid.Visibility = Visibility.Collapsed;
            window.SettingsGrid.Visibility = Visibility.Collapsed;
            window.GoToNextYearGrid.Visibility = Visibility.Collapsed;

        }
    }
}
