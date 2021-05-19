using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Collections;
using WinForms = System.Windows.Forms;

namespace WpfApp12.strategiesForAdmin
{
    class SelectFolderBackUpRestore:IButtonClick
    {
        private AdminWindow windowObj;

        public SelectFolderBackUpRestore(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            WinForms.FolderBrowserDialog FolderBrowserDialog = new WinForms.FolderBrowserDialog();
            if (FolderBrowserDialog.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FolderBrowserDialog.SelectedPath.Length; i++)
                {
                    if ((FolderBrowserDialog.SelectedPath[i] >= 'а' && FolderBrowserDialog.SelectedPath[i] <= 'я') || (FolderBrowserDialog.SelectedPath[i] >= 'А' && FolderBrowserDialog.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                windowObj.rsSybdPyt.Text = FolderBrowserDialog.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList ListFromBatFile = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                ListFromBatFile.Add(StreamReader.ReadLine());
            }
            object[] StringArrFromBatFile = ListFromBatFile.ToArray();
            string[] disk = FolderBrowserDialog.SelectedPath.Split('\\');
            StringArrFromBatFile[0] = disk[0];
            StringArrFromBatFile[1] = "cd " + FolderBrowserDialog.SelectedPath;
            StreamReader.Close();
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < StringArrFromBatFile.Length; i++)
            {
                StreamWriter.WriteLine(StringArrFromBatFile[i]);
            }

            StreamWriter.Close();
        }
    }
}
