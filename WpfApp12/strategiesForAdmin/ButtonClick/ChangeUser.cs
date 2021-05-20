using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForAdmin
{
    class ChangeUser : IButtonClick
    {
        private AdminWindow windowObj;

        public ChangeUser(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            if (windowObj.uCFio.Text == "" || windowObj.uCpas.Text == "" || windowObj.uClog.Text == "" || (windowObj.uCadm.IsChecked == false && windowObj.uCbh.IsChecked == false && windowObj.uCdr.IsChecked == false)) { MessageBox.Show("Такие изменения не возможны"); return; }
            try
            {

                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(windowObj.connectionString);
                npgSqlConnection.Open();
                string sql = "update users set log = '" + windowObj.uClog.Text + "', fio = '" + windowObj.uCFio.Text + "', pas = '" + windowObj.uCpas.Text + "', admin = '" + windowObj.adminRole + "', buhgalter = '" + windowObj.bookkeeperRole + "', director = '" + windowObj.managerRole + "' where uid=" + windowObj.userID;
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
                MessageBox.Show("Пользователь изменен");

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.hideAll();
            windowObj.UsersGrid.Visibility = Visibility.Visible;
            windowObj.usersDGrid.SelectedItem = null;
            windowObj.changeUser.IsEnabled = false;
            windowObj.dellUser.IsEnabled = false;
            DataGridUpdater.updateUsersDataGrid(windowObj);
        }
    }
}
