using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections;

namespace WpfApp12.strategiesForAdmin.MenuClick
{
    class GoToCreateBackUp : IMenuClick
    {
        private AdminWindow windowObj;

        public GoToCreateBackUp(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.hideAll();
            windowObj.CreateBackUpGrid.Visibility = Visibility.Visible;

            windowObj.BackUpFileNameCreateBackUp.Text = "";
            windowObj.BackUpPathCreateBackUp.Text = "";
            windowObj.DBPathCreateBackUp.Text = "";
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList createBackUpList = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                createBackUpList.Add(StreamReader.ReadLine());
            }
            object[] stringArrFromBatFile = createBackUpList.ToArray();
            string DBPathString = stringArrFromBatFile[1].ToString().Substring(2);
            windowObj.DBPathCreateBackUp.Text = DBPathString.Trim(' ');
            string BackUpPathString = stringArrFromBatFile[2].ToString();
            int PathIndex = 0;

            for (int i = 0; i < BackUpPathString.Length; i++)
            {
                if (BackUpPathString[i] == '>') PathIndex = i + 1;
            }
            string[] FoldersArr = BackUpPathString.Substring(PathIndex).Split('\\');
            for (int i = 0; i < FoldersArr.Length - 1; i++)
            { windowObj.BackUpPathCreateBackUp.Text += FoldersArr[i] + "\\"; }
            windowObj.BackUpPathCreateBackUp.Text = windowObj.BackUpPathCreateBackUp.Text.Trim(' ');
            StreamReader.Close();
        }
    }
}
