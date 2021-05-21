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
    class AddChangeSubject:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeSubject(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("subid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("commetn", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.SubjectsDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = windowObj.SubjectsDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название предмета"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется название предмета " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            string sql = "select * from subject";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            windowObj.SubjectsDataGrid.SelectedItem = null;

            //предметы
            windowObj.DeleteSubject.IsEnabled = false;


            DataGridUpdater.updateSubjectDataGrid(windowObj);
        }
    }
}
