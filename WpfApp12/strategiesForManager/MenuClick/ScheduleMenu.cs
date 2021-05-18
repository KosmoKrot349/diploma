using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ScheduleMenu:IMenuClick
    {
        ManagerWindow window;

        public ScheduleMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.MenuRolesD.BorderBrush = null;
            window.raspMenu.BorderBrush = Brushes.DarkRed;
            window.sotrMenu.BorderBrush = null;
            window.obuchMenu.BorderBrush = null;
            window.skidki.BorderBrush = null;
            window.MenuOtchety.BorderBrush = null;
        }
    }
}
