using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class AddPosition:IButtonClick
    {
        ManagerWindow windowObj;

        public AddPosition(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.PositionAddSalary.Text == "" || windowObj.PositionAddTitle.Text == "" || windowObj.AddWorksDay1Month.Text == "" || windowObj.AddWorksDay2Month.Text == "" || windowObj.AddWorksDay3Month.Text == "" || windowObj.AddWorksDay4Month.Text == "" || windowObj.AddWorksDay5Month.Text == "" || windowObj.AddWorksDay6Month.Text == "" || windowObj.AddWorksDay7Month.Text == "" || windowObj.AddWorksDay8Month.Text == "" || windowObj.AddWorksDay9Month.Text == "" || windowObj.AddWorksDay10Month.Text == "" || windowObj.AddWorksDay11Month.Text == "" || windowObj.AddWorksDay12Month.Text == "") { MessageBox.Show("Поля не заполненны"); return; }
            if (Convert.ToInt32(windowObj.AddWorksDay1Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay2Month.Text) > 29 || Convert.ToInt32(windowObj.AddWorksDay3Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay4Month.Text) > 30 || Convert.ToInt32(windowObj.AddWorksDay5Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay6Month.Text) > 30 || Convert.ToInt32(windowObj.AddWorksDay7Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay8Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay9Month.Text) > 30 || Convert.ToInt32(windowObj.AddWorksDay10Month.Text) > 31 || Convert.ToInt32(windowObj.AddWorksDay11Month.Text) > 30 || Convert.ToInt32(windowObj.AddWorksDay12Month.Text) > 31) { MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO states(title, kol_work_day, zp, comment) VALUES ('" + windowObj.PositionAddTitle.Text + "', '{" + windowObj.AddWorksDay1Month.Text + "," + windowObj.AddWorksDay2Month.Text + "," + windowObj.AddWorksDay3Month.Text + "," + windowObj.AddWorksDay4Month.Text + "," + windowObj.AddWorksDay5Month.Text + "," + windowObj.AddWorksDay6Month.Text + "," + windowObj.AddWorksDay7Month.Text + "," + windowObj.AddWorksDay8Month.Text + "," + windowObj.AddWorksDay9Month.Text + "," + windowObj.AddWorksDay10Month.Text + "," + windowObj.AddWorksDay11Month.Text + "," + windowObj.AddWorksDay12Month.Text + "}', " + windowObj.PositionAddSalary.Text.Replace(',', '.') + ", '" + windowObj.PositionAddComment.Text + "')";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Должность добавлена.\nПродолжить добавление?", "Продолжение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.AddWorksDay1Month.Text = "22";
                windowObj.AddWorksDay2Month.Text = "22";
                windowObj.AddWorksDay3Month.Text = "22";
                windowObj.AddWorksDay4Month.Text = "22";
                windowObj.AddWorksDay5Month.Text = "22";
                windowObj.AddWorksDay6Month.Text = "22";
                windowObj.AddWorksDay7Month.Text = "22";
                windowObj.AddWorksDay8Month.Text = "22";
                windowObj.AddWorksDay9Month.Text = "22";
                windowObj.AddWorksDay10Month.Text = "22";
                windowObj.AddWorksDay11Month.Text = "22";
                windowObj.AddWorksDay12Month.Text = "22";
                windowObj.PositionAddTitle.Text = "";
                windowObj.PositionAddSalary.Text = "";
                windowObj.PositionAddComment.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.PositionGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updatePositionsDataGrid(windowObj);
            }
        }
    }
}
