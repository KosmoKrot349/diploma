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
    class AddChangeEmployee:IButtonClick
    {
        ManagerWindow windoObj;

        public AddChangeEmployee(ManagerWindow windoObj)
        {
            this.windoObj = windoObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("sotrid", System.Type.GetType("System.Int32"));
            table.Columns.Add("fio", System.Type.GetType("System.String"));
            table.Columns.Add("phone", System.Type.GetType("System.String"));
            table.Columns.Add("num_trud", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));

            ArrayList list = new ArrayList();
            for (int i = 0; i < windoObj.EmployeesDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = windoObj.EmployeesDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "" || recordArr[2].ToString() == "" || recordArr[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано одно из обязательных значений"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется имя сотрудника " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);

            }

            string sql = "select * from sotrudniki";
            NpgsqlConnection conccetion = new NpgsqlConnection(windoObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);

            windoObj.EmployeesDataGrid.SelectedItem = null;
            //все сотрудники
            windoObj.EmployeeDelet.IsEnabled = false;
            windoObj.GoToEmployeeToTeacher.IsEnabled = false;
            windoObj.GoToEmployeeToStuff.IsEnabled = false;
            DataGridUpdater.updateEmploeesDataGrid(windoObj);
        }
    }
}
