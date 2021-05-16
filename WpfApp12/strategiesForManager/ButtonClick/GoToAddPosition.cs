using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddPosition:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToAddPosition(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.days1.Text = "22";
            windowObj.days2.Text = "22";
            windowObj.days3.Text = "22";
            windowObj.days4.Text = "22";
            windowObj.days5.Text = "22";
            windowObj.days6.Text = "22";
            windowObj.days7.Text = "22";
            windowObj.days8.Text = "22";
            windowObj.days9.Text = "22";
            windowObj.days10.Text = "22";
            windowObj.days11.Text = "22";
            windowObj.days12.Text = "22";
            windowObj.StateAddTitle.Text = "";
            windowObj.StateAddPay.Text = "";
            windowObj.StateAddCom.Text = "";
            windowObj.StateAddGrid.Visibility = Visibility.Visible;
        }
    }
}
