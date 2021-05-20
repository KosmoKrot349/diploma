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
            DataRowView DRV = windowObj.ListenersDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Невозможно изменить, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.listenerID = Convert.ToInt32(arr[0]);

            windowObj.ListenerChangeName.Text = "";
            windowObj.ListenerChangePhone.Text = "";
            windowObj.ListenerChangeComment.Text = "";
            windowObj.GroopsOfListenerGrid.Children.Clear();
            windowObj.GroopsOfListenerGrid.RowDefinitions.Clear();

            ArrayList GroopList = new ArrayList();
            String[] groopsOfListenerFinalArr;
            string didntParsGroopsOfListenerArr = "";

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
                        didntParsGroopsOfListenerArr = reader1.GetString(0);

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
                        GroopList.Add(reader1.GetString(0));

                    }
                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            groopsOfListenerFinalArr = didntParsGroopsOfListenerArr.Split('_');

            int quanGroop = 0;
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
                        quanGroop = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.checkBoxArrForListeners = new CheckBox[quanGroop];
            windowObj.textBoxArrForListeners = new TextBox[quanGroop];


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
                        windowObj.GroopsOfListenerGrid.RowDefinitions.Add(rwd);

                        windowObj.textBoxArrForListeners[i].IsEnabled = false;
                        windowObj.checkBoxArrForListeners[i].Content = reader.GetString(0) + "-льгота: ";
                        windowObj.checkBoxArrForListeners[i].Name = "chbxMasgrlg_" + i;
                        if (GroopList.IndexOf(reader.GetString(0)) != -1) { windowObj.checkBoxArrForListeners[i].IsChecked = true; windowObj.textBoxArrForListeners[i].Text = groopsOfListenerFinalArr[GroopList.IndexOf(reader.GetString(0))]; }

                        Grid.SetRow(windowObj.checkBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.checkBoxArrForListeners[i], 0);
                        windowObj.GroopsOfListenerGrid.Children.Add(windowObj.checkBoxArrForListeners[i]);
                        Grid.SetRow(windowObj.textBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.textBoxArrForListeners[i], 1);
                        windowObj.GroopsOfListenerGrid.Children.Add(windowObj.textBoxArrForListeners[i]);

                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.ListenerChangeName.Text = arr[1].ToString();
            windowObj.ListenerChangePhone.Text = arr[2].ToString();
            windowObj.ListenerChangeComment.Text = arr[4].ToString();
            windowObj.HideAll();
            windowObj.ListenerChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
