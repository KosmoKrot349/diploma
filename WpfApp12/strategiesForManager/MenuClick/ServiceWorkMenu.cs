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
        DirectorWindow window;

        public ServiceWorkMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.ObslWorkGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridRaboty(window.connectionString, window.ObslWorkDataGrid);
        }
    }
}
