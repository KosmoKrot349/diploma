using Npgsql;
using OxyPlot.Axes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForBookkeeper.MenuClick
{
    class ListOfPaymentReportMenu:IMenuClick
    {
        BookkeeperWindow windowObj;

        public ListOfPaymentReportMenu(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.ZpOthcetGrid.Visibility = Visibility.Visible;
            windowObj.ZPOtchetVivodGrid.ColumnDefinitions.Clear();
            windowObj.ZPOtchetVivodGrid.Children.Clear();

            windowObj.ZPOtchetLabel.Content = "Отчёт 'Списки выплат' на " + DateTime.Now.ToShortDateString();

            ColumnDefinition cmd = new ColumnDefinition();
            cmd.Width = new GridLength(200);
            Grid grid = new Grid();
            Grid.SetColumn(grid, 0);
            windowObj.ZPOtchetVivodGrid.ColumnDefinitions.Add(cmd);
            windowObj.ZPOtchetVivodGrid.Children.Add(grid);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select  count(fio) from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for (int i = 0; i < reader.GetInt32(0) + 3; i++)
                        {
                            RowDefinition cmdGr = new RowDefinition();
                            cmdGr.Height = new GridLength(50);
                            grid.RowDefinitions.Add(cmdGr);
                        }
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            Label monthLabel = new Label();
            monthLabel.Content = "Месяц";
            monthLabel.BorderBrush = Brushes.Black;
            monthLabel.BorderThickness = new Thickness(2);

            Label salaryLabel = new Label();
            salaryLabel.Content = "ЗП";
            salaryLabel.BorderBrush = Brushes.Black;
            salaryLabel.BorderThickness = new Thickness(2);

            Label totalLabel = new Label();
            totalLabel.Content = "Итого";
            totalLabel.BorderBrush = Brushes.Black;
            totalLabel.BorderThickness = new Thickness(2);

            Grid.SetRow(monthLabel, 0);
            Grid.SetRow(salaryLabel, 1);
            Grid.SetRow(totalLabel, grid.RowDefinitions.Count - 1);

            grid.Children.Add(monthLabel); grid.Children.Add(salaryLabel); grid.Children.Add(totalLabel);

            //заполнение первого грида
            ArrayList emploeesList = new ArrayList();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select  fio,sotrid from sotrudniki order by fio";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 2;
                    while (reader.Read())
                    {
                        Label employeesLabel = new Label();
                        employeesLabel.Content = reader.GetString(0);
                        employeesLabel.BorderBrush = Brushes.Black;
                        employeesLabel.BorderThickness = new Thickness(2);
                        Grid.SetRow(employeesLabel, i);
                        grid.Children.Add(employeesLabel);
                        emploeesList.Add(reader.GetInt32(1));
                        i++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение дат (мемсяц/год из таблиц начислений)

            ArrayList dateList = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select payday from nachisl order by payday";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        int month = reader1.GetDateTime(0).Month;
                        int year = reader1.GetDateTime(0).Year;



                        DateTime dd = new DateTime(year, month, 1);
                        if (dateList.IndexOf(DateTimeAxis.ToDouble(dd)) == -1) dateList.Add(DateTimeAxis.ToDouble(dd));
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            for (int i = 0; i < dateList.Count; i++)
            {

                ColumnDefinition cmdd = new ColumnDefinition();
                cmdd.Width = new GridLength(300);
                windowObj.ZPOtchetVivodGrid.ColumnDefinitions.Add(cmdd);
                Grid monthGrid = new Grid();
                for (int j = 0; j < 4; j++)
                {
                    ColumnDefinition cmdd2 = new ColumnDefinition();
                    monthGrid.ColumnDefinitions.Add(cmdd2);
                }
                DataGridUpdater.updatePaymentListGrid(windowObj.connectionString, DateTimeAxis.ToDateTime(Convert.ToDouble(dateList[i])), emploeesList, monthGrid);
                Grid.SetColumn(monthGrid, i + 1);
                windowObj.ZPOtchetVivodGrid.Children.Add(monthGrid);
            }
        }
    }
}
