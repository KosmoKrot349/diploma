using Npgsql;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12
{
    class DataGridUpdater
    {

        //списки выплат обновление грида+
        public static void updatePaymentListGrid(string connectionString, DateTime payday,ArrayList employeesList,Grid grid)
        {
            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(50);
            RowDefinition rwd2 = new RowDefinition();
            rwd2.Height = new GridLength(50);
            grid.RowDefinitions.Add(rwd1);
            grid.RowDefinitions.Add(rwd2);

            Label monthLabelArr = new Label();
            monthLabelArr.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            monthLabelArr.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            monthLabelArr.BorderBrush = Brushes.Black;
            monthLabelArr.BorderThickness = new Thickness(2);
            switch (payday.Month)
            {
                case 1: { monthLabelArr.Content = "Январь " + payday.Year; break; }
                case 2: { monthLabelArr.Content = "Февраль " + payday.Year; break; }
                case 3: { monthLabelArr.Content = "Март " + payday.Year; break; }
                case 4: { monthLabelArr.Content = "Апрель " + payday.Year; break; }
                case 5: { monthLabelArr.Content = "Май " + payday.Year; break; }
                case 6: { monthLabelArr.Content = "Июнь " + payday.Year; break; }
                case 7: { monthLabelArr.Content = "Июль " + payday.Year; break; }
                case 8: { monthLabelArr.Content = "Август " + payday.Year; break; }
                case 9: { monthLabelArr.Content = "Сентябрь " + payday.Year; break; }
                case 10: { monthLabelArr.Content = "Октябрь " + payday.Year; break; }
                case 11: { monthLabelArr.Content = "Ноябрь " + payday.Year; break; }
                case 12: { monthLabelArr.Content = "Декабрь " + payday.Year; break; }
            }
            Grid.SetColumnSpan(monthLabelArr, 5);
            Grid.SetRow(monthLabelArr, 0);
            grid.Children.Add(monthLabelArr);

            Label teacherSalaryLabel = new Label();
            teacherSalaryLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            teacherSalaryLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            teacherSalaryLabel.Content = "ЗП преподавателя";
            teacherSalaryLabel.BorderBrush = Brushes.Black;
            teacherSalaryLabel.BorderThickness = new Thickness(2);

            Label staffSalaryLabel = new Label();
            staffSalaryLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            staffSalaryLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            staffSalaryLabel.Content = "ЗП штатная";
            staffSalaryLabel.BorderBrush = Brushes.Black;
            staffSalaryLabel.BorderThickness = new Thickness(2);


            Label allSalaryLabel = new Label();
            allSalaryLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            allSalaryLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            allSalaryLabel.Content = "ЗП вся";
            allSalaryLabel.BorderBrush = Brushes.Black;
            allSalaryLabel.BorderThickness = new Thickness(2);

            Label payOutSalaryLabel = new Label();
            payOutSalaryLabel.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            payOutSalaryLabel.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            payOutSalaryLabel.Content = "Выплачено";
            payOutSalaryLabel.BorderBrush = Brushes.Black;
            payOutSalaryLabel.BorderThickness = new Thickness(2);

            Grid.SetRow(teacherSalaryLabel, 1); Grid.SetRow(staffSalaryLabel, 1);  Grid.SetRow(allSalaryLabel, 1); Grid.SetRow(payOutSalaryLabel, 1);
            Grid.SetColumn(teacherSalaryLabel, 0); Grid.SetColumn(staffSalaryLabel, 1); Grid.SetColumn(allSalaryLabel, 2); Grid.SetColumn(payOutSalaryLabel, 3);
            grid.Children.Add(teacherSalaryLabel); grid.Children.Add(staffSalaryLabel);  grid.Children.Add(allSalaryLabel); grid.Children.Add(payOutSalaryLabel);

            double allTeacherSalary = 0, allStaffSalary = 0, allSalaryForServiceWork = 0, allPayoutSalary = 0;
            for (int i = 0; i < employeesList.Count; i++)
            {
                try
                {
                    NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                    con1.Open();
                    string sql1 = "SELECT prepzp, shtatzp, obslzp,viplacheno FROM nachisl where sotrid = "+employeesList[i]+" and extract(Month from payday)="+payday.Month+ " and extract(Year from payday)=" + payday.Year;
                    NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                    NpgsqlDataReader reader1 = com1.ExecuteReader();
                    if (reader1.HasRows)
                    {

                        while (reader1.Read())
                        {
                            RowDefinition rwd3 = new RowDefinition();
                            rwd3.Height = new GridLength(50);
                            grid.RowDefinitions.Add(rwd3);

                            Label teacherSalaryLabelForValue = new Label();
                            teacherSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            teacherSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            teacherSalaryLabelForValue.Content = reader1.GetDouble(0);
                            teacherSalaryLabelForValue.BorderBrush = Brushes.Black;
                            teacherSalaryLabelForValue.BorderThickness = new Thickness(2);

                            Label staffSalaryLabelForValue = new Label();
                            staffSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            staffSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            staffSalaryLabelForValue.Content = reader1.GetDouble(1)+reader1.GetDouble(2);
                            staffSalaryLabelForValue.BorderBrush = Brushes.Black;
                            staffSalaryLabelForValue.BorderThickness = new Thickness(2);

                           

                            Label allSalaryLabelForValue = new Label();

                            allSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            allSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                            allSalaryLabelForValue.Content = reader1.GetDouble(1)+ reader1.GetDouble(2)+ reader1.GetDouble(0); ;
                            allSalaryLabelForValue.BorderBrush = Brushes.Black;
                            allSalaryLabelForValue.BorderThickness = new Thickness(2);

                            Label payoutSalaryLabelForValue = new Label();

                            payoutSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                            payoutSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                            payoutSalaryLabelForValue.Content = reader1.GetDouble(3);
                            payoutSalaryLabelForValue.BorderBrush = Brushes.Black;
                            payoutSalaryLabelForValue.BorderThickness = new Thickness(2);

                            allTeacherSalary += reader1.GetDouble(0);
                            allStaffSalary += reader1.GetDouble(1)+ reader1.GetDouble(2);
                            allPayoutSalary += reader1.GetDouble(3);

                            Grid.SetRow(teacherSalaryLabelForValue, i+2); Grid.SetRow(staffSalaryLabelForValue, i + 2);  Grid.SetRow(allSalaryLabelForValue, i + 2); Grid.SetRow(payoutSalaryLabelForValue, i + 2);
                            Grid.SetColumn(teacherSalaryLabelForValue, 0); Grid.SetColumn(staffSalaryLabelForValue, 1);  Grid.SetColumn(allSalaryLabelForValue,2); Grid.SetColumn(payoutSalaryLabelForValue, 3);
                            grid.Children.Add(teacherSalaryLabelForValue); grid.Children.Add(staffSalaryLabelForValue);  grid.Children.Add(allSalaryLabelForValue); grid.Children.Add(payoutSalaryLabelForValue);
                       
                        }

                    }

                    if (reader1.HasRows==false)
                    {

                            RowDefinition rwd3 = new RowDefinition();
                            rwd3.Height = new GridLength(50);
                            grid.RowDefinitions.Add(rwd3);

                            Label teacherSalaryLabelForValue = new Label();

                        teacherSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        teacherSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        teacherSalaryLabelForValue.Content = "-";
                            teacherSalaryLabelForValue.BorderBrush = Brushes.Black;
                            teacherSalaryLabelForValue.BorderThickness = new Thickness(2);

                            Label staffSalaryLabelForValue = new Label();

                        staffSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        staffSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        staffSalaryLabelForValue.Content = "-";
                            staffSalaryLabelForValue.BorderBrush = Brushes.Black;
                            staffSalaryLabelForValue.BorderThickness = new Thickness(2);


                            Label allSalaryLabelForValue = new Label();


                        allSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        allSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        allSalaryLabelForValue.Content = "-";
                            allSalaryLabelForValue.BorderBrush = Brushes.Black;
                            allSalaryLabelForValue.BorderThickness = new Thickness(2);

                            Label payoutSalaryLabelForValue = new Label();

                        payoutSalaryLabelForValue.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                        payoutSalaryLabelForValue.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

                        payoutSalaryLabelForValue.Content = "-";
                            payoutSalaryLabelForValue.BorderBrush = Brushes.Black;
                            payoutSalaryLabelForValue.BorderThickness = new Thickness(2);

                            allTeacherSalary += 0;
                            allStaffSalary += 0;
                            allPayoutSalary += 0;

                            Grid.SetRow(teacherSalaryLabelForValue, i + 2); Grid.SetRow(staffSalaryLabelForValue, i + 2);  Grid.SetRow(allSalaryLabelForValue, i + 2); Grid.SetRow(payoutSalaryLabelForValue, i + 2);
                            Grid.SetColumn(teacherSalaryLabelForValue, 0); Grid.SetColumn(staffSalaryLabelForValue, 1);  Grid.SetColumn(allSalaryLabelForValue, 2); Grid.SetColumn(payoutSalaryLabelForValue, 3);
                            grid.Children.Add(teacherSalaryLabelForValue); grid.Children.Add(staffSalaryLabelForValue);  grid.Children.Add(allSalaryLabelForValue); grid.Children.Add(payoutSalaryLabelForValue);
                    }
                    con1.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных "); return; }
            }

            RowDefinition rwd4 = new RowDefinition();
            rwd4.Height = new GridLength(50);
            grid.RowDefinitions.Add(rwd4);

            Label teacherSalaryLabelForSum = new Label();

            teacherSalaryLabelForSum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            teacherSalaryLabelForSum.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;

            teacherSalaryLabelForSum.Content = Math.Round(allTeacherSalary,2);
            teacherSalaryLabelForSum.BorderBrush = Brushes.Black;
            teacherSalaryLabelForSum.BorderThickness = new Thickness(2);

            Label staffSalaryLabelForSum = new Label();

            staffSalaryLabelForSum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            staffSalaryLabelForSum.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            staffSalaryLabelForSum.Content = Math.Round(allStaffSalary, 2);
            staffSalaryLabelForSum.BorderBrush = Brushes.Black;
            staffSalaryLabelForSum.BorderThickness = new Thickness(2);
            Label allSalaryLabelForSum = new Label();

            allSalaryLabelForSum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            allSalaryLabelForSum.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            allSalaryLabelForSum.Content = Math.Round(allSalaryForServiceWork + allTeacherSalary + allStaffSalary, 2);
            allSalaryLabelForSum.BorderBrush = Brushes.Black;
            allSalaryLabelForSum.BorderThickness = new Thickness(2);

            Label payoutSalaryLabelForSum = new Label();

            payoutSalaryLabelForSum.VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
            payoutSalaryLabelForSum.HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
            payoutSalaryLabelForSum.Content = Math.Round(allPayoutSalary, 2);
            payoutSalaryLabelForSum.BorderBrush = Brushes.Black;
            payoutSalaryLabelForSum.BorderThickness = new Thickness(2);

            Grid.SetRow(teacherSalaryLabelForSum, grid.RowDefinitions.Count - 1); Grid.SetRow(staffSalaryLabelForSum, grid.RowDefinitions.Count - 1);  Grid.SetRow(allSalaryLabelForSum, grid.RowDefinitions.Count - 1); Grid.SetRow(payoutSalaryLabelForSum, grid.RowDefinitions.Count - 1);
            Grid.SetColumn(teacherSalaryLabelForSum, 0); Grid.SetColumn(staffSalaryLabelForSum, 1);  Grid.SetColumn(allSalaryLabelForSum, 2); Grid.SetColumn(payoutSalaryLabelForSum, 3);
            grid.Children.Add(teacherSalaryLabelForSum); grid.Children.Add(staffSalaryLabelForSum);  grid.Children.Add(allSalaryLabelForSum); grid.Children.Add(payoutSalaryLabelForSum);
        }

        //обновление грида отчтёа ститистики+
        public static void updateStatisticGrid(string connectionString, OxyPlot.Wpf.PlotView plot)
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
                    if (reader1.GetDateTime(1) == reader1.GetDateTime(0)) { MessageBox.Show("Невозможно построить график"); return; }
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
            ArrayList List = new ArrayList();
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
                        List.Add(reader.GetDouble(0));
                        List.Add(reader.GetDouble(1));
                        List.Add(reader.GetDouble(2));
                        List.Add(reader.GetDouble(3));
                    }

                }
                if (reader.HasRows == false) { MessageBox.Show("Невозможно построить график"); return;  }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            List.Sort();

            if (Convert.ToDouble(List[0]) == Convert.ToDouble(List[3])) { MessageBox.Show("Невозможно построить график"); return; }

            ax2.AbsoluteMinimum = Convert.ToDouble(List[0]);
            ax2.AbsoluteMaximum = Convert.ToDouble(List[3]);
            ax2.Zoom(Convert.ToDouble(List[0]), Convert.ToDouble(List[3]));

            model.Axes.Add(ax2);


            LineSeries LineSeriesForProfit = new LineSeries();
            LineSeriesForProfit.Color = OxyColor.FromRgb(0, 255, 0);
            LineSeries LineSeriesForCosts = new LineSeries();
            LineSeriesForCosts.Color = OxyColor.FromRgb(255, 0, 0);

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
                        LineSeriesForProfit.Points.Add(new DataPoint(DateTimeAxis.ToDouble(reader.GetDateTime(2)), reader.GetDouble(0)));
                        LineSeriesForCosts.Points.Add(new DataPoint(DateTimeAxis.ToDouble(reader.GetDateTime(2)), reader.GetDouble(1)));
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            model.Series.Add(LineSeriesForProfit);
            model.Series.Add(LineSeriesForCosts);
            model.Title = "Отчёт 'Статистика доходов\\расходов' на "+DateTime.Now.ToShortDateString();
            plot.Model = model;
        }

        //обновление грида кассы+
        public static void updateCashBoxGrid(string connectionString, Grid cashboxProfitGrid, Grid cashboxCostsGrid, Label cashboxTitleLabel, Label cashboxTotalProfit, Label cashboxTotalCosts, Label totalProfitLabel,string sqlProfit,string sqlCosts)
        {
            cashboxTitleLabel.Content = "Отчёт 'Касса' на " + DateTime.Now.ToShortDateString();
            double sumCosts = 0;
            double sumProfit = 0;
            cashboxProfitGrid.RowDefinitions.Clear();
            cashboxProfitGrid.Children.Clear();

            cashboxCostsGrid.RowDefinitions.Clear();
            cashboxCostsGrid.Children.Clear();

            RowDefinition rwd = new RowDefinition();
            rwd.Height = new GridLength(50);

            RowDefinition rwdd = new RowDefinition();
            rwdd.Height = new GridLength(50);

            cashboxProfitGrid.RowDefinitions.Add(rwd);
            Label LabelForProfitDate = new Label();
            Label LabelForProfitType = new Label();
            Label LabelForProfitPerson = new Label();
            Label LabelForProfitSum = new Label();
            LabelForProfitDate.Content = "Дата";
            LabelForProfitType.Content = "Тип";
            LabelForProfitPerson.Content = "Кто внес";
            LabelForProfitSum.Content = "Сумма";

            LabelForProfitDate.BorderThickness = new Thickness(2);
            LabelForProfitDate.BorderBrush = Brushes.Black;
            LabelForProfitType.BorderThickness = new Thickness(2);
            LabelForProfitType.BorderBrush = Brushes.Black;
            LabelForProfitPerson.BorderThickness = new Thickness(2);
            LabelForProfitPerson.BorderBrush = Brushes.Black;
            LabelForProfitSum.BorderThickness = new Thickness(2);
            LabelForProfitSum.BorderBrush = Brushes.Black;

            cashboxCostsGrid.RowDefinitions.Add(rwdd);
            Label LabelForCostsDate = new Label();
            Label LabelForCostsType = new Label();
            Label LabelForCostsPerson = new Label();
            Label LabelForCostsSum = new Label();


            LabelForCostsDate.Content = "Дата";
            LabelForCostsType.Content = "Тип";
            LabelForCostsPerson.Content = "Кому";
            LabelForCostsSum.Content = "Сумма";

            LabelForCostsDate.BorderThickness = new Thickness(2);
            LabelForCostsDate.BorderBrush = Brushes.Black;
            LabelForCostsType.BorderThickness = new Thickness(2);
            LabelForCostsType.BorderBrush = Brushes.Black;
            LabelForCostsPerson.BorderThickness = new Thickness(2);
            LabelForCostsPerson.BorderBrush = Brushes.Black;
            LabelForCostsSum.BorderThickness = new Thickness(2);
            LabelForCostsSum.BorderBrush = Brushes.Black;


            Grid.SetRow(LabelForProfitDate,0);
            Grid.SetRow(LabelForProfitType, 0);
            Grid.SetRow(LabelForProfitPerson, 0);
            Grid.SetRow(LabelForProfitSum, 0);
            Grid.SetRow(LabelForCostsDate, 0);
            Grid.SetRow(LabelForCostsType, 0);
            Grid.SetRow(LabelForCostsPerson, 0);
            Grid.SetRow(LabelForCostsSum, 0);

            Grid.SetColumn(LabelForProfitDate, 0);
            Grid.SetColumn(LabelForProfitType, 1);
            Grid.SetColumn(LabelForProfitPerson, 2);
            Grid.SetColumn(LabelForProfitSum, 3);
            Grid.SetColumn(LabelForCostsDate, 0);
            Grid.SetColumn(LabelForCostsType, 1);
            Grid.SetColumn(LabelForCostsPerson, 2);
            Grid.SetColumn(LabelForCostsSum, 3);

            cashboxProfitGrid.Children.Add(LabelForProfitDate);
            cashboxProfitGrid.Children.Add(LabelForProfitType);
            cashboxProfitGrid.Children.Add(LabelForProfitPerson);
            cashboxProfitGrid.Children.Add(LabelForProfitSum);

            cashboxCostsGrid.Children.Add(LabelForCostsDate);
            cashboxCostsGrid.Children.Add(LabelForCostsType);
            cashboxCostsGrid.Children.Add(LabelForCostsPerson);
            cashboxCostsGrid.Children.Add(LabelForCostsSum);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                
                NpgsqlCommand com = new NpgsqlCommand(sqlProfit, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        Label date = new Label();
                        Label type = new Label();
                        Label sum = new Label();
                        Label name = new Label();
                        date.Content = reader.GetDateTime(0).ToShortDateString();
                        type.Content = reader.GetString(1);
                        sum.Content = reader.GetDouble(2);
                        name.Content = reader.GetString(3);

                        sumProfit += reader.GetDouble(2);

                        date.BorderThickness = new Thickness(2);
                        date.BorderBrush = Brushes.Black;
                        type.BorderThickness = new Thickness(2);
                        type.BorderBrush = Brushes.Black;
                        sum.BorderThickness = new Thickness(2);
                        sum.BorderBrush = Brushes.Black;
                        name.BorderThickness = new Thickness(2);
                        name.BorderBrush = Brushes.Black;

                        RowDefinition rwd1= new RowDefinition();
                        rwd1.Height = new GridLength(50);
                        cashboxProfitGrid.RowDefinitions.Add(rwd1);

                        Grid.SetRow(date, i);
                        Grid.SetRow(type, i);
                        Grid.SetRow(sum, i);
                        Grid.SetRow(name, i);

                        Grid.SetColumn(date, 0);
                        Grid.SetColumn(type, 1);
                        Grid.SetColumn(name, 2);
                        Grid.SetColumn(sum, 3);
                        cashboxProfitGrid.Children.Add(date);
                        cashboxProfitGrid.Children.Add(type);
                        cashboxProfitGrid.Children.Add(sum);
                        cashboxProfitGrid.Children.Add(name);
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
               
                NpgsqlCommand com = new NpgsqlCommand(sqlCosts, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 1;
                    while (reader.Read())
                    {
                        Label date = new Label();
                        Label type = new Label();
                        Label name = new Label();
                        Label sum = new Label();
                        date.Content = reader.GetDateTime(0).ToShortDateString();
                        type.Content = reader.GetString(1);
                        name.Content = reader.GetString(2);
                        sum.Content = reader.GetDouble(3);

                        sumCosts += reader.GetDouble(3);

                        date.BorderThickness = new Thickness(2);
                        date.BorderBrush = Brushes.Black;
                        type.BorderThickness = new Thickness(2);
                        type.BorderBrush = Brushes.Black;
                        sum.BorderThickness = new Thickness(2);
                        sum.BorderBrush = Brushes.Black;
                        name.BorderThickness = new Thickness(2);
                        name.BorderBrush = Brushes.Black;

                        RowDefinition rwd1 = new RowDefinition();
                        rwd1.Height = new GridLength(50);
                        cashboxCostsGrid.RowDefinitions.Add(rwd1);

                        Grid.SetRow(date, i);
                        Grid.SetRow(type, i);
                        Grid.SetRow(sum, i);
                        Grid.SetRow(name, i);

                        Grid.SetColumn(date, 0);
                        Grid.SetColumn(type, 1);
                        Grid.SetColumn(name, 2);
                        Grid.SetColumn(sum, 3);
                        cashboxCostsGrid.Children.Add(date);
                        cashboxCostsGrid.Children.Add(type);
                        cashboxCostsGrid.Children.Add(sum);
                        cashboxCostsGrid.Children.Add(name);
                        i++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            cashboxTotalProfit.Content = "Итого: " + sumProfit;
            cashboxTotalCosts.Content = "Итого: " + sumCosts;
            totalProfitLabel.Content = "Общий доход: " + (sumProfit - sumCosts);
        }

        //обновление грида оплат(долг)+
        public static void updateDebtDataGrid(BookkeeperWindow window)
        {
            window.MonthOplGridDolg.ColumnDefinitions.Clear();
            window.MonthOplGridDolg.Children.Clear();
            //построение таблицы
            int quanMonth = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payformonth,'_'), array_to_string(payedlist,'_'), array_to_string(skidkiforpay,'_'), array_to_string(topay,'_'), array_to_string(penya,'_'), date_stop,year  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + window.ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + window.GroupsDolg.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payForMonth = reader.GetString(0);
                        string[] payForMonthArr = payForMonth.Split('_');

                        string payedByListener = reader.GetString(1);
                        string[] payedByListenerArr = payedByListener.Split('_');

                        string discount = reader.GetString(2);
                        string[] discountArr = discount.Split('_');

                        string toPay = reader.GetString(3);
                        string[] toPayArr = toPay.Split('_');

                        string fine = reader.GetString(4);
                        string[] fineArr = fine.Split('_');

                      

                        if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { window.isStopDolg.Content = "Обучение остановленно " + reader.GetDateTime(5).ToShortDateString(); }
                        if (reader.IsDBNull(reader.GetOrdinal("date_stop"))) { window.isStopDolg.Content = "Обучение не остановленно"; }
                        window.DataPerehoda.Content = "Дата добавления записи " + reader.GetDateTime(6).ToShortDateString(); ;

                        ArrayList Month = new ArrayList();

                        for (int i = 0; i < 12; i++)
                        {
                            if (payForMonthArr[i] != "0")
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
                                quanMonth++;

                            }
                        }


                        Label[] monthLabelArr = new Label[quanMonth];
                        Label[] payForMonthLabelArr = new Label[quanMonth];
                        Label[] payedByListenerLabelArr = new Label[quanMonth];
                        Label[] discountLabelArr = new Label[quanMonth];
                        Label[] toPayLabelArr = new Label[quanMonth];
                        Label[] fineLabelArr = new Label[quanMonth];
                        int j = 0;



                        for (int i = 0; i < quanMonth; i++)
                        {

                            monthLabelArr[i] = new Label();
                            payForMonthLabelArr[i] = new Label();
                            payedByListenerLabelArr[i] = new Label();
                            discountLabelArr[i] = new Label();
                            toPayLabelArr[i] = new Label();
                            fineLabelArr[i] = new Label();

                            window.textBoxArrForArrearsDefreyment[i].BorderThickness = new Thickness(2);
                            monthLabelArr[i].BorderThickness = new Thickness(2);
                            payForMonthLabelArr[i].BorderThickness = new Thickness(2);
                            payedByListenerLabelArr[i].BorderThickness = new Thickness(2);
                            discountLabelArr[i].BorderThickness = new Thickness(2);
                            toPayLabelArr[i].BorderThickness = new Thickness(2);
                            fineLabelArr[i].BorderThickness = new Thickness(2);

                            window.textBoxArrForArrearsDefreyment[i].BorderBrush = Brushes.Black;
                            monthLabelArr[i].BorderBrush = Brushes.Black;
                            payForMonthLabelArr[i].BorderBrush = Brushes.Black;
                            payedByListenerLabelArr[i].BorderBrush = Brushes.Black;
                            discountLabelArr[i].BorderBrush = Brushes.Black;
                            toPayLabelArr[i].BorderBrush = Brushes.Black;
                            fineLabelArr[i].BorderBrush = Brushes.Black;


                            while (payForMonthArr[j] == "0")
                            {
                                j++;
                            }

                            payForMonthLabelArr[i].Content = payForMonthArr[j];
                            payedByListenerLabelArr[i].Content = payedByListenerArr[j];
                            discountLabelArr[i].Content = discountArr[j];
                            toPayLabelArr[i].Content = toPayArr[j];
                            fineLabelArr[i].Content = fineArr[j];

                            monthLabelArr[i].Content = Month[i];

                            ColumnDefinition cmd = new ColumnDefinition();
                            cmd.Width = new GridLength(100);
                            window.MonthOplGridDolg.ColumnDefinitions.Add(cmd);

                            Grid.SetColumn(window.textBoxArrForArrearsDefreyment[i], (i));
                            Grid.SetColumn(monthLabelArr[i], (i));
                            Grid.SetColumn(payForMonthLabelArr[i], (i));
                            Grid.SetColumn(payedByListenerLabelArr[i], (i));
                            Grid.SetColumn(discountLabelArr[i], (i));
                            Grid.SetColumn(toPayLabelArr[i], (i));
                            Grid.SetColumn(fineLabelArr[i], (i));


                            Grid.SetRow(window.textBoxArrForArrearsDefreyment[i], 6);
                            Grid.SetRow(monthLabelArr[i], 0);
                            Grid.SetRow(payForMonthLabelArr[i], 1);
                            Grid.SetRow(payedByListenerLabelArr[i], 2);
                            Grid.SetRow(discountLabelArr[i], 3);
                            Grid.SetRow(toPayLabelArr[i], 5);
                            Grid.SetRow(fineLabelArr[i], 4);

                           window.MonthOplGridDolg.Children.Add(window.textBoxArrForArrearsDefreyment[i]);
                            window.MonthOplGridDolg.Children.Add(monthLabelArr[i]);
                            window.MonthOplGridDolg.Children.Add(payForMonthLabelArr[i]);
                            window.MonthOplGridDolg.Children.Add(payedByListenerLabelArr[i]);
                            window.MonthOplGridDolg.Children.Add(discountLabelArr[i]);
                            window.MonthOplGridDolg.Children.Add(toPayLabelArr[i]);
                            window.MonthOplGridDolg.Children.Add(fineLabelArr[i]);
                            j++;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление grid начислений зп+
        public static void updateAccrualsSalaryDataGrid(BookkeeperWindow window)
        {
           window.NachSotrGrid.Children.Clear();
            window.NachSotrGrid.RowDefinitions.Clear();
            window.NachMonthLabel.Content = "Начисления на ";
            switch (window.dateAccrual.Month)
            {
                case 1: { window.NachMonthLabel.Content += "январь " + window.dateAccrual.Year; break; }
                case 2: { window.NachMonthLabel.Content += "февраль " + window.dateAccrual.Year; break; }
                case 3: { window.NachMonthLabel.Content += "март " + window.dateAccrual.Year; break; }
                case 4: { window.NachMonthLabel.Content += "апрель " + window.dateAccrual.Year; break; }
                case 5: { window.NachMonthLabel.Content += "май " + window.dateAccrual.Year; break; }
                case 6: { window.NachMonthLabel.Content += "июнь " + window.dateAccrual.Year; break; }
                case 7: { window.NachMonthLabel.Content += "июль " + window.dateAccrual.Year; break; }
                case 8: { window.NachMonthLabel.Content += "август " + window.dateAccrual.Year; break; }
                case 9: { window.NachMonthLabel.Content += "сентябрь " + window.dateAccrual.Year; break; }
                case 10: { window.NachMonthLabel.Content += "октябрь " + window.dateAccrual.Year; break; }
                case 11: { window.NachMonthLabel.Content += "ноябрь " + window.dateAccrual.Year; break; }
                case 12: { window.NachMonthLabel.Content += "декабрь " + window.dateAccrual.Year; break; }

            }
          
            //заполнение грида сотрудников
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select fio,sotrid from sotrudniki where sotrid in (select sotrid from shtat) or sotrid in (select sotrid from prep)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.checkBoxArrStaffForAccrual[i] = new CheckBox();
                        window.checkBoxArrStaffForAccrual[i].Name = "id_" + reader.GetInt32(1);
                        window.checkBoxArrStaffForAccrual[i].Content = reader.GetString(0);
                        RowDefinition cmd = new RowDefinition();
                        cmd.Height = new GridLength(20);
                        window.NachSotrGrid.RowDefinitions.Add(cmd);
                        Grid.SetRow(window.checkBoxArrStaffForAccrual[i], i);
                        window.NachSotrGrid.Children.Add(window.checkBoxArrStaffForAccrual[i]);
                        i++;

                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //заполнение грида начислений
            try
            {
                DataTable table = new DataTable();
                object[] sqlArr = new object[10];
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
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select nachisl.nachid ,sotrudniki.fio,nachisl.prepzp,nachisl.shtatzp,nachisl.obslzp,nachisl.vs,nachisl.fss,nachisl.ndfl,nachisl.viplacheno from nachisl inner join sotrudniki using(sotrid) where EXTRACT(Year FROM nachisl.payday)=" + window.dateAccrual.Year+" and  EXTRACT(Month FROM nachisl.payday)=" + window.dateAccrual.Month;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sqlArr[0] = reader.GetInt32(0);
                        sqlArr[1] = reader.GetString(1);
                        sqlArr[2] = reader.GetDouble(2);
                        sqlArr[3] = reader.GetDouble(3) + reader.GetDouble(4);
                     

                        sqlArr[4] = reader.GetDouble(5);
                        sqlArr[5] = reader.GetDouble(6);
                        sqlArr[6] = reader.GetDouble(7);

                        sqlArr[7] = Math.Round(reader.GetDouble(2)+ reader.GetDouble(3)+ reader.GetDouble(4),2);
                        sqlArr[8] = reader.GetDouble(8);

                        
                        sqlArr[9] = Math.Round((reader.GetDouble(2) + reader.GetDouble(3) + reader.GetDouble(4))- reader.GetDouble(8),2);
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);
                    }


                }
                con.Close();
                window.NachDataGrid.ItemsSource = table.DefaultView;
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление grid штатного расписания+
        public static void updateStaffScheduleGrid(ManagerWindow window)
        {
            window.MonthGrid.Children.Clear();
            window.ShtatRaspSotrpGrid.Children.Clear();
            window.ShtatRaspSotrpGrid.RowDefinitions.Clear();
            window.ShtatRaspMonthYearLabel.Content = "Посещения на ";
            switch (window.date.Month)
            {
                case 1: { window.ShtatRaspMonthYearLabel.Content += "январь " + window.date.Year; break; }
                case 2: { window.ShtatRaspMonthYearLabel.Content += "февраль " + window.date.Year; break; }
                case 3: { window.ShtatRaspMonthYearLabel.Content += "март " + window.date.Year; break; }
                case 4: { window.ShtatRaspMonthYearLabel.Content += "апрель " + window.date.Year; break; }
                case 5: { window.ShtatRaspMonthYearLabel.Content += "май " + window.date.Year; break; }
                case 6: { window.ShtatRaspMonthYearLabel.Content += "июнь " + window.date.Year; break; }
                case 7: { window.ShtatRaspMonthYearLabel.Content += "июль " + window.date.Year; break; }
                case 8: { window.ShtatRaspMonthYearLabel.Content += "август " + window.date.Year; break; }
                case 9: { window.ShtatRaspMonthYearLabel.Content += "сентябрь " + window.date.Year; break; }
                case 10: { window.ShtatRaspMonthYearLabel.Content += "октябрь " + window.date.Year; break; }
                case 11: { window.ShtatRaspMonthYearLabel.Content += "ноябрь " + window.date.Year; break; }
                case 12: { window.ShtatRaspMonthYearLabel.Content += "декабрь " + window.date.Year; break; }

            }
            DateTime newDate = new DateTime(window.date.Year, window.date.Month, 1);
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Grid.SetColumn(window.labelArrForStaffSchedule[i, j],j);
                    Grid.SetRow(window.labelArrForStaffSchedule[i, j], i);
                    window.MonthGrid.Children.Add(window.labelArrForStaffSchedule[i, j]);

                    if (i==0)
                    {
                        switch (j)
                        {
                            case 0: { window.labelArrForStaffSchedule[i, j].Content = "ПН"; break; }
                            case 1: { window.labelArrForStaffSchedule[i, j].Content = "ВТ"; break; }
                            case 2: { window.labelArrForStaffSchedule[i, j].Content = "СР"; break; }
                            case 3: { window.labelArrForStaffSchedule[i, j].Content = "ЧТ"; break; }
                            case 4: { window.labelArrForStaffSchedule[i, j].Content = "ПТ"; break; }
                            case 5: { window.labelArrForStaffSchedule[i, j].Content = "СБ"; break; }
                            case 6: { window.labelArrForStaffSchedule[i, j].Content = "ВС"; break; }
                        }


                    }
                }
            
            }
            int index_j = 0;
            int index_i = 1;
            while (newDate.Month == window.date.Month)
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
                window.labelArrForStaffSchedule[index_i, index_j].Content = newDate.Day;
                if (index_j == 6) index_i++;
                newDate= newDate.AddDays(1);

            }
           

            try {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select fio,sotrid from shtat inner join sotrudniki using(sotrid)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.checkBoxArrForStaffSchedule[i] = new CheckBox();
                        window.checkBoxArrForStaffSchedule[i].Name = "id_"+reader.GetInt32(1);
                        window.checkBoxArrForStaffSchedule[i].Content = reader.GetString(0);
                        RowDefinition cmd = new RowDefinition();
                        cmd.Height = new GridLength(20);
                        window.ShtatRaspSotrpGrid.RowDefinitions.Add(cmd);
                        Grid.SetRow(window.checkBoxArrForStaffSchedule[i], i);
                        window.ShtatRaspSotrpGrid.Children.Add(window.checkBoxArrForStaffSchedule[i]);
                        i++;

                    }
                        
                            
                }
                con.Close();
            } 
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление DataGrid штата+
        public static void updateStaffDataGrid(ManagerWindow window)
        {

            try
            {
                DataTable table = new DataTable();
                object[] sqlArr = new object[4];
                table.Columns.Add("shtatid", System.Type.GetType("System.Int32"));
                table.Columns.Add("fio", System.Type.GetType("System.String"));
                table.Columns.Add("states", System.Type.GetType("System.String"));
                table.Columns.Add("obslwork", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();

                
                NpgsqlCommand com = new NpgsqlCommand(window.filter.sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        sqlArr[0] = reader.GetInt32(0);
                        sqlArr[1] = reader.GetString(1);
                        sqlArr[2] = "Нет";
                        sqlArr[3] = "Нет";
                        //вывод должностей
                        if (reader.GetString(2)!="")
                        {
                            sqlArr[2]="";
                            string[] rate = reader.GetString(2).Split('_');
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                                con1.Open();

                                string sql1 = "select title from states where ARRAY[statesid] <@ (select states from shtat where shtatid=" + sqlArr[0] + " ) order by statesid";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    int i = 0;
                                    while (reader1.Read())
                                    {
                                        sqlArr[2] += reader1.GetString(0) + " - " + rate[i] + "; ";
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
                            sqlArr[3] = "";
                            string[] workVolume = reader.GetString(3).Split('_');
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                                con1.Open();

                                string sql1 = "select title from raboty_obsl where ARRAY[rabotyid] <@ (select obslwork from shtat where shtatid=" + sqlArr[0] + " ) order by rabotyid";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    int i = 0;
                                    while (reader1.Read())
                                    {
                                        sqlArr[3] += reader1.GetString(0) + " - " + workVolume[i] + "; ";
                                        i++;
                                    }
                                }
                                con1.Close();
                    }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }

                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);
                    }

                }
                con.Close();
                window.ShtatDataGrid.ItemsSource = table.DefaultView;

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }


        //обновление DataGrid должностей+
        public static void updatePositionsDataGrid(ManagerWindow window)
        {

            try
            {
                DataTable table = new DataTable();
                object[] sqlArr = new object[5];
                table.Columns.Add("statesid", System.Type.GetType("System.Int32"));
                table.Columns.Add("title", System.Type.GetType("System.String"));
                table.Columns.Add("kol_work_day", System.Type.GetType("System.String"));
                table.Columns.Add("zp", System.Type.GetType("System.Double"));
                table.Columns.Add("comment", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT statesid, title, array_to_string(kol_work_day,'_'), zp, comment FROM states";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
    
                    while (reader.Read())
                    {
                        sqlArr[0] = reader.GetInt32(0);
                        sqlArr[1] = reader.GetString(1);
                        sqlArr[3] = reader.GetDouble(3);
                        sqlArr[4] = reader.GetString(4);
                        string[] daysArr = reader.GetString(2).Split('_');
                    string workDay = "Январь - " + daysArr[0] + " Февраль - " + daysArr[1] + " Март - " + daysArr[2] + " Апрель - " + daysArr[3] + " Май - " + daysArr[4] + "Июнь - " + daysArr[5] + " Июль - " + daysArr[6] + " Август - " + daysArr[7] + " Сентябрь - " + daysArr[8] + " Октябрь - " + daysArr[9] + " Ноябрь - " + daysArr[10] + " Декабрь - " + daysArr[11];
                        sqlArr[2] = workDay;
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);
                    }

                }
                con.Close();
                window.StateDataGrid.ItemsSource = table.DefaultView;

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
}


        //обновление DataGrid работ обслуживания+
        public static void updateServiceWorksDataGrid(ManagerWindow window)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT rabotyid, title, pay,ed_izm, comment FROM raboty_obsl";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.ObslWorkDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid коефициентов за выслугу лет+
        public static void updateWorkCoeffDataGrid(ManagerWindow window)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * from koef_vislugi";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.KoefDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid расходов+
        public static void updateCostsDataGrid(BookkeeperWindow window)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(window.filter.sql, con);
                Adapter.Fill(Table);
                window.RoshodyDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid доходов+
        public static void updateProfitDataGrid(BookkeeperWindow window)
        {

            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(window.filter.sql, con);
                Adapter.Fill(Table);
                window.DohodyDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление грида оплат+              
        public static void updatePaymentDataGrid(BookkeeperWindow window)
        {
            window.MonthOplGrid.ColumnDefinitions.Clear();
            window.MonthOplGrid.Children.Clear();
            //построение таблицы
            int quanMonth = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payformonth,'_'), array_to_string(payedlist,'_'), array_to_string(skidkiforpay,'_'), array_to_string(topay,'_'), array_to_string(penya,'_'), date_stop, isclose  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + window.Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + window.Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payForMonth = reader.GetString(0);
                        string[] payForMonthArr = payForMonth.Split('_');

                        string payedByListener = reader.GetString(1);
                        string[] payedByListenerArr = payedByListener.Split('_');

                        string discount = reader.GetString(2);
                        string[] discountArr = discount.Split('_');

                        string toPay = reader.GetString(3);
                        string[] toPayArr = toPay.Split('_');

                        string fine = reader.GetString(4);
                        string[] fineArr = fine.Split('_');

                        if (reader.GetInt32(6) == 1)
                        {
                            window.isClose.Content = "Запись об оплате закрыта"; window.Closeing.Visibility = Visibility.Collapsed; window.Open.Visibility = Visibility.Visible;
                            
                        }
                        else { window.isClose.Content = "Запись об оплате открыта"; window.Closeing.Visibility = Visibility.Visible; window.Open.Visibility = Visibility.Collapsed; }

                        if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { window.isStop.Content = "Обучение остановленно " + reader.GetDateTime(5).ToShortDateString(); window.RestartLern.Visibility = Visibility.Visible; window.StopLern.Visibility = Visibility.Collapsed; }
                        if (reader.IsDBNull(reader.GetOrdinal("date_stop"))) { window.isStop.Content = "Обучение не остановленно"; window.RestartLern.Visibility = Visibility.Collapsed; window.StopLern.Visibility = Visibility.Visible; }


                        ArrayList Month = new ArrayList();

                        for (int i = 0; i < 12; i++)
                        {
                            if (payForMonthArr[i] != "0")
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
                                quanMonth++;

                            }
                        }


                        Label[] monthLabelArr = new Label[quanMonth];
                        Label[] payForMonthLabelArr = new Label[quanMonth];
                        Label[] payedByListenerLabelAdd = new Label[quanMonth];
                        Label[] discountLabelArr = new Label[quanMonth];
                        Label[] toPayLabelArr = new Label[quanMonth];
                        Label[] fineLabelArr = new Label[quanMonth];
                        int j = 0;



                        for (int i = 0; i < quanMonth; i++)
                        {

                            monthLabelArr[i] = new Label();
                            payForMonthLabelArr[i] = new Label();
                            payedByListenerLabelAdd[i] = new Label();
                            discountLabelArr[i] = new Label();
                            toPayLabelArr[i] = new Label();
                            fineLabelArr[i] = new Label();

                            window.textBoxArrForDefreyment[i].BorderThickness = new Thickness(2);
                            monthLabelArr[i].BorderThickness = new Thickness(2);
                            payForMonthLabelArr[i].BorderThickness = new Thickness(2);
                            payedByListenerLabelAdd[i].BorderThickness = new Thickness(2);
                            discountLabelArr[i].BorderThickness = new Thickness(2);
                            toPayLabelArr[i].BorderThickness = new Thickness(2);
                            fineLabelArr[i].BorderThickness = new Thickness(2);

                            window.textBoxArrForDefreyment[i].BorderBrush = Brushes.Black;
                            monthLabelArr[i].BorderBrush = Brushes.Black;
                            payForMonthLabelArr[i].BorderBrush = Brushes.Black;
                            payedByListenerLabelAdd[i].BorderBrush = Brushes.Black;
                            discountLabelArr[i].BorderBrush = Brushes.Black;
                            toPayLabelArr[i].BorderBrush = Brushes.Black;
                            fineLabelArr[i].BorderBrush = Brushes.Black;
                           

                            while (payForMonthArr[j] == "0")
                            {
                                j++;
                            }

                            payForMonthLabelArr[i].Content = payForMonthArr[j];
                            payedByListenerLabelAdd[i].Content = payedByListenerArr[j];
                            discountLabelArr[i].Content = discountArr[j];
                            toPayLabelArr[i].Content = toPayArr[j];
                            fineLabelArr[i].Content = fineArr[j];

                            monthLabelArr[i].Content = Month[i];

                            ColumnDefinition cmd = new ColumnDefinition();
                            cmd.Width = new GridLength(100);
                            window.MonthOplGrid.ColumnDefinitions.Add(cmd);

                            Grid.SetColumn(window.textBoxArrForDefreyment[i], (i));
                            Grid.SetColumn(monthLabelArr[i], (i));
                            Grid.SetColumn(payForMonthLabelArr[i], (i));
                            Grid.SetColumn(payedByListenerLabelAdd[i], (i));
                            Grid.SetColumn(discountLabelArr[i], (i));
                            Grid.SetColumn(toPayLabelArr[i], (i));
                            Grid.SetColumn(fineLabelArr[i], (i));


                            Grid.SetRow(window.textBoxArrForDefreyment[i], 6);
                            Grid.SetRow(monthLabelArr[i], 0);
                            Grid.SetRow(payForMonthLabelArr[i], 1);
                            Grid.SetRow(payedByListenerLabelAdd[i], 2);
                            Grid.SetRow(discountLabelArr[i], 3);
                            Grid.SetRow(toPayLabelArr[i], 5);
                            Grid.SetRow(fineLabelArr[i], 4);

                            window.MonthOplGrid.Children.Add(window.textBoxArrForDefreyment[i]);
                            window.MonthOplGrid.Children.Add(monthLabelArr[i]);
                            window.MonthOplGrid.Children.Add(payForMonthLabelArr[i]);
                            window.MonthOplGrid.Children.Add(payedByListenerLabelAdd[i]);
                            window.MonthOplGrid.Children.Add(discountLabelArr[i]);
                            window.MonthOplGrid.Children.Add(toPayLabelArr[i]);
                            window.MonthOplGrid.Children.Add(fineLabelArr[i]);
                            j++;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
        }

        //обновление грида типов расходов+ 
        public static void updateTypeCostsDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * from typerash";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.TypeRashDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление грида типов доходов+
        public static void updateProfitTypeDataGri(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * from typedohod";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.TypeDohDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление грида кабинетов+
        public static void updateCabinetDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * from cabinet";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.cabDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }

        //обновление DataGrid слушателей+
        public static void updateListenerDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable table = new DataTable();
                object[] sqlArr = new object[5];
                table.Columns.Add("listenerid", System.Type.GetType("System.Int32"));
                table.Columns.Add("fio", System.Type.GetType("System.String"));
                table.Columns.Add("phones", System.Type.GetType("System.String"));
                table.Columns.Add("gr_lg", System.Type.GetType("System.String"));
                table.Columns.Add("comment", System.Type.GetType("System.String"));
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                
                NpgsqlCommand com = new NpgsqlCommand(window.filter.sql,con);
                NpgsqlDataReader reader = com.ExecuteReader();
            if (reader.HasRows)
            {
                int arrLeng = 0;
                string[] groopArr;

                while (reader.Read())
                {
                    sqlArr[0] = reader.GetInt32(0);
                    sqlArr[1] = reader.GetString(1);
                    sqlArr[2] = reader.GetString(2);
                    sqlArr[4] = reader.GetString(3);
                    if (!reader.IsDBNull(reader.GetOrdinal("grid")))
                    {
                        arrLeng = reader.GetInt32(4);
                        groopArr = new string[arrLeng];
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(window.connectionString);
                            con2.Open();
                            string sql2 = "select nazvanie from groups where ARRAY[grid] <@ (select grid from listeners where listenerid =" + sqlArr[0] + ") order by grid";
                            NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                int i = 0;
                                while (reader2.Read())
                                {
                                    groopArr[i] = reader2.GetString(0);
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
                        sql3 += " from listeners where listenerid =" + sqlArr[0];
                        try
                        {
                            NpgsqlConnection con3 = new NpgsqlConnection(window.connectionString);
                            con3.Open();
                            NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                            NpgsqlDataReader reader3 = com3.ExecuteReader();
                            string srtGr_Lg = "";
                            if (reader3.HasRows)
                            {
                                while (reader3.Read())
                                {
                                    for (int i = 0; i < arrLeng; i++) { srtGr_Lg += "Группа: " + groopArr[i] + " Процент " + reader3.GetDouble(i) + " "; }
                                }

                                sqlArr[3] = srtGr_Lg;
                            }
                            con3.Close();

                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        }
                        else sqlArr[3] = "нет";
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);


                   
                }
            }
                
                con.Close();
                window.listenerDataGrid.ItemsSource = table.DefaultView;

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
}

        //обновление грида расписания (по кабинетам)+
        public static void updateScheduleCabinetGrid(ManagerWindow window)
        {
            DateTime dateSunday = window.dateMonday.AddDays(6);
            Array.Clear(window.labelArr, 0, window.labelArr.Length);
            window.tGс.Children.Clear();
            window.tGс.RowDefinitions.Clear();
            window.tGс.ColumnDefinitions.Clear();
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
            DateTime dm = window.dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n" + dm.AddDays(i).ToShortDateString();
            }


            for (int i = 0; i < window.quanGroops + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                window.tGс.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                window.tGс.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    window.labelArr[i, j] = new Label();
                    window.labelArr[i, j].FontSize = 16;
                    window.labelArr[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    window.labelArr[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    window.labelArr[i, j].Name = "_" + i + "_" + j;
                    window.labelArr[i, j].Content = "";
                    window.labelArr[i, j].BorderThickness = new Thickness(2);
                    window.labelArr[i, j].BorderBrush = Brushes.Black;
                    window.tGс.Children.Add(window.labelArr[i, j]);
                    Grid.SetRow(window.labelArr[i, j], i);
                    Grid.SetColumn(window.labelArr[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
                window.tGс.Children.Add(day[i - 1]);
                day[i - 1].BorderThickness = new Thickness(2);
                day[i - 1].BorderBrush = Brushes.Black;
                Grid.SetRowSpan(day[i - 1], window.quanLessonsInDay);
                Grid.SetRow(day[i - 1], (i * window.quanLessonsInDay) - (window.quanLessonsInDay - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
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
                        for (int i = number; i <= window.quanLessonsInDay * 7; i += window.quanLessonsInDay)
                        {
                            window.labelArr[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод кабинетов
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select num,cabid from cabinet order by num";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        window.labelArr[0, j].Content = reader.GetString(0);
                        window.labelArr[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + window.dateMonday.Day + "-" + window.dateMonday.Month + "-" + window.dateMonday.Year + "' order by raspisanie.day,cabinet.num,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayOfWeek = reader1.GetInt32(0);
                        string groopTitle = reader1.GetString(1);
                        int lessonNumber = reader1.GetInt32(2);
                        string subjectTitle = reader1.GetString(3);
                        string teacherName = reader1.GetString(4);
                        string cabinet = reader1.GetString(5);
                        int i = (((dayOfWeek * window.quanLessonsInDay) - window.quanLessonsInDay)) + lessonNumber;
                        int jj = 0;
                        for (int j = 2; j < window.quanGroops + 2; j++)
                        {
                            if (window.labelArr[0, j].Content.ToString() == cabinet) { jj = j; break; }

                        }
                        window.labelArr[i, jj].Content = "" + subjectTitle + "\n" + teacherName + "\n" + groopTitle;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление грида расписания (по преподавателям)+
        public static void updateTeacherScheduleGrid(ManagerWindow window)
        {
           DateTime dateSunday = window.dateMonday.AddDays(6);
            Array.Clear(window.labelArr, 0, window.labelArr.Length);
            window.tGp.Children.Clear();
            window.tGp.RowDefinitions.Clear();
            window.tGp.ColumnDefinitions.Clear();
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
            DateTime dm = window.dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n" + dm.AddDays(i).ToShortDateString();
            }

            for (int i = 0; i < window.quanGroops + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                window.tGp.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                window.tGp.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    window.labelArr[i, j] = new Label();
                    window.labelArr[i, j].FontSize = 16;
                    window.labelArr[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    window.labelArr[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    window.labelArr[i, j].Name = "_" + i + "_" + j;
                    window.labelArr[i, j].Content = "";
                    window.labelArr[i, j].BorderThickness = new Thickness(2);
                    window.labelArr[i, j].BorderBrush = Brushes.Black;
                    window.tGp.Children.Add(window.labelArr[i, j]);
                    Grid.SetRow(window.labelArr[i, j], i);
                    Grid.SetColumn(window.labelArr[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
              window.tGp.Children.Add(day[i - 1]);
                day[i - 1].BorderThickness = new Thickness(2);
                day[i - 1].BorderBrush = Brushes.Black;
                Grid.SetRowSpan(day[i - 1], window.quanLessonsInDay);
                Grid.SetRow(day[i - 1], (i * window.quanLessonsInDay) - (window.quanLessonsInDay - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
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
                        for (int i = number; i <= window.quanLessonsInDay * 7; i += window.quanLessonsInDay)
                        {
                            window.labelArr[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод преподавателей
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select sotrudniki.fio, prep.prepid from prep inner join sotrudniki using(sotrid) order by fio";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        window.labelArr[0, j].Content = reader.GetString(0);
                        window.labelArr[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + window.dateMonday.Day + "-" + window.dateMonday.Month + "-" + window.dateMonday.Year + "' order by raspisanie.day,sotrudniki.fio,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayOfWeek = reader1.GetInt32(0);
                        string groopTitle = reader1.GetString(1);
                        int lessonNumber = reader1.GetInt32(2);
                        string subjectTitle = reader1.GetString(3);
                        string teacherName = reader1.GetString(4);
                        string cabinet = reader1.GetString(5);
                        int i = (((dayOfWeek * window.quanLessonsInDay) - window.quanLessonsInDay)) + lessonNumber;
                        int jj = 0;
                        for (int j = 2; j < window.quanGroops + 2; j++)
                        {
                            if (window.labelArr[0, j].Content.ToString() == teacherName) { jj = j; break; }

                        }
                        window.labelArr[i, jj].Content = "" + subjectTitle + "\n" + groopTitle+"\n"+cabinet;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //обновление грида расписания (по группам)+
        public static void updateGroopScheduleGrid(ManagerWindow window)
        {
            DateTime dateSunday = window.dateMonday.AddDays(6);
            Array.Clear(window.labelArr, 0, window.labelArr.Length);
            window.tG.Children.Clear();
            window.tG.RowDefinitions.Clear();
            window.tG.ColumnDefinitions.Clear();
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
            DateTime dm = window.dateMonday;
            for (int i = 0; i < 7; i++)
            {
                day[i].Content += "\n"+dm.AddDays(i).ToShortDateString();
            }

            for (int i = 0; i < window.quanGroops + 2; i++)
            {
                ColumnDefinition cmd1 = new ColumnDefinition();
                cmd1.Width = new GridLength(100);
                window.tG.ColumnDefinitions.Add(cmd1);
            }

            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                RowDefinition cmd1 = new RowDefinition();
                cmd1.Height = new GridLength(100);
                window.tG.RowDefinitions.Add(cmd1);
            }
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i == 0 && j == 1)
                    {
                        continue;
                    }

                    window.labelArr[i, j] = new Label();
                    window.labelArr[i, j].FontSize = 16;
                    window.labelArr[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                   window.labelArr[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    window.labelArr[i, j].Name = "_" + i + "_" + j;
                    window.labelArr[i, j].Content = "";
                    window.labelArr[i, j].BorderThickness = new Thickness(2);
                    window.labelArr[i, j].BorderBrush = Brushes.Black;
                    window.tG.Children.Add(window.labelArr[i, j]);
                    Grid.SetRow(window.labelArr[i, j], i);
                    Grid.SetColumn(window.labelArr[i, j], j);


                }

            }
            for (int i = 1; i <= 7; i++)
            {
                window.tG.Children.Add(day[i - 1]);
                day[i - 1].BorderThickness = new Thickness(2);
                day[i - 1].BorderBrush = Brushes.Black;
                Grid.SetRowSpan(day[i - 1], window.quanLessonsInDay);
                Grid.SetRow(day[i - 1], (i * window.quanLessonsInDay) - (window.quanLessonsInDay - 1));
                Grid.SetColumn(day[i - 1], 0);

            }
            //вывод времени занятий
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
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
                        for (int i = number; i <= window.quanLessonsInDay * 7; i += window.quanLessonsInDay)
                        {
                            window.labelArr[i, 1].Content = s;
                        }
                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод групп
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select nazvanie,grid from groups order by nazvanie";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int j = 2;
                    while (reader.Read())
                    {

                        window.labelArr[0, j].Content = reader.GetString(0);
                        window.labelArr[0, j].Name = "name_" + reader.GetString(1);
                        j++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод предметов
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                con1.Open();
                string sql1 = "SELECT raspisanie.day,groups.nazvanie,lessons_time.lesson_number,subject.title,sotrudniki.fio,cabinet.num FROM raspisanie inner join groups using (grid) inner join lessons_time using (lesson_number) inner join subject using (subid) inner join prep using (prepid) inner join sotrudniki using(sotrid) inner join cabinet using(cabid) where date <= '" + dateSunday.Day + "-" + dateSunday.Month + "-" + dateSunday.Year + "' and date >= '" + window.dateMonday.Day + "-" + window.dateMonday.Month + "-" + window.dateMonday.Year + "' order by raspisanie.day,groups.nazvanie,lessons_time.lesson_number";


                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {

                    while (reader1.Read())
                    {
                        int dayOfWeek = reader1.GetInt32(0);
                        string groopTitle = reader1.GetString(1);
                        int lessonNumber = reader1.GetInt32(2);
                        string subjectTitle = reader1.GetString(3);
                        string teacherName = reader1.GetString(4);
                        string cabinet= reader1.GetString(5);
                        int i = (((dayOfWeek * window.quanLessonsInDay) - window.quanLessonsInDay)) + lessonNumber;
                        int jj = 0;
                        for (int j = 2; j < window.quanGroops + 2; j++)
                        {
                            if (window.labelArr[0, j].Content.ToString() == groopTitle) { jj = j; break; }

                        }
                        window.labelArr[i, jj].Content = "" + subjectTitle + "\n" + teacherName+"\n"+cabinet;
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }

        //обновление грида курсов+
        public static void updateСoursesDataGrid(ManagerWindow window)
        {
                DataTable table = new DataTable();
            table.Columns.Add("courseid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("subs", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            try { 
            NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                NpgsqlCommand command = new NpgsqlCommand(window.filter.sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string subjects = "";
                    object[] sqlArr= new object[4];
                    sqlArr[0] = reader.GetInt32(0);
                    sqlArr[1] = reader.GetString(1);
                    sqlArr[3] = reader.GetString(2);
                    try
                    {
                        NpgsqlConnection con1 = new NpgsqlConnection(window.connectionString);
                        con1.Open();
                        string sql1 = "SELECT title FROM subject where(select courses.subs from courses where courseid = "+ sqlArr[0] + " )  @> ARRAY[subid]";
                        NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                        NpgsqlDataReader reader1 = command1.ExecuteReader();
                        if (reader1.HasRows)
                        {
                            while (reader1.Read())
                            {
                                subjects += reader1.GetString(0)+", ";
                            }
                        }
                            con1.Close();
                    }
                    catch {MessageBox.Show("Не удалось подключиться к базе даных"); return; }
                        sqlArr[2] = subjects.Substring(0,subjects.Length-2);
                    DataRow row;
                    row = table.NewRow();
                    row.ItemArray = sqlArr;
                    table.Rows.Add(row);
                }
                }
                window.coursDataGrid.ItemsSource = table.DefaultView;
            con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе даных"); return; }

        }

        //обновление грида предметов+
        public static void updateSubjectDataGrid(ManagerWindow window)
        {
           
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select * from subject";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.subsDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление грида пользователей+
        public static void updateUsersDataGrid(AdminWindow window)
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
                object[] sqlArr = new object[7];

                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
          
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(window.filter.sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        sqlArr[0] = reader.GetInt32(6);
                        sqlArr[1] = reader.GetString(0);
                        sqlArr[2] = reader.GetString(1);
                        sqlArr[3] = reader.GetString(2);
                        if (reader.GetInt32(3) == 1) sqlArr[4] = "Да"; else sqlArr[4] = "Нет";
                        if (reader.GetInt32(4) == 1) sqlArr[5] = "Да"; else sqlArr[5] = "Нет";
                        if (reader.GetInt32(5) == 1) sqlArr[6] = "Да"; else sqlArr[6] = "Нет";
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);
                    }


                }
                window.usersDGrid.ItemsSource = table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid групп+
        public static void updateGroopsDataGrid(ManagerWindow window)
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
                object[] sqlArr = new object[7];
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(window.filter.sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sqlArr[0] = reader.GetInt32(0);
                        sqlArr[1] = reader.GetString(1);
                        sqlArr[2] = reader.GetString(2);
                        sqlArr[6] = reader.GetString(3);
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
                    sqlArr[3] = sum;

                        sqlArr[4] = reader.GetDateTime(16);
                        sqlArr[5] = reader.GetDateTime(17);
                        DataRow row;
                        row = table.NewRow();
                        row.ItemArray = sqlArr;
                        table.Rows.Add(row);
                    }
                
                }
                window.groupsDataGrid.ItemsSource = table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid времени занятий+
        public static void updateTimeScheduleDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * FROM lessons_time order by lesson_number";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.zvonkiDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
        //обновление DataGrid преподавателей+
        public static void updateTeachersDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(window.filter.sql, con);
                Adapter.Fill(Table);
                window.prepDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
        //обновление DataGrid сотрудников+
        public static void updateEmploeesDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
             
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(window.filter.sql, con);
                Adapter.Fill(Table);
                window.allSotrDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }

        //обновление DataGrid категорий+
        public static void updateCategoriesDataGrid(ManagerWindow window)
        {
            try
            {
                DataTable Table = new DataTable();
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT * FROM kategorii";
                NpgsqlDataAdapter Adapter = new NpgsqlDataAdapter(sql, con);
                Adapter.Fill(Table);
                window.kategDataGrid.ItemsSource = Table.DefaultView;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
        }
    }
}
