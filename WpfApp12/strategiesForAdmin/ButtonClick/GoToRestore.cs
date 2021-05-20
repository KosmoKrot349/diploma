using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace WpfApp12.strategiesForAdmin
{
    class GoToRestore:IButtonClick
    {
        private AdminWindow windowObj;

        public GoToRestore(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            windowObj.hideAll();
            windowObj.rsDumpGrid.Visibility = Visibility.Visible;

            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList restoreBackUpList = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                restoreBackUpList.Add(StreamReader.ReadLine());
            }
            object[] StringArrFromBatFile = restoreBackUpList.ToArray();
            string DBPathString = StringArrFromBatFile[1].ToString().Substring(2);
            windowObj.DBPathRestore.Text = DBPathString;
            string FileString = StringArrFromBatFile[2].ToString();
            StreamReader.Close();
        }
    }
}
