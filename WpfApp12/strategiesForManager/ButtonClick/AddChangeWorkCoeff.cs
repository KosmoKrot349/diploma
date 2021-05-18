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
            for (int i = 0; i < windowObj.KoefDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = windowObj.KoefDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано количество лет"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан коефициент"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Количество лет повторяется в строке" + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from koef_vislugi";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateDataGridKoef(windowObj.connectionString, windowObj.KoefDataGrid);
            windowObj.KoefDataGrid.SelectedItem = null;
            windowObj.KoefDeleteButton.IsEnabled = false;
        }
    }
}
