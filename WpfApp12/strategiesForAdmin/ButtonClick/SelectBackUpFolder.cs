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
            WinForms.FolderBrowserDialog FilderBrowserDialog = new WinForms.FolderBrowserDialog();
            if (FilderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FilderBrowserDialog.SelectedPath.Length; i++)
                {
                    if ((FilderBrowserDialog.SelectedPath[i] >= 'а' && FilderBrowserDialog.SelectedPath[i] <= 'я') || (FilderBrowserDialog.SelectedPath[i] >= 'А' && FilderBrowserDialog.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                if (but.Name == "SelectBackUpPathNextYear") windowObj.BackUpFilePathGoToNextYear.Text = FilderBrowserDialog.SelectedPath + "\\";
                else
                    windowObj.BackUpPathCreateBackUp.Text = FilderBrowserDialog.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList ListFromBatFile = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                ListFromBatFile.Add(StreamReader.ReadLine());
            }
            object[] stringArrFromBatFile = ListFromBatFile.ToArray();
            string batLastStr = stringArrFromBatFile[2].ToString();
            StreamReader.Close();
            int PathIndex = 0;
            for (int i = 0; i < batLastStr.Length; i++)
            {
                if (batLastStr[i] == '>') PathIndex = i + 1;
            }
            StringBuilder newBatLastStr = new StringBuilder(batLastStr.Substring(0, PathIndex) + " " + FilderBrowserDialog.SelectedPath + "\\");
            stringArrFromBatFile[2] = newBatLastStr;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < stringArrFromBatFile.Length; i++)
            {
                StreamWriter.WriteLine(stringArrFromBatFile[i]);
            }

            StreamWriter.Close();
        }
    }
}
