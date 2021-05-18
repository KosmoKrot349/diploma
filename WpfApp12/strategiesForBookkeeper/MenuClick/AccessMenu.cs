using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class AccessMenu : IMenuClick
    {
        BookkeeperWindow windowObj;

        public AccessMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {

            windowObj.MenuRolesB.BorderBrush = Brushes.DarkRed;
            windowObj.Dohody.BorderBrush = null;
            windowObj.Rashody.BorderBrush = null;
            windowObj.Nalogi.BorderBrush = null;
            windowObj.otchetMenu.BorderBrush = null;
        }
    }
}
