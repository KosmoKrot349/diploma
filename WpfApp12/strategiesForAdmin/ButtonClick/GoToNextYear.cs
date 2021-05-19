using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace WpfApp12.strategiesForAdmin
{
    class GoToNextYear:IButtonClick
    {
        private AdminWindow windowObj;

        public GoToNextYear(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            windowObj.MenuRolesA.BorderBrush = null;
            windowObj.usersMenu.BorderBrush = null;
            windowObj.arhivMenu.BorderBrush = null;
            windowObj.settingMenu.BorderBrush = null;
            windowObj.ToNextYearMenu.BorderBrush = Brushes.DarkRed;
            windowObj.hideAll();
            windowObj.NextYearGrid.Visibility = Visibility.Visible;
            windowObj.bckpNameNextYear.Text = "";
            windowObj.bckpPytNextYear.Text = "";
            windowObj.sybdPytNextYear.Text = "";
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] createBackUpList = arLs.ToArray();
            string DBPathString = createBackUpList[1].ToString().Substring(2);
            windowObj.sybdPytNextYear.Text = DBPathString.Trim(' ');
            string BackUpPathString = createBackUpList[2].ToString();
            int PathIndex = 0;

            for (int i = 0; i < BackUpPathString.Length; i++)
            {
                if (BackUpPathString[i] == '>') PathIndex = i + 1;
            }
            string[] FoldersArr = BackUpPathString.Substring(PathIndex).Split('\\');
            for (int i = 0; i < FoldersArr.Length - 1; i++)
            { windowObj.bckpPytNextYear.Text += FoldersArr[i] + "\\"; }
            windowObj.bckpPytNextYear.Text = windowObj.bckpPytNextYear.Text.Trim(' ');
            StreamReader.Close();
        }
    }
}
