using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class PersonnelMenu:IMenuClick
    {
        ManagerWindow window;

        public PersonnelMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.MenuRolesD.BorderBrush = null;
            window.raspMenu.BorderBrush = null;
            window.sotrMenu.BorderBrush = Brushes.DarkRed;
            window.obuchMenu.BorderBrush = null;
            window.skidki.BorderBrush = null;
            window.MenuOtchety.BorderBrush = null;
        }
    }
}
