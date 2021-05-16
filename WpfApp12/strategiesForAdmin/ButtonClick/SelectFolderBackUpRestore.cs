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
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                windowObj.rsSybdPyt.Text = FBD.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string[] disk = FBD.SelectedPath.Split('\\');
            batStrMas[0] = disk[0];
            batStrMas[1] = "cd " + FBD.SelectedPath;
            StreamReader.Close();
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
    }
}
