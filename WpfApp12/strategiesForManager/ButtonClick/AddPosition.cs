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
        DirectorWindow windowObj;

        public AddPosition(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.StateAddPay.Text == "" || windowObj.StateAddTitle.Text == "" || windowObj.days1.Text == "" || windowObj.days2.Text == "" || windowObj.days3.Text == "" || windowObj.days4.Text == "" || windowObj.days5.Text == "" || windowObj.days6.Text == "" || windowObj.days7.Text == "" || windowObj.days8.Text == "" || windowObj.days9.Text == "" || windowObj.days10.Text == "" || windowObj.days11.Text == "" || windowObj.days12.Text == "") { MessageBox.Show("Поля не заполненны"); return; }
            if (Convert.ToInt32(windowObj.days1.Text) > 31 || Convert.ToInt32(windowObj.days2.Text) > 29 || Convert.ToInt32(windowObj.days3.Text) > 31 || Convert.ToInt32(windowObj.days4.Text) > 30 || Convert.ToInt32(windowObj.days5.Text) > 31 || Convert.ToInt32(windowObj.days6.Text) > 30 || Convert.ToInt32(windowObj.days7.Text) > 31 || Convert.ToInt32(windowObj.days8.Text) > 31 || Convert.ToInt32(windowObj.days9.Text) > 30 || Convert.ToInt32(windowObj.days10.Text) > 31 || Convert.ToInt32(windowObj.days11.Text) > 30 || Convert.ToInt32(windowObj.days12.Text) > 31) { MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO states(title, kol_work_day, zp, comment) VALUES ('" + windowObj.StateAddTitle.Text + "', '{" + windowObj.days1.Text + "," + windowObj.days2.Text + "," + windowObj.days3.Text + "," + windowObj.days4.Text + "," + windowObj.days5.Text + "," + windowObj.days6.Text + "," + windowObj.days7.Text + "," + windowObj.days8.Text + "," + windowObj.days9.Text + "," + windowObj.days10.Text + "," + windowObj.days11.Text + "," + windowObj.days12.Text + "}', " + windowObj.StateAddPay.Text.Replace(',', '.') + ", '" + windowObj.StateAddCom.Text + "')";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Должность добавлена.\nПродолжить добавление?", "Продолжение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.days1.Text = "22";
                windowObj.days2.Text = "22";
                windowObj.days3.Text = "22";
                windowObj.days4.Text = "22";
                windowObj.days5.Text = "22";
                windowObj.days6.Text = "22";
                windowObj.days7.Text = "22";
                windowObj.days8.Text = "22";
                windowObj.days9.Text = "22";
                windowObj.days10.Text = "22";
                windowObj.days11.Text = "22";
                windowObj.days12.Text = "22";
                windowObj.StateAddTitle.Text = "";
                windowObj.StateAddPay.Text = "";
                windowObj.StateAddCom.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.StateGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridStates(windowObj.connectionString, windowObj.StateDataGrid);
            }
        }
    }
}
