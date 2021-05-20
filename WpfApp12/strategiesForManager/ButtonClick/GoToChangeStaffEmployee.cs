using Npgsql;
using System;
using System.Collections;
using System.Data;
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
            DataRowView DRV = windowObj.StaffDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.staffID = Convert.ToInt32(arr[0]);
            windowObj.HideAll();
            windowObj.ChangeShtatGrid.Visibility = Visibility.Visible;

            windowObj.fioChangeShtat.Text = arr[1].ToString();

            windowObj.ChangeStaffPositions.Children.Clear();
            windowObj.ChangeStaffPositions.RowDefinitions.Clear();

            windowObj.ChangeStaffServiceWork.Children.Clear();
            windowObj.ChangeStaffServiceWork.RowDefinitions.Clear();

            int quanPositions = -1, quanServiceWork = -1;
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
                        quanPositions = reader.GetInt32(0);
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
                        quanServiceWork = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.textBoxArrRate = new TextBox[quanPositions];
            windowObj.checkBoxArrPositions = new CheckBox[quanPositions];
            windowObj.textBoxArrVolumeWork = new TextBox[quanServiceWork];
            windowObj.checkBoxArrServiceWorks = new CheckBox[quanServiceWork];



            //получение должностей 
            ArrayList positionsList = new ArrayList();
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
                        positionsList.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение работ 
            ArrayList serviceWorkList = new ArrayList();
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
                        serviceWorkList.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение обёма работ и ставок
            string rates = "";
            string workVolume = "";
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
                        rates = reader12.GetString(0);
                        workVolume = reader12.GetString(1);
                    }
                }
                con12.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            string[] rateArr = rates.Split('_');
            string[] workVolumeArr = workVolume.Split('_'); ;

            //заполнение грида должностей 
            Label positionLabel = new Label();
            positionLabel.Content = "Должность";
            Label rateLabel = new Label();
            rateLabel.Content = "Ставка";


            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            windowObj.ChangeStaffPositions.RowDefinitions.Add(rwd1);

            Grid.SetRow(positionLabel, 0);
            Grid.SetRow(rateLabel, 0);


            Grid.SetColumn(rateLabel, 1);
            Grid.SetColumn(positionLabel, 0);


            windowObj.ChangeStaffPositions.Children.Add(positionLabel);
            windowObj.ChangeStaffPositions.Children.Add(rateLabel);

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

                        if (positionsList.IndexOf(reader.GetString(1)) != -1) { windowObj.checkBoxArrPositions[i].IsChecked = true; windowObj.textBoxArrRate[i].Text = rateArr[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeStaffPositions.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrRate[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrPositions[i], (i + 1));


                        Grid.SetColumn(windowObj.textBoxArrRate[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrPositions[i], 0);


                        windowObj.ChangeStaffPositions.Children.Add(windowObj.textBoxArrRate[i]);
                        windowObj.ChangeStaffPositions.Children.Add(windowObj.checkBoxArrPositions[i]);

                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //заполнение грида работ 

            Label serviceWorkLabel = new Label();
            serviceWorkLabel.Content = "Работа";
            Label workVolumeLabel = new Label();
            workVolumeLabel.Content = "Объём";
            Label unitsLabel = new Label();
            unitsLabel.Content = "Еденицы измерения";

            RowDefinition rwd11 = new RowDefinition();
            rwd11.Height = new GridLength(40);

            windowObj.ChangeStaffServiceWork.RowDefinitions.Add(rwd11);

            Grid.SetRow(serviceWorkLabel, 0);
            Grid.SetRow(workVolumeLabel, 0);
            Grid.SetRow(unitsLabel, 0);

            Grid.SetColumn(workVolumeLabel, 1);
            Grid.SetColumn(serviceWorkLabel, 0);
            Grid.SetColumn(unitsLabel, 2);

            windowObj.ChangeStaffServiceWork.Children.Add(serviceWorkLabel);
            windowObj.ChangeStaffServiceWork.Children.Add(workVolumeLabel);
            windowObj.ChangeStaffServiceWork.Children.Add(unitsLabel);

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

                        if (serviceWorkList.IndexOf(reader.GetString(1)) != -1) { windowObj.checkBoxArrServiceWorks[i].IsChecked = true; windowObj.textBoxArrVolumeWork[i].Text = workVolumeArr[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.ChangeStaffServiceWork.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrVolumeWork[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrServiceWorks[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrVolumeWork[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrServiceWorks[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.ChangeStaffServiceWork.Children.Add(windowObj.textBoxArrVolumeWork[i]);
                        windowObj.ChangeStaffServiceWork.Children.Add(windowObj.checkBoxArrServiceWorks[i]);
                        windowObj.ChangeStaffServiceWork.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
