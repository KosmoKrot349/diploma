using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class LearningMenu:IMenuClick
    {
        DirectorWindow window;

        public LearningMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.MenuRolesD.BorderBrush = null;
            window.raspMenu.BorderBrush = null;
            window.sotrMenu.BorderBrush = null;
            window.skidki.BorderBrush = null;
            window.MenuOtchety.BorderBrush = null;
            window.obuchMenu.BorderBrush = Brushes.DarkRed;
        }
    }
}
