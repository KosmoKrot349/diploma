using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class GoToChangeUser : IButtonClick
    {
        private AdminWindow windowObj;

        public GoToChangeUser(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            DataRowView DRV = windowObj.usersDGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменениею не выбрав запись."); return; }
            DataRow DR = DRV.Row;

            object[] arr = DR.ItemArray;
            windowObj.userID = Convert.ToInt32(arr[0]);
            windowObj.hideAll();
            windowObj.ChangeUserGrid.Visibility = Visibility.Visible;
            windowObj.uCFio.Text = arr[1].ToString();
            windowObj.uClog.Text = arr[2].ToString();
            windowObj.uCpas.Text = arr[3].ToString();
            if (Convert.ToString(arr[4]) == "Да")
            {
                windowObj.uCadm.IsChecked = true;
            }
            else windowObj.uCadm.IsChecked = false;
            if (Convert.ToString(arr[5]) == "Да")
            {
                windowObj.uCbh.IsChecked = true;
            }
            else windowObj.uCbh.IsChecked = false;
            if (Convert.ToString(arr[6]) == "Да")
            {
                windowObj.uCdr.IsChecked = true;
            }
            else windowObj.uCdr.IsChecked = false;
        }
    }
}
