using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class PositionMenu:IMenuClick
    {
        ManagerWindow window;

        public PositionMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
           window.PositionGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updatePositionsDataGrid(window);
        }
    }
}
