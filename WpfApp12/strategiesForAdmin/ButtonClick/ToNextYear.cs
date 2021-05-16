using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;

namespace WpfApp12.strategiesForAdmin
{
    class ToNextYear:IButtonClick
    {
        private AdminWindow windowObj;

        public ToNextYear(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            //создание бэкапа за старый год
            MessageBoxResult res = MessageBox.Show("Вы собираетесь выполнить переход к новому году.\n Текущая версия Вашей базы данных будет сохранена отдельным файлом на вашем ПК.\n Будут подсчитаны расходы и доходы за весь год.\n Так же очищены выплаченные зп и таблицы дохода и расхода.\n Для слушателей оплативших весь год обучения записи будут обнулены, должники занесены в отдельную таблицу.\n Год в дате обучения у групп будет увеличен на 1.\n Вы не потеряете Ваши данные, Вы всегда можете их восстановить из файла который будет создан после этой процедуры. ", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                if (windowObj.bckpNameNextYear.Text != "")
                {
                    string bckpname = windowObj.bckpName.Text;
                    for (int i = 0; i < bckpname.Length; i++)
                    {
                        if ((bckpname[i] >= 'а' && bckpname[i] <= 'я') || (bckpname[i] >= 'А' && bckpname[i] <= 'Я')) { MessageBox.Show("В имени копии не должно быть русскких символов"); return; }
                    }

                }
                StreamReader StreamReader = new StreamReader(@"crDump.bat");
                ArrayList arLs = new ArrayList();
                while (!StreamReader.EndOfStream)
                {
                    arLs.Add(StreamReader.ReadLine());
                }
                object[] batStrMas = arLs.ToArray();
                StreamReader.Close();
                StreamReader StreamReader2 = new StreamReader(@"setting.txt");
                ArrayList arLs2 = new ArrayList();
                while (!StreamReader2.EndOfStream)
                {
                    arLs2.Add(StreamReader2.ReadLine());
                }
                StreamReader2.Close();
                object[] batStrMas2 = arLs2.ToArray();

                string batLastStr = "pg_dump -d postgresql://postgres:" + batStrMas2[1].ToString().Split(':')[1] + "@" + batStrMas2[0].ToString().Split(':')[1] + ":" + batStrMas2[2].ToString().Split(':')[1] + "/db > ";
                if (windowObj.bckpNameNextYear.Text == "")
                {
                    DateTime a = DateTime.Now;
                    batLastStr += windowObj.bckpPytNextYear.Text + "" + a.Day + "_" + a.Month + "_" + a.Year + "_" + a.Hour + "_" + a.Minute + "_" + a.Second + "_stary_god.sql";
                }
                else
                {
                    batLastStr += windowObj.bckpPytNextYear.Text + windowObj.bckpName.Text + "_stary_god.sql";

                }
                batStrMas[2] = batLastStr;
                StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
                for (int i = 0; i < batStrMas.Length; i++)
                {
                    StreamWriter.WriteLine(batStrMas[i]);
                }

                StreamWriter.Close();

                Process.Start("crDump.bat");
            }
            //запись суммы расходов и доходов за год
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO itog(itogidate, dohod, rashod)VALUES ( now(), (select coalesce(sum(sum),0) from dodhody), (select coalesce(sum(summ),0) from rashody))";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //очитска таблицы доходов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from dodhody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //очитска таблицы расходов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from rashody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удаление выплаченных зп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from nachisl where (prepzp+shtatzp+obslzp)-viplacheno=0";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //полчуение закрытыйх записей из оплат слушателей (+ удаление группы у слушателя)
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select listenerid,grid from listnuch where isclose=1";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        int listenerid = reader.GetInt32(0);
                        int grid = reader.GetInt32(1);
                        //полчуение массива скидок и групп слушателя из удаляемой записи
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                            con2.Open();
                            string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid=" + listenerid;

                            NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    string[] gridstr = reader2.GetString(0).Split('_');
                                    string[] lgtstr = reader2.GetString(1).Split('_');

                                    string gridstr2 = "'{";
                                    string lgtstr2 = "'{";
                                    //запись массивов без удаляемого елемента
                                    for (int i = 0; i < gridstr.Length; i++)
                                    {

                                        if (Convert.ToInt32(gridstr[i]) != grid)
                                        {

                                            gridstr2 += gridstr[i] + ",";
                                            lgtstr2 += lgtstr[i] + ",";
                                        }

                                    }
                                    if (gridstr2.Length != 2)
                                    {
                                        gridstr2 = gridstr2.Substring(0, gridstr2.Length - 1) + "}'";
                                        lgtstr2 = lgtstr2.Substring(0, lgtstr2.Length - 1) + "}'";
                                    }
                                    else
                                    {
                                        gridstr2 += "}'";
                                        lgtstr2 += "}'";
                                    }
                                    try
                                    {
                                        NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                                        con3.Open();
                                        string sql3 = "UPDATE listeners SET  grid=" + gridstr2 + ", lgt=" + lgtstr2 + " WHERE listenerid = " + listenerid;
                                        NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                        com3.ExecuteReader();
                                        con3.Close();
                                    }
                                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                }
                                con2.Close();
                            }
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удаление закрытых записей об оплате
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from listnuch where isclose=1";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            ArrayList list = new ArrayList();
            //получение id остановленных записей в которых всё оплачено 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select array_to_string(listnuch.topay,'_'),listnuch.date_stop,groups.date_start,groups.date_end,listnuch.listnuchid from listnuch inner join groups using(grid) where date_stop != null";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    string[] topay = reader.GetString(0).Split('_');
                    DateTime dateStop = reader.GetDateTime(1);
                    DateTime DatehStartLernG = reader.GetDateTime(2);
                    DateTime DatehEndLernG = reader.GetDateTime(3);
                    int listnuchid = reader.GetInt32(4);

