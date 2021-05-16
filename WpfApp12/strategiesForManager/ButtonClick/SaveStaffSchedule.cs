﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class SaveStaffSchedule:IButtonClick
    {
        DirectorWindow windowObj;

        public SaveStaffSchedule(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
          
                int day = 0;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (windowObj.lbmas_shtatRasp[i, j].Background == Brushes.Aqua) { day = Convert.ToInt32(windowObj.lbmas_shtatRasp[i, j].Content.ToString()); windowObj.lbmas_shtatRasp[i, j].Background = Brushes.White; }
                    }
                }
                DateTime dateToAdd = new DateTime(windowObj.date.Year, windowObj.date.Month, day);

                bool b = false;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select shraspid from shtatrasp where date= '" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows) b = true;
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                string sotrMas = "'{-1,";
                for (int i = 0; i < windowObj.chbxMas_stateRasp.Length; i++)
                {
                    if (windowObj.chbxMas_stateRasp[i].IsChecked == true)
                    {
                        sotrMas += windowObj.chbxMas_stateRasp[i].Name.Split('_')[1] + ",";
                    }
                }
                sotrMas = sotrMas.Substring(0, sotrMas.Length - 1) + "}'";

                if (b == false)
                {
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "INSERT INTO shtatrasp(shtatid, date)VALUES (" + sotrMas + ", '" + dateToAdd.ToShortDateString().Replace('.', '-') + "')";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                }
                else
                {

                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                        con.Open();
                        string sql = "update shtatrasp set shtatid=" + sotrMas + " where date ='" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                }
                DataGridUpdater.updateGridShtatRasp(windowObj.connectionString, windowObj.MonthGrid, windowObj.ShtatRaspSotrpGrid, windowObj.lbmas_shtatRasp, windowObj.chbxMas_stateRasp, windowObj.ShtatRaspMonthYearLabel, windowObj.date);
            windowObj.ShtatRaspSaveBut.IsEnabled = false;
         

        }
    }
}