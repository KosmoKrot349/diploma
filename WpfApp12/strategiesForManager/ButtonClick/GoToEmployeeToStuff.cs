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
            DataRowView DRV = windowObj.allSotrDataGrid.SelectedItem as DataRowView;
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

            windowObj.textBoxArrRate = new TextBox[kol_states];
            windowObj.checkBoxArrPositions = new CheckBox[kol_states];
            windowObj.textBoxArrVolumeWork = new TextBox[kol_obsWork];
            windowObj.checkBoxArrServiceWorks = new CheckBox[kol_obsWork];

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

                        windowObj.States.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrRate[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrPositions[i], (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrRate[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrPositions[i], 0);

                        windowObj.States.Children.Add(windowObj.textBoxArrRate[i]);
                        windowObj.States.Children.Add(windowObj.checkBoxArrPositions[i]);
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

                        windowObj.ObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(windowObj.textBoxArrVolumeWork[i], (i + 1));
                        Grid.SetRow(windowObj.checkBoxArrServiceWorks[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(windowObj.textBoxArrVolumeWork[i], 1);
                        Grid.SetColumn(windowObj.checkBoxArrServiceWorks[i], 0);
                        Grid.SetColumn(lb, 2);

                        windowObj.ObslWorks.Children.Add(windowObj.textBoxArrVolumeWork[i]);
                        windowObj.ObslWorks.Children.Add(windowObj.checkBoxArrServiceWorks[i]);
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
