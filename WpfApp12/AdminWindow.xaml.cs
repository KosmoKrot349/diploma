using System.Text;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Npgsql;
using System.Collections;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using WinForms = System.Windows.Forms;
using System.Collections.Generic;
using System.Data;


namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public int adminR = 0;
        public int rykR = 0;
        public int buhgR = 0;
        public int uID = 0;
        public int logUser;
        public string FIO = "";
        //строка подключения
        string connectionString = "";


        filtr filtr = new filtr();
        //+
        public AdminWindow()
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
            MenuRolesA.BorderBrush = null;
            usersMenu.BorderBrush = null;
            arhivMenu.BorderBrush = null;
            settingMenu.BorderBrush = null;
            ToNextYearMenu.BorderBrush = null;

        }
        //+
        public void hideAll()
        {
            helloGrdi.Visibility = Visibility.Collapsed;
            delChUserGrid.Visibility = Visibility.Collapsed;
            regGrid.Visibility = Visibility.Collapsed;
            crDumpGrid.Visibility = Visibility.Collapsed;
            rsDumpGrid.Visibility = Visibility.Collapsed;
            userChangeGrid.Visibility = Visibility.Collapsed;
            settingGrid.Visibility = Visibility.Collapsed;
            NextYearGrid.Visibility = Visibility.Collapsed;

        }
        //переход из меню админа в меню админа+
        private void AdminRoleA_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже выбрали роль администратора");
        }
        //переход из меню админа в меню бухгалтера+
        private void BuhgRoleA_Click(object sender, RoutedEventArgs e)
        {
            int b = 0;
           if (logUser!=-1)b = Checker.buhgCheck(logUser,connectionString);
            if (b == 1 || logUser == -1)
            {
                BuhgalterWindow wind = new BuhgalterWindow();
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
                            if (dReader.GetInt32(0) == 0) { wind.AdminRoleB.IsEnabled = false; }
                            if (dReader.GetInt32(1) == 0) { wind.BuhgRoleB.IsEnabled = false; }
                            if (dReader.GetInt32(2) == 0) { wind.DirectorRoleB.IsEnabled = false; }

                        }
                    }
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                wind.logUser = logUser;
                wind.FIO = FIO;
                wind.Title = FIO + " - Бухгалтер";
                wind.hello_label.Text = "Здравствуйте, Ваша текущая роль бухгалтер. Для начала роботы выберите один из пунктов меню.";
                wind.Width = this.Width;
                wind.Height = this.Height;
                wind.Left = this.Left;
                wind.Top = this.Top;
                wind.Show();
                this.Close();
            }
            else { MessageBox.Show("Вы не имете доступа к этой роли"); }
        }
        //переход из меню админа в меню директора+
        private void DirectorRoleA_Click(object sender, RoutedEventArgs e)
        {
            int d = 0;
            if (logUser!=-1)d = Checker.dirCheck(logUser, connectionString);

            if (d == 1||logUser==-1)
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
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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

        //Выбор роли админа для пользователя+
        private void adm_Checked(object sender, RoutedEventArgs e)
        {
            adminR = 1;

        }
        //Выбор роли админа для пользователя+
        private void adm_Unchecked(object sender, RoutedEventArgs e)
        {
            adminR = 0;
        }
        //Выбор роли бухгалтера для пользователя+
        private void bh_Unchecked(object sender, RoutedEventArgs e)
        {
            buhgR = 0;
        }
        //Выбор роли бухгалтера для пользователя+
        private void bh_Checked(object sender, RoutedEventArgs e)
        {
            buhgR = 1;
        }
        //Выбор роли директора для пользователя+
        private void dr_Checked(object sender, RoutedEventArgs e)
        {
            rykR = 1;
        }
        //Выбор роли директора для пользователя+
        private void dr_Unchecked(object sender, RoutedEventArgs e)
        {
            rykR = 0;
        }
        //Переход к окну регистрации+
        private void useradd_menu_Click(object sender, RoutedEventArgs e)
        {
            fio.Text = "";
            log_reg.Text = "";
            pas_reg.Password = "";
            rePas.Password = "";
            adm.IsChecked = false;
            bh.IsChecked = false;
            dr.IsChecked = false;

            hideAll();
            regGrid.Visibility = Visibility.Visible;
         

        }
        //регистрация+
        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (fio.Text == "" || log_reg.Text == "" || pas_reg.Password == "" || rePas.Password == "") { MessageBox.Show("Некоторые поля не заполнены, регистрация невозможна"); return; }
            if (pas_reg.Password != rePas.Password) { MessageBox.Show("Пароли не совподают"); return; }
            if (log_reg.Text == "root") { MessageBox.Show("Пользователь root уже существует"); return; }
            try
            {
                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                string sql = "select log from users where log='" + log_reg.Text + "'";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                NpgsqlDataReader reader = Command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show( "Пользовтель с таким логином уже существует"); return; }
                npgSqlConnection.Close();
                npgSqlConnection.Open();
                sql = "insert into users (fio, log, pas, admin, buhgalter, director) values('" + fio.Text + "','" + log_reg.Text + "','" + pas_reg.Password + "'," + adminR + "," + buhgR + "," + rykR + ")";
                Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            MessageBoxResult but = MessageBox.Show("Пользователь добавлен.\nПродолжить добавление?","Добавление",MessageBoxButton.YesNo);

            if (but == MessageBoxResult.Yes)
            {
                fio.Text = "";
                log_reg.Text = "";
                pas_reg.Password = "";
                rePas.Password = "";
                adm.IsChecked = false;
                bh.IsChecked = false;
                dr.IsChecked = false;
            }
            else {
                hideAll();
                delChUserGrid.Visibility = Visibility.Visible;
                usersDGrid.SelectedItem = null;

                changeUser.IsEnabled = false;
                dellUser.IsEnabled = false;
                DataGridUpdater.updateDataGridUsers(connectionString, filtr.sql, usersDGrid);
            }
        }
      
        //удаление пользователя+
        private void dellUser_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = usersDGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (Convert.ToInt32(arr[0]) == logUser) { MessageBox.Show("Вы не можете самого себя"); return; }
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
            try
            {
                npgSqlConnection.Open();
                string sql = "DELETE FROM users WHERE uid =" + arr[0];
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            usersDGrid.SelectedItem = null;

            changeUser.IsEnabled = false;
            dellUser.IsEnabled = false;

            DataGridUpdater.updateDataGridUsers(connectionString, filtr.sql, usersDGrid);

        }
        //переход к изменению пользователя+
        private void changeUser_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = usersDGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменениею не выбрав запись."); return; }
            DataRow DR = DRV.Row;
          
            object[] arr = DR.ItemArray;
            uID = Convert.ToInt32(arr[0]);
            hideAll();
            userChangeGrid.Visibility = Visibility.Visible;
            uCFio.Text = arr[1].ToString();
            uClog.Text = arr[2].ToString();
            uCpas.Text = arr[3].ToString();
            if (Convert.ToString(arr[4]) == "Да")
                {
                uCadm.IsChecked = true;
                }
            else uCadm.IsChecked = false;
            if (Convert.ToString(arr[5]) == "Да")
                {
                uCbh.IsChecked = true;
            }
            else uCbh.IsChecked = false;
            if (Convert.ToString(arr[6]) == "Да")
                {
                uCdr.IsChecked = true;
            }
            else uCdr.IsChecked = false;


        }
        //изменение  пользоателя+

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (uCFio.Text == "" || uCpas.Text == "" || uClog.Text == "" || (uCadm.IsChecked == false && uCbh.IsChecked == false && uCdr.IsChecked == false)) { MessageBox.Show("Такие изменения не возможны"); return; }
            try
            {

                NpgsqlConnection npgSqlConnection = new NpgsqlConnection(connectionString);
                npgSqlConnection.Open();
                string sql = "update users set log = '" + uClog.Text + "', fio = '" + uCFio.Text + "', pas = '" + uCpas.Text + "', admin = '" + adminR + "', buhgalter = '" + buhgR + "', director = '" + rykR + "' where uid=" + uID;
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
                MessageBox.Show("Пользователь изменен");

            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            hideAll();
            delChUserGrid.Visibility = Visibility.Visible;
          

            usersDGrid.SelectedItem = null;

            changeUser.IsEnabled = false;
            dellUser.IsEnabled = false;

            DataGridUpdater.updateDataGridUsers(connectionString, filtr.sql, usersDGrid);
        }
        //
        //переход к окну бэкапа+
        private void crDump_Click(object sender, RoutedEventArgs e)
        {
            hideAll();
            crDumpGrid.Visibility = Visibility.Visible;
            
            bckpName.Text = "";
            bckpPyt.Text = "";
            sybdPyt.Text = "";
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            sybdPyt.Text = splitMas1.Trim(' ');
            string splitMas2 = batStrMas[2].ToString();
            int index_puti = 0;

            for (int i = 0; i < splitMas2.Length; i++)
            {
                if (splitMas2[i] == '>') index_puti = i + 1;
            }
            string[] masFolders = splitMas2.Substring(index_puti).Split('\\');
            for (int i = 0; i < masFolders.Length - 1; i++)
            { bckpPyt.Text += masFolders[i] + "\\"; }
            bckpPyt.Text = bckpPyt.Text.Trim(' ');
            StreamReader.Close();

        }

        //выбор папки СУБД+
        private void selectBin_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) {MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }

                if(but.Name== "selectBinNextYear") sybdPytNextYear.Text = FBD.SelectedPath + "\\";
                else 
                sybdPyt.Text = FBD.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string[] disk = FBD.SelectedPath.Split('\\');
            batStrMas[0] = disk[0];
            batStrMas[1] = "cd " + FBD.SelectedPath;
            StreamReader.Close();
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
        //Выбор папки для бэкапа+
        private void selectbckp_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                if (but.Name == "selectbckpNextYear") bckpPytNextYear.Text = FBD.SelectedPath + "\\";
                else
                    bckpPyt.Text = FBD.SelectedPath+"\\";
            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string batLastStr = batStrMas[2].ToString();
            StreamReader.Close();
            int index_puti = 0;
            for (int i = 0; i < batLastStr.Length; i++)
            {
                if (batLastStr[i] == '>') index_puti = i + 1;
            }
            StringBuilder newBatLastStr = new StringBuilder(batLastStr.Substring(0, index_puti) + " " + FBD.SelectedPath + "\\");
            batStrMas[2] = newBatLastStr;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
        //Создание бэкапа+
        private void crDumpButton_Click(object sender, RoutedEventArgs e)
        {
            if (bckpName.Text != "")
            {
                string bckpname = bckpName.Text;
                for (int i = 0; i < bckpname.Length; i++)
                {
                    if ((bckpname[i] >= 'а' && bckpname[i] <= 'я') || (bckpname[i] >= 'А' && bckpname[i] <= 'Я')) { MessageBox.Show("В имени копии не должно быть русскких символов"); return; }
                }

            }
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            StreamReader.Close();
            StreamReader StreamReader2 = new StreamReader(@"setting.txt");
            ArrayList arLs2 = new ArrayList();
            while (!StreamReader2.EndOfStream)
            {
                arLs2.Add(StreamReader2.ReadLine());
            }
            StreamReader2.Close();
            object[] batStrMas2 = arLs2.ToArray();

            string batLastStr = "pg_dump -d postgresql://postgres:"+ batStrMas2[1].ToString().Split(':')[1] + "@"+ batStrMas2[0].ToString().Split(':')[1] + ":"+ batStrMas2[2].ToString().Split(':')[1] + "/db > ";
            if (bckpName.Text == "")
            {
                DateTime a = DateTime.Now;
                batLastStr += bckpPyt.Text+"" + a.Day + "_" + a.Month + "_" + a.Year + "_" + a.Hour + "_" + a.Minute + "_" + a.Second + ".sql";
            }
            else
            {
                batLastStr += bckpPyt.Text + bckpName.Text+".sql";

            }
            batStrMas[2] = batLastStr;
            StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();

            Process.Start("crDump.bat");


        }
        //выбор бэкапа+
        private void rsSelectbckp_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            if (fileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < fileDialog.FileName.Length; i++)
                {
                    if ((fileDialog.FileName[i] >= 'а' && fileDialog.FileName[i] <= 'я') || (fileDialog.FileName[i] >= 'А' && fileDialog.FileName[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                rsBckpPyt.Text = fileDialog.FileName;
            }
        }
        //выбор папки СУБД+
        private void rsSelectBin_Click(object sender, RoutedEventArgs e)
        {
            WinForms.FolderBrowserDialog FBD = new WinForms.FolderBrowserDialog();
            if (FBD.ShowDialog() == WinForms.DialogResult.OK)
            {
                for (int i = 0; i < FBD.SelectedPath.Length; i++)
                {
                    if ((FBD.SelectedPath[i] >= 'а' && FBD.SelectedPath[i] <= 'я') || (FBD.SelectedPath[i] >= 'А' && FBD.SelectedPath[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русскких символов"); return; }
                }
                rsSybdPyt.Text = FBD.SelectedPath + "\\";
            }
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string[] disk = FBD.SelectedPath.Split('\\');
            batStrMas[0] = disk[0];
            batStrMas[1] = "cd " + FBD.SelectedPath;
            StreamReader.Close();
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }

            StreamWriter.Close();
        }
        //переход к окну восстановления+
        private void rstrDump_Click(object sender, RoutedEventArgs e)
        {
            hideAll();
            rsDumpGrid.Visibility = Visibility.Visible;
            
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            rsSybdPyt.Text = splitMas1;
            string splitMas2 = batStrMas[2].ToString();
            StreamReader.Close();
        }
        //восстановление бэкапа+
        private void rsDumpButton_Click(object sender, RoutedEventArgs e)
        {
            StreamReader a = new StreamReader(@"rsDump.bat");
            while (!a.EndOfStream)
            {
                string str = a.ReadLine();
                for (int i = 0; i < str.Length; i++)
                {
                        if ((str[i] >= 'а' && str[i] <= 'я') || (str[i] >= 'А' && str[i] <= 'Я')) { MessageBox.Show("В пути не должно быть русских символов"); return; }
                }

            }
            a.Close();

            if (rsBckpPyt.Text == "") { MessageBox.Show("Укажите файл для восстановления"); return; }
            StreamReader reader = new StreamReader(@"setting.txt");
            ArrayList ls = new ArrayList();
            while (!reader.EndOfStream)
            {
                ls.Add(reader.ReadLine());
            }
            object[] mas = ls.ToArray();
            string newConnStr = "Server=" + mas[0].ToString().Split(':')[1] + ";Port=" + mas[2].ToString().Split(':')[1] + ";User Id=postgres;Password=" + mas[1].ToString().Split(':')[1] + ";";
           
            NpgsqlConnection npgSqlConnection = new NpgsqlConnection(newConnStr);
            try
            {

                npgSqlConnection.Open();
                string sql = "SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = 'db' AND pid<> pg_backend_pid(); ";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к БД.");return; }

            try
            {
                npgSqlConnection.Open();
                string sql = "drop database if exists db";
                NpgsqlCommand Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteNonQuery();
                npgSqlConnection.Close();
                npgSqlConnection.Open();
                sql = "create database db";
                Command = new NpgsqlCommand(sql, npgSqlConnection);
                Command.ExecuteReader();
                npgSqlConnection.Close();
            }

            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            StreamReader StreamReader = new StreamReader(@"rsDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            StreamReader.Close();
            object[] batStrMas = arLs.ToArray();
            string lastStr = batStrMas[2].ToString();
            int index_puti = 0;
            for (int i = 0; i < lastStr.Length; i++)
            {
                if (lastStr[i] == '<') { index_puti = i; break; }
            }
            batStrMas[2] = "psql -d postgresql://postgres:" + mas[1].ToString().Split(':')[1] + "@" + mas[0].ToString().Split(':')[1] + ":" + mas[2].ToString().Split(':')[1] + "/db < "  + rsBckpPyt.Text;
            StreamWriter StreamWriter = new StreamWriter(@"rsDump.bat");
            for (int i = 0; i < batStrMas.Length; i++)
            {
                StreamWriter.WriteLine(batStrMas[i]);
            }
            StreamWriter.Close();
            Process.Start("rsDump.bat");
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select fio from users";
                NpgsqlCommand com = new NpgsqlCommand(sql,con);
                NpgsqlDataReader reader1 = com.ExecuteReader();
                con.Close();
            }
            catch { }

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
        // переход к настройкам+
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesA.BorderBrush = null;
            usersMenu.BorderBrush = null;
            arhivMenu.BorderBrush = null;
            settingMenu.BorderBrush = Brushes.DarkRed;
            ToNextYearMenu.BorderBrush = null;
            hideAll();
            settingGrid.Visibility = Visibility.Visible;
           
            StreamReader streamReader = new StreamReader(@"setting.txt");
            ArrayList list = new ArrayList();
            while (!streamReader.EndOfStream)
            {
                list.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            object []mas_str = list.ToArray();
            connect.Text = mas_str[0].ToString().Split(':')[1];
            dbPassText.Text = mas_str[1].ToString().Split(':')[1];
            dbPortText.Text = mas_str[2].ToString().Split(':')[1];
           

            if (logUser == -1)
            {
                try
                {
                    rootSettings.Visibility = Visibility.Visible;
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select pas from users where uid = -1";
                    NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rootpass.Text = reader.GetString(0);

                        }

                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }else rootSettings.Visibility = Visibility.Collapsed;


        }
      
        //проверка подключения+
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string testConStr = "Server=" + connect.Text + ";Port=" + dbPortText.Text + ";User Id=postgres;Password=" + dbPassText.Text + ";Database=db;";
            NpgsqlConnection testcon = new NpgsqlConnection(testConStr);
            try
            {
                testcon.Open();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе по заданный параметрам"); return; }
            testcon.Close();
          MessageBoxResult res=  MessageBox.Show("Подключение по данным пораметрам прошло успешно.\nСохранить параметры?","Сохранение",MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                saveSettings();

            }
            if (res == MessageBoxResult.No)
            {
                return;
            }
        }
        //сохранение настроек+
        public void saveSettings()
        {
            object[] mas = new object[4];

            mas[0] = "conn:"+connect.Text;
            mas[1] = "pass:"+dbPassText.Text;
            mas[2] = "port:" + dbPortText.Text;

            StreamWriter writer = new StreamWriter(@"setting.txt");
            writer.WriteLine(mas[0]); writer.WriteLine(mas[1]); writer.WriteLine(mas[2]); writer.WriteLine(mas[3]);
            writer.Close();

            connectionString = "Server=" + connect.Text + ";Port=" + dbPortText.Text + ";User Id=postgres;Password=" + dbPassText.Text + ";Database=db";

          MessageBox.Show("Настройки сохранены и применены");
        }
        //принудительное сохранение+
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            saveSettings();
        }
        //сохранение пароля рута+
        private void rootSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection conn = new NpgsqlConnection(connectionString);
                conn.Open();
                string sql = "UPDATE users SET pas = '" + rootpass.Text+"' where uid = -1";
                NpgsqlCommand com = new NpgsqlCommand(sql,conn);
                com.ExecuteNonQuery();
                conn.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //переход к пользователям+
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            
            MenuRolesA.BorderBrush = null;
            usersMenu.BorderBrush = Brushes.DarkRed;
            arhivMenu.BorderBrush = null;
            settingMenu.BorderBrush = null;
            ToNextYearMenu.BorderBrush = null;

            usersDGrid.SelectedItem = null;

            changeUser.IsEnabled = false;
            dellUser.IsEnabled = false;

            hideAll();
            delChUserGrid.Visibility = Visibility.Visible;

            filtr.CreateUsersFiltr(FiltrGridRoles);

            filtr.sql = "select * from users where uid != -1";

            DataGridUpdater.updateDataGridUsers(connectionString, filtr.sql, usersDGrid);
        }
        //клик по меню права+
        private void MenuRolesA_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesA.BorderBrush = Brushes.DarkRed;
            usersMenu.BorderBrush = null;
            arhivMenu.BorderBrush = null;
            settingMenu.BorderBrush = null;
            ToNextYearMenu.BorderBrush = null;
        }
        //клик по меню архив+
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            MenuRolesA.BorderBrush = null;
            usersMenu.BorderBrush = null;
            arhivMenu.BorderBrush = Brushes.DarkRed;
            settingMenu.BorderBrush = null;
            ToNextYearMenu.BorderBrush = null;
        }
        //изменение кнопок контроля для DataGrid
        private void usersDGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            changeUser.IsEnabled = true;
            dellUser.IsEnabled = true;
        }
        //применение фильтра по ролям
        private void FiltrRolesButton_Click(object sender, RoutedEventArgs e)
        {
            filtr.ApplyUsersFiltr();
            DataGridUpdater.updateDataGridUsers(connectionString, filtr.sql, usersDGrid);
        }
        //переход к следующему году+
        private void ToNextYearMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesA.BorderBrush = null;
            usersMenu.BorderBrush = null;
            arhivMenu.BorderBrush = null;
            settingMenu.BorderBrush = null;
            ToNextYearMenu.BorderBrush = Brushes.DarkRed;
            hideAll();
            NextYearGrid.Visibility = Visibility.Visible;
            bckpNameNextYear.Text = "";
            bckpPytNextYear.Text = "";
            sybdPytNextYear.Text = "";
            StreamReader StreamReader = new StreamReader(@"crDump.bat");
            ArrayList arLs = new ArrayList();
            while (!StreamReader.EndOfStream)
            {
                arLs.Add(StreamReader.ReadLine());
            }
            object[] batStrMas = arLs.ToArray();
            string splitMas1 = batStrMas[1].ToString().Substring(2);
            sybdPytNextYear.Text = splitMas1.Trim(' ');
            string splitMas2 = batStrMas[2].ToString();
            int index_puti = 0;

            for (int i = 0; i < splitMas2.Length; i++)
            {
                if (splitMas2[i] == '>') index_puti = i + 1;
            }
            string[] masFolders = splitMas2.Substring(index_puti).Split('\\');
            for (int i = 0; i < masFolders.Length - 1; i++)
            { bckpPytNextYear.Text += masFolders[i] + "\\"; }
            bckpPytNextYear.Text=bckpPytNextYear.Text.Trim(' ');
            StreamReader.Close();
        }
        //переход на новый год
        private void crDumpButtonNextYear_Click(object sender, RoutedEventArgs e)
        {
            //создание бэкапа за старый год
          MessageBoxResult res= MessageBox.Show("Вы собираетесь выполнить переход к новому году.\n Текущая версия Вашей базы данных будет сохранена отдельным файлом на вашем ПК.\n Будут подсчитаны расходы и доходы за весь год.\n Так же очищены выплаченные зп и таблицы дохода и расхода.\n Для слушателей оплативших весь год обучения записи будут обнулены, должники занесены в отдельную таблицу.\n Год в дате обучения у групп будет увеличен на 1.\n Вы не потеряете Ваши данные, Вы всегда можете их восстановить из файла который будет создан после этой процедуры. ", "Предупреждение",MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                if (bckpNameNextYear.Text != "")
                {
                    string bckpname = bckpName.Text;
                    for (int i = 0; i < bckpname.Length; i++)
                    {
                        if ((bckpname[i] >= 'а' && bckpname[i] <= 'я') || (bckpname[i] >= 'А' && bckpname[i] <= 'Я')) { MessageBox.Show("В имени копии не должно быть русскких символов"); return; }
                    }

                }
                StreamReader StreamReader = new StreamReader(@"crDump.bat");
                ArrayList arLs = new ArrayList();
                while (!StreamReader.EndOfStream)
                {
                    arLs.Add(StreamReader.ReadLine());
                }
                object[] batStrMas = arLs.ToArray();
                StreamReader.Close();
                StreamReader StreamReader2 = new StreamReader(@"setting.txt");
                ArrayList arLs2 = new ArrayList();
                while (!StreamReader2.EndOfStream)
                {
                    arLs2.Add(StreamReader2.ReadLine());
                }
                StreamReader2.Close();
                object[] batStrMas2 = arLs2.ToArray();

                string batLastStr = "pg_dump -d postgresql://postgres:" + batStrMas2[1].ToString().Split(':')[1] + "@" + batStrMas2[0].ToString().Split(':')[1] + ":" + batStrMas2[2].ToString().Split(':')[1] + "/db > ";
                if (bckpNameNextYear.Text == "")
                {
                    DateTime a = DateTime.Now;
                    batLastStr += bckpPytNextYear.Text + "" + a.Day + "_" + a.Month + "_" + a.Year + "_" + a.Hour + "_" + a.Minute + "_" + a.Second + "_stary_god.sql";
                }
                else
                {
                    batLastStr += bckpPytNextYear.Text + bckpName.Text + "_stary_god.sql";

                }
                batStrMas[2] = batLastStr;
                StreamWriter StreamWriter = new StreamWriter(@"crDump.bat");
                for (int i = 0; i < batStrMas.Length; i++)
                {
                    StreamWriter.WriteLine(batStrMas[i]);
                }

                StreamWriter.Close();

                Process.Start("crDump.bat");
            }
            //запись суммы расходов и доходов за год
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO itog(itogidate, dohod, rashod)VALUES ( now(), (select coalesce(sum(sum),0) from dodhody), (select coalesce(sum(summ),0) from rashody))";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //очитска таблицы доходов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from dodhody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //очитска таблицы расходов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from rashody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удаление выплаченных зп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from nachisl where (prepzp+shtatzp+obslzp)-viplacheno=0";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //полчуение закрытыйх записей из оплат слушателей (+ удаление группы у слушателя)
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select listenerid,grid from listnuch where isclose=1";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        int listenerid = reader.GetInt32(0);
                        int grid = reader.GetInt32(1);
                        //полчуение массива скидок и групп слушателя из удаляемой записи
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                            con2.Open();
                            string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid="+ listenerid;
                            
                            NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    string[] gridstr = reader2.GetString(0).Split('_');
                                    string[] lgtstr = reader2.GetString(1).Split('_');

                                    string gridstr2 = "'{";
                                    string lgtstr2 = "'{";
                                    //запись массивов без удаляемого елемента
                                    for (int i = 0; i < gridstr.Length; i++)
                                    {

                                        if (Convert.ToInt32(gridstr[i]) != grid)
                                        {

                                            gridstr2 += gridstr[i] + ",";
                                            lgtstr2 += lgtstr[i] + ",";
                                        }

                                    }
                                    if (gridstr2.Length != 2)
                                    {
                                        gridstr2 = gridstr2.Substring(0, gridstr2.Length - 1) + "}'";
                                        lgtstr2 = lgtstr2.Substring(0, lgtstr2.Length - 1) + "}'";
                                    }
                                    else
                                    {
                                        gridstr2 += "}'";
                                        lgtstr2 += "}'";
                                    }
                                    try
                                    {
                                        NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                                        con3.Open();
                                        string sql3 = "UPDATE listeners SET  grid=" + gridstr2 + ", lgt=" + lgtstr2 + " WHERE listenerid = " + listenerid;
                                        NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                        com3.ExecuteReader();
                                        con3.Close();
                                    }
                                    catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                }
                                con2.Close();
                            }
                        }
                        catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                    }

                }
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удаление закрытых записей об оплате
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from listnuch where isclose=1";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            ArrayList list = new ArrayList();
            //получение id остановленных записей в которых всё оплачено 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select array_to_string(listnuch.topay,'_'),listnuch.date_stop,groups.date_start,groups.date_end,listnuch.listnuchid from listnuch inner join groups using(grid) where date_stop != null";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    string[] topay = reader.GetString(0).Split('_');
                    DateTime dateStop = reader.GetDateTime(1);
                    DateTime DatehStartLernG = reader.GetDateTime(2);
                    DateTime DatehEndLernG = reader.GetDateTime(3);
                    int listnuchid = reader.GetInt32(4);

                    while (reader.Read())
                    {

                        //начало перерасчёта пени
                        bool oplacheno = true;
                        bool prosmotrenno = false;
                        if (DatehStartLernG.Month > DatehEndLernG.Month)
                        {
                            if (DatehStartLernG.Month < dateStop.Month && DatehStartLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break;  }

                                }
                                prosmotrenno = true;
                            }


                            if (DatehEndLernG.Month >= dateStop.Month && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                {

                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                for (int i = 0; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }

                            if (DatehEndLernG.Month < dateStop.Month && DatehStartLernG.Month > dateStop.Month && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < 12; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                for (int i = 0; i <= DatehEndLernG.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }
                        }

                        if (DatehStartLernG.Month < DatehEndLernG.Month && DatehStartLernG.Year <= dateStop.Year && DatehEndLernG.Year <= dateStop.Year && prosmotrenno == false)
                        {
                            for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                            {
                                if (topay[i] != "0") { oplacheno = false; break; }
                            }
                            prosmotrenno = true;
                        }

                        if (DatehStartLernG.Month == DatehEndLernG.Month)
                        {
                            if (DatehStartLernG.Year == DatehEndLernG.Year && DatehStartLernG.Year <= dateStop.Year && prosmotrenno == false)
                            {
                                for (int i = DatehStartLernG.Month - 1; i < dateStop.Month - 1; i++)
                                {
                                    if (topay[i] != "0") { oplacheno = false; break; }
                                }
                                prosmotrenno = true;
                            }
                        }
            
                        if (oplacheno == true && prosmotrenno == true)
                        {
                            list.Add(listnuchid);
                        }


                    }

                }

                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //очитска остановленных записей об оплате где выплачена вся сумма
            object[] listArr = list.ToArray();
            for (int i = 0; i < listArr.Length; i++)
            {

                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select listenerid,grid from listnuch where listnuchid="+ listArr[i];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {


                            int listenerid = reader.GetInt32(0);
                            int grid = reader.GetInt32(1);
                            //полчуение массива скидок и групп слушателя из удаляемой записи
                            try
                            {
                                NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                                con2.Open();
                                string sql2 = "select array_to_string(grid,'_'),array_to_string(lgt,'_') from listeners where listenerid=" + listenerid;
                                NpgsqlCommand com2 = new NpgsqlCommand(sql2, con2);
                                NpgsqlDataReader reader2 = com2.ExecuteReader();
                                if (reader2.HasRows)
                                {
                                    while (reader2.Read())
                                    {
                                        string[] gridstr = reader2.GetString(0).Split('_');
                                        string[] lgtstr = reader2.GetString(1).Split('_');

                                        string gridstr2 = "'{";
                                        string lgtstr2 = "'{";
                                        //запись массивов без удаляемого елемента
                                        for (int i2 = 0; i2 < gridstr.Length; i2++)
                                        {
                                            if (Convert.ToInt32(gridstr[i2]) != grid)
                                            {
                                                gridstr2 += gridstr[i2] + ",";
                                                lgtstr2 += lgtstr[i2] + ",";
                                            }

                                        }
                                        if (gridstr2.Length != 2)
                                        {
                                            gridstr2 = gridstr2.Substring(0, gridstr2.Length - 1) + "}'";
                                            lgtstr2 = lgtstr2.Substring(0, lgtstr2.Length - 1) + "}'";
                                        }
                                        else {
                                            gridstr2  += "}'";
                                            lgtstr2  += "}'";
                                        }
                                        try
                                        {
                                            NpgsqlConnection con3 = new NpgsqlConnection(connectionString);
                                            con3.Open();
                                            string sql3 = "UPDATE listeners SET  grid=" + gridstr2 + ", lgt=" + lgtstr2 + " WHERE listenerid = " + listenerid;
                                            NpgsqlCommand com3 = new NpgsqlCommand(sql3, con3);
                                            com3.ExecuteReader();
                                            con3.Close();
                                        }
                                        catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                                    }
                                    con2.Close();
                                }
                            }
                            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                        }

                    }
                    con.Close();

                }
                catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from listnuch where  listnuchid=" + listArr[i];
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    com.ExecuteNonQuery();
                    con.Close();

                }
                catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            }

            //перенос записей об оплате в таблицу должников
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO listdolg(listenerid, grid, year, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose, date_start, date_end) (select  listenerid, grid, now() ,payformonth, payedlist,skidkiforpay,topay,penya,date_stop,isclose,date_start,date_end from listnuch inner join groups using(grid) where topay[1]!=0 or topay[2]!=0 or topay[3]!=0 or topay[4]!=0 or topay[5]!=0 or topay[6]!=0 or topay[7]!=0 or topay[8]!=0 or topay[9]!=0 or topay[10]!=0 or topay[11]!=0 or topay[12]!=0)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //обновление записей у нормальных слушателей
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE listnuch SET payedlist='{0,0,0,0,0,0,0,0,0,0,0,0}', skidkiforpay='{0,0,0,0,0,0,0,0,0,0,0,0}', topay=payformonth, penya='{0,0,0,0,0,0,0,0,0,0,0,0}'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //обновление года в группах
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "update groups set  date_start =date_start + interval '1 year',date_end=date_end + interval '1 year'";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

            }
            catch { WinForms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
