using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForAdmin.MenuClick
{
    internal class AccessMenuClick:IMenuClick
    {
        private AdminWindow windowObj;

        public AccessMenuClick(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRoles.BorderBrush = Brushes.DarkRed;
            windowObj.usersMenu.BorderBrush = null;
            windowObj.archiveMenu.BorderBrush = null;
            windowObj.settingMenu.BorderBrush = null;
            windowObj.GoToNextYear.BorderBrush = null;
        }
    }
}
