using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddListener:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddListener(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.ListenerAddGrid.Visibility = Visibility.Visible;
            windowObj.gr_lg.Children.Clear();
            windowObj.gr_lg.RowDefinitions.Clear();
            windowObj.listenerFIO.Text = "";
            windowObj.listenerPhones.Text = "";
            windowObj.listenerComm.Text = "";
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
                        windowObj.gr_lg.RowDefinitions.Add(rwd);
                        windowObj.textBoxArrForListeners[i].IsEnabled = false;
                        windowObj.checkBoxArrForListeners[i].Content = reader.GetString(0) + "-льгота: ";
                        windowObj.checkBoxArrForListeners[i].Name = "chbxMasgrlg_" + i;
                        Grid.SetRow(windowObj.checkBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.checkBoxArrForListeners[i], 0);
                        windowObj.gr_lg.Children.Add(windowObj.checkBoxArrForListeners[i]);
                        Grid.SetRow(windowObj.textBoxArrForListeners[i], i);
                        Grid.SetColumn(windowObj.textBoxArrForListeners[i], 1);
                        windowObj.gr_lg.Children.Add(windowObj.textBoxArrForListeners[i]);

                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
