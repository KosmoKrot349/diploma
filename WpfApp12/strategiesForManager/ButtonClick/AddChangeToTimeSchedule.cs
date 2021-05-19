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
    class AddChangeToTimeSchedule:IButtonClick
    {
        ManagerWindow windowObj;

        public AddChangeToTimeSchedule(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", System.Type.GetType("System.Int32"));
            table.Columns.Add("lesson_number", System.Type.GetType("System.Int32"));
            table.Columns.Add("time_start", System.Type.GetType("System.TimeSpan"));
            table.Columns.Add("time_end", System.Type.GetType("System.TimeSpan"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < windowObj.zvonkiDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = windowObj.zvonkiDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] recordArr = row.ItemArray;
                if (recordArr[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан номер занятия"); return; }
                if (recordArr[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано время начала занятия"); return; }
                if (recordArr[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано время конца занятия"); return; }
                if (list.IndexOf(recordArr[1]) != -1) { MessageBox.Show("Повторяется номер занятия " + recordArr[1]); return; }
                list.Add(recordArr[1]);
                table.ImportRow(row);
            }
            list.Sort();
            if (list.Count != 0) { if (Convert.ToInt32(list[0]) != 1) { MessageBox.Show("Занятия пронумерованы не верно"); return; } }
            for (int i = 0; i < list.Count; i++)
            {
                if (i != Convert.ToInt32(list[i]) - 1)
                {
                    MessageBox.Show("Занятия пронумерованы не верно"); return;
                }


            }
            string sql = "select * from lessons_time";
            NpgsqlConnection conccetion = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);

            windowObj.zvonkiDataGrid.SelectedItem = null;
            //расписание звонков
            windowObj.zvonkiDeleteButton.IsEnabled = false;
            DataGridUpdater.updateTimeScheduleDataGrid(windowObj);
        }
    }
}
