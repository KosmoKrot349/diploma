using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class SaveRootPassword : IButtonClick
    {
        private AdminWindow windowObj;

        public SaveRootPassword(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void buttonClick()
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(windowObj.connectionString);
                conn.Open();
                string sql = "UPDATE users SET pas = '" + windowObj.rootPassSettings.Text + "' where uid = -1";
                NpgsqlCommand com = new NpgsqlCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
