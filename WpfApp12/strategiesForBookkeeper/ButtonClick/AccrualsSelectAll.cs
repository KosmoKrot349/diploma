using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class AccrualsSelectAll:IButtonClick
    {
        BookkeeperWindow windowObj;

        public AccrualsSelectAll(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.selected == false)
            {
                for (int i = 0; i < windowObj.checkBoxArrStaffForAccrual.Length; i++)
                {
                    windowObj.checkBoxArrStaffForAccrual[i].IsChecked = true;

                }
                windowObj.selected = true; return;
            }
            else
            {
                for (int i = 0; i < windowObj.checkBoxArrStaffForAccrual.Length; i++)
                {
                    windowObj.checkBoxArrStaffForAccrual[i].IsChecked = false;

                }
                windowObj.selected = false; return;
            }
        }
    }
}
