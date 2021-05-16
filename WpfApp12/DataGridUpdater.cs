using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;
using System.Data;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace WpfApp12
{
    class DataGridUpdater
    {


        public static void updateGridSpisciVyplat(string connectionString, DateTime payday,ArrayList sotrlist,Grid grid)
        {
            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(50);
            RowDefinition rwd2 = new RowDefinition();
            rwd2.Height = new GridLength(50);
            grid.RowDefinitions.Add(rwd1);
            grid.RowDefinitions.Add(rwd2);

            Label lbmes = new Label();
            lbmes.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            lbmes.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            lbmes.BorderBrush = Brushes.Black;
            lbmes.BorderThickness = new Thickness(2);
            switch (payday.Month)
            {
                case 1: { lbmes.Content = "Январь " + payday.Year; break; }
                case 2: { lbmes.Content = "Февраль " + payday.Year; break; }
                case 3: { lbmes.Content = "Март " + payday.Year; break; }
                case 4: { lbmes.Content = "Апрель " + payday.Year; break; }
                case 5: { lbmes.Content = "Май " + payday.Year; break; }
                case 6: { lbmes.Content = "Июнь " + payday.Year; break; }
                case 7: { lbmes.Content = "Июль " + payday.Year; break; }
                case 8: { lbmes.Content = "Август " + payday.Year; break; }
                case 9: { lbmes.Content = "Сентябрь " + payday.Year; break; }
                case 10: { lbmes.Content = "Октябрь " + payday.Year; break; }
                case 11: { lbmes.Content = "Ноябрь " + payday.Year; break; }
                case 12: { lbmes.Content = "Декабрь " + payday.Year; break; }
            }
            Grid.SetColumnSpan(lbmes, 5);
            Grid.SetRow(lbmes, 0);
            grid.Children.Add(lbmes);

            Label prepzpLb = new Label();
            prepzpLb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            prepzpLb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            prepzpLb.Content = "ЗП преподавателя";
            prepzpLb.BorderBrush = Brushes.Black;
            prepzpLb.BorderThickness = new Thickness(2);

            Label shtatzpLb = new Label();
            shtatzpLb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            shtatzpLb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            shtatzpLb.Content = "ЗП штатная";
            shtatzpLb.BorderBrush = Brushes.Black;
            shtatzpLb.BorderThickness = new Thickness(2);


            Label allzpLb = new Label();
            allzpLb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            allzpLb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            allzpLb.Content = "ЗП вся";
            allzpLb.BorderBrush = Brushes.Black;
            allzpLb.BorderThickness = new Thickness(2);

            Label viplzpLb = new Label();
            viplzpLb.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            viplzpLb.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            viplzpLb.Content = "Выплачено";
            viplzpLb.BorderBrush = Brushes.Black;
            viplzpLb.BorderThickness = new Thickness(2);

            Grid.SetRow(prepzpLb, 1); Grid.SetRow(shtatzpLb, 1);  Grid.SetRow(allzpLb, 1); Grid.SetRow(viplzpLb, 1);
            Grid.SetColumn(prepzpLb, 0); Grid.SetColumn(shtatzpLb, 1); Grid.SetColumn(allzpLb, 2); Grid.SetColumn(viplzpLb, 3);
            grid.Children.Add(prepzpLb); grid.Children.Add(shtatzpLb);  grid.Children.Add(allzpLb); grid.Children.Add(viplzpLb);

            double allprep = 0, allshtat = 0, allobsl = 0, allvipl = 0;
            for (int i = 0; i < sotrlist.Count; i++)
            {
                try
                {
                    NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                    con1.Open();
                    string sql1 = "SELECT prepzp, shtatzp, obslzp,viplacheno FROM nachisl where sotrid = "+sotrlist[i]+" and extract(Month from payday)="+payday.Month+ " and extract(Year from payday)=" + payday.Year;
                    NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                    NpgsqlDataReader reader1 = com1.ExecuteReader();
                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            RowDefinition rwd3 = new RowDefinition();
                            rwd3.Height = new GridLength(50);
                            grid.RowDefinitions.Add(rwd3);

                            Label prepzpLb2 = new Label();
                            prepzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            prepzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            prepzpLb2.Content = reader1.GetDouble(0);
                            prepzpLb2.BorderBrush = Brushes.Black;
                            prepzpLb2.BorderThickness = new Thickness(2);

                            Label shtatzpLb2 = new Label();
                            shtatzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            shtatzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            shtatzpLb2.Content = reader1.GetDouble(1)+reader1.GetDouble(2);
                            shtatzpLb2.BorderBrush = Brushes.Black;
                            shtatzpLb2.BorderThickness = new Thickness(2);

                           

                            Label allzpLb2 = new Label();

                            allzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            allzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                            allzpLb2.Content = reader1.GetDouble(1)+ reader1.GetDouble(2)+ reader1.GetDouble(0); ;
                            allzpLb2.BorderBrush = Brushes.Black;
                            allzpLb2.BorderThickness = new Thickness(2);

                            Label viplzpLb2 = new Label();

                            viplzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            viplzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            viplzpLb2.Content = reader1.GetDouble(3);
                            viplzpLb2.BorderBrush = Brushes.Black;
                            viplzpLb2.BorderThickness = new Thickness(2);

                            allprep += reader1.GetDouble(0);
                            allshtat += reader1.GetDouble(1)+ reader1.GetDouble(2);
                            allvipl += reader1.GetDouble(3);

                            Grid.SetRow(prepzpLb2, i+2); Grid.SetRow(shtatzpLb2, i + 2);  Grid.SetRow(allzpLb2, i + 2); Grid.SetRow(viplzpLb2, i + 2);
                            Grid.SetColumn(prepzpLb2, 0); Grid.SetColumn(shtatzpLb2, 1);  Grid.SetColumn(allzpLb2,2); Grid.SetColumn(viplzpLb2, 3);
                            grid.Children.Add(prepzpLb2); grid.Children.Add(shtatzpLb2);  grid.Children.Add(allzpLb2); grid.Children.Add(viplzpLb2);
                       
                        }

                    }

                    if (reader1.HasRows==false)
                    {

                            RowDefinition rwd3 = new RowDefinition();
                            rwd3.Height = new GridLength(50);
                            grid.RowDefinitions.Add(rwd3);

                            Label prepzpLb2 = new Label();

                        prepzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        prepzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        prepzpLb2.Content = "-";
                            prepzpLb2.BorderBrush = Brushes.Black;
                            prepzpLb2.BorderThickness = new Thickness(2);

                            Label shtatzpLb2 = new Label();

                        shtatzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        shtatzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        shtatzpLb2.Content = "-";
                            shtatzpLb2.BorderBrush = Brushes.Black;
                            shtatzpLb2.BorderThickness = new Thickness(2);


                            Label allzpLb2 = new Label();


                        allzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        allzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        allzpLb2.Content = "-";
                            allzpLb2.BorderBrush = Brushes.Black;
                            allzpLb2.BorderThickness = new Thickness(2);

                            Label viplzpLb2 = new Label();

                        viplzpLb2.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        viplzpLb2.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        viplzpLb2.Content = "-";
                            viplzpLb2.BorderBrush = Brushes.Black;
                            viplzpLb2.BorderThickness = new Thickness(2);

                            allprep += 0;
                            allshtat += 0;
                            allvipl += 0;

                            Grid.SetRow(prepzpLb2, i + 2); Grid.SetRow(shtatzpLb2, i + 2);  Grid.SetRow(allzpLb2, i + 2); Grid.SetRow(viplzpLb2, i + 2);
                            Grid.SetColumn(prepzpLb2, 0); Grid.SetColumn(shtatzpLb2, 1);  Grid.SetColumn(allzpLb2, 2); Grid.SetColumn(viplzpLb2, 3);
                            grid.Children.Add(prepzpLb2); grid.Children.Add(shtatzpLb2);  grid.Children.Add(allzpLb2); grid.Children.Add(viplzpLb2);
                    }
                    con1.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных "); return; }
            }

            RowDefinition rwd4 = new RowDefinition();
            rwd4.Height = new GridLength(50);
            grid.RowDefinitions.Add(rwd4);

            Label prepzpLb3 = new Label();

            prepzpLb3.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            prepzpLb3.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

            prepzpLb3.Content = Math.Round(allprep,2);
            prepzpLb3.BorderBrush = Brushes.Black;
            prepzpLb3.BorderThickness = new Thickness(2);

            Label shtatzpLb3 = new Label();

            shtatzpLb3.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            shtatzpLb3.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            shtatzpLb3.Content = Math.Round(allshtat, 2);
            shtatzpLb3.BorderBrush = Brushes.Black;
            shtatzpLb3.BorderThickness = new Thickness(2);
            Label allzpLb3 = new Label();

            allzpLb3.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            allzpLb3.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            allzpLb3.Content = Math.Round(allobsl + allprep + allshtat, 2);
            allzpLb3.BorderBrush = Brushes.Black;
            allzpLb3.BorderThickness = new Thickness(2);

            Label viplzpLb3 = new Label();

            viplzpLb3.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            viplzpLb3.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            viplzpLb3.Content = Math.Round(allvipl, 2);
            viplzpLb3.BorderBrush = Brushes.Black;
            viplzpLb3.BorderThickness = new Thickness(2);

            Grid.SetRow(prepzpLb3, grid.RowDefinitions.Count - 1); Grid.SetRow(shtatzpLb3, grid.RowDefinitions.Count - 1);  Grid.SetRow(allzpLb3, grid.RowDefinitions.Count - 1); Grid.SetRow(viplzpLb3, grid.RowDefinitions.Count - 1);
            Grid.SetColumn(prepzpLb3, 0); Grid.SetColumn(shtatzpLb3, 1);  Grid.SetColumn(allzpLb3, 2); Grid.SetColumn(viplzpLb3, 3);
            grid.Children.Add(prepzpLb3); grid.Children.Add(shtatzpLb3);  grid.Children.Add(allzpLb3); grid.Children.Add(viplzpLb3);
        }



        //обновление грида отчтёа ститистики 
        public static void updateGridStatistica(string connectionString, OxyPlot.Wpf.PlotView plot)
        {
            PlotModel model = new PlotModel();
            Axis ax = new DateTimeAxis();
            
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select max(itogidate),min(itogidate) from itog";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                    if (reader1.GetDateTime(1) == reader1.GetDateTime(0)) { System.Windows.Forms.MessageBox.Show("Невозможно построить график"); return; }
                        ax.AbsoluteMinimum = DateTimeAxis.ToDouble(reader1.GetDateTime(1));
                        ax.AbsoluteMaximum = DateTimeAxis.ToDouble(reader1.GetDateTime(0));
                        ax.Zoom(DateTimeAxis.ToDouble(reader1.GetDateTime(1)), DateTimeAxis.ToDouble(reader1.GetDateTime(0)));
                        model.Axes.Add(ax);
                    }

                }
                con1.Close();

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных "); return; }




    Axis ax2 = new LinearAxis();
            ArrayList ls = new ArrayList();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select max(dohod),min(dohod),max(rashod),min(rashod) from itog";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ls.Add(reader.GetDouble(0));
                        ls.Add(reader.GetDouble(1));
                        ls.Add(reader.GetDouble(2));
                        ls.Add(reader.GetDouble(3));
                    }

                }
                if (reader.HasRows == false) { System.Windows.Forms.MessageBox.Show("Невозможно построить график"); return;  }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            ls.Sort();

            if (Convert.ToDouble(ls[0]) == Convert.ToDouble(ls[3])) { System.Windows.Forms.MessageBox.Show("Невозможно построить график"); return; }

            ax2.AbsoluteMinimum = Convert.ToDouble(ls[0]);
            ax2.AbsoluteMaximum = Convert.ToDouble(ls[3]);
            ax2.Zoom(Convert.ToDouble(ls[0]), Convert.ToDouble(ls[3]));

            model.Axes.Add(ax2);


            LineSeries ls1 = new LineSeries();
            ls1.Color = OxyColor.FromRgb(0, 255, 0);
            LineSeries ls2 = new LineSeries();
            ls2.Color = OxyColor.FromRgb(255, 0, 0);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select dohod,rashod,itogidate from itog";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        ls1.Points.Add(new DataPoint(DateTimeAxis.ToDouble(reader.GetDateTime(2)), reader.GetDouble(0)));
                        ls2.Points.Add(new DataPoint(DateTimeAxis.ToDouble(reader.GetDateTime(2)), reader.GetDouble(1)));
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            model.Series.Add(ls1);
            model.Series.Add(ls2);
            model.Title = "Отчёт 'Статистика доходов\\расходов' на "+DateTime.Now.ToShortDateString();
            plot.Model = model;
        }

        //обновление грида кассы
        public static void updateGridKassa(string connectionString, Grid KassaDodohGrid, Grid KassaRashodGrid, Label kassaTitleLabel, Label KassaItogoDohod, Label KassaItogoRashod, Label kassaAllDohodLabel,string sqld,string sqlr)
        {
            kassaTitleLabel.Content = "Отчёт 'Касса' на " + DateTime.Now.ToShortDateString();
            double sum_ras = 0;
            double sum_doh = 0;
            KassaDodohGrid.RowDefinitions.Clear();
            KassaDodohGrid.Children.Clear();

            KassaRashodGrid.RowDefinitions.Clear();
            KassaRashodGrid.Children.Clear();

            RowDefinition rwd = new RowDefinition();
            rwd.Height = new GridLength(50);

            RowDefinition rwdd = new RowDefinition();
            rwdd.Height = new GridLength(50);

            KassaDodohGrid.RowDefinitions.Add(rwd);
            Label l11 = new Label();
            Label l12 = new Label();
            Label l13 = new Label();
            Label l14 = new Label();
            l11.Content = "Дата";
            l12.Content = "Тип";
            l13.Content = "Кто внес";
            l14.Content = "Сумма";

            l11.BorderThickness = new Thickness(2);
            l11.BorderBrush = Brushes.Black;
            l12.BorderThickness = new Thickness(2);
            l12.BorderBrush = Brushes.Black;
            l13.BorderThickness = new Thickness(2);
            l13.BorderBrush = Brushes.Black;
            l14.BorderThickness = new Thickness(2);
            l14.BorderBrush = Brushes.Black;

            KassaRashodGrid.RowDefinitions.Add(rwdd);
            Label l21 = new Label();
            Label l22 = new Label();
            Label l23 = new Label();
            Label l24 = new Label();


            l21.Content = "Дата";
            l22.Content = "Тип";
            l23.Content = "Кому";
            l24.Content = "Сумма";

            l21.BorderThickness = new Thickness(2);
            l21.BorderBrush = Brushes.Black;
            l22.BorderThickness = new Thickness(2);
            l22.BorderBrush = Brushes.Black;
            l23.BorderThickness = new Thickness(2);
            l23.BorderBrush = Brushes.Black;
            l24.BorderThickness = new Thickness(2);
            l24.BorderBrush = Brushes.Black;


            Grid.SetRow(l11,0);
            Grid.SetRow(l12, 0);
            Grid.SetRow(l13, 0);
            Grid.SetRow(l14, 0);
            Grid.SetRow(l21, 0);
            Grid.SetRow(l22, 0);
            Grid.SetRow(l23, 0);
            Grid.SetRow(l24, 0);

            Grid.SetColumn(l11, 0);
            Grid.SetColumn(l12, 1);
            Grid.SetColumn(l13, 2);
            Grid.SetColumn(l14, 3);
            Grid.SetColumn(l21, 0);
            Grid.SetColumn(l22, 1);
            Grid.SetColumn(l23, 2);
            Grid.SetColumn(l24, 3);

            KassaDodohGrid.Children.Add(l11);
            KassaDodohGrid.Children.Add(l12);
            KassaDodohGrid.Children.Add(l13);
            KassaDodohGrid.Children.Add(l14);

            KassaRashodGrid.Children.Add(l21);
            KassaRashodGrid.Children.Add(l22);
            KassaRashodGrid.Children.Add(l23);
            KassaRashodGrid.Children.Add(l24);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                
                NpgsqlCommand com = new NpgsqlCommand(sqld, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        Label date = new Label();
                        Label type = new Label();
                        Label sum = new Label();
                        Label fio = new Label();
                        date.Content = reader.GetDateTime(0).ToShortDateString();
                        type.Content = reader.GetString(1);
                        sum.Content = reader.GetDouble(2);
                        fio.Content = reader.GetString(3);

                        sum_doh += reader.GetDouble(2);

                        date.BorderThickness = new Thickness(2);
                        date.BorderBrush = Brushes.Black;
                        type.BorderThickness = new Thickness(2);
                        type.BorderBrush = Brushes.Black;
                        sum.BorderThickness = new Thickness(2);
                        sum.BorderBrush = Brushes.Black;
                        fio.BorderThickness = new Thickness(2);
                        fio.BorderBrush = Brushes.Black;

                        RowDefinition rwd1= new RowDefinition();
                        rwd1.Height = new GridLength(50);
                        KassaDodohGrid.RowDefinitions.Add(rwd1);

                        Grid.SetRow(date, i);
                        Grid.SetRow(type, i);
                        Grid.SetRow(sum, i);
                        Grid.SetRow(fio, i);

                        Grid.SetColumn(date, 0);
                        Grid.SetColumn(type, 1);
                        Grid.SetColumn(fio, 2);
                        Grid.SetColumn(sum, 3);
                        KassaDodohGrid.Children.Add(date);
                        KassaDodohGrid.Children.Add(type);
                        KassaDodohGrid.Children.Add(sum);
                        KassaDodohGrid.Children.Add(fio);
                        i++;
                    }

                }
                con.Close();

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
               
                NpgsqlCommand com = new NpgsqlCommand(sqlr, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        Label date = new Label();
                        Label type = new Label();
                        Label fio = new Label();
                        Label sum = new Label();
                        date.Content = reader.GetDateTime(0).ToShortDateString();
                        type.Content = reader.GetString(1);
                        fio.Content = reader.GetString(2);
                        sum.Content = reader.GetDouble(3);

                        sum_ras += reader.GetDouble(3);

                        date.BorderThickness = new Thickness(2);
                        date.BorderBrush = Brushes.Black;
                        type.BorderThickness = new Thickness(2);
                        type.BorderBrush = Brushes.Black;
                        sum.BorderThickness = new Thickness(2);
                        sum.BorderBrush = Brushes.Black;
                        fio.BorderThickness = new Thickness(2);
                        fio.BorderBrush = Brushes.Black;

                        RowDefinition rwd1 = new RowDefinition();
                        rwd1.Height = new GridLength(50);
                        KassaRashodGrid.RowDefinitions.Add(rwd1);

                        Grid.SetRow(date, i);
                        Grid.SetRow(type, i);
                        Grid.SetRow(sum, i);
                        Grid.SetRow(fio, i);

                        Grid.SetColumn(date, 0);
                        Grid.SetColumn(type, 1);
                        Grid.SetColumn(fio, 2);
                        Grid.SetColumn(sum, 3);
                        KassaRashodGrid.Children.Add(date);
                        KassaRashodGrid.Children.Add(type);
                        KassaRashodGrid.Children.Add(sum);
                        KassaRashodGrid.Children.Add(fio);
                        i++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            KassaItogoDohod.Content = "Итого: " + sum_doh;
            KassaItogoRashod.Content = "Итого: " + sum_ras;
            kassaAllDohodLabel.Content = "Общий доход: " + (sum_doh - sum_ras);
        }


        //обновление грида оплат
        public static void updateDataGridDolg(string connectionString, Grid MonthOplGridDolg, ComboBox GroupsDolg, ComboBox ListenerDolg, TextBox[] masTbx2, Label DataPerehoda, Label isStopDolg)
        {
            MonthOplGridDolg.ColumnDefinitions.Clear();
            MonthOplGridDolg.Children.Clear();
            //построение таблицы
            int kol_Month = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payformonth,'_'), array_to_string(payedlist,'_'), array_to_string(skidkiforpay,'_'), array_to_string(topay,'_'), array_to_string(penya,'_'), date_stop,year  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + GroupsDolg.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payformonth = reader.GetString(0);
                        string[] payformonthMas = payformonth.Split('_');

                        string payedlist = reader.GetString(1);
                        string[] payedlistMas = payedlist.Split('_');

                        string skidkiforpay = reader.GetString(2);
                        string[] skidkiforpayMas = skidkiforpay.Split('_');

                        string topay = reader.GetString(3);
                        string[] topayMas = topay.Split('_');

                        string penya = reader.GetString(4);
                        string[] penyaMas = penya.Split('_');

                      

                        if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { isStopDolg.Content = "Обучение остановленно " + reader.GetDateTime(5).ToShortDateString(); }
                        if (reader.IsDBNull(reader.GetOrdinal("date_stop"))) { isStopDolg.Content = "Обучение не остановленно"; }
                        DataPerehoda.Content = "Дата добавления записи " + reader.GetDateTime(6).ToShortDateString(); ;

                        ArrayList Month = new ArrayList();

                        for (int i = 0; i < 12; i++)
                        {
                            if (payformonthMas[i] != "0")
                            {
                                switch (i)
                                {
                                    case 0: { Month.Add("Январь"); break; }
                                    case 1: { Month.Add("Февраль"); break; }
                                    case 2: { Month.Add("Март"); break; }
                                    case 3: { Month.Add("Апрель"); break; }
                                    case 4: { Month.Add("Май"); break; }
                                    case 5: { Month.Add("Июнь"); break; }
                                    case 6: { Month.Add("Июль"); break; }
                                    case 7: { Month.Add("Август"); break; }
                                    case 8: { Month.Add("Сентябрь"); break; }
                                    case 9: { Month.Add("Октрябрь"); break; }
                                    case 10: { Month.Add("Ноябрь"); break; }
                                    case 11: { Month.Add("Декабрь"); break; }

                                }
                                kol_Month++;

                            }
                        }


                        Label[] Monthlabel = new Label[kol_Month];
                        Label[] payformonthLabel = new Label[kol_Month];
                        Label[] payedlistLabel = new Label[kol_Month];
                        Label[] skidkiforpayLabel = new Label[kol_Month];
                        Label[] topayLabel = new Label[kol_Month];
                        Label[] penyalabel = new Label[kol_Month];
                        int j = 0;



                        for (int i = 0; i < kol_Month; i++)
                        {

                            Monthlabel[i] = new Label();
                            payformonthLabel[i] = new Label();
                            payedlistLabel[i] = new Label();
                            skidkiforpayLabel[i] = new Label();
                            topayLabel[i] = new Label();
                            penyalabel[i] = new Label();

                            masTbx2[i].BorderThickness = new Thickness(2);
                            Monthlabel[i].BorderThickness = new Thickness(2);
                            payformonthLabel[i].BorderThickness = new Thickness(2);
                            payedlistLabel[i].BorderThickness = new Thickness(2);
                            skidkiforpayLabel[i].BorderThickness = new Thickness(2);
                            topayLabel[i].BorderThickness = new Thickness(2);
                            penyalabel[i].BorderThickness = new Thickness(2);

                            masTbx2[i].BorderBrush = Brushes.Black;
                            Monthlabel[i].BorderBrush = Brushes.Black;
                            payformonthLabel[i].BorderBrush = Brushes.Black;
                            payedlistLabel[i].BorderBrush = Brushes.Black;
                            skidkiforpayLabel[i].BorderBrush = Brushes.Black;
                            topayLabel[i].BorderBrush = Brushes.Black;
                            penyalabel[i].BorderBrush = Brushes.Black;


                            while (payformonthMas[j] == "0")
                            {
                                j++;
                            }

                            payformonthLabel[i].Content = payformonthMas[j];
                            payedlistLabel[i].Content = payedlistMas[j];
                            skidkiforpayLabel[i].Content = skidkiforpayMas[j];
                            topayLabel[i].Content = topayMas[j];
                            penyalabel[i].Content = penyaMas[j];

                            Monthlabel[i].Content = Month[i];

                            ColumnDefinition cmd = new ColumnDefinition();
                            cmd.Width = new GridLength(100);
                            MonthOplGridDolg.ColumnDefinitions.Add(cmd);

                            Grid.SetColumn(masTbx2[i], (i));
                            Grid.SetColumn(Monthlabel[i], (i));
                            Grid.SetColumn(payformonthLabel[i], (i));
                            Grid.SetColumn(payedlistLabel[i], (i));
                            Grid.SetColumn(skidkiforpayLabel[i], (i));
                            Grid.SetColumn(topayLabel[i], (i));
                            Grid.SetColumn(penyalabel[i], (i));


                            Grid.SetRow(masTbx2[i], 6);
                            Grid.SetRow(Monthlabel[i], 0);
                            Grid.SetRow(payformonthLabel[i], 1);
                            Grid.SetRow(payedlistLabel[i], 2);
                            Grid.SetRow(skidkiforpayLabel[i], 3);
                            Grid.SetRow(topayLabel[i], 5);
                            Grid.SetRow(penyalabel[i], 4);

                            MonthOplGridDolg.Children.Add(masTbx2[i]);
                            MonthOplGridDolg.Children.Add(Monthlabel[i]);
                            MonthOplGridDolg.Children.Add(payformonthLabel[i]);
                            MonthOplGridDolg.Children.Add(payedlistLabel[i]);
                            MonthOplGridDolg.Children.Add(skidkiforpayLabel[i]);
                            MonthOplGridDolg.Children.Add(topayLabel[i]);
                            MonthOplGridDolg.Children.Add(penyalabel[i]);
                            j++;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление grid начислений зп
        public static void updateGridNachZp(string connectionString, Label NachMonthLabel,CheckBox [] ChbxMas_SotrNuch, Grid NachSotrGrid,DataGrid NachDataGrid,DateTime dateNuch)
        {
            NachSotrGrid.Children.Clear();
            NachSotrGrid.RowDefinitions.Clear();
            NachMonthLabel.Content = "Начисления на ";
            switch (dateNuch.Month)
            {
                case 1: { NachMonthLabel.Content += "январь " + dateNuch.Year; break; }
                case 2: { NachMonthLabel.Content += "февраль " + dateNuch.Year; break; }
                case 3: { NachMonthLabel.Content += "март " + dateNuch.Year; break; }
                case 4: { NachMonthLabel.Content += "апрель " + dateNuch.Year; break; }
                case 5: { NachMonthLabel.Content += "май " + dateNuch.Year; break; }
                case 6: { NachMonthLabel.Content += "июнь " + dateNuch.Year; break; }
                case 7: { NachMonthLabel.Content += "июль " + dateNuch.Year; break; }
                case 8: { NachMonthLabel.Content += "август " + dateNuch.Year; break; }
                case 9: { NachMonthLabel.Content += "сентябрь " + dateNuch.Year; break; }
                case 10: { NachMonthLabel.Content += "октябрь " + dateNuch.Year; break; }
                case 11: { NachMonthLabel.Content += "ноябрь " + dateNuch.Year; break; }
                case 12: { NachMonthLabel.Content += "декабрь " + dateNuch.Year; break; }

            }
          
            //заполнение грида сотрудников
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio,sotrid from sotrudniki where sotrid in (select sotrid from shtat) or sotrid in (select sotrid from prep)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ChbxMas_SotrNuch[i] = new CheckBox();
                        ChbxMas_SotrNuch[i].Name = "id_" + reader.GetInt32(1);
                        ChbxMas_SotrNuch[i].Content = reader.GetString(0);
                        RowDefinition cmd = new RowDefinition();
                        cmd.Height = new GridLength(20);
                        NachSotrGrid.RowDefinitions.Add(cmd);
                        Grid.SetRow(ChbxMas_SotrNuch[i], i);
                        NachSotrGrid.Children.Add(ChbxMas_SotrNuch[i]);
                        i++;

                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //заполнение грида начислений
            try
            {
                DataTable table = new DataTable();
                object[] sql_mass = new object[10];
                table.Columns.Add("nachid", System.Type.GetType("System.Int32"));
                table.Columns.Add("fio", System.Type.GetType("System.String"));
                table.Columns.Add("prepzp", System.Type.GetType("System.Double"));
                table.Columns.Add("shtatzp", System.Type.GetType("System.Double"));

                table.Columns.Add("vs", System.Type.GetType("System.Double"));
                table.Columns.Add("fss", System.Type.GetType("System.Double"));
                table.Columns.Add("ndfl", System.Type.GetType("System.Double"));

                table.Columns.Add("allzp", System.Type.GetType("System.Double"));
                table.Columns.Add("viplacheno", System.Type.GetType("System.Double"));
                table.Columns.Add("topay", System.Type.GetType("System.Double"));
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nachisl.nachid ,sotrudniki.fio,nachisl.prepzp,nachisl.shtatzp,nachisl.obslzp,nachisl.vs,nachisl.fss,nachisl.ndfl,nachisl.viplacheno from nachisl inner join sotrudniki using(sotrid) where EXTRACT(Year FROM nachisl.payday)=" + dateNuch.Year+" and  EXTRACT(Month FROM nachisl.payday)=" + dateNuch.Month;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sql_mass[0] = reader.GetInt32(0);
                        sql_mass[1] = reader.GetString(1);
                        sql_mass[2] = reader.GetDouble(2);
                        sql_mass[3] = reader.GetDouble(3) + reader.GetDouble(4);
                     

                        sql_mass[4] = reader.GetDouble(5);
                        sql_mass[5] = reader.GetDouble(6);
                        sql_mass[6] = reader.GetDouble(7);

                        sql_mass[7] = Math.Round(reader.GetDouble(2)+ reader.GetDouble(3)+ reader.GetDouble(4),2);
                        sql_mass[8] = reader.GetDouble(8);

                        
                        sql_mass[9] = Math.Round((reader.GetDouble(2) + reader.GetDouble(3) + reader.GetDouble(4))- reader.GetDouble(8),2);
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sql_mass;
                        table.Rows.Add(row);
                    }


                }
                con.Close();
                NachDataGrid.ItemsSource = table.DefaultView;
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление grid штатного расписания
        public static void updateGridShtatRasp(string connectionString, Grid gridDate, Grid gridSotr,Label[,] lbmas,CheckBox []ChbxMas,Label ShtatRaspMonthYearLabel, DateTime date)
        {
            gridDate.Children.Clear();
            gridSotr.Children.Clear();
            gridSotr.RowDefinitions.Clear();
            ShtatRaspMonthYearLabel.Content = "Посещения на ";
            switch (date.Month)
            {
                case 1: { ShtatRaspMonthYearLabel.Content += "январь " + date.Year; break; }
                case 2: { ShtatRaspMonthYearLabel.Content += "февраль " + date.Year; break; }
                case 3: { ShtatRaspMonthYearLabel.Content += "март " + date.Year; break; }
                case 4: { ShtatRaspMonthYearLabel.Content += "апрель " + date.Year; break; }
                case 5: { ShtatRaspMonthYearLabel.Content += "май " + date.Year; break; }
                case 6: { ShtatRaspMonthYearLabel.Content += "июнь " + date.Year; break; }
                case 7: { ShtatRaspMonthYearLabel.Content += "июль " + date.Year; break; }
                case 8: { ShtatRaspMonthYearLabel.Content += "август " + date.Year; break; }
                case 9: { ShtatRaspMonthYearLabel.Content += "сентябрь " + date.Year; break; }
                case 10: { ShtatRaspMonthYearLabel.Content += "октябрь " + date.Year; break; }
                case 11: { ShtatRaspMonthYearLabel.Content += "ноябрь " + date.Year; break; }
                case 12: { ShtatRaspMonthYearLabel.Content += "декабрь " + date.Year; break; }

            }
            DateTime newDate = new DateTime(date.Year, date.Month, 1);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Grid.SetColumn(lbmas[i, j],j);
                    Grid.SetRow(lbmas[i, j], i);
                    gridDate.Children.Add(lbmas[i, j]);

                    if (i==0)
                    {
                        switch (j)
                        {
                            case 0: { lbmas[i, j].Content = "ПН"; break; }
                            case 1: { lbmas[i, j].Content = "ВТ"; break; }
                            case 2: { lbmas[i, j].Content = "СР"; break; }
                            case 3: { lbmas[i, j].Content = "ЧТ"; break; }
                            case 4: { lbmas[i, j].Content = "ПТ"; break; }
                            case 5: { lbmas[i, j].Content = "СБ"; break; }
                            case 6: { lbmas[i, j].Content = "ВС"; break; }
                        }


                    }
                }
            
            }
            int index_j = 0;
            int index_i = 1;
            while (newDate.Month == date.Month)
            {
                switch(newDate.DayOfWeek.ToString())
                {
                    case "Monday": { index_j = 0;break; }
                    case "Tuesday": { index_j = 1; break; }
                    case "Wednesday": { index_j = 2; break; }
                    case "Thursday": { index_j = 3; break; }
                    case "Friday": { index_j = 4; break; }
                    case "Saturday": { index_j = 5; break; }
                    case "Sunday": { index_j = 6; break; }
                }
                lbmas[index_i, index_j].Content = newDate.Day;
                if (index_j == 6) index_i++;
                newDate= newDate.AddDays(1);

            }
           

            try {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio,sotrid from shtat inner join sotrudniki using(sotrid)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ChbxMas[i] = new CheckBox();
                        ChbxMas[i].Name = "id_"+reader.GetInt32(1);
                        ChbxMas[i].Content = reader.GetString(0);
                        RowDefinition cmd = new RowDefinition();
                        cmd.Height = new GridLength(20);
                        gridSotr.RowDefinitions.Add(cmd);
                        Grid.SetRow(ChbxMas[i], i);
                        gridSotr.Children.Add(ChbxMas[i]);
                        i++;

                    }
                        
                            
                }
                con.Close();
            } 
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление DataGrid штата
        public static void updateDataGridShtat(string connectionString, string sql ,DataGrid grid)
        {

            try
            {
                DataTable table = new DataTable();
                object[] sql_mass = new object[4];
                table.Columns.Add("shtatid", System.Type.GetType("System.Int32"));
                table.Columns.Add("fio", System.Type.GetType("System.String"));
                table.Columns.Add("states", System.Type.GetType("System.String"));
                table.Columns.Add("obslwork", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();

                
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        sql_mass[0] = reader.GetInt32(0);
                        sql_mass[1] = reader.GetString(1);
                        sql_mass[2] = "Нет";
                        sql_mass[3] = "Нет";
                        //вывод должностей
                        if (reader.GetString(2)!="")
                        {
                            sql_mass[2]="";
                            string[] stavky = reader.GetString(2).Split('_');
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();

                                string sql1 = "select title from states where ARRAY[statesid] <@ (select states from shtat where shtatid=" + sql_mass[0] + " ) order by statesid";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    int i = 0;
                                    while (reader1.Read())
                                    {
                                        sql_mass[2] += reader1.GetString(0) + " - " + stavky[i] + "; ";
                                        i++;
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        }


                        //вывод обслуживающих работ
                        if (reader.GetString(3) != "")
                        {
                            sql_mass[3] = "";
                            string[] obem = reader.GetString(3).Split('_');
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();

                                string sql1 = "select title from raboty_obsl where ARRAY[rabotyid] <@ (select obslwork from shtat where shtatid=" + sql_mass[0] + " ) order by rabotyid";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    int i = 0;
                                    while (reader1.Read())
                                    {
                                        sql_mass[3] += reader1.GetString(0) + " - " + obem[i] + "; ";
                                        i++;
                                    }
                                }
                                con1.Close();
                    }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }

                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sql_mass;
                        table.Rows.Add(row);
                    }

                }
                con.Close();
                grid.ItemsSource = table.DefaultView;

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }


        //обновление DataGrid должностей
        public static void updateDataGridStates(string connectionString, DataGrid grid)
        {

            try
            {
                DataTable table = new DataTable();
                object[] sql_mass = new object[5];
                table.Columns.Add("statesid", System.Type.GetType("System.Int32"));
                table.Columns.Add("title", System.Type.GetType("System.String"));
                table.Columns.Add("kol_work_day", System.Type.GetType("System.String"));
                table.Columns.Add("zp", System.Type.GetType("System.Double"));
                table.Columns.Add("comment", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT statesid, title, array_to_string(kol_work_day,'_'), zp, comment FROM states";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
    
                    while (reader.Read())
                    {
                        sql_mass[0] = reader.GetInt32(0);
                        sql_mass[1] = reader.GetString(1);
                        sql_mass[3] = reader.GetDouble(3);
                        sql_mass[4] = reader.GetString(4);
                        string[] daysMas = reader.GetString(2).Split('_');
                    string workday = "Январь - " + daysMas[0] + " Февраль - " + daysMas[1] + " Март - " + daysMas[2] + " Апрель - " + daysMas[3] + " Май - " + daysMas[4] + "Июнь - " + daysMas[5] + " Июль - " + daysMas[6] + " Август - " + daysMas[7] + " Сентябрь - " + daysMas[8] + " Октябрь - " + daysMas[9] + " Ноябрь - " + daysMas[10] + " Декабрь - " + daysMas[11];
                        sql_mass[2] = workday;
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sql_mass;
                        table.Rows.Add(row);
                    }

                }
                con.Close();
                grid.ItemsSource = table.DefaultView;

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
}


        //обновление DataGrid работ обслуживания
        public static void updateDataGridRaboty(string connectionString, DataGrid grid)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT rabotyid, title, pay,ed_izm, comment FROM raboty_obsl";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid коефициентов за выслугу лет
        public static void updateDataGridKoef(string connectionString, DataGrid grid)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * from koef_vislugi";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid расходов
        public static void updateDataGridRashody(string connectionString, string sql, DataGrid grid)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid доходов
        public static void updateDataGridDohody(string connectionString,string sql ,DataGrid grid)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }


        //обновление грида оплат
        public static void updateDataGridOpat(string connectionString, Grid MonthOplGrid, ComboBox Groups, ComboBox Listener,TextBox [] masTbx,Label isClose,Label isStop,Button Closeing, Button Open, Button StopLern, Button RestartLern)
        {
            MonthOplGrid.ColumnDefinitions.Clear();
            MonthOplGrid.Children.Clear();
            //построение таблицы
            int kol_Month = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payformonth,'_'), array_to_string(payedlist,'_'), array_to_string(skidkiforpay,'_'), array_to_string(topay,'_'), array_to_string(penya,'_'), date_stop, isclose  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payformonth = reader.GetString(0);
                        string[] payformonthMas = payformonth.Split('_');

                        string payedlist = reader.GetString(1);
                        string[] payedlistMas = payedlist.Split('_');

                        string skidkiforpay = reader.GetString(2);
                        string[] skidkiforpayMas = skidkiforpay.Split('_');

                        string topay = reader.GetString(3);
                        string[] topayMas = topay.Split('_');

                        string penya = reader.GetString(4);
                        string[] penyaMas = penya.Split('_');

                        if (reader.GetInt32(6) == 1)
                        {
                            isClose.Content = "Запись об оплате закрыта"; Closeing.Visibility = Visibility.Collapsed; Open.Visibility = Visibility.Visible;
                            
                        }
                        else { isClose.Content = "Запись об оплате открыта"; Closeing.Visibility = Visibility.Visible; Open.Visibility = Visibility.Collapsed; }

                        if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { isStop.Content = "Обучение остановленно " + reader.GetDateTime(5).ToShortDateString(); RestartLern.Visibility = Visibility.Visible; StopLern.Visibility = Visibility.Collapsed; }
                        if (reader.IsDBNull(reader.GetOrdinal("date_stop"))) { isStop.Content = "Обучение не остановленно"; RestartLern.Visibility = Visibility.Collapsed; StopLern.Visibility = Visibility.Visible; }


                        ArrayList Month = new ArrayList();

                        for (int i = 0; i < 12; i++)
                        {
                            if (payformonthMas[i] != "0")
                            {
                                switch (i)
                                {
                                    case 0: { Month.Add("Январь"); break; }
                                    case 1: { Month.Add("Февраль"); break; }
                                    case 2: { Month.Add("Март"); break; }
                                    case 3: { Month.Add("Апрель"); break; }
                                    case 4: { Month.Add("Май"); break; }
                                    case 5: { Month.Add("Июнь"); break; }
                                    case 6: { Month.Add("Июль"); break; }
                                    case 7: { Month.Add("Август"); break; }
                                    case 8: { Month.Add("Сентябрь"); break; }
                                    case 9: { Month.Add("Октрябрь"); break; }
                                    case 10: { Month.Add("Ноябрь"); break; }
                                    case 11: { Month.Add("Декабрь"); break; }

                                }
                                kol_Month++;

                            }
                        }


                        Label[] Monthlabel = new Label[kol_Month];
                        Label[] payformonthLabel = new Label[kol_Month];
                        Label[] payedlistLabel = new Label[kol_Month];
                        Label[] skidkiforpayLabel = new Label[kol_Month];
                        Label[] topayLabel = new Label[kol_Month];
                        Label[] penyalabel = new Label[kol_Month];
                        int j = 0;



                        for (int i = 0; i < kol_Month; i++)
                        {

                            Monthlabel[i] = new Label();
                            payformonthLabel[i] = new Label();
                            payedlistLabel[i] = new Label();
                            skidkiforpayLabel[i] = new Label();
                            topayLabel[i] = new Label();
                            penyalabel[i] = new Label();

                            masTbx[i].BorderThickness = new Thickness(2);
                            Monthlabel[i].BorderThickness = new Thickness(2);
                            payformonthLabel[i].BorderThickness = new Thickness(2);
                            payedlistLabel[i].BorderThickness = new Thickness(2);
                            skidkiforpayLabel[i].BorderThickness = new Thickness(2);
                            topayLabel[i].BorderThickness = new Thickness(2);
                            penyalabel[i].BorderThickness = new Thickness(2);

                            masTbx[i].BorderBrush = Brushes.Black;
                            Monthlabel[i].BorderBrush = Brushes.Black;
                            payformonthLabel[i].BorderBrush = Brushes.Black;
                            payedlistLabel[i].BorderBrush = Brushes.Black;
                            skidkiforpayLabel[i].BorderBrush = Brushes.Black;
                            topayLabel[i].BorderBrush = Brushes.Black;
                            penyalabel[i].BorderBrush = Brushes.Black;
                           

                            while (payformonthMas[j] == "0")
                            {
                                j++;
                            }

                            payformonthLabel[i].Content = payformonthMas[j];
                            payedlistLabel[i].Content = payedlistMas[j];
                            skidkiforpayLabel[i].Content = skidkiforpayMas[j];
                            topayLabel[i].Content = topayMas[j];
                            penyalabel[i].Content = penyaMas[j];

                            Monthlabel[i].Content = Month[i];

                            ColumnDefinition cmd = new ColumnDefinition();
                            cmd.Width = new GridLength(100);
                            MonthOplGrid.ColumnDefinitions.Add(cmd);

                            Grid.SetColumn(masTbx[i], (i));
                            Grid.SetColumn(Monthlabel[i], (i));
                            Grid.SetColumn(payformonthLabel[i], (i));
                            Grid.SetColumn(payedlistLabel[i], (i));
                            Grid.SetColumn(skidkiforpayLabel[i], (i));
                            Grid.SetColumn(topayLabel[i], (i));
                            Grid.SetColumn(penyalabel[i], (i));


                            Grid.SetRow(masTbx[i], 6);
                            Grid.SetRow(Monthlabel[i], 0);
                            Grid.SetRow(payformonthLabel[i], 1);
                            Grid.SetRow(payedlistLabel[i], 2);
                            Grid.SetRow(skidkiforpayLabel[i], 3);
                            Grid.SetRow(topayLabel[i], 5);
                            Grid.SetRow(penyalabel[i], 4);

                            MonthOplGrid.Children.Add(masTbx[i]);
                            MonthOplGrid.Children.Add(Monthlabel[i]);
                            MonthOplGrid.Children.Add(payformonthLabel[i]);
                            MonthOplGrid.Children.Add(payedlistLabel[i]);
                            MonthOplGrid.Children.Add(skidkiforpayLabel[i]);
                            MonthOplGrid.Children.Add(topayLabel[i]);
                            MonthOplGrid.Children.Add(penyalabel[i]);
                            j++;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
        }

        //обновление грида типов расходов
        public static void updateDataGriTypeRash(string connectionString, DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * from typerash";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление грида типов доходов
        public static void updateDataGriTypeDoh(string connectionString, DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * from typedohod";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление грида кабинетов
        public static void updateDataGridСab(string connectionString, DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * from cabinet";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid слушателей
        public static void updateDataGridListener(string connectionString, string sql ,DataGrid grid)
        {

            try
            {
                DataTable table = new DataTable();
                object[] sql_mass = new object[5];
                table.Columns.Add("listenerid", System.Type.GetType("System.Int32"));
                table.Columns.Add("fio", System.Type.GetType("System.String"));
                table.Columns.Add("phones", System.Type.GetType("System.String"));
                table.Columns.Add("gr_lg", System.Type.GetType("System.String"));
                table.Columns.Add("comment", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                
                NpgsqlCommand com = new NpgsqlCommand(sql,con);
                NpgsqlDataReader reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                int arrLeng = 0;
                string[] grMas;

                while (reader.Read())
                {
                    sql_mass[0] = reader.GetInt32(0);
                    sql_mass[1] = reader.GetString(1);
                    sql_mass[2] = reader.GetString(2);
                    sql_mass[4] = reader.GetString(3);
                    if (!reader.IsDBNull(reader.GetOrdinal("grid")))
                    {
                        arrLeng = reader.GetInt32(4);
                        grMas = new string[arrLeng];
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                            con2.Open();
                            string sql2 = "select nazvanie from groups where ARRAY[grid] <@ (select grid from listeners where listenerid =" + sql_mass[0] + ") order by grid";
                            NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                int i = 0;
                                while (reader2.Read())
                                {
                                    grMas[i] = reader2.GetString(0);
                                    i++;
                                }
                            }
                            con2.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        string sql3 = "select ";
                        for (int ii = 0; ii < arrLeng; ii++)
                        {
                            sql3 += "lgt[" + (ii + 1) + "],";
                        }
                        sql3 = sql3.Substring(0, sql3.Length - 1);
                        sql3 += " from listeners where listenerid =" + sql_mass[0];
                        try
                        {
                            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                            con3.Open();
                            NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                            NpgsqlDataReader reader3 = com3.ExecuteReader();
                            string srtGr_Lg = "";
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    for (int i = 0; i < arrLeng; i++) { srtGr_Lg += "Группа: " + grMas[i] + " Процент " + reader3.GetDouble(i) + " "; }
                                }

                                sql_mass[3] = srtGr_Lg;
                            }
                            con3.Close();

                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        }
                        else sql_mass[3] = "нет";
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sql_mass;
                        table.Rows.Add(row);


                   
                }
            }
                
                con.Close();
                grid.ItemsSource = table.DefaultView;

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
}

        //обновление грида расписания (по кабинетам)
        public static void updateGridRaspС(string connectionString, Grid tG, int m, int n, Label[,] lbmas, DateTime dateMonday, DateTime dateSunday)
        {
            Array.Clear(lbmas, 0, lbmas.Length);
            tG.Children.Clear();
            tG.RowDefinitions.Clear();
            tG.ColumnDefinitions.Clear();
            Label[] day = new Label[7];
            for (int i = 0; i < 7; i++)
            {
                day[i] = new Label();
                day[i].FontSize = 20;
                day[i].LayoutTransform = new RotateTransform(90);
                day[i].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                day[i].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            }
            day[0].Content = "Понедельник";
            day[1].Content = "Вторник";
            day[2].Content = "Среда";
            day[3].Content = "Четверг";
            day[4].Content = "Пятница";
            day[5].Content = "Суббота";
            day[6].Content = "Воскресенье";
            DateTime dm = dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n" + dm.AddDays(i).ToShortDateString();
            }


            for (int i = 0; i < n + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                tG.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (m * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                tG.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    lbmas[i, j] = new Label();
                    lbmas[i, j].FontSize = 16;
                    lbmas[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    lbmas[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    lbmas[i, j].Name = "_" + i + "_" + j;
                    lbmas[i, j].Content = "";
                    Thickness thk = new Thickness(2);
                    Brush brsh = Brushes.Black;
                    lbmas[i, j].BorderThickness = thk;
                    lbmas[i, j].BorderBrush = brsh;
                    tG.Children.Add(lbmas[i, j]);
                    Grid.SetRow(lbmas[i, j], i);
                    Grid.SetColumn(lbmas[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
                tG.Children.Add(day[i - 1]);
                Thickness thk = new Thickness(2);
                Brush brsh = Brushes.Black;
                day[i - 1].BorderThickness = thk;
                day[i - 1].BorderBrush = brsh;
                Grid.SetRowSpan(day[i - 1], m);
                Grid.SetRow(day[i - 1], (i * m) - (m - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from lessons_time order by lesson_number";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int number = reader.GetInt32(1);
                        string s = "" + number + "\n" + reader.GetTimeSpan(2).ToString() + "\n" + reader.GetTimeSpan(3).ToString();
                        for (int i = number; i <= m * 7; i += m)
                        {
                            lbmas[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод кабинетов
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select num,cabid from cabinet order by num";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        lbmas[0, j].Content = reader.GetString(0);
                        lbmas[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + dateMonday.Day + "-" + dateMonday.Month + "-" + dateMonday.Year + "' order by raspisanie.day,cabinet.num,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayWeek = reader1.GetInt32(0);
                        string grTitle = reader1.GetString(1);
                        int lesNum = reader1.GetInt32(2);
                        string subTitle = reader1.GetString(3);
                        string prepFio = reader1.GetString(4);
                        string cab = reader1.GetString(5);
                        int i = (((dayWeek * m) - m)) + lesNum;
                        int jj = 0;
                        for (int j = 2; j < n + 2; j++)
                        {
                            if (lbmas[0, j].Content.ToString() == cab) { jj = j; break; }

                        }
                        lbmas[i, jj].Content = "" + subTitle + "\n" + prepFio + "\n" + grTitle;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление грида расписания (по преподавателям)
        public static void updateGridRaspP(string connectionString, Grid tG, int m, int n, Label[,] lbmas, DateTime dateMonday, DateTime dateSunday)
        {
            Array.Clear(lbmas, 0, lbmas.Length);
            tG.Children.Clear();
            tG.RowDefinitions.Clear();
            tG.ColumnDefinitions.Clear();
            Label[] day = new Label[7];
            for (int i = 0; i < 7; i++)
            {
                day[i] = new Label();
                day[i].FontSize = 20;
                day[i].LayoutTransform = new RotateTransform(90);
                day[i].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                day[i].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            }
            day[0].Content = "Понедельник";
            day[1].Content = "Вторник";
            day[2].Content = "Среда";
            day[3].Content = "Четверг";
            day[4].Content = "Пятница";
            day[5].Content = "Суббота";
            day[6].Content = "Воскресенье";
            DateTime dm = dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n" + dm.AddDays(i).ToShortDateString();
            }

            for (int i = 0; i < n + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                tG.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (m * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                tG.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    lbmas[i, j] = new Label();
                    lbmas[i, j].FontSize = 16;
                    lbmas[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    lbmas[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    lbmas[i, j].Name = "_" + i + "_" + j;
                    lbmas[i, j].Content = "";
                    Thickness thk = new Thickness(2);
                    Brush brsh = Brushes.Black;
                    lbmas[i, j].BorderThickness = thk;
                    lbmas[i, j].BorderBrush = brsh;
                    tG.Children.Add(lbmas[i, j]);
                    Grid.SetRow(lbmas[i, j], i);
                    Grid.SetColumn(lbmas[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
                tG.Children.Add(day[i - 1]);
                Thickness thk = new Thickness(2);
                Brush brsh = Brushes.Black;
                day[i - 1].BorderThickness = thk;
                day[i - 1].BorderBrush = brsh;
                Grid.SetRowSpan(day[i - 1], m);
                Grid.SetRow(day[i - 1], (i * m) - (m - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from lessons_time order by lesson_number";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int number = reader.GetInt32(1);
                        string s = "" + number + "\n" + reader.GetTimeSpan(2).ToString() + "\n" + reader.GetTimeSpan(3).ToString();
                        for (int i = number; i <= m * 7; i += m)
                        {
                            lbmas[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод преподавателей
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select sotrudniki.fio, prep.prepid from prep inner join sotrudniki using(sotrid) order by fio";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        lbmas[0, j].Content = reader.GetString(0);
                        lbmas[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + dateMonday.Day + "-" + dateMonday.Month + "-" + dateMonday.Year + "' order by raspisanie.day,sotrudniki.fio,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayWeek = reader1.GetInt32(0);
                        string grTitle = reader1.GetString(1);
                        int lesNum = reader1.GetInt32(2);
                        string subTitle = reader1.GetString(3);
                        string prepFio = reader1.GetString(4);
                        string cab = reader1.GetString(5);
                        int i = (((dayWeek * m) - m)) + lesNum;
                        int jj = 0;
                        for (int j = 2; j < n + 2; j++)
                        {
                            if (lbmas[0, j].Content.ToString() == prepFio) { jj = j; break; }

                        }
                        lbmas[i, jj].Content = "" + subTitle + "\n" + grTitle+"\n"+cab;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //обновление грида расписания (по группам)

        public static void updateGridRaspG(string connectionString, Grid tG, int m, int n, Label[,] lbmas, DateTime dateMonday, DateTime dateSunday)
        {
            Array.Clear(lbmas, 0, lbmas.Length);
            tG.Children.Clear();
            tG.RowDefinitions.Clear();
            tG.ColumnDefinitions.Clear();
            Label[] day = new Label[7];
            for (int i = 0; i < 7; i++)
            {
                day[i] = new Label();
                day[i].FontSize = 20;
                day[i].LayoutTransform = new RotateTransform(90);
                day[i].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                day[i].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            }
            day[0].Content = "Понедельник";
            day[1].Content = "Вторник";
            day[2].Content = "Среда";
            day[3].Content = "Четверг";
            day[4].Content = "Пятница";
            day[5].Content = "Суббота";
            day[6].Content = "Воскресенье";
            DateTime dm = dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n"+dm.AddDays(i).ToShortDateString();
            }

            for (int i = 0; i < n + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                tG.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (m * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                tG.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    lbmas[i, j] = new Label();
                    lbmas[i, j].FontSize = 16;
                    lbmas[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    lbmas[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    lbmas[i, j].Name = "_" + i + "_" + j;
                    lbmas[i, j].Content = "";
                    Thickness thk = new Thickness(2);
                    Brush brsh = Brushes.Black;
                    lbmas[i, j].BorderThickness = thk;
                    lbmas[i, j].BorderBrush = brsh;
                    tG.Children.Add(lbmas[i, j]);
                    Grid.SetRow(lbmas[i, j], i);
                    Grid.SetColumn(lbmas[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
                tG.Children.Add(day[i - 1]);
                Thickness thk = new Thickness(2);
                Brush brsh = Brushes.Black;
                day[i - 1].BorderThickness = thk;
                day[i - 1].BorderBrush = brsh;
                Grid.SetRowSpan(day[i - 1], m);
                Grid.SetRow(day[i - 1], (i * m) - (m - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from lessons_time order by lesson_number";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        int number = reader.GetInt32(1);
                        string s = "" + number + "\n" + reader.GetTimeSpan(2).ToString() + "\n" + reader.GetTimeSpan(3).ToString();
                        for (int i = number; i <= m * 7; i += m)
                        {
                            lbmas[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод групп
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie,grid from groups order by nazvanie";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        lbmas[0, j].Content = reader.GetString(0);
                        lbmas[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + dateMonday.Day + "-" + dateMonday.Month + "-" + dateMonday.Year + "' order by raspisanie.day,groups.nazvanie,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayWeek = reader1.GetInt32(0);
                        string grTitle = reader1.GetString(1);
                        int lesNum = reader1.GetInt32(2);
                        string subTitle = reader1.GetString(3);
                        string prepFio = reader1.GetString(4);
                        string cab= reader1.GetString(5);
                        int i = (((dayWeek * m) - m)) + lesNum;
                        int jj = 0;
                        for (int j = 2; j < n + 2; j++)
                        {
                            if (lbmas[0, j].Content.ToString() == grTitle) { jj = j; break; }

                        }
                        lbmas[i, jj].Content = "" + subTitle + "\n" + prepFio+"\n"+cab;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление грида курсов
        public static void updateDataGridСourses(string connectionString, string sql ,DataGrid grid)
        {
                DataTable table = new DataTable();
            table.Columns.Add("courseid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("subs", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            try { 
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string predmets = "";
                    object[] arr_sql= new object[4];
                    arr_sql[0] = reader.GetInt32(0);
                    arr_sql[1] = reader.GetString(1);
                    arr_sql[3] = reader.GetString(2);
                    try
                    {
                        NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                        con1.Open();
                        string sql1 = "SELECT title FROM subject where(select courses.subs from courses where courseid = "+ arr_sql[0] + " )  @> ARRAY[subid]";
                        NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                        NpgsqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                predmets += reader1.GetString(0)+", ";
                            }
                        }
                            con1.Close();
                    }
                    catch {MessageBox.Show("Не удалось подключиться к базе даных"); return; }
                        arr_sql[2] = predmets.Substring(0,predmets.Length-2);
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = arr_sql;
                    table.Rows.Add(row);
                }
                }
            grid.ItemsSource = table.DefaultView;
            con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе даных"); return; }

        }

        //обновление грида предметов
        public static void updateDataGridSubjects(string connectionString ,DataGrid grid)
        {
           
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from subject";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление грида пользователей
        public static void updateDataGridUsers(string connectionString, string sql ,DataGrid grid)
        {

            DataTable table = new DataTable();
            table.Columns.Add("uid", System.Type.GetType("System.Int32"));
            table.Columns.Add("fio", System.Type.GetType("System.String"));
            table.Columns.Add("log", System.Type.GetType("System.String"));
            table.Columns.Add("pas", System.Type.GetType("System.String"));
            table.Columns.Add("admin", System.Type.GetType("System.String"));
            table.Columns.Add("buhgalter", System.Type.GetType("System.String"));
            table.Columns.Add("director", System.Type.GetType("System.String"));
            try
            {
                object[] arr_sql = new object[7];

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
          
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        arr_sql[0] = reader.GetInt32(6);
                        arr_sql[1] = reader.GetString(0);
                        arr_sql[2] = reader.GetString(1);
                        arr_sql[3] = reader.GetString(2);
                        if (reader.GetInt32(3) == 1) arr_sql[4] = "Да"; else arr_sql[4] = "Нет";
                        if (reader.GetInt32(4) == 1) arr_sql[5] = "Да"; else arr_sql[5] = "Нет";
                        if (reader.GetInt32(5) == 1) arr_sql[6] = "Да"; else arr_sql[6] = "Нет";
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = arr_sql;
                        table.Rows.Add(row);
                    }


                }
                grid.ItemsSource = table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid групп
        public static void updateDataGridGroups(string connectionString, string sql ,DataGrid grid)
        {
            try
            {
                DataTable table = new DataTable();
                table.Columns.Add("grid", System.Type.GetType("System.Int32"));
            table.Columns.Add("gtitle", System.Type.GetType("System.String"));
            table.Columns.Add("ctitle", System.Type.GetType("System.String"));
                table.Columns.Add("payment", System.Type.GetType("System.Double"));
                table.Columns.Add("date_start", System.Type.GetType("System.DateTime"));
                table.Columns.Add("date_end", System.Type.GetType("System.DateTime"));
                table.Columns.Add("comment", System.Type.GetType("System.String"));
                object[] arr_sql = new object[7];
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        arr_sql[0] = reader.GetInt32(0);
                        arr_sql[1] = reader.GetString(1);
                        arr_sql[2] = reader.GetString(2);
                        arr_sql[6] = reader.GetString(3);
                    double sum = 0;
                    sum = reader.GetDouble(4);
                    sum += reader.GetDouble(5);
                    sum += reader.GetDouble(6);
                    sum += reader.GetDouble(7);
                    sum += reader.GetDouble(8);
                    sum += reader.GetDouble(9);
                    sum += reader.GetDouble(10);
                    sum += reader.GetDouble(11);
                    sum += reader.GetDouble(12);
                    sum += reader.GetDouble(13);
                    sum += reader.GetDouble(14);
                    sum += reader.GetDouble(15);
                    arr_sql[3] = sum;

                        arr_sql[4] = reader.GetDateTime(16);
                        arr_sql[5] = reader.GetDateTime(17);
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = arr_sql;
                        table.Rows.Add(row);
                    }
                
                }
                grid.ItemsSource = table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid звонков
        public static void updateDataGridZvonki(string connectionString, DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * FROM lessons_time order by lesson_number";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
        //обновление DataGrid преподавателей
        public static void updateDataGridPrep(string connectionString, string sql ,DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
        //обновление DataGrid сотрудников
        public static void updateDataGridSotr(string connectionString, string sql ,DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
             
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid категорий
        public static void updateDataGridKateg(string connectionString, DataGrid grid)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT * FROM kategorii";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                grid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
    }
}
