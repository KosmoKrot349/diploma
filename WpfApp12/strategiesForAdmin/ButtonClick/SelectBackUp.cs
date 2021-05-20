using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class SelectBackUp:IButtonClick
    {
        private AdminWindow windowObject;

        public SelectBackUp(AdminWindow windowObject)
        {
            this.windowObject = windowObject;
        }

        public void buttonClick()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            if (fileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < fileDialog.FileName.Length; i++)
                {
                    if ((fileDialog.FileName[i] >= 'а' && fileDialog.FileName[i] <= 'я') || (fileDialog.FileName[i] >= 'А' && fileDialog.FileName[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                windowObject.BackUpPathRestore.Text = fileDialog.FileName;
            }
        }
    }
}