                    while (reader.Read())
                    {

                        //начало перерасчёта пени
                        bool oplacheno = true;
                        bool prosmotrenno = false;
                        if (DatehStartLernG.Month > DatehEndLernG.Month)
                        {
                            if (DatehStartLernG.Month < dateStop.Month && DatehStartLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }

                                }
                                prosmotrenno = true;
                            }


                            if (DatehEndLernG.Month >= dateStop.Month && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                {

                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                for (int i = 0; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }

                            if (DatehEndLernG.Month < dateStop.Month && DatehStartLernG.Month > dateStop.Month && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                for (int i = 0; i <= DatehEndLernG.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }
                        }

                        if (DatehStartLernG.Month < DatehEndLernG.Month && DatehStartLernG.Year <= dateStop.Year && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                        {
                            for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                            {
                                if (topay[i] != "0") { oplacheno = false; break; }
                            }
                            prosmotrenno = true;
                        }

                        if (DatehStartLernG.Month == DatehEndLernG.Month)
                        {
                            if (DatehStartLernG.Year == DatehEndLernG.Year && DatehStartLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }
                        }

                        if (oplacheno == true && prosmotrenno == true)
                        {
                            list.Add(listnuchid);
                        }


                    }

                }

                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //очитска остановленных записей об оплате где выплачена вся сумма
            object[] listArr = list.ToArray();
            for (int i = 0; i < listArr.Length; i++)
            {

                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select listenerid,grid from listnuch where listnuchid=" + listArr[i];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            int listenerid = reader.GetInt32(0);
                            int grid = reader.GetInt32(1);
                            //полчуение массива скидок и групп слушателя из удаляемой записи
                            try
                            {
                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                con2.Open();
                                string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid=" + listenerid;
                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                if (reader2.HasRows)
                                {
                                    while (reader2.Read())
                                    {
                                        string[] gridstr = reader2.GetString(0).Split('_');
                                        string[] lgtstr = reader2.GetString(1).Split('_');

                                        string gridstr2 = "'{";
                                        string lgtstr2 = "'{";
                                        //запись массивов без удаляемого елемента
                                        for (int i2 = 0; i2 < gridstr.Length; i2++)
                                        {
                                            if (Convert.ToInt32(gridstr[i2]) != grid)
                                            {
                                                gridstr2 += gridstr[i2] + ",";
                                                lgtstr2 += lgtstr[i2] + ",";
                                            }

                                        }
                                        if (gridstr2.Length != 2)
                                        {
                                            gridstr2 = gridstr2.Substring(0, gridstr2.Length - 1) + "}'";
                                            lgtstr2 = lgtstr2.Substring(0, lgtstr2.Length - 1) + "}'";
                                        }
                                        else
                                        {
                                            gridstr2 += "}'";
                                            lgtstr2 += "}'";
                                        }
                                        try
                                        {
                                            NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                                            con3.Open();
                                            string sql3 = "UPDATE listeners SET  grid=" + gridstr2 + ", lgt=" + lgtstr2 + " WHERE listenerid = " + listenerid;
                                            NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                            com3.ExecuteReader();
                                            con3.Close();
                                        }
                                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                    }
                                    con2.Close();
                                }
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        }

                    }
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "delete from listnuch where  listnuchid=" + listArr[i];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }

            //перенос записей об оплате в таблицу должников
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO listdolg(listenerid, grid, year, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose, date_start, date_end) (select  listenerid, grid, now() ,payformonth, payedlist,skidkiforpay,topay,penya,date_stop,isclose,date_start,date_end from listnuch inner join groups using(grid) where topay[1]!=0 or topay[2]!=0 or topay[3]!=0 or topay[4]!=0 or topay[5]!=0 or topay[6]!=0 or topay[7]!=0 or topay[8]!=0 or topay[9]!=0 or topay[10]!=0 or topay[11]!=0 or topay[12]!=0)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //обновление записей у нормальных слушателей
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE listnuch SET payedlist='{0,0,0,0,0,0,0,0,0,0,0,0}', skidkiforpay='{0,0,0,0,0,0,0,0,0,0,0,0}', topay=payformonth, penya='{0,0,0,0,0,0,0,0,0,0,0,0}'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //обновление года в группах
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "update groups set  date_start =date_start + interval '1 year',date_end=date_end + interval '1 year'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
    }
}
