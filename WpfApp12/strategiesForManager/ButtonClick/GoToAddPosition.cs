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
        ManagerWindow windowObj;

        public GoToAddPosition(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.AddWorksDay1Month.Text = "22";
            windowObj.AddWorksDay2Month.Text = "22";
            windowObj.AddWorksDay3Month.Text = "22";
            windowObj.AddWorksDay4Month.Text = "22";
            windowObj.AddWorksDay5Month.Text = "22";
            windowObj.AddWorksDay6Month.Text = "22";
            windowObj.AddWorksDay7Month.Text = "22";
            windowObj.AddWorksDay8Month.Text = "22";
            windowObj.AddWorksDay9Month.Text = "22";
            windowObj.AddWorksDay10Month.Text = "22";
            windowObj.AddWorksDay11Month.Text = "22";
            windowObj.AddWorksDay12Month.Text = "22";
            windowObj.PositionAddTitle.Text = "";
            windowObj.PositionAddSalary.Text = "";
            windowObj.PositionAddComment.Text = "";
            windowObj.PositionAddGrid.Visibility = Visibility.Visible;
        }
    }
}
