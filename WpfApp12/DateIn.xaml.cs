using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Npgsql;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для DateIn.xaml
    /// </summary>
    public partial class DateIn : Window
    {
       private DateTime dateMonday = new DateTime();

        public int AccrualRecordId = -1;
        public double toPay=0;

        public string connectionString;
        public DateIn()
        {
            InitializeComponent();
        }
        public DateTime getDm()
        {
            return dateMonday;
        }

        //дата для расписания
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePick.Text == "") { MessageBox.Show("Дата не выбрана");  return;  }
            if (Convert.ToDateTime(datePick.Text).DayOfWeek.ToString() != "Monday") { MessageBox.Show("Дата не понедельник"); return; }
            dateMonday = Convert.ToDateTime(datePick.Text);
            this.Close();

        }
        //дата для остановки расчёта 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectDateToPay.Text == "") { MessageBox.Show("Дата не выбрана"); return; }
            dateMonday = Convert.ToDateTime(SelectDateToPay.Text);
            this.Close();
        }
        //размер пени
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double percent = 0;
            if (FinePrecent.Text == "") { percent = 0; }
            else { percent = Convert.ToDouble(FinePrecent.Text); }

            if (percent > 100) { MessageBox.Show("Процент не может быть больше 100"); return; }

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();
                string sql = "UPDATE last_pereraschet SET penyaproc="+percent.ToString().Replace(',','.');
                NpgsqlCommand com = new NpgsqlCommand(sql,conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
          MessageBox.Show("Пеня сохранена");
            this.Close();

        }
        //ввод только цифр
        private void PenyaProc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            {  e.Handled = true; }
        }
        //выплата зп
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (PaymentSalary.Text != "" && Convert.ToDouble(PaymentSalary.Text) <= toPay)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "UPDATE nachisl SET  viplacheno=viplacheno+" + PaymentSalary.Text.Replace(',', '.') + " WHERE nachid =" + AccrualRecordId;
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description) VALUES (1, (select sotrid from nachisl where nachid="+AccrualRecordId+"), "+ PaymentSalary.Text.Replace(',', '.') + ", now(), 'Выплата зп')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                this.Close();
            }
            else { MessageBox.Show("Указана не верная сумма");return; }
        }
    }
}
