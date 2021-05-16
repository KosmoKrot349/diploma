using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterMenuClick
{
    class ProfitMenu:IMenuClick
    {
        BuhgalterWindow windowObj;

        public ProfitMenu(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRolesB.BorderBrush = null;
            windowObj.Dohody.BorderBrush = Brushes.DarkRed;
            windowObj.Rashody.BorderBrush = null;
            windowObj.Nalogi.BorderBrush = null;
            windowObj.otchetMenu.BorderBrush = null;
        }
    }
}
