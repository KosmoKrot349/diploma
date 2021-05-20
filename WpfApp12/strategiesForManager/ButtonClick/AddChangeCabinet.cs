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
    class AddChangeCabinet:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeCabinet(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("cabid", System.Type.GetType("System.Int32"));
            table.Columns.Add("num", System.Type.GetType("System.String"));
            table.Columns.Add("adres", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.CabinetsDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = windowObj.CabinetsDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан адрес кабинета"); return; }
                if (recordArr[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан номер кабинета"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется название кабинета " + recordArr[1]); return; }
                list.Add(recordArr[3]);
                table.ImportRow(row);
            }
            string sql = "select * from cabinet";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            windowObj.CabinetsDataGrid.SelectedItem = null;

            //кабинеты
            windowObj.cabDeleteButton.IsEnabled = false;

            DataGridUpdater.updateCabinetDataGrid(windowObj);
        }
    }
}
