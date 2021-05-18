using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ReportMenu:IMenuClick
    {
        ManagerWindow window;

        public ReportMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.MenuRolesD.BorderBrush = null;
            window.raspMenu.BorderBrush = null;
            window.sotrMenu.BorderBrush = null;
            window.obuchMenu.BorderBrush = null;
            window.skidki.BorderBrush = null;
            window.MenuOtchety.BorderBrush = Brushes.DarkRed;
        }
    }
}
