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
            if (windowObj.GroopChangeTitle.Text == "" || windowObj.GroopChangePayForYear.Content.ToString() == "") { MessageBox.Show("Поле названия или оплаты не заполнено"); return; }
            //проверка существования группы
            if (windowObj.GroopChangeTitle.Text != windowObj.dontChangeGroopName)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = ("select grid from groups where nazvanie ='" + windowObj.GroopChangeTitle.Text + "'");
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
                string sql = "select courseid from courses where title ='" + windowObj.GroopChangeCourse.SelectedItem + "'";
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
            if (windowObj.GroopChangePaymentFor1Month.Text != "") montPay[0] = Convert.ToDouble(windowObj.GroopChangePaymentFor1Month.Text);
            if (windowObj.GroopChangePaymentFor2Month.Text != "") montPay[1] = Convert.ToDouble(windowObj.GroopChangePaymentFor2Month.Text);
            if (windowObj.GroopChangePaymentFor3Month.Text != "") montPay[2] = Convert.ToDouble(windowObj.GroopChangePaymentFor3Month.Text);
            if (windowObj.GroopChangePaymentFor4Month.Text != "") montPay[3] = Convert.ToDouble(windowObj.GroopChangePaymentFor4Month.Text);
            if (windowObj.GroopChangePaymentFor5Month.Text != "") montPay[4] = Convert.ToDouble(windowObj.GroopChangePaymentFor5Month.Text);
            if (windowObj.GroopChangePaymentFor6Month.Text != "") montPay[5] = Convert.ToDouble(windowObj.GroopChangePaymentFor6Month.Text);
            if (windowObj.GroopChangePaymentFor7Month.Text != "") montPay[6] = Convert.ToDouble(windowObj.GroopChangePaymentFor7Month.Text);
            if (windowObj.GroopChangePaymentFor8Month.Text != "") montPay[7] = Convert.ToDouble(windowObj.GroopChangePaymentFor8Month.Text);
            if (windowObj.GroopChangePaymentFor9Month.Text != "") montPay[8] = Convert.ToDouble(windowObj.GroopChangePaymentFor9Month.Text);
            if (windowObj.GroopChangePaymentFor10Month.Text != "") montPay[9] = Convert.ToDouble(windowObj.GroopChangePaymentFor10Month.Text);
            if (windowObj.GroopChangePaymentFor11Month.Text != "") montPay[10] = Convert.ToDouble(windowObj.GroopChangePaymentFor11Month.Text);
            if (windowObj.GroopChangePaymentFor12Month.Text != "") montPay[11] = Convert.ToDouble(windowObj.GroopChangePaymentFor12Month.Text);

            DateTime dateStartAdd = Convert.ToDateTime(windowObj.GroopChangeDateStartLearn.Text);
            DateTime dateEndAdd = Convert.ToDateTime(windowObj.GroopChangeDateEndLearn.Text);
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
                string sql = "UPDATE groups SET date_start='" + dateStartAdd.ToShortDateString().Replace('.', '-') + "',date_end='" + dateEndAdd.ToShortDateString().Replace('.', '-') + "',courseid =" + courseID + ", nazvanie ='" + windowObj.GroopChangeTitle.Text + "', comment ='" + windowObj.GroopChangeComment.Text + "', payment ='{" + montPay[0].ToString().Replace(',', '.') + "," + montPay[1].ToString().Replace(',', '.') + "," + montPay[2].ToString().Replace(',', '.') + "," + montPay[3].ToString().Replace(',', '.') + "," + montPay[4].ToString().Replace(',', '.') + "," + montPay[5].ToString().Replace(',', '.') + "," + montPay[6].ToString().Replace(',', '.') + "," + montPay[7].ToString().Replace(',', '.') + "," + montPay[8].ToString().Replace(',', '.') + "," + montPay[9].ToString().Replace(',', '.') + "," + montPay[10].ToString().Replace(',', '.') + "," + montPay[11].ToString().Replace(',', '.') + "}' WHERE grid=" + windowObj.groopID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.GroopsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateGroopsDataGrid(windowObj);
        }
    }
}
