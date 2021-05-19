using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeStaffEmplyee:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeStaffEmplyee(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.fioChangeShtat.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            string serviceWorkArr = "'{ ";
            string workVolumeArr = "'{ ";
            string positionsArr = "'{ ";
            string ratesArr = "'{ ";
            bool b = false;
            for (int i = 0; i < windowObj.checkBoxArrServiceWorks.Length; i++)
            {
                if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true && windowObj.textBoxArrVolumeWork[i].Text == "") { System.Windows.Forms.MessageBox.Show("Объём работ не заполнен"); return; }
                else { if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true) { b = true; serviceWorkArr += windowObj.checkBoxArrServiceWorks[i].Name.Split('_')[2] + ","; workVolumeArr += windowObj.textBoxArrVolumeWork[i].Text.Replace(',', '.') + ","; } }
            }
            for (int i = 0; i < windowObj.checkBoxArrPositions.Length; i++)
            {
                if (windowObj.checkBoxArrPositions[i].IsChecked == true && windowObj.textBoxArrRate[i].Text == "") { System.Windows.Forms.MessageBox.Show("Ставки не указаны"); return; }
                else
                { if (windowObj.checkBoxArrPositions[i].IsChecked == true) { b = true; positionsArr += windowObj.checkBoxArrPositions[i].Name.Split('_')[2] + ","; ratesArr += windowObj.textBoxArrRate[i].Text.Replace(',', '.') + ","; } }
            }
            if (b == false) { MessageBox.Show("Выберите хотя бы одну должность или работу для сотрудника"); return; }


            positionsArr = positionsArr.Substring(0, positionsArr.Length - 1) + "}'";
            ratesArr = ratesArr.Substring(0, ratesArr.Length - 1) + "}'";
            serviceWorkArr = serviceWorkArr.Substring(0, serviceWorkArr.Length - 1) + "}'";
            workVolumeArr = workVolumeArr.Substring(0, workVolumeArr.Length - 1) + "}'";

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE shtat SET  states=" + positionsArr + ", stavky=" + ratesArr + ", obslwork=" + serviceWorkArr + ", obem=" + workVolumeArr + " WHERE shtatid = " + windowObj.staffID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE sotrudniki SET fio='" + windowObj.fioChangeShtat.Text + "' WHERE sotrid=(select sotrid from shtat where shtatid =" + windowObj.staffID + ")";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateStaffDataGrid(windowObj);
            windowObj.HideAll();
            windowObj.ShtatGrid.Visibility = Visibility.Visible;
        }
    }
}
