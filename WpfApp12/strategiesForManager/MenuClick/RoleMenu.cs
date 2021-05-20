using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class RoleMenu:IMenuClick
    {
        ManagerWindow window;

        public RoleMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.GoToAdminMenu.BorderBrush = Brushes.DarkRed;
            window.ScheduleMenu.BorderBrush = null;
            window.EmployeesMenu.BorderBrush = null;
            window.LearningMenu.BorderBrush = null;
            window.DiscountMenu.BorderBrush = null;
            window.ReportsMenu.BorderBrush = null;
        }
    }
}
