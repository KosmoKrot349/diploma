using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WpfApp12.strategiesForAdmin.MenuClick
{
    class UsersMenuClick : IMenuClick
    {
        public AdminWindow windowObj;

        public UsersMenuClick(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRoles.BorderBrush = null;
            windowObj.usersMenu.BorderBrush = Brushes.DarkRed;
            windowObj.archiveMenu.BorderBrush = null;
            windowObj.settingMenu.BorderBrush = null;
            windowObj.GoToNextYear.BorderBrush = null;

            windowObj.usersDGrid.SelectedItem = null;

            windowObj.GoToChangeUser.IsEnabled = false;
            windowObj.DelUser.IsEnabled = false;

            windowObj.hideAll();
            windowObj.UsersGrid.Visibility = Visibility.Visible;

            windowObj.filter.CreateUsersFilter(windowObj.FiltrGridRoles);

            windowObj.filter.sql = "select * from users where uid != -1";

            DataGridUpdater.updateUsersDataGrid(windowObj);
        }
    }
}
