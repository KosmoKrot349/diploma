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
        DirectorWindow windowObj;

        public ChangeStaffEmplyee(DirectorWindow windowObj)
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
            for (int i = 0; i < windowObj.chbxMas_obslwork.Length; i++)
            {
                if (windowObj.chbxMas_obslwork[i].IsChecked == true && windowObj.tbxMas_obem[i].Text == "") { System.Windows.Forms.MessageBox.Show("Объём работ не заполнен"); return; }
                else { if (windowObj.chbxMas_obslwork[i].IsChecked == true) { b = true; oblswork += windowObj.chbxMas_obslwork[i].Name.Split('_')[2] + ","; obem += windowObj.tbxMas_obem[i].Text.Replace(',', '.') + ","; } }
            }
            for (int i = 0; i < windowObj.chbxMas_state.Length; i++)
            {
                if (windowObj.chbxMas_state[i].IsChecked == true && windowObj.tbxMas_stavki[i].Text == "") { System.Windows.Forms.MessageBox.Show("Ставки не указаны"); return; }
                else
                { if (windowObj.chbxMas_state[i].IsChecked == true) { b = true; states += windowObj.chbxMas_state[i].Name.Split('_')[2] + ","; stavki += windowObj.tbxMas_stavki[i].Text.Replace(',', '.') + ","; } }
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
                string sql = "UPDATE shtat SET  states=" + states + ", stavky=" + stavki + ", obslwork=" + oblswork + ", obem=" + obem + " WHERE shtatid = " + windowObj.ShtatID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE sotrudniki SET fio='" + windowObj.fioChangeShtat.Text + "' WHERE sotrid=(select sotrid from shtat where shtatid =" + windowObj.ShtatID + ")";
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
