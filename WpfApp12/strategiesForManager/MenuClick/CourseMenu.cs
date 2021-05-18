using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class CourseMenu:IMenuClick
    {
        ManagerWindow windowObj;

        public CourseMenu(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.courseGrid.Visibility = Visibility.Visible;


            windowObj.FiltrGridCourse.Children.Clear();
            windowObj.FiltrGridCourse.ColumnDefinitions.Clear();


            windowObj.filtr.CreateCourseFiltr(windowObj.FiltrGridSubs);

            windowObj.filtr.sql = "select courseid,title,comment FROM courses";

            DataGridUpdater.updateDataGridСourses(windowObj.connectionString, windowObj.filtr.sql, windowObj.coursDataGrid);
        }
    }
}
