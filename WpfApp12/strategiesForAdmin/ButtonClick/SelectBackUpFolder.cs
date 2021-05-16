using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Windows;
using System.Collections;

namespace WpfApp12.strategiesForAdmin
{
    class SelectBackUpFolder : IButtonClick
    {
        private object sender;
        private AdminWindow windowObj;

        public SelectBackUpFolder(AdminWindow windowObj, object sender)
        {
            this.sender = sender;
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            Button but = sender as Button;
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                if (but.Name == "selectbckpNextYear") windowObj.bckpPytNextYear.Text = FBD.SelectedPath + "\\";
                else
                    windowObj.bckpPyt.Text = FBD.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string batLastStr = batStrMas[2].ToString();
            StreamReader.Close();
            int index_puti = 0;
            for (int i = 0; i < batLastStr.Length; i++)
            {
                if (batLastStr[i] == '>') index_puti = i + 1;
            }
            StringBuilder newBatLastStr = new StringBuilder(batLastStr.Substring(0, index_puti) + " " + FBD.SelectedPath + "\\");
            batStrMas[2] = newBatLastStr;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
    }
}
