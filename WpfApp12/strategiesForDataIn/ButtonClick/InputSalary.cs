using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForDataIn.ButtonClick
{
    class InputSalary:IButtonClick
    {
        DateIn window;

        public InputSalary(DateIn window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (window.PaymentSalary.Text != "" && Convert.ToDouble(window.PaymentSalary.Text) <= window.toPay)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();
                    string sql = "UPDATE nachisl SET  viplacheno=viplacheno+" + window.PaymentSalary.Text.Replace(',', '.') + " WHERE nachid =" + window.AccrualRecordId;
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                    con.Open();
                    string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description) VALUES (1, (select sotrid from nachisl where nachid=" + window.AccrualRecordId + "), " + window.PaymentSalary.Text.Replace(',', '.') + ", now(), 'Выплата зп')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                window.Close();
            }
            else { MessageBox.Show("Указана не верная сумма"); return; }
        }
    }
}
