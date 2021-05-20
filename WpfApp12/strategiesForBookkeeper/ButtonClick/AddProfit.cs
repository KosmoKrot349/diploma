using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class AddProfit:IButtonClick
    {
        BookkeeperWindow windowObj;

        public AddProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (windowObj.ProfitAddSum.Text == "" || windowObj.ProfitAddDate.Text == "" || windowObj.ProfitAddPersonNmae.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ((select idtype from typedohod where title='" + windowObj.ProfitAddType.SelectedItem + "'), " + windowObj.ProfitAddSum.Text.Replace(',', '.') + ", '" + windowObj.ProfitAddDate.Text.Replace('.', '-') + "','" + windowObj.ProfitAddPersonNmae.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.ProfitAddSum.Text = "";
                windowObj.ProfitAddDate.Text = DateTime.Now.ToShortDateString();
                windowObj.ProfitAddType.SelectedIndex = 0;
                windowObj.ProfitAddPerson.SelectedIndex = 0;
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.ProfitGrid.Visibility = Visibility.Visible;

                windowObj.ProfitDataGrid.SelectedItem = null;
                windowObj.DohDeleteButton.IsEnabled = false;
                windowObj.DohChangeButton.IsEnabled = false;
                DataGridUpdater.updateProfitDataGrid(windowObj);
            }
        }
    }
}
