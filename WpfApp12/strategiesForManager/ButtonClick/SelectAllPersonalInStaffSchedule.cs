using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class SelectAllPersonalInStaffSchedule:IButtonClick
    {
        DirectorWindow windowObj;

        public SelectAllPersonalInStaffSchedule(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.selectd == false)
            {
                for (int i = 0; i < windowObj.chbxMas_stateRasp.Length; i++)
                {
                    windowObj.chbxMas_stateRasp[i].IsChecked = true;
                }
                windowObj.selectd = true;
                return;
            }
            else
            {

                for (int i = 0; i < windowObj.chbxMas_stateRasp.Length; i++)
                {
                    windowObj.chbxMas_stateRasp[i].IsChecked = false;
                }
                windowObj.selectd = false;
                return;
            }
        }
    }
}
