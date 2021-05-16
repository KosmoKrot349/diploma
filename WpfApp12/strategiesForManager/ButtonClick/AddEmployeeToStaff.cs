using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddEmployeeToStaff:IButtonClick
    {
        DirectorWindow windowObj;

        public AddEmployeeToStaff(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            string oblswork = "'{ ";
            string obem = "'{ ";
            string states = "'{ ";
            string stavki = "'{ ";
            bool b = false;
            for (int i = 0; i < windowObj.chbxMas_obslwork.Length; i++)
            {
                if (windowObj.chbxMas_obslwork[i].IsChecked == true && windowObj.tbxMas_obem[i].Text == "") { System.Windows.Forms.MessageBox.Show("Обьём работ не заполнен"); return; }
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
                string sql = "INSERT INTO shtat( sotrid, states, stavky, obslwork, obem) VALUES ( " + windowObj.sotrID + ", " + states + ", " + stavki + ", " + oblswork + ", " + obem + ")";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE shtatrasp SET  shtatid=array_append(shtatid," + windowObj.sotrID + ") WHERE extract(Month from date)=" + DateTime.Now.Month;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            MessageBox.Show("Сотрудник определён как штатный работник");
            DataGridUpdater.updateDataGridSotr(windowObj.connectionString, windowObj.sqlAllSotr, windowObj.ShtatDataGrid);
            windowObj.HideAll();
            windowObj.allSotrGrid.Visibility = Visibility.Visible;
        }
    }
}
