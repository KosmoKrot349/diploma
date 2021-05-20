using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class ServiceWorkMenu:IMenuClick
    {
        ManagerWindow window;

        public ServiceWorkMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.ServiceWorksGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateServiceWorksDataGrid(window);
        }
    }
}
