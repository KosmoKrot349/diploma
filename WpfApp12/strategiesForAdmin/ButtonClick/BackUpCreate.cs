using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows;
using System.IO;
using System.Diagnostics;

namespace WpfApp12.strategiesForAdmin
{
    class BackUpCreate : IButtonClick
    {
        private AdminWindow windowObj;

        public BackUpCreate(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            if (windowObj.bckpName.Text != "")
            {
                string bckpname = windowObj.bckpName.Text;
                for (int i = 0; i < bckpname.Length; i++)
                {
                    if ((bckpname[i] >= 'а' && bckpname[i] <= 'я') || (bckpname[i] >= 'А' && bckpname[i] <= 'Я')) { MessageBox.Show("В имени копии не должно быть русскких символов"); return; }
                }

            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            StreamReader.Close();
            StreamReader StreamReader2 = new StreamReader(@"setting.txt");
            ArrayList arLs2 = new ArrayList();
            while (!StreamReader2.EndOfStream)
            {
                arLs2.Add(StreamReader2.ReadLine());
            }
            StreamReader2.Close();
            object[] batStrMas2 = arLs2.ToArray();

            string batLastStr = "pg_dump -d postgresql://postgres:" + batStrMas2[1].ToString().Split(':')[1] + "@" + batStrMas2[0].ToString().Split(':')[1] + ":" + batStrMas2[2].ToString().Split(':')[1] + "/db > ";
            if (windowObj.bckpName.Text == "")
            {
                DateTime a = DateTime.Now;
                batLastStr += windowObj.bckpPyt.Text + "" + a.Day + "_" + a.Month + "_" + a.Year + "_" + a.Hour + "_" + a.Minute + "_" + a.Second + ".sql";
            }
            else
            {
                batLastStr += windowObj.bckpPyt.Text + windowObj.bckpName.Text + ".sql";

            }
            batStrMas[2] = batLastStr;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();

            Process.Start("crDump.bat");
        }
    }
}
