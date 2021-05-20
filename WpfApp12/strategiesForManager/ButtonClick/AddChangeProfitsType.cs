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
    class AddChangeProfitsType:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeProfitsType(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("idtype", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("descriprion", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.ProfitTypesDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = windowObj.ProfitTypesDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[0].GetType() == typeof(int))
                    if (Convert.ToInt32(recordArr[0]) == 1) continue;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название дохода"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется название дохода " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            string sql = "select * from typedohod";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateProfitTypeDataGri(windowObj);
            windowObj.ProfitTypesDataGrid.SelectedItem = null;
            windowObj.TypeDohDeleteButton.IsEnabled = false;
        }
    }
}
