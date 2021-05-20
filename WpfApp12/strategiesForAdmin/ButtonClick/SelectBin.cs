using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;
using System.IO;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class SelectBin : IButtonClick
    {
        private AdminWindow windowObj;
        private object sender;

        public SelectBin(AdminWindow windowObj,object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void buttonClick()
        {
            Button but = sender as Button;
            WinForms.FolderBrowserDialog FolderBrowserDialog = new WinForms.FolderBrowserDialog();
            if (FolderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FolderBrowserDialog.SelectedPath.Length; i++)
                {
                    if ((FolderBrowserDialog.SelectedPath[i] >= 'а' && FolderBrowserDialog.SelectedPath[i] <= 'я') || (FolderBrowserDialog.SelectedPath[i] >= 'А' && FolderBrowserDialog.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }

                if (but.Name == "SelectBinPathNextYear") windowObj.DBPathGoToNextYear.Text = FolderBrowserDialog.SelectedPath + "\\";
                else
                    windowObj.DBPathCreateBackUp.Text = FolderBrowserDialog.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList ListFromBatFile = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                ListFromBatFile.Add(StreamReader.ReadLine());
            }
            object[] StringArrFrombatFile = ListFromBatFile.ToArray();
            string[] disk = FolderBrowserDialog.SelectedPath.Split('\\');
            StringArrFrombatFile[0] = disk[0];
            StringArrFrombatFile[1] = "cd " + FolderBrowserDialog.SelectedPath;
            StreamReader.Close();
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < StringArrFrombatFile.Length; i++)
            {
                StreamWriter.WriteLine(StringArrFrombatFile[i]);
            }

            StreamWriter.Close();
        }
    }
}
