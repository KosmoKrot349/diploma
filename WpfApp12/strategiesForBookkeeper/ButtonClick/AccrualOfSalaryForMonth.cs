using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class AccrualOfSalaryForMonth:IButtonClick
    {
        BookkeeperWindow windowObj;

        public AccrualOfSalaryForMonth(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            for (int i = 0; i < windowObj.checkBoxArrStaffForAccrual.Length; i++)
            {
                if (windowObj.checkBoxArrStaffForAccrual[i].IsChecked == true)
                {
                    //проверка есть ли запись уже
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "select nachid from nachisl where sotrid = " + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + " and EXTRACT(Year FROM nachisl.payday)=" + windowObj.dateAccrual.Year + " and  EXTRACT(Month FROM nachisl.payday)=" + windowObj.dateAccrual.Month;
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        //если запись есть
                        if (reader.HasRows)
                        {
                            //подсчёт за преподавателя
                            double teacherSalary = 0;
                            double teaceherSalaryWithCategory = 0;
                            double teacherSalaryForHour = 0;
                            double teacherSalaryWithWorkCoeff = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teaceherSalaryWithCategory = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid=" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= " + windowObj.dateAccrual.Month + " and EXTRACT(Year FROM  raspisanie.date)= " + windowObj.dateAccrual.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teacherSalaryForHour = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('" + windowObj.dateAccrual.ToShortDateString().Replace('.', '-') + "',prep.date_start)) from prep where sotrid = " + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teacherSalaryWithWorkCoeff = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            teacherSalary = teaceherSalaryWithCategory * teacherSalaryForHour * teacherSalaryWithWorkCoeff;

                            //подсчёт за штат
                            string[] postionsArr;
                            string[] ratesArr;
                            string[] serviceWorkArr;
                            string[] workVolumeArr;
                            double StaffSalary = 0;
                            double SalaryForServiceWork = 0;
                            //получение кол-ва отработаных дней в месяце
                            int quanWorkDay = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + "] and extract(Year from date)=" + windowObj.dateAccrual.Year + " and extract(Month from date)=" + windowObj.dateAccrual.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        quanWorkDay = reader1.GetInt32(0);
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
                                string sql13 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid =" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1];
                                NpgsqlCommand com13 = new NpgsqlCommand(sql13, con13);
                                NpgsqlDataReader reader13 = com13.ExecuteReader();
                                if (reader13.HasRows)
                                {
                                    while (reader13.Read())
                                    {
                                        postionsArr = reader13.GetString(0).Split('_');
                                        ratesArr = reader13.GetString(1).Split('_');
                                        serviceWorkArr = reader13.GetString(2).Split('_');
                                        workVolumeArr = reader13.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < postionsArr.Length; j++)
                                        {
                                            if (postionsArr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day[" + windowObj.dateAccrual.Month + "] from states where statesid =" + postionsArr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        StaffSalary += reader2.GetDouble(0) * Convert.ToDouble(ratesArr[j].Replace('.', ',')) * (quanWorkDay / reader2.GetInt32(1));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                        //подсчёт зп сдельной
                                        for (int j = 0; j < serviceWorkArr.Length; j++)
                                        {
                                            if (serviceWorkArr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + serviceWorkArr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        SalaryForServiceWork += reader2.GetDouble(0) * Convert.ToDouble(workVolumeArr[j].Replace('.', ','));
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
                                        vs = Math.Round((teacherSalary * reader1.GetDouble(1) / 100) + (StaffSalary * reader1.GetDouble(1) / 100) + (SalaryForServiceWork * reader1.GetDouble(1) / 100), 2);
                                        fss = Math.Round((teacherSalary * reader1.GetDouble(0) / 100) + (StaffSalary * reader1.GetDouble(0) / 100) + (SalaryForServiceWork * reader1.GetDouble(0) / 100), 2);
                                        ndfl = Math.Round((teacherSalary * reader1.GetDouble(2) / 100) + (StaffSalary * reader1.GetDouble(2) / 100) + (SalaryForServiceWork * reader1.GetDouble(2) / 100), 2);

                                        teacherSalary = Math.Round(teacherSalary - ((teacherSalary * reader1.GetDouble(1) / 100) + (teacherSalary * reader1.GetDouble(0) / 100) + (teacherSalary * reader1.GetDouble(2) / 100)), 2);
                                        StaffSalary = Math.Round(StaffSalary - ((StaffSalary * reader1.GetDouble(1) / 100) + (StaffSalary * reader1.GetDouble(0) / 100) + (StaffSalary * reader1.GetDouble(2) / 100)), 2);
                                        SalaryForServiceWork = Math.Round(SalaryForServiceWork - ((SalaryForServiceWork * reader1.GetDouble(1) / 100) + (SalaryForServiceWork * reader1.GetDouble(0) / 100) + (SalaryForServiceWork * reader1.GetDouble(2) / 100)), 2);
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
                                string sql12 = "UPDATE nachisl SET  prepzp=" + teacherSalary.ToString().Replace(',', '.') + ", shtatzp=" + StaffSalary.ToString().Replace(',', '.') + ", obslzp=" + SalaryForServiceWork.ToString().Replace(',', '.') + ", vs=" + vs.ToString().Replace(',', '.') + ", fss=" + fss.ToString().Replace(',', '.') + ", ndfl=" + ndfl.ToString().Replace(',', '.') + " WHERE sotrid=" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + " and extract(Year from payday)=" + windowObj.dateAccrual.Year + " and extract(month from payday)=" + windowObj.dateAccrual.Month;
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
                            double teacherSalary = 0;
                            double teaceherSalaryWithCategory = 0;
                            double teacherSalaryForHour = 0;
                            double teacherSalaryWithWorkCoeff = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teaceherSalaryWithCategory = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid=" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= " + windowObj.dateAccrual.Month + " and EXTRACT(Year FROM  raspisanie.date)= " + windowObj.dateAccrual.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teacherSalaryForHour = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('" + windowObj.dateAccrual.ToShortDateString().Replace('.', '-') + "',prep.date_start)) from prep where sotrid = " + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { teacherSalaryWithWorkCoeff = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            teacherSalary = teaceherSalaryWithCategory * teacherSalaryForHour * teacherSalaryWithWorkCoeff;

                            //подсчёт за штат
                            string[] postionsArr;
                            string[] ratesArr;
                            string[] serviceWorkArr;
                            string[] workVolumeArr;
                            double StaffSalary = 0;
                            double SalaryForServiceWork = 0;
                            //получение кол-ва отработаных дней в месяце
                            int quanWorkDay = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + "] and extract(Year from date)=" + windowObj.dateAccrual.Year + " and extract(Month from date)=" + windowObj.dateAccrual.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        quanWorkDay = reader1.GetInt32(0);
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
                                string sql11 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid =" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1];
                                NpgsqlCommand com11 = new NpgsqlCommand(sql11, con11);
                                NpgsqlDataReader reader11 = com11.ExecuteReader();
                                if (reader11.HasRows)
                                {
                                    while (reader11.Read())
                                    {
                                        postionsArr = reader11.GetString(0).Split('_');
                                        ratesArr = reader11.GetString(1).Split('_');
                                        serviceWorkArr = reader11.GetString(2).Split('_');
                                        workVolumeArr = reader11.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < postionsArr.Length; j++)
                                        {
                                            if (postionsArr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day[" + windowObj.dateAccrual.Month + "] from states where statesid =" + postionsArr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        StaffSalary += reader2.GetDouble(0) * Convert.ToDouble(ratesArr[j].Replace('.', ',')) * (quanWorkDay / reader2.GetInt32(1));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                        //подсчёт зп сдельной

                                        for (int j = 0; j < serviceWorkArr.Length; j++)
                                        {
                                            if (serviceWorkArr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + serviceWorkArr[j];

                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        SalaryForServiceWork += reader2.GetDouble(0) * Convert.ToDouble(workVolumeArr[j].Replace('.', ','));
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
                                        vs = Math.Round((teacherSalary * reader1.GetDouble(1) / 100) + (StaffSalary * reader1.GetDouble(1) / 100) + (SalaryForServiceWork * reader1.GetDouble(1) / 100), 2);
                                        fss = Math.Round((teacherSalary * reader1.GetDouble(0) / 100) + (StaffSalary * reader1.GetDouble(0) / 100) + (SalaryForServiceWork * reader1.GetDouble(0) / 100), 2);
                                        ndfl = Math.Round((teacherSalary * reader1.GetDouble(2) / 100) + (StaffSalary * reader1.GetDouble(2) / 100) + (SalaryForServiceWork * reader1.GetDouble(2) / 100), 2);

                                        teacherSalary = Math.Round(teacherSalary - ((teacherSalary * reader1.GetDouble(1) / 100) + (teacherSalary * reader1.GetDouble(0) / 100) + (teacherSalary * reader1.GetDouble(2) / 100)), 2);
                                        StaffSalary = Math.Round(StaffSalary - ((StaffSalary * reader1.GetDouble(1) / 100) + (StaffSalary * reader1.GetDouble(0) / 100) + (StaffSalary * reader1.GetDouble(2) / 100)), 2);
                                        SalaryForServiceWork = Math.Round(SalaryForServiceWork - ((SalaryForServiceWork * reader1.GetDouble(1) / 100) + (SalaryForServiceWork * reader1.GetDouble(0) / 100) + (SalaryForServiceWork * reader1.GetDouble(2) / 100)), 2);
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
                                string sql1 = "INSERT INTO nachisl(sotrid, prepzp, shtatzp, obslzp, viplacheno, payday,vs, fss, ndfl) VALUES (" + windowObj.checkBoxArrStaffForAccrual[i].Name.Split('_')[1] + ", " + teacherSalary.ToString().Replace(',', '.') + " , " + StaffSalary.ToString().Replace(',', '.') + ", " + SalaryForServiceWork.ToString().Replace(',', '.') + ", 0, '" + windowObj.dateAccrual.ToShortDateString().Replace('.', '-') + "', " + vs.ToString().Replace(',', '.') + ", " + fss.ToString().Replace(',', '.') + ", " + ndfl.ToString().Replace(',', '.') + ")";
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
            windowObj.AccrualsDataGrid.SelectedItem = null;
            windowObj.AccrualOfSalaryForMonth.IsEnabled = false;
            DataGridUpdater.updateAccrualsSalaryDataGrid(windowObj);
        }
    }
}
