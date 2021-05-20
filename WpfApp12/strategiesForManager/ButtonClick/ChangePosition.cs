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
            if (windowObj.PositionChangeSalary.Text == "" || windowObj.PositionChangeTitle.Text == "" || windowObj.ChangeWorksDaysIn1Month.Text == "" || windowObj.ChangeWorksDaysIn2Month.Text == "" || windowObj.ChangeWorksDaysIn3Month.Text == "" || windowObj.ChangeWorksDaysIn4Month.Text == "" || windowObj.ChangeWorksDaysIn5Month.Text == "" || windowObj.ChangeWorksDaysIn6Month.Text == "" || windowObj.ChangeWorksDaysIn7Month.Text == "" || windowObj.ChangeWorksDaysIn8Month.Text == "" || windowObj.ChangeWorksDaysIn9Month.Text == "" || windowObj.ChangeWorksDaysIn10Month.Text == "" || windowObj.ChangeWorksDaysIn11Month.Text == "" || windowObj.ChangeWorksDaysIn12Month.Text == "") {MessageBox.Show("Поля не заполнены"); return; }
            if (Convert.ToInt32(windowObj.ChangeWorksDaysIn1Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn2Month.Text) > 29 || Convert.ToInt32(windowObj.ChangeWorksDaysIn3Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn4Month.Text) > 30 || Convert.ToInt32(windowObj.ChangeWorksDaysIn5Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn6Month.Text) > 30 || Convert.ToInt32(windowObj.ChangeWorksDaysIn7Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn8Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn9Month.Text) > 30 || Convert.ToInt32(windowObj.ChangeWorksDaysIn10Month.Text) > 31 || Convert.ToInt32(windowObj.ChangeWorksDaysIn11Month.Text) > 30 || Convert.ToInt32(windowObj.ChangeWorksDaysIn12Month.Text) > 31) { MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "update states set title='" + windowObj.PositionChangeTitle.Text + "', kol_work_day='{" + windowObj.ChangeWorksDaysIn1Month.Text + "," + windowObj.ChangeWorksDaysIn2Month.Text + "," + windowObj.ChangeWorksDaysIn3Month.Text + "," + windowObj.ChangeWorksDaysIn4Month.Text + "," + windowObj.ChangeWorksDaysIn5Month.Text + "," + windowObj.ChangeWorksDaysIn6Month.Text + "," + windowObj.ChangeWorksDaysIn7Month.Text + "," + windowObj.ChangeWorksDaysIn8Month.Text + "," + windowObj.ChangeWorksDaysIn9Month.Text + "," + windowObj.ChangeWorksDaysIn10Month.Text + "," + windowObj.ChangeWorksDaysIn11Month.Text + "," + windowObj.ChangeWorksDaysIn12Month.Text + "}', zp=" + windowObj.PositionChangeSalary.Text.Replace(',', '.') + ",comment='" + windowObj.PositionChangeComment.Text + "' where statesid=" + windowObj.positionID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.PositionGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updatePositionsDataGrid(windowObj);
        }
    }
}
