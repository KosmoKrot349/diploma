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
    class GoToChangeStaffEmployee:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToChangeStaffEmployee(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ShtatDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.ShtatID = Convert.ToInt32(arr[0]);
            windowObj.HideAll();
            windowObj.ChangeShtatGrid.Visibility = Visibility.Visible;

            windowObj.fioChangeShtat.Text = arr[1].ToString();

            windowObj.ChangeStates.Children.Clear();
            windowObj.ChangeStates.RowDefinitions.Clear();

            windowObj.ChangeObslWorks.Children.Clear();
            windowObj.ChangeObslWorks.RowDefinitions.Clear();

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



            //получение должностей 
            ArrayList StatesLs = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();

                string sql1 = "select title from states where ARRAY[statesid] <@ (select states from shtat where shtatid=" + windowObj.ShtatID + " ) order by statesid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        StatesLs.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение работ 
            ArrayList WorkLs = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();

                string sql1 = "select title from raboty_obsl where ARRAY[rabotyid] <@ (select obslwork from shtat where shtatid=" + windowObj.ShtatID + " ) order by rabotyid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        WorkLs.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение обёма работ и ставок
            string stavki = "";
            string obem = "";
            try
            {
                NpgsqlConnection con12 = new NpgsqlConnection(windowObj.connectionString);
                con12.Open();

                string sql12 = "select array_to_string(stavky,'_'),array_to_string(obem,'_') from shtat where shtatid=" + windowObj.ShtatID;
                NpgsqlCommand com12 = new NpgsqlCommand(sql12, con12);
                NpgsqlDataReader reader12 = com12.ExecuteReader();
                if (reader12.HasRows)
                {
                    while (reader12.Read())
                    {
                        stavki = reader12.GetString(0);
                        obem = reader12.GetString(1);
                    }
                }
                con12.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            string[] stavkiMas = stavki.Split('_');
            string[] obemMas = obem.Split('_'); ;

            //заполнение грида должностей 
            Label l1 = new Label();
            l1.Content = "Должность";
            Label l2 = new Label();
            l2.Content = "Ставка";


            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            windowObj.ChangeStates.RowDefinitions.Add(rwd1);

            Grid.SetRow(l1, 0);
            Grid.SetRow(l2, 0);


            Grid.SetColumn(l2, 1);
            Grid.SetColumn(l1, 0);


            windowObj.ChangeStates.Children.Add(l1);
            windowObj.ChangeStates.Children.Add(l2);

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
                    int j = 0;
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

                        if (StatesLs.IndexOf(reader.GetString(1)) != -1) { windowObj.chbxMas_state[i].IsChecked = true; windowObj.tbxMas_stavki[i].Text = stavkiMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeStates.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.tbxMas_stavki[i], (i + 1));
                        Grid.SetRow(windowObj.chbxMas_state[i], (i + 1));


                        Grid.SetColumn(windowObj.tbxMas_stavki[i], 1);
                        Grid.SetColumn(windowObj.chbxMas_state[i], 0);


                        windowObj.ChangeStates.Children.Add(windowObj.tbxMas_stavki[i]);
                        windowObj.ChangeStates.Children.Add(windowObj.chbxMas_state[i]);

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

            windowObj.ChangeObslWorks.RowDefinitions.Add(rwd11);

            Grid.SetRow(l11, 0);
            Grid.SetRow(l22, 0);
            Grid.SetRow(l33, 0);

            Grid.SetColumn(l22, 1);
            Grid.SetColumn(l11, 0);
            Grid.SetColumn(l33, 2);

            windowObj.ChangeObslWorks.Children.Add(l11);
            windowObj.ChangeObslWorks.Children.Add(l22);
            windowObj.ChangeObslWorks.Children.Add(l33);

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
                    int j = 0;
                    while (reader.Read())
                    {
                        windowObj.tbxMas_obem[i] = new TextBox();
                        windowObj.chbxMas_obslwork[i] = new CheckBox();
                        Label lb = new Label();

                        lb.Content = reader.GetString(2);
                        windowObj.chbxMas_obslwork[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        windowObj.chbxMas_obslwork[i].Content = reader.GetString(1);
                        windowObj.chbxMas_obslwork[i].Checked += windowObj.Shtat_Checked;
                        windowObj.chbxMas_obslwork[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.tbxMas_obem[i].IsEnabled = false;
                        windowObj.tbxMas_obem[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        if (WorkLs.IndexOf(reader.GetString(1)) != -1) { windowObj.chbxMas_obslwork[i].IsChecked = true; windowObj.tbxMas_obem[i].Text = obemMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.tbxMas_obem[i], (i + 1));
                        Grid.SetRow(windowObj.chbxMas_obslwork[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.tbxMas_obem[i], 1);
                        Grid.SetColumn(windowObj.chbxMas_obslwork[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.ChangeObslWorks.Children.Add(windowObj.tbxMas_obem[i]);
                        windowObj.ChangeObslWorks.Children.Add(windowObj.chbxMas_obslwork[i]);
                        windowObj.ChangeObslWorks.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
