﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class DeleteCourse:IButtonClick
    {
        DirectorWindow windowObj;

        public DeleteCourse(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.coursDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //удаление курса из групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select nazvanie from groups where courseid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string groups = "";
                    while (reader.Read())
                    {

                        groups += reader.GetString(0) + " ";

                    }
                    MessageBox.Show("Этот курс нельзя удалить, он используется в группах: " + groups);
                    return;

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "DELETE FROM courses WHERE courseid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridСourses(windowObj.connectionString, windowObj.filtr.sql, windowObj.coursDataGrid);

            windowObj.coursDataGrid.SelectedItem = null;

            //курсы
            windowObj.coursDeleteButton.IsEnabled = false;
            windowObj.coursChangeButton.IsEnabled = false;
        }
    }
}