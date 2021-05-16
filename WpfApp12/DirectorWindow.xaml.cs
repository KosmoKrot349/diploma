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
    /// Логика взаимодействия для DirectorWindow.xaml
    /// </summary>
    public partial class DirectorWindow : Window
    {
        public int logUser;
        int prepID = -1;
        int grID = -1;
        int courseID = -1;
        int sotrID = -1;
        string dontChCName = "";
        string dontChGName = "";

        //массивы дял слушателей
        CheckBox[] chbxMas_gr_lg;
        TextBox[] tbxMas_gr_lg;

        CheckBox[] chbxMas;
        public string FIO = "";
        Label[,] lbmas;
        int m = 0;//число зантий в дне
        int n = 0;//число групп
        int iRaspLebale;
        int jRaspLebale;
        DateTime dateMonday;
        int listenerID = -1;
        int stateID = -1;
        int ShtatID = -1;
        bool selectd=false;
        //массивы для штата 
        TextBox[] tbxMas_stavki;
        CheckBox[] chbxMas_state;
        TextBox[] tbxMas_obem;
        CheckBox[] chbxMas_obslwork;

        //массивы для штатного расписания
        CheckBox[] chbxMas_stateRasp;
        Label [,] lbmas_shtatRasp = new Label[7,7];
        DateTime date = DateTime.Now;


        filtr filtr = new filtr();

        filtr fda = new filtr();
        filtr fdb = new filtr();

        filtr fra = new filtr();
        filtr frb = new filtr();


        //фильтр для всех сотрудников
        string sqlAllSotr = "";

       

        //строка подключения
        string connectionString = "";

       //+
        public DirectorWindow()
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
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = null;
            obuchMenu.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
            skidki.BorderBrush = null;
        }
        //+
        public void HideAll()
        {
            listenerDataGrid.SelectedItem = null;
            coursDataGrid.SelectedItem = null;
            subsDataGrid.SelectedItem = null;
            groupsDataGrid.SelectedItem = null;
            prepDataGrid.SelectedItem = null;
            kategDataGrid.SelectedItem = null;
            allSotrDataGrid.SelectedItem = null;
            zvonkiDataGrid.SelectedItem = null;
            cabDataGrid.SelectedItem = null;
            StateDataGrid.SelectedItem = null;
            ObslWorkDataGrid.SelectedItem = null;
            ShtatDataGrid.SelectedItem = null;
            KoefDataGrid.SelectedItem = null;
            TypeRashDataGrid.SelectedItem = null;
            TypeDohDataGrid.SelectedItem = null;



            //слушатели
            listenerDeleteButton.IsEnabled = false;
            listenerChangeButton.IsEnabled = false;

            //курсы
            coursDeleteButton.IsEnabled = false;
            coursChangeButton.IsEnabled = false;

            //предметы
            subDeleteButton.IsEnabled = false;

            //группы
            groupDeleteButton.IsEnabled = false;
            groupChangeButton.IsEnabled = false;

            //преподаватели
            prepDeleteButton.IsEnabled = false;
            prepChangeButton.IsEnabled = false;

            //категории
            kategDeleteButton.IsEnabled = false;

            //все сотрудники
            allSotrDeleteButton.IsEnabled = false;
            allSotrToPrepBtton.IsEnabled = false;
            allSotrToShtatBtton.IsEnabled = false;

            //расписание звонков
            zvonkiDeleteButton.IsEnabled = false;

            //кабинеты
            cabDeleteButton.IsEnabled = false;

            //типы дохода
            TypeDohDeleteButton.IsEnabled = false;

            //типы расходов
            TypeRashDeleteButton.IsEnabled = false;

            //коефициент за выслугу лет
            KoefDeleteButton.IsEnabled = false;

            //работы обслуживания
            ObslWorkDeleteButton.IsEnabled = false;

            //должности 
            StateChangeButton.IsEnabled = false;
            StateDeleteButton.IsEnabled = false;

            //штат
            shtatDeleteButton.IsEnabled = false;
            shtatChangeButton.IsEnabled = false;

            //штатное расписание
            ShtatRaspSaveBut.IsEnabled = false;

            ListenerAddGrid.Visibility = Visibility.Collapsed;
            helloGrdi.Visibility = Visibility.Collapsed;
            zvonkiGrid.Visibility = Visibility.Collapsed;
            prepGrid.Visibility = Visibility.Collapsed;
            kategGrid.Visibility = Visibility.Collapsed;
            allSotrGrid.Visibility = Visibility.Collapsed;
            groupsGrid.Visibility = Visibility.Collapsed;
            prepChangeGrid.Visibility = Visibility.Collapsed;
            subGrid.Visibility = Visibility.Collapsed;
            courseGrid.Visibility = Visibility.Collapsed;
            groupAddGrid.Visibility = Visibility.Collapsed;
            groupChangeGrid.Visibility = Visibility.Collapsed;
            courseAddGrid.Visibility = Visibility.Collapsed;
            courseChangeGrid.Visibility = Visibility.Collapsed;
            addPrepGrid.Visibility = Visibility.Collapsed;
            raspGridG.Visibility = Visibility.Collapsed;
            raspGridP.Visibility = Visibility.Collapsed;
            raspGridС.Visibility = Visibility.Collapsed;
            addRaspGrid.Visibility = Visibility.Collapsed;
            changeRaspGrid.Visibility = Visibility.Collapsed;
            ListenerGrid.Visibility = Visibility.Collapsed;
            ListenerChangeGrid.Visibility = Visibility.Collapsed;
            addRaspGridKab.Visibility = Visibility.Collapsed;
            changeRaspGridKab.Visibility = Visibility.Collapsed;
            addRaspGridPrep.Visibility = Visibility.Collapsed;
            changeRaspGridPrep.Visibility = Visibility.Collapsed;
            cabGrid.Visibility = Visibility.Collapsed;
            skidkiGrid.Visibility = Visibility.Collapsed;
            TypeDohGrid.Visibility = Visibility.Collapsed;
            TypeRashGrid.Visibility = Visibility.Collapsed;
            KoefGrid.Visibility = Visibility.Collapsed;
            ObslWorkGrid.Visibility = Visibility.Collapsed;
            StateGrid.Visibility = Visibility.Collapsed;
            StateAddGrid.Visibility = Visibility.Collapsed;
            StateChaneGrid.Visibility = Visibility.Collapsed;
            addShtatGrid.Visibility = Visibility.Collapsed;
            ShtatGrid.Visibility = Visibility.Collapsed;
            ChangeShtatGrid.Visibility = Visibility.Collapsed;
            ShtatRaspGrid.Visibility = Visibility.Collapsed;
            kassaGrid.Visibility = Visibility.Collapsed;
            StatisticaGrid.Visibility = Visibility.Collapsed;
            ZpOthcetGrid.Visibility = Visibility.Collapsed;

        }
        //переход из меню директора в меню админа+
        private void AdminRoleD_Click(object sender, RoutedEventArgs e)
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
            else { MessageBox.Show("Вы не имеете доступа к этой роли"); }
        }
        //переход из меню директора в меню бухгалтера+
        private void BuhgRoleD_Click(object sender, RoutedEventArgs e)
        {
            int b = 0;
            if (logUser != -1) b = Checker.buhgCheck(logUser, connectionString);
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
            else { MessageBox.Show("Вы не имеете доступа к этой роли"); }
        }
        //переход из меню директора в меню директора+
        private void DirectorRoleD_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вы уже выбрали роль директора");
        }
        //переход к расписанию звоноков+
        private void zvonkiMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            zvonkiGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridZvonki(connectionString, zvonkiDataGrid);
        }
        //удаление записи в расписаии звонков+
        private void ZvonkiDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = zvonkiDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка расписания
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select lesson_number from raspisanie where lesson_number = " + arr[1];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("В расписании есть занятия в это время."); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //удлаение из таблицы
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from lessons_time where id =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //обновление тех что после
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE lessons_time SET lesson_number=lesson_number-1 WHERE lesson_number> " + arr[1];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridZvonki(connectionString, zvonkiDataGrid);
            zvonkiDataGrid.SelectedItem = null;
            //расписание звонков
            zvonkiDeleteButton.IsEnabled = false;
        }
        //добавление/изменение записи в расписании звоноков +
        private void ZvonkiAddButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("id", System.Type.GetType("System.Int32"));
            table.Columns.Add("lesson_number", System.Type.GetType("System.Int32"));
            table.Columns.Add("time_start", System.Type.GetType("System.TimeSpan"));
            table.Columns.Add("time_end", System.Type.GetType("System.TimeSpan"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < zvonkiDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = zvonkiDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан номер занятия"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано время начала занятия"); return; }
                if (rMas[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано время конца занятия"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется номер занятия " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            list.Sort();
            if (list.Count != 0) { if (Convert.ToInt32(list[0]) != 1) { System.Windows.Forms.MessageBox.Show("Занятия пронумерованы не верно"); return; } }
            for (int i = 0; i < list.Count; i++)
            {
                if (i != Convert.ToInt32(list[i]) - 1)
                {
                    System.Windows.Forms.MessageBox.Show("Занятия пронумерованы не верно"); return;
                }


            }
            string sql = "select * from lessons_time";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);

            zvonkiDataGrid.SelectedItem = null;
            //расписание звонков
            zvonkiDeleteButton.IsEnabled = false;
            DataGridUpdater.updateDataGridZvonki(connectionString, zvonkiDataGrid);
        }
        //переход к сотрудникам+
        private void allSotr_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            allSotrGrid.Visibility = Visibility.Visible;
            sqlAllSotr = "SELECT * FROM sotrudniki";
            DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, allSotrDataGrid);
        }
        //переход к преподавателям+
        private void preps_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            prepGrid.Visibility = Visibility.Visible;

            FiltrGridPrep.Children.Clear();
            FiltrGridPrep.ColumnDefinitions.Clear();

            filtr.CreatePrepFiltr(FiltrGridPrep);
            filtr.sql = "SELECT prep.prepid as prid,kategorii.title as nazva ,sotrudniki.fio as name ,prep.date_start as date,sotrudniki.comment as comm FROM sotrudniki inner join prep using(sotrid) inner join kategorii using(kategid)";

            DataGridUpdater.updateDataGridPrep(connectionString, filtr.sql, prepDataGrid);
        }
        //переход к категориям+
        private void kategorii_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            kategGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridKateg(connectionString, kategDataGrid);

        }
        //добавление категори +
        private void kategAddButton_Click(object sender, RoutedEventArgs e)
        {
           
            ArrayList list = new ArrayList();
            for (int i = 0; i < kategDataGrid.Items.Count - 1; i++)
            {
                DataTable table = new DataTable();
                table.Columns.Add("kategid", System.Type.GetType("System.Int32"));
                table.Columns.Add("title", System.Type.GetType("System.String"));
                table.Columns.Add("pay", System.Type.GetType("System.Double"));

                DataRowView DRV = kategDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название категории"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указана оплата"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется название категории " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);

                string sql = "select * from kategorii";
                NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
                NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
                NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
                adapter.Update(table);
            }

            kategDataGrid.SelectedItem = null;

            //категории
            kategDeleteButton.IsEnabled = false;

            DataGridUpdater.updateDataGridKateg(connectionString, kategDataGrid);
        }
        //удаление категории+
        private void kategDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = kategDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("select prepid from prep where kategid =" + arr[0]);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Эту категорию невозможно удалить, она используется преподавателями."); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("delete from kategorii where kategid =" + arr[0]);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridKateg(connectionString, kategDataGrid);

            kategDataGrid.SelectedItem = null;

            //категории
            kategDeleteButton.IsEnabled = false;

        }
        //сохранение изменений в таблице преподавателей +
        private void save_Click(object sender, RoutedEventArgs e)
        {

            if (prepFio.Text == "" || dateStartAdd.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            int kategId = 0;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("select kategid from kategorii where title = '" + kategCMBX.SelectedValue + "'");
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kategId = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            string[] date = dateStart.Text.Split('.');

            int sotrId = 0;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select sotrid from prep where prepid = " + prepID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sotrId = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE prep SET  kategid =" + kategId + ", date_start ='" + dateStartAdd.Text + "' WHERE prepid = " + prepID ;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("UPDATE sotrudniki SET  fio ='" + prepFio.Text + "', comment ='" + prepCom.Text + "' WHERE sotrid = " + sotrId + ";");
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            HideAll();
            prepGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridPrep(connectionString, filtr.sql, prepDataGrid);
        }
        //добавление сотрудника +
        private void allSotrAddButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("sotrid", System.Type.GetType("System.Int32"));
            table.Columns.Add("fio", System.Type.GetType("System.String"));
            table.Columns.Add("phone", System.Type.GetType("System.String"));
            table.Columns.Add("num_trud", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));

            ArrayList list = new ArrayList();
            for (int i = 0; i < allSotrDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = allSotrDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "" || rMas[2].ToString() == "" || rMas[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано одно из обязательных значений"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется имя сотрудника " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);

            }

            string sql = "select * from sotrudniki";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);

            allSotrDataGrid.SelectedItem = null;
            //все сотрудники
            allSotrDeleteButton.IsEnabled = false;
            allSotrToPrepBtton.IsEnabled = false;
            allSotrToShtatBtton.IsEnabled = false;
            DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, allSotrDataGrid);

        }
        //удаление из сотрудников+
        private void allSotrDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = allSotrDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка препода в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select raspisanie.prepid from sotrudniki inner join prep using(sotrid) inner join raspisanie using(prepid) where sotrid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Преподаватель используется в расписании"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
           

        MessageBoxResult res=    MessageBox.Show("Данные об этом сотруднике будут удалены из штатного расписания, преподавателей, штата и таблицы сотрудников.\n Так же будет удалена информация о начислениях и расходах.\n Удалить?","Предупреждение",MessageBoxButton.YesNo);

            if (res == MessageBoxResult.Yes)
            {
                //проверка в  х зп


                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from nachisl where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteReader();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                //проверка в расходах
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from rashody where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows) { MessageBox.Show("Запись в расходах связана с этим сотрудником"); return; }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                //удаление из штатного расписания
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "UPDATE shtatrasp SET shtatid=array_remove(shtatid," + arr[0] + ")";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                //удаление из преподавателей
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from prep where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                //удаление из штата
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from shtat where shtatid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "delete from sotrudniki where sotrid =" + arr[0];
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    command.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, allSotrDataGrid);

            allSotrDataGrid.SelectedItem = null;
            //все сотрудники
            allSotrDeleteButton.IsEnabled = false;
            allSotrToPrepBtton.IsEnabled = false;
            allSotrToShtatBtton.IsEnabled = false;

        }
        //удаление из преподавателей +
        private void prepDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = prepDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select prepid from raspisanie where prepid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Преподаватель используется в расписании"); return; }
                con.Close();
            }     catch { MessageBox.Show("Не удалось подключиться к базе данных1"); return; }

          
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("delete from prep where prepid =" + arr[0]);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных2"); return; }
            DataGridUpdater.updateDataGridPrep(connectionString, filtr.sql, prepDataGrid);

            prepDataGrid.SelectedItem = null;

            //преподаватели
            prepDeleteButton.IsEnabled = false;
            prepChangeButton.IsEnabled = false;
        }
        //переход к форме изменения преподавателей+
        private void prepChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = prepDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            prepID = Convert.ToInt32(arr[0]);
            prepFio.Text = arr[2].ToString();
            prepCom.Text = arr[4].ToString();
            string[] date1 = arr[3].ToString().Split(' ');
            string date2 = date1[0];
            dateStartAdd.Text = date2;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from kategorii";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                int itmeIndex = 0;
                kategCMBX.SelectedIndex = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kategCMBX.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { kategCMBX.SelectedIndex = itmeIndex; }
                        itmeIndex++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            HideAll();
            prepChangeGrid.Visibility = Visibility.Visible;
        }
        //переход к группам+
        private void groupsMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            groupsGrid.Visibility = Visibility.Visible;

            FiltrGridCourse.Children.Clear();
            FiltrGridCourse.ColumnDefinitions.Clear();
            filtr.CreateGroupFiltr(FiltrGridCourse);
            filtr.sql = "SELECT groups.grid as grid,  groups.nazvanie as gtitle,courses.title as ctitle, groups.comment as comment ,groups.payment[1],groups.payment[2],groups.payment[3],groups.payment[4],groups.payment[5],groups.payment[6],groups.payment[7],groups.payment[8],groups.payment[9],groups.payment[10],groups.payment[11],groups.payment[12],date_start,date_end FROM groups inner join courses using (courseid)  ";
            DataGridUpdater.updateDataGridGroups(connectionString, filtr.sql, groupsDataGrid);


        }
        //переход к гриду добавления группы+
        private void groupAddButton_Click(object sender, RoutedEventArgs e)
        {
            grTitle.Text = "";
            grComm.Text = "";
            grCourse.SelectedIndex = 0;
            grPayment1.Text = "";
            grPayment2.Text = "";
            grPayment3.Text = "";
            grPayment4.Text = "";
            grPayment5.Text = "";
            grPayment6.Text = "";
            grPayment7.Text = "";
            grPayment8.Text = "";
            grPayment9.Text = "";
            grPayment10.Text = "";
            grPayment11.Text = "";
            grPayment12.Text = "";
            payToYear.Content = "";
            HideAll();
            groupAddGrid.Visibility = Visibility.Visible;
            DateStartGrAdd.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
            DateEndGrAdd.Text = DateTime.Now.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from courses";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grCourse.Items.Add(reader.GetString(0));

                    }
                    grCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        //добавление группы +
        private void grAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (grTitle.Text == ""||payToYear.Content.ToString()==""|| DateStartGrAdd.Text==""|| DateEndGrAdd.Text == "") { MessageBox.Show("Поля не заполнены"); return; }
            //проверка существования группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("select grid from groups where nazvanie ='" + grTitle.Text + "'");
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Группа с таким названием уже существует");
                    return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение номера курса
            int courseID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select courseid from courses where title ='" + grCourse.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        courseID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //добавление группы
            double[] montPay = new double[12];
            if (grPayment1.Text != "") montPay[0] = Convert.ToDouble(grPayment1.Text);
            if (grPayment2.Text != "") montPay[1] = Convert.ToDouble(grPayment2.Text);
            if (grPayment3.Text != "") montPay[2] = Convert.ToDouble(grPayment3.Text);
            if (grPayment4.Text != "") montPay[3] = Convert.ToDouble(grPayment4.Text);
            if (grPayment5.Text != "") montPay[4] = Convert.ToDouble(grPayment5.Text);
            if (grPayment6.Text != "") montPay[5] = Convert.ToDouble(grPayment6.Text);
            if (grPayment7.Text != "") montPay[6] = Convert.ToDouble(grPayment7.Text);
            if (grPayment8.Text != "") montPay[7] = Convert.ToDouble(grPayment8.Text);
            if (grPayment9.Text != "") montPay[8] = Convert.ToDouble(grPayment9.Text);
            if (grPayment10.Text != "") montPay[9] = Convert.ToDouble(grPayment10.Text);
            if (grPayment11.Text != "") montPay[10] = Convert.ToDouble(grPayment11.Text);
            if (grPayment12.Text != "") montPay[11] = Convert.ToDouble(grPayment12.Text);
            //проверка оплаты за нужные месяца

            DateTime dateStartAdd = Convert.ToDateTime(DateStartGrAdd.Text);
            DateTime dateEndAdd = Convert.ToDateTime(DateEndGrAdd.Text);
            if (dateEndAdd.Year - dateStartAdd.Year == 1 || dateEndAdd.Year - dateStartAdd.Year == 0)
            {
                if (dateStartAdd.Month > dateEndAdd.Month)
                {
                    if (dateStartAdd.Year >= dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }

                    for (int i = dateStartAdd.Month-1; i < 12; i++)
                    {
                        if (montPay[i] == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return;
                        }
                    }

                    for (int i = 0; i <= dateEndAdd.Month-1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + "не стоит оплата, хотя он отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month < dateEndAdd.Month)
                {
                    if (dateStartAdd.Year != dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }
                    for (int i = 0; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < 12; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateStartAdd.Month - 1; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month == dateEndAdd.Month)
                {
                    if (dateStartAdd.Year == dateEndAdd.Year)
                    {
                        for (int i = 0; i < dateStartAdd.Month - 1; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце1 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                        for (int i = dateEndAdd.Month; i < 12; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце2 " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                    }
                    else
                    {System.Windows.Forms.MessageBox.Show("Дата введена не корректно"); return;}
                }
            }
           else { MessageBox.Show("Дата введена не корректно"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO groups(courseid, nazvanie, comment, payment,date_start,date_end) VALUES(" + courseID + ",'" + grTitle.Text + "' ,'" + grComm.Text + "' , '{"+montPay[0].ToString().Replace(',','.')+","+montPay[1].ToString().Replace(',', '.') + ","+montPay[2].ToString().Replace(',', '.') + ","+montPay[3].ToString().Replace(',', '.') + ","+ montPay[4].ToString().Replace(',', '.') + ","+ montPay[5].ToString().Replace(',', '.') + ","+ montPay[6].ToString().Replace(',', '.') + ","+ montPay[7].ToString().Replace(',', '.') + ","+ montPay[8].ToString().Replace(',', '.') + ","+ montPay[9].ToString().Replace(',', '.') + ","+ montPay[10].ToString().Replace(',', '.') + ","+ montPay[11].ToString().Replace(',', '.') + "}','"+dateStartAdd.ToShortDateString().Replace('.','-')+ "','" + dateEndAdd.ToShortDateString().Replace('.', '-') + "' ); ";
            NpgsqlCommand command = new NpgsqlCommand(sql, con);
            command.ExecuteNonQuery();
            con.Close();



        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
    var btn = MessageBox.Show("Группа добавлена. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (btn == MessageBoxResult.Yes)
            {
                grTitle.Text = "";
                grComm.Text = "";
                grCourse.SelectedIndex = 0;
                grPayment1.Text = "";
                grPayment2.Text = "";
                grPayment3.Text = "";
                grPayment4.Text = "";
                grPayment5.Text = "";
                grPayment6.Text = "";
                grPayment7.Text = "";
                grPayment8.Text = "";
                grPayment9.Text = "";
                grPayment10.Text = "";
                grPayment11.Text = "";
                grPayment12.Text = "";
                payToYear.Content = "";
                DateStartGrAdd.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
                DateEndGrAdd.Text = DateTime.Now.ToShortDateString();
            }
            if (btn == MessageBoxResult.No)
            {
                HideAll();
                groupsGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridGroups(connectionString,filtr.sql ,groupsDataGrid);
            }

        }
        //удаление группы +
        private void groupDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = groupsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid FROM raspisanie WHERE grid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Группа используется в расписании"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //проверка в слушателях
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select listenerid from listeners where grid @> ARRAY["+ arr[0] + "] ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Группа используется в слушателях"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //проверка в начислениях
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT listnuchid FROM listnuch where grid ="+ arr[0]+ " and isclose=0";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Группа используется в начислениях"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удаление
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = ("DELETE FROM groups WHERE grid = " + arr[0]);
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridGroups(connectionString, filtr.sql, groupsDataGrid);
            groupsDataGrid.SelectedItem = null;

            //группы
            groupDeleteButton.IsEnabled = false;
            groupChangeButton.IsEnabled = false;
        }
        //переход к изменению группы+
        private void groupChangeButton_Click(object sender, RoutedEventArgs e)
        {
            grTitleCh.Text = "";
            grCourseCh.Items.Clear();
            grPayment1Ch.Text = "";
            grPayment2Ch.Text = "";
            grPayment3Ch.Text = "";
            grPayment4Ch.Text = "";
            grPayment5Ch.Text = "";
            grPayment6Ch.Text = "";
            grPayment7Ch.Text = "";
            grPayment8Ch.Text = "";
            grPayment9Ch.Text = "";
            grPayment10Ch.Text = "";
            grPayment11Ch.Text = "";
            grPayment12Ch.Text = "";
            payToYearCh.Content = "";
            grCommCh.Text = "";
            DataRowView DRV = groupsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            HideAll();
            groupChangeGrid.Visibility = Visibility.Visible;
            object[] arr = DR.ItemArray;
            grID = Convert.ToInt32(arr[0]);
            grTitleCh.Text = arr[1].ToString();
            dontChGName = arr[1].ToString();
            payToYearCh.Content = arr[3].ToString();
            grCommCh.Text = arr[6].ToString();
            bool ListHasGr = false;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select listenerid from listeners where ARRAY["+grID+"] <@ grid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ListHasGr = true;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12],date_start,date_end from groups where grid =" + grID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grPayment1Ch.Text = reader.GetDouble(0).ToString();
                        if (reader.GetDouble(0) == 0 && ListHasGr == true) { grPayment1Ch.IsEnabled = false; }
                        grPayment2Ch.Text = reader.GetDouble(1).ToString();
                        if (reader.GetDouble(1) == 0 && ListHasGr == true) { grPayment2Ch.IsEnabled = false; }
                        grPayment3Ch.Text = reader.GetDouble(2).ToString();
                        if (reader.GetDouble(2) == 0 && ListHasGr == true) { grPayment3Ch.IsEnabled = false; }
                        grPayment4Ch.Text = reader.GetDouble(3).ToString();
                        if (reader.GetDouble(3) == 0 && ListHasGr == true) { grPayment4Ch.IsEnabled = false; }
                        grPayment5Ch.Text = reader.GetDouble(4).ToString();
                        if (reader.GetDouble(4) == 0 && ListHasGr == true) { grPayment5Ch.IsEnabled = false; }
                        grPayment6Ch.Text = reader.GetDouble(5).ToString();
                        if (reader.GetDouble(5) == 0 && ListHasGr == true) { grPayment6Ch.IsEnabled = false; }
                        grPayment7Ch.Text = reader.GetDouble(6).ToString();
                        if (reader.GetDouble(6) == 0 && ListHasGr == true) { grPayment7Ch.IsEnabled = false; }
                        grPayment8Ch.Text = reader.GetDouble(7).ToString();
                        if (reader.GetDouble(7) == 0 && ListHasGr == true) { grPayment8Ch.IsEnabled = false; }
                        grPayment9Ch.Text = reader.GetDouble(8).ToString();
                        if (reader.GetDouble(8) == 0 && ListHasGr == true) { grPayment9Ch.IsEnabled = false; }
                        grPayment10Ch.Text = reader.GetDouble(9).ToString();
                        if (reader.GetDouble(9) == 0 && ListHasGr == true) { grPayment10Ch.IsEnabled = false; }
                        grPayment11Ch.Text = reader.GetDouble(10).ToString();
                        if (reader.GetDouble(10) == 0 && ListHasGr == true) { grPayment11Ch.IsEnabled = false; }
                        grPayment12Ch.Text = reader.GetDouble(11).ToString();
                        if (reader.GetDouble(11) == 0 && ListHasGr == true) { grPayment12Ch.IsEnabled = false; }
                       DateStartCh.Text = reader.GetDateTime(12).ToShortDateString();
                        DateEndCh.Text = reader.GetDateTime(13).ToShortDateString();
                        if (ListHasGr == true) { DateStartCh.IsEnabled = false; DateEndCh.IsEnabled = false; }
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from courses";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                bool b = false;
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        grCourseCh.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { grCourseCh.SelectedIndex = i; b = true; }
                        i++;
                    }
                    if (b == false)
                        grCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        //переход к предметам+
        private void subjectMeny_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            subGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridSubjects(connectionString, subsDataGrid);
        }
        //удаление предмета +
        private void subDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = subsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка предмета в курсах
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from courses WHERE subs @> '{" + arr[0] + "}'; ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string title = "";
                    while (reader.Read())
                    {
                        title += reader.GetString(0) + " ";
                    }
                    MessageBox.Show("Этот предмет не удаётся удалить, он используется в курсах: " + title); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //проверка предмета в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from raspisanie where subid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Предмет используется в расписании"); return; }
                con.Close();
            }

            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //удаление из предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM subject WHERE subid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridSubjects(connectionString, subsDataGrid);

            subsDataGrid.SelectedItem = null;

            //предметы
            subDeleteButton.IsEnabled = false;

        }
        //добавление изменение предметов +
        private void subChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("subid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("commetn", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < subsDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = subsDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название предмета"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется название предмета " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from subject";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            subsDataGrid.SelectedItem = null;
 
            //предметы
            subDeleteButton.IsEnabled = false;


            DataGridUpdater.updateDataGridSubjects(connectionString, subsDataGrid);
        }
        //переход к гриду курсов+
        private void coursesMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            courseGrid.Visibility = Visibility.Visible;


            FiltrGridCourse.Children.Clear();
            FiltrGridCourse.ColumnDefinitions.Clear();


            filtr.CreateCourseFiltr(FiltrGridSubs);

            filtr.sql = "select courseid,title,comment FROM courses";

            DataGridUpdater.updateDataGridСourses(connectionString, filtr.sql, coursDataGrid);
        }
        //ввод только чисел с запятой +
        private void grPayment_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
               e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            { System.Windows.Forms.MessageBox.Show("Test");e.Handled = true;  }
            
        }
        //применение изменений в группе +
        private void grChangeAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (grTitleCh.Text == "" || payToYearCh.Content.ToString() == "") { MessageBox.Show("Поле названия или оплаты не заполнено"); return; }
            //проверка существования группы
            if (grTitleCh.Text != dontChGName)
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = ("select grid from groups where nazvanie ='" + grTitleCh.Text + "'");
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        MessageBox.Show("Группа с таким названием уже существует");
                        return;
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            //получение номера курса
            int courseID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select courseid from courses where title ='" + grCourseCh.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        courseID = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            double[] montPay = new double[12];
            if (grPayment1Ch.Text != "") montPay[0] = Convert.ToDouble(grPayment1Ch.Text);
            if (grPayment2Ch.Text != "") montPay[1] = Convert.ToDouble(grPayment2Ch.Text);
            if (grPayment3Ch.Text != "") montPay[2] = Convert.ToDouble(grPayment3Ch.Text);
            if (grPayment4Ch.Text != "") montPay[3] = Convert.ToDouble(grPayment4Ch.Text);
            if (grPayment5Ch.Text != "") montPay[4] = Convert.ToDouble(grPayment5Ch.Text);
            if (grPayment6Ch.Text != "") montPay[5] = Convert.ToDouble(grPayment6Ch.Text);
            if (grPayment7Ch.Text != "") montPay[6] = Convert.ToDouble(grPayment7Ch.Text);
            if (grPayment8Ch.Text != "") montPay[7] = Convert.ToDouble(grPayment8Ch.Text);
            if (grPayment9Ch.Text != "") montPay[8] = Convert.ToDouble(grPayment9Ch.Text);
            if (grPayment10Ch.Text != "") montPay[9] = Convert.ToDouble(grPayment10Ch.Text);
            if (grPayment11Ch.Text != "") montPay[10] = Convert.ToDouble(grPayment11Ch.Text);
            if (grPayment12Ch.Text != "") montPay[11] = Convert.ToDouble(grPayment12Ch.Text);

            DateTime dateStartAdd = Convert.ToDateTime(DateStartCh.Text);
            DateTime dateEndAdd = Convert.ToDateTime(DateEndCh.Text);
            if (dateEndAdd.Year - dateStartAdd.Year == 1 || dateEndAdd.Year - dateStartAdd.Year == 0)
            {
                if (dateStartAdd.Month > dateEndAdd.Month)
                {
                    if (dateStartAdd.Year >= dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }

                    for (int i = dateStartAdd.Month - 1; i < 12; i++)
                    {
                        if (montPay[i] == 0)
                        {
                            System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return;
                        }
                    }

                    for (int i = 0; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + "не стоит оплата, хотя он отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month < dateEndAdd.Month)
                {
                    if (dateStartAdd.Year != dateEndAdd.Year) { MessageBox.Show("Дата введена не корректно"); return; }
                    for (int i = 0; i < dateStartAdd.Month - 1; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateEndAdd.Month; i < 12; i++)
                    { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                    for (int i = dateStartAdd.Month - 1; i <= dateEndAdd.Month - 1; i++)
                    { if (montPay[i] == 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " не стоит оплата, хотя он отмечен как месяц обучения"); return; } }
                }

                if (dateStartAdd.Month == dateEndAdd.Month)
                {
                    if (dateStartAdd.Year == dateEndAdd.Year)
                    {
                        for (int i = 0; i < dateStartAdd.Month - 1; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }

                        for (int i = dateEndAdd.Month; i < 12; i++)
                        { if (montPay[i] != 0) { System.Windows.Forms.MessageBox.Show("В месяце " + (i + 1) + " стоит оплата, хотя он не отмечен как месяц обучения"); return; } }
                    }
                    else
                    { System.Windows.Forms.MessageBox.Show("Дата введена не корректно"); return; }
                }
            }
            else { MessageBox.Show("Дата введена не корректно"); return; }

            //изменение группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE groups SET date_start='"+dateStartAdd.ToShortDateString().Replace('.','-')+ "',date_end='" + dateEndAdd.ToShortDateString().Replace('.', '-') + "',courseid =" + courseID + ", nazvanie ='" + grTitleCh.Text + "', comment ='" + grCommCh.Text + "', payment ='{" + montPay[0].ToString().Replace(',', '.') + "," + montPay[1].ToString().Replace(',', '.') + "," + montPay[2].ToString().Replace(',', '.') + "," + montPay[3].ToString().Replace(',', '.') + "," + montPay[4].ToString().Replace(',', '.') + "," + montPay[5].ToString().Replace(',', '.') + "," + montPay[6].ToString().Replace(',', '.') + "," + montPay[7].ToString().Replace(',', '.') + "," + montPay[8].ToString().Replace(',', '.') + "," + montPay[9].ToString().Replace(',', '.') + "," + montPay[10].ToString().Replace(',', '.') + "," + montPay[11].ToString().Replace(',', '.') + "}' WHERE grid=" + grID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            HideAll();
            groupsGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridGroups(connectionString, filtr.sql, groupsDataGrid);

        }
        //переход к добавлению курса+
        private void coursAddButton_Click(object sender, RoutedEventArgs e)
        {
            courseTitle.Text = "";
            courseComm.Text = "";
            subsCanvas.Children.Clear();
            HideAll();
            courseAddGrid.Visibility = Visibility.Visible;
            int chbxMasLength = 0;
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(title) from subject ";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        chbxMasLength = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,subid from subject";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                chbxMas = new CheckBox[chbxMasLength];
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Name = "id" + reader.GetInt32(1).ToString();
                        chbxMas[i].Content = reader.GetString(0);

                        subsCanvas.Children.Add(chbxMas[i]);
                        Canvas.SetLeft(chbxMas[i], 1);
                        Canvas.SetTop(chbxMas[i], i * 15);
                        i++;

                    }
                }
                subsCanvas.Height = i * 15;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //добавление курса +
        private void courseAddButton_Click(object sender, RoutedEventArgs e)
        {
            string subsMas = "'{";
            bool b = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true)
                {
                    b = true;
                    subsMas += chbxMas[i].Name.Substring(2) + ",";
                }
                chbxMas[i].IsChecked = false;
            }
            subsMas = subsMas.Substring(0, subsMas.Length - 1);
            subsMas += "}'";
            if (b == false || courseTitle.Text == "") { MessageBox.Show("Название курса или предметы не добавлены"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(courseid) from courses where title='" + courseTitle.Text + "'";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader = command1.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        if (reader.GetInt32(0) > 1) { MessageBox.Show("Такое название курса уже существует"); return; }
                    }

                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "INSERT INTO courses(title, subs, comment) VALUES('" + courseTitle.Text + "', " + subsMas + ", '" + courseComm.Text + "'); ";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            var mbx = MessageBox.Show("Курс добавлен. \n Продолжить добавление?", "Добавить", MessageBoxButton.YesNo);
            if (mbx == MessageBoxResult.Yes)
            {
                courseTitle.Text = "";
                courseComm.Text = "";
            }
            else
            {
                HideAll();
                courseGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridСourses(connectionString, filtr.sql, coursDataGrid);

            }
        }
        //переход к гриду изменения курсов+
        private void coursChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = coursDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            subsChangeCanvas.Children.Clear();
            HideAll();
            courseChangeGrid.Visibility = Visibility.Visible;
            object[] arr = DR.ItemArray;
            courseID = Convert.ToInt32(arr[0]);
            courseChangeTitle.Text = arr[1].ToString();
            dontChCName = arr[1].ToString();
            courseChangeComm.Text = arr[3].ToString();
            object[] masSubjects = arr[2].ToString().Replace(" ", "").Split(',');
            ArrayList list = new ArrayList(masSubjects);
            int chbxMasLength = 0;
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(title) from subject ";
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = command1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        chbxMasLength = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,subid from subject";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                chbxMas = new CheckBox[chbxMasLength];
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Name = "id" + reader.GetInt32(1).ToString();
                        chbxMas[i].Content = reader.GetString(0);
                        if (list.IndexOf(reader.GetString(0)) != -1) { chbxMas[i].IsChecked = true; }
                        subsChangeCanvas.Children.Add(chbxMas[i]);
                        Canvas.SetLeft(chbxMas[i], 1);
                        Canvas.SetTop(chbxMas[i], i * 15);
                        i++;

                    }
                }
                subsCanvas.Height = i * 15;
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //сохранение изменений курса +
        private void courseChangeButton2_Click(object sender, RoutedEventArgs e)
        {

            string subsMas = "'{";
            bool b = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true)
                {
                    b = true;
                    subsMas += chbxMas[i].Name.Substring(2) + ",";
                }
            }
            subsMas = subsMas.Substring(0, subsMas.Length - 1);
            subsMas += "}'";
            if (b == false || courseChangeTitle.Text == "") { MessageBox.Show("Название курса или предметы не добавлены"); return; }
            if (dontChCName != courseChangeTitle.Text)
            {
                try
                {
                    NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                    con1.Open();
                    string sql1 = "select count(courseid) from courses where title='" + courseChangeTitle.Text + "'";
                    NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                    NpgsqlDataReader reader = command1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {

                            if (reader.GetInt32(0) > 0) { MessageBox.Show("Такое название курса уже существует"); return; }
                        }

                    }
                    con1.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "UPDATE courses SET title ='" + courseChangeTitle.Text + "', subs =" + subsMas + ", comment ='" + courseChangeComm.Text + "' WHERE courseid=" + courseID;
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            HideAll();
            courseGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridСourses(connectionString, filtr.sql, coursDataGrid);
        }
        //удаление курса +
        private void coursDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = coursDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //удаление курса из групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie from groups where courseid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    string groups = "";
                    while (reader.Read())
                    {

                        groups += reader.GetString(0) + " ";

                    }
                    MessageBox.Show("Этот курс нельзя удалить, он используется в группах: " + groups);
                    return;

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM courses WHERE courseid=" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridСourses(connectionString, filtr.sql, coursDataGrid);

            coursDataGrid.SelectedItem = null;

            //курсы
            coursDeleteButton.IsEnabled = false;
            coursChangeButton.IsEnabled = false;

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
        //клик по меню прав +
        private void MenuRolesD_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesD.BorderBrush = Brushes.DarkRed;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = null;
            obuchMenu.BorderBrush = null;
            skidki.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
        }
        //клик по меню расписания +
        private void raspMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = Brushes.DarkRed;
            sotrMenu.BorderBrush = null;
            obuchMenu.BorderBrush = null;
            skidki.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
        }
        //клик по меню персонала +
        private void sotrMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = Brushes.DarkRed;
            obuchMenu.BorderBrush = null;
            skidki.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
        }
        //клик по меню обучения +
        private void obuchMenu_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = null;
            skidki.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
            obuchMenu.BorderBrush = Brushes.DarkRed;
        }
        //сделать сотрудника преподавателем(переход к гриду)+
        private void allSotrToPrepBtton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = allSotrDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            sotrID = Convert.ToInt32(arr[0]);
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select prepid from prep where sotrid=" + sotrID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Сотрудник уже является преподавателем"); con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            HideAll();
            kategCMB.Items.Clear();
            addPrepGrid.Visibility = Visibility.Visible;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title from kategorii";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        kategCMB.Items.Add(reader.GetString(0));

                    }
                    kategCMB.SelectedIndex = 0;
                    dateStart.Text = DateTime.Now.ToShortDateString();
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        //сделать сотрудника штатным(переход к гриду)+
        private void allSotrToShtatBtton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = allSotrDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            sotrID = Convert.ToInt32(arr[0]);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select shtatid from shtat where sotrid=" + sotrID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                { MessageBox.Show("Сотрудник уже является штатным работником"); con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            HideAll();
            addShtatGrid.Visibility = Visibility.Visible;
            ObslWorks.Children.Clear();
            ObslWorks.RowDefinitions.Clear();
            States.Children.Clear();
            States.RowDefinitions.Clear();
            int kol_states = -1, kol_obsWork = -1;
            //получени е кол-ва должностей
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(distinct title) from states";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_states = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение кол-ва облс. работ
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(distinct title) from raboty_obsl";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_obsWork = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            tbxMas_stavki = new TextBox[kol_states];
            chbxMas_state = new CheckBox[kol_states];
            tbxMas_obem=new TextBox[kol_obsWork];
            chbxMas_obslwork = new CheckBox[kol_obsWork];

            //заполнение грида должностей 
            Label l1 = new Label();
            l1.Content = "Должность";
            Label l2 = new Label();
            l2.Content = "Ставка";

            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            States.RowDefinitions.Add(rwd1);

            Grid.SetRow(l1,0);
            Grid.SetRow(l2,0);

            Grid.SetColumn(l2, 1);
            Grid.SetColumn(l1, 0);

            States.Children.Add(l1);
            States.Children.Add(l2);

           
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select statesid,title from states order by statesid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        tbxMas_stavki[i] = new TextBox();
                        chbxMas_state[i] = new CheckBox();

                        chbxMas_state[i].Name = "Name_" + i + "_" + reader.GetInt32(0)+"_state";
                        chbxMas_state[i].Content = reader.GetString(1);
                        chbxMas_state[i].Checked += Shtat_Checked;
                        chbxMas_state[i].Unchecked += Shtat_UnChecked;

                        tbxMas_stavki[i].IsEnabled = false;
                        tbxMas_stavki[i].PreviewTextInput += grPayment_PreviewTextInput;

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height =new GridLength(40);

                        States.RowDefinitions.Add(rwd);

                        Grid.SetRow(tbxMas_stavki[i], (i+1));
                        Grid.SetRow(chbxMas_state[i], (i+1));

                        Grid.SetColumn(tbxMas_stavki[i], 1);
                        Grid.SetColumn(chbxMas_state[i], 0);

                        States.Children.Add(tbxMas_stavki[i]);
                        States.Children.Add(chbxMas_state[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //заполнение грида работ 

            Label l11 = new Label();
            l11.Content = "Работа";
            Label l22 = new Label();
            l22.Content = "Объём";
            Label l33 = new Label();
            l33.Content = "Еденицы измерения";

            RowDefinition rwd11 = new RowDefinition();
            rwd11.Height = new GridLength(40);

            ObslWorks.RowDefinitions.Add(rwd11);

            Grid.SetRow(l11, 0);
            Grid.SetRow(l22, 0);
            Grid.SetRow(l33, 0);

            Grid.SetColumn(l22, 1);
            Grid.SetColumn(l11, 0);
            Grid.SetColumn(l33, 2);

            ObslWorks.Children.Add(l11);
            ObslWorks.Children.Add(l22);
            ObslWorks.Children.Add(l33);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select rabotyid,title,ed_izm from raboty_obsl order by rabotyid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        tbxMas_obem[i] = new TextBox();
                        chbxMas_obslwork[i] = new CheckBox();
                        Label lb = new Label();
                        
                        chbxMas_obslwork[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        chbxMas_obslwork[i].Content = reader.GetString(1);
                        chbxMas_obslwork[i].Checked += Shtat_Checked;
                        chbxMas_obslwork[i].Unchecked += Shtat_UnChecked;

                        tbxMas_obem[i].IsEnabled = false;
                        tbxMas_obem[i].PreviewTextInput += grPayment_PreviewTextInput;

                        lb.Content = reader.GetString(2);

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        ObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(tbxMas_obem[i], (i + 1));
                        Grid.SetRow(chbxMas_obslwork[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(tbxMas_obem[i], 1);
                        Grid.SetColumn(chbxMas_obslwork[i], 0);
                        Grid.SetColumn(lb,2);

                        ObslWorks.Children.Add(tbxMas_obem[i]);
                        ObslWorks.Children.Add(chbxMas_obslwork[i]);
                        ObslWorks.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        //чекбокс в штате +
        private void Shtat_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name.Split('_')[3] == "state")
            {
                tbxMas_stavki[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
            if (cb.Name.Split('_')[3] == "obsl")
            {
                tbxMas_obem[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
        }
        private void Shtat_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name.Split('_')[3] == "state")
            {
                tbxMas_stavki[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = false;
            }
            if (cb.Name.Split('_')[3] == "obsl")
            {
                tbxMas_obem[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = false;
            }
        }
        //добавление сотрудника в таблицу преподавателей +
        private void addToPrerB_Click(object sender, RoutedEventArgs e)
        {

            if (dateStart.Text == "") { MessageBox.Show("Дата начала работы не выбрана"); return; }
            int kategID = -1;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select kategid from kategorii where title='" + kategCMB.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kategID = reader.GetInt32(0);
                    }

                }
                else { con.Close(); return; }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO prep(kategid, date_start, sotrid) VALUES(" + kategID + ", '" + dateStart.Text + "', " + sotrID + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBox.Show("Сотрудник определён как преподаватель");
            DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, allSotrDataGrid);
            HideAll();
            allSotrGrid.Visibility = Visibility.Visible;

        }
        //вывод расписания по групрпам +
        public void showRaspG(DateTime dm, DateTime ds)
        {
            m = 0;//число зантий в дне
            n = 0;//число групп
            LabelDateRasp.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(grid) from groups";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (n == 0) { MessageBox.Show("Нету групп"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (m == 0) { MessageBox.Show("Нету занятий"); return; }
            HideAll();
            DeleteRaspBut.IsEnabled = false;
            ChangeRaspBut.IsEnabled = false;
            AddRaspBut.IsEnabled = false;
            raspGridG.Visibility = Visibility.Visible;
            lbmas = new Label[(m * 7) + 1, n + 2];
            DataGridUpdater.updateGridRaspG(connectionString, tG, m, n, lbmas, dm, ds);
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].MouseDown += Label_MouseDown;
                }

            }
        }
        //переход к расписанию уроков по группам +
        private void yrokiMenu_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            dateMonday = new DateTime();
            int day_razn = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { day_razn = 0; } break;
                case "Tuesday": { day_razn = -1; } break;
                case "Wednesday": { day_razn = -2; } break;
                case "Thursday": { day_razn = -3; } break;
                case "Friday": { day_razn = -4; } break;
                case "Saturday": { day_razn = -5; } break;
                case "Sunday": { day_razn = -6; } break;
            }
            dateMonday = dateNow.AddDays(day_razn);
            showRaspG(dateMonday, dateMonday.AddDays(6));
        }
        //клик на лейбл в расписании по группам +
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Label l = sender as Label;
            iRaspLebale = Convert.ToInt32(l.Name.Split('_')[1]);
            jRaspLebale = Convert.ToInt32(l.Name.Split('_')[2]);
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].Background = Brushes.White;
                }
            }
            lbmas[iRaspLebale, jRaspLebale].Background = Brushes.Aqua;
            DeleteRaspBut.IsEnabled = false;
            ChangeRaspBut.IsEnabled = false;
            AddRaspBut.IsEnabled = false;
            DeleteRaspButС.IsEnabled = false;
            ChangeRaspButС.IsEnabled = false;
            AddRaspButС.IsEnabled = false;
            DeleteRaspButP.IsEnabled = false;
            ChangeRaspButP.IsEnabled = false;
            AddRaspButP.IsEnabled = false;
            if (lbmas[iRaspLebale, jRaspLebale].Content.ToString() == "") { AddRaspBut.IsEnabled = true; AddRaspButС.IsEnabled = true; AddRaspButP.IsEnabled = true; }
            if (lbmas[iRaspLebale, jRaspLebale].Content.ToString() != "")
            {
                DeleteRaspBut.IsEnabled = true;
                ChangeRaspBut.IsEnabled = true;

                DeleteRaspButС.IsEnabled = true;
                ChangeRaspButС.IsEnabled = true;

                DeleteRaspButP.IsEnabled = true;
                ChangeRaspButP.IsEnabled = true;
            }
        }
        //добавление записив таблицу расписания по группам +
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int prepid = -1;
            int cabid = -1;
            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + raspAddKab.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cabid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspAddSubs.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id препода
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + raspAddPrep.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prepid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int grid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspAddDayOfWeek.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //вставка записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO raspisanie(grid, lesson_number, subid, prepid, date, day,cabid) VALUES(" + grid + ", " + raspAddLesNum.Text + ", " + subid + ", " + prepid + ", '" + raspAddDate.Text.Replace('.', '-') + "', " + day + ","+cabid+"); ";
                
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspG(dateMonday, dateMonday.AddDays(6));

        }
        //сохранение изменений в расписании по группам +
        private void raspChangeButton_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int prepid = -1;
            int cabid = -1;

            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + raspChangeKab.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cabid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspChangeSubs.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id препода
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + raspChangePrep.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prepid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int grid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspChangeDayOfWeek.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //обновление записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE raspisanie SET subid=" + subid + ", prepid=" + prepid + ",cabid = "+cabid+" WHERE grid=" + grid + " and  lesson_number=" + raspChangeLesNum.Text + " and date='" + raspChangeDate.Text.Replace('.', '-') + "' and day=" + day;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspG(dateMonday, dateMonday.AddDays(6));
        }
        //изменение записи в расписании(переход к форме) по группам+
        private void ChangeRaspBut_Click(object sender, RoutedEventArgs e)
        {
            int grid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            raspChangeSubs.Items.Clear();
            raspChangePrep.Items.Clear();
            raspChangeKab.Items.Clear();
            //вывод предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where grid =" + grid + " )  @> ARRAY[subid]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    raspChangeSubs.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        raspChangeSubs.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[0]) { raspChangeSubs.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == false) {
                    raspChangePrep.SelectedIndex = 0;
                    raspChangePrep.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    raspChangePrep.SelectedIndex = 0;
                    raspChangePrep.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        raspChangePrep.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]) { raspChangePrep.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) {
                    raspChangeKab.SelectedIndex = 0;
                    raspChangeKab.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    raspChangeKab.SelectedIndex = 0;
                    raspChangeKab.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                    while (reader.Read())
                    {
                        raspChangeKab.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]) { raspChangeKab.SelectedIndex = i; }
                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            
            switch (day + 1)
            {
                case 1: { raspChangeDayOfWeek.Text = "Понедельник"; } break;
                case 2: { raspChangeDayOfWeek.Text = "Вторник"; } break;
                case 3: { raspChangeDayOfWeek.Text = "Среда"; } break;
                case 4: { raspChangeDayOfWeek.Text = "Четверг"; } break;
                case 5: { raspChangeDayOfWeek.Text = "Пятница"; } break;
                case 6: { raspChangeDayOfWeek.Text = "Суббота"; } break;
                case 7: { raspChangeDayOfWeek.Text = "Воскресенье"; } break;
            }
            raspChangeDate.Text = dateMonday.AddDays(day).ToShortDateString();
            raspChangeLesNum.Text = "" + lesNum;
            raspChangeGr.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            changeRaspGrid.Visibility = Visibility.Visible;
        }
        //переход к форме добавления записи в расписание по группам+
        private void AddRaspBut_Click(object sender, RoutedEventArgs e)
        {
            int grid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            //добавление
            raspAddSubs.Items.Clear();
            raspAddPrep.Items.Clear();
            raspAddKab.Items.Clear();
            //вывод предметов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where grid =" + grid + " )  @> ARRAY[subid]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        raspAddSubs.Items.Add(reader.GetString(0));
                    }
                    raspAddSubs.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного преподавателя"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        raspAddPrep.Items.Add(reader.GetString(0));
                    }
                    raspAddPrep.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lesNum + " and day= "+(day+1)+ " and EXTRACT(day FROM date)=" + dayRasp.Day+ " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного кабинета"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        raspAddKab.Items.Add(reader.GetString(0));
                    }
                    raspAddKab.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            switch (day + 1)
            {
                case 1: { raspAddDayOfWeek.Text = "Понедельник"; } break;
                case 2: { raspAddDayOfWeek.Text = "Вторник"; } break;
                case 3: { raspAddDayOfWeek.Text = "Среда"; } break;
                case 4: { raspAddDayOfWeek.Text = "Четверг"; } break;
                case 5: { raspAddDayOfWeek.Text = "Пятница"; } break;
                case 6: { raspAddDayOfWeek.Text = "Суббота"; } break;
                case 7: { raspAddDayOfWeek.Text = "Воскресенье"; } break;
            }

            raspAddDate.Text = dateMonday.AddDays(day).ToShortDateString();
            raspAddLesNum.Text = "" + lesNum;
            raspAddGr.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            addRaspGrid.Visibility = Visibility.Visible;

        }
        //удаление записи из расписания по группам +
        private void DeleteRaspBut_Click(object sender, RoutedEventArgs e)
        {
            int grid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from raspisanie where grid =" + grid + " and lesson_number = " + lesNum + " and day=" + (day + 1) + " and date='" + dateMonday.AddDays(day).ToShortDateString().Replace('.', '-') + "'";
                System.Windows.Forms.MessageBox.Show(sql);
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspG(dateMonday, dateMonday.AddDays(6));
        }
        //клик на кнопку в расписнии "Предидущее" по группам+
        private void PrevRaspBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where day = 1 and date<'" + dateMonday.ToShortDateString().Replace('.', '-') + "' order by  date desc limit 1";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("Старее расписания нет"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dateMonday = reader.GetDateTime(0);
                    }
                }

                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            LabelDateRasp.Content = "Расписание на " + dateMonday.ToShortDateString() + " - " + dateMonday.AddDays(6).ToShortDateString();
            Button but = sender as Button;
            if(but.Name== "PrevRaspBut") showRaspG(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "PrevRaspButP") showRaspP(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "PrevRaspButС") showRaspС(dateMonday, dateMonday.AddDays(6));

            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].MouseDown += Label_MouseDown;
                }

            }
        }
        //клик на кнопку в расписнии "Следующее" по группам+
        private void NextRaspBut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where day = 1 and date>'" + dateMonday.ToShortDateString().Replace('.', '-') + "' order by  date limit 1";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("Новее расписания нет"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dateMonday = reader.GetDateTime(0);
                    }
                }

                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            LabelDateRasp.Content = "Расписание на " + dateMonday.ToShortDateString() + " - " + dateMonday.AddDays(6).ToShortDateString();
            Button but = sender as Button;
            if (but.Name == "NextRaspBut") showRaspG(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "NextRaspButP") showRaspP(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "NextRaspButС") showRaspС(dateMonday, dateMonday.AddDays(6));
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].MouseDown += Label_MouseDown;
                }

            }
        }
        //клик на кнопку в расписнии "На эту неделю" по группам+
        private void NuwRaspBut_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            dateMonday = new DateTime();
            int day_razn = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { day_razn = 0; } break;
                case "Tuesday": { day_razn = -1; } break;
                case "Wednesday": { day_razn = -2; } break;
                case "Thursday": { day_razn = -3; } break;
                case "Friday": { day_razn = -4; } break;
                case "Saturday": { day_razn = -5; } break;
                case "Sunday": { day_razn = -6; } break;
            }

            dateMonday = dateNow.AddDays(day_razn);
            Button but = sender as Button;
            if (but.Name == "NuwRaspBut") { showRaspG(dateMonday, dateMonday.AddDays(6)); }
            if (but.Name == "NuwRaspButP") { showRaspP(dateMonday, dateMonday.AddDays(6)); }
            if (but.Name == "NuwRaspButС") { showRaspС(dateMonday, dateMonday.AddDays(6)); }
        }
        //расписание на новую неделю по группам+
        private void NewRaspBut_Click(object sender, RoutedEventArgs e)
        {
            DateIn wind = new DateIn();
            wind.gridToRas.Visibility = Visibility.Visible;
            wind.ShowDialog();
            DateTime dm = wind.getDm();
            if (dm.Day == 1 && dm.Month == 1 && dm.Year == 1) { return; }
            dateMonday = dm;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT  distinct date FROM raspisanie where  date='" + dateMonday.ToShortDateString().Replace('.', '-') + "'";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = comand.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Расписание на эту неделю уже есть"); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            Button but = sender as Button;
            if (but.Name == "NewRaspBut") showRaspG(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "NewRaspButP") showRaspP(dateMonday, dateMonday.AddDays(6));
            if (but.Name == "NewRaspButС") showRaspС(dateMonday, dateMonday.AddDays(6));

        }
        //переход к слушателям+
        private void listenerMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ListenerGrid.Visibility = Visibility.Visible;
            FiltrGridGroups.Children.Clear();
            FiltrGridGroups.ColumnDefinitions.Clear();
            filtr.CreateListenersFiltr(FiltrGridGroups);
            filtr.sql = "SELECT listenerid,  fio,  phones, comment,array_length(grid, 1) as grid FROM listeners order by listenerid";
            DataGridUpdater.updateDataGridListener(connectionString, filtr.sql, listenerDataGrid);
        }
        //переход к добавлению слушателя+
        private void listenerAddButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ListenerAddGrid.Visibility = Visibility.Visible;
            gr_lg.Children.Clear();
            gr_lg.RowDefinitions.Clear();
            listenerFIO.Text = "";
            listenerPhones.Text = "";
            listenerComm.Text = "";
            int countGr = 0;
            try 
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(nazvanie) from groups";
                NpgsqlCommand com1= new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        countGr=reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            chbxMas_gr_lg = new CheckBox[countGr];
            tbxMas_gr_lg = new TextBox[countGr];


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie from groups order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        chbxMas_gr_lg[i] = new CheckBox();
                        chbxMas_gr_lg[i].Unchecked += CheckBox_Unchecked;
                        chbxMas_gr_lg[i].Checked += CheckBox_Checked;
                        tbxMas_gr_lg[i] = new TextBox();
                        tbxMas_gr_lg[i].PreviewTextInput += grPayment_PreviewTextInput;
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);
                    gr_lg.RowDefinitions.Add(rwd);
                        tbxMas_gr_lg[i].IsEnabled = false;
                        chbxMas_gr_lg[i].Content = reader.GetString(0)+"-льгота: ";
                        chbxMas_gr_lg[i].Name = "chbxMasgrlg_" + i;
                        Grid.SetRow(chbxMas_gr_lg[i], i);
                        Grid.SetColumn(chbxMas_gr_lg[i], 0);
                    gr_lg.Children.Add(chbxMas_gr_lg[i]);
                        Grid.SetRow(tbxMas_gr_lg[i], i);
                        Grid.SetColumn(tbxMas_gr_lg[i], 1);
                    gr_lg.Children.Add(tbxMas_gr_lg[i]);

                    i++;
                }
                }
                con.Close();
        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
}
        //добавление слушателя в базу +
        private void listenerAddBut_Click(object sender, RoutedEventArgs e)
        {
            if (listenerFIO.Text == "" || listenerPhones.Text == "") { MessageBox.Show("Поля не заполнены или заполнены не правильно."); return; }
            bool b = false;
            string grLgMas = "'{";
            ArrayList ls = new ArrayList();
            string sql = "select grid from groups where ";
            for (int i = 0; i < chbxMas_gr_lg.Length; i++)
            {
                if (chbxMas_gr_lg[i].IsChecked == true) 
                { b = true; 
                    sql+="nazvanie='"+chbxMas_gr_lg[i].Content.ToString().Substring(0, chbxMas_gr_lg[i].Content.ToString().Length-9)+"' or ";
                    if (tbxMas_gr_lg[i].Text != "" && Convert.ToDouble(tbxMas_gr_lg[i].Text) > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }
                    if (tbxMas_gr_lg[i].Text != "") grLgMas += Convert.ToDouble(tbxMas_gr_lg[i].Text).ToString().Replace(',', '.') + ",";
                    else
                        grLgMas += "0,";
                }
               
            }
            grLgMas = grLgMas.Substring(0, grLgMas.Length - 1) + "}'";
            sql = sql.Substring(0, sql.Length - 3)+" order by grid";
            if (b == false) { System.Windows.Forms.MessageBox.Show("Группа не выбрана");  return; }
            string grMasId = "'{";
            ArrayList GroupList = new ArrayList();
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        grMasId+= reader.GetInt32(0).ToString()+",";
                        GroupList.Add(reader.GetInt32(0));
                    }

                }
                con.Close();
                grMasId = grMasId.Substring(0, grMasId.Length - 1) + "}'";
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int listid = -1;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql2 = "INSERT INTO listeners(fio, phones, grid, lgt, comment)VALUES ('" + listenerFIO.Text + "', '" + listenerPhones.Text + "', " + grMasId + ", " + grLgMas + ", '" + listenerComm.Text + "') returning listenerid";
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
               NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        listid = reader.GetInt32(0);
                    }
                
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            //добавление записей в таблицу оплаты
            for (int i = 0; i < GroupList.Count; i++)
            {
                double [] payMonth = new double[12];
                //получение массива оплаты за группу
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sqll = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12] from groups where grid= " + GroupList[i];
                   
                    NpgsqlCommand com = new NpgsqlCommand(sqll,con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            payMonth[0]= reader.GetDouble(0);
                            payMonth[1] = reader.GetDouble(1);
                            payMonth[2] = reader.GetDouble(2);
                            payMonth[3] = reader.GetDouble(3);
                            payMonth[4] = reader.GetDouble(4);
                            payMonth[5] = reader.GetDouble(5);
                            payMonth[6] = reader.GetDouble(6);
                            payMonth[7] = reader.GetDouble(7);
                            payMonth[8] = reader.GetDouble(8);
                            payMonth[9] = reader.GetDouble(9);
                            payMonth[10] = reader.GetDouble(10);
                            payMonth[11] = reader.GetDouble(11);
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                //вычисление оплаты для студента
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sqll = "select lgt[array_position(grid,'"+ GroupList[i] + "')] from listeners where listenerid= " + listid;

                    NpgsqlCommand com = new NpgsqlCommand(sqll, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            payMonth[0] = Math.Round(payMonth[0] - payMonth[0]* reader.GetDouble(0)/100,2);
                            payMonth[1] = Math.Round(payMonth[1] - payMonth[1] * reader.GetDouble(0) / 100, 2);
                            payMonth[2] = Math.Round(payMonth[2] - payMonth[2] * reader.GetDouble(0) / 100, 2);
                            payMonth[3] = Math.Round(payMonth[3] - payMonth[3] * reader.GetDouble(0) / 100, 2);
                            payMonth[4] = Math.Round(payMonth[4] - payMonth[4] * reader.GetDouble(0) / 100, 2);
                            payMonth[5] = Math.Round(payMonth[5] - payMonth[5] * reader.GetDouble(0) / 100, 2);
                            payMonth[6] = Math.Round(payMonth[6] - payMonth[6] * reader.GetDouble(0) / 100, 2);
                            payMonth[7] = Math.Round(payMonth[7] - payMonth[7] * reader.GetDouble(0) / 100, 2);
                            payMonth[8] = Math.Round(payMonth[8] - payMonth[8] * reader.GetDouble(0) / 100, 2);
                            payMonth[9] = Math.Round(payMonth[9] - payMonth[9] * reader.GetDouble(0) / 100, 2);
                            payMonth[10] = Math.Round(payMonth[10] - payMonth[10] * reader.GetDouble(0) / 100, 2);
                            payMonth[11] = Math.Round(payMonth[11] - payMonth[11] * reader.GetDouble(0) / 100, 2);
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                string masPay = "'{";
                for (int i2 = 0; i2 < 12; i2++)
                {
                    masPay += payMonth[i2].ToString().Replace(',','.') + ",";
                }
                masPay=masPay.Substring(0, masPay.Length - 1) + "}'";
                //добавление записи в таблицу
                try
                {

                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql2 = "INSERT INTO listnuch(listenerid, grid, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose) VALUES("+listid+", "+ GroupList[i] + ", "+ masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', '{0,0,0,0,0,0,0,0,0,0,0,0}', " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', null, 0)";
                    NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                   com.ExecuteNonQuery();
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                
            }


            MessageBoxResult res = MessageBox.Show("Слушатель добавлен. \n Продолжить добавление?", "Добавление", MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                listenerFIO.Text = "";
                listenerPhones.Text = "";
                listenerComm.Text = "";
                for (int i = 0; i < chbxMas_gr_lg.Length; i++)
                { chbxMas_gr_lg[i].IsChecked = false; }
            }
            if (res == MessageBoxResult.No)
            {
                HideAll();

                listenerDataGrid.SelectedItem = null;
                //слушатели
                listenerDeleteButton.IsEnabled = false;
                listenerChangeButton.IsEnabled = false;

                DataGridUpdater.updateDataGridListener(connectionString, filtr.sql, listenerDataGrid);
                ListenerGrid.Visibility = Visibility.Visible;

            }


        }
        //удаление слушателя  + 
        private void listenerDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = listenerDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            //проверка на долги 
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select listdolgid from listdolg where listenerid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Вы не можете удалить этого слушателя, так как у него есть долги"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //проверка закрыта ли оплата у этого слушателя
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid from listnuch where isclose=0 and listenerid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
               NpgsqlDataReader reader= command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Вы не можете удалить этого слушателя, так как у него есть не закрытые оплаты"); return; }
                con.Close();
        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //удлаение из таблицы
            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "delete from listeners  where listenerid =" + arr[0];
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {

                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "delete from listnuch  where listenerid =" + arr[0];
                NpgsqlCommand command1 = new NpgsqlCommand(sql1, con1);
                command1.ExecuteNonQuery();
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            

            listenerDataGrid.SelectedItem = null;

            //слушатели
            listenerDeleteButton.IsEnabled = false;
            listenerChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridListener(connectionString, filtr.sql, listenerDataGrid);

        }
        //переход к гриду изменения слушателя+
        private void listenerChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = listenerDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Невозможно изменить, Вы не выбрали запись для изменения."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            listenerID = Convert.ToInt32(arr[0]);

            listenerFIOCh.Text = "";
            listenerPhonesCh.Text = "";
            listenerCommCh.Text = "";
            gr_lgCh.Children.Clear();
            gr_lgCh.RowDefinitions.Clear();

            ArrayList GrMas=new ArrayList();
            String []LGMas;
            string lgmas = "";

            try {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select array_to_string(lgt,'_') from listeners where listenerid= " + arr[0];
                NpgsqlCommand com1 = new NpgsqlCommand(sql1,con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        lgmas = reader1.GetString(0);
                    
                    }
                }
                con1.Close();

            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select nazvanie from groups where ARRAY[grid] <@ (select grid from listeners where listenerid="+arr[0]+") order by grid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        GrMas.Add(reader1.GetString(0));

                    }
                }
                con1.Close();

            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            LGMas = lgmas.Split('_');

            int countGr = 0;
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(nazvanie) from groups";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        countGr = reader1.GetInt32(0);
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            chbxMas_gr_lg = new CheckBox[countGr];
            tbxMas_gr_lg = new TextBox[countGr];


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie from groups order by grid";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        chbxMas_gr_lg[i] = new CheckBox();
                        chbxMas_gr_lg[i].Unchecked += CheckBox_Unchecked;
                        chbxMas_gr_lg[i].Checked += CheckBox_Checked;
                        tbxMas_gr_lg[i] = new TextBox();
                        tbxMas_gr_lg[i].PreviewTextInput += grPayment_PreviewTextInput;
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);
                        gr_lgCh.RowDefinitions.Add(rwd);

                        tbxMas_gr_lg[i].IsEnabled = false;
                        chbxMas_gr_lg[i].Content = reader.GetString(0) + "-льгота: ";
                        chbxMas_gr_lg[i].Name = "chbxMasgrlg_" + i;
                    if (GrMas.IndexOf(reader.GetString(0)) != -1) { chbxMas_gr_lg[i].IsChecked = true; tbxMas_gr_lg[i].Text = LGMas[GrMas.IndexOf(reader.GetString(0))]; }
                      
                            Grid.SetRow(chbxMas_gr_lg[i], i);
                        Grid.SetColumn(chbxMas_gr_lg[i], 0);
                        gr_lgCh.Children.Add(chbxMas_gr_lg[i]);
                        Grid.SetRow(tbxMas_gr_lg[i], i);
                        Grid.SetColumn(tbxMas_gr_lg[i], 1);
                        gr_lgCh.Children.Add(tbxMas_gr_lg[i]);

                        i++;
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            listenerFIOCh.Text = arr[1].ToString();
            listenerPhonesCh.Text = arr[2].ToString();
            listenerCommCh.Text = arr[4].ToString();
            HideAll();
            ListenerChangeGrid.Visibility = Visibility.Visible;
        }
        //сохранение изменения в слушателях +
        private void listenerChangeBut_Click(object sender, RoutedEventArgs e)
        {
            if (listenerFIOCh.Text == "" || listenerPhonesCh.Text == "") { MessageBox.Show("Поля не заполнены или заполнены не правильно."); return; }
            bool b = false;
            string grLgMas = "'{";
            ArrayList ls = new ArrayList();
            string sql = "select grid from groups where ";
            for (int i = 0; i < chbxMas_gr_lg.Length; i++)
            {
                if (chbxMas_gr_lg[i].IsChecked == true)
                {
                    b = true;
                    sql += "nazvanie='" + chbxMas_gr_lg[i].Content.ToString().Substring(0, chbxMas_gr_lg[i].Content.ToString().Length - 9) + "' or ";
                    if (tbxMas_gr_lg[i].Text != "" && Convert.ToDouble(tbxMas_gr_lg[i].Text) > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }
                    if (tbxMas_gr_lg[i].Text != "") grLgMas += Convert.ToDouble(tbxMas_gr_lg[i].Text).ToString().Replace(',', '.') + ",";
                    else
                        grLgMas += "0,";
                }

            }
            grLgMas = grLgMas.Substring(0, grLgMas.Length - 1) + "}'";
            sql = sql.Substring(0, sql.Length - 3) + " order by grid";
            if (b == false) { System.Windows.Forms.MessageBox.Show("Группа не выбрана"); return; }
            string grMasId = "'{";
            ArrayList GroupsOfListNew = new ArrayList();
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        grMasId += reader.GetInt32(0).ToString() + ",";
                        GroupsOfListNew.Add(reader.GetInt32(0));
                    }

                }
                con.Close();
                grMasId = grMasId.Substring(0, grMasId.Length - 1) + "}'";
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение групп слуштеля из таблицы оплат

            ArrayList GroupsOfListFromTable = new ArrayList();
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql2 = "select grid from listnuch where listenerid = " + listenerID + " and isclose=0";
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        GroupsOfListFromTable.Add(reader.GetString(0));
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //проверка не убраны ли не закрытые групп
            for (int i = 0; i < GroupsOfListFromTable.Count; i++)
            {
                bool b1 = false;
                for (int j = 0; j < GroupsOfListNew.Count; j++)
                {
                    int a = Convert.ToInt32(GroupsOfListNew[j]);
                    int b2 = Convert.ToInt32(GroupsOfListFromTable[i]);
                    if (a==b2) { b1 = true;break; }
                }
                if (b1==false) { MessageBox.Show("Вы не можете убрать у слушателя группу, за которую у него не закрыта оплата"); return; }
            }

            //обновление записи
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql2 = "UPDATE listeners SET  fio='" + listenerFIOCh.Text + "', phones='" + listenerPhonesCh.Text + "', grid=" + grMasId + ", lgt=" + grLgMas + ", comment='" + listenerCommCh.Text + "' WHERE listenerid=" + listenerID;
                NpgsqlCommand com = new NpgsqlCommand(sql2, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //добавление записи в таблицу оплат

            for (int i = 0; i < GroupsOfListNew.Count; i++)
            {
                try {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql2 = "select isclose from listnuch where listenerid="+ listenerID + " and grid="+ GroupsOfListNew[i]+ "";
                    NpgsqlCommand com = new NpgsqlCommand(sql2,con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows == false)
                    {

                        double[] payMonth = new double[12];
                        //получение массива оплаты за группу
                        try
                        {
                            NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                            con2.Open();
                            string sqll = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12] from groups where grid= " + GroupsOfListNew[i];

                            NpgsqlCommand com2 = new NpgsqlCommand(sqll, con2);
                            NpgsqlDataReader reader2 = com2.ExecuteReader();
                            if (reader2.HasRows)
                            {
                                while (reader2.Read())
                                {
                                    payMonth[0] = reader2.GetDouble(0);
                                    payMonth[1] = reader2.GetDouble(1);
                                    payMonth[2] = reader2.GetDouble(2);
                                    payMonth[3] = reader2.GetDouble(3);
                                    payMonth[4] = reader2.GetDouble(4);
                                    payMonth[5] = reader2.GetDouble(5);
                                    payMonth[6] = reader2.GetDouble(6);
                                    payMonth[7] = reader2.GetDouble(7);
                                    payMonth[8] = reader2.GetDouble(8);
                                    payMonth[9] = reader2.GetDouble(9);
                                    payMonth[10] = reader2.GetDouble(10);
                                    payMonth[11] = reader2.GetDouble(11);
                                }
                            }
                            con2.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                        //вычисление оплаты для студента
                        try
                        {
                            NpgsqlConnection con22 = new NpgsqlConnection(connectionString);
                            con22.Open();
                            string sqll = "select lgt[array_position(grid,'" + GroupsOfListNew[i] + "')] from listeners where listenerid= " + listenerID;

                            NpgsqlCommand com22 = new NpgsqlCommand(sqll, con22);
                            NpgsqlDataReader reader22 = com22.ExecuteReader();
                            if (reader22.HasRows)
                            {
                                while (reader22.Read())
                                {
                                    payMonth[0] = Math.Round(payMonth[0] - payMonth[0] * reader22.GetDouble(0) / 100, 2);
                                    payMonth[1] = Math.Round(payMonth[1] - payMonth[1] * reader22.GetDouble(0) / 100,2);
                                    payMonth[2] = Math.Round(payMonth[2] - payMonth[2] * reader22.GetDouble(0) / 100,2);
                                    payMonth[3] = Math.Round(payMonth[3] - payMonth[3] * reader22.GetDouble(0) / 100,2);
                                    payMonth[4] = Math.Round(payMonth[4] - payMonth[4] * reader22.GetDouble(0) / 100,2);
                                    payMonth[5] = Math.Round(payMonth[5] - payMonth[5] * reader22.GetDouble(0) / 100,2);
                                    payMonth[6] = Math.Round(payMonth[6] - payMonth[6] * reader22.GetDouble(0) / 100,2);
                                    payMonth[7] = Math.Round(payMonth[7] - payMonth[7] * reader22.GetDouble(0) / 100,2);
                                    payMonth[8] = Math.Round(payMonth[8] - payMonth[8] * reader22.GetDouble(0) / 100,2);
                                    payMonth[9] = Math.Round(payMonth[9] - payMonth[9] * reader22.GetDouble(0) / 100,2);
                                    payMonth[10] = Math.Round(payMonth[10] - payMonth[10] * reader22.GetDouble(0) / 100,2);
                                    payMonth[11] = Math.Round(payMonth[11] - payMonth[11] * reader22.GetDouble(0) / 100,2);
                                }
                            }
                            con22.Close();
                    }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                string masPay = "'{";
                        for (int i2 = 0; i2 < 12; i2++)
                        {
                            masPay += payMonth[i2].ToString().Replace(',','.') + ",";
                        }
                        masPay = masPay.Substring(0, masPay.Length - 1) + "}'";
                        //добавление записи в таблицу
                        try
                        {

                            NpgsqlConnection con2 = new NpgsqlConnection(connectionString);
                            con2.Open();
                            string sql22 = "INSERT INTO listnuch(listenerid, grid, payformonth, payedlist, skidkiforpay, topay, penya, date_stop, isclose) VALUES(" + listenerID + ", " + GroupsOfListNew[i] + ", " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', '{0,0,0,0,0,0,0,0,0,0,0,0}', " + masPay + ", '{0,0,0,0,0,0,0,0,0,0,0,0}', null, 0)";
                            NpgsqlCommand com2 = new NpgsqlCommand(sql22, con2);
                            com2.ExecuteNonQuery();
                            con2.Close();
                        }
                        catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                    }
                
                } catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

         
           
                HideAll();
            //слушатели
            listenerDeleteButton.IsEnabled = false;
            listenerChangeButton.IsEnabled = false;
            DataGridUpdater.updateDataGridListener(connectionString, filtr.sql, listenerDataGrid);
                ListenerGrid.Visibility = Visibility.Visible;

        }
        //изменение кнопок контроля для DataGrid+
        private void listenerDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            //слушатели
            listenerDeleteButton.IsEnabled = true;
            listenerChangeButton.IsEnabled = true;

            //курсы
            coursDeleteButton.IsEnabled = true;
            coursChangeButton.IsEnabled = true;

            //предметы
            subDeleteButton.IsEnabled = true;

            //группы
            groupDeleteButton.IsEnabled = true;
            groupChangeButton.IsEnabled = true;

            //преподаватели
            prepDeleteButton.IsEnabled = true;
            prepChangeButton.IsEnabled = true;

            //категории
            kategDeleteButton.IsEnabled = true;

            //все сотрудники
            allSotrDeleteButton.IsEnabled = true;
            allSotrToPrepBtton.IsEnabled = true;
            allSotrToShtatBtton.IsEnabled = true;

            //расписание звонков
            zvonkiDeleteButton.IsEnabled = true;

            //кабинеты
            cabDeleteButton.IsEnabled = true;

            //типы дохода 
            TypeDohDeleteButton.IsEnabled = true;

            //типы расходов
            TypeRashDeleteButton.IsEnabled = true;

            //коефициент за выслугу лет
            KoefDeleteButton.IsEnabled = true;

            //работы обслуживания
            ObslWorkDeleteButton.IsEnabled = true;

            //должности 
            StateChangeButton.IsEnabled = true;
            StateDeleteButton.IsEnabled = true;

            //штат
            shtatDeleteButton.IsEnabled = true;
            shtatChangeButton.IsEnabled = true;
        }
        //переход к гриду кабинетов+
        private void kabMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            cabGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridСab(connectionString, cabDataGrid);
        }
        //добавление и изменение в кабинетах +
        private void cabChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("cabid", System.Type.GetType("System.Int32"));
            table.Columns.Add("num", System.Type.GetType("System.String"));
            table.Columns.Add("adres", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < cabDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = cabDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан адрес кабинета"); return; }
                if (rMas[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан номер кабинета"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется название кабинета " + rMas[1]); return; }
                list.Add(rMas[3]);
                table.ImportRow(row);
            }
            string sql = "select * from cabinet";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            cabDataGrid.SelectedItem = null;

            //кабинеты
            cabDeleteButton.IsEnabled = false;

            DataGridUpdater.updateDataGridСab(connectionString, cabDataGrid);
        }
        //удаление кабинета +
        private void cabDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = cabDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            //проверка кабинета в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select idrasp from raspisanie WHERE cabid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Этот кабинет не удаётся удалить, он используется в расписании"); return;
                }
                con.Close();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //удаление кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM cabinet WHERE cabid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridСab(connectionString,cabDataGrid);
            cabDataGrid.SelectedItem = null;

            //кабинет
            cabDeleteButton.IsEnabled = false;
        }
        //вывод расписания по преподавателям+
        public void showRaspP(DateTime dm, DateTime ds)
        {
            m = 0;//число зантий в дне
            n = 0;//число преподавателей
            LabelDateRaspP.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(prepid) from prep";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (n == 0) { MessageBox.Show("Нету преподавателей"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (m == 0) { MessageBox.Show("Нету занятий"); return; }
            HideAll();
            DeleteRaspButP.IsEnabled = false;
            ChangeRaspButP.IsEnabled = false;
            AddRaspButP.IsEnabled = false;
            raspGridP.Visibility = Visibility.Visible;
            lbmas = new Label[(m * 7) + 1, n + 2];
            DataGridUpdater.updateGridRaspP(connectionString, tGp, m, n, lbmas, dm, ds);
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].MouseDown += Label_MouseDown;
                }

            }
        }
        //переход к расписанию уроков по преподавателям+
        private void yrokiPMenu_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            dateMonday = new DateTime();
            int day_razn = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { day_razn = 0; } break;
                case "Tuesday": { day_razn = -1; } break;
                case "Wednesday": { day_razn = -2; } break;
                case "Thursday": { day_razn = -3; } break;
                case "Friday": { day_razn = -4; } break;
                case "Saturday": { day_razn = -5; } break;
                case "Sunday": { day_razn = -6; } break;
            }
            dateMonday = dateNow.AddDays(day_razn);
            showRaspP(dateMonday, dateMonday.AddDays(6));
        }
        //вывод расписания по кабинетам+
        public void showRaspС(DateTime dm, DateTime ds)
        {
            m = 0;//число зантий в дне
            n = 0;//число кабинетов
            LabelDateRaspС.Content = "Расписание на " + dm.ToShortDateString() + " - " + ds.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(cabid) from cabinet";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        n = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (n == 0) { MessageBox.Show("Нет кабинетов"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(lesson_number) from lessons_time";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        m = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            if (m == 0) { MessageBox.Show("Нет занятий"); return; }
            HideAll();
            DeleteRaspButС.IsEnabled = false;
            ChangeRaspButС.IsEnabled = false;
            AddRaspButС.IsEnabled = false;
            raspGridС.Visibility = Visibility.Visible;
            lbmas = new Label[(m * 7) + 1, n + 2];
            DataGridUpdater.updateGridRaspС(connectionString, tGс, m, n, lbmas, dm, ds);
            for (int i = 0; i < (m * 7) + 1; i++)
            {
                for (int j = 1; j < n + 2; j++)
                {
                    if (i != 0 && j != 1)
                        lbmas[i, j].MouseDown += Label_MouseDown;
                }

            }
        }
        //переход к расписанию уроков по кабинетам+
        private void yrokiCMenu_Click(object sender, RoutedEventArgs e)
        {
            DateTime dateNow = DateTime.Now;
            dateMonday = new DateTime();
            int day_razn = 0;
            switch (dateNow.DayOfWeek.ToString())
            {
                case "Monday": { day_razn = 0; } break;
                case "Tuesday": { day_razn = -1; } break;
                case "Wednesday": { day_razn = -2; } break;
                case "Thursday": { day_razn = -3; } break;
                case "Friday": { day_razn = -4; } break;
                case "Saturday": { day_razn = -5; } break;
                case "Sunday": { day_razn = -6; } break;
            }
            dateMonday = dateNow.AddDays(day_razn);
            showRaspС(dateMonday, dateMonday.AddDays(6));
        }
        //переход к форме добавления в расписание занятий по кабинетам +
        private void AddRaspButС_Click(object sender, RoutedEventArgs e)
        {
            int kab = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            //добавление
            raspAddSubsK.Items.Clear();
            raspAddPrepK.Items.Clear();
            raspAddGroupK.Items.Clear();
            int grid = -1;
            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободных групп"); return; }
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        raspAddGroupK.Items.Add(reader.GetString(0));
                       if (i == 0) { grid = reader.GetInt32(1);i++; }
                        
                    }
                    raspAddGroupK.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободного преподавателя"); return; }
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        raspAddPrepK.Items.Add(reader.GetString(0));
                    }
                    raspAddPrepK.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { raspAddDayOfWeekK.Text = "Понедельник"; } break;
                case 2: { raspAddDayOfWeekK.Text = "Вторник"; } break;
                case 3: { raspAddDayOfWeekK.Text = "Среда"; } break;
                case 4: { raspAddDayOfWeekK.Text = "Четверг"; } break;
                case 5: { raspAddDayOfWeekK.Text = "Пятница"; } break;
                case 6: { raspAddDayOfWeekK.Text = "Суббота"; } break;
                case 7: { raspAddDayOfWeekK.Text = "Воскресенье"; } break;
            }

            raspAddDateK.Text = dateMonday.AddDays(day).ToShortDateString();
            raspAddLesNumK.Text = "" + lesNum;
            raspAddKabK.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            addRaspGridKab.Visibility = Visibility.Visible;

        }
        //изменение группы в расписании по кабинетам+
        private void raspAddGroupK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box.Name == "raspAddGroupK")
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + raspAddGroupK.SelectedItem + "' )  @> ARRAY[subid]";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        raspAddSubsK.Items.Clear();
                        while (reader.Read())
                        {
                            raspAddSubsK.Items.Add(reader.GetString(0));
                        }
                        raspAddSubsK.SelectedIndex = 0;
                    }

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

            if (box.Name == "raspAddGroupP")
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + raspAddGroupP.SelectedItem + "' )  @> ARRAY[subid]";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        raspAddSubsP.Items.Clear();
                        while (reader.Read())
                        {
                            raspAddSubsP.Items.Add(reader.GetString(0));
                        }
                        raspAddSubsP.SelectedIndex = 0;
                    }

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

        }
        //переход к форме изменения в расписании занятий по кабинетам+
        private void ChangeRaspButС_Click(object sender, RoutedEventArgs e)
        {
            int kab = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            raspChangeSubsK.Items.Clear();
            raspChangePrepK.Items.Clear();
            raspChangeGroupK.Items.Clear();
            int grid = -1;

            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    raspChangeGroupK.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                    raspChangeGroupK.SelectedIndex = 0;
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    bool b = false;
                    raspChangeGroupK.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                    while (reader.Read())
                    {
                        raspChangeGroupK.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]) { raspChangeGroupK.SelectedIndex = i; grid = reader.GetInt32(1); b = true; }
                    }
                    if (b == false) { raspChangeGroupK.SelectedIndex = 0; }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод преподов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select fio from sotrudniki inner join prep using(sotrid) where sotrid in (select sotrid from prep) and prepid not in(select prepid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows == false)
                {
                    raspChangePrepK.SelectedIndex = 0;
                    raspChangePrepK.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    raspChangePrepK.SelectedIndex = 0;
                    raspChangePrepK.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        raspChangePrepK.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]) { raspChangePrepK.SelectedIndex = i; }
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
           

            switch (day + 1)
            {
                case 1: { raspChangeDayOfWeekK.Text = "Понедельник"; } break;
                case 2: { raspChangeDayOfWeekK.Text = "Вторник"; } break;
                case 3: { raspChangeDayOfWeekK.Text = "Среда"; } break;
                case 4: { raspChangeDayOfWeekK.Text = "Четверг"; } break;
                case 5: { raspChangeDayOfWeekK.Text = "Пятница"; } break;
                case 6: { raspChangeDayOfWeekK.Text = "Суббота"; } break;
            }
            raspChangeDateK.Text = dateMonday.AddDays(day).ToShortDateString();
            raspChangeLesNumK.Text = "" + lesNum;
            raspChangeKabK.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            changeRaspGridKab.Visibility = Visibility.Visible;
        }
        //изменение группы в изменении в расписании по кабинетам+
        private void raspChangeGroupK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box.Name == "raspChangeGroupK")
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + raspChangeGroupK.SelectedItem + "' )  @> ARRAY[subid]";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        raspChangeSubsK.Items.Clear();
                        raspChangeSubsK.SelectedIndex = 0;
                        int i = 0;
                        while (reader.Read())
                        {
                            raspChangeSubsK.Items.Add(reader.GetString(0));
                            if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[0]) { raspChangeSubsK.SelectedIndex = i; }
                            i++;
                        }

                    }

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }

            if (box.Name == "raspChangeGroupP")
            {
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + raspChangeGroupP.SelectedItem + "' )  @> ARRAY[subid]";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        raspChangeSubsP.Items.Clear();
                        raspChangeSubsP.SelectedIndex = 0;
                        int i = 0;
                        while (reader.Read())
                        {
                            raspChangeSubsP.Items.Add(reader.GetString(0));
                            if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[0]) { raspChangeSubsP.SelectedIndex = i; }
                            i++;
                        }

                    }

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
        }
        //переход к форме добавления в роасписание занятий по преподавателю+
        private void AddRaspButP_Click(object sender, RoutedEventArgs e)
        {
            int prep = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            //добавление
            raspAddSubsP.Items.Clear();
            raspAddKabP.Items.Clear();
            raspAddGroupP.Items.Clear();
            int grid = -1;
            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select nazvanie,grid from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false) { MessageBox.Show("На этом занятии нет свободных групп"); return; }
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        raspAddGroupP.Items.Add(reader.GetString(0));
                        if (i == 0) { grid = reader.GetInt32(1); i++; }

                    }
                    raspAddGroupP.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    raspAddKabP.SelectedIndex = 0;
                    while (reader.Read())
                    {
                        raspAddKabP.Items.Add(reader.GetString(0));
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            switch (day + 1)
            {
                case 1: { raspAddDayOfWeekP.Text = "Понедельник"; } break;
                case 2: { raspAddDayOfWeekP.Text = "Вторник"; } break;
                case 3: { raspAddDayOfWeekP.Text = "Среда"; } break;
                case 4: { raspAddDayOfWeekP.Text = "Четверг"; } break;
                case 5: { raspAddDayOfWeekP.Text = "Пятница"; } break;
                case 6: { raspAddDayOfWeekP.Text = "Суббота"; } break;
                case 7: { raspAddDayOfWeekP.Text = "Воскресенье"; } break;
            }

            raspAddDateP.Text = dateMonday.AddDays(day).ToShortDateString();
            raspAddLesNumP.Text = "" + lesNum;
            raspAddPrepP.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            addRaspGridPrep.Visibility = Visibility.Visible;
        }
        //переход к форме изменения записи в расписании занятий по преподавателям+
        private void ChangeRaspButP_Click(object sender, RoutedEventArgs e)
        {
            int prep = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }
            raspChangeSubsP.Items.Clear();
            raspChangeKabP.Items.Clear();
            raspChangeGroupP.Items.Clear();

            //вывод групп
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select nazvanie from groups where grid not in (select grid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    raspChangeGroupP.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                    raspChangeGroupP.SelectedIndex = 0;
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    bool b = false;
                    raspChangeGroupP.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]);
                    while (reader.Read())
                    {
                        raspChangeGroupP.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[1]) { raspChangeGroupP.SelectedIndex = i;  b = true; }
                    }
                    if (b == false) { raspChangeGroupP.SelectedIndex = 0; }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //вывод кабинетов
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                DateTime dayRasp = dateMonday.AddDays(day);
                string sql = "select num from cabinet where cabid not in (select cabid from raspisanie where lesson_number = " + lesNum + " and day= " + (day + 1) + " and EXTRACT(day FROM date)=" + dayRasp.Day + " and EXTRACT(Month FROM date)=" + dayRasp.Month + " and EXTRACT(Year FROM date)=" + dayRasp.Year + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows == false)
                {
                    raspChangeKabP.SelectedIndex = 0;
                    raspChangeKabP.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                }
                if (reader.HasRows)
                {
                    int i = 0;
                    raspChangeKabP.SelectedIndex = 0;
                    raspChangeKabP.Items.Add(lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]);
                    
                    while (reader.Read())
                    {
                        raspChangeKabP.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == lbmas[iRaspLebale, jRaspLebale].Content.ToString().Split('\n')[2]) {raspChangeKabP.SelectedIndex = i; }
                        i++;
                    }         
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            switch (day + 1)
            {
                case 1: { raspChangeDayOfWeekP.Text = "Понедельник"; } break;
                case 2: { raspChangeDayOfWeekP.Text = "Вторник"; } break;
                case 3: { raspChangeDayOfWeekP.Text = "Среда"; } break;
                case 4: { raspChangeDayOfWeekP.Text = "Четверг"; } break;
                case 5: { raspChangeDayOfWeekP.Text = "Пятница"; } break;
                case 6: { raspChangeDayOfWeekP.Text = "Суббота"; } break;
            }
            raspChangeDateP.Text = dateMonday.AddDays(day).ToShortDateString();
            raspChangeLesNumP.Text = "" + lesNum;
            raspChangePrepP.Text = lbmas[0, jRaspLebale].Content.ToString();
            HideAll();
            changeRaspGridPrep.Visibility = Visibility.Visible;
        }
        //удаление записи из расписания занятий по преподавателям +
        private void DeleteRaspButP_Click(object sender, RoutedEventArgs e)
        {
            int prep = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from raspisanie where prepid =" + prep + " and lesson_number = " + lesNum + " and day=" + (day + 1) + " and date='" + dateMonday.AddDays(day).ToShortDateString().Replace('.', '-') + "'";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspP(dateMonday, dateMonday.AddDays(6));
        }
        //удаление записи из расписания занятий по кабинетам +
        private void DeleteRaspButС_Click(object sender, RoutedEventArgs e)
        {
            int cab = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(lbmas[iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * m < iRaspLebale) { day++; } else break;
            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from raspisanie where cabid =" + cab + " and lesson_number = " + lesNum + " and day=" + (day + 1) + " and date='" + dateMonday.AddDays(day).ToShortDateString().Replace('.', '-') + "'";
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspС(dateMonday, dateMonday.AddDays(6));
        }
        //добавление записи в таблицу расписание по преподавателям +
        private void raspAddButToTableP_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int grid = -1;
            int cabid = -1;
            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + raspAddKabP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cabid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspAddSubsP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid from groups where nazvanie = '" + raspAddGroupP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int prep = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspAddDayOfWeekP.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //вставка записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO raspisanie(grid, lesson_number, subid, prepid, date, day,cabid) VALUES(" + grid + ", " + raspAddLesNumP.Text + ", " + subid + ", " + prep + ", '" + raspAddDateP.Text.Replace('.', '-') + "', " + day + "," + cabid + "); ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspP(dateMonday, dateMonday.AddDays(6));
        }
        //добавление записи в таблицу расписание по кабинетам +
        private void raspAddButToTableK_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int grid = -1;
            int prep = -1;
            //получение id препода
            try
            {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + raspAddPrepK.SelectedItem + "'";
                    NpgsqlCommand command = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            prep = reader.GetInt32(0);
                        }
                    }
                    con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspAddSubsK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение id группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid from groups where nazvanie = '" + raspAddGroupK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int cabid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspAddDayOfWeekK.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //вставка записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO raspisanie(grid, lesson_number, subid, prepid, date, day,cabid) VALUES(" + grid + ", " + raspAddLesNumK.Text + ", " + subid + ", " + prep + ", '" + raspAddDateK.Text.Replace('.', '-') + "', " + day + "," + cabid + "); ";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspС(dateMonday, dateMonday.AddDays(6));
        }
        //имзенение записи в таблице расписание по преподавателям +
        private void raspChangeButtonP_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int grid = -1;
            int cabid = -1;

            //получение id кабинета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select cabid from cabinet  where num = '" + raspChangeKabP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cabid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspChangeSubsP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid from groups where nazvanie = '" + raspChangeGroupP.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            int prepid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspChangeDayOfWeekP.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //обновление записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE raspisanie SET subid=" + subid + ", grid=" + grid + ",cabid = " + cabid + " WHERE prepid=" + prepid + " and  lesson_number=" + raspChangeLesNumP.Text + " and date='" + raspChangeDateP.Text.Replace('.', '-') + "' and day=" + day;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspP(dateMonday, dateMonday.AddDays(6));
        }
        //имзенение записи в таблице расписание по кабинетам +
        private void raspChangeButtonK_Click(object sender, RoutedEventArgs e)
        {
            int subid = -1;
            int grid = -1;
            int prepid = -1;

            //получение id предмета
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select subid from subject  where title = '" + raspChangeSubsK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id группы
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select grid from groups where nazvanie = '" + raspChangeGroupK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        grid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение id препода
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select prepid from prep inner join sotrudniki using(sotrid) where sotrudniki.fio = '" + raspChangePrepK.SelectedItem + "'";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        prepid = reader.GetInt32(0);
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            int cabid = Convert.ToInt32(lbmas[0, jRaspLebale].Name.Split('_')[1]);
            int day = 0;

            switch (raspChangeDayOfWeekK.Text)
            {
                case "Понедельник": { day = 1; } break;
                case "Вторник": { day = 2; } break;
                case "Среда": { day = 3; } break;
                case "Четверг": { day = 4; } break;
                case "Пятница": { day = 5; } break;
                case "Суббота": { day = 6; } break;
                case "Воскресенье": { day = 7; } break;

            }
            //обновление записи 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE raspisanie SET subid=" + subid + ", grid=" + grid + ",prepid = " + prepid + " WHERE cabid=" + cabid + " and  lesson_number=" + raspChangeLesNumK.Text + " and date='" + raspChangeDateK.Text.Replace('.', '-') + "' and day=" + day;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            showRaspС(dateMonday, dateMonday.AddDays(6));
        }
        //подсчёт общей оплаты за год для группы +
        private void grPayment1_TextChanged(object sender, TextChangedEventArgs e)
        {
            double res = 0;
            if (grPayment1.Text != "") res += Convert.ToDouble(grPayment1.Text);
            if (grPayment2.Text != "") res += Convert.ToDouble(grPayment2.Text);
            if (grPayment3.Text != "") res += Convert.ToDouble(grPayment3.Text);
            if (grPayment4.Text != "") res += Convert.ToDouble(grPayment4.Text);
            if (grPayment5.Text != "") res += Convert.ToDouble(grPayment5.Text);
            if (grPayment6.Text != "") res += Convert.ToDouble(grPayment6.Text);
            if (grPayment7.Text != "") res += Convert.ToDouble(grPayment7.Text);
            if (grPayment8.Text != "") res += Convert.ToDouble(grPayment8.Text);
            if (grPayment9.Text != "") res += Convert.ToDouble(grPayment9.Text);
            if (grPayment10.Text != "") res += Convert.ToDouble(grPayment10.Text);
            if (grPayment11.Text != "") res += Convert.ToDouble(grPayment11.Text);
            if (grPayment12.Text != "") res += Convert.ToDouble(grPayment12.Text);
            payToYear.Content = Math.Round(res,2);

            double res2 = 0;
            if (grPayment1Ch.Text != "") res2 += Convert.ToDouble(grPayment1Ch.Text);
            if (grPayment2Ch.Text != "") res2 += Convert.ToDouble(grPayment2Ch.Text);
            if (grPayment3Ch.Text != "") res2 += Convert.ToDouble(grPayment3Ch.Text);
            if (grPayment4Ch.Text != "") res2 += Convert.ToDouble(grPayment4Ch.Text);
            if (grPayment5Ch.Text != "") res2 += Convert.ToDouble(grPayment5Ch.Text);
            if (grPayment6Ch.Text != "") res2 += Convert.ToDouble(grPayment6Ch.Text);
            if (grPayment7Ch.Text != "") res2 += Convert.ToDouble(grPayment7Ch.Text);
            if (grPayment8Ch.Text != "") res2 += Convert.ToDouble(grPayment8Ch.Text);
            if (grPayment9Ch.Text != "") res2 += Convert.ToDouble(grPayment9Ch.Text);
            if (grPayment10Ch.Text != "") res2 += Convert.ToDouble(grPayment10Ch.Text);
            if (grPayment11Ch.Text != "") res2 += Convert.ToDouble(grPayment11Ch.Text);
            if (grPayment12Ch.Text != "") res2 += Convert.ToDouble(grPayment12Ch.Text);
            payToYearCh.Content = Math.Round(res2, 2);
        }
        //чекбокс (чек) для выбора групп +
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            tbxMas_gr_lg[indexText].IsEnabled = true;

        }
        //чекбокс (анчек) для выбора групп +
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            tbxMas_gr_lg[indexText].IsEnabled = false;
            tbxMas_gr_lg[indexText].Text = "";

        }
        //переход к скидкам +
        private void skidki_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            skidkiGrid.Visibility = Visibility.Visible;
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = null;
            skidki.BorderBrush = Brushes.DarkRed; ;
            obuchMenu.BorderBrush = null;
            MenuOtchety.BorderBrush = null;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select skidka from skidki order by kol_month";
                NpgsqlCommand com = new NpgsqlCommand(sql,con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        switch (i)
                        {
                            case 0: { sk1.Text = reader.GetDouble(0).ToString(); break; }
                            case 1: { sk2.Text = reader.GetDouble(0).ToString(); break; }
                            case 2: { sk3.Text = reader.GetDouble(0).ToString(); break; }
                            case 3: { sk4.Text = reader.GetDouble(0).ToString(); break; }
                            case 4: { sk5.Text = reader.GetDouble(0).ToString(); break; }
                            case 5: { sk6.Text = reader.GetDouble(0).ToString(); break; }
                            case 6: { sk7.Text = reader.GetDouble(0).ToString(); break; }
                            case 7: { sk8.Text = reader.GetDouble(0).ToString(); break; }
                            case 8: { sk9.Text = reader.GetDouble(0).ToString(); break; }
                            case 9: { sk10.Text = reader.GetDouble(0).ToString(); break; }
                            case 10: { sk11.Text = reader.GetDouble(0).ToString(); break; }
                            case 11: { sk12.Text = reader.GetDouble(0).ToString(); break; }
                              
                        }
                        i++;
                    }
                
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //сохранение скидок +
        private void skidkiSave_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToDouble(sk1.Text) > 100 || Convert.ToDouble(sk2.Text) > 100 || Convert.ToDouble(sk3.Text) > 100 || Convert.ToDouble(sk4.Text) > 100 || Convert.ToDouble(sk5.Text) > 100 || Convert.ToDouble(sk6.Text) > 100 || Convert.ToDouble(sk7.Text) > 100 || Convert.ToDouble(sk8.Text) > 100 || Convert.ToDouble(sk9.Text) > 100 || Convert.ToDouble(sk10.Text) > 100 || Convert.ToDouble(sk11.Text) > 100 || Convert.ToDouble(sk12.Text) > 100) { System.Windows.Forms.MessageBox.Show("Процент не может быть больше 100"); return; }
            NpgsqlConnection con = new NpgsqlConnection(connectionString);
            NpgsqlCommand com;
            string sql = "";
            try
            {
                con.Open();
                sql = "update skidki set skidka=" + sk1.Text + " where kol_month=1 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk2.Text + " where kol_month=2 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk3.Text + " where kol_month=3 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk4.Text + " where kol_month=4 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk5.Text + " where kol_month=5 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk6.Text + " where kol_month=6 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk7.Text + " where kol_month=7 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk8.Text + " where kol_month=8 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk9.Text + " where kol_month=9";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk10.Text + " where kol_month=10 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk11.Text + " where kol_month=11 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + sk12.Text + " where kol_month=12 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();



            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных");  return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select skidka from skidki order by kol_month";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    int i = 0;
                    while (reader1.Read())
                    {
                        switch (i)
                        {
                            case 0: { sk1.Text = reader1.GetDouble(0).ToString(); break; }
                            case 1: { sk2.Text = reader1.GetDouble(0).ToString(); break; }
                            case 2: { sk3.Text = reader1.GetDouble(0).ToString(); break; }
                            case 3: { sk4.Text = reader1.GetDouble(0).ToString(); break; }
                            case 4: { sk5.Text = reader1.GetDouble(0).ToString(); break; }
                            case 5: { sk6.Text = reader1.GetDouble(0).ToString(); break; }
                            case 6: { sk7.Text = reader1.GetDouble(0).ToString(); break; }
                            case 7: { sk8.Text = reader1.GetDouble(0).ToString(); break; }
                            case 8: { sk9.Text = reader1.GetDouble(0).ToString(); break; }
                            case 9: { sk10.Text = reader1.GetDouble(0).ToString(); break; }
                            case 10: { sk11.Text = reader1.GetDouble(0).ToString(); break; }
                            case 11: { sk12.Text = reader1.GetDouble(0).ToString(); break; }

                        }
                        i++;
                    }

                }
                con1.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //переход к таблице расходов +
        private void rash_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            TypeRashGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGriTypeRash(connectionString, TypeRashDataGrid);
        }
        //переход к таблице доходов +
        private void doh_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            TypeDohGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGriTypeDoh(connectionString,TypeDohDataGrid);
        }
        //обновление/сохранение типов дохода +
        private void TypeDohChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("idtype", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("descriprion", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < TypeDohDataGrid.Items.Count - 1; i++)
            {
                DataRowView DRV = TypeDohDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[0].GetType() == typeof(int))
                    if (Convert.ToInt32(rMas[0]) ==1  ) continue;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название дохода"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется название дохода " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from typedohod";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateDataGriTypeDoh(connectionString, TypeDohDataGrid);
            TypeDohDataGrid.SelectedItem = null;
            TypeDohDeleteButton.IsEnabled = false;
        }
        //удаление типов доходов +
        private void TypeDohDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = TypeDohDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (Convert.ToInt32(arr[0]) == 1) { System.Windows.Forms.MessageBox.Show("Невозможно удалить этот тип дохода"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT dohid FROM dodhody where idtype = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
              NpgsqlDataReader reader =  command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Этот тип используется в таблице доходов"); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM typedohod WHERE idtype = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGriTypeDoh(connectionString, TypeDohDataGrid);
            TypeDohDataGrid.SelectedItem = null;
            TypeDohDeleteButton.IsEnabled = false;
        }
        //обновление/сохранение типов расходов +
        private void TypeRashChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("typeid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("description", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < TypeRashDataGrid.Items.Count - 1; i++)
            {
                
                DataRowView DRV = TypeRashDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if(rMas[0].GetType() == typeof(int))
                if (Convert.ToInt32(rMas[0]) >= 1 && Convert.ToInt32(rMas[0]) <= 4) continue;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название расхода"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Повторяется название расхода " + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from typerash";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateDataGriTypeRash(connectionString, TypeRashDataGrid);
            TypeRashDataGrid.SelectedItem = null;
            TypeRashDeleteButton.IsEnabled = false;
        }
        //удаление типов расходов +
        private void TypeRashDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = TypeRashDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            if (Convert.ToInt32(arr[0]) >= 1 && Convert.ToInt32(arr[0]) <=4) { System.Windows.Forms.MessageBox.Show("Невозможно удалить этот тип расхода"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT rashid   FROM rashody where typeid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    MessageBox.Show("Этот тип используется в таблице расходов"); return;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM typerash WHERE typeid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGriTypeRash(connectionString, TypeRashDataGrid);
            TypeRashDataGrid.SelectedItem = null;
            TypeRashDeleteButton.IsEnabled = false;
        }
        //пеня +
        private void penya_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select penyaproc from last_pereraschet";
                NpgsqlCommand com = new NpgsqlCommand(sql,con);
                DateIn wind = new DateIn();
                wind.gridProcentPeni.Visibility = Visibility.Visible;
                wind.constr = connectionString;
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        wind.PenyaProc.Text = reader.GetDouble(0).ToString();
                    }
                }
                con.Close();
                wind.Show();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //переход к работе с коефициентом выслуги лет+
        private void koefVisl_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            KoefGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridKoef(connectionString, KoefDataGrid);
        }
        //обновление/сохранение коефициента за выслуги лет +
        private void KoefChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("koefid", System.Type.GetType("System.Int32"));
            table.Columns.Add("kol_year", System.Type.GetType("System.Int32"));
            table.Columns.Add("koef", System.Type.GetType("System.Double"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < KoefDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = KoefDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано количество лет"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указан коефициент"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Количество лет повторяется в строке" + rMas[1]); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from koef_vislugi";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateDataGridKoef(connectionString, KoefDataGrid);
            KoefDataGrid.SelectedItem = null;
            KoefDeleteButton.IsEnabled = false;
        }
        //удаление коефициента за выслуги лет +
        private void KoefDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = KoefDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
 
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM koef_vislugi WHERE koefid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridKoef(connectionString, KoefDataGrid);
            KoefDataGrid.SelectedItem = null;
            KoefDeleteButton.IsEnabled = false;
        }
        //переход к работам обслуживания+
        private void sdelniy_rab_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ObslWorkGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridRaboty(connectionString, ObslWorkDataGrid);
        }
        //обновление/сохранение работ обслуживания +
        private void ObslWorkChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = new DataTable();
            table.Columns.Add("rabotyid", System.Type.GetType("System.Int32"));
            table.Columns.Add("title", System.Type.GetType("System.String"));
            table.Columns.Add("pay", System.Type.GetType("System.Double"));
            table.Columns.Add("ed_izm", System.Type.GetType("System.String"));
            table.Columns.Add("comment", System.Type.GetType("System.String"));
            ArrayList list = new ArrayList();
            for (int i = 0; i < ObslWorkDataGrid.Items.Count - 1; i++)
            {

                DataRowView DRV = ObslWorkDataGrid.Items[i] as DataRowView;
                DataRow row = DRV.Row;
                object[] rMas = row.ItemArray;
                if (rMas[1].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указано название"); return; }
                if (rMas[2].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указана оплата"); return; }
                if (rMas[3].ToString() == "") { MessageBox.Show("В " + (i + 1) + " строке не указаны единицы измерения"); return; }
                if (list.IndexOf(rMas[1]) != -1) { MessageBox.Show("Название " + rMas[1]+" повторяется"); return; }
                list.Add(rMas[1]);
                table.ImportRow(row);
            }
            string sql = "select * from raboty_obsl";
            NpgsqlConnection conccetion = new NpgsqlConnection(connectionString);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conccetion);
            NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(comm);
            NpgsqlCommandBuilder comandbuilder = new NpgsqlCommandBuilder(adapter);
            adapter.Update(table);
            DataGridUpdater.updateDataGridRaboty(connectionString, ObslWorkDataGrid);
            ObslWorkDataGrid.SelectedItem = null;
            ObslWorkDeleteButton.IsEnabled = false;
        }
        //удаление работ обслуживания +
        private void ObslWorkDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = ObslWorkDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT shtatid  FROM shtat where  obslwork @> ARRAY["+ arr[0] + "]";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
               NpgsqlDataReader reader= command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Эта работа используется сотрудником"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }



            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM raboty_obsl WHERE rabotyid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridRaboty(connectionString, ObslWorkDataGrid);
            ObslWorkDataGrid.SelectedItem = null;
            ObslWorkDeleteButton.IsEnabled = false;
        }
        //переход к должностям+
        private void states_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            StateGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGridStates(connectionString,StateDataGrid);
        }
        //переход к добавлению должности+
        private void StateAddButton_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            days1.Text = "22";
            days2.Text = "22";
            days3.Text = "22";
            days4.Text = "22";
            days5.Text = "22";
            days6.Text = "22";
            days7.Text = "22";
            days8.Text = "22";
            days9.Text = "22";
            days10.Text = "22";
            days11.Text = "22";
            days12.Text = "22";
            StateAddTitle.Text = "";
            StateAddPay.Text = "";
            StateAddCom.Text = "";
            StateAddGrid.Visibility = Visibility.Visible;
        }
        //цифры без запятой +
        private void days1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        //добавление должности в базу +
        private void StateAddBut_Click(object sender, RoutedEventArgs e)
        {
            if (StateAddPay.Text == "" || StateAddTitle.Text == "" || days1.Text == "" || days2.Text == "" || days3.Text == "" || days4.Text == "" || days5.Text == "" || days6.Text == "" || days7.Text == "" || days8.Text == "" || days9.Text == "" || days10.Text == "" || days11.Text == "" || days12.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполненны"); return; }
            if(Convert.ToInt32(days1.Text)>31|| Convert.ToInt32(days2.Text) > 29 || Convert.ToInt32(days3.Text) > 31 || Convert.ToInt32(days4.Text) > 30 || Convert.ToInt32(days5.Text) > 31 || Convert.ToInt32(days6.Text) > 30 || Convert.ToInt32(days7.Text) > 31 || Convert.ToInt32(days8.Text) > 31 || Convert.ToInt32(days9.Text) > 30 || Convert.ToInt32(days10.Text) > 31 || Convert.ToInt32(days11.Text) > 30 || Convert.ToInt32(days12.Text) > 31) { System.Windows.Forms.MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO states(title, kol_work_day, zp, comment) VALUES ('"+ StateAddTitle.Text+ "', '{"+ days1 .Text+ "," + days2.Text + "," + days3.Text + "," + days4.Text + "," + days5.Text + "," + days6.Text + "," + days7.Text + "," + days8.Text + "," + days9.Text + "," + days10.Text + "," + days11.Text + "," + days12.Text + "}', "+StateAddPay.Text.Replace(',','.')+", '"+StateAddCom.Text+"')";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            MessageBoxResult res = MessageBox.Show("Должность добавлена.\nПродолжить добавление?", "Продолжение",MessageBoxButton.YesNo);
            if (res == MessageBoxResult.Yes)
            {
                days1.Text = "22";
                days2.Text = "22";
                days3.Text = "22";
                days4.Text = "22";
                days5.Text = "22";
                days6.Text = "22";
                days7.Text = "22";
                days8.Text = "22";
                days9.Text = "22";
                days10.Text = "22";
                days11.Text = "22";
                days12.Text = "22";
                StateAddTitle.Text = "";
                StateAddPay.Text = "";
                StateAddCom.Text = "";
            }
            if (res == MessageBoxResult.No)
            {
                HideAll();
                StateGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridStates(connectionString, StateDataGrid);
            }
        }
        //удаление должности +
        private void StateDeleteButton_Click(object sender, RoutedEventArgs e)
        {

            DataRowView DRV = StateDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "SELECT shtatid FROM shtat where states @> ARRAY[" + arr[0] + "] " ;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
               NpgsqlDataReader reader=  command.ExecuteReader();
                if (reader.HasRows) { MessageBox.Show("Должность используется сотрудником"); return; }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "DELETE FROM states WHERE statesid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridStates(connectionString, StateDataGrid);
            StateDataGrid.SelectedItem = null;
            StateChangeButton.IsEnabled = false;
            StateDeleteButton.IsEnabled = false;

        }
        //переход к изменению должности+
        private void StateChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = StateDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Изменение не возможно, Вы не выбрали запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            stateID = (int)arr[0];
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select array_to_string(kol_work_day,'_') from states where statesid = " + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
               NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        string[] ss = reader.GetString(0).Split('_');
                        Chanedays1.Text = ss[0]; Chanedays2.Text = ss[1]; Chanedays3.Text = ss[2]; Chanedays4.Text = ss[3]; Chanedays5.Text = ss[4]; Chanedays6.Text = ss[5]; Chanedays7.Text = ss[6]; Chanedays8.Text = ss[7]; Chanedays9.Text = ss[8]; Chanedays10.Text = ss[9]; Chanedays11.Text = ss[10]; Chanedays12.Text = ss[11];
                    }
                
                }
                con.Close();
                StateChaneTitle.Text = arr[1].ToString();
                StateChanePay.Text = arr[3].ToString();
                StateChaneCom.Text = arr[4].ToString();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            HideAll();
            StateChaneGrid.Visibility = Visibility.Visible;
        }
        //сохранение изменений в должности +
        private void StateChaneBut_Click(object sender, RoutedEventArgs e)
        {
            if (StateChanePay.Text == "" || StateChaneTitle.Text == "" || Chanedays1.Text == "" || Chanedays2.Text == "" || Chanedays3.Text == "" || Chanedays4.Text == "" || Chanedays5.Text == "" || Chanedays6.Text == "" || Chanedays7.Text == "" || Chanedays8.Text == "" || Chanedays9.Text == "" || Chanedays10.Text == "" || Chanedays11.Text == "" || Chanedays12.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            if (Convert.ToInt32(Chanedays1.Text) > 31 || Convert.ToInt32(Chanedays2.Text) > 29 || Convert.ToInt32(Chanedays3.Text) > 31 || Convert.ToInt32(Chanedays4.Text) > 30 || Convert.ToInt32(Chanedays5.Text) > 31 || Convert.ToInt32(Chanedays6.Text) > 30 || Convert.ToInt32(Chanedays7.Text) > 31 || Convert.ToInt32(Chanedays8.Text) > 31 || Convert.ToInt32(Chanedays9.Text) > 30 || Convert.ToInt32(Chanedays10.Text) > 31 || Convert.ToInt32(Chanedays11.Text) > 30 || Convert.ToInt32(Chanedays12.Text) > 31) { System.Windows.Forms.MessageBox.Show("Дни в месяцах указаны не верно"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "update states set title='" + StateChaneTitle.Text + "', kol_work_day='{" + Chanedays1.Text + "," + Chanedays2.Text + "," + Chanedays3.Text + "," + Chanedays4.Text + "," + Chanedays5.Text + "," + Chanedays6.Text + "," + Chanedays7.Text + "," + Chanedays8.Text + "," + Chanedays9.Text + "," + Chanedays10.Text + "," + Chanedays11.Text + "," + Chanedays12.Text + "}', zp=" + StateChanePay.Text.Replace(',', '.') + ",comment='" + StateChaneCom.Text + "' where statesid="+stateID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
                HideAll();
                StateGrid.Visibility = Visibility.Visible;
                DataGridUpdater.updateDataGridStates(connectionString, StateDataGrid);
         
        }
        //переход к штату +
        private void shtat_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ShtatGrid.Visibility = Visibility.Visible;

            ShtatFiltrCmbx.SelectedIndex = 0;


            filtr.sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateDataGridShtat(connectionString, filtr.sql, ShtatDataGrid);
        }
        //добавление сотрдуника в таблицу штата  +
        private void addToShtat_Click(object sender, RoutedEventArgs e)
        {
            string oblswork = "'{ ";
            string obem = "'{ ";
            string states = "'{ ";
            string stavki = "'{ ";
            bool b= false;
            for (int i = 0; i < chbxMas_obslwork.Length; i++)
            {
                if (chbxMas_obslwork[i].IsChecked == true && tbxMas_obem[i].Text == "") { System.Windows.Forms.MessageBox.Show("Обьём работ не заполнен"); return; }
                else { if (chbxMas_obslwork[i].IsChecked == true) { b = true; oblswork += chbxMas_obslwork[i].Name.Split('_')[2] + ","; obem += tbxMas_obem[i].Text.Replace(',', '.') + ","; } }
            }
            for (int i = 0; i < chbxMas_state.Length; i++)
            {
                if (chbxMas_state[i].IsChecked == true && tbxMas_stavki[i].Text == "") { System.Windows.Forms.MessageBox.Show("Ставки не указаны"); return; }
                else
                { if (chbxMas_state[i].IsChecked == true) { b = true; states += chbxMas_state[i].Name.Split('_')[2] + ","; stavki += tbxMas_stavki[i].Text.Replace(',', '.') + ","; } }
            }
            if (b == false) { MessageBox.Show("Выберите хотя бы одну должность или работу для сотрудника"); return; }

           
                states = states.Substring(0, states.Length - 1) + "}'";
                stavki = stavki.Substring(0, stavki.Length - 1) + "}'";
                oblswork = oblswork.Substring(0, oblswork.Length - 1) + "}'";
            obem = obem.Substring(0, obem.Length - 1) + "}'";

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "INSERT INTO shtat( sotrid, states, stavky, obslwork, obem) VALUES ( "+ sotrID + ", "+states+", "+stavki+", "+oblswork+", "+obem+")";
            NpgsqlCommand com = new NpgsqlCommand(sql,con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE shtatrasp SET  shtatid=array_append(shtatid,"+ sotrID + ") WHERE extract(Month from date)="+DateTime.Now.Month;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            MessageBox.Show("Сотрудник определён как штатный работник");
            DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, ShtatDataGrid);
            HideAll();
            allSotrGrid.Visibility = Visibility.Visible;

        }
        //удаление из штата +
        private void shtatDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = ShtatDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Удаление прервано, Вы не выбрали запись для удаления."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;



            //проверка в расписании
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE shtatrasp SET shtatid = array_remove(shtatid, " + arr[0] + ")";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                 command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "delete from shtat where shtatid =" + arr[0];
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                command.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); }
            DataGridUpdater.updateDataGridShtat(connectionString, filtr.sql, ShtatDataGrid);

            ShtatDataGrid.SelectedItem = null;

            //штат
            shtatDeleteButton.IsEnabled = false;
            shtatChangeButton.IsEnabled = false;
        }
        //переход к изменению штата+
        private void shtatChangeButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView DRV = ShtatDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Добавление прервано, Вы не выбрали сотрудника."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            ShtatID = Convert.ToInt32(arr[0]);
            HideAll();
            ChangeShtatGrid.Visibility = Visibility.Visible;

            fioChangeShtat.Text = arr[1].ToString();

            ChangeStates.Children.Clear();
            ChangeStates.RowDefinitions.Clear();

            ChangeObslWorks.Children.Clear();
            ChangeObslWorks.RowDefinitions.Clear();

            int kol_states = -1, kol_obsWork = -1;
            //получени е кол-ва должностей
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(distinct title) from states";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_states = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение кол-ва облс. работ
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(distinct title) from raboty_obsl";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        kol_obsWork = reader.GetInt32(0);
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            tbxMas_stavki = new TextBox[kol_states];
            chbxMas_state = new CheckBox[kol_states];
            tbxMas_obem = new TextBox[kol_obsWork];
            chbxMas_obslwork = new CheckBox[kol_obsWork];

           

            //получение должностей 
            ArrayList StatesLs = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();

                string sql1 = "select title from states where ARRAY[statesid] <@ (select states from shtat where shtatid=" + ShtatID + " ) order by statesid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        StatesLs.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            //получение работ 
            ArrayList WorkLs = new ArrayList();
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();

                string sql1 = "select title from raboty_obsl where ARRAY[rabotyid] <@ (select obslwork from shtat where shtatid=" + ShtatID + " ) order by rabotyid";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        WorkLs.Add(reader1.GetString(0));
                    }
                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            //получение обёма работ и ставок
            string stavki="";
            string obem="";
            try
            {
                NpgsqlConnection con12 = new NpgsqlConnection(connectionString);
                con12.Open();

                string sql12 = "select array_to_string(stavky,'_'),array_to_string(obem,'_') from shtat where shtatid=" + ShtatID;
                NpgsqlCommand com12 = new NpgsqlCommand(sql12, con12);
                NpgsqlDataReader reader12 = com12.ExecuteReader();
                if (reader12.HasRows)
                {
                    while (reader12.Read())
                    {
                        stavki=reader12.GetString(0);
                        obem= reader12.GetString(1);
                    }
                }
                con12.Close();
        }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

    string [] stavkiMas = stavki.Split('_');
            string [] obemMas = obem.Split('_'); ;

            //заполнение грида должностей 
            Label l1 = new Label();
            l1.Content = "Должность";
            Label l2 = new Label();
            l2.Content = "Ставка";
          

            RowDefinition rwd1 = new RowDefinition();
            rwd1.Height = new GridLength(40);

            ChangeStates.RowDefinitions.Add(rwd1);

            Grid.SetRow(l1, 0);
            Grid.SetRow(l2, 0);
     

            Grid.SetColumn(l2, 1);
            Grid.SetColumn(l1, 0);
        

            ChangeStates.Children.Add(l1);
            ChangeStates.Children.Add(l2);
          
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select statesid,title from states order by statesid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    int j = 0;
                    while (reader.Read())
                    {
                        tbxMas_stavki[i] = new TextBox();
                        chbxMas_state[i] = new CheckBox();
                       

                     
                        chbxMas_state[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_state";
                        chbxMas_state[i].Content = reader.GetString(1);
                        chbxMas_state[i].Checked += Shtat_Checked;
                        chbxMas_state[i].Unchecked += Shtat_UnChecked;

                        tbxMas_stavki[i].IsEnabled = false;
                        tbxMas_stavki[i].PreviewTextInput += grPayment_PreviewTextInput;

                        if (StatesLs.IndexOf(reader.GetString(1)) != -1) { chbxMas_state[i].IsChecked = true; tbxMas_stavki[i].Text = stavkiMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        ChangeStates.RowDefinitions.Add(rwd);

                        Grid.SetRow(tbxMas_stavki[i], (i + 1));
                        Grid.SetRow(chbxMas_state[i], (i + 1));
       

                        Grid.SetColumn(tbxMas_stavki[i], 1);
                        Grid.SetColumn(chbxMas_state[i], 0);
                     

                        ChangeStates.Children.Add(tbxMas_stavki[i]);
                        ChangeStates.Children.Add(chbxMas_state[i]);
                   
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            
            //заполнение грида работ 

            Label l11 = new Label();
            l11.Content = "Работа";
            Label l22 = new Label();
            l22.Content = "Объём";
            Label l33 = new Label();
            l33.Content = "Еденицы измерения";

            RowDefinition rwd11 = new RowDefinition();
            rwd11.Height = new GridLength(40);

            ChangeObslWorks.RowDefinitions.Add(rwd11);

            Grid.SetRow(l11, 0);
            Grid.SetRow(l22, 0);
            Grid.SetRow(l33, 0);

            Grid.SetColumn(l22, 1);
            Grid.SetColumn(l11, 0);
            Grid.SetColumn(l33, 2);

            ChangeObslWorks.Children.Add(l11);
            ChangeObslWorks.Children.Add(l22);
            ChangeObslWorks.Children.Add(l33);

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select rabotyid,title,ed_izm from raboty_obsl order by rabotyid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    int j = 0;
                    while (reader.Read())
                    {
                        tbxMas_obem[i] = new TextBox();
                        chbxMas_obslwork[i] = new CheckBox();
                        Label lb = new Label();

                        lb.Content = reader.GetString(2);
                        chbxMas_obslwork[i].Name = "Name_" + i + "_" + reader.GetInt32(0) + "_obsl";
                        chbxMas_obslwork[i].Content = reader.GetString(1);
                        chbxMas_obslwork[i].Checked += Shtat_Checked;
                        chbxMas_obslwork[i].Unchecked += Shtat_UnChecked;

                        tbxMas_obem[i].IsEnabled = false;
                        tbxMas_obem[i].PreviewTextInput += grPayment_PreviewTextInput;

                        if (WorkLs.IndexOf(reader.GetString(1)) != -1) { chbxMas_obslwork[i].IsChecked = true; tbxMas_obem[i].Text = obemMas[j]; j++; }

                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(40);

                        ChangeObslWorks.RowDefinitions.Add(rwd);

                        Grid.SetRow(tbxMas_obem[i], (i + 1));
                        Grid.SetRow(chbxMas_obslwork[i], (i + 1));
                        Grid.SetRow(lb, (i + 1));

                        Grid.SetColumn(tbxMas_obem[i], 1);
                        Grid.SetColumn(chbxMas_obslwork[i], 0);
                        Grid.SetColumn(lb, 2);

                        ChangeObslWorks.Children.Add(tbxMas_obem[i]);
                        ChangeObslWorks.Children.Add(chbxMas_obslwork[i]);
                        ChangeObslWorks.Children.Add(lb);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        //сохранение изменений в штате +
        private void ChangeShtat_Click(object sender, RoutedEventArgs e)
        {

            if (fioChangeShtat.Text == "") { System.Windows.Forms.MessageBox.Show("Поля не заполнены"); return; }
            string oblswork = "'{ ";
            string obem = "'{ ";
            string states = "'{ ";
            string stavki = "'{ ";
            bool b = false;
            for (int i = 0; i < chbxMas_obslwork.Length; i++)
            {
                if (chbxMas_obslwork[i].IsChecked == true && tbxMas_obem[i].Text == "") { System.Windows.Forms.MessageBox.Show("Объём работ не заполнен"); return; }
                else { if (chbxMas_obslwork[i].IsChecked == true) { b = true; oblswork += chbxMas_obslwork[i].Name.Split('_')[2] + ","; obem += tbxMas_obem[i].Text.Replace(',', '.') + ","; } }
            }
            for (int i = 0; i < chbxMas_state.Length; i++)
            {
                if (chbxMas_state[i].IsChecked == true && tbxMas_stavki[i].Text == "") { System.Windows.Forms.MessageBox.Show("Ставки не указаны"); return; }
                else
                { if (chbxMas_state[i].IsChecked == true) { b = true; states += chbxMas_state[i].Name.Split('_')[2] + ","; stavki += tbxMas_stavki[i].Text.Replace(',', '.') + ","; } }
            }
            if (b == false) { MessageBox.Show("Выберите хотя бы одну должность или работу для сотрудника"); return; }


            states = states.Substring(0, states.Length - 1) + "}'";
            stavki = stavki.Substring(0, stavki.Length - 1) + "}'";
            oblswork = oblswork.Substring(0, oblswork.Length - 1) + "}'";
            obem = obem.Substring(0, obem.Length - 1) + "}'";

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE shtat SET  states="+ states + ", stavky="+ stavki + ", obslwork="+ oblswork + ", obem="+ obem + " WHERE shtatid = "+ShtatID;
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "UPDATE sotrudniki SET fio='"+ fioChangeShtat.Text + "' WHERE sotrid=(select sotrid from shtat where shtatid ="+ShtatID+")";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            DataGridUpdater.updateDataGridShtat(connectionString, filtr.sql, ShtatDataGrid);
            HideAll();
            ShtatGrid.Visibility = Visibility.Visible;
        }
        //переход к штатному расписанию+
        private void shtatRasp_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ShtatRaspGrid.Visibility = Visibility.Visible;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    lbmas_shtatRasp[i, j] = new Label();
                    lbmas_shtatRasp[i, j].Content = "";
                    lbmas_shtatRasp[i, j].FontSize = 16;
                    lbmas_shtatRasp[i, j].HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center;
                    lbmas_shtatRasp[i, j].VerticalContentAlignment = System.Windows.VerticalAlignment.Center;
                    lbmas_shtatRasp[i, j].Name = "name_" +i+"_"+j;
                    lbmas_shtatRasp[i, j].BorderThickness = new Thickness(2);
                    lbmas_shtatRasp[i, j].BorderBrush = Brushes.Black;
                    lbmas_shtatRasp[i, j].MouseDown += Label_shtatRasp_MouseDown;
                }
            
            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(sotrid) from shtat";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        chbxMas_stateRasp = new CheckBox[reader.GetInt32(0)];
                    }
                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            DataGridUpdater.updateGridShtatRasp(connectionString, MonthGrid, ShtatRaspSotrpGrid, lbmas_shtatRasp, chbxMas_stateRasp, ShtatRaspMonthYearLabel, date);
        }
        //клик по лейблу штатного расписания +
        private void Label_shtatRasp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    lbmas_shtatRasp[i, j].Background = Brushes.White;
                }
            }
            
            Label lb = sender as Label;
            int index_i = Convert.ToInt32(lb.Name.Split('_')[1]);
            int index_j = Convert.ToInt32(lb.Name.Split('_')[2]);
            if (index_i != 0 && lb.Content.ToString() != "")
            {
                lbmas_shtatRasp[index_i, index_j].Background = Brushes.Aqua;
                ShtatRaspSaveBut.IsEnabled = true;
                for (int j = 0; j < chbxMas_stateRasp.Length; j++)
                {
                    chbxMas_stateRasp[j].IsChecked = false; 

                }
                try
                {
                    DateTime dateToSelect = new DateTime(date.Year, date.Month, Convert.ToInt32(lb.Content.ToString()));
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select array_to_string(shtatid,'_') from shtatrasp where date='" + dateToSelect.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] strSotrid = reader.GetString(0).Split('_');
                            for (int i = 0; i < strSotrid.Length; i++)
                            {
                                for (int j = 0; j < chbxMas_stateRasp.Length; j++)
                                {
                                    if (strSotrid[i] == chbxMas_stateRasp[j].Name.Split('_')[1]) { chbxMas_stateRasp[j].IsChecked = true; }
                                
                                }
                            }
                        }
                    
                    }
                    con.Close();
                }
                catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
        }
        //переход к предыдщуему месяцу+
        private void ShtatRaspPrevBut_Click(object sender, RoutedEventArgs e)
        {
            date=date.AddMonths(-1);
            ShtatRaspSaveBut.IsEnabled = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    lbmas_shtatRasp[i, j].Content = "";

                }
            }

            DataGridUpdater.updateGridShtatRasp(connectionString, MonthGrid, ShtatRaspSotrpGrid, lbmas_shtatRasp, chbxMas_stateRasp, ShtatRaspMonthYearLabel, date);
       
        }
        //переход к следующему месяцу+
        private void ShtatRaspNextBut_Click(object sender, RoutedEventArgs e)
        {
            date = date.AddMonths(1);
            ShtatRaspSaveBut.IsEnabled = false;
            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    lbmas_shtatRasp[i, j].Content = "";

                }
            }
            DataGridUpdater.updateGridShtatRasp(connectionString, MonthGrid, ShtatRaspSotrpGrid, lbmas_shtatRasp, chbxMas_stateRasp, ShtatRaspMonthYearLabel, date);
        }
        //сохранение расписания штатного сотрудника +
        private void ShtatRaspSaveBut_Click(object sender, RoutedEventArgs e)
        {

            Button but = sender as Button;
            if (but.Name == "ShtatRaspSaveBut")
            {
                int day = 0;
                for (int i = 0; i < 7; i++)
                {
                    for (int j = 0; j < 7; j++)
                    {
                        if (lbmas_shtatRasp[i, j].Background == Brushes.Aqua) { day = Convert.ToInt32(lbmas_shtatRasp[i, j].Content.ToString()); lbmas_shtatRasp[i, j].Background = Brushes.White; }
                    }
                }
                DateTime dateToAdd = new DateTime(date.Year, date.Month, day);

                bool b = false;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "select shraspid from shtatrasp where date= '" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows) b = true;
                    con.Close();
                }
                catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                string sotrMas = "'{-1,";
                for (int i = 0; i < chbxMas_stateRasp.Length; i++)
                {
                    if (chbxMas_stateRasp[i].IsChecked == true)
                    {
                        sotrMas += chbxMas_stateRasp[i].Name.Split('_')[1] + ",";
                    }
                }
                sotrMas = sotrMas.Substring(0, sotrMas.Length - 1) + "}'";

                if (b == false)
                {
                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(connectionString);
                        con.Open();
                        string sql = "INSERT INTO shtatrasp(shtatid, date)VALUES (" + sotrMas + ", '" + dateToAdd.ToShortDateString().Replace('.', '-') + "')";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                }
                else
                {

                    try
                    {
                        NpgsqlConnection con = new NpgsqlConnection(connectionString);
                        con.Open();
                        string sql = "update shtatrasp set shtatid=" + sotrMas + " where date ='" + dateToAdd.ToShortDateString().Replace('.', '-') + "'";
                        NpgsqlCommand com = new NpgsqlCommand(sql, con);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                    catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


                }
                DataGridUpdater.updateGridShtatRasp(connectionString, MonthGrid, ShtatRaspSotrpGrid, lbmas_shtatRasp, chbxMas_stateRasp, ShtatRaspMonthYearLabel, date);
                ShtatRaspSaveBut.IsEnabled = false;
            }

            if (but.Name == "ShtatRaspSelectAllBut")
            {
                if (selectd == false)
                {
                    for (int i = 0; i < chbxMas_stateRasp.Length; i++)
                    {
                        chbxMas_stateRasp[i].IsChecked = true;
                    }
                    selectd = true;
                    return;
                }
                else {

                    for (int i = 0; i < chbxMas_stateRasp.Length; i++)
                    {
                        chbxMas_stateRasp[i].IsChecked = false;
                    }
                    selectd = false;
                    return;
                }
            }

        }
        //применение фильтров+
        private void FiltrGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but.Name == "FiltrGroupsButton")
            {
                filtr.ApplyListFiltr();
                DataGridUpdater.updateDataGridListener(connectionString, filtr.sql, listenerDataGrid);
            }


            if (but.Name == "FiltrCourseButton")
            {
                filtr.ApplyGroupsFiltr();
                DataGridUpdater.updateDataGridGroups(connectionString, filtr.sql, groupsDataGrid);
            }

            if (but.Name == "FiltrSubsButton")
            {
                filtr.ApplyCourseFiltr();
                DataGridUpdater.updateDataGridСourses(connectionString, filtr.sql, coursDataGrid);
            }


            if (but.Name == "FiltrPrepButton")
            {
                filtr.ApplyPrepFiltr();
                DataGridUpdater.updateDataGridPrep(connectionString, filtr.sql, prepDataGrid);
            }

            if (but.Name == "FiltrAllSotrButton")
            {
                sqlAllSotr = "SELECT * FROM sotrudniki";
                if (DatePickAllSotr.Text == "") { System.Windows.Forms.MessageBox.Show("Неоюходимо выбрать месяц"); return; }

                if (FirstMethodFiltr.IsChecked == true)
                {
                    sqlAllSotr = "select * from sotrudniki inner join nachisl using(sotrid) where extract(Month from payday) = " + DatePickAllSotr.Text.Split('.')[1] + " and extract(Year from payday)=" + DatePickAllSotr.Text.Split('.')[2] + " and (prepzp+shtatzp+obslzp)-viplacheno!=0";

                }
                if (SecondMethodFiltr.IsChecked == true)
                {

                    sqlAllSotr = "select * from sotrudniki where sotrid not in (select sotrid from nachisl where extract(Month from payday) = " + DatePickAllSotr.Text.Split('.')[1] + " and extract(Year from payday)=" + DatePickAllSotr.Text.Split('.')[2] + ")";

                }

                DataGridUpdater.updateDataGridSotr(connectionString, sqlAllSotr, allSotrDataGrid);
            }

            if (but.Name == "FiltrShtatButton")
            {
                if (ShtatFiltrCmbx.SelectedIndex == 0)
                {
                    filtr.ApplyShtatFiltrFirst();
                }
                else
                {

                    filtr.ApplyShtatFiltrSecond();
                }
                DataGridUpdater.updateDataGridShtat(connectionString, filtr.sql, ShtatDataGrid);
            }
        }
        //выбор варианта фильтрации всех сотрудников+
        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            if (cbx.Name == "FirstMethodFiltr") SecondMethodFiltr.IsChecked = false;
            else FirstMethodFiltr.IsChecked = false;
        }
        //выбор варианта фильтрации штата+
        private void ShtatFiltrCmbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ShtatFiltrCmbx.SelectedIndex == 0)
            {

                FiltrShtatSotr.Children.Clear();
                FiltrShtatSotr.ColumnDefinitions.Clear();
                filtr.CreateShtatFiltrFirst(FiltrShtatSotr);
            }
            else {
                FiltrShtatSotr.Children.Clear();
                FiltrShtatSotr.ColumnDefinitions.Clear();
                filtr.CreateShtatFiltrSecond(FiltrShtatSotr);
            }
        }
        ////переход к гриду отчета кассы+
        private void kassaMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            kassaGrid.Visibility = Visibility.Visible;
            fda.CreateKassaDAFiltr(pD);
            fdb.CreateKassaDBFiltr(tD);
            fra.CreateKassaRAFiltr(pR);
            frb.CreateKassaRBFiltr(tR);
            fda.sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype)";
            fra.sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel,fda.sql,fra.sql);
        }
        //+
        private void SpiskiVyplatMenu_Click(object sender, RoutedEventArgs e)
        {
            DataGridUpdater.updateGridStatistica(connectionString, statGraf);
            HideAll();
            StatisticaGrid.Visibility = Visibility.Visible;
        }
        //+
        private void StatistikaMenu_Click(object sender, RoutedEventArgs e)
        {
            HideAll();
            ZpOthcetGrid.Visibility = Visibility.Visible;
            ZPOtchetVivodGrid.ColumnDefinitions.Clear();
            ZPOtchetVivodGrid.Children.Clear();

            ZPOtchetLabel.Content = "Отчёт 'Списки выплат' на " + DateTime.Now.ToShortDateString();

            ColumnDefinition cmd = new ColumnDefinition();
            cmd.Width = new GridLength(200);
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
                        for (int i = 0; i < reader.GetInt32(0) + 3; i++)
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
            Grid.SetRow(itogLb, gr.RowDefinitions.Count - 1);

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
                        if (dateList.IndexOf(DateTimeAxis.ToDouble(dd)) == -1) dateList.Add(DateTimeAxis.ToDouble(dd));
                    }

                }
                con1.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            for (int i = 0; i < dateList.Count; i++)
            {

                ColumnDefinition cmdd = new ColumnDefinition();
                cmdd.Width = new GridLength(600);
                ZPOtchetVivodGrid.ColumnDefinitions.Add(cmdd);
                Grid monthGrid = new Grid();
                for (int j = 0; j < 4; j++)
                {
                    ColumnDefinition cmdd2 = new ColumnDefinition();
                    monthGrid.ColumnDefinitions.Add(cmdd2);
                }
                DataGridUpdater.updateGridSpisciVyplat(connectionString, DateTimeAxis.ToDateTime(Convert.ToDouble(dateList[i])), sotrList, monthGrid);
                Grid.SetColumn(monthGrid, i + 1);
                ZPOtchetVivodGrid.Children.Add(monthGrid);
            }
        }
        //клилк по меню отчётов
        private void MenuOtchety_Click(object sender, RoutedEventArgs e)
        {
            MenuRolesD.BorderBrush = null;
            raspMenu.BorderBrush = null;
            sotrMenu.BorderBrush = null;
            obuchMenu.BorderBrush = null;
            skidki.BorderBrush = null;
            MenuOtchety.BorderBrush = Brushes.DarkRed;
        }
        //применение фильтров для кассы
        private void Button_Click_1(object sender, RoutedEventArgs e)
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
    }
}
