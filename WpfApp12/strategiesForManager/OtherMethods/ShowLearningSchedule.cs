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
        public static void ShowForGroops(DateTime dm, DateTime ds,ManagerWindow window)
        {
            window.quanLessonsInDay = 0;//число зантий в дне
            window.quanGroops = 0;//число групп
            window.GroopsScheduleDateLabel.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
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
                        window.quanGroops = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanGroops == 0) { MessageBox.Show("Нету групп"); return; }
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
                        window.quanLessonsInDay = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanLessonsInDay == 0) { MessageBox.Show("Нету занятий"); return; }
            window.HideAll();
            window.DeleteScheduleGroop.IsEnabled = false;
            window.GoToChangeScheduleGroop.IsEnabled = false;
            window.GoToAddScheduleGroop.IsEnabled = false;
            window.GroopScheduleGrid.Visibility = Visibility.Visible;
            window.labelArr = new Label[(window.quanLessonsInDay * 7) + 1, window.quanGroops + 2];
            DataGridUpdater.updateGroopScheduleGrid(window);
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.labelArr[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
        public static void ShowForTeachers(DateTime dm, DateTime ds,ManagerWindow window)
        {
            window.quanLessonsInDay = 0;//число зантий в дне
            window.quanGroops = 0;//число преподавателей
            window.TeachersScheduleDateLabel.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
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
                        window.quanGroops = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanGroops == 0) { MessageBox.Show("Нету преподавателей"); return; }
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
                        window.quanLessonsInDay = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanLessonsInDay == 0) { MessageBox.Show("Нету занятий"); return; }
            window.HideAll();
            window.DeleteScheduleTeacher.IsEnabled = false;
            window.GoToChangeScheduleTeacher.IsEnabled = false;
            window.GoToAddScheduleTeacher.IsEnabled = false;
            window.TeacherScheduleGrid.Visibility = Visibility.Visible;
            window.labelArr = new Label[(window.quanLessonsInDay * 7) + 1, window.quanGroops + 2];
            DataGridUpdater.updateTeacherScheduleGrid(window);
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.labelArr[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
        public static void ShowForCabinets(DateTime dm, DateTime ds,ManagerWindow window)
        {
           window.quanLessonsInDay = 0;//число зантий в дне
            window.quanGroops = 0;//число кабинетов
            window.CabinetsScheduleDateLabel.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
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
                        window.quanGroops = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanGroops == 0) { MessageBox.Show("Нет кабинетов"); return; }
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
                        window.quanLessonsInDay = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (window.quanLessonsInDay == 0) { MessageBox.Show("Нет занятий"); return; }
            window.HideAll();
            window.DeleteScheduleCabinet.IsEnabled = false;
            window.GoToChangeScheduleCabinet.IsEnabled = false;
            window.GoToAddScheduleCabinet.IsEnabled = false;
            window.CabinetScheduleGrid.Visibility = Visibility.Visible;
            window.labelArr = new Label[(window.quanLessonsInDay * 7) + 1, window.quanGroops + 2];
            DataGridUpdater.updateScheduleCabinetGrid(window);
            for (int i = 0; i < (window.quanLessonsInDay * 7) + 1; i++)
            {
                for (int j = 1; j < window.quanGroops + 2; j++)
                {
                    if (i != 0 && j != 1)
                        window.labelArr[i, j].MouseDown += window.Label_MouseDown;
                }

            }
        }
    }
}
