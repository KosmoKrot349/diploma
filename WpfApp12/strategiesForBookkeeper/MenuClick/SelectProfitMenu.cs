using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class SelectProfitMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public SelectProfitMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.RolesMenu.BorderBrush = null;
            windowObj.ProfitMenu.BorderBrush = Brushes.DarkRed;
            windowObj.CostsMenu.BorderBrush = null;
            windowObj.TaxesMenu.BorderBrush = null;
            windowObj.ReportsMenu.BorderBrush = null;
        }
    }
}
