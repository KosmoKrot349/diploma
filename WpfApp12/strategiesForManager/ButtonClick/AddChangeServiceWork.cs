using Npgsql;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddChangeServiceWork:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeServiceWork(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("rabotyid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("pay", System.Type.GetType("System.Double"));
            table.Columns.Add("ed_izm", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.ObslWorkDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = windowObj.ObslWorkDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название"); return; }
                if (recordArr[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указана оплата"); return; }
                if (recordArr[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указаны единицы измерения"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Название " + recordArr[1] + " повторяется"); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            string sql = "select * from raboty_obsl";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateServiceWorksDataGrid(windowObj);
            windowObj.ObslWorkDataGrid.SelectedItem = null;
            windowObj.ObslWorkDeleteButton.IsEnabled = false;
        }
    }
}
