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
    class AddChangeWorkCoeff:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeWorkCoeff(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("koefid", System.Type.GetType("System.Int32"));
            table.Columns.Add("kol_year", System.Type.GetType("System.Int32"));
            table.Columns.Add("koef", System.Type.GetType("System.Double"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.WorkCoeffDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = windowObj.WorkCoeffDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано количество лет"); return; }
                if (recordArr[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан коефициент"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Количество лет повторяется в строке" + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            string sql = "select * from koef_vislugi";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateWorkCoeffDataGrid(windowObj);
            windowObj.WorkCoeffDataGrid.SelectedItem = null;
            windowObj.DeleteWorkCoeff.IsEnabled = false;
        }
    }
}
