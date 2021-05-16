using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class SalaryPay:IButtonClick
    {
        BuhgalterWindow windowObj;

        public SalaryPay(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.NachDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Выплата прервана, Вы не выбрали запись для выплаты."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
           
            if (Convert.ToInt32(arr[9])>0)
            {
                DateIn wind = new DateIn();
                wind.gridViplataZp.Visibility = Visibility.Visible;
                wind.zapid = Convert.ToInt32(arr[0]);
                wind.topay = Convert.ToDouble(arr[9]);
                wind.constr = windowObj.connectionString;
                wind.Owner = windowObj;
                wind.ShowDialog();
                windowObj.NachDataGrid.SelectedItem = null;
                windowObj.ViplataBut.IsEnabled = false;
                DataGridUpdater.updateGridNachZp(windowObj.connectionString, windowObj.NachMonthLabel, windowObj.ChbxMas_SotrNuch, windowObj.NachSotrGrid, windowObj.NachDataGrid, windowObj.dateNuch);
            }
        }
    }
}
