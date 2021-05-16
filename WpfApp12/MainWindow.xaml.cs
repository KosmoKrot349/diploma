using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Npgsql;
using System.Collections;
using System.IO;
using WinForms = System.Windows.Forms;
using WpfApp12.strategiesForMainWind.strategiesForMainWindButtonClick;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //строка подключения
        public string connectionString = "";
        public MainWindow()
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
            pereraschet();
        }
        //авторизация пользователя
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new Authorize(this);
            actionReact.ButtonClick();
        }
        //проверка подключения
        private void Button_Click11(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ConnectionCheck(this);
            actionReact.ButtonClick();
        }
        //сохранение настроек
        public void saveSettings()
        {
            object[] mas = new object[4];

            mas[0] = "conn:" + connect.Text;
            mas[1] = "pass:" + dbPassText.Text;
            mas[2] = "port:" + dbPortText.Text;

            StreamWriter writer = new StreamWriter(@"setting.txt");
            writer.WriteLine(mas[0]); writer.WriteLine(mas[1]); writer.WriteLine(mas[2]); writer.WriteLine(mas[3]);
            writer.Close();

            connectionString = "Server=" + connect.Text + ";Port=" + dbPortText.Text + ";User Id=postgres;Password=" + dbPassText.Text + ";Database=db";

            MessageBox.Show("Настройки сохранены и применены");
            settingGrid.Visibility = Visibility.Collapsed;
            autGrid.Visibility = Visibility.Visible;

        }
        //принудительное сохранение
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new EnforcementSeatings(this);
            actionReact.ButtonClick();
        }
        //воод только цифр
        private void ip1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)))
            {
                { e.Handled = true; }
            }
        }
        public void pereraschet()
        {
            DateTime DT = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            DateTime DT2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            bool b = false;

            while (DT2.Month == DateTime.Now.Month)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();

                    string sql = "select shraspid from shtatrasp where date='" + DT2.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows == false)
                    {
                        b = true;
                        break;
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                DT2 = DT2.AddDays(1);
            }


            if (b == true)
            {
                MessageBoxResult res2 = MessageBox.Show("Есть возможность автоматически заполнить\nштатное расписание для этого месяца. Заполнить?", "Предупреждение", MessageBoxButton.YesNo);

                if (res2 == MessageBoxResult.Yes)
                {
                    while (DT.Month == DateTime.Now.Month)
                    {
                        try
                        {
                            NpgsqlConnection con = new NpgsqlConnection(connectionString);
                            con.Open();

                            string sql = "select shraspid from shtatrasp where date='" + DT.ToShortDateString().Replace('.', '-') + "'";
                            NpgsqlCommand com = new NpgsqlCommand(sql, con);
                            NpgsqlDataReader reader = com.ExecuteReader();
                            if (reader.HasRows == false)
                            {
                                string shtatID = "'{-1,";
                                //получение всех id штатных сотрудников
                                try
                                {
                                    NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                    con2.Open();

                                    string sql2 = "select sotrid from shtat";
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                    NpgsqlDataReader reader2 = com2.ExecuteReader();
                                    if (reader2.HasRows)
                                    {
                                        while (reader2.Read())
                                        {
                                            shtatID += reader2.GetInt32(0) + ",";
                                        }
                                    }
                                    con2.Close();

                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                                shtatID = shtatID.Substring(0, shtatID.Length - 1) + "}'";

                                //добавление записи в штатное расписание
                                try
                                {
                                    NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                    con2.Open();

                                    string sql2 = "INSERT INTO shtatrasp(shtatid, date) VALUES (" + shtatID + ", '" + DT.ToShortDateString().Replace('.', '-') + "')";
                                    NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                    NpgsqlDataReader reader2 = com2.ExecuteReader();
                                    con2.Close();

                                }
                                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                            }
                            con.Close();

                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        DT = DT.AddDays(1);
                    }
                }
            }


            DateTime DateNow = DateTime.Now;
            DateTime DateOfLastPerer = new DateTime();
            double penyaproc = 0;
            //для обычной таблицы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select * from last_pereraschet";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DateOfLastPerer = reader.GetDateTime(0);
                        penyaproc = reader.GetDouble(1);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }



            if (DateOfLastPerer.Month != DateNow.Month)
            {
                MessageBoxResult res = MessageBox.Show("Есть возможность переасчёта пени. Выполнить?", "Предупреждение", MessageBoxButton.YesNo);
                if (res == MessageBoxResult.Yes)
                { 
                //для обычной таблицы
                try
                {

                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select listnuchid,listenerid,grid,array_to_string(payformonth,'_'),array_to_string(payedlist,'_'),array_to_string(skidkiforpay,'_'),array_to_string(penya ,'_'),date_stop from listnuch where isclose=0";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int idzapisi = reader.GetInt32(0);
                            int grid = reader.GetInt32(2);
                            int listId = reader.GetInt32(1);

                            string OplataZaMes = reader.GetString(3);
                            string[] OplataZaMesMass = OplataZaMes.Split('_');
                            double[] OplataZaMesMasDouble = new double[12];

                            string YjeOplacheno = reader.GetString(4);
                            string[] YjeOplachenoMass = YjeOplacheno.Split('_');
                            double[] YjeOplachenoMasDouble = new double[12];

                            string SkidkiPriOplate = reader.GetString(5);
                            string[] SkidkiPriOplateMass = SkidkiPriOplate.Split('_');
                            double[] SkidkiPriOplateMasDouble = new double[12];

                            string Penya = reader.GetString(6);
                            string[] PenyaMas = Penya.Split('_');
                            double[] PenyaMasDouble = new double[12];

                            double[] NeedToPay = new double[12];

                            for (int i = 0; i < 12; i++)
                            {
                                OplataZaMesMasDouble[i] = Convert.ToDouble(OplataZaMesMass[i].ToString().Replace('.', ','));
                                YjeOplachenoMasDouble[i] = Convert.ToDouble(YjeOplachenoMass[i].ToString().Replace('.', ','));
                                SkidkiPriOplateMasDouble[i] = Convert.ToDouble(SkidkiPriOplateMass[i].ToString().Replace('.', ','));
                                PenyaMasDouble[i] = Convert.ToDouble(PenyaMas[i].ToString().Replace('.', ','));
                            }
                            DateTime MonthToPererachet = DateTime.Now;
                            if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { MonthToPererachet = reader.GetDateTime(7); }

                            DateTime DatehStartLernG = new DateTime();
                            DateTime DatehEndLernG = new DateTime();

                            //получение месяцев начала и конаца обучения группы
                            try
                            {
                                NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                con2.Open();
                                string sql2 = "select date_start,date_end from groups where grid = " + grid;
                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                if (reader2.HasRows)
                                {
                                    while (reader2.Read())
                                    {
                                        DatehStartLernG = reader2.GetDateTime(0);
                                        DatehEndLernG = reader2.GetDateTime(1);

                                    }
                                }
                                con2.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }




                            //начало перерасчёта пени
                            bool start_rasch = false;
                            if (DatehStartLernG.Month > DatehEndLernG.Month)
                            {
                                if (DatehStartLernG.Month < MonthToPererachet.Month && DatehStartLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);

                                    }
                                    start_rasch = true;
                                }


                                if (DatehEndLernG.Month >= MonthToPererachet.Month && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                    {

                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    for (int i = 0; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }

                                if (DatehEndLernG.Month < MonthToPererachet.Month && DatehStartLernG.Month > MonthToPererachet.Month && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    for (int i = 0; i <= DatehEndLernG.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }
                            }

                            if (DatehStartLernG.Month < DatehEndLernG.Month && DatehStartLernG.Year <= MonthToPererachet.Year && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                {
                                    PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                }
                                start_rasch = true;
                            }


                            if (DatehStartLernG.Month == DatehEndLernG.Month)
                            {
                                if (DatehStartLernG.Year == DatehEndLernG.Year && DatehStartLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }
                            }



                            for (int i = 0; i < 12; i++)
                            {
                               NeedToPay[i] = Math.Round(OplataZaMesMasDouble[i] + PenyaMasDouble[i] - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100)),2);
                            }

                            OplataZaMes = "'{";

                            YjeOplacheno = "'{";

                            SkidkiPriOplate = "'{";

                            Penya = "'{";

                            string NeedToPayString = "'{";

                            for (int i = 0; i < 12; i++)
                            {

                                OplataZaMes += OplataZaMesMasDouble[i].ToString().Replace(',', '.') + ",";
                                YjeOplacheno += YjeOplachenoMasDouble[i].ToString().Replace(',', '.') + ",";
                                SkidkiPriOplate += SkidkiPriOplateMasDouble[i].ToString().Replace(',', '.') + ",";
                                Penya += PenyaMasDouble[i].ToString().Replace(',', '.') + ",";
                                NeedToPayString += NeedToPay[i].ToString().Replace(',', '.') + ",";

                            }
                            OplataZaMes = OplataZaMes.Substring(0, OplataZaMes.Length - 1) + "}'";
                            YjeOplacheno = YjeOplacheno.Substring(0, YjeOplacheno.Length - 1) + "}'";
                            SkidkiPriOplate = SkidkiPriOplate.Substring(0, SkidkiPriOplate.Length - 1) + "}'";
                            Penya = Penya.Substring(0, Penya.Length - 1) + "}'";
                            NeedToPayString = NeedToPayString.Substring(0, NeedToPayString.Length - 1) + "}'";
                            try
                            {
                                NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                                con3.Open();
                                string sql3 = "UPDATE listnuch SET payformonth=" + OplataZaMes + ", payedlist=" + YjeOplacheno + ", skidkiforpay=" + SkidkiPriOplate + ",topay=" + NeedToPayString + ", penya=" + Penya + " WHERE listnuchid=" + idzapisi;
                                WinForms.MessageBox.Show(sql3);
                                NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                com3.ExecuteNonQuery();
                                con3.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                        }
                    }


                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }


                //для таблицы долгов
                try
                {

                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select listdolgid,listenerid,grid,array_to_string(payformonth,'_'),array_to_string(payedlist,'_'),array_to_string(skidkiforpay,'_'),array_to_string(penya ,'_'),date_stop,date_start,date_end from listdolg where isclose=0";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            int idzapisi = reader.GetInt32(0);
                            int grid = reader.GetInt32(2);
                            int listId = reader.GetInt32(1);

                            string OplataZaMes = reader.GetString(3);
                            string[] OplataZaMesMass = OplataZaMes.Split('_');
                            double[] OplataZaMesMasDouble = new double[12];

                            string YjeOplacheno = reader.GetString(4);
                            string[] YjeOplachenoMass = YjeOplacheno.Split('_');
                            double[] YjeOplachenoMasDouble = new double[12];

                            string SkidkiPriOplate = reader.GetString(5);
                            string[] SkidkiPriOplateMass = SkidkiPriOplate.Split('_');
                            double[] SkidkiPriOplateMasDouble = new double[12];

                            string Penya = reader.GetString(6);
                            string[] PenyaMas = Penya.Split('_');
                            double[] PenyaMasDouble = new double[12];

                            double[] NeedToPay = new double[12];

                            for (int i = 0; i < 12; i++)
                            {
                                OplataZaMesMasDouble[i] = Convert.ToDouble(OplataZaMesMass[i].ToString().Replace('.', ','));
                                YjeOplachenoMasDouble[i] = Convert.ToDouble(YjeOplachenoMass[i].ToString().Replace('.', ','));
                                SkidkiPriOplateMasDouble[i] = Convert.ToDouble(SkidkiPriOplateMass[i].ToString().Replace('.', ','));
                                PenyaMasDouble[i] = Convert.ToDouble(PenyaMas[i].ToString().Replace('.', ','));
                            }
                            DateTime MonthToPererachet = DateTime.Now;
                            if (!reader.IsDBNull(reader.GetOrdinal("date_stop"))) { MonthToPererachet = reader.GetDateTime(7); }

                            DateTime DatehStartLernG = reader.GetDateTime(8);
                            DateTime DatehEndLernG = reader.GetDateTime(9);

                            //начало перерасчёта пени
                            bool start_rasch = false;
                            if (DatehStartLernG.Month > DatehEndLernG.Month)
                            {
                                if (DatehStartLernG.Month < MonthToPererachet.Month && DatehStartLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);

                                    }
                                    start_rasch = true;
                                }


                                if (DatehEndLernG.Month >= MonthToPererachet.Month && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                    {

                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    for (int i = 0; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }

                                if (DatehEndLernG.Month < MonthToPererachet.Month && DatehStartLernG.Month > MonthToPererachet.Month && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    for (int i = 0; i <= DatehEndLernG.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }
                            }

                            if (DatehStartLernG.Month < DatehEndLernG.Month && DatehStartLernG.Year <= MonthToPererachet.Year && DatehEndLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                {
                                    PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                }
                                start_rasch = true;
                            }


                            if (DatehStartLernG.Month == DatehEndLernG.Month)
                            {
                                if (DatehStartLernG.Year == DatehEndLernG.Year && DatehStartLernG.Year <= MonthToPererachet.Year && start_rasch == false)
                                {
                                    for (int i = DatehStartLernG.Month - 1; i < MonthToPererachet.Month - 1; i++)
                                    {
                                        PenyaMasDouble[i] = Math.Round(PenyaMasDouble[i] + ((OplataZaMesMasDouble[i] + PenyaMasDouble[i]) - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100))) * penyaproc / 100,2);
                                    }
                                    start_rasch = true;
                                }
                            }



                            for (int i = 0; i < 12; i++)
                            {
                                NeedToPay[i] = Math.Round(OplataZaMesMasDouble[i] + PenyaMasDouble[i] - (YjeOplachenoMasDouble[i] + (OplataZaMesMasDouble[i] * SkidkiPriOplateMasDouble[i] / 100)),2);
                            }

                            OplataZaMes = "'{";

                            YjeOplacheno = "'{";

                            SkidkiPriOplate = "'{";

                            Penya = "'{";

                            string NeedToPayString = "'{";

                            for (int i = 0; i < 12; i++)
                            {

                                OplataZaMes += OplataZaMesMasDouble[i].ToString().Replace(',', '.') + ",";
                                YjeOplacheno += YjeOplachenoMasDouble[i].ToString().Replace(',', '.') + ",";
                                SkidkiPriOplate += SkidkiPriOplateMasDouble[i].ToString().Replace(',', '.') + ",";
                                Penya += PenyaMasDouble[i].ToString().Replace(',', '.') + ",";
                                NeedToPayString += NeedToPay[i].ToString().Replace(',', '.') + ",";

                            }
                            OplataZaMes = OplataZaMes.Substring(0, OplataZaMes.Length - 1) + "}'";
                            YjeOplacheno = YjeOplacheno.Substring(0, YjeOplacheno.Length - 1) + "}'";
                            SkidkiPriOplate = SkidkiPriOplate.Substring(0, SkidkiPriOplate.Length - 1) + "}'";
                            Penya = Penya.Substring(0, Penya.Length - 1) + "}'";
                            NeedToPayString = NeedToPayString.Substring(0, NeedToPayString.Length - 1) + "}'";
                            try
                            {
                                NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                                con3.Open();
                                string sql3 = "UPDATE listdolg SET payformonth=" + OplataZaMes + ", payedlist=" + YjeOplacheno + ", skidkiforpay=" + SkidkiPriOplate + ",topay=" + NeedToPayString + ", penya=" + Penya + " WHERE listdolgid=" + idzapisi;
                                WinForms.MessageBox.Show(sql3);
                                NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                com3.ExecuteNonQuery();
                                con3.Close();
                            }
                            catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                        }
                    }


                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
                try
                {
                    NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                    con3.Open();
                    string sql3 = "UPDATE last_pereraschet SET last_date=now()";
                    NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                    com3.ExecuteNonQuery();
                    con3.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе по заданным параметрам"); return; }
            }
        }
        }


    }

    }

