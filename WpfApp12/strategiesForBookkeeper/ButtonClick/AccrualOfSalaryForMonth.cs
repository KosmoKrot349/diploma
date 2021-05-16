using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class AccrualOfSalaryForMonth:IButtonClick
    {
        BuhgalterWindow windowObj;

        public AccrualOfSalaryForMonth(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            for (int i = 0; i < windowObj.ChbxMas_SotrNuch.Length; i++)
            {
                if (windowObj.ChbxMas_SotrNuch[i].IsChecked == true)
                {
                    //проверка есть ли запись уже
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "select nachid from nachisl where sotrid = " + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Year FROM nachisl.payday)=" + windowObj.dateNuch.Year + " and  EXTRACT(Month FROM nachisl.payday)=" + windowObj.dateNuch.Month;
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        //если запись есть
                        if (reader.HasRows)
                        {
                            //подсчёт за преподавателя
                            double prep_zp = 0;
                            double prep_zp_kateg = 0;
                            double prep_zp_kol_chas = 0;
                            double prep_zp_koefVislugi = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kateg = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid=" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= " + windowObj.dateNuch.Month + " and EXTRACT(Year FROM  raspisanie.date)= " + windowObj.dateNuch.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kol_chas = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('" + windowObj.dateNuch.ToShortDateString().Replace('.', '-') + "',prep.date_start)) from prep where sotrid = " + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_koefVislugi = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            prep_zp = prep_zp_kateg * prep_zp_kol_chas * prep_zp_koefVislugi;

                            //подсчёт за штат
                            string[] statesStr;
                            string[] stavkyStr;
                            string[] obslworkStr;
                            string[] obemStr;
                            double payShtat = 0;
                            double payObsl = 0;
                            //получение кол-ва отработаных дней в месяце
                            int kol_work_day = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + "] and extract(Year from date)=" + windowObj.dateNuch.Year + " and extract(Month from date)=" + windowObj.dateNuch.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        kol_work_day = reader1.GetInt32(0);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение массивов должностей, ставок, сдельных работ и их объёма.
                            try
                            {
                                NpgsqlConnection con13 = new NpgsqlConnection(windowObj.connectionString);
                                con13.Open();
                                string sql13 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid =" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com13 = new NpgsqlCommand(sql13, con13);
                                NpgsqlDataReader reader13 = com13.ExecuteReader();
                                if (reader13.HasRows)
                                {
                                    while (reader13.Read())
                                    {
                                        statesStr = reader13.GetString(0).Split('_');
                                        stavkyStr = reader13.GetString(1).Split('_');
                                        obslworkStr = reader13.GetString(2).Split('_');
                                        obemStr = reader13.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < statesStr.Length; j++)
                                        {
                                            if (statesStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day[" + windowObj.dateNuch.Month + "] from states where statesid =" + statesStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payShtat += reader2.GetDouble(0) * Convert.ToDouble(stavkyStr[j].Replace('.', ',')) * (kol_work_day / reader2.GetInt32(1));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                        //подсчёт зп сдельной
                                        for (int j = 0; j < obslworkStr.Length; j++)
                                        {
                                            if (obslworkStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + obslworkStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payObsl += reader2.GetDouble(0) * Convert.ToDouble(obemStr[j].Replace('.', ','));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                        }
                                    }
                                }
                                con13.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                            //получение процентов налогов
                            double vs = 0, fss = 0, ndfl = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select * from nalogi";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        vs = Math.Round((prep_zp * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(1) / 100), 2);
                                        fss = Math.Round((prep_zp * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(0) / 100), 2);
                                        ndfl = Math.Round((prep_zp * reader1.GetDouble(2) / 100) + (payShtat * reader1.GetDouble(2) / 100) + (payObsl * reader1.GetDouble(2) / 100), 2);

                                        prep_zp = Math.Round(prep_zp - ((prep_zp * reader1.GetDouble(1) / 100) + (prep_zp * reader1.GetDouble(0) / 100) + (prep_zp * reader1.GetDouble(2) / 100)), 2);
                                        payShtat = Math.Round(payShtat - ((payShtat * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(2) / 100)), 2);
                                        payObsl = Math.Round(payObsl - ((payObsl * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(2) / 100)), 2);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //обновление записи
                            try
                            {
                                NpgsqlConnection con12 = new NpgsqlConnection(windowObj.connectionString);
                                con12.Open();
                                string sql12 = "UPDATE nachisl SET  prepzp=" + prep_zp.ToString().Replace(',', '.') + ", shtatzp=" + payShtat.ToString().Replace(',', '.') + ", obslzp=" + payObsl.ToString().Replace(',', '.') + ", vs=" + vs.ToString().Replace(',', '.') + ", fss=" + fss.ToString().Replace(',', '.') + ", ndfl=" + ndfl.ToString().Replace(',', '.') + " WHERE sotrid=" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and extract(Year from payday)=" + windowObj.dateNuch.Year + " and extract(month from payday)=" + windowObj.dateNuch.Month;
                                NpgsqlCommand com12 = new NpgsqlCommand(sql12, con12);
                                com12.ExecuteNonQuery();
                                con12.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        }
                        //если записи нет
                        if (reader.HasRows == false)
                        {
                            //подсчёт за преподавателя
                            double prep_zp = 0;
                            double prep_zp_kateg = 0;
                            double prep_zp_kol_chas = 0;
                            double prep_zp_koefVislugi = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kateg = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid=" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= " + windowObj.dateNuch.Month + " and EXTRACT(Year FROM  raspisanie.date)= " + windowObj.dateNuch.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kol_chas = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('" + windowObj.dateNuch.ToShortDateString().Replace('.', '-') + "',prep.date_start)) from prep where sotrid = " + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_koefVislugi = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            prep_zp = prep_zp_kateg * prep_zp_kol_chas * prep_zp_koefVislugi;

                            //подсчёт за штат
                            string[] statesStr;
                            string[] stavkyStr;
                            string[] obslworkStr;
                            string[] obemStr;
                            double payShtat = 0;
                            double payObsl = 0;
                            //получение кол-ва отработаных дней в месяце
                            int kol_work_day = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + "] and extract(Year from date)=" + windowObj.dateNuch.Year + " and extract(Month from date)=" + windowObj.dateNuch.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        kol_work_day = reader1.GetInt32(0);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение массивов должностей, ставок, сдельных работ и их объёма.
                            try
                            {
                                NpgsqlConnection con11 = new NpgsqlConnection(windowObj.connectionString);
                                con11.Open();
                                string sql11 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid =" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com11 = new NpgsqlCommand(sql11, con11);
                                NpgsqlDataReader reader11 = com11.ExecuteReader();
                                if (reader11.HasRows)
                                {
                                    while (reader11.Read())
                                    {
                                        statesStr = reader11.GetString(0).Split('_');
                                        stavkyStr = reader11.GetString(1).Split('_');
                                        obslworkStr = reader11.GetString(2).Split('_');
                                        obemStr = reader11.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < statesStr.Length; j++)
                                        {
                                            if (statesStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day[" + windowObj.dateNuch.Month + "] from states where statesid =" + statesStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payShtat += reader2.GetDouble(0) * Convert.ToDouble(stavkyStr[j].Replace('.', ',')) * (kol_work_day / reader2.GetInt32(1));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                        //подсчёт зп сдельной

                                        for (int j = 0; j < obslworkStr.Length; j++)
                                        {
                                            if (obslworkStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + obslworkStr[j];

                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payObsl += reader2.GetDouble(0) * Convert.ToDouble(obemStr[j].Replace('.', ','));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                        }
                                    }
                                }
                                con11.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                            //получение процентов налогов
                            double vs = 0, fss = 0, ndfl = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select * from nalogi";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        vs = Math.Round((prep_zp * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(1) / 100), 2);
                                        fss = Math.Round((prep_zp * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(0) / 100), 2);
                                        ndfl = Math.Round((prep_zp * reader1.GetDouble(2) / 100) + (payShtat * reader1.GetDouble(2) / 100) + (payObsl * reader1.GetDouble(2) / 100), 2);

                                        prep_zp = Math.Round(prep_zp - ((prep_zp * reader1.GetDouble(1) / 100) + (prep_zp * reader1.GetDouble(0) / 100) + (prep_zp * reader1.GetDouble(2) / 100)), 2);
                                        payShtat = Math.Round(payShtat - ((payShtat * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(2) / 100)), 2);
                                        payObsl = Math.Round(payObsl - ((payObsl * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(2) / 100)), 2);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //добавление записи
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO nachisl(sotrid, prepzp, shtatzp, obslzp, viplacheno, payday,vs, fss, ndfl) VALUES (" + windowObj.ChbxMas_SotrNuch[i].Name.Split('_')[1] + ", " + prep_zp.ToString().Replace(',', '.') + " , " + payShtat.ToString().Replace(',', '.') + ", " + payObsl.ToString().Replace(',', '.') + ", 0, '" + windowObj.dateNuch.ToShortDateString().Replace('.', '-') + "', " + vs.ToString().Replace(',', '.') + ", " + fss.ToString().Replace(',', '.') + ", " + ndfl.ToString().Replace(',', '.') + ")";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        }
                        con.Close();
                    }
                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
            }
            windowObj.NachDataGrid.SelectedItem = null;
            windowObj.ViplataBut.IsEnabled = false;
            DataGridUpdater.updateGridNachZp(windowObj.connectionString, windowObj.NachMonthLabel, windowObj.ChbxMas_SotrNuch, windowObj.NachSotrGrid, windowObj.NachDataGrid, windowObj.dateNuch);
        }
    }
}
