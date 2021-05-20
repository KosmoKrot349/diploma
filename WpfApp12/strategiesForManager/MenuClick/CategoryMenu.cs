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
        ManagerWindow windowObj;

        public CategoryMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.CategoriesGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateCategoriesDataGrid(windowObj);
        }
    }
}
