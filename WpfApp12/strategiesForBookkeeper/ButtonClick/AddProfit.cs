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
            if (windowObj.DohodyAddSum.Text == "" || windowObj.DohodyAddDate.Text == "" || windowObj.dohAddKtoVnesTb.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ((select idtype from typedohod where title='" + windowObj.DohodyAddType.SelectedItem + "'), " + windowObj.DohodyAddSum.Text.Replace(',', '.') + ", '" + windowObj.DohodyAddDate.Text.Replace('.', '-') + "','" + windowObj.dohAddKtoVnesTb.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                windowObj.DohodyAddSum.Text = "";
                windowObj.DohodyAddDate.Text = DateTime.Now.ToShortDateString();
                windowObj.DohodyAddType.SelectedIndex = 0;
                windowObj.dohAddKtoVnesCm.SelectedIndex = 0;
            }
            if (res == MessageBoxResult.No)
            {
                windowObj.HideAll();
                windowObj.DohodyrGrid.Visibility = Visibility.Visible;

                windowObj.DohodyDataGrid.SelectedItem = null;
                windowObj.DohDeleteButton.IsEnabled = false;
                windowObj.DohChangeButton.IsEnabled = false;
                DataGridUpdater.updateDataGridDohody(windowObj.connectionString, windowObj.filter.sql, windowObj.DohodyDataGrid);
            }
        }
    }
}
