using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class WorkCoeffMenu:IMenuClick
    {
        DirectorWindow window;

        public WorkCoeffMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.KoefGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridKoef(window.connectionString, window.KoefDataGrid);
        }
    }
}
