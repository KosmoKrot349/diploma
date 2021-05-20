using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddListener:IButtonClick
    {
        ManagerWindow windowObj;

        public AddListener(ManagerWindow windowobj)
        {
            this.windowObj = windowobj;
        }

        public void ButtonClick()
        {
            if (windowObj.ListenerAddName.Text == "" || windowObj.ListenerAddPhones.Text == "") { MessageBox.Show("Поля не заполнены или заполнены не правильно."); return; }
            bool b = false;
            string groopsOfListenerArr = "'{";
            ArrayList ls = new ArrayList();
            string sql = "select grid from groups where ";
            for (int i = 0; i < windowObj.checkBoxArrForListeners.Length; i++)
            {
                if (windowObj.checkBoxArrForListeners[i].IsChecked == true)
                {
                    b = true;
                    sql += "nazvanie='" + windowObj.checkBoxArrForListeners[i].Content.ToString().Substring(0, windowObj.checkBoxArrForListeners[i].Content.ToString().Length - 9) + "' or ";
                    if (windowObj.textBoxArrForListeners[i].Text != "" && Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text) > 100) { MessageBox.Show("Процент не может быть больше 100"); return; }
                    if (windowObj.textBoxArrForListeners[i].Text != "") groopsOfListenerArr += Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text).ToString().Replace(',', '.') + ",";
                    else
                        groopsOfListenerArr += "0,";
                }

            }
            groopsOfListenerArr = groopsOfListenerArr.Substring(0, groopsOfListenerArr.Length - 1) + "}'";
            sql = sql.Substring(0, sql.Length - 3) + " order by grid";
            if (b == false) { MessageBox.Show("Группа не выбрана"); return; }
            string groopsIdArr = "'{";
            ArrayList GroupList = new ArrayList();
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        groopsIdArr += reader.GetInt32(0).ToString() + ",";
                        GroupList.Add(reader.GetInt32(0));
                    }

                }
                con.Close();
                groopsIdArr = groopsIdArr.Substring(0, groopsIdArr.Length - 1) + "}'";
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int listenerID = -1;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql2 = "INSERT INTO listeners(fio, phones, grid, lgt, comment)VALUES ('" + windowObj.ListenerAddName.Text + "', '" + windowObj.ListenerAddPhones.Text + "', " + groopsIdArr + ", " + groopsOfListenerArr + ", '" + windowObj.ListenerAddComment.Text + "') returning listenerid";
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listenerID = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //добавление записей в таблицу оплаты
            for (int i = 0; i < GroupList.Count; i++)
            {
                double[] payMonth = new double[12];
                //получение массива оплаты за группу
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sqll = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12] from groups where grid= " + GroupList[i];

                    NpgsqlCommand com = new NpgsqlCommand(sqll, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            payMonth[0] = reader.GetDouble(0);
                            payMonth[1] = reader.GetDouble(1);
                            payMonth[2] = reader.GetDouble(2);
                            payMonth[3] = reader.GetDouble(3);
                            payMonth[4] = reader.GetDouble(4);
                            payMonth[5] = reader.GetDouble(5);
                            payMonth[6] = reader.GetDouble(6);
                            payMonth[7] = reader.GetDouble(7);
                            payMonth[8] = reader.GetDouble(8);
                            payMonth[9] = reader.GetDouble(9);
                            payMonth[10] = reader.GetDouble(10);
                            payMonth[11] = reader.GetDouble(11);
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                //вычисление оплаты для студента
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sqll = "select lgt[array_position(grid,'" + GroupList[i] + "')] from listeners where listenerid= " + listenerID;

                    NpgsqlCommand com = new NpgsqlCommand(sqll, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            payMonth[0] = Math.Round(payMonth[0] - payMonth[0] * reader.GetDouble(0) / 100, 2);
                            payMonth[1] = Math.Round(payMonth[1] - payMonth[1] * reader.GetDouble(0) / 100, 2);
                            payMonth[2] = Math.Round(payMonth[2] - payMonth[2] * reader.GetDouble(0) / 100, 2);
                            payMonth[3] = Math.Round(payMonth[3] - payMonth[3] * reader.GetDouble(0) / 100, 2);
                            payMonth[4] = Math.Round(payMonth[4] - payMonth[4] * reader.GetDouble(0) / 100, 2);
                            payMonth[5] = Math.Round(payMonth[5] - payMonth[5] * reader.GetDouble(0) / 100, 2);
                            payMonth[6] = Math.Round(payMonth[6] - payMonth[6] * reader.GetDouble(0) / 100, 2);
                            payMonth[7] = Math.Round(payMonth[7] - payMonth[7] * reader.GetDouble(0) / 100, 2);
                            payMonth[8] = Math.Round(payMonth[8] - payMonth[8] * reader.GetDouble(0) / 100, 2);
                            payMonth[9] = Math.Round(payMonth[9] - payMonth[9] * reader.GetDouble(0) / 100, 2);
                            payMonth[10] = Math.Round(payMonth[10] - payMonth[10] * reader.GetDouble(0) / 100, 2);
                            payMonth[11] = Math.Round(payMonth[11] - payMonth[11] * reader.GetDouble(0) / 100, 2);
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                string PayArr = "'{";
                for (int i2 = 0; i2 < 12; i2++)
                {
                    PayArr += payMonth[i2].ToString().Replace(',', '.') + ",";
                }
                PayArr = PayArr.Substring(0, PayArr.Length - 1) + "}'";
                //добавление записи в таблицу
                try
                {

                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql2 = "INSERT INTO listnuch(listenerid, grid, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose) VALUES(" + listenerID + ", " + GroupList[i] + ", " + PayArr + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', '{0,0,0,0,0,0,0,0,0,0,0,0}', " + PayArr + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', null, 0)";
                    NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }


            MessageBoxResult res = MessageBox.Show("Слушатель добавлен. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.ListenerAddName.Text = "";
                windowObj.ListenerAddPhones.Text = "";
                windowObj.ListenerAddComment.Text = "";
                for (int i = 0; i < windowObj.checkBoxArrForListeners.Length; i++)
                { windowObj.checkBoxArrForListeners[i].IsChecked = false; }
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();

                windowObj.ListenersDataGrid.SelectedItem = null;
                //слушатели
                windowObj.listenerDeleteButton.IsEnabled = false;
                windowObj.listenerChangeButton.IsEnabled = false;

                DataGridUpdater.updateListenerDataGrid(windowObj);
                windowObj.ListenerGrid.Visibility = Visibility.Visible;

            }
        }
    }
}
