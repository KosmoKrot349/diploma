using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class CabinteMenu:IMenuClick
    {
        ManagerWindow window;

        public CabinteMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.CabinetsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateCabinetDataGrid(window);
        }
    }
}
