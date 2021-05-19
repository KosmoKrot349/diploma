using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class TypeOfCostsMenu:IMenuClick
    {
        ManagerWindow window;

        public TypeOfCostsMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
           window.HideAll();
            window.TypeRashGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateTypeCostsDataGrid(window);
        }
    }
}
