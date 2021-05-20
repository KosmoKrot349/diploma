using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class PersonnelMenu:IMenuClick
    {
        ManagerWindow window;

        public PersonnelMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.GoToAdminMenu.BorderBrush = null;
            window.ScheduleMenu.BorderBrush = null;
            window.EmployeesMenu.BorderBrush = Brushes.DarkRed;
            window.LearningMenu.BorderBrush = null;
            window.DiscountMenu.BorderBrush = null;
            window.ReportsMenu.BorderBrush = null;
        }
    }
}
