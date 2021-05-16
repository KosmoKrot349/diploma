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
            StreamReader a = new StreamReader(@"rsDump.bat");
            while (!a.EndOfStream)
            {
                string str = a.ReadLine();
                for (int i = 0; i < str.Length; i++)
                {
                    if ((str[i] >= 'а' && str[i] <= 'я') || (str[i] >= 'А' && str[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русских символов"); return; }
                }

            }
            a.Close();

            if (windowObj.rsBckpPyt.Text == "") { MessageBox.Show("Укажите файл для восстановления"); return; }
            StreamReader reader = new StreamReader(@"setting.txt");
            ArrayList ls = new ArrayList();
            while (!reader.EndOfStream)
            {
                ls.Add(reader.ReadLine());
            }
            object[] mas = ls.ToArray();
            string newConnStr = "Server=" + mas[0].ToString().Split(':')[1] + ";Port=" + mas[2].ToString().Split(':')[1] + ";User Id=postgres;Password=" + mas[1].ToString().Split(':')[1] + ";";

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
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            StreamReader.Close();
            object[] batStrMas = arLs.ToArray();
            string lastStr = batStrMas[2].ToString();
            int index_puti = 0;
            for (int i = 0; i < lastStr.Length; i++)
            {
                if (lastStr[i] == '<') { index_puti = i; break; }
            }
            batStrMas[2] = "psql -d postgresql://postgres:" + mas[1].ToString().Split(':')[1] + "@" + mas[0].ToString().Split(':')[1] + ":" + mas[2].ToString().Split(':')[1] + "/db < " + windowObj.rsBckpPyt.Text;
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
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
