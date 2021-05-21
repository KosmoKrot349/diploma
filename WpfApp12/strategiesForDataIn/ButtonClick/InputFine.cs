using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForDataIn.ButtonClick
{
    class InputFine:IButtonClick
    {
        DateIn window;

        public InputFine(DateIn window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            double percent = 0;
            if (window.FinePrecent.Text == "") { percent = 0; }
            else { percent = Convert.ToDouble(window.FinePrecent.Text); }

            if (percent > 100) { MessageBox.Show("Процент не может быть больше 100"); return; }

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(window.connectionString);
                conn.Open();
                string sql = "UPDATE last_pereraschet SET penyaproc=" + percent.ToString().Replace(',', '.');
                NpgsqlCommand com = new NpgsqlCommand(sql, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBox.Show("Пеня сохранена");
            window.Close();
        }
    }
}
