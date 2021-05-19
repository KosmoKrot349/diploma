using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeGroop:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.grTitleCh.Text == "" || windowObj.payToYearCh.Content.ToString() == "") { MessageBox.Show("Поле названия или оплаты не заполнено"); return; }
            //проверка существования группы
            if (windowObj.grTitleCh.Text != windowObj.dontChangeGroopName)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = ("select grid from groups where nazvanie ='" + windowObj.grTitleCh.Text + "'");
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Группа с таким названием уже существует");
                        return;
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            //получение номера курса
            int courseID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select courseid from courses where title ='" + windowObj.grCourseCh.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        courseID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            double[] montPay = new double[12];
            if (windowObj.grPayment1Ch.Text != "") montPay[0] = Convert.ToDouble(windowObj.grPayment1Ch.Text);
            if (windowObj.grPayment2Ch.Text != "") montPay[1] = Convert.ToDouble(windowObj.grPayment2Ch.Text);
            if (windowObj.grPayment3Ch.Text != "") montPay[2] = Convert.ToDouble(windowObj.grPayment3Ch.Text);
            if (windowObj.grPayment4Ch.Text != "") montPay[3] = Convert.ToDouble(windowObj.grPayment4Ch.Text);
            if (windowObj.grPayment5Ch.Text != "") montPay[4] = Convert.ToDouble(windowObj.grPayment5Ch.Text);
            if (windowObj.grPayment6Ch.Text != "") montPay[5] = Convert.ToDouble(windowObj.grPayment6Ch.Text);
            if (windowObj.grPayment7Ch.Text != "") montPay[6] = Convert.ToDouble(windowObj.grPayment7Ch.Text);
            if (windowObj.grPayment8Ch.Text != "") montPay[7] = Convert.ToDouble(windowObj.grPayment8Ch.Text);
            if (windowObj.grPayment9Ch.Text != "") montPay[8] = Convert.ToDouble(windowObj.grPayment9Ch.Text);
            if (windowObj.grPayment10Ch.Text != "") montPay[9] = Convert.ToDouble(windowObj.grPayment10Ch.Text);
            if (windowObj.grPayment11Ch.Text != "") montPay[10] = Convert.ToDouble(windowObj.grPayment11Ch.Text);
            if (windowObj.grPayment12Ch.Text != "") montPay[11] = Convert.ToDouble(windowObj.grPayment12Ch.Text);

            DateTime dateStartAdd = Convert.ToDateTime(windowObj.DateStartCh.Text);
            DateTime dateEndAdd = Convert.ToDateTime(windowObj.DateEndCh.Text);
            if (dateEndAdd.Year - dateStartAdd.Year == 1 || dateEndAdd.Year - dateStartAdd.Year == 0)
            {
                if (dateStartAdd.Month > dateEndAdd.Month)
                {
                    if (dateStartAdd.Year >= dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }

                    for (int i = dateStartAdd.Month - 1; i < 12; i++)
                    {
                        if (montPay[i] == 0)
                        {
                            MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return;
                        }
                    }

                    for (int i = 0; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { MessageBox.Show("В месяце " + (i + 1) + "не стоит оплата, хотя он отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) {MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month < dateEndAdd.Month)
                {
                    if (dateStartAdd.Year != dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }
                    for (int i = 0; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < 12; i++)
                    { if (montPay[i] != 0) { MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateStartAdd.Month - 1; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month == dateEndAdd.Month)
                {
                    if (dateStartAdd.Year == dateEndAdd.Year)
                    {
                        for (int i = 0; i < dateStartAdd.Month - 1; i++)
                        { if (montPay[i] != 0) {MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                        for (int i = dateEndAdd.Month; i < 12; i++)
                        { if (montPay[i] != 0) {MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                    }
                    else
                    { MessageBox.Show("Дата введена не корректно"); return; }
                }
            }
            else { MessageBox.Show("Дата введена не корректно"); return; }

            //изменение группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE groups SET date_start='" + dateStartAdd.ToShortDateString().Replace('.', '-') + "',date_end='" + dateEndAdd.ToShortDateString().Replace('.', '-') + "',courseid =" + courseID + ", nazvanie ='" + windowObj.grTitleCh.Text + "', comment ='" + windowObj.grCommCh.Text + "', payment ='{" + montPay[0].ToString().Replace(',', '.') + "," + montPay[1].ToString().Replace(',', '.') + "," + montPay[2].ToString().Replace(',', '.') + "," + montPay[3].ToString().Replace(',', '.') + "," + montPay[4].ToString().Replace(',', '.') + "," + montPay[5].ToString().Replace(',', '.') + "," + montPay[6].ToString().Replace(',', '.') + "," + montPay[7].ToString().Replace(',', '.') + "," + montPay[8].ToString().Replace(',', '.') + "," + montPay[9].ToString().Replace(',', '.') + "," + montPay[10].ToString().Replace(',', '.') + "," + montPay[11].ToString().Replace(',', '.') + "}' WHERE grid=" + windowObj.groopID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.groupsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateGroopsDataGrid(windowObj);
        }
    }
}
