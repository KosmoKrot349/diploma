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
using WpfApp12.strategiesForManager.ButtonClick;
using WpfApp12.strategiesForManager.MenuClick;


namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для DirectorWindow.xaml
    /// </summary>
    public partial class DirectorWindow : Window
    {
        
public int logUser;
        public int prepID = -1;
        public int grID = -1;
        public int courseID = -1;
        public int sotrID = -1;
        public string dontChCName = "";
        public string dontChGName = "";

        //массивы дял слушателей
        public CheckBox[] chbxMas_gr_lg;
        public TextBox[] tbxMas_gr_lg;

        public CheckBox[] chbxMas;
        public string FIO = "";
        public Label[,] lbmas;
        public int m = 0;//число зантий в дне
        public int n = 0;//число групп
        public int iRaspLebale;
        public int jRaspLebale;
        public DateTime dateMonday;
        public int listenerID = -1;
        public int stateID = -1;
        public int ShtatID = -1;
        public bool selectd = false;
        //массивы для штата 
        public TextBox[] tbxMas_stavki;
        public CheckBox[] chbxMas_state;
        public TextBox[] tbxMas_obem;
        public CheckBox[] chbxMas_obslwork;

        //массивы для штатного расписания
        public CheckBox[] chbxMas_stateRasp;
        public Label[,] lbmas_shtatRasp = new Label[7, 7];
        public DateTime date = DateTime.Now;


        public filtr filtr = new filtr();

        public filtr fda = new filtr();
        public filtr fdb = new filtr();

        public filtr fra = new filtr();
        public filtr frb = new filtr();


        //фильтр для всех сотрудников
        public string sqlAllSotr = "";



        //строка подключения
        public string connectionString = "";
    

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
            IButtonClick actionReact = new GoToAdmin(this);
            actionReact.ButtonClick();
        }
        //переход из меню директора в меню бухгалтера+
        private void BuhgRoleD_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToBuhgalter(this);
            actionReact.ButtonClick();
        }
        //переход из меню директора в меню директора+
        private void DirectorRoleD_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToManager();
            actionReact.ButtonClick();
        }
        //переход к расписанию звоноков+
        private void zvonkiMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new TimeScheduleMenu(this);
            actionReact.MenuClick();
        }
        //удаление записи в расписаии звонков+
        private void ZvonkiDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteFromTimeSchedule(this);
            actionReact.ButtonClick();
        }
        //добавление/изменение записи в расписании звоноков +
        private void ZvonkiAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddChangeToTimeSchedule(this);
            actionReact.ButtonClick();
        }
        //переход к сотрудникам+
        private void allSotr_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new EmploeesMenu(this);
            actionReact.MenuClick();
        }
        //переход к преподавателям+
        private void preps_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new TeachersMenu(this);
            actionReact.MenuClick();
        }
        //переход к категориям+
        private void kategorii_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CategoryMenu(this);
            actionReact.MenuClick();
        }
        //добавление категори +
        private void kategAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddChangeCategory(this);
            actionReact.ButtonClick();
        }
        //удаление категории+
        private void kategDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteCategory(this);
            actionReact.ButtonClick();
        }
        //сохранение изменений в таблице преподавателей +
        private void save_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeTeacher(this);
            actionReact.ButtonClick();
        }
        //добавление сотрудника +
        private void allSotrAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddEmployee(this);
            actionReact.ButtonClick();

        }
        //удаление из сотрудников+
        private void allSotrDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteEmployee(this);
            actionReact.ButtonClick();
        }
        //удаление из преподавателей +
        private void prepDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteTeacher(this);
            actionReact.ButtonClick();
        }
        //переход к форме изменения преподавателей+
        private void prepChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeTeacher(this);
            actionReact.ButtonClick();
        }
        //переход к группам+
        private void groupsMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new GroopMenu(this);
            actionReact.MenuClick();
        }
        //переход к гриду добавления группы+
        private void groupAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddGroop(this);
            actionReact.ButtonClick();
        }
        //добавление группы +
        private void grAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddGroop(this);
            actionReact.ButtonClick();
        }
        //удаление группы +
        private void groupDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteGroop(this);
            actionReact.ButtonClick();
        }
        //переход к изменению группы+
        private void groupChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeGroop(this);
            actionReact.ButtonClick();
        }
        //переход к предметам+
        private void subjectMeny_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new SubjectMenu(this);
            actionReact.MenuClick();
        }
        //удаление предмета +
        private void subDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteSubject(this);
            actionReact.ButtonClick();
        }
        //добавление изменение предметов +
        private void subChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddChangeSubject(this);
            actionReact.ButtonClick();
        }
        //переход к гриду курсов+
        private void coursesMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CourseMenu(this);
            actionReact.MenuClick();
        }
        //ввод только чисел с запятой +
        public void grPayment_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            IButtonClick actionReact = new ChangeGroop(this);
            actionReact.ButtonClick();

        }
        //переход к добавлению курса+
        private void coursAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddCourse(this);
            actionReact.ButtonClick();
        }
        //добавление курса +
        private void courseAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddCourse(this);
            actionReact.ButtonClick();
        }
        //переход к гриду изменения курсов+
        private void coursChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeCourse(this);
            actionReact.ButtonClick();
        }
        //сохранение изменений курса +
        private void courseChangeButton2_Click(object sender, RoutedEventArgs e)
        {

            IButtonClick actionReact = new ChangeCourse(this);
            actionReact.ButtonClick();
        }
        //удаление курса +
        private void coursDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteCourse(this);
            actionReact.ButtonClick();

        }
        //выход из пользователя+
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new UnLogin(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToEmployeeToTeacher(this);
            actionReact.ButtonClick();

        }
        //сделать сотрудника штатным(переход к гриду)+
        private void allSotrToShtatBtton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToEmployeeToStuff(this);
            actionReact.ButtonClick();

        }
        //чекбокс в штате +
        public void Shtat_Checked(object sender, RoutedEventArgs e)
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
        public void Shtat_UnChecked(object sender, RoutedEventArgs e)
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
            IButtonClick actionReact = new EmployeeToTeacher(this);
            actionReact.ButtonClick();
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
            IMenuClick actionReact = new ScheduleGroopMenu(this);
            actionReact.MenuClick();
        }
        //клик на лейбл в расписании по группам +
        public void Label_MouseDown(object sender, MouseButtonEventArgs e)
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
            IButtonClick actionReact = new AddScheduleGroops(this);
            actionReact.ButtonClick();
        }
        //сохранение изменений в расписании по группам +
        private void raspChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeScheduleGroops(this);
            actionReact.ButtonClick();
        }
        //изменение записи в расписании(переход к форме) по группам+
        private void ChangeRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeScheduleGroop(this);
            actionReact.ButtonClick();
        }
        //переход к форме добавления записи в расписание по группам+
        private void AddRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddScheduleGroop(this);
            actionReact.ButtonClick();

        }
        //удаление записи из расписания по группам +
        private void DeleteRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteScheduleGroop(this);
            actionReact.ButtonClick();
        }
        //клик на кнопку в расписнии "Предидущее" по группам+
        private void PrevRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new PreviouslyWeek(this,sender);
            actionReact.ButtonClick();
        }
        //клик на кнопку в расписнии "Следующее" по группам+
        private void NextRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new NextWeek(this, sender);
            actionReact.ButtonClick();
        }
        //клик на кнопку в расписнии "На эту неделю" по группам+
        private void NuwRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new OnThisWeek(this, sender);
            actionReact.ButtonClick();
        }
        //расписание на новую неделю по группам+
        private void NewRaspBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ScheduleForNewWeek(this, sender);
            actionReact.ButtonClick();

        }
        //переход к слушателям+
        private void listenerMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ListenerMenu(this);
            actionReact.MenuClick();
        }
        //переход к добавлению слушателя+
        private void listenerAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddListener(this);
            actionReact.ButtonClick();
        }
        //добавление слушателя в базу +
        private void listenerAddBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddListener(this);
            actionReact.ButtonClick();

        }
        //удаление слушателя  + 
        private void listenerDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteListener(this);
            actionReact.ButtonClick();

        }
        //переход к гриду изменения слушателя+
        private void listenerChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeListener(this);
            actionReact.ButtonClick();
        }
        //сохранение изменения в слушателях +
        private void listenerChangeBut_Click(object sender, RoutedEventArgs e)
        {

            IButtonClick actionReact = new ChangeListener(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new AddChangeCabinet(this);
            actionReact.ButtonClick();
        }
        //удаление кабинета +
        private void cabDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteCabinet(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToAddScheduleCabinet(this);
            actionReact.ButtonClick();

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
            IButtonClick actionReact = new GoToChangeScheduleCabinet(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToAddScheduleTeacher(this);
            actionReact.ButtonClick();
        }
        //переход к форме изменения записи в расписании занятий по преподавателям+
        private void ChangeRaspButP_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeScheduleTeacher(this);
            actionReact.ButtonClick();
        }
        //удаление записи из расписания занятий по преподавателям +
        private void DeleteRaspButP_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteScheduleTeacher(this);
            actionReact.ButtonClick();
        }
        //удаление записи из расписания занятий по кабинетам +
        private void DeleteRaspButС_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteScheduleCabinet(this);
            actionReact.ButtonClick();
        }
        //добавление записи в таблицу расписание по преподавателям +
        private void raspAddButToTableP_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddScheduleTeacher(this);
            actionReact.ButtonClick();
        }
        //добавление записи в таблицу расписание по кабинетам +
        private void raspAddButToTableK_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddScheduleCabinet(this);
            actionReact.ButtonClick();
        }
        //имзенение записи в таблице расписание по преподавателям +
        private void raspChangeButtonP_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeScheduleTeacher(this);
            actionReact.ButtonClick();
        }
        //имзенение записи в таблице расписание по кабинетам +
        private void raspChangeButtonK_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeScheduleCabinet(this);
            actionReact.ButtonClick();
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
        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            tbxMas_gr_lg[indexText].IsEnabled = true;

        }
        //чекбокс (анчек) для выбора групп +
        public void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            tbxMas_gr_lg[indexText].IsEnabled = false;
            tbxMas_gr_lg[indexText].Text = "";

        }
        //переход к скидкам +
        private void skidki_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new DiscountMenu(this);
            actionReact.MenuClick();
        }
        //сохранение скидок +
        private void skidkiSave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeDiscounts(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new AddChangeProfitsType(this);
            actionReact.ButtonClick();
        }
        //удаление типов доходов +
        private void TypeDohDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteProfit(this);
            actionReact.ButtonClick();
        }
        //обновление/сохранение типов расходов +
        private void TypeRashChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddChangeCostsType(this);
            actionReact.ButtonClick();
        }
        //удаление типов расходов +
        private void TypeRashDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteCost(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new AddChangeWorkCoeff(this);
            actionReact.ButtonClick();
        }
        //удаление коефициента за выслуги лет +
        private void KoefDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteWorkCoeff(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new AddChangeServiceWork(this);
            actionReact.ButtonClick();
        }
        //удаление работ обслуживания +
        private void ObslWorkDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteServiceWork(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToAddPosition(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new AddPosition(this);
            actionReact.ButtonClick();
        }
        //удаление должности +
        private void StateDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeletePosition(this);
            actionReact.ButtonClick();
        }
        //переход к изменению должности+
        private void StateChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangePosition(this);
            actionReact.ButtonClick();
        }
        //сохранение изменений в должности +
        private void StateChaneBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangePosition(this);
            actionReact.ButtonClick();

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

            IButtonClick actionReact = new AddEmployeeToStaff(this);
            actionReact.ButtonClick();
        }
        //удаление из штата +
        private void shtatDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteFromStaff(this);
            actionReact.ButtonClick();
        }
        //переход к изменению штата+
        private void shtatChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeStaffEmployee(this);
            actionReact.ButtonClick();
        }
        //сохранение изменений в штате +
        private void ChangeShtat_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeStaffEmplyee(this);
            actionReact.ButtonClick();
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
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
        }
        //переход к предыдщуему месяцу+
        private void ShtatRaspPrevBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new PrevMonthStaffSchedule(this);
            actionReact.ButtonClick();
        }
        //переход к следующему месяцу+
        private void ShtatRaspNextBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new NextMonthStaffSchedule(this);
            actionReact.ButtonClick();
        }
        //сохранение расписания штатного сотрудника +
        private void ShtatRaspSaveBut_Click(object sender, RoutedEventArgs e)
        {
            Button but = sender as Button;
            if (but.Name == "ShtatRaspSaveBut")
            {
                IButtonClick actionReact = new SaveStaffSchedule(this);
                actionReact.ButtonClick();
            }

            if (but.Name == "ShtatRaspSelectAllBut")
            {
                IButtonClick actionReact = new SelectAllPersonalInStaffSchedule(this);
                actionReact.ButtonClick();
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
