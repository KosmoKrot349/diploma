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
        DirectorWindow window;

        public CabinteMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.cabGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridСab(window.connectionString, window.cabDataGrid);
        }
    }
}
