﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class NextWeek:IButtonClick
    {
        DirectorWindow windowObj;
        object sender;

        public NextWeek(DirectorWindow windowObj, object sender)
        {
            this.windowObj = windowObj;
            this.sender = sender;
        }

        public void ButtonClick()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where day = 1 and date>'" + windowObj.dateMonday.ToShortDateString().Replace('.', '-') + "' order by  date limit 1";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("Новее расписания нет"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.dateMonday = reader.GetDateTime(0);
                    }
                }

                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.LabelDateRasp.Content = "Расписание на " + windowObj.dateMonday.ToShortDateString() + " - " + windowObj.dateMonday.AddDays(6).ToShortDateString();
            Button but = sender as Button;
            if (but.Name == "NextRaspBut") windowObj.showRaspG(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
            if (but.Name == "NextRaspButP") windowObj.showRaspP(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
            if (but.Name == "NextRaspButС") windowObj.showRaspС(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
            for (int i = 0; i < (windowObj.m * 7) + 1; i++)
            {
                for (int j = 1; j < windowObj.n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        windowObj.lbmas[i, j].MouseDown += windowObj.Label_MouseDown;
                }

            }
        }
    }
}