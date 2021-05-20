using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class LearningMenu:IMenuClick
    {
        ManagerWindow window;

        public LearningMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.GoToAdminMenu.BorderBrush = null;
            window.ScheduleMenu.BorderBrush = null;
            window.EmployeesMenu.BorderBrush = null;
            window.DiscountMenu.BorderBrush = null;
            window.ReportsMenu.BorderBrush = null;
            window.LearningMenu.BorderBrush = Brushes.DarkRed;
        }
    }
}
