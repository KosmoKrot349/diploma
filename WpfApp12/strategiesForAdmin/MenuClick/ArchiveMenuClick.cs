using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForAdmin.MenuClick
{
    class ArchiveMenuClick:IMenuClick
    {
        private AdminWindow windowObj;

        public ArchiveMenuClick(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRoles.BorderBrush = null;
            windowObj.usersMenu.BorderBrush = null;
            windowObj.archiveMenu.BorderBrush = Brushes.DarkRed;
            windowObj.settingMenu.BorderBrush = null;
            windowObj.GoToNextYear.BorderBrush = null;
        }
    }
}
