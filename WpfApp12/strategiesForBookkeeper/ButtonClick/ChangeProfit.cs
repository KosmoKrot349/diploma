using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class ChangeProfit : IButtonClick
    {
        BookkeeperWindow windowObj;

        public ChangeProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.ProfitChangeSumm.Text == "" || windowObj.ProfitChangeDate.Text == "" || windowObj.ProfitChangePersonName.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE dodhody SET idtype=(select idtype from typedohod where title='" + windowObj.ProfitChangeType.SelectedItem + "'), sum=" + windowObj.ProfitChangeSumm.Text.Replace(',', '.') + ", data='" + windowObj.ProfitChangeDate.Text.Replace('.', '-') + "',fio='" + windowObj.ProfitChangePersonName.Text + "' WHERE dohid= " + windowObj.profitID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.ProfitGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateProfitDataGrid(windowObj);
        }
    }
}
