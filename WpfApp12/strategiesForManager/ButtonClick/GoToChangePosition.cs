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
        ManagerWindow windowObj;

        public GoToChangePosition(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.PositionsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.positionID = (int)arr[0];
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
                        windowObj.ChangeWorksDaysIn1Month.Text = ss[0]; windowObj.ChangeWorksDaysIn2Month.Text = ss[1]; windowObj.ChangeWorksDaysIn3Month.Text = ss[2]; windowObj.ChangeWorksDaysIn4Month.Text = ss[3]; windowObj.ChangeWorksDaysIn5Month.Text = ss[4]; windowObj.ChangeWorksDaysIn6Month.Text = ss[5]; windowObj.ChangeWorksDaysIn7Month.Text = ss[6]; windowObj.ChangeWorksDaysIn8Month.Text = ss[7]; windowObj.ChangeWorksDaysIn9Month.Text = ss[8]; windowObj.ChangeWorksDaysIn10Month.Text = ss[9]; windowObj.ChangeWorksDaysIn11Month.Text = ss[10]; windowObj.ChangeWorksDaysIn12Month.Text = ss[11];
                    }

                }
                con.Close();
                windowObj.PositionChangeTitle.Text = arr[1].ToString();
                windowObj.PositionChangeSalary.Text = arr[3].ToString();
                windowObj.PositionChangeComment.Text = arr[4].ToString();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.PositionsChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
