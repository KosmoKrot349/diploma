using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangePosition:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToChangePosition(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.StateDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.stateID = (int)arr[0];
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select array_to_string(kol_work_day,'_') from states where statesid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] ss = reader.GetString(0).Split('_');
                        windowObj.Chanedays1.Text = ss[0]; windowObj.Chanedays2.Text = ss[1]; windowObj.Chanedays3.Text = ss[2]; windowObj.Chanedays4.Text = ss[3]; windowObj.Chanedays5.Text = ss[4]; windowObj.Chanedays6.Text = ss[5]; windowObj.Chanedays7.Text = ss[6]; windowObj.Chanedays8.Text = ss[7]; windowObj.Chanedays9.Text = ss[8]; windowObj.Chanedays10.Text = ss[9]; windowObj.Chanedays11.Text = ss[10]; windowObj.Chanedays12.Text = ss[11];
                    }

                }
                con.Close();
                windowObj.StateChaneTitle.Text = arr[1].ToString();
                windowObj.StateChanePay.Text = arr[3].ToString();
                windowObj.StateChaneCom.Text = arr[4].ToString();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.StateChaneGrid.Visibility = Visibility.Visible;
        }
    }
}
