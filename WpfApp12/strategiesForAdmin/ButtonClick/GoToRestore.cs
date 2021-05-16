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
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            windowObj.rsSybdPyt.Text = splitMas1;
            string splitMas2 = batStrMas[2].ToString();
            StreamReader.Close();
        }
    }
}
