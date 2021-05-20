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
        ManagerWindow windowObj;

        public AddEmployeeToStaff(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            string serviceWorkArr = "'{ ";
            string volumeArr = "'{ ";
            string positionsArr = "'{ ";
            string ratesArr = "'{ ";
            bool b = false;
            for (int i = 0; i < windowObj.checkBoxArrServiceWorks.Length; i++)
            {
                if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true && windowObj.textBoxArrVolumeWork[i].Text == "") { MessageBox.Show("Обьём работ не заполнен"); return; }
                else { if (windowObj.checkBoxArrServiceWorks[i].IsChecked == true) { b = true; serviceWorkArr += windowObj.checkBoxArrServiceWorks[i].Name.Split('_')[2] + ","; volumeArr += windowObj.textBoxArrVolumeWork[i].Text.Replace(',', '.') + ","; } }
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
            volumeArr = volumeArr.Substring(0, volumeArr.Length - 1) + "}'";

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO shtat( sotrid, states, stavky, obslwork, obem) VALUES ( " + windowObj.employeeID + ", " + positionsArr + ", " + ratesArr + ", " + serviceWorkArr + ", " + volumeArr + ")";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE shtatrasp SET  shtatid=array_append(shtatid," + windowObj.employeeID + ") WHERE extract(Month from date)=" + DateTime.Now.Month;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            MessageBox.Show("Сотрудник определён как штатный работник");
            DataGridUpdater.updateEmploeesDataGrid(windowObj);
            windowObj.HideAll();
            windowObj.EmployeesGrid.Visibility = Visibility.Visible;
        }
    }
}
