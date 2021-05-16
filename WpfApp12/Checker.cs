using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Windows;

namespace WpfApp12
{
    class Checker
    {
        //возвращает 1 или 0 на вопрос является ли пользователь директором
        public static int dirCheck(int uid,string conStr)
        {
            int a = 0;
            try
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(conStr);
                npgSqlConnection.Open();
                string sql = "select director from users where uid="+uid;
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                NpgsqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        a = reader.GetInt32(0);
                    }
                else { MessageBox.Show("Пользователя больше не существует"); }
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
            return a;
        }
        //возвращает 1 или 0 на вопрос является ли пользователь бухгалтером
        public static int buhgCheck(int uid, string conStr)
        {
            int a = 0;
            try
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(conStr);
                npgSqlConnection.Open();
                string sql = "select buhgalter from users where uid=" + uid;
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                NpgsqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        a = reader.GetInt32(0);
                    }
                else { MessageBox.Show("Пользователя больше не существует"); }
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
            return a;
        }
        //возвращает 1 или 0 на вопрос является ли пользователь админом
        public static int adminCheck(int uid, string conStr)
        {
            int a = 0;
            try
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(conStr);
                npgSqlConnection.Open();
                string sql = "select admin from users where uid=" + uid;
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                NpgsqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows)
                    while (reader.Read())
                    {
                        a = reader.GetInt32(0);
                    }
                else { MessageBox.Show("Пользователя больше не существует"); }
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }

            return a;
        }
    }

}
