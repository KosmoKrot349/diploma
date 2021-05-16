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
        DirectorWindow window;

        public PositionMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
           window.StateGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridStates(window.connectionString, window.StateDataGrid);
        }
    }
}
