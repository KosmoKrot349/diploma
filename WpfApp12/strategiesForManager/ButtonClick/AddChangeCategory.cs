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
    class AddChangeCategory:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeCategory(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.kategDataGrid.Items.Count - 1; i++)
            {
                DataTable table = new DataTable();
                table.Columns.Add("kategid", System.Type.GetType("System.Int32"));
                table.Columns.Add("title", System.Type.GetType("System.String"));
                table.Columns.Add("pay", System.Type.GetType("System.Double"));

                DataRowView DRV = windowObj.kategDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название категории"); return; }
                if (recordArr[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указана оплата"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется название категории " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);

                string sql = "select * from kategorii";
                NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
                NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
                NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
                adapter.Update(table);
            }

            windowObj.kategDataGrid.SelectedItem = null;

            //категории
            windowObj.kategDeleteButton.IsEnabled = false;

            DataGridUpdater.updateCategoriesDataGrid(windowObj);
        }
    }
}
