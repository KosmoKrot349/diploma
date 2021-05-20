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
            windowObj.CourcesGrid.Visibility = Visibility.Visible;


            windowObj.FilterGridCourse.Children.Clear();
            windowObj.FilterGridCourse.ColumnDefinitions.Clear();


            windowObj.filter.CreateCourseFilter(windowObj.FilterGridSubs);

            windowObj.filter.sql = "select courseid,title,comment FROM courses";

            DataGridUpdater.updateСoursesDataGrid(windowObj);
        }
    }
}
