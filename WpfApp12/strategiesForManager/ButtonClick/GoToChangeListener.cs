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
        DirectorWindow windowObj;

        public GoToChangeListener(DirectorWindow windowObj)
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

            windowObj.chbxMas_gr_lg = new CheckBox[countGr];
            windowObj.tbxMas_gr_lg = new TextBox[countGr];


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
                        windowObj.chbxMas_gr_lg[i] = new CheckBox();
                        windowObj.chbxMas_gr_lg[i].Unchecked += windowObj.CheckBox_Unchecked;
                        windowObj.chbxMas_gr_lg[i].Checked += windowObj.CheckBox_Checked;
                        windowObj.tbxMas_gr_lg[i] = new TextBox();
                        windowObj.tbxMas_gr_lg[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);
                        windowObj.gr_lgCh.RowDefinitions.Add(rwd);

                        windowObj.tbxMas_gr_lg[i].IsEnabled = false;
                        windowObj.chbxMas_gr_lg[i].Content = reader.GetString(0) + "-льгота: ";
                        windowObj.chbxMas_gr_lg[i].Name = "chbxMasgrlg_" + i;
                        if (GrMas.IndexOf(reader.GetString(0)) != -1) { windowObj.chbxMas_gr_lg[i].IsChecked = true; windowObj.tbxMas_gr_lg[i].Text = LGMas[GrMas.IndexOf(reader.GetString(0))]; }

                        Grid.SetRow(windowObj.chbxMas_gr_lg[i], i);
                        Grid.SetColumn(windowObj.chbxMas_gr_lg[i], 0);
                        windowObj.gr_lgCh.Children.Add(windowObj.chbxMas_gr_lg[i]);
                        Grid.SetRow(windowObj.tbxMas_gr_lg[i], i);
                        Grid.SetColumn(windowObj.tbxMas_gr_lg[i], 1);
                        windowObj.gr_lgCh.Children.Add(windowObj.tbxMas_gr_lg[i]);

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
