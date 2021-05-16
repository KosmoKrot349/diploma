using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForAdmin
{
    class EnforcementSeatings:IButtonClick
    {
        private AdminWindow windowObj;

        public EnforcementSeatings(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            windowObj.saveSettings();
        }
    }
}
