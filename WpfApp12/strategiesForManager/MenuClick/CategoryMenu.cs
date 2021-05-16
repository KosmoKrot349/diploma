using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class CategoryMenu : IMenuClick
    {
        DirectorWindow windowObj;

        public CategoryMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.kategGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridKateg(windowObj.connectionString, windowObj.kategDataGrid);
        }
    }
}
