using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class DeleteProfit:IButtonClick
    {
        BookkeeperWindow windowObj;

        public DeleteProfit(BookkeeperWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.ProfitDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите удалить этот доход?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "DELETE FROM dodhody WHERE dohid=" + arr[0];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                windowObj.ProfitDataGrid.SelectedItem = null;
                windowObj.DeleteProfit.IsEnabled = false;
                windowObj.GoToChangeProfit.IsEnabled = false;
                DataGridUpdater.updateProfitDataGrid(windowObj);
            }
        }
    }
}
