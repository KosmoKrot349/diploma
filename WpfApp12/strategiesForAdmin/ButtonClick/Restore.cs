using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Npgsql;
using System.Windows;
using System.Collections;
using System.Diagnostics;

namespace WpfApp12.strategiesForAdmin
{
    class Restore:IButtonClick
    {
        private AdminWindow windowObj;

        public Restore(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            StreamReader StramReader = new StreamReader(@"rsDump.bat");
            while (!StramReader.EndOfStream)
            {
                string str = StramReader.ReadLine();
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] >= 'а' && str[i] <= 'я') || (str[i] >= 'А' && str[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русских символов"); return; }
                }

            }
            StramReader.Close();

            if (windowObj.rsBckpPyt.Text == "") { MessageBox.Show("Укажите файл для восстановления"); return; }
            StreamReader reader = new StreamReader(@"setting.txt");
            ArrayList ListFromSettingFile = new ArrayList();
            while (!reader.EndOfStream)
            {
                ListFromSettingFile.Add(reader.ReadLine());
            }
            object[] arr = ListFromSettingFile.ToArray();
            string newConnStr = "Server=" + arr[0].ToString().Split(':')[1] + ";Port=" + arr[2].ToString().Split(':')[1] + ";User Id=postgres;Password=" + arr[1].ToString().Split(':')[1] + ";";

            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(newConnStr);
            try
            {

                npgSqlConnection.Open();
                string sql = "SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = 'db' AND pid<> pg_backend_pid(); ";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к БД."); return; }

            try
            {
                npgSqlConnection.Open();
                string sql = "drop database if exists db";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
                npgSqlConnection.Open();
                sql = "create database db";
                Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
            }

            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList ListFromBatFile = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                ListFromBatFile.Add(StreamReader.ReadLine());
            }
            StreamReader.Close();
            object[] StringArrFromBatFile = ListFromBatFile.ToArray();
            string lastStr = StringArrFromBatFile[2].ToString();
            int PathFileIndex = 0;
            for (int i = 0; i < lastStr.Length; i++)
            {
                if (lastStr[i] == '<') { PathFileIndex = i; break; }
            }
            StringArrFromBatFile[2] = "psql -d postgresql://postgres:" + arr[1].ToString().Split(':')[1] + "@" + arr[0].ToString().Split(':')[1] + ":" + arr[2].ToString().Split(':')[1] + "/db < " + windowObj.rsBckpPyt.Text;
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < StringArrFromBatFile.Length; i++)
            {
                StreamWriter.WriteLine(StringArrFromBatFile[i]);
            }
            StreamWriter.Close();
            Process.Start("rsDump.bat");
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select fio from users";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader1 = com.ExecuteReader();
                con.Close();
            }
            catch { }
        }
    }
}
