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
        ManagerWindow windowObj;

        public AddGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.GroopAddTitle.Text == "" || windowObj.GroopAddPayForYear.Content.ToString() == "" || windowObj.GroopAddDateStartLearn.Text == "" || windowObj.GroopAddDateEndLearn.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            //проверка существования группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = ("select grid from groups where nazvanie ='" + windowObj.GroopAddTitle.Text + "'");
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
                string sql = "select courseid from courses where title ='" + windowObj.GroopAddCourse.SelectedItem + "'";
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
            double[] payInMonth = new double[12];
            if (windowObj.GroopAddPaymentFor1Month.Text != "") payInMonth[0] = Convert.ToDouble(windowObj.GroopAddPaymentFor1Month.Text);
            if (windowObj.GroopAddPaymentFor2Month.Text != "") payInMonth[1] = Convert.ToDouble(windowObj.GroopAddPaymentFor2Month.Text);
            if (windowObj.GroopAddPaymentFor3Month.Text != "") payInMonth[2] = Convert.ToDouble(windowObj.GroopAddPaymentFor3Month.Text);
            if (windowObj.GroopAddPaymentFor4Month.Text != "") payInMonth[3] = Convert.ToDouble(windowObj.GroopAddPaymentFor4Month.Text);
            if (windowObj.GroopAddPaymentFor5Month.Text != "") payInMonth[4] = Convert.ToDouble(windowObj.GroopAddPaymentFor5Month.Text);
            if (windowObj.GroopAddPaymentFor6Month.Text != "") payInMonth[5] = Convert.ToDouble(windowObj.GroopAddPaymentFor6Month.Text);
            if (windowObj.GroopAddPaymentFor7Month.Text != "") payInMonth[6] = Convert.ToDouble(windowObj.GroopAddPaymentFor7Month.Text);
            if (windowObj.GroopAddPaymentFor8Month.Text != "") payInMonth[7] = Convert.ToDouble(windowObj.GroopAddPaymentFor8Month.Text);
            if (windowObj.GroopAddPaymentFor9Month.Text != "") payInMonth[8] = Convert.ToDouble(windowObj.GroopAddPaymentFor9Month.Text);
            if (windowObj.GroopAddPaymentFor10Month.Text != "") payInMonth[9] = Convert.ToDouble(windowObj.GroopAddPaymentFor10Month.Text);
            if (windowObj.GroopAddPaymentFor11Month.Text != "") payInMonth[10] = Convert.ToDouble(windowObj.GroopAddPaymentFor11Month.Text);
            if (windowObj.GroopAddPaymentFor12Month.Text != "") payInMonth[11] = Convert.ToDouble(windowObj.GroopAddPaymentFor12Month.Text);
            //проверка оплаты за нужные месяца

            DateTime dateStartAdd = Convert.ToDateTime(windowObj.GroopAddDateStartLearn.Text);
            DateTime dateEndAdd = Convert.ToDateTime(windowObj.GroopAddDateEndLearn.Text);
            if (dateEndAdd.Year - dateStartAdd.Year == 1 || dateEndAdd.Year - dateStartAdd.Year == 0)
            {
                if (dateStartAdd.Month > dateEndAdd.Month)
                {
                    if (dateStartAdd.Year >= dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }

                    for (int i = dateStartAdd.Month - 1; i < 12; i++)
                    {
                        if (payInMonth[i] == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return;
                        }
                    }

                    for (int i = 0; i <= dateEndAdd.Month - 1; i++)
                    { if (payInMonth[i] == 0) {MessageBox.Show("В месяце " + (i + 1) + "не стоит оплата, хотя он отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < dateStartAdd.Month - 1; i++)
                    { if (payInMonth[i] != 0) {MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month < dateEndAdd.Month)
                {
                    if (dateStartAdd.Year != dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }
                    for (int i = 0; i < dateStartAdd.Month - 1; i++)
                    { if (payInMonth[i] != 0) { MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < 12; i++)
                    { if (payInMonth[i] != 0) { MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateStartAdd.Month - 1; i <= dateEndAdd.Month - 1; i++)
                    { if (payInMonth[i] == 0) { MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month == dateEndAdd.Month)
                {
                    if (dateStartAdd.Year == dateEndAdd.Year)
                    {
                        for (int i = 0; i < dateStartAdd.Month - 1; i++)
                        { if (payInMonth[i] != 0) { MessageBox.Show("В месяце1 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                        for (int i = dateEndAdd.Month; i < 12; i++)
                        { if (payInMonth[i] != 0) { MessageBox.Show("В месяце2 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                    }
                    else
                    {MessageBox.Show("Дата введена не корректно"); return; }
                }
            }
            else { MessageBox.Show("Дата введена не корректно"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO groups(courseid, nazvanie, comment, payment,date_start,date_end) VALUES(" + courseID + ",'" + windowObj.GroopAddTitle.Text + "' ,'" + windowObj.GroopAddComment.Text + "' , '{" + payInMonth[0].ToString().Replace(',', '.') + "," + payInMonth[1].ToString().Replace(',', '.') + "," + payInMonth[2].ToString().Replace(',', '.') + "," + payInMonth[3].ToString().Replace(',', '.') + "," + payInMonth[4].ToString().Replace(',', '.') + "," + payInMonth[5].ToString().Replace(',', '.') + "," + payInMonth[6].ToString().Replace(',', '.') + "," + payInMonth[7].ToString().Replace(',', '.') + "," + payInMonth[8].ToString().Replace(',', '.') + "," + payInMonth[9].ToString().Replace(',', '.') + "," + payInMonth[10].ToString().Replace(',', '.') + "," + payInMonth[11].ToString().Replace(',', '.') + "}','" + dateStartAdd.ToShortDateString().Replace('.', '-') + "','" + dateEndAdd.ToShortDateString().Replace('.', '-') + "' ); ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();



            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            var btn = MessageBox.Show("Группа добавлена. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (btn == MessageBoxResult.Yes)
            {
                windowObj.GroopAddTitle.Text = "";
                windowObj.GroopAddComment.Text = "";
                windowObj.GroopAddCourse.SelectedIndex = 0;
                windowObj.GroopAddPaymentFor1Month.Text = "";
                windowObj.GroopAddPaymentFor2Month.Text = "";
                windowObj.GroopAddPaymentFor3Month.Text = "";
                windowObj.GroopAddPaymentFor4Month.Text = "";
                windowObj.GroopAddPaymentFor5Month.Text = "";
                windowObj.GroopAddPaymentFor6Month.Text = "";
                windowObj.GroopAddPaymentFor7Month.Text = "";
                windowObj.GroopAddPaymentFor8Month.Text = "";
                windowObj.GroopAddPaymentFor9Month.Text = "";
                windowObj.GroopAddPaymentFor10Month.Text = "";
                windowObj.GroopAddPaymentFor11Month.Text = "";
                windowObj.GroopAddPaymentFor12Month.Text = "";
                windowObj.GroopAddPayForYear.Content = "";
                windowObj.GroopAddDateStartLearn.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
                windowObj.GroopAddDateEndLearn.Text = DateTime.Now.ToShortDateString();
            }
            if (btn == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.GroopsGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateGroopsDataGrid(windowObj);
            }
        }
    }
}
