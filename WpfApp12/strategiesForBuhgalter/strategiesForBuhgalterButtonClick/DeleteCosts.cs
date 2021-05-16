﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class DeleteCosts:IButtonClick
    {
        BuhgalterWindow windowObj;

        public DeleteCosts(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.RoshodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите удалить этот расход?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "DELETE FROM rashody WHERE rashid=" + arr[0];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                windowObj.RoshodyDataGrid.SelectedItem = null;
                windowObj.RashDeleteButton.IsEnabled = false;
                windowObj.RashChangeButton.IsEnabled = false;
                DataGridUpdater.updateDataGridRashody(windowObj.connectionString, windowObj.filtr.sql, windowObj.RoshodyDataGrid);
            }
        }
    }
}