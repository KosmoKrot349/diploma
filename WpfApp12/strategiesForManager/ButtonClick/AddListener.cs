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
            if (windowObj.listenerFIO.Text == "" || windowObj.listenerPhones.Text == "") { MessageBox.Show("Поля не заполнены или заполнены не правильно."); return; }
            bool b = false;
            string grLgMas = "'{";
            ArrayList ls = new ArrayList();
            string sql = "select grid from groups where ";
            for (int i = 0; i < windowObj.checkBoxArrForListeners.Length; i++)
            {
                if (windowObj.checkBoxArrForListeners[i].IsChecked == true)
                {
                    b = true;
                    sql += "nazvanie='" + windowObj.checkBoxArrForListeners[i].Content.ToString().Substring(0, windowObj.checkBoxArrForListeners[i].Content.ToString().Length - 9) + "' or ";
                    if (windowObj.textBoxArrForListeners[i].Text != "" && Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text) > 100) { MessageBox.Show("Процент не может быть больше 100"); return; }
                    if (windowObj.textBoxArrForListeners[i].Text != "") grLgMas += Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text).ToString().Replace(',', '.') + ",";
                    else
                        grLgMas += "0,";
                }

            }
            grLgMas = grLgMas.Substring(0, grLgMas.Length - 1) + "}'";
            sql = sql.Substring(0, sql.Length - 3) + " order by grid";
            if (b == false) { MessageBox.Show("Группа не выбрана"); return; }
            string grMasId = "'{";
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
                        grMasId += reader.GetInt32(0).ToString() + ",";
                        GroupList.Add(reader.GetInt32(0));
                    }

                }
                con.Close();
                grMasId = grMasId.Substring(0, grMasId.Length - 1) + "}'";
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int listid = -1;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql2 = "INSERT INTO listeners(fio, phones, grid, lgt, comment)VALUES ('" + windowObj.listenerFIO.Text + "', '" + windowObj.listenerPhones.Text + "', " + grMasId + ", " + grLgMas + ", '" + windowObj.listenerComm.Text + "') returning listenerid";
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listid = reader.GetInt32(0);
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
                    string sqll = "select lgt[array_position(grid,'" + GroupList[i] + "')] from listeners where listenerid= " + listid;

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
                string masPay = "'{";
                for (int i2 = 0; i2 < 12; i2++)
                {
                    masPay += payMonth[i2].ToString().Replace(',', '.') + ",";
                }
                masPay = masPay.Substring(0, masPay.Length - 1) + "}'";
                //добавление записи в таблицу
                try
                {

                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql2 = "INSERT INTO listnuch(listenerid, grid, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose) VALUES(" + listid + ", " + GroupList[i] + ", " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', '{0,0,0,0,0,0,0,0,0,0,0,0}', " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', null, 0)";
                    NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }


            MessageBoxResult res = MessageBox.Show("Слушатель добавлен. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.listenerFIO.Text = "";
                windowObj.listenerPhones.Text = "";
                windowObj.listenerComm.Text = "";
                for (int i = 0; i < windowObj.checkBoxArrForListeners.Length; i++)
                { windowObj.checkBoxArrForListeners[i].IsChecked = false; }
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();

                windowObj.listenerDataGrid.SelectedItem = null;
                //слушатели
                windowObj.listenerDeleteButton.IsEnabled = false;
                windowObj.listenerChangeButton.IsEnabled = false;

                DataGridUpdater.updateDataGridListener(windowObj.connectionString, windowObj.filtr.sql, windowObj.listenerDataGrid);
                windowObj.ListenerGrid.Visibility = Visibility.Visible;

            }
        }
    }
}
