using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class GroopMenu:IMenuClick
    {
        DirectorWindow windowObj;

        public GroopMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {

            windowObj.HideAll();
            windowObj.groupsGrid.Visibility = Visibility.Visible;

            windowObj.FiltrGridCourse.Children.Clear();
            windowObj.FiltrGridCourse.ColumnDefinitions.Clear();
            windowObj.filtr.CreateGroupFiltr(windowObj.FiltrGridCourse);
            windowObj.filtr.sql = "SELECT groups.grid as grid,  groups.nazvanie as gtitle,courses.title as ctitle, groups.comment as comment ,groups.payment[1],groups.payment[2],groups.payment[3],groups.payment[4],groups.payment[5],groups.payment[6],groups.payment[7],groups.payment[8],groups.payment[9],groups.payment[10],groups.payment[11],groups.payment[12],date_start,date_end FROM groups inner join courses using (courseid)  ";
            DataGridUpdater.updateDataGridGroups(windowObj.connectionString, windowObj.filtr.sql, windowObj.groupsDataGrid);
        }
    }
}
