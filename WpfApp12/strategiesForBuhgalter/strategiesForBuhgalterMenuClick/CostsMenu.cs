using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterMenuClick
{
    class CostsMenu:IMenuClick
    {
        BuhgalterWindow windowObj;

        public CostsMenu(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRolesB.BorderBrush = null;
            windowObj.Dohody.BorderBrush = null;
            windowObj.Rashody.BorderBrush = Brushes.DarkRed;
            windowObj.Nalogi.BorderBrush = null;
            windowObj.otchetMenu.BorderBrush = null;
        }
    }
}
