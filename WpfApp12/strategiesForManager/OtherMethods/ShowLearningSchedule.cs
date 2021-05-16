using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.OtherMethods
{
    public class ShowLearningSchedule
    {
        public static void ShowForGroops(DateTime dm, DateTime ds,DirectorWindow window)
        {
            window.m = 0;//число зантий в дне
            window.n = 0;//число групп
            window.LabelDateRasp.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(grid) from groups";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.n == 0) { MessageBox.Show("Нету групп"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.m == 0) { MessageBox.Show("Нету занятий"); return; }
            window.HideAll();
            window.DeleteRaspBut.IsEnabled = false;
            window.ChangeRaspBut.IsEnabled = false;
            window.AddRaspBut.IsEnabled = false;
            window.raspGridG.Visibility = Visibility.Visible;
            window.lbmas = new Label[(window.m * 7) + 1, window.n + 2];
            DataGridUpdater.updateGridRaspG(window.connectionString, window.tG, window.m, window.n, window.lbmas, dm, ds);
            for (int i = 0; i < (window.m * 7) + 1; i++)
            {
                for (int j = 1; j < window.n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.lbmas[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
        public static void ShowForTeachers(DateTime dm, DateTime ds,DirectorWindow window)
        {
            window.m = 0;//число зантий в дне
            window.n = 0;//число преподавателей
            window.LabelDateRaspP.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(prepid) from prep";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.n == 0) { MessageBox.Show("Нету преподавателей"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.m == 0) { MessageBox.Show("Нету занятий"); return; }
            window.HideAll();
            window.DeleteRaspButP.IsEnabled = false;
            window.ChangeRaspButP.IsEnabled = false;
            window.AddRaspButP.IsEnabled = false;
            window.raspGridP.Visibility = Visibility.Visible;
            window.lbmas = new Label[(window.m * 7) + 1, window.n + 2];
            DataGridUpdater.updateGridRaspP(window.connectionString, window.tGp, window.m, window.n, window.lbmas, dm, ds);
            for (int i = 0; i < (window.m * 7) + 1; i++)
            {
                for (int j = 1; j < window.n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.lbmas[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
        public static void ShowForCabinets(DateTime dm, DateTime ds,DirectorWindow window)
        {
           window.m = 0;//число зантий в дне
            window.n = 0;//число кабинетов
            window.LabelDateRaspС.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(cabid) from cabinet";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.n == 0) { MessageBox.Show("Нет кабинетов"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        window.m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.m == 0) { MessageBox.Show("Нет занятий"); return; }
            window.HideAll();
            window.DeleteRaspButС.IsEnabled = false;
            window.ChangeRaspButС.IsEnabled = false;
            window.AddRaspButС.IsEnabled = false;
            window.raspGridС.Visibility = Visibility.Visible;
            window.lbmas = new Label[(window.m * 7) + 1, window.n + 2];
            DataGridUpdater.updateGridRaspС(window.connectionString, window.tGс,window.m, window.n, window.lbmas, dm, ds);
            for (int i = 0; i < (window.m * 7) + 1; i++)
            {
                for (int j = 1; j < window.n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.lbmas[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
    }
}
