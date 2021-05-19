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
                string backUpName = windowObj.bckpName.Text;
                for (int i = 0; i < backUpName.Length; i++)
                {
                    if ((backUpName[i] >= 'а' && backUpName[i] <= 'я') || (backUpName[i] >= 'А' && backUpName[i] <= 'Я')) { MessageBox.Show("В имени копии не должно быть русскких символов"); return; }
                }

            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList createBackUpList = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                createBackUpList.Add(StreamReader.ReadLine());
            }
            object[] stringArrFromBatFile = createBackUpList.ToArray();
            StreamReader.Close();
            StreamReader StreamReader2 = new StreamReader(@"setting.txt");
            ArrayList settingList = new ArrayList();
            while (!StreamReader2.EndOfStream)
            {
                settingList.Add(StreamReader2.ReadLine());
            }
            StreamReader2.Close();
            object[] stringArrFromSettingFile = settingList.ToArray();

            string lastStringForBatFile = "pg_dump -d postgresql://postgres:" + stringArrFromSettingFile[1].ToString().Split(':')[1] + "@" + stringArrFromSettingFile[0].ToString().Split(':')[1] + ":" + stringArrFromSettingFile[2].ToString().Split(':')[1] + "/db > ";
            if (windowObj.bckpName.Text == "")
            {
                DateTime dateNow = DateTime.Now;
                lastStringForBatFile += windowObj.bckpPyt.Text + "" + dateNow.Day + "_" + dateNow.Month + "_" + dateNow.Year + "_" + dateNow.Hour + "_" + dateNow.Minute + "_" + dateNow.Second + ".sql";
            }
            else
            {
                lastStringForBatFile += windowObj.bckpPyt.Text + windowObj.bckpName.Text + ".sql";

            }
            stringArrFromBatFile[2] = lastStringForBatFile;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < stringArrFromBatFile.Length; i++)
            {
                StreamWriter.WriteLine(stringArrFromBatFile[i]);
            }

            StreamWriter.Close();

            Process.Start("crDump.bat");
        }
    }
}
