using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForAdmin
{
    class FilterApp : IButtonClick
    {
        private AdminWindow windowObj;

        public FilterApp(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            windowObj.filter.ApplyUsersFilter();
            DataGridUpdater.updateUsersDataGrid(windowObj);
        }
    }
}
