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
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }

                if (but.Name == "selectBinNextYear") windowObj.sybdPytNextYear.Text = FBD.SelectedPath + "\\";
                else
                    windowObj.sybdPyt.Text = FBD.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
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
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
    }
}
