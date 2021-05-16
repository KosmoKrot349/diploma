using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для DateIn.xaml
    /// </summary>
    public partial class DateIn : Window
    {
       private DateTime dm = new DateTime();

        public int zapid = -1;
        public double topay=0;

        public string constr;
        public DateIn()
        {
            InitializeComponent();
        }
        public DateTime getDm()
        {
            return dm;
        }

        //дата для расписания
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePick.Text == "") { MessageBox.Show("Дата не выбрана");  return;  }
            if (Convert.ToDateTime(datePick.Text).DayOfWeek.ToString() != "Monday") { MessageBox.Show("Дата не понедельник"); return; }
            dm = Convert.ToDateTime(datePick.Text);
            this.Close();

        }
        //дата для остановки расчёта 
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (datePick2.Text == "") { MessageBox.Show("Дата не выбрана"); return; }
            dm = Convert.ToDateTime(datePick2.Text);
            this.Close();
        }
        //размер пени
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            double proc = 0;
            if (PenyaProc.Text == "") { proc = 0; }
            else { proc = Convert.ToDouble(PenyaProc.Text); }

            if (proc > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }

            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(constr);
                conn.Open();
                string sql = "UPDATE last_pereraschet SET penyaproc="+proc.ToString().Replace(',','.');
                NpgsqlCommand com = new NpgsqlCommand(sql,conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
            if (ZpViplata.Text != "" && Convert.ToDouble(ZpViplata.Text) <= topay)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(constr);
                    con.Open();
                    string sql = "UPDATE nachisl SET  viplacheno=viplacheno+" + ZpViplata.Text.Replace(',', '.') + " WHERE nachid =" + zapid;
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(constr);
                    con.Open();
                    string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description) VALUES (1, (select sotrid from nachisl where nachid="+zapid+"), "+ ZpViplata.Text.Replace(',', '.') + ", now(), 'Выплата зп')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                this.Close();
            }
            else { System.Windows.Forms.MessageBox.Show("Указана не верная сумма");return; }
        }
    }
}
