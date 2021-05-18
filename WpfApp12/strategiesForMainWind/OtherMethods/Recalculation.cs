using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForMainWind.OtherMethods
{
    class Recalculation
    {
        public static void Recalc(MainWindow window)
        {
            DateTime DateNow = DateTime.Now;
            DateTime DateOfLastRecalc = new DateTime();
            double finePrecent = 0;
            //для обычной таблицы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select * from last_pereraschet";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateOfLastRecalc = reader.GetDateTime(0);
                        finePrecent = reader.GetDouble(1);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }



            if (DateOfLastRecalc.Month != DateNow.Month)
            {
                MessageBoxResult res = MessageBox.Show("Есть возможность переасчёта пени. Выполнить?", "Предупреждение", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                {
                    //для обычной таблицы
                    try
                    {

                        NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                        con.Open();
                        string sql = "select listnuchid,listenerid,grid,array_to_string(payformonth,'_'),array_to_string(payedlist,'_'),array_to_string(skidkiforpay,'_'),array_to_string(penya ,'_'),date_stop from listnuch where isclose=0";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int listenerAccrualsID = reader.GetInt32(0);
                                int groopID = reader.GetInt32(2);
                                int listenerID = reader.GetInt32(1);

                                string PayForMonthString = reader.GetString(3);
                                string[] PayForMonthStringArr = PayForMonthString.Split('_');
                                double[] PayForMonthDoubleArr = new double[12];

                                string AlredyPayment = reader.GetString(4);
                                string[] AlredyPaymentStringArr = AlredyPayment.Split('_');
                                double[] AlredyPaymentDoubleArr = new double[12];

                                string Discounts = reader.GetString(5);
                                string[] DiscountsStringArr = Discounts.Split('_');
                                double[] DiscountsDoubleArr = new double[12];

                                string Fine = reader.GetString(6);
                                string[] FineStringArr = Fine.Split('_');
                                double[] FineDoubleArr = new double[12];

                                double[] NeedToPay = new double[12];

                                for (int i = 0; i < 12; i++)
                                {
                                    PayForMonthDoubleArr[i] = Convert.ToDouble(PayForMonthStringArr[i].ToString().Replace('.', ','));
                                    AlredyPaymentDoubleArr[i] = Convert.ToDouble(AlredyPaymentStringArr[i].ToString().Replace('.', ','));
                                    DiscountsDoubleArr[i] = Convert.ToDouble(DiscountsStringArr[i].ToString().Replace('.', ','));
                                    FineDoubleArr[i] = Convert.ToDouble(FineStringArr[i].ToString().Replace('.', ','));
                                }
                                DateTime MonthToRecalc = DateTime.Now;
                                if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { MonthToRecalc = reader.GetDateTime(7); }

                                DateTime DatehStartLernForGroop = new DateTime();
                                DateTime DatehEndLernForGroop = new DateTime();

                                //получение месяцев начала и конаца обучения группы
                                try
                                {
                                    NpgsqlConnection con2 = new NpgsqlConnection(window.connectionString);
                                    con2.Open();
                                    string sql2 = "select date_start,date_end from groups where grid = " + groopID;
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                    NpgsqlDataReader reader2 = com2.ExecuteReader();
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            DatehStartLernForGroop = reader2.GetDateTime(0);
                                            DatehEndLernForGroop = reader2.GetDateTime(1);

                                        }
                                    }
                                    con2.Close();
                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                                //начало перерасчёта пени
                                bool calcStart = false;
                                if (DatehStartLernForGroop.Month > DatehEndLernForGroop.Month)
                                {
                                    if (DatehStartLernForGroop.Month < MonthToRecalc.Month && DatehStartLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);

                                        }
                                        calcStart = true;
                                    }


                                    if (DatehEndLernForGroop.Month >= MonthToRecalc.Month && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < 12; i++)
                                        {

                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        for (int i = 0; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }

                                    if (DatehEndLernForGroop.Month < MonthToRecalc.Month && DatehStartLernForGroop.Month > MonthToRecalc.Month && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < 12; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        for (int i = 0; i <= DatehEndLernForGroop.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }
                                }
                                if (DatehStartLernForGroop.Month < DatehEndLernForGroop.Month && DatehStartLernForGroop.Year <= MonthToRecalc.Year && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                {
                                    for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                    {
                                        FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                    }
                                    calcStart = true;
                                }
                                if (DatehStartLernForGroop.Month == DatehEndLernForGroop.Month)
                                {
                                    if (DatehStartLernForGroop.Year == DatehEndLernForGroop.Year && DatehStartLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }
                                }
                                for (int i = 0; i < 12; i++)
                                {
                                    NeedToPay[i] = Math.Round(PayForMonthDoubleArr[i] + FineDoubleArr[i] - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100)), 2);
                                }

                                PayForMonthString = "'{";

                                AlredyPayment = "'{";

                                Discounts = "'{";

                                Fine = "'{";

                                string NeedToPayString = "'{";

                                for (int i = 0; i < 12; i++)
                                {

                                    PayForMonthString += PayForMonthDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    AlredyPayment += AlredyPaymentDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    Discounts += DiscountsDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    Fine += FineDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    NeedToPayString += NeedToPay[i].ToString().Replace(',', '.') + ",";

                                }
                                PayForMonthString = PayForMonthString.Substring(0, PayForMonthString.Length - 1) + "}'";
                                AlredyPayment = AlredyPayment.Substring(0, AlredyPayment.Length - 1) + "}'";
                                Discounts = Discounts.Substring(0, Discounts.Length - 1) + "}'";
                                Fine = Fine.Substring(0, Fine.Length - 1) + "}'";
                                NeedToPayString = NeedToPayString.Substring(0, NeedToPayString.Length - 1) + "}'";
                                try
                                {
                                    NpgsqlConnection con3 = new NpgsqlConnection(window.connectionString);
                                    con3.Open();
                                    string sql3 = "UPDATE listnuch SET payformonth=" + PayForMonthString + ", payedlist=" + AlredyPayment + ", skidkiforpay=" + Discounts + ",topay=" + NeedToPayString + ", penya=" + Fine + " WHERE listnuchid=" + listenerAccrualsID;
                                    NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                    com3.ExecuteNonQuery();
                                    con3.Close();
                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                            }
                        }


                        con.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }


                    //для таблицы долгов
                    try
                    {

                        NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                        con.Open();
                        string sql = "select listdolgid,listenerid,grid,array_to_string(payformonth,'_'),array_to_string(payedlist,'_'),array_to_string(skidkiforpay,'_'),array_to_string(penya ,'_'),date_stop,date_start,date_end from listdolg where isclose=0";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int listenerAccrualsID = reader.GetInt32(0);
                                int groopID = reader.GetInt32(2);
                                int listenerID = reader.GetInt32(1);

                                string PayForMonthString = reader.GetString(3);
                                string[] PayForMonthStringArr = PayForMonthString.Split('_');
                                double[] PayForMonthDoubleArr = new double[12];

                                string AlredyPayment = reader.GetString(4);
                                string[] AlredyPaymentStringArr = AlredyPayment.Split('_');
                                double[] AlredyPaymentDoubleArr = new double[12];

                                string Discounts = reader.GetString(5);
                                string[] DiscountsStringArr = Discounts.Split('_');
                                double[] DiscountsDoubleArr = new double[12];

                                string Fine = reader.GetString(6);
                                string[] FineStringArr = Fine.Split('_');
                                double[] FineDoubleArr = new double[12];

                                double[] NeedToPay = new double[12];

                                for (int i = 0; i < 12; i++)
                                {
                                    PayForMonthDoubleArr[i] = Convert.ToDouble(PayForMonthStringArr[i].ToString().Replace('.', ','));
                                    AlredyPaymentDoubleArr[i] = Convert.ToDouble(AlredyPaymentStringArr[i].ToString().Replace('.', ','));
                                    DiscountsDoubleArr[i] = Convert.ToDouble(DiscountsStringArr[i].ToString().Replace('.', ','));
                                    FineDoubleArr[i] = Convert.ToDouble(FineStringArr[i].ToString().Replace('.', ','));
                                }
                                DateTime MonthToRecalc = DateTime.Now;
                                if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { MonthToRecalc = reader.GetDateTime(7); }

                                DateTime DatehStartLernForGroop = reader.GetDateTime(8);
                                DateTime DatehEndLernForGroop = reader.GetDateTime(9);

                                //начало перерасчёта пени
                                bool calcStart = false;
                                if (DatehStartLernForGroop.Month > DatehEndLernForGroop.Month)
                                {
                                    if (DatehStartLernForGroop.Month < MonthToRecalc.Month && DatehStartLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);

                                        }
                                        calcStart = true;
                                    }


                                    if (DatehEndLernForGroop.Month >= MonthToRecalc.Month && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < 12; i++)
                                        {

                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        for (int i = 0; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }

                                    if (DatehEndLernForGroop.Month < MonthToRecalc.Month && DatehStartLernForGroop.Month > MonthToRecalc.Month && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < 12; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        for (int i = 0; i <= DatehEndLernForGroop.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }
                                }

                                if (DatehStartLernForGroop.Month < DatehEndLernForGroop.Month && DatehStartLernForGroop.Year <= MonthToRecalc.Year && DatehEndLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                {
                                    for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                    {
                                        FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                    }
                                    calcStart = true;
                                }


                                if (DatehStartLernForGroop.Month == DatehEndLernForGroop.Month)
                                {
                                    if (DatehStartLernForGroop.Year == DatehEndLernForGroop.Year && DatehStartLernForGroop.Year <= MonthToRecalc.Year && calcStart == false)
                                    {
                                        for (int i = DatehStartLernForGroop.Month - 1; i < MonthToRecalc.Month - 1; i++)
                                        {
                                            FineDoubleArr[i] = Math.Round(FineDoubleArr[i] + ((PayForMonthDoubleArr[i] + FineDoubleArr[i]) - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100))) * finePrecent / 100, 2);
                                        }
                                        calcStart = true;
                                    }
                                }



                                for (int i = 0; i < 12; i++)
                                {
                                    NeedToPay[i] = Math.Round(PayForMonthDoubleArr[i] + FineDoubleArr[i] - (AlredyPaymentDoubleArr[i] + (PayForMonthDoubleArr[i] * DiscountsDoubleArr[i] / 100)), 2);
                                }

                                PayForMonthString = "'{";

                                AlredyPayment = "'{";

                                Discounts = "'{";

                                Fine = "'{";

                                string NeedToPayString = "'{";

                                for (int i = 0; i < 12; i++)
                                {

                                    PayForMonthString += PayForMonthDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    AlredyPayment += AlredyPaymentDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    Discounts += DiscountsDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    Fine += FineDoubleArr[i].ToString().Replace(',', '.') + ",";
                                    NeedToPayString += NeedToPay[i].ToString().Replace(',', '.') + ",";

                                }
                                PayForMonthString = PayForMonthString.Substring(0, PayForMonthString.Length - 1) + "}'";
                                AlredyPayment = AlredyPayment.Substring(0, AlredyPayment.Length - 1) + "}'";
                                Discounts = Discounts.Substring(0, Discounts.Length - 1) + "}'";
                                Fine = Fine.Substring(0, Fine.Length - 1) + "}'";
                                NeedToPayString = NeedToPayString.Substring(0, NeedToPayString.Length - 1) + "}'";
                                try
                                {
                                    NpgsqlConnection con3 = new NpgsqlConnection(window.connectionString);
                                    con3.Open();
                                    string sql3 = "UPDATE listdolg SET payformonth=" + PayForMonthString + ", payedlist=" + AlredyPayment + ", skidkiforpay=" + Discounts + ",topay=" + NeedToPayString + ", penya=" + Fine + " WHERE listdolgid=" + listenerAccrualsID;
                                    NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                    com3.ExecuteNonQuery();
                                    con3.Close();
                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                            }
                        }


                        con.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                    try
                    {
                        NpgsqlConnection con3 = new NpgsqlConnection(window.connectionString);
                        con3.Open();
                        string sql3 = "UPDATE last_pereraschet SET last_date=now()";
                        NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                        com3.ExecuteNonQuery();
                        con3.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                }
            }

        }
    }
}
