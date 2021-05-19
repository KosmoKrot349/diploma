using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangePosition:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangePosition(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.StateChanePay.Text == "" || windowObj.StateChaneTitle.Text == "" || windowObj.Chanedays1.Text == "" || windowObj.Chanedays2.Text == "" || windowObj.Chanedays3.Text == "" || windowObj.Chanedays4.Text == "" || windowObj.Chanedays5.Text == "" || windowObj.Chanedays6.Text == "" || windowObj.Chanedays7.Text == "" || windowObj.Chanedays8.Text == "" || windowObj.Chanedays9.Text == "" || windowObj.Chanedays10.Text == "" || windowObj.Chanedays11.Text == "" || windowObj.Chanedays12.Text == "") {MessageBox.Show("Поля не заполнены"); return; }
            if (Convert.ToInt32(windowObj.Chanedays1.Text) > 31 || Convert.ToInt32(windowObj.Chanedays2.Text) > 29 || Convert.ToInt32(windowObj.Chanedays3.Text) > 31 || Convert.ToInt32(windowObj.Chanedays4.Text) > 30 || Convert.ToInt32(windowObj.Chanedays5.Text) > 31 || Convert.ToInt32(windowObj.Chanedays6.Text) > 30 || Convert.ToInt32(windowObj.Chanedays7.Text) > 31 || Convert.ToInt32(windowObj.Chanedays8.Text) > 31 || Convert.ToInt32(windowObj.Chanedays9.Text) > 30 || Convert.ToInt32(windowObj.Chanedays10.Text) > 31 || Convert.ToInt32(windowObj.Chanedays11.Text) > 30 || Convert.ToInt32(windowObj.Chanedays12.Text) > 31) { MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "update states set title='" + windowObj.StateChaneTitle.Text + "', kol_work_day='{" + windowObj.Chanedays1.Text + "," + windowObj.Chanedays2.Text + "," + windowObj.Chanedays3.Text + "," + windowObj.Chanedays4.Text + "," + windowObj.Chanedays5.Text + "," + windowObj.Chanedays6.Text + "," + windowObj.Chanedays7.Text + "," + windowObj.Chanedays8.Text + "," + windowObj.Chanedays9.Text + "," + windowObj.Chanedays10.Text + "," + windowObj.Chanedays11.Text + "," + windowObj.Chanedays12.Text + "}', zp=" + windowObj.StateChanePay.Text.Replace(',', '.') + ",comment='" + windowObj.StateChaneCom.Text + "' where statesid=" + windowObj.positionID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.StateGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updatePositionsDataGrid(windowObj);
        }
    }
}
