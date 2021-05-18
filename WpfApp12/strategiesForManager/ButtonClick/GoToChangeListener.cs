using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeListener:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToChangeListener(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.listenerDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Невозможно изменить, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.listenerID = Convert.ToInt32(arr[0]);

            windowObj.listenerFIOCh.Text = "";
            windowObj.listenerPhonesCh.Text = "";
            windowObj.listenerCommCh.Text = "";
            windowObj.gr_lgCh.Children.Clear();
            windowObj.gr_lgCh.RowDefinitions.Clear();

            ArrayList GrMas = new ArrayList();
            String[] LGMas;
            string lgmas = "";

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select array_to_string(lgt,'_') from listeners where listenerid= " + arr[0];
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        lgmas = reader1.GetString(0);

                    }
                }
                con1.Close();

            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select nazvanie from groups where ARRAY[grid] <@ (select grid from listeners where listenerid=" + arr[0] + ") order by grid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        GrMas.Add(reader1.GetString(0));

                    }
                }
                con1.Close();

            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            LGMas = lgmas.Split('_');

            int countGr = 0;
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select count(nazvanie) from groups";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        countGr = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.checkBoxArrForListeners = new CheckBox[countGr];
            windowObj.textBoxArrForListeners = new TextBox[countGr];


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select nazvanie from groups order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.checkBoxArrForListeners[i] = new CheckBox();
                        windowObj.checkBoxArrForListeners[i].Unchecked += windowObj.CheckBox_Unchecked;
                        windowObj.checkBoxArrForListeners[i].Checked += windowObj.CheckBox_Checked;
                        windowObj.textBoxArrForListeners[i] = new TextBox();
                        windowObj.textBoxArrForListeners[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);
                        windowObj.gr_lgCh.RowDefinitions.Add(rwd);

                        windowObj.textBoxArrForListeners[i].IsEnabled = false;
                        windowObj.checkBoxArrForListeners[i].Content = reader.GetString(0) + "-льгота: ";
                        windowObj.checkBoxArrForListeners[i].Name = "chbxMasgrlg_" + i;
                        if (GrMas.IndexOf(reader.GetString(0)) != -1) { windowObj.checkBoxArrForListeners[i].IsChecked = true; windowObj.textBoxArrForListeners[i].Text = LGMas[GrMas.IndexOf(reader.GetString(0))]; }

                        Grid.SetRow(windowObj.checkBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.checkBoxArrForListeners[i], 0);
                        windowObj.gr_lgCh.Children.Add(windowObj.checkBoxArrForListeners[i]);
                        Grid.SetRow(windowObj.textBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.textBoxArrForListeners[i], 1);
                        windowObj.gr_lgCh.Children.Add(windowObj.textBoxArrForListeners[i]);

                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.listenerFIOCh.Text = arr[1].ToString();
            windowObj.listenerPhonesCh.Text = arr[2].ToString();
            windowObj.listenerCommCh.Text = arr[4].ToString();
            windowObj.HideAll();
            windowObj.ListenerChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
