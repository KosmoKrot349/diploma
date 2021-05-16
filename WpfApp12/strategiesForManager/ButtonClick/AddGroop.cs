using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddGroop:IButtonClick
    {
        DirectorWindow windowObj;

        public AddGroop(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.grTitle.Text == "" || windowObj.payToYear.Content.ToString() == "" || windowObj.DateStartGrAdd.Text == "" || windowObj.DateEndGrAdd.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            //проверка существования группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = ("select grid from groups where nazvanie ='" + windowObj.grTitle.Text + "'");
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
            //получение номера курса
            int courseID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select courseid from courses where title ='" + windowObj.grCourse.SelectedItem + "'";
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
            //добавление группы
            double[] montPay = new double[12];
            if (windowObj.grPayment1.Text != "") montPay[0] = Convert.ToDouble(windowObj.grPayment1.Text);
            if (windowObj.grPayment2.Text != "") montPay[1] = Convert.ToDouble(windowObj.grPayment2.Text);
            if (windowObj.grPayment3.Text != "") montPay[2] = Convert.ToDouble(windowObj.grPayment3.Text);
            if (windowObj.grPayment4.Text != "") montPay[3] = Convert.ToDouble(windowObj.grPayment4.Text);
            if (windowObj.grPayment5.Text != "") montPay[4] = Convert.ToDouble(windowObj.grPayment5.Text);
            if (windowObj.grPayment6.Text != "") montPay[5] = Convert.ToDouble(windowObj.grPayment6.Text);
            if (windowObj.grPayment7.Text != "") montPay[6] = Convert.ToDouble(windowObj.grPayment7.Text);
            if (windowObj.grPayment8.Text != "") montPay[7] = Convert.ToDouble(windowObj.grPayment8.Text);
            if (windowObj.grPayment9.Text != "") montPay[8] = Convert.ToDouble(windowObj.grPayment9.Text);
            if (windowObj.grPayment10.Text != "") montPay[9] = Convert.ToDouble(windowObj.grPayment10.Text);
            if (windowObj.grPayment11.Text != "") montPay[10] = Convert.ToDouble(windowObj.grPayment11.Text);
            if (windowObj.grPayment12.Text != "") montPay[11] = Convert.ToDouble(windowObj.grPayment12.Text);
            //проверка оплаты за нужные месяца

            DateTime dateStartAdd = Convert.ToDateTime(windowObj.DateStartGrAdd.Text);
            DateTime dateEndAdd = Convert.ToDateTime(windowObj.DateEndGrAdd.Text);
            if (dateEndAdd.Year - dateStartAdd.Year == 1 || dateEndAdd.Year - dateStartAdd.Year == 0)
            {
                if (dateStartAdd.Month > dateEndAdd.Month)
                {
                    if (dateStartAdd.Year >= dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }

                    for (int i = dateStartAdd.Month - 1; i < 12; i++)
                    {
                        if (montPay[i] == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return;
                        }
                    }

                    for (int i = 0; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + "не стоит оплата, хотя он отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month < dateEndAdd.Month)
                {
                    if (dateStartAdd.Year != dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }
                    for (int i = 0; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < 12; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateStartAdd.Month - 1; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month == dateEndAdd.Month)
                {
                    if (dateStartAdd.Year == dateEndAdd.Year)
                    {
                        for (int i = 0; i < dateStartAdd.Month - 1; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце1 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                        for (int i = dateEndAdd.Month; i < 12; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце2 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                    }
                    else
                    { System.Windows.Forms.MessageBox.Show("Дата введена не корректно"); return; }
                }
            }
            else { MessageBox.Show("Дата введена не корректно"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO groups(courseid, nazvanie, comment, payment,date_start,date_end) VALUES(" + courseID + ",'" + windowObj.grTitle.Text + "' ,'" + windowObj.grComm.Text + "' , '{" + montPay[0].ToString().Replace(',', '.') + "," + montPay[1].ToString().Replace(',', '.') + "," + montPay[2].ToString().Replace(',', '.') + "," + montPay[3].ToString().Replace(',', '.') + "," + montPay[4].ToString().Replace(',', '.') + "," + montPay[5].ToString().Replace(',', '.') + "," + montPay[6].ToString().Replace(',', '.') + "," + montPay[7].ToString().Replace(',', '.') + "," + montPay[8].ToString().Replace(',', '.') + "," + montPay[9].ToString().Replace(',', '.') + "," + montPay[10].ToString().Replace(',', '.') + "," + montPay[11].ToString().Replace(',', '.') + "}','" + dateStartAdd.ToShortDateString().Replace('.', '-') + "','" + dateEndAdd.ToShortDateString().Replace('.', '-') + "' ); ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();



            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            var btn = MessageBox.Show("Группа добавлена. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (btn == MessageBoxResult.Yes)
            {
                windowObj.grTitle.Text = "";
                windowObj.grComm.Text = "";
                windowObj.grCourse.SelectedIndex = 0;
                windowObj.grPayment1.Text = "";
                windowObj.grPayment2.Text = "";
                windowObj.grPayment3.Text = "";
                windowObj.grPayment4.Text = "";
                windowObj.grPayment5.Text = "";
                windowObj.grPayment6.Text = "";
                windowObj.grPayment7.Text = "";
                windowObj.grPayment8.Text = "";
                windowObj.grPayment9.Text = "";
                windowObj.grPayment10.Text = "";
                windowObj.grPayment11.Text = "";
                windowObj.grPayment12.Text = "";
                windowObj.payToYear.Content = "";
                windowObj.DateStartGrAdd.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
                windowObj.DateEndGrAdd.Text = DateTime.Now.ToShortDateString();
            }
            if (btn == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.groupsGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridGroups(windowObj.connectionString, windowObj.filtr.sql, windowObj.groupsDataGrid);
            }
        }
    }
}
