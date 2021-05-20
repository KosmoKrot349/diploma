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
            if (windowObj.CostsChangeSum.Text == "" || windowObj.CostsChangeDate.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE rashody SET typeid=(SELECT typeid FROM typerash where title='" + windowObj.CostsChangeType.SelectedItem + "'), sotrid=(SELECT sotrid FROM sotrudniki where fio='" + windowObj.CostsChangePersonName.SelectedItem + "'), summ=" + windowObj.CostsChangeSum.Text.Replace(',', '.') + ", data='" + windowObj.CostsChangeDate.Text.Replace('.', '-') + "', description='" + windowObj.CostsChangeComment.Text + "' WHERE rashid =" + windowObj.costID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            windowObj.HideAll();
            windowObj.CostsGrid.Visibility = Visibility.Visible;

            windowObj.CostsDataGrid.SelectedItem = null;
            windowObj.RashDeleteButton.IsEnabled = false;
            windowObj.RashChangeButton.IsEnabled = false;
            DataGridUpdater.updateCostsDataGrid(windowObj);
        }
    }
}
