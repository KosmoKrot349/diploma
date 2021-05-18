using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp12.strategiesForBookkeeper.OtherMethods;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class LearnStop:IButtonClick
    {
        BookkeeperWindow windowObj;

        public LearnStop(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DateIn wind = new DateIn();
            wind.gridToPay.Visibility = Visibility.Visible;
            wind.ShowDialog();
            DateTime dm = wind.getDm();
            if (dm.Day == 1 && dm.Month == 1 && dm.Year == 1) { return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE listnuch SET date_stop='" + dm.ToString().Replace('.', '-') + "' WHERE listenerid = (select listenerid from listeners where fio='" + windowObj.Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + windowObj.Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBox.Show("Запись успешно остановлена");


            updateDefraymentTable.Update(windowObj,1);
        }
    }
}
