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
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            windowObj.sybdPyt.Text = splitMas1.Trim(' ');
            string splitMas2 = batStrMas[2].ToString();
            int index_puti = 0;

            for (int i = 0; i < splitMas2.Length; i++)
            {
                if (splitMas2[i] == '>') index_puti = i + 1;
            }
            string[] masFolders = splitMas2.Substring(index_puti).Split('\\');
            for (int i = 0; i < masFolders.Length - 1; i++)
            { windowObj.bckpPyt.Text += masFolders[i] + "\\"; }
            windowObj.bckpPyt.Text = windowObj.bckpPyt.Text.Trim(' ');
            StreamReader.Close();
        }
    }
}
