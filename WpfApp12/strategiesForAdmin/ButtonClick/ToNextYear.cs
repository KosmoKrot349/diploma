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
                ArrayList ListFromBatFile = new ArrayList();
                while (!StreamReader.EndOfStream)
                {
                    ListFromBatFile.Add(StreamReader.ReadLine());
                }
                object[] stringArrFromBatFile = ListFromBatFile.ToArray();
                StreamReader.Close();
                StreamReader StreamReader2 = new StreamReader(@"setting.txt");
                ArrayList ListFromSettingsFile = new ArrayList();
                while (!StreamReader2.EndOfStream)
                {
                    ListFromSettingsFile.Add(StreamReader2.ReadLine());
                }
                StreamReader2.Close();
                object[] StringArrFromSettingsFile = ListFromSettingsFile.ToArray();

                string batLastStr = "pg_dump -d postgresql://postgres:" + StringArrFromSettingsFile[1].ToString().Split(':')[1] + "@" + StringArrFromSettingsFile[0].ToString().Split(':')[1] + ":" + StringArrFromSettingsFile[2].ToString().Split(':')[1] + "/db > ";
                if (windowObj.bckpNameNextYear.Text == "")
                {
                    DateTime dateNow = DateTime.Now;
                    batLastStr += windowObj.bckpPytNextYear.Text + "" + dateNow.Day + "_" + dateNow.Month + "_" + dateNow.Year + "_" + dateNow.Hour + "_" + dateNow.Minute + "_" + dateNow.Second + "_stary_god.sql";
                }
                else
                {
                    batLastStr += windowObj.bckpPytNextYear.Text + windowObj.bckpName.Text + "_stary_god.sql";

                }
                stringArrFromBatFile[2] = batLastStr;
                StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
                for (int i = 0; i < stringArrFromBatFile.Length; i++)
                {
                    StreamWriter.WriteLine(stringArrFromBatFile[i]);
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


                        int listenerID = reader.GetInt32(0);
                        int groopID = reader.GetInt32(1);
                        //полчуение массива скидок и групп слушателя из удаляемой записи
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                            con2.Open();
                            string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid=" + listenerID;

                            NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    string[] GroopIDStrArr = reader2.GetString(0).Split('_');
                                    string[] GroopsOfListenerStringArr = reader2.GetString(1).Split('_');

                                    string newArrGroopID = "'{";
                                    string newArrGroopsOFListeners = "'{";
                                    //запись массивов без удаляемого елемента
                                    for (int i = 0; i < GroopIDStrArr.Length; i++)
                                    {

                                        if (Convert.ToInt32(GroopIDStrArr[i]) != groopID)
                                        {

                                            newArrGroopID += GroopIDStrArr[i] + ",";
                                            newArrGroopsOFListeners += GroopsOfListenerStringArr[i] + ",";
                                        }

                                    }
                                    if (newArrGroopID.Length != 2)
                                    {
                                        newArrGroopID = newArrGroopID.Substring(0, newArrGroopID.Length - 1) + "}'";
                                        newArrGroopsOFListeners = newArrGroopsOFListeners.Substring(0, newArrGroopsOFListeners.Length - 1) + "}'";
                                    }
                                    else
                                    {
                                        newArrGroopID += "}'";
                                        newArrGroopsOFListeners += "}'";
                                    }
                                    try
                                    {
                                        NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                                        con3.Open();
                                        string sql3 = "UPDATE listeners SET  grid=" + newArrGroopID + ", lgt=" + newArrGroopsOFListeners + " WHERE listenerid = " + listenerID;
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

            ArrayList ListenersAccrualIDlist = new ArrayList();
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
                    string[] toPayArr = reader.GetString(0).Split('_');
                    DateTime dateStopLearn = reader.GetDateTime(1);
                    DateTime dateStartLearnGroop = reader.GetDateTime(2);
                    DateTime dateEndLearnGroop = reader.GetDateTime(3);
                    int ListenersAccrualID = reader.GetInt32(4);

                    while (reader.Read())
                    {

                        //начало перерасчёта пени
                        bool payed = true;
                        bool checkedBool = false;
                        if (dateStartLearnGroop.Month > dateEndLearnGroop.Month)
                        {
                            if (dateStartLearnGroop.Month < dateStopLearn.Month && dateStartLearnGroop.Year <= dateStopLearn.Year && checkedBool == false)
                            {
                                for (int i = dateStartLearnGroop.Month - 1; i < dateStopLearn.Month - 1; i++)
                                {
                                    if (toPayArr[i] != "0") { payed = false; break; }

                                }
                                checkedBool = true;
                            }


                            if (dateEndLearnGroop.Month >= dateStopLearn.Month && dateEndLearnGroop.Year <= dateStopLearn.Year && checkedBool == false)
                            {
                                for (int i = dateStartLearnGroop.Month - 1; i < 12; i++)
                                {

                                    if (toPayArr[i] != "0") { payed = false; break; }
                                }
                                for (int i = 0; i < dateStopLearn.Month - 1; i++)
                                {
                                    if (toPayArr[i] != "0") { payed = false; break; }
                                }
                                checkedBool = true;
                            }

                            if (dateEndLearnGroop.Month < dateStopLearn.Month && dateStartLearnGroop.Month > dateStopLearn.Month && dateEndLearnGroop.Year <= dateStopLearn.Year && checkedBool == false)
                            {
                                for (int i = dateStartLearnGroop.Month - 1; i < 12; i++)
                                {
                                    if (toPayArr[i] != "0") { payed = false; break; }
                                }
                                for (int i = 0; i <= dateEndLearnGroop.Month - 1; i++)
                                {
                                    if (toPayArr[i] != "0") { payed = false; break; }
                                }
                                checkedBool = true;
                            }
                        }

                        if (dateStartLearnGroop.Month < dateEndLearnGroop.Month && dateStartLearnGroop.Year <= dateStopLearn.Year && dateEndLearnGroop.Year <= dateStopLearn.Year && checkedBool == false)
                        {
                            for (int i = dateStartLearnGroop.Month - 1; i < dateStopLearn.Month - 1; i++)
                            {
                                if (toPayArr[i] != "0") { payed = false; break; }
                            }
                            checkedBool = true;
                        }

                        if (dateStartLearnGroop.Month == dateEndLearnGroop.Month)
                        {
                            if (dateStartLearnGroop.Year == dateEndLearnGroop.Year && dateStartLearnGroop.Year <= dateStopLearn.Year && checkedBool == false)
                            {
                                for (int i = dateStartLearnGroop.Month - 1; i < dateStopLearn.Month - 1; i++)
                                {
                                    if (toPayArr[i] != "0") { payed = false; break; }
                                }
                                checkedBool = true;
                            }
                        }

                        if (payed == true && checkedBool == true)
                        {
                            ListenersAccrualIDlist.Add(ListenersAccrualID);
                        }


                    }

                }

                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //очитска остановленных записей об оплате где выплачена вся сумма
            object[] ListenersAccrualIDArr = ListenersAccrualIDlist.ToArray();
            for (int i = 0; i < ListenersAccrualIDArr.Length; i++)
            {

                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select listenerid,grid from listnuch where listnuchid=" + ListenersAccrualIDArr[i];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int listenerID = reader.GetInt32(0);
                            int groopID = reader.GetInt32(1);
                            //полчуение массива скидок и групп слушателя из удаляемой записи
                            try
                            {
                                NpgsqlConnection con2 = new NpgsqlConnection(windowObj.connectionString);
                                con2.Open();
                                string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid=" + listenerID;
                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                if (reader2.HasRows)
                                {
                                    while (reader2.Read())
                                    {
                                        string[] groopIDStringArr = reader2.GetString(0).Split('_');
                                        string[] groopOfListenersStringArr = reader2.GetString(1).Split('_');

                                        string newGroopIDStringArr = "'{";
                                        string newGroopOfListenersStringArr = "'{";
                                        //запись массивов без удаляемого елемента
                                        for (int i2 = 0; i2 < groopIDStringArr.Length; i2++)
                                        {
                                            if (Convert.ToInt32(groopIDStringArr[i2]) != groopID)
                                            {
                                                newGroopIDStringArr += groopIDStringArr[i2] + ",";
                                                newGroopOfListenersStringArr += groopOfListenersStringArr[i2] + ",";
                                            }

                                        }
                                        if (newGroopIDStringArr.Length != 2)
                                        {
                                            newGroopIDStringArr = newGroopIDStringArr.Substring(0, newGroopIDStringArr.Length - 1) + "}'";
                                            newGroopOfListenersStringArr = newGroopOfListenersStringArr.Substring(0, newGroopOfListenersStringArr.Length - 1) + "}'";
                                        }
                                        else
                                        {
                                            newGroopIDStringArr += "}'";
                                            newGroopOfListenersStringArr += "}'";
                                        }
                                        try
                                        {
                                            NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                                            con3.Open();
                                            string sql3 = "UPDATE listeners SET  grid=" + newGroopIDStringArr + ", lgt=" + newGroopOfListenersStringArr + " WHERE listenerid = " + listenerID;
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
                    string sql = "delete from listnuch where  listnuchid=" + ListenersAccrualIDArr[i];
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
