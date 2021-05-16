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
        DirectorWindow windowObj;

        public GoToAddListener(DirectorWindow windowObj)
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
                        windowObj.gr_lg.RowDefinitions.Add(rwd);
                        windowObj.tbxMas_gr_lg[i].IsEnabled = false;
                        windowObj.chbxMas_gr_lg[i].Content = reader.GetString(0) + "-льгота: ";
                        windowObj.chbxMas_gr_lg[i].Name = "chbxMasgrlg_" + i;
                        Grid.SetRow(windowObj.chbxMas_gr_lg[i], i);
                        Grid.SetColumn(windowObj.chbxMas_gr_lg[i], 0);
                        windowObj.gr_lg.Children.Add(windowObj.chbxMas_gr_lg[i]);
                        Grid.SetRow(windowObj.tbxMas_gr_lg[i], i);
                        Grid.SetColumn(windowObj.tbxMas_gr_lg[i], 1);
                        windowObj.gr_lg.Children.Add(windowObj.tbxMas_gr_lg[i]);

                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
