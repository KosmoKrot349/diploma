using Npgsql;
using System;
using System.Collections;
using System.Data;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddChangeCostsType:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeCostsType(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("typeid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("description", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.TypeRashDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = windowObj.TypeRashDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[0].GetType() == typeof(int))
                    if (Convert.ToInt32(recordArr[0]) >= 1 && Convert.ToInt32(recordArr[0]) <= 4) continue;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название расхода"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется название расхода " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            string sql = "select * from typerash";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateTypeCostsDataGrid(windowObj);
            windowObj.TypeRashDataGrid.SelectedItem = null;
            windowObj.TypeRashDeleteButton.IsEnabled = false;
        }
    }
}
