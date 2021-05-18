using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class ChangeCosts:IButtonClick
    {
        BookkeeperWindow windowObj;

        public ChangeCosts(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.RashodyChangeSum.Text == "" || windowObj.RashodyChangeDate.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE rashody SET typeid=(SELECT typeid FROM typerash where title='" + windowObj.RashodyChangeType.SelectedItem + "'), sotrid=(SELECT sotrid FROM sotrudniki where fio='" + windowObj.RashodyChangeFIO.SelectedItem + "'), summ=" + windowObj.RashodyChangeSum.Text.Replace(',', '.') + ", data='" + windowObj.RashodyChangeDate.Text.Replace('.', '-') + "', description='" + windowObj.RashodyChangeDesc.Text + "' WHERE rashid =" + windowObj.costID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.HideAll();
            windowObj.RoshodyGrid.Visibility = Visibility.Visible;

            windowObj.RoshodyDataGrid.SelectedItem = null;
            windowObj.RashDeleteButton.IsEnabled = false;
            windowObj.RashChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridRashody(windowObj.connectionString, windowObj.filter.sql, windowObj.RoshodyDataGrid);
        }
    }
}
