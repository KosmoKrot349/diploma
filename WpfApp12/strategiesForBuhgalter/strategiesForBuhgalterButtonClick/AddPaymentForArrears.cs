using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AddPaymentForArrears:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AddPaymentForArrears(BuhgalterWindow windowObj)
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
                        string payedlist = reader.GetString(0);
                        string[] payedlistMas = payedlist.Split('_');
                        double[] payedlistMasDouble = new double[12];

                        string topay = reader.GetString(1);
                        string[] topayMas = topay.Split('_');
                        double[] topayMasDouble = new double[12];

                        string payformonth = reader.GetString(2);
                        string[] payformonthMas = payformonth.Split('_');
                        double[] payformonthMasDouble = new double[12];

                        string skidkiforpay = reader.GetString(3);
                        string[] skidkiforpayMas = skidkiforpay.Split('_');
                        double[] skidkiforpayMasDouble = new double[12];
                        for (int i = 0; i < 12; i++)
                        {

                            payedlistMasDouble[i] = Convert.ToDouble(payedlistMas[i].Replace('.', ','));
                            topayMasDouble[i] = Convert.ToDouble(topayMas[i].Replace('.', ','));
                            payformonthMasDouble[i] = Convert.ToDouble(payformonthMas[i].Replace('.', ','));
                            skidkiforpayMasDouble[i] = Convert.ToDouble(skidkiforpayMas[i].Replace('.', ','));

                        }

                        double[] oplMas = new double[12];
                        int j = 0;
                        for (int i = 0; i < payformonthMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {

                                if (windowObj.masTbx2[j].Text == "") oplMas[i] = 0;
                                else { oplMas[i] = Convert.ToDouble(windowObj.masTbx2[j].Text); }
                                j++;
                                continue;

                            }
                            oplMas[i] = 0;
                        }

                        ArrayList monthSkidkoy = new ArrayList();
                        for (int i = 0; i < payedlistMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (topayMasDouble[i] < oplMas[i]) { System.Windows.Forms.MessageBox.Show("Невозможно принять оплаты больше чем необходимо заплатить"); return; }
                                if (payedlistMasDouble[i] == 0 && topayMasDouble[i] != 0 && topayMasDouble[i] == oplMas[i]) { monthSkidkoy.Add(i); }

                            }
                        }
                        double skidka = 0;

                        //получение процента скидки 
                        if (monthSkidkoy.Count > 1)
                        {
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "SELECT skidka FROM skidki where kol_month=" + monthSkidkoy.Count + "";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        skidka = reader1.GetDouble(0);
                                    }

                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }
                        payedlist = "'{";
                        topay = "'{";
                        skidkiforpay = "'{";
                        double zdacha = 0;
                        double allSum = 0;

                        for (int i = 0; i < oplMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (monthSkidkoy.IndexOf(i) != -1) { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i] - (oplMas[i] * skidka / 100); skidkiforpayMasDouble[i] = skidka; zdacha += oplMas[i] * skidka / 100; }
                                else { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i]; }

                            }
                            allSum += oplMas[i];
                            payedlist += payedlistMasDouble[i].ToString().Replace(',', '.') + ",";
                            topay += topayMasDouble[i].ToString().Replace(',', '.') + ",";
                            skidkiforpay += skidkiforpayMasDouble[i].ToString().Replace(',', '.') + ",";
                        }

                        payedlist = payedlist.Substring(0, payedlist.Length - 1) + "}'";
                        topay = topay.Substring(0, topay.Length - 1) + "}'";
                        skidkiforpay = skidkiforpay.Substring(0, skidkiforpay.Length - 1) + "}'";
                        //обновление записи
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                            con1.Open();
                            string sql1 = "UPDATE listdolg SET payedlist=" + payedlist + ", skidkiforpay=" + skidkiforpay + ", topay=" + topay + "  WHERE listenerid = (select listenerid from listeners where fio='" + windowObj.ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + windowObj.GroupsDolg.SelectedItem + "')";
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
                                allSum -= zdacha;
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ( 1, " + allSum.ToString().Replace(',', '.') + ", now(),'" + windowObj.ListenerDolg.SelectedItem + "');";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }

                        System.Windows.Forms.MessageBox.Show("Остаток от суммы оплаты из-за скидок при оплате на перед: " + zdacha);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            windowObj.updateOplataTable(2);
        }
    }
}
