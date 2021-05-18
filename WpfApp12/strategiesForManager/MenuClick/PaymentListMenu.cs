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

namespace WpfApp12.strategiesForManager.MenuClick
{
    class PaymentListMenu:IMenuClick
    {
        ManagerWindow window;

        public PaymentListMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.ZpOthcetGrid.Visibility = Visibility.Visible;
            window.ZPOtchetVivodGrid.ColumnDefinitions.Clear();
            window.ZPOtchetVivodGrid.Children.Clear();

            window.ZPOtchetLabel.Content = "Отчёт 'Списки выплат' на " + DateTime.Now.ToShortDateString();

            ColumnDefinition cmd = new ColumnDefinition();
            cmd.Width = new GridLength(200);
            Grid gr = new Grid();
            Grid.SetColumn(gr, 0);
            window.ZPOtchetVivodGrid.ColumnDefinitions.Add(cmd);
            window.ZPOtchetVivodGrid.Children.Add(gr);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
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
                            gr.RowDefinitions.Add(cmdGr);
                        }
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            Label mesLb = new Label();
            mesLb.Content = "Месяц";
            mesLb.BorderBrush = Brushes.Black;
            mesLb.BorderThickness = new Thickness(2);

            Label zpLb = new Label();
            zpLb.Content = "ЗП";
            zpLb.BorderBrush = Brushes.Black;
            zpLb.BorderThickness = new Thickness(2);

            Label itogLb = new Label();
            itogLb.Content = "Итого";
            itogLb.BorderBrush = Brushes.Black;
            itogLb.BorderThickness = new Thickness(2);

            Grid.SetRow(mesLb, 0);
            Grid.SetRow(zpLb, 1);
            Grid.SetRow(itogLb, gr.RowDefinitions.Count - 1);

            gr.Children.Add(mesLb); gr.Children.Add(zpLb); gr.Children.Add(itogLb);

            //заполнение первого грида
            ArrayList sotrList = new ArrayList();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select  fio,sotrid from sotrudniki order by fio";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 2;
                    while (reader.Read())
                    {
                        Label sotrLb = new Label();
                        sotrLb.Content = reader.GetString(0);
                        sotrLb.BorderBrush = Brushes.Black;
                        sotrLb.BorderThickness = new Thickness(2);
                        Grid.SetRow(sotrLb, i);
                        gr.Children.Add(sotrLb);
                        sotrList.Add(reader.GetInt32(1));
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
                NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
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
                cmdd.Width = new GridLength(600);
                window.ZPOtchetVivodGrid.ColumnDefinitions.Add(cmdd);
                Grid monthGrid = new Grid();
                for (int j = 0; j < 4; j++)
                {
                    ColumnDefinition cmdd2 = new ColumnDefinition();
                    monthGrid.ColumnDefinitions.Add(cmdd2);
                }
                DataGridUpdater.updateGridSpisciVyplat(window.connectionString, DateTimeAxis.ToDateTime(Convert.ToDouble(dateList[i])), sotrList, monthGrid);
                Grid.SetColumn(monthGrid, i + 1);
                window.ZPOtchetVivodGrid.Children.Add(monthGrid);
            }
        }
    }
}
