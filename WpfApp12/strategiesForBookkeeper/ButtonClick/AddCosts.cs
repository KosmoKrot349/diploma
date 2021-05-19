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
            if (windowObj.RashodyAddSum.Text == "" || windowObj.RashodyAddDate.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description)VALUES ((SELECT typeid FROM typerash where title='" + windowObj.RashodyAddType.SelectedItem + "'), (SELECT sotrid FROM sotrudniki where fio='" + windowObj.RashodyAddFIO.SelectedItem + "'), " + windowObj.RashodyAddSum.Text.Replace(',', '.') + ", '" + windowObj.RashodyAddDate.Text.Replace('.', '-') + "', '" + windowObj.RashodyAddDesc.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.RashodyAddType.SelectedIndex = 0;
                windowObj.RashodyAddFIO.SelectedIndex = 0;
                windowObj.RashodyAddSum.Text = "";
                windowObj.RashodyAddDate.Text = DateTime.Now.ToShortDateString();
                windowObj.RashodyAddDesc.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.RoshodyGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateCostsDataGrid(windowObj);
            }

            windowObj.RoshodyDataGrid.SelectedItem = null;
            windowObj.RashDeleteButton.IsEnabled = false;
            windowObj.RashChangeButton.IsEnabled = false;
        }
    }
}
