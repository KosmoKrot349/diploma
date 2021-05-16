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
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            windowObj.sybdPytNextYear.Text = splitMas1.Trim(' ');
            string splitMas2 = batStrMas[2].ToString();
            int index_puti = 0;

            for (int i = 0; i < splitMas2.Length; i++)
            {
                if (splitMas2[i] == '>') index_puti = i + 1;
            }
            string[] masFolders = splitMas2.Substring(index_puti).Split('\\');
            for (int i = 0; i < masFolders.Length - 1; i++)
            { windowObj.bckpPytNextYear.Text += masFolders[i] + "\\"; }
            windowObj.bckpPytNextYear.Text = windowObj.bckpPytNextYear.Text.Trim(' ');
            StreamReader.Close();
        }
    }
}
