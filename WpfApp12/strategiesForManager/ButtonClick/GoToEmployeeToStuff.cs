using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToEmployeeToStuff:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToEmployeeToStuff(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.allSotrDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.sotrID = Convert.ToInt32(arr[0]);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select shtatid from shtat where sotrid=" + windowObj.sotrID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Сотрудник уже является штатным работником"); con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.HideAll();
            windowObj.addShtatGrid.Visibility = Visibility.Visible;
            windowObj.ObslWorks.Children.Clear();
            windowObj.ObslWorks.RowDefinitions.Clear();
            windowObj.States.Children.Clear();
            windowObj.States.RowDefinitions.Clear();
            int kol_states = -1, kol_obsWork = -1;
            //получени е кол-ва должностей
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select count(distinct title) from states";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_states = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение кол-ва облс. работ
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select count(distinct title) from raboty_obsl";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_obsWork = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.tbxMas_stavki = new TextBox[kol_states];
            windowObj.chbxMas_state = new CheckBox[kol_states];
            windowObj.tbxMas_obem = new TextBox[kol_obsWork];
            windowObj.chbxMas_obslwork = new CheckBox[kol_obsWork];

            //заполнение грида должностей 
            Label l1 = new Label();
            l1.Content = "Должность";
            Label l2 = new Label();
            l2.Content = "Ставка";

            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            windowObj.States.RowDefinitions.Add(rwd1);

            Grid.SetRow(l1, 0);
            Grid.SetRow(l2, 0);

            Grid.SetColumn(l2, 1);
            Grid.SetColumn(l1, 0);

            windowObj.States.Children.Add(l1);
            windowObj.States.Children.Add(l2);


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select statesid,title from states order by statesid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.tbxMas_stavki[i] = new TextBox();
                        windowObj.chbxMas_state[i] = new CheckBox();

                        windowObj.chbxMas_state[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_state";
                        windowObj.chbxMas_state[i].Content = reader.GetString(1);
                        windowObj.chbxMas_state[i].Checked += windowObj.Shtat_Checked;
                        windowObj.chbxMas_state[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.tbxMas_stavki[i].IsEnabled = false;
                        windowObj.tbxMas_stavki[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.States.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.tbxMas_stavki[i], (i + 1));
                        Grid.SetRow(windowObj.chbxMas_state[i], (i + 1));

                        Grid.SetColumn(windowObj.tbxMas_stavki[i], 1);
                        Grid.SetColumn(windowObj.chbxMas_state[i], 0);

                        windowObj.States.Children.Add(windowObj.tbxMas_stavki[i]);
                        windowObj.States.Children.Add(windowObj.chbxMas_state[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //заполнение грида работ 

            Label l11 = new Label();
            l11.Content = "Работа";
            Label l22 = new Label();
            l22.Content = "Объём";
            Label l33 = new Label();
            l33.Content = "Еденицы измерения";

            RowDefinition rwd11 = new RowDefinition();
            rwd11.Height = new GridLength(40);

            windowObj.ObslWorks.RowDefinitions.Add(rwd11);

            Grid.SetRow(l11, 0);
            Grid.SetRow(l22, 0);
            Grid.SetRow(l33, 0);

            Grid.SetColumn(l22, 1);
            Grid.SetColumn(l11, 0);
            Grid.SetColumn(l33, 2);

            windowObj.ObslWorks.Children.Add(l11);
            windowObj.ObslWorks.Children.Add(l22);
            windowObj.ObslWorks.Children.Add(l33);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select rabotyid,title,ed_izm from raboty_obsl order by rabotyid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        windowObj.tbxMas_obem[i] = new TextBox();
                        windowObj.chbxMas_obslwork[i] = new CheckBox();
                        Label lb = new Label();

                        windowObj.chbxMas_obslwork[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        windowObj.chbxMas_obslwork[i].Content = reader.GetString(1);
                        windowObj.chbxMas_obslwork[i].Checked += windowObj.Shtat_Checked;
                        windowObj.chbxMas_obslwork[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.tbxMas_obem[i].IsEnabled = false;
                        windowObj.tbxMas_obem[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        lb.Content = reader.GetString(2);

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.tbxMas_obem[i], (i + 1));
                        Grid.SetRow(windowObj.chbxMas_obslwork[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.tbxMas_obem[i], 1);
                        Grid.SetColumn(windowObj.chbxMas_obslwork[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.ObslWorks.Children.Add(windowObj.tbxMas_obem[i]);
                        windowObj.ObslWorks.Children.Add(windowObj.chbxMas_obslwork[i]);
                        windowObj.ObslWorks.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
