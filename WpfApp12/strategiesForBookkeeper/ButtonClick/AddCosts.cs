using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class AddCosts:IButtonClick
    {
        BookkeeperWindow windowObj;

        public AddCosts(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.CostsAddSum.Text == "" || windowObj.CostsAddDate.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description)VALUES ((SELECT typeid FROM typerash where title='" + windowObj.CostsAddType.SelectedItem + "'), (SELECT sotrid FROM sotrudniki where fio='" + windowObj.CostsAddPersonName.SelectedItem + "'), " + windowObj.CostsAddSum.Text.Replace(',', '.') + ", '" + windowObj.CostsAddDate.Text.Replace('.', '-') + "', '" + windowObj.CostsAddDesc.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.CostsAddType.SelectedIndex = 0;
                windowObj.CostsAddPersonName.SelectedIndex = 0;
                windowObj.CostsAddSum.Text = "";
                windowObj.CostsAddDate.Text = DateTime.Now.ToShortDateString();
                windowObj.CostsAddDesc.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.CostsGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateCostsDataGrid(windowObj);
            }

            windowObj.CostsDataGrid.SelectedItem = null;
            windowObj.DeleteCosts.IsEnabled = false;
            windowObj.GoToAddCosts.IsEnabled = false;
        }
    }
}
