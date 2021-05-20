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

            windowObj.RolesMenu.BorderBrush = Brushes.DarkRed;
            windowObj.ProfitMenu.BorderBrush = null;
            windowObj.CostsMenu.BorderBrush = null;
            windowObj.TaxesMenu.BorderBrush = null;
            windowObj.ReportsMenu.BorderBrush = null;
        }
    }
}
