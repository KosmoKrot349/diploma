using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class SubjectMenu:IMenuClick
    {
        DirectorWindow windowObj;

        public SubjectMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.subGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridSubjects(windowObj.connectionString, windowObj.subsDataGrid);
        }
    }
}
