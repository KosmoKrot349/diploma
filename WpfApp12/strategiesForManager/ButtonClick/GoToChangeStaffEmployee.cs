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
        ManagerWindow windowObj;

        public GoToChangeStaffEmployee(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ShtatDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.staffID = Convert.ToInt32(arr[0]);
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

            windowObj.textBoxArrRate = new TextBox[kol_states];
            windowObj.checkBoxArrPositions = new CheckBox[kol_states];
            windowObj.textBoxArrVolumeWork = new TextBox[kol_obsWork];
            windowObj.checkBoxArrServiceWorks = new CheckBox[kol_obsWork];



            //получение должностей 
            ArrayList StatesLs = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();

                string sql1 = "select title from states where ARRAY[statesid] <@ (select states from shtat where shtatid=" + windowObj.staffID + " ) order by statesid";
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

                string sql1 = "select title from raboty_obsl where ARRAY[rabotyid] <@ (select obslwork from shtat where shtatid=" + windowObj.staffID + " ) order by rabotyid";
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

                string sql12 = "select array_to_string(stavky,'_'),array_to_string(obem,'_') from shtat where shtatid=" + windowObj.staffID;
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
                        windowObj.textBoxArrRate[i] = new TextBox();
                        windowObj.checkBoxArrPositions[i] = new CheckBox();



                        windowObj.checkBoxArrPositions[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_state";
                        windowObj.checkBoxArrPositions[i].Content = reader.GetString(1);
                        windowObj.checkBoxArrPositions[i].Checked += windowObj.Shtat_Checked;
                        windowObj.checkBoxArrPositions[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.textBoxArrRate[i].IsEnabled = false;
                        windowObj.textBoxArrRate[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        if (StatesLs.IndexOf(reader.GetString(1)) != -1) { windowObj.checkBoxArrPositions[i].IsChecked = true; windowObj.textBoxArrRate[i].Text = stavkiMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeStates.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrRate[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrPositions[i], (i + 1));


                        Grid.SetColumn(windowObj.textBoxArrRate[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrPositions[i], 0);


                        windowObj.ChangeStates.Children.Add(windowObj.textBoxArrRate[i]);
                        windowObj.ChangeStates.Children.Add(windowObj.checkBoxArrPositions[i]);

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
                        windowObj.textBoxArrVolumeWork[i] = new TextBox();
                        windowObj.checkBoxArrServiceWorks[i] = new CheckBox();
                        Label lb = new Label();

                        lb.Content = reader.GetString(2);
                        windowObj.checkBoxArrServiceWorks[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        windowObj.checkBoxArrServiceWorks[i].Content = reader.GetString(1);
                        windowObj.checkBoxArrServiceWorks[i].Checked += windowObj.Shtat_Checked;
                        windowObj.checkBoxArrServiceWorks[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.textBoxArrVolumeWork[i].IsEnabled = false;
                        windowObj.textBoxArrVolumeWork[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        if (WorkLs.IndexOf(reader.GetString(1)) != -1) { windowObj.checkBoxArrServiceWorks[i].IsChecked = true; windowObj.textBoxArrVolumeWork[i].Text = obemMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrVolumeWork[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrServiceWorks[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrVolumeWork[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrServiceWorks[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.ChangeObslWorks.Children.Add(windowObj.textBoxArrVolumeWork[i]);
                        windowObj.ChangeObslWorks.Children.Add(windowObj.checkBoxArrServiceWorks[i]);
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
