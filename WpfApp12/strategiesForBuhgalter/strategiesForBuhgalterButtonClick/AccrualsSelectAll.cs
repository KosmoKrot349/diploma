using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AccrualsSelectAll:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AccrualsSelectAll(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.selected == false)
            {
                for (int i = 0; i < windowObj.ChbxMas_SotrNuch.Length; i++)
                {
                    windowObj.ChbxMas_SotrNuch[i].IsChecked = true;

                }
                windowObj.selected = true; return;
            }
            else
            {
                for (int i = 0; i < windowObj.ChbxMas_SotrNuch.Length; i++)
                {
                    windowObj.ChbxMas_SotrNuch[i].IsChecked = false;

                }
                windowObj.selected = false; return;
            }
        }
    }
}
