using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.strategiesForBookkeeper.OtherMethods;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class AddPaymentForArrears:IButtonClick
    {
        BookkeeperWindow windowObj;

        public AddPaymentForArrears(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payedlist,'_'),array_to_string(topay,'_'),array_to_string(payformonth,'_'),array_to_string(skidkiforpay,'_')  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + windowObj.ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + windowObj.GroupsDolg.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payedByListener = reader.GetString(0);
                        string[] payedByListenerStringArr = payedByListener.Split('_');
                        double[] payedByListenerDoubleArr = new double[12];

                        string toPay = reader.GetString(1);
                        string[] toPayStringArr = toPay.Split('_');
                        double[] toPayDoubleArr = new double[12];

                        string payForMonth = reader.GetString(2);
                        string[] payForMonthStringArr = payForMonth.Split('_');
                        double[] payForMonthDobuleArr = new double[12];

                        string dicount = reader.GetString(3);
                        string[] dicountStringArr = dicount.Split('_');
                        double[] dicountDoubleArr = new double[12];
                        for (int i = 0; i < 12; i++)
                        {

                            payedByListenerDoubleArr[i] = Convert.ToDouble(payedByListenerStringArr[i].Replace('.', ','));
                            toPayDoubleArr[i] = Convert.ToDouble(toPayStringArr[i].Replace('.', ','));
                            payForMonthDobuleArr[i] = Convert.ToDouble(payForMonthStringArr[i].Replace('.', ','));
                            dicountDoubleArr[i] = Convert.ToDouble(dicountStringArr[i].Replace('.', ','));

                        }

                        double[] payArr = new double[12];
                        int j = 0;
                        for (int i = 0; i < payForMonthStringArr.Length; i++)
                        {
                            if (payForMonthDobuleArr[i] != 0)
                            {

                                if (windowObj.textBoxArrForArrearsDefreyment[j].Text == "") payArr[i] = 0;
                                else { payArr[i] = Convert.ToDouble(windowObj.textBoxArrForArrearsDefreyment[j].Text); }
                                j++;
                                continue;

                            }
                            payArr[i] = 0;
                        }

                        ArrayList monthWithDiscountList = new ArrayList();
                        for (int i = 0; i < payedByListenerStringArr.Length; i++)
                        {
                            if (payForMonthDobuleArr[i] != 0)
                            {
                                if (toPayDoubleArr[i] < payArr[i]) { System.Windows.Forms.MessageBox.Show("Невозможно принять оплаты больше чем необходимо заплатить"); return; }
                                if (payedByListenerDoubleArr[i] == 0 && toPayDoubleArr[i] != 0 && toPayDoubleArr[i] == payArr[i]) { monthWithDiscountList.Add(i); }

                            }
                        }
                        double dicountPercent = 0;

                        //получение процента скидки 
                        if (monthWithDiscountList.Count > 1)
                        {
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "SELECT skidka FROM skidki where kol_month=" + monthWithDiscountList.Count + "";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        dicountPercent = reader1.GetDouble(0);
                                    }

                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }
                        payedByListener = "'{";
                        toPay = "'{";
                        dicount = "'{";
                        double surplus = 0;
                        double allSum = 0;

                        for (int i = 0; i < payArr.Length; i++)
                        {
                            if (payForMonthDobuleArr[i] != 0)
                            {
                                if (monthWithDiscountList.IndexOf(i) != -1) { toPayDoubleArr[i] -= payArr[i]; payedByListenerDoubleArr[i] += payArr[i] - (payArr[i] * dicountPercent / 100); dicountDoubleArr[i] = dicountPercent; surplus += payArr[i] * dicountPercent / 100; }
                                else { toPayDoubleArr[i] -= payArr[i]; payedByListenerDoubleArr[i] += payArr[i]; }

                            }
                            allSum += payArr[i];
                            payedByListener += payedByListenerDoubleArr[i].ToString().Replace(',', '.') + ",";
                            toPay += toPayDoubleArr[i].ToString().Replace(',', '.') + ",";
                            dicount += dicountDoubleArr[i].ToString().Replace(',', '.') + ",";
                        }

                        payedByListener = payedByListener.Substring(0, payedByListener.Length - 1) + "}'";
                        toPay = toPay.Substring(0, toPay.Length - 1) + "}'";
                        dicount = dicount.Substring(0, dicount.Length - 1) + "}'";
                        //обновление записи
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                            con1.Open();
                            string sql1 = "UPDATE listdolg SET payedlist=" + payedByListener + ", skidkiforpay=" + dicount + ", topay=" + toPay + "  WHERE listenerid = (select listenerid from listeners where fio='" + windowObj.ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + windowObj.GroupsDolg.SelectedItem + "')";
                            NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                            com1.ExecuteNonQuery();
                            con1.Close();
                        }
                        catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }

                        //добавление записи в таблицу дохода 
                        if (allSum != 0)
                        {
                            try
                            {
                                allSum -= surplus;
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ( 1, " + allSum.ToString().Replace(',', '.') + ", now(),'" + windowObj.ListenerDolg.SelectedItem + "');";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }

                        MessageBox.Show("Остаток от суммы оплаты из-за скидок при оплате на перед: " + surplus);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            updateDefraymentTable.Update(windowObj,2);
        }
    }
}
