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
        ManagerWindow windowObj;

        public GoToEmployeeToStuff(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.EmployeesDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.employeeID = Convert.ToInt32(arr[0]);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select shtatid from shtat where sotrid=" + windowObj.employeeID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Сотрудник уже является штатным работником"); con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.HideAll();
            windowObj.AddStaffGrid.Visibility = Visibility.Visible;
            windowObj.SeviceWorks.Children.Clear();
            windowObj.SeviceWorks.RowDefinitions.Clear();
            windowObj.Positions.Children.Clear();
            windowObj.Positions.RowDefinitions.Clear();
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

            //заполнение грида должностей 
            Label positionLabel = new Label();
            positionLabel.Content = "Должность";
            Label rateLabel = new Label();
            rateLabel.Content = "Ставка";

            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            windowObj.Positions.RowDefinitions.Add(rwd1);

            Grid.SetRow(positionLabel, 0);
            Grid.SetRow(rateLabel, 0);

            Grid.SetColumn(rateLabel, 1);
            Grid.SetColumn(positionLabel, 0);

            windowObj.Positions.Children.Add(positionLabel);
            windowObj.Positions.Children.Add(rateLabel);


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
                        windowObj.textBoxArrRate[i] = new TextBox();
                        windowObj.checkBoxArrPositions[i] = new CheckBox();

                        windowObj.checkBoxArrPositions[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_state";
                        windowObj.checkBoxArrPositions[i].Content = reader.GetString(1);
                        windowObj.checkBoxArrPositions[i].Checked += windowObj.Shtat_Checked;
                        windowObj.checkBoxArrPositions[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.textBoxArrRate[i].IsEnabled = false;
                        windowObj.textBoxArrRate[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.Positions.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrRate[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrPositions[i], (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrRate[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrPositions[i], 0);

                        windowObj.Positions.Children.Add(windowObj.textBoxArrRate[i]);
                        windowObj.Positions.Children.Add(windowObj.checkBoxArrPositions[i]);
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
            Label ubitsLabel = new Label();
            ubitsLabel.Content = "Еденицы измерения";

            RowDefinition rwd11 = new RowDefinition();
            rwd11.Height = new GridLength(40);

            windowObj.SeviceWorks.RowDefinitions.Add(rwd11);

            Grid.SetRow(serviceWorkLabel, 0);
            Grid.SetRow(workVolumeLabel, 0);
            Grid.SetRow(ubitsLabel, 0);

            Grid.SetColumn(workVolumeLabel, 1);
            Grid.SetColumn(serviceWorkLabel, 0);
            Grid.SetColumn(ubitsLabel, 2);

            windowObj.SeviceWorks.Children.Add(serviceWorkLabel);
            windowObj.SeviceWorks.Children.Add(workVolumeLabel);
            windowObj.SeviceWorks.Children.Add(ubitsLabel);

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
                        windowObj.textBoxArrVolumeWork[i] = new TextBox();
                        windowObj.checkBoxArrServiceWorks[i] = new CheckBox();
                        Label lb = new Label();

                        windowObj.checkBoxArrServiceWorks[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        windowObj.checkBoxArrServiceWorks[i].Content = reader.GetString(1);
                        windowObj.checkBoxArrServiceWorks[i].Checked += windowObj.Shtat_Checked;
                        windowObj.checkBoxArrServiceWorks[i].Unchecked += windowObj.Shtat_UnChecked;

                        windowObj.textBoxArrVolumeWork[i].IsEnabled = false;
                        windowObj.textBoxArrVolumeWork[i].PreviewTextInput += windowObj.grPayment_PreviewTextInput;

                        lb.Content = reader.GetString(2);

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        windowObj.SeviceWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrVolumeWork[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrServiceWorks[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrVolumeWork[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrServiceWorks[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.SeviceWorks.Children.Add(windowObj.textBoxArrVolumeWork[i]);
                        windowObj.SeviceWorks.Children.Add(windowObj.checkBoxArrServiceWorks[i]);
                        windowObj.SeviceWorks.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
