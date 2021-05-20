using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.SelectionChanged
{
    class ChangeFilterTypeForStaff:ISelectionChaged
    {
        ManagerWindow window;

        public ChangeFilterTypeForStaff(ManagerWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            if (window.StaffFilterCMBX.SelectedIndex == 0)
            {

                window.StaffFilter.Children.Clear();
                window.StaffFilter.ColumnDefinitions.Clear();
                window.filter.CreateStaffFirstFilter(window.StaffFilter);
            }
            else
            {
                window.StaffFilter.Children.Clear();
                window.StaffFilter.ColumnDefinitions.Clear();
                window.filter.CreateStaffSecondFilter(window.StaffFilter);
            }
        }
    }
}
