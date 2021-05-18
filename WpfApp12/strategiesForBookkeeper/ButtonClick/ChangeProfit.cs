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
            if (windowObj.DohodyChangeSum.Text == "" || windowObj.DohodyChangeDate.Text == "" || windowObj.dohChKtoVnesTb.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE dodhody SET idtype=(select idtype from typedohod where title='" + windowObj.DohodyChangeType.SelectedItem + "'), sum=" + windowObj.DohodyChangeSum.Text.Replace(',', '.') + ", data='" + windowObj.DohodyChangeDate.Text.Replace('.', '-') + "',fio='" + windowObj.dohChKtoVnesTb.Text + "' WHERE dohid= " + windowObj.profitID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.DohodyrGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridDohody(windowObj.connectionString, windowObj.filter.sql, windowObj.DohodyDataGrid);
        }
    }
}
