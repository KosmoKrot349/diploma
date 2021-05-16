﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class ConnectionCheck : IButtonClick
    {
        private AdminWindow windowObj;

        public ConnectionCheck(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            string testConStr = "Server=" + windowObj.connect.Text + ";Port=" + windowObj.dbPortText.Text + ";User Id=postgres;Password=" + windowObj.dbPassText.Text + ";Database=db;";
            NpgsqlConnection testcon = new NpgsqlConnection(testConStr);
            try
            {
                testcon.Open();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе по заданный параметрам"); return; }
            testcon.Close();
            MessageBoxResult res = MessageBox.Show("Подключение по данным пораметрам прошло успешно.\nСохранить параметры?", "Сохранение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.saveSettings();

            }
            if (res == MessageBoxResult.No)
            {
                return;
            }
        }
    }
}
