using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace WpfApp12.strategiesForAdmin.strategiesForAdminMenuClick
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
            windowObj.MenuRolesA.BorderBrush = null;
            windowObj.usersMenu.BorderBrush = Brushes.DarkRed;
            windowObj.arhivMenu.BorderBrush = null;
            windowObj.settingMenu.BorderBrush = null;
            windowObj.ToNextYearMenu.BorderBrush = null;

            windowObj.usersDGrid.SelectedItem = null;

            windowObj.changeUser.IsEnabled = false;
            windowObj.dellUser.IsEnabled = false;

            windowObj.hideAll();
            windowObj.delChUserGrid.Visibility = Visibility.Visible;

            windowObj.filter.CreateUsersFiltr(windowObj.FiltrGridRoles);

            windowObj.filter.sql = "select * from users where uid != -1";

            DataGridUpdater.updateDataGridUsers(windowObj.connectionString, windowObj.filter.sql, windowObj.usersDGrid);
        }
    }
}
