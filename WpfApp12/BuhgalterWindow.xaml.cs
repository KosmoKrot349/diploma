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
using System.IO;
using Npgsql;
using System.Data;
using System.Collections;
using Microsoft;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для BuhgalterWindow.xaml
    /// </summary>
    public partial class BuhgalterWindow : Window
    {
        public int logUser;
        public string FIO = "";
        TextBox[] masTbx;
        TextBox[] masTbx2;
        //строка подключения
        string connectionString = "";

        int DohodID = -1;
        int RashodID = 1;

        //начисления
        DateTime dateNuch;
        CheckBox[] ChbxMas_SotrNuch;
        bool selected = false;


        filtr filtr = new filtr();
        filtr fda = new filtr();
        filtr fdb = new filtr();

        filtr fra = new filtr();
        filtr frb = new filtr();
        string strrr = "";


        //+
        public BuhgalterWindow()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader(@"setting.txt");
            ArrayList ls = new ArrayList();
            while (!reader.EndOfStream)
            {
                ls.Add(reader.ReadLine());
            }
            object[] mas = ls.ToArray();
            connectionString = "Server=" + mas[0].ToString().Split(':')[1] + ";Port=" + mas[2].ToString().Split(':')[1] + ";User Id=postgres;Password=" + mas[1].ToString().Split(':')[1] + ";Database=db";
            filtr.connectionString = connectionString;
            fda.connectionString = connectionString;
            fra.connectionString = connectionString;
            fdb.connectionString = connectionString;
            frb.connectionString = connectionString;
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;

        }
        //+
        public void HideAll()
        {

            NachDataGrid.SelectedItem = null;
            RoshodyDataGrid.SelectedItem = null;
            DohodyDataGrid.SelectedItem = null;

            helloGrdi.Visibility = Visibility.Collapsed;
            OplataGrid.Visibility = Visibility.Collapsed;
            DohodyrGrid.Visibility = Visibility.Collapsed;
            DohodyrAddGrid.Visibility = Visibility.Collapsed;
            DohodyChangeGrid.Visibility = Visibility.Collapsed;
            RoshodyGrid.Visibility = Visibility.Collapsed;
            RashodyAddGrid.Visibility = Visibility.Collapsed;
            RashodyChangeGrid.Visibility = Visibility.Collapsed;
            NalogiGrid.Visibility = Visibility.Collapsed;
            GlNachGrid.Visibility = Visibility.Collapsed;
            DolgGrid.Visibility = Visibility.Collapsed;
            NoDolgGrdi.Visibility = Visibility.Collapsed;
            kassaGrid.Visibility = Visibility.Collapsed;
            StatisticaGrid.Visibility = Visibility.Collapsed;
            ZpOthcetGrid.Visibility = Visibility.Collapsed;

            //начисления
            ViplataBut.IsEnabled = false;

            //расходы
            RashDeleteButton.IsEnabled = false;
            RashChangeButton.IsEnabled = false;

            //доходы
            DohDeleteButton.IsEnabled = false;
            DohChangeButton.IsEnabled = false;
        }
        //+
        public void updateOplataTable(int a)
        {
            if (a == 1)
            {
                int kol_Month = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payformonth = reader.GetString(0);
                            string[] payformonthMas = payformonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payformonthMas[i] != "0")
                                {

                                    kol_Month++;

                                }
                            }
                            masTbx = new TextBox[kol_Month];
                            for (int i = 0; i < kol_Month; i++)
                            {
                                masTbx[i] = new TextBox();
                                masTbx[i].PreviewTextInput += TextBox_PreviewTextInput;
                            }
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridOpat(connectionString, MonthOplGrid, Groups, Listener, masTbx, isClose, isStop, Closeing, Open, StopLern, RestartLern);
            }


            if (a == 2)
            {
                
                int kol_Month = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + GroupsDolg.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payformonth = reader.GetString(0);
                            string[] payformonthMas = payformonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payformonthMas[i] != "0")
                                {

                                    kol_Month++;

                                }
                            }
                            masTbx2 = new TextBox[kol_Month];
                            for (int i = 0; i < kol_Month; i++)
                            {
                                masTbx2[i] = new TextBox();
                                masTbx2[i].PreviewTextInput += TextBox_PreviewTextInput;
                            }
                        }
                    }
                con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridDolg(connectionString, MonthOplGridDolg, GroupsDolg, ListenerDolg, masTbx2, DataPerehoda, isStopDolg);
            }
        }
        //переход из меню бухаглтера в меню админа +
        private void AdminRoleB_Click(object sender, RoutedEventArgs e)
        {
            int a = 0;
            if (logUser != -1) a = Checker.adminCheck(logUser, connectionString);

            if (a == 1 || logUser == -1)
            {
                AdminWindow wind = new AdminWindow();
                try
                {
                    NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                    string sql = "select admin,buhgalter,director from users where uid = " + logUser;
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader dReader = command.ExecuteReader();
                    if (dReader.HasRows)
                    {
                        while (dReader.Read())
                        {
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleA.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleA.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleA.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                wind.logUser = logUser;
                wind.FIO = FIO;
                wind.Title = FIO + " - Админ";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль администратор. Для начала роботы выберите один из пунктов меню.";
                wind.Width = this.Width;
                wind.Height = this.Height;
                wind.Left = this.Left;
                wind.Top = this.Top;
                wind.Show();
                this.Close();
            }
            else { MessageBox.Show("Вы не имете доступа к этой роли"); }
        }
        //переход из меню бухаглтера в меню бухглатера +
        private void BuhgRoleB_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже выбрали роль бухгалтера");
        }
        //переход из меню бухаглтера в меню директора + 
        private void DirectorRoleB_Click(object sender, RoutedEventArgs e)
        {
            int d = 0;
            if (logUser != -1) d = Checker.dirCheck(logUser, connectionString);

            if (d == 1 || logUser == -1)
            {
                DirectorWindow wind = new DirectorWindow();
                try
                {
                    NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                    string sql = "select admin,buhgalter,director from users where uid = " + logUser;
                    connection.Open();
                    NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                    NpgsqlDataReader dReader = command.ExecuteReader();
                    if (dReader.HasRows)
                    {
                        while (dReader.Read())
                        {
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleD.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleD.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleD.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                wind.logUser = logUser;
                wind.FIO = FIO;
                wind.Title = FIO + " - Директор";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль директор. Для начала роботы выберите один из пунктов меню.";
                wind.Width = this.Width;
                wind.Height = this.Height;
                wind.Left = this.Left;
                wind.Top = this.Top;
                wind.Show();
                this.Close();
            }
            else { MessageBox.Show("Вы не имете доступа к этой роли"); }
        }
        //выход из пользователя+
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            logUser = -2;
            MainWindow wind = new MainWindow();
            wind.log.Text = "";
            wind.pas.Password = "";
            wind.Width = this.Width;
            wind.Height = this.Height;
            wind.Left = this.Left;
            wind.Top = this.Top;
            wind.Show();
            this.Close();
        }
        
        //переход к оплате слушателей+
        private void OplataSlysh_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            OplataGrid.Visibility = Visibility.Visible;
            Groups.Items.Clear();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie from groups where grid in (select distinct grid from listnuch) order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Groups.Items.Add(reader.GetString(0));
                    }
                    Groups.SelectedIndex = 0;
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //изменение группы выбор слушталей +
        private void Groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
       
 


            if (cmb.Name == "dohAddKtoVnesCmF")
            {

                if (cmb.SelectedIndex ==1)
                {
                    dohAddKtoVnesCm.Items.Clear();
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select fio from listeners";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dohAddKtoVnesCm.Items.Add(reader.GetString(0));
                        }
                        dohAddKtoVnesCm.SelectedIndex = 0;
                    }
                    con.Close();
                    }  
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
                    if (cmb.SelectedIndex == 0)
                    {
                    dohAddKtoVnesCm.Items.Clear();
                    try
                    {

                        NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                        con3.Open();
                        string sql = "select fio from sotrudniki";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con3);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                dohAddKtoVnesCm.Items.Add(reader.GetString(0));
                            }
                            dohAddKtoVnesCm.SelectedIndex = 0;
                        }
                        con3.Close();

                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
                if (cmb.SelectedIndex == 2) { dohAddKtoVnesCm.Items.Clear(); dohAddKtoVnesCm.Items.Add("Нет в списке"); }
                    dohAddKtoVnesCm.SelectedIndex = 0;

                
            }

            if (cmb.Name == "dohChKtoVnesCmF")
            {

                if (cmb.SelectedIndex == 1)
                {
                    dohChKtoVnesCm.Items.Clear();
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(connectionString);
                        con.Open();
                        string sql = "select fio from listeners";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dohChKtoVnesCm.SelectedIndex = 0;
                            int i = 0;
                            while (reader.Read())
                            {
                                dohChKtoVnesCm.Items.Add(reader.GetString(0));
                               
                                if (reader.GetString(0) == strrr) { System.Windows.Forms.MessageBox.Show(""+i); dohChKtoVnesCm.SelectedIndex = i; }
                                i++;
                            }
                        }
                        con.Close();
                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
                if (cmb.SelectedIndex == 0)
                {
                    dohChKtoVnesCm.Items.Clear();
                    try
                    {

                        NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                        con3.Open();
                        string sql = "select fio from sotrudniki";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con3);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        if (reader.HasRows)
                        {
                            dohChKtoVnesCm.SelectedIndex = 0;
                            int i = 0;
                            while (reader.Read())
                            {
                                dohChKtoVnesCm.Items.Add(reader.GetString(0));
                                if (reader.GetString(0) == strrr) { dohChKtoVnesCm.SelectedIndex = i; }
                                i++;
                            }
                        }
                        con3.Close();

                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
                if (cmb.SelectedIndex == 2) {  dohChKtoVnesCm.Items.Clear(); dohChKtoVnesCm.SelectedIndex = 0; dohChKtoVnesCm.Items.Add("Нет в списке");  }
                


            }


            if (cmb.Name== "Groups")
            { 
            Listener.Items.Clear();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select distinct fio from listeners where grid @> ARRAY[(select grid from groups where nazvanie = '" + Groups.SelectedItem + "')] order by fio";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        Listener.Items.Add(reader1.GetString(0));
                    }
                    Listener.SelectedIndex = 0;
                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }


            if (cmb.Name == "GroupsDolg")
            {
                ListenerDolg.Items.Clear();
                try
                {
                    NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                    con1.Open();
                    string sql1 = "select distinct fio from listeners where listenerid in (select listenerid from listdolg inner join groups using(grid) where nazvanie ='"+ GroupsDolg.SelectedItem + "') order by fio";
                    NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                    NpgsqlDataReader reader1 = com1.ExecuteReader();
                    if (reader1.HasRows)
                    {
                        while (reader1.Read())
                        {
                            ListenerDolg.Items.Add(reader1.GetString(0));
                        }
                        ListenerDolg.SelectedIndex = 0;
                    }
                    con1.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

        }
        //выбор слушателя +
        private void Listener_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if(cmb.Name== "Listener")
            updateOplataTable(1);
            if (cmb.Name == "ListenerDolg")
            updateOplataTable(2);

        }
        //закрытие запими об оплате +
        private void Closeing_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите остановить выплату для этой записи и сделать ее неактивной?", "Предупреждение", MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == res)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "UPDATE listnuch SET isclose=1 WHERE listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                System.Windows.Forms.MessageBox.Show("Запись успешно остановленна");
                updateOplataTable(1);
            }
        }
        //открытие записи +
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите восстановить выплату для этой записи и сделать ее активной?", "Предупреждение", MessageBoxButton.YesNo);
            if (MessageBoxResult.Yes == res)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "UPDATE listnuch SET isclose=0 WHERE listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                System.Windows.Forms.MessageBox.Show("Запись успешно восстановлена");


                updateOplataTable(1);
            }
        }
        //приняте оплаты +
        private void AddPAy_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payedlist,'_'),array_to_string(topay,'_'),array_to_string(payformonth,'_'),array_to_string(skidkiforpay,'_')  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payedlist = reader.GetString(0);
                        string[] payedlistMas = payedlist.Split('_');
                        double[] payedlistMasDouble = new double[12];

                        string topay = reader.GetString(1);
                        string[] topayMas = topay.Split('_');
                        double[] topayMasDouble = new double[12];

                        string payformonth = reader.GetString(2);
                        string[] payformonthMas = payformonth.Split('_');
                        double[] payformonthMasDouble = new double[12];

                        string skidkiforpay = reader.GetString(3);
                        string[] skidkiforpayMas = skidkiforpay.Split('_');
                        double[] skidkiforpayMasDouble = new double[12];
                        for (int i = 0; i < 12; i++)
                        {

                            payedlistMasDouble[i] = Convert.ToDouble(payedlistMas[i].Replace('.', ','));
                            topayMasDouble[i] = Convert.ToDouble(topayMas[i].Replace('.', ','));
                            payformonthMasDouble[i] = Convert.ToDouble(payformonthMas[i].Replace('.', ','));
                            skidkiforpayMasDouble[i] = Convert.ToDouble(skidkiforpayMas[i].Replace('.', ','));

                        }

                        double[] oplMas = new double[12];
                        int j = 0;
                        for (int i = 0; i < payformonthMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {

                                if (masTbx[j].Text == "") oplMas[i] = 0;
                                else { oplMas[i] = Convert.ToDouble(masTbx[j].Text); }
                                j++;
                                continue;

                            }
                            oplMas[i] = 0;
                        }

                        ArrayList monthSkidkoy = new ArrayList();
                        for (int i = 0; i < payedlistMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (topayMasDouble[i] < oplMas[i]) { System.Windows.Forms.MessageBox.Show("Невозможно принять оплаты больше чем необходимо заплатить"); return; }
                                if (payedlistMasDouble[i] == 0 && topayMasDouble[i] != 0 && topayMasDouble[i] == oplMas[i]) { monthSkidkoy.Add(i); }

                            }
                        }
                        double skidka = 0;

                        //получение процента скидки 
                        if (monthSkidkoy.Count > 1)
                        {
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "SELECT skidka FROM skidki where kol_month=" + monthSkidkoy.Count + "";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        skidka = reader1.GetDouble(0);
                                    }

                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        }
                        payedlist = "'{";
                        topay = "'{";
                        skidkiforpay = "'{";
                        double zdacha = 0;
                        double allSum = 0;

                        for (int i = 0; i < oplMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (monthSkidkoy.IndexOf(i) != -1) { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i] - (oplMas[i] * skidka / 100); skidkiforpayMasDouble[i] = skidka; zdacha += oplMas[i] * skidka / 100; }
                                else { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i]; }

                            }
                            allSum += oplMas[i];
                            payedlist += payedlistMasDouble[i].ToString().Replace(',', '.') + ",";
                            topay += topayMasDouble[i].ToString().Replace(',', '.') + ",";
                            skidkiforpay += skidkiforpayMasDouble[i].ToString().Replace(',', '.') + ",";
                        }

                        payedlist = payedlist.Substring(0, payedlist.Length - 1) + "}'";
                        topay = topay.Substring(0, topay.Length - 1) + "}'";
                        skidkiforpay = skidkiforpay.Substring(0, skidkiforpay.Length - 1) + "}'";
                        //обновление записи
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                            con1.Open();
                            string sql1 = "UPDATE listnuch SET payedlist=" + payedlist + ", skidkiforpay=" + skidkiforpay + ", topay=" + topay + "  WHERE listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                            NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                            com1.ExecuteNonQuery();
                            con1.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        //добавление записи в таблицу дохода 
                        if (allSum != 0)
                        {
                            try
                            {
                                allSum -= zdacha;
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ( 1, " + allSum.ToString().Replace(',', '.') + ", now(),'"+ Listener.SelectedItem+ "');";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        }

                        System.Windows.Forms.MessageBox.Show("Остаток от суммы оплаты из-за скидок при оплате на перед: " + zdacha);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных4"); return; }
            updateOplataTable(1);
        }
        //вод только цифер +
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            { e.Handled = true; }
        }
      //остановка оучения+
        private void StopLern_Click(object sender, RoutedEventArgs e)
        {
            DateIn wind = new DateIn();
            wind.gridToPay.Visibility = Visibility.Visible;
            wind.ShowDialog();
            DateTime dm = wind.getDm();
            if (dm.Day == 1 && dm.Month == 1 && dm.Year == 1) { return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE listnuch SET date_stop='" + dm.ToString().Replace('.', '-') + "' WHERE listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            System.Windows.Forms.MessageBox.Show("Запись успешно остановлена");


            updateOplataTable(1);
        }
        //восстановление обучения+
        private void RestartLern_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE listnuch SET date_stop=NULL WHERE listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            System.Windows.Forms.MessageBox.Show("Запись успешно восстановлена");
            updateOplataTable(1);
        }
        //переход к таблице доходов+
        private void DodhodyTable_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            DohodyrGrid.Visibility = Visibility.Visible;
            FiltrGridDohody.Children.Clear();
            FiltrGridDohody.ColumnDefinitions.Clear();
            filtr.CreateFiltrDohody(FiltrGridDohody);
            filtr.sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) ";
            DataGridUpdater.updateDataGridDohody(connectionString, filtr.sql, DohodyDataGrid);
        }
        // переход к добавление дохода+
        private void DohAddButton_Click(object sender, RoutedEventArgs e)
        {
            DohodyAddSum.Text = "";
            DohodyAddDate.Text =DateTime.Now.ToShortDateString();
            DohodyAddType.Items.Clear();
            dohAddKtoVnesTb.Text = "";
            dohAddKtoVnesCmF.SelectedIndex = 0;
            

            HideAll();
            DohodyrAddGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DohodyAddType.Items.Add(reader.GetString(0));
                    }
                    DohodyAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


        }
        //добавление дохода в базу+
        private void DohodyAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (DohodyAddSum.Text == "" || DohodyAddDate.Text == ""|| dohAddKtoVnesTb.Text=="") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ((select idtype from typedohod where title='" + DohodyAddType.SelectedItem + "'), " + DohodyAddSum.Text.Replace(',', '.') + ", '" + DohodyAddDate.Text.Replace('.', '-') + "','"+ dohAddKtoVnesTb.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                DohodyAddSum.Text = "";
                DohodyAddDate.Text = DateTime.Now.ToShortDateString();
                DohodyAddType.SelectedIndex = 0;
                dohAddKtoVnesCm.SelectedIndex = 0;
            }
            if (res == MessageBoxResult.No)
            {
                HideAll();
                DohodyrGrid.Visibility = Visibility.Visible;

                DohodyDataGrid.SelectedItem = null;
                DohDeleteButton.IsEnabled = false;
                DohChangeButton.IsEnabled = false;
                DataGridUpdater.updateDataGridDohody(connectionString, filtr.sql, DohodyDataGrid);
            }
        }
        //удаление дохода+ 
        private void DohDeleteButton_Click(object sender, RoutedEventArgs e)
        {

            DataRowView DRV = DohodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите удалить этот доход?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "DELETE FROM dodhody WHERE dohid=" + arr[0];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DohodyDataGrid.SelectedItem = null;
                DohDeleteButton.IsEnabled = false;
                DohChangeButton.IsEnabled = false;
                DataGridUpdater.updateDataGridDohody(connectionString, filtr.sql, DohodyDataGrid);
            }
        }
        //переход к имзенению дохода+
        private void DohChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = DohodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            DohodyChangeSum.Text = arr[2].ToString();
            DohodyChangeDate.Text = arr[3].ToString().Replace('/', '.');
            DohodID = (int)arr[0];
            DohodyChangeType.Items.Clear();
           
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int ii = 0;
                if (reader.HasRows)
                {
                    DohodyChangeType.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        DohodyChangeType.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { DohodyChangeType.SelectedIndex = ii; }
                        ii++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            
    
            
            strrr = arr[4].ToString();
           bool a =false, b = false;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio from listeners where fio = '"+strrr+"'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    dohChKtoVnesCmF.SelectedIndex = 1;
                    a = true;
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio from sotrudniki where fio='"+strrr+"'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    dohChKtoVnesCmF.SelectedIndex = 0;
                    b = true;
                

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (a == false && b == false) { dohChKtoVnesCmF.SelectedIndex = 2; }
            dohChKtoVnesTb.Text = arr[4].ToString();
            HideAll();
            DohodyChangeGrid.Visibility = Visibility.Visible;

        }
        //изменение дохода+
        private void DohodyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (DohodyChangeSum.Text == "" || DohodyChangeDate.Text == ""|| dohChKtoVnesTb.Text=="") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE dodhody SET idtype=(select idtype from typedohod where title='" + DohodyChangeType.SelectedItem + "'), sum=" + DohodyChangeSum.Text.Replace(',', '.') + ", data='" + DohodyChangeDate.Text.Replace('.', '-') + "',fio='"+ dohChKtoVnesTb.Text+ "' WHERE dohid= " + DohodID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
        }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
    HideAll();
            DohodyrGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridDohody(connectionString, filtr.sql, DohodyDataGrid);
        }
        //переход к расходам+
        private void RashodyTable_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            RoshodyGrid.Visibility = Visibility.Visible;
            FiltrGridRashody.Children.Clear();
            FiltrGridRashody.ColumnDefinitions.Clear();
            filtr.CreateFiltrRashody(FiltrGridRashody);
            filtr.sql = "SELECT rashody.rashid as rashid, typerash.title as title, sotrudniki.fio as fio, rashody.summ as summ , rashody.data as data, rashody.description as description FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DohodyDataGrid.SelectedItem = null;
            DohDeleteButton.IsEnabled = false;
            DohChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridRashody(connectionString, filtr.sql, RoshodyDataGrid);
        }
        //переход к доабвлению расходов+
        private void RashAddButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            RashodyAddGrid.Visibility = Visibility.Visible;
            RashodyAddType.Items.Clear();
            RashodyAddFIO.Items.Clear();
            RashodyAddSum.Text = "";
            RashodyAddDate.Text = DateTime.Now.ToShortDateString();
            RashodyAddDesc.Text = "";
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RashodyAddType.Items.Add(reader.GetString(0));
                    }
                    RashodyAddType.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        RashodyAddFIO.Items.Add(reader.GetString(0));
                    }
                    RashodyAddFIO.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //добавление расхода в базу+
        private void RashodyAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (RashodyAddSum.Text == "" || RashodyAddDate.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO rashody(typeid, sotrid, summ, data, description)VALUES ((SELECT typeid FROM typerash where title='" + RashodyAddType.SelectedItem + "'), (SELECT sotrid FROM sotrudniki where fio='" + RashodyAddFIO.SelectedItem + "'), " + RashodyAddSum.Text.Replace(',', '.') + ", '" + RashodyAddDate.Text.Replace('.', '-') + "', '" + RashodyAddDesc.Text + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Сумма добавленна.\nПродолжить добавление?", "Продолжить", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                RashodyAddType.SelectedIndex = 0;
                RashodyAddFIO.SelectedIndex = 0;
                RashodyAddSum.Text = "";
                RashodyAddDate.Text = DateTime.Now.ToShortDateString();
                RashodyAddDesc.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                HideAll();
                RoshodyGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridRashody(connectionString, filtr.sql, RoshodyDataGrid);
            }

            RoshodyDataGrid.SelectedItem = null;
            RashDeleteButton.IsEnabled = false;
            RashChangeButton.IsEnabled = false;
        }
        //удаление расхода+
        private void RashDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = RoshodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            MessageBoxResult res = MessageBox.Show("Вы действительно хотите удалить этот расход?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "DELETE FROM rashody WHERE rashid=" + arr[0];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();
                }
                catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                RoshodyDataGrid.SelectedItem = null;
                RashDeleteButton.IsEnabled = false;
                RashChangeButton.IsEnabled = false;
                DataGridUpdater.updateDataGridRashody(connectionString, filtr.sql, RoshodyDataGrid);
            }
        }
        //переход к изменению расхода+
        private void RashChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = RoshodyDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                RashodyChangeType.Items.Clear();
                if (reader.HasRows)
                {
                    RashodyChangeType.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        RashodyChangeType.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { RashodyChangeType.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подклюситься к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                RashodyChangeFIO.Items.Clear();
                if (reader.HasRows)
                {
                    RashodyChangeFIO.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        RashodyChangeFIO.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { RashodyChangeFIO.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            RashodID = (int)arr[0];
            RashodyChangeSum.Text = arr[3].ToString();
            RashodyChangeDate.Text = arr[4].ToString().Replace('/', '.');
            RashodyChangeDesc.Text = arr[5].ToString();
            HideAll();
            RashodyChangeGrid.Visibility = Visibility.Visible;
        }
        //изменение расхода+
        private void RashodyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if (RashodyChangeSum.Text == "" || RashodyChangeDate.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE rashody SET typeid=(SELECT typeid FROM typerash where title='" + RashodyChangeType.SelectedItem + "'), sotrid=(SELECT sotrid FROM sotrudniki where fio='" + RashodyChangeFIO.SelectedItem + "'), summ=" + RashodyChangeSum.Text.Replace(',', '.') + ", data='" + RashodyChangeDate.Text.Replace('.', '-') + "', description='" + RashodyChangeDesc.Text + "' WHERE rashid =" + RashodID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            HideAll();
            RoshodyGrid.Visibility = Visibility.Visible;

            RoshodyDataGrid.SelectedItem = null;
            RashDeleteButton.IsEnabled = false;
            RashChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridRashody(connectionString, filtr.sql, RoshodyDataGrid);
        }
        //переход к налогам+
        private void Nalogi_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = Brushes.DarkRed;
            otchetMenu.BorderBrush = null;
            HideAll();
            NalogiGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from nalogi";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FondSotsStrah.Text = reader.GetDouble(0).ToString();
                        VoenSbor.Text = reader.GetDouble(1).ToString();
                        NDFL.Text = reader.GetDouble(2).ToString();

                    }
                }
                con.Close();

            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //сохранение налогов+
        private void NalogiSave_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(FondSotsStrah.Text.Replace('.', ',')) > 100 || Convert.ToDouble(VoenSbor.Text.Replace('.', ',')) > 100 || Convert.ToDouble(NDFL.Text.Replace('.', ',')) > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }
            double[] mas = new double[3];
            if (FondSotsStrah.Text != "") { mas[0] = Convert.ToDouble(FondSotsStrah.Text.Replace('.', ',')); }
            if (VoenSbor.Text != "") { mas[1] = Convert.ToDouble(VoenSbor.Text.Replace('.', ',')); }
            if (NDFL.Text != "") { mas[2] = Convert.ToDouble(NDFL.Text.Replace('.', ',')); }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE nalogi SET sotsstarh=" + mas[0].ToString().Replace(',', '.') + ", voensbor=" + mas[1].ToString().Replace(',', '.') + ", ndfl=" + mas[2].ToString().Replace(',', '.');
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from nalogi";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        FondSotsStrah.Text = reader.GetDouble(0).ToString();
                        VoenSbor.Text = reader.GetDouble(1).ToString();
                        NDFL.Text = reader.GetDouble(2).ToString();

                    }
                }
                con.Close();

            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //переход к начислениям+
        private void ZP_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            GlNachGrid.Visibility = Visibility.Visible;
            dateNuch = DateTime.Now;
            try {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(sotrid) from sotrudniki where sotrid in (select sotrid from shtat) or sotrid in (select sotrid from prep)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ChbxMas_SotrNuch = new CheckBox[reader.GetInt32(0)];
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            DataGridUpdater.updateGridNachZp(connectionString, NachMonthLabel, ChbxMas_SotrNuch, NachSotrGrid, NachDataGrid, dateNuch);
        }
        //выбрать всех в таблице начилсений +
        private void NuchSelectAllSotrBut_Click(object sender, RoutedEventArgs e)
        {
            if (selected == false)
            {
                for (int i = 0; i < ChbxMas_SotrNuch.Length; i++)
                {
                    ChbxMas_SotrNuch[i].IsChecked = true;

                }
                selected = true; return;
            }
            else {
                for (int i = 0; i < ChbxMas_SotrNuch.Length; i++)
                {
                    ChbxMas_SotrNuch[i].IsChecked = false;

                }
                selected = false; return;
            }
        }
        //переход к предыдущему месяцу в начислениях +
        private void NachMonthPrev_Click(object sender, RoutedEventArgs e)
        {
            dateNuch = dateNuch.AddMonths(-1);
            DataGridUpdater.updateGridNachZp(connectionString, NachMonthLabel, ChbxMas_SotrNuch, NachSotrGrid, NachDataGrid, dateNuch);
        }
        //переход к следующему месяцу в начислениях +
        private void NachMonthNext_Click(object sender, RoutedEventArgs e)
        {
            dateNuch = dateNuch.AddMonths(1);
            DataGridUpdater.updateGridNachZp(connectionString, NachMonthLabel, ChbxMas_SotrNuch, NachSotrGrid, NachDataGrid, dateNuch);
        }
        //начисление зп за месяц +
        private void NachAddBut_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ChbxMas_SotrNuch.Length; i++)
            {
                if (ChbxMas_SotrNuch[i].IsChecked == true)
                {
                    //проверка есть ли запись уже
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(connectionString);
                        con.Open();
                        string sql = "select nachid from nachisl where sotrid = " + ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Year FROM nachisl.payday)=" + dateNuch.Year + " and  EXTRACT(Month FROM nachisl.payday)=" + dateNuch.Month;
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        NpgsqlDataReader reader = com.ExecuteReader();
                        //если запись есть
                        if (reader.HasRows)
                        {
                            //подсчёт за преподавателя
                            double prep_zp = 0;
                            double prep_zp_kateg = 0;
                            double prep_zp_kol_chas = 0;
                            double prep_zp_koefVislugi = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kateg = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid="+ ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= "+dateNuch.Month+" and EXTRACT(Year FROM  raspisanie.date)= "+dateNuch.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kol_chas = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('"+dateNuch.ToShortDateString().Replace('.','-')+"',prep.date_start)) from prep where sotrid = "+ ChbxMas_SotrNuch[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_koefVislugi = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            prep_zp = prep_zp_kateg * prep_zp_kol_chas * prep_zp_koefVislugi;

                            //подсчёт за штат
                            string[] statesStr;
                            string[] stavkyStr;
                            string[] obslworkStr;
                            string[] obemStr;
                            double payShtat = 0;
                            double payObsl = 0;
                            //получение кол-ва отработаных дней в месяце
                            int kol_work_day = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + ChbxMas_SotrNuch[i].Name.Split('_')[1] + "] and extract(Year from date)=" + dateNuch.Year + " and extract(Month from date)=" + dateNuch.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        kol_work_day = reader1.GetInt32(0);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        //получение массивов должностей, ставок, сдельных работ и их объёма.
                        try
                        {
                            NpgsqlConnection con13 = new NpgsqlConnection(connectionString);
                                con13.Open();
                                string sql13 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid ="+ ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com13 = new NpgsqlCommand(sql13, con13);
                                NpgsqlDataReader reader13 = com13.ExecuteReader();
                                if (reader13.HasRows)
                                {
                                    while (reader13.Read())
                                    {
                                        statesStr = reader13.GetString(0).Split('_');
                                        stavkyStr = reader13.GetString(1).Split('_');
                                        obslworkStr = reader13.GetString(2).Split('_');
                                        obemStr = reader13.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < statesStr.Length; j++)
                                        {
                                            if (statesStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day["+dateNuch.Month+"] from states where statesid ="+ statesStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payShtat += reader2.GetDouble(0) * Convert.ToDouble(stavkyStr[j].Replace('.', ','))*(kol_work_day/reader2.GetInt32(1));
                                                    }
                                                }
                                                    con2.Close();
                                            }
                                             catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                        //подсчёт зп сдельной
                                        for (int j = 0; j < obslworkStr.Length; j++)
                                        {
                                            if (obslworkStr[j] == "") continue;
                                            try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + obslworkStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payObsl += reader2.GetDouble(0) * Convert.ToDouble(obemStr[j].Replace('.', ','));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                        }
                                    }
                                }
                                con13.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        //получение процентов налогов
                        double vs = 0, fss = 0, ndfl = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select * from nalogi";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        vs = Math.Round((prep_zp * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(1) / 100), 2);
                                        fss = Math.Round((prep_zp * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(0) / 100), 2);
                                        ndfl = Math.Round((prep_zp * reader1.GetDouble(2) / 100) + (payShtat * reader1.GetDouble(2) / 100) + (payObsl * reader1.GetDouble(2) / 100), 2);

                                        prep_zp = Math.Round(prep_zp - ((prep_zp * reader1.GetDouble(1) / 100) + (prep_zp * reader1.GetDouble(0) / 100) + (prep_zp * reader1.GetDouble(2) / 100)), 2);
                                        payShtat = Math.Round(payShtat - ((payShtat * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(2) / 100)), 2);
                                        payObsl = Math.Round(payObsl - ((payObsl * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(2) / 100)), 2);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        //обновление записи
                        try
                        {
                            NpgsqlConnection con12 = new NpgsqlConnection(connectionString);
                                con12.Open();
                                string sql12 = "UPDATE nachisl SET  prepzp=" + prep_zp.ToString().Replace(',','.')+", shtatzp="+payShtat.ToString().Replace(',', '.') +", obslzp="+payObsl.ToString().Replace(',', '.') + ", vs="+vs.ToString().Replace(',', '.') + ", fss="+fss.ToString().Replace(',', '.') + ", ndfl="+ndfl.ToString().Replace(',', '.') + " WHERE sotrid="+ ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and extract(Year from payday)="+dateNuch.Year+" and extract(month from payday)="+dateNuch.Month;
                                NpgsqlCommand com12 = new NpgsqlCommand(sql12, con12);
                                com12.ExecuteNonQuery();
                                con12.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                    }
                        //если записи нет
                        if (reader.HasRows==false)
                        {
                            //подсчёт за преподавателя
                            double prep_zp = 0;
                            double prep_zp_kateg = 0;
                            double prep_zp_kol_chas = 0;
                            double prep_zp_koefVislugi = 1;
                            DateTime DateStartWork = DateTime.Now;
                            //получение оплаты за категорию
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select kategorii.pay from kategorii inner join prep using(kategid) where sotrid = " + ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kateg = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение кол-ва часов
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select count(raspisanie.idrasp) from prep inner join raspisanie using(prepid) where prep.sotrid=" + ChbxMas_SotrNuch[i].Name.Split('_')[1] + " and EXTRACT(Month FROM raspisanie.date)= " + dateNuch.Month + " and EXTRACT(Year FROM  raspisanie.date)= " + dateNuch.Year;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_kol_chas = reader1.GetInt32(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //получение коефицента выслуги лет
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select koef from koef_vislugi where kol_year<=(select Extract(Year from age('" + dateNuch.ToShortDateString().Replace('.', '-') + "',prep.date_start)) from prep where sotrid = " + ChbxMas_SotrNuch[i].Name.Split('_')[1] + ") order by kol_year desc limit 1";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    { prep_zp_koefVislugi = reader1.GetDouble(0); }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            prep_zp = prep_zp_kateg * prep_zp_kol_chas * prep_zp_koefVislugi;

                            //подсчёт за штат
                            string[] statesStr;
                            string[] stavkyStr;
                            string[] obslworkStr;
                            string[] obemStr;
                            double payShtat = 0;
                            double payObsl = 0;
                            //получение кол-ва отработаных дней в месяце
                            int kol_work_day = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select count(shraspid) from shtatrasp where shtatid @> ARRAY[" + ChbxMas_SotrNuch[i].Name.Split('_')[1] + "] and extract(Year from date)=" + dateNuch.Year + " and extract(Month from date)=" + dateNuch.Month;
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        kol_work_day = reader1.GetInt32(0);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        //получение массивов должностей, ставок, сдельных работ и их объёма.
                        try
                        {
                            NpgsqlConnection con11 = new NpgsqlConnection(connectionString);
                                con11.Open();
                                string sql11 = "select array_to_string(states,'_'),array_to_string(stavky,'_'),array_to_string(obslwork,'_'),array_to_string(obem,'_') from shtat where sotrid =" + ChbxMas_SotrNuch[i].Name.Split('_')[1];
                                NpgsqlCommand com11 = new NpgsqlCommand(sql11, con11);
                                NpgsqlDataReader reader11 = com11.ExecuteReader();
                                if (reader11.HasRows)
                                {
                                    while (reader11.Read())
                                    {
                                        statesStr = reader11.GetString(0).Split('_');
                                        stavkyStr = reader11.GetString(1).Split('_');
                                        obslworkStr = reader11.GetString(2).Split('_');
                                        obemStr = reader11.GetString(3).Split('_');

                                        //подсчёт зп штата
                                        for (int j = 0; j < statesStr.Length; j++)
                                        {
                                    if (statesStr[j] == "") continue;
                                    try
                                            {
                                                NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                                con2.Open();
                                                string sql2 = "select zp, kol_work_day[" + dateNuch.Month + "] from states where statesid =" + statesStr[j];
                                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payShtat += reader2.GetDouble(0) * Convert.ToDouble(stavkyStr[j].Replace('.', ',')) * (kol_work_day / reader2.GetInt32(1));
                                                    }
                                                }
                                                con2.Close();
                                            }
                                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                        }
                                //подсчёт зп сдельной

                                for (int j = 0; j < obslworkStr.Length; j++)
                                        {
                                    if (obslworkStr[j] == "") continue;
                                    try
                                    {
                                        NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                                con2.Open();
                                                string sql2 = "select pay from raboty_obsl where rabotyid =" + obslworkStr[j];
                                   
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                                if (reader2.HasRows)
                                                {
                                                    while (reader2.Read())
                                                    {
                                                        payObsl += reader2.GetDouble(0) * Convert.ToDouble(obemStr[j].Replace('.', ','));
                                                    }
                                                }
                                                con2.Close();
                                    }
                                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                }
                                    }
                                }
                                con11.Close();
                    }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                    //получение процентов налогов
                    double vs = 0, fss = 0, ndfl = 0;
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "select * from nalogi";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        vs =Math.Round( (prep_zp * reader1.GetDouble(1)/100) + (payShtat * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(1) / 100),2);
                                        fss = Math.Round((prep_zp * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(0) / 100),2);
                                        ndfl = Math.Round((prep_zp * reader1.GetDouble(2) / 100) + (payShtat * reader1.GetDouble(2) / 100) + (payObsl * reader1.GetDouble(2) / 100),2);

                                        prep_zp = Math.Round(prep_zp - ((prep_zp * reader1.GetDouble(1) / 100) + (prep_zp * reader1.GetDouble(0) / 100) + (prep_zp * reader1.GetDouble(2) / 100)),2);
                                        payShtat = Math.Round(payShtat - ((payShtat * reader1.GetDouble(1) / 100) + (payShtat * reader1.GetDouble(0) / 100) + (payShtat * reader1.GetDouble(2) / 100)),2);
                                        payObsl = Math.Round(payObsl - ((payObsl * reader1.GetDouble(1) / 100) + (payObsl * reader1.GetDouble(0) / 100) + (payObsl * reader1.GetDouble(2) / 100)),2);
                                    }
                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                            //добавление записи
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO nachisl(sotrid, prepzp, shtatzp, obslzp, viplacheno, payday,vs, fss, ndfl) VALUES ("+ ChbxMas_SotrNuch[i].Name.Split('_')[1] + ", " + prep_zp.ToString().Replace(',', '.') + " , " + payShtat.ToString().Replace(',', '.') + ", " + payObsl.ToString().Replace(',', '.') + ", 0, '"+dateNuch.ToShortDateString().Replace('.','-')+"', "+vs.ToString().Replace(',', '.') + ", "+fss.ToString().Replace(',', '.') + ", "+ndfl.ToString().Replace(',', '.') + ")";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        }
                        con.Close();
                  }
                    catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                }
            }
            NachDataGrid.SelectedItem=null;
            ViplataBut.IsEnabled = false;
            DataGridUpdater.updateGridNachZp(connectionString, NachMonthLabel, ChbxMas_SotrNuch, NachSotrGrid, NachDataGrid, dateNuch);
        }
        //выплата зп +
        private void ViplataBut_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = NachDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Выплата прервана, Вы не выбрали запись для выплаты."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
           
            if (Convert.ToInt32(arr[9])>0)
            {
                DateIn wind = new DateIn();
                wind.gridViplataZp.Visibility = Visibility.Visible;
                wind.zapid = Convert.ToInt32(arr[0]);
                wind.topay = Convert.ToDouble(arr[9]);
                wind.constr = connectionString;
                wind.Owner = this;
                wind.ShowDialog();
                NachDataGrid.SelectedItem = null;
                ViplataBut.IsEnabled = false;
                DataGridUpdater.updateGridNachZp(connectionString, NachMonthLabel, ChbxMas_SotrNuch, NachSotrGrid, NachDataGrid, dateNuch);
            }

            
        }
        //применение фильтров + 
        private void FiltrButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but.Name == "FiltrRashodyButton")
            {
                filtr.ApplyRashodyFiltr();
                DataGridUpdater.updateDataGridRashody(connectionString, filtr.sql, RoshodyDataGrid);
            }

            if (but.Name == "FiltrDohodyButton")
            {
                filtr.ApplyDohodyFiltr();
                DataGridUpdater.updateDataGridDohody(connectionString, filtr.sql, DohodyDataGrid);
            }
        }
        //переход к гриду долга+
        private void OplataDolgMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            DolgGrid.Visibility = Visibility.Visible;

            GroupsDolg.Items.Clear();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie from groups where grid in (select distinct grid from listdolg) order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        GroupsDolg.Items.Add(reader.GetString(0));
                    }
                    GroupsDolg.SelectedIndex = 0;
                }
                if (reader.HasRows == false)
                {
                    HideAll();
                    NoDolgGrdi.Visibility = Visibility.Visible;
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //оплата долга слушателем +
        private void AddPAyDolg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  array_to_string(payedlist,'_'),array_to_string(topay,'_'),array_to_string(payformonth,'_'),array_to_string(skidkiforpay,'_')  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + GroupsDolg.SelectedItem + "')";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string payedlist = reader.GetString(0);
                        string[] payedlistMas = payedlist.Split('_');
                        double[] payedlistMasDouble = new double[12];

                        string topay = reader.GetString(1);
                        string[] topayMas = topay.Split('_');
                        double[] topayMasDouble = new double[12];

                        string payformonth = reader.GetString(2);
                        string[] payformonthMas = payformonth.Split('_');
                        double[] payformonthMasDouble = new double[12];

                        string skidkiforpay = reader.GetString(3);
                        string[] skidkiforpayMas = skidkiforpay.Split('_');
                        double[] skidkiforpayMasDouble = new double[12];
                        for (int i = 0; i < 12; i++)
                        {

                            payedlistMasDouble[i] = Convert.ToDouble(payedlistMas[i].Replace('.', ','));
                            topayMasDouble[i] = Convert.ToDouble(topayMas[i].Replace('.', ','));
                            payformonthMasDouble[i] = Convert.ToDouble(payformonthMas[i].Replace('.', ','));
                            skidkiforpayMasDouble[i] = Convert.ToDouble(skidkiforpayMas[i].Replace('.', ','));

                        }

                        double[] oplMas = new double[12];
                        int j = 0;
                        for (int i = 0; i < payformonthMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {

                                if (masTbx2[j].Text == "") oplMas[i] = 0;
                                else { oplMas[i] = Convert.ToDouble(masTbx2[j].Text); }
                                j++;
                                continue;

                            }
                            oplMas[i] = 0;
                        }

                        ArrayList monthSkidkoy = new ArrayList();
                        for (int i = 0; i < payedlistMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (topayMasDouble[i] < oplMas[i]) { System.Windows.Forms.MessageBox.Show("Невозможно принять оплаты больше чем необходимо заплатить"); return; }
                                if (payedlistMasDouble[i] == 0 && topayMasDouble[i] != 0 && topayMasDouble[i] == oplMas[i]) { monthSkidkoy.Add(i); }

                            }
                        }
                        double skidka = 0;

                        //получение процента скидки 
                        if (monthSkidkoy.Count > 1)
                        {
                            try
                            {
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "SELECT skidka FROM skidki where kol_month=" + monthSkidkoy.Count + "";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                NpgsqlDataReader reader1 = com1.ExecuteReader();
                                if (reader1.HasRows)
                                {
                                    while (reader1.Read())
                                    {
                                        skidka = reader1.GetDouble(0);
                                    }

                                }
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }
                        payedlist = "'{";
                        topay = "'{";
                        skidkiforpay = "'{";
                        double zdacha = 0;
                        double allSum = 0;

                        for (int i = 0; i < oplMas.Length; i++)
                        {
                            if (payformonthMasDouble[i] != 0)
                            {
                                if (monthSkidkoy.IndexOf(i) != -1) { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i] - (oplMas[i] * skidka / 100); skidkiforpayMasDouble[i] = skidka; zdacha += oplMas[i] * skidka / 100; }
                                else { topayMasDouble[i] -= oplMas[i]; payedlistMasDouble[i] += oplMas[i]; }

                            }
                            allSum += oplMas[i];
                            payedlist += payedlistMasDouble[i].ToString().Replace(',', '.') + ",";
                            topay += topayMasDouble[i].ToString().Replace(',', '.') + ",";
                            skidkiforpay += skidkiforpayMasDouble[i].ToString().Replace(',', '.') + ",";
                        }

                        payedlist = payedlist.Substring(0, payedlist.Length - 1) + "}'";
                        topay = topay.Substring(0, topay.Length - 1) + "}'";
                        skidkiforpay = skidkiforpay.Substring(0, skidkiforpay.Length - 1) + "}'";
                        //обновление записи
                        try
                        {
                            NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                            con1.Open();
                            string sql1 = "UPDATE listdolg SET payedlist=" + payedlist + ", skidkiforpay=" + skidkiforpay + ", topay=" + topay + "  WHERE listenerid = (select listenerid from listeners where fio='" + ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + GroupsDolg.SelectedItem + "')";
                            NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                            com1.ExecuteNonQuery();
                            con1.Close();
                        }
                        catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }

                        //добавление записи в таблицу дохода 
                        if (allSum != 0)
                        {
                            try
                            {
                                allSum -= zdacha;
                                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                                con1.Open();
                                string sql1 = "INSERT INTO dodhody(idtype, sum, data,fio)VALUES ( 1, " + allSum.ToString().Replace(',', '.') + ", now(),'"+ ListenerDolg.SelectedItem+ "');";
                                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                                com1.ExecuteNonQuery();
                                con1.Close();
                            }
                            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
                        }

                        System.Windows.Forms.MessageBox.Show("Остаток от суммы оплаты из-за скидок при оплате на перед: " + zdacha);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            updateOplataTable(2);
        }
        //удаление долга+
        private void deleteDolg_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Вы согласны удалить эту запись навсегда?", "Предупреждение", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from listdolg where grid in (select grid from groups where nazvanie ='"+GroupsDolg.SelectedItem+ "') and listenerid in (select listenerid from listeners where fio ='" + ListenerDolg.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                GroupsDolg.Items.Clear();
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select nazvanie from groups where grid in (select distinct grid from listdolg) order by grid";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            GroupsDolg.Items.Add(reader.GetString(0));
                        }
                        GroupsDolg.SelectedIndex = 0;
                    }
                    if (reader.HasRows == false)
                    {
                        HideAll();
                        NoDolgGrdi.Visibility = Visibility.Visible;
                    }
                    con.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }
        }
        //переход к гриду отчета кассы+
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            kassaGrid.Visibility = Visibility.Visible;
            fda.CreateKassaDAFiltr(pD);
            fdb.CreateKassaDBFiltr(tD);
            fra.CreateKassaRAFiltr(pR);
            frb.CreateKassaRBFiltr(tR);
            fda.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            fra.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel, fda.sql, fra.sql);
        }
        //переход к гриду отчета статистика+
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            DataGridUpdater.updateGridStatistica(connectionString, statGraf);
            HideAll();
            StatisticaGrid.Visibility = Visibility.Visible;
           
        }
        //переход к гриду отчета списки выплат+
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            HideAll();
            ZpOthcetGrid.Visibility = Visibility.Visible;
            ZPOtchetVivodGrid.ColumnDefinitions.Clear();
            ZPOtchetVivodGrid.Children.Clear();

            ZPOtchetLabel.Content = "Отчёт 'Списки выплат' на "+ DateTime.Now.ToShortDateString();

            ColumnDefinition cmd = new ColumnDefinition();
            cmd.Width = new  GridLength(200);
            Grid gr = new Grid();
            Grid.SetColumn(gr, 0);
            ZPOtchetVivodGrid.ColumnDefinitions.Add(cmd);
            ZPOtchetVivodGrid.Children.Add(gr);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select  count(fio) from sotrudniki";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        for(int i=0;i<reader.GetInt32(0)+3;i++)
                        {
                            RowDefinition cmdGr = new RowDefinition();
                            cmdGr.Height = new GridLength(50);
                            gr.RowDefinitions.Add(cmdGr);
                        }
                    }
                   
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            Label mesLb = new Label();
            mesLb.Content = "Месяц";
            mesLb.BorderBrush = Brushes.Black;
            mesLb.BorderThickness = new Thickness(2);

            Label zpLb = new Label();
            zpLb.Content = "ЗП";
            zpLb.BorderBrush = Brushes.Black;
            zpLb.BorderThickness = new Thickness(2);

            Label itogLb = new Label();
            itogLb.Content = "Итого";
            itogLb.BorderBrush = Brushes.Black;
            itogLb.BorderThickness = new Thickness(2);

            Grid.SetRow(mesLb, 0);
            Grid.SetRow(zpLb, 1);
            Grid.SetRow(itogLb, gr.RowDefinitions.Count-1);

            gr.Children.Add(mesLb); gr.Children.Add(zpLb); gr.Children.Add(itogLb);

            //заполнение первого грида
            ArrayList sotrList = new ArrayList();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select  fio,sotrid from sotrudniki order by fio";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 2;
                    while (reader.Read())
                    {
                            Label sotrLb = new Label();
                        sotrLb.Content = reader.GetString(0);
                        sotrLb.BorderBrush = Brushes.Black;
                        sotrLb.BorderThickness = new Thickness(2);
                        Grid.SetRow(sotrLb, i);
                            gr.Children.Add(sotrLb);
                        sotrList.Add(reader.GetInt32(1));
                        i++;
                    }

                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение дат (мемсяц/год из таблиц начислений)

            ArrayList dateList = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select payday from nachisl order by payday";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        int month = reader1.GetDateTime(0).Month;
                        int year = reader1.GetDateTime(0).Year;
                   
                    

                    DateTime dd = new DateTime(year, month, 1);
                    if (dateList.IndexOf(DateTimeAxis.ToDouble(dd))==-1) dateList.Add(DateTimeAxis.ToDouble(dd));
                    }

                }
                con1.Close();

        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            for (int i = 0; i < dateList.Count; i++)
            {
                
                    ColumnDefinition cmdd = new ColumnDefinition();
                    cmdd.Width = new GridLength(300);
                ZPOtchetVivodGrid.ColumnDefinitions.Add(cmdd);
                    Grid monthGrid = new Grid();
                    for(int j=0;j<4;j++)
                    {
                        ColumnDefinition cmdd2 = new ColumnDefinition();
                        monthGrid.ColumnDefinitions.Add(cmdd2);
                    }
                DataGridUpdater.updateGridSpisciVyplat(connectionString, DateTimeAxis.ToDateTime(Convert.ToDouble(dateList[i])), sotrList, monthGrid);
                    Grid.SetColumn(monthGrid, i + 1);
                    ZPOtchetVivodGrid.Children.Add(monthGrid);
            }
        }
        //кликл по меню прав+
        private void MenuRolesB_Click(object sender, RoutedEventArgs e)
        {

            MenuRolesB.BorderBrush = Brushes.DarkRed;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;
        }
        //кликл по меню доходов+
        private void Dohody_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = Brushes.DarkRed;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;
        }
        //кликл по меню расходов+
        private void Rashody_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = Brushes.DarkRed;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;
        }
        //кликл по меню отчётов+
        private void otchetMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = Brushes.DarkRed;
        }
        //разблокировка всех кнопок +
        private void NachDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //начисления
            ViplataBut.IsEnabled = true;

           //расходы
            RashDeleteButton.IsEnabled = true;
            RashChangeButton.IsEnabled = true;

            //доходы
            DohDeleteButton.IsEnabled = true;
            DohChangeButton.IsEnabled = true;


        }
        private void PrimFKD_Click(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt.Name == "PrimFKD")
            {
                fda.ApplyDohFiltr(fdb);
                DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel, fda.sql, fra.sql);
            }
            if (bt.Name == "PrimFKR")
            {

                fra.ApplyRashFiltr(frb);
                DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel, fda.sql, fra.sql);
            }
        }

        private void dohAddKtoVnesCm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "dohAddKtoVnesCm")
            {
                if (cmb.Items.Count == 0) { return; }
                if (cmb.SelectedItem.ToString() == "Нет в списке") { dohAddKtoVnesTb.Text = ""; dohAddKtoVnesTb.IsEnabled = true; }
                else { dohAddKtoVnesTb.Text = cmb.SelectedItem.ToString(); dohAddKtoVnesTb.IsEnabled = false; }

            }

            if (cmb.Name == "dohChKtoVnesCm")
            {

                if (cmb.Items.Count == 0) { return; }
                if (cmb.SelectedItem.ToString() == "Нет в списке") { dohChKtoVnesTb.Text = ""; dohChKtoVnesTb.IsEnabled = true; }
                else
                { dohChKtoVnesTb.Text = cmb.SelectedItem.ToString(); dohChKtoVnesTb.IsEnabled = false; }

            }
        }
    }
}
    

           
        
   



