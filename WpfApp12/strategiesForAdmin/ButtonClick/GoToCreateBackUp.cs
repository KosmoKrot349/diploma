using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Collections;

namespace WpfApp12.strategiesForAdmin
{
    class GoToCreateBackUp : IButtonClick
    {
        private AdminWindow windowObj;

        public GoToCreateBackUp(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            windowObj.hideAll();
            windowObj.crDumpGrid.Visibility = Visibility.Visible;

            windowObj.bckpName.Text = "";
            windowObj.bckpPyt.Text = "";
            windowObj.sybdPyt.Text = "";
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList createBackUpList = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                createBackUpList.Add(StreamReader.ReadLine());
            }
            object[] stringArrFromBatFile = createBackUpList.ToArray();
            string DBPathString = stringArrFromBatFile[1].ToString().Substring(2);
            windowObj.sybdPyt.Text = DBPathString.Trim(' ');
            string BackUpPathString = stringArrFromBatFile[2].ToString();
            int PathIndex = 0;

            for (int i = 0; i < BackUpPathString.Length; i++)
            {
                if (BackUpPathString[i] == '>') PathIndex = i + 1;
            }
            string[] FoldersArr = BackUpPathString.Substring(PathIndex).Split('\\');
            for (int i = 0; i < FoldersArr.Length - 1; i++)
            { windowObj.bckpPyt.Text += FoldersArr[i] + "\\"; }
            windowObj.bckpPyt.Text = windowObj.bckpPyt.Text.Trim(' ');
            StreamReader.Close();
        }
    }
}
