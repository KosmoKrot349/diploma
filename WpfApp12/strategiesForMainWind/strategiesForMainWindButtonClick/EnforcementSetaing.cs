using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForMainWind.strategiesForMainWindButtonClick
{
    class EnforcementSeatings : IButtonClick
    {
        private MainWindow windowObj;

        public EnforcementSeatings(MainWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.saveSettings();
        }
    }
}
