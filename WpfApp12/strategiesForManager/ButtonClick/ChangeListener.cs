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
    class ChangeListener:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeListener(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.listenerFIOCh.Text == "" || windowObj.listenerPhonesCh.Text == "") { MessageBox.Show("Поля не заполнены или заполнены не правильно."); return; }
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
                    if (windowObj.textBoxArrForListeners[i].Text != "" && Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text) > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }
                    if (windowObj.textBoxArrForListeners[i].Text != "") grLgMas += Convert.ToDouble(windowObj.textBoxArrForListeners[i].Text).ToString().Replace(',', '.') + ",";
                    else
                        grLgMas += "0,";
                }

            }
            grLgMas = grLgMas.Substring(0, grLgMas.Length - 1) + "}'";
            sql = sql.Substring(0, sql.Length - 3) + " order by grid";
            if (b == false) { System.Windows.Forms.MessageBox.Show("Группа не выбрана"); return; }
            string grMasId = "'{";
            ArrayList GroupsOfListNew = new ArrayList();
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
                        GroupsOfListNew.Add(reader.GetInt32(0));
                    }

                }
                con.Close();
                grMasId = grMasId.Substring(0, grMasId.Length - 1) + "}'";
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение групп слуштеля из таблицы оплат

            ArrayList GroupsOfListFromTable = new ArrayList();
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql2 = "select grid from listnuch where listenerid = " + windowObj.listenerID + " and isclose=0";
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        GroupsOfListFromTable.Add(reader.GetString(0));
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //проверка не убраны ли не закрытые групп
            for (int i = 0; i < GroupsOfListFromTable.Count; i++)
            {
                bool b1 = false;
                for (int j = 0; j < GroupsOfListNew.Count; j++)
                {
                    int a = Convert.ToInt32(GroupsOfListNew[j]);
                    int b2 = Convert.ToInt32(GroupsOfListFromTable[i]);
                    if (a == b2) { b1 = true; break; }
                }
                if (b1 == false) { MessageBox.Show("Вы не можете убрать у слушателя группу, за которую у него не закрыта оплата"); return; }
            }

            //обновление записи
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql2 = "UPDATE listeners SET  fio='" + windowObj.listenerFIOCh.Text + "', phones='" + windowObj.listenerPhonesCh.Text + "', grid=" + grMasId + ", lgt=" + grLgMas + ", comment='" + windowObj.listenerCommCh.Text + "' WHERE listenerid=" + windowObj.listenerID;
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //добавление записи в таблицу оплат

            for (int i = 0; i < GroupsOfListNew.Count; i++)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql2 = "select isclose from listnuch where listenerid=" + windowObj.listenerID + " and grid=" + GroupsOfListNew[i] + "";
                    NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows == false)
                    {

                        double[] payMonth = new double[12];
                        //получение массива оплаты за группу
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                            con2.Open();
                            string sqll = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12] from groups where grid= " + GroupsOfListNew[i];

                            NpgsqlCommand com2 = new NpgsqlCommand(sqll, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    payMonth[0] = reader2.GetDouble(0);
                                    payMonth[1] = reader2.GetDouble(1);
                                    payMonth[2] = reader2.GetDouble(2);
                                    payMonth[3] = reader2.GetDouble(3);
                                    payMonth[4] = reader2.GetDouble(4);
                                    payMonth[5] = reader2.GetDouble(5);
                                    payMonth[6] = reader2.GetDouble(6);
                                    payMonth[7] = reader2.GetDouble(7);
                                    payMonth[8] = reader2.GetDouble(8);
                                    payMonth[9] = reader2.GetDouble(9);
                                    payMonth[10] = reader2.GetDouble(10);
                                    payMonth[11] = reader2.GetDouble(11);
                                }
                            }
                            con2.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        //вычисление оплаты для студента
                        try
                        {
                            NpgsqlConnection con22 = new NpgsqlConnection(windowObj.connectionString);
                            con22.Open();
                            string sqll = "select lgt[array_position(grid,'" + GroupsOfListNew[i] + "')] from listeners where listenerid= " + windowObj.listenerID;

                            NpgsqlCommand com22 = new NpgsqlCommand(sqll, con22);
                            NpgsqlDataReader reader22 = com22.ExecuteReader();
                            if (reader22.HasRows)
                            {
                                while (reader22.Read())
                                {
                                    payMonth[0] = Math.Round(payMonth[0] - payMonth[0] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[1] = Math.Round(payMonth[1] - payMonth[1] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[2] = Math.Round(payMonth[2] - payMonth[2] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[3] = Math.Round(payMonth[3] - payMonth[3] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[4] = Math.Round(payMonth[4] - payMonth[4] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[5] = Math.Round(payMonth[5] - payMonth[5] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[6] = Math.Round(payMonth[6] - payMonth[6] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[7] = Math.Round(payMonth[7] - payMonth[7] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[8] = Math.Round(payMonth[8] - payMonth[8] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[9] = Math.Round(payMonth[9] - payMonth[9] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[10] = Math.Round(payMonth[10] - payMonth[10] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[11] = Math.Round(payMonth[11] - payMonth[11] * reader22.GetDouble(0) / 100, 2);
                                }
                            }
                            con22.Close();
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

                            NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                            con2.Open();
                            string sql22 = "INSERT INTO listnuch(listenerid, grid, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose) VALUES(" + windowObj.listenerID + ", " + GroupsOfListNew[i] + ", " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', '{0,0,0,0,0,0,0,0,0,0,0,0}', " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', null, 0)";
                            NpgsqlCommand com2 = new NpgsqlCommand(sql22, con2);
                            com2.ExecuteNonQuery();
                            con2.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                    }

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }



            windowObj.HideAll();
            //слушатели
            windowObj.listenerDeleteButton.IsEnabled = false;
            windowObj.listenerChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridListener(windowObj.connectionString, windowObj.filtr.sql, windowObj.listenerDataGrid);
            windowObj.ListenerGrid.Visibility = Visibility.Visible;
        }
    }
}
