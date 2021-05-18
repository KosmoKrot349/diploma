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
            string oblswork = "'{ ";
            string obem = "'{ ";
            string states = "'{ ";
            string stavki = "'{ ";
            bool b = false;
            for (int i = 0; i < windowObj.checkBoxArrServiceWorks.Length; i++)
            {
                if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true && windowObj.textBoxArrVolumeWork[i].Text == "") { System.Windows.Forms.MessageBox.Show("Объём работ не заполнен"); return; }
                else { if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true) { b = true; oblswork += windowObj.checkBoxArrServiceWorks[i].Name.Split('_')[2] + ","; obem += windowObj.textBoxArrVolumeWork[i].Text.Replace(',', '.') + ","; } }
            }
            for (int i = 0; i < windowObj.checkBoxArrPositions.Length; i++)
            {
                if (windowObj.checkBoxArrPositions[i].IsChecked == true && windowObj.textBoxArrRate[i].Text == "") { System.Windows.Forms.MessageBox.Show("Ставки не указаны"); return; }
                else
                { if (windowObj.checkBoxArrPositions[i].IsChecked == true) { b = true; states += windowObj.checkBoxArrPositions[i].Name.Split('_')[2] + ","; stavki += windowObj.textBoxArrRate[i].Text.Replace(',', '.') + ","; } }
            }
            if (b == false) { MessageBox.Show("Выберите хотя бы одну должность или работу для сотрудника"); return; }


            states = states.Substring(0, states.Length - 1) + "}'";
            stavki = stavki.Substring(0, stavki.Length - 1) + "}'";
            oblswork = oblswork.Substring(0, oblswork.Length - 1) + "}'";
            obem = obem.Substring(0, obem.Length - 1) + "}'";

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE shtat SET  states=" + states + ", stavky=" + stavki + ", obslwork=" + oblswork + ", obem=" + obem + " WHERE shtatid = " + windowObj.staffID;
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
            DataGridUpdater.updateDataGridShtat(windowObj.connectionString, windowObj.filtr.sql, windowObj.ShtatDataGrid);
            windowObj.HideAll();
            windowObj.ShtatGrid.Visibility = Visibility.Visible;
        }
    }
}
