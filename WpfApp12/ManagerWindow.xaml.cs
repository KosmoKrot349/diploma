using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Collections;
using WpfApp12.strategiesForManager.ButtonClick;
using WpfApp12.strategiesForManager.MenuClick;
using WpfApp12.strategiesForManager.LabelMousDown;
using WpfApp12.strategiesForManager.SelectedChanged;
using WpfApp12.strategiesForManager.SelectionChanged;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для DirectorWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        
public int logUser;
        public int teacherID = -1;
        public int groopID = -1;
        public int courseID = -1;
        public int employeeID = -1;
        public string dontChangeCourseName = "";
        public string dontChangeGroopName = "";

        //массивы дял слушателей
        public CheckBox[] checkBoxArrForListeners;
        public TextBox[] textBoxArrForListeners;
        
        public CheckBox[] checkBoxArr;
        public string userName = "";
        public Label[,] labelArr;
        public int quanLessonsInDay = 0;//число зантий в дне
        public int quanGroops = 0;//число групп
        public int iCoordScheduleLabel;
        public int jCoordScheduleLabel;
        public DateTime dateMonday;
        public int listenerID = -1;
        public int positionID = -1;
        public int staffID = -1;
        public bool selectd = false;
        //массивы для штата 
        public TextBox[] textBoxArrRate;
        public CheckBox[] checkBoxArrPositions;
        public TextBox[] textBoxArrVolumeWork;
        public CheckBox[] checkBoxArrServiceWorks;

        //массивы для штатного расписания
        public CheckBox[] checkBoxArrForStaffSchedule;
        public Label[,] labelArrForStaffSchedule = new Label[7, 7];
        public DateTime date = DateTime.Now;


        public filter filter = new filter();

        public filter PeopleFromCashboxFilter = new filter();
        public filter ProfitTypesFromCashboxFilter = new filter();

        public filter StaffFromCashboxFiltr = new filter();
        public filter CostsTypesFromCashboxFilter = new filter();

       
        IMenuClick actionReactMenu;
        ISelectionChaged actionReactComboBox;
        //фильтр для всех сотрудников
        public string sqlForAllEmployees = "";
        //строка подключения
        public string connectionString = "";
    

       //+
        public ManagerWindow()
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
            filter.connectionString = connectionString;
            PeopleFromCashboxFilter.connectionString = connectionString;
            StaffFromCashboxFiltr.connectionString = connectionString;
            ProfitTypesFromCashboxFilter.connectionString = connectionString;
            CostsTypesFromCashboxFilter.connectionString = connectionString;
            MenuRoles.BorderBrush = null;
            ScheduleMenu.BorderBrush = null;
            EmployeesMenu.BorderBrush = null;
            LearningMenu.BorderBrush = null;
            ReportsMenu.BorderBrush = null;
            DiscountMenu.BorderBrush = null;
        }
        //+
        public void HideAll()
        {
            strategiesForManager.OtherMethods.HideAll.Hide(this);

        }
        //клики на кнопку
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReactButtonClick=null;
            Button button = sender as Button;
            switch (button.Name)
            {
                //удаление записи в расписаии звонков
                case "TimeScheduleDelete": { actionReactButtonClick = new DeleteFromTimeSchedule(this);break; }
                //добавление/изменение записи в расписании звоноков
                case "TimeScheduleAddChange": { actionReactButtonClick = new AddChangeToTimeSchedule(this); break; }
                //добавление категори
                case "AddChangeCategory": { actionReactButtonClick = new AddChangeCategory(this); break; }
                //Удаление категории
                case "DeleteCategory": { actionReactButtonClick = new DeleteCategory(this); break; }
                //сохранение изменений в таблице преподавателей
                case "ChangeTeacher": { actionReactButtonClick = new ChangeTeacher(this); break; }
                //добавление изменение сотрудника
                case "EmployeeAddChange": { actionReactButtonClick = new AddChangeEmployee(this); break; }
                //удаление сотрудника
                case "EmployeeDelet":{ actionReactButtonClick = new DeleteEmployee(this); break; }
                //удаление препода
                case "TeacherDelete":{ actionReactButtonClick = new DeleteTeacher(this); break; }
                //переход к изменеию препода 
                case "GoToChangeTeacher": { actionReactButtonClick = new GoToChangeTeacher(this); break; }
                //переход к гриду добавления группы+
                case "GoToAddGroop": { actionReactButtonClick = new GoToAddGroop(this); break; }
                //добавление группы
                case "AddGroop": { actionReactButtonClick = new AddGroop(this); break; }
                //удаление группы
                case "DeleteGroop": { actionReactButtonClick = new DeleteGroop(this); break; }
                //переход к изменению группы
                case "GoToChangeGroop": { actionReactButtonClick = new GoToChangeGroop(this); break; }
                //удаление предмета +
                case "DeleteSubject":{ actionReactButtonClick = new DeleteSubject(this); break; }
                //добавление изменение предметов +
                case "AddChangeSubject":{ actionReactButtonClick = new AddChangeSubject(this); break; }
                //добавление изменение предметов +
                case "ChangeGroop":{ actionReactButtonClick = new ChangeGroop(this); break; }
                //переход к добавлению курса+
                case "GoToAddCourse":{ actionReactButtonClick = new GoToAddCourse(this); break; }
                //добавление курса + 
                case "AddCourse":{ actionReactButtonClick = new AddCourse(this); break; }
                //переход к гриду изменения курсов
                case "GoToChangeCourse":{ actionReactButtonClick = new GoToChangeCourse(this); break; }
                //сохранение изменений курса +
                case "ChangeCourse":{ actionReactButtonClick = new ChangeCourse(this); break; }
                //удаление курса +
                case "DeleteCourse":{ actionReactButtonClick = new DeleteCourse(this); break; }
                //сделать сотрудника преподавателем(переход к гриду)+
                case "GoToEmployeeToTeacher":{ actionReactButtonClick = new GoToEmployeeToTeacher(this); break; }
                //сделать сотрудника штатным(переход к гриду)+
                case "GoToEmployeeToStuff":{ actionReactButtonClick = new GoToEmployeeToStuff(this); break; }
                //добавление сотрудника в таблицу преподавателей +
                case "EmployeeToTeacher":{ actionReactButtonClick = new EmployeeToTeacher(this); break; }
                //добавление записив таблицу расписания по группам +
                case "AddScheduleGroops":{ actionReactButtonClick = new AddScheduleGroops(this); break; }
                //сохранение изменений в расписании по группам +
                case "ChangeScheduleGroops":{ actionReactButtonClick = new ChangeScheduleGroops(this); break; }
                //изменение записи в расписании(переход к форме) по группам+
                case "GoToChangeScheduleGroop":{ actionReactButtonClick = new GoToChangeScheduleGroop(this); break; }
                //переход к форме добавления записи в расписание по группам+
                case "GoToAddScheduleGroop":{ actionReactButtonClick = new GoToAddScheduleGroop(this); break; }
                //удаление записи из расписания по группам +
                case "DeleteScheduleGroop":{ actionReactButtonClick = new DeleteScheduleGroop(this); break; }
                //клик на кнопку в расписнии "Предидущее" по группам+
                case "PreviouslyWeekGroopSchedule": { actionReactButtonClick = new PreviouslyWeek(this,sender); break; }
                //клик на кнопку в расписнии "Предидущее" по преподам+
                case "PreviouslyWeekTeacherSchedule": { actionReactButtonClick = new PreviouslyWeek(this, sender); break; }
                //клик на кнопку в расписнии "Предидущее" по кабинетам+
                case "PreviouslyWeekCabinetSchedule": { actionReactButtonClick = new PreviouslyWeek(this, sender); break; }
                //клик на кнопку в расписнии "Следующее" по группам+
                case "NextWeekGroopSchedule": { actionReactButtonClick = new NextWeek(this,sender); break; }
                //клик на кнопку в расписнии "Следующее" по преподам+
                case "NextWeekTeacherSchedule": { actionReactButtonClick = new NextWeek(this, sender); break; }
                //клик на кнопку в расписнии "Следующее" по кабинетам+
                case "NextWeekCabinetSchedule": { actionReactButtonClick = new NextWeek(this, sender); break; }
                //клик на кнопку в расписнии "На эту неделю" по группам+
                case "OnThisWeekGroopSchedule": { actionReactButtonClick = new OnThisWeek(this,sender); break; }
                //клик на кнопку в расписнии "На эту неделю" по преподам+
                case "OnThisWeekTeacherSchedule": { actionReactButtonClick = new OnThisWeek(this, sender); break; }
                //клик на кнопку в расписнии "На эту неделю" по кабинетам+
                case "OnThisWeekCabinetSchedule": { actionReactButtonClick = new OnThisWeek(this, sender); break; }
                //расписание на новую неделю по группам+
                case "ScheduleForNewWeekroopSchedule": { actionReactButtonClick = new ScheduleForNewWeek(this,sender); break; }
                //расписание на новую неделю по преподам+
                case "ScheduleForNewWeeTeachersSchedule": { actionReactButtonClick = new ScheduleForNewWeek(this, sender); break; }
                //расписание на новую неделю по кабинетам+
                case "ScheduleForNewWeeCabinetsSchedule": { actionReactButtonClick = new ScheduleForNewWeek(this, sender); break; }
                //переход к добавлению слушателя+
                case "GoToAddListener": { actionReactButtonClick = new GoToAddListener(this); break; }
                //добавление слушателя в базу +
                case "AddListener": { actionReactButtonClick = new AddListener(this); break; }
                //удаление слушателя  + 
                case "DeleteListener": { actionReactButtonClick = new DeleteListener(this); break; }
                //переход к гриду изменения слушателя+
                case "GoToChangeListener": { actionReactButtonClick = new GoToChangeListener(this); break; }
                //сохранение изменения в слушателях +
                case "ChangeListener": { actionReactButtonClick = new ChangeListener(this); break; }
                //добавление и изменение в кабинетах +
                case "AddChangeCabinet": { actionReactButtonClick = new AddChangeCabinet(this); break; }
                //удаление кабинета +
                case "DeleteCabinet": { actionReactButtonClick = new DeleteCabinet(this); break; }
                //переход к форме добавления в расписание занятий по кабинетам +
                case "GoToAddScheduleCabinet": { actionReactButtonClick = new GoToAddScheduleCabinet(this); break; }
                //переход к форме изменения в расписании занятий по кабинетам+
                case "GoToChangeScheduleCabinet": { actionReactButtonClick = new GoToChangeScheduleCabinet(this); break; }
                //переход к форме добавления в роасписание занятий по преподавателю+
                case "GoToAddScheduleTeacher": { actionReactButtonClick = new GoToAddScheduleTeacher(this); break; }
                //удаление записи из расписания занятий по преподавателям +
                case "DeleteScheduleTeacher": { actionReactButtonClick = new DeleteScheduleTeacher(this); break; }
                //переход к форме изменения записи в расписании занятий по преподавателям+
                case "GoToChangeScheduleTeacher": { actionReactButtonClick = new GoToChangeScheduleTeacher(this); break; }
                //удаление записи из расписания занятий по кабинетам +
                case "DeleteScheduleCabinet": { actionReactButtonClick = new DeleteScheduleCabinet(this); break; }
                //добавление записи в таблицу расписание по преподавателям +
                case "AddScheduleTeacher": { actionReactButtonClick = new AddScheduleTeacher(this); break; }
                //добавление записи в таблицу расписание по кабинетам +
                case "AddScheduleCabinet": { actionReactButtonClick = new AddScheduleCabinet(this); break; }
                //имзенение записи в таблице расписание по кабинетам +
                case "ChangeScheduleCabinet": { actionReactButtonClick = new ChangeScheduleCabinet(this); break; }
                //имзенение записи в таблице расписание по преподавателям +
                case "ChangeScheduleTeacher": { actionReactButtonClick = new ChangeScheduleTeacher(this); break; }
                //сохранение скидок +
                case "ChangeDiscounts":{ actionReactButtonClick = new ChangeDiscounts(this); break; }
                //обновление/сохранение типов дохода +
                case "AddChangeProfitsType":{ actionReactButtonClick = new AddChangeProfitsType(this); break; }
                //удаление типов доходов +
                case "DeleteProfit":{ actionReactButtonClick = new DeleteProfit(this); break; }
                //обновление/сохранение типов расходов +
                case "AddChangeCostsType":{ actionReactButtonClick = new AddChangeCostsType(this); break; }
                //удаление типов расходов +
                case "DeleteCost":{ actionReactButtonClick = new DeleteCost(this); break; }
                //обновление/сохранение коефициента за выслуги лет +
                case "AddChangeWorkCoeff":{ actionReactButtonClick = new AddChangeWorkCoeff(this); break; }
                //удаление коефициента за выслуги лет +
                case "DeleteWorkCoeff":{ actionReactButtonClick = new DeleteWorkCoeff(this); break; }
                //удаление работ обслуживания +
                case "DeleteServiceWork":{ actionReactButtonClick = new DeleteServiceWork(this); break; }
                //обновление/сохранение работ обслуживания +
                case "AddChangeServiceWork":{ actionReactButtonClick = new AddChangeServiceWork(this); break; }
                //переход к добавлению должности+
                case "GoToAddPosition":{ actionReactButtonClick = new GoToAddPosition(this); break; }
                //добавление должности в базу +
                case "AddPosition":{ actionReactButtonClick = new AddPosition(this); break; }
                //удаление должности +
                case "DeletePosition":{ actionReactButtonClick = new DeletePosition(this); break; }
                //переход к изменению должности+
                case "GoToChangePosition":{ actionReactButtonClick = new GoToChangePosition(this); break; }
                //сохранение изменений в должности +
                case "ChangePosition":{ actionReactButtonClick = new ChangePosition(this); break; }
                //добавление сотрдуника в таблицу штата  +
                case "AddEmployeeToStaff":{ actionReactButtonClick = new AddEmployeeToStaff(this); break; }
                //удаление из штата +
                case "GoToChangeStaffEmployee":{ actionReactButtonClick = new GoToChangeStaffEmployee(this); break; }
                //переход к предыдщуему месяцу+
                case "PrevMonthStaffSchedule":{ actionReactButtonClick = new PrevMonthStaffSchedule(this); break; }
                //переход к следующему месяцу+
                case "NextMonthStaffSchedule":{ actionReactButtonClick = new NextMonthStaffSchedule(this); break; }
                //Сохранение штатного расписания
                case "SaveStaffSchedule":{ actionReactButtonClick = new SaveStaffSchedule(this); break; }
                //Выбор всех в штатном расписании
                case "SelectAllPersonalInStaffSchedule":{ actionReactButtonClick = new SelectAllPersonalInStaffSchedule(this); break; }
                //сохранение изменений в штате +
                case "ChangeStaffEmplyee":{ actionReactButtonClick = new ChangeStaffEmplyee(this); break; }
                //применение фильтров для кассы доходы
                case "ApplyProfitFilterForCashboxReport":{PeopleFromCashboxFilter.ApplyProfitFilterForCashboxReport(ProfitTypesFromCashboxFilter); DataGridUpdater.updateCashBoxGrid(connectionString, CashboxProfitGrid, CashboxCostsGrid, CashboxTitleLabel, CashboxTotalProfit, CashboxTotalCosts, CashboxFinalProfit, PeopleFromCashboxFilter.sql, StaffFromCashboxFiltr.sql); break;}
                //применение фильтров для кассы расходы
                case "ApplyCostsFilterForCashboxReport":{StaffFromCashboxFiltr.ApplyCostsFilterForCashboxReport(CostsTypesFromCashboxFilter);DataGridUpdater.updateCashBoxGrid(connectionString, CashboxProfitGrid, CashboxCostsGrid, CashboxTitleLabel, CashboxTotalProfit, CashboxTotalCosts, CashboxFinalProfit, PeopleFromCashboxFilter.sql, StaffFromCashboxFiltr.sql); break; }
                  

            }
           if(actionReactButtonClick!=null) actionReactButtonClick.ButtonClick();
        }
        //клик по меню
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = sender as Menu;
            switch (menu.Name)
            {
                //переход из меню директора в меню админа+
                case "GoToAdminMenu": { actionReactMenu = new GoToAdmin(this);  break; }
                 //переход из меню директора в меню админа+
                case "GoToBookkeeperMenu": { actionReactMenu = new GoToBookkeeper(this);  break; }
                //переход из меню директора в меню директора+
                case "GoToManagerMenu": { actionReactMenu = new GoToManager();  break; }
                //переход к расписанию звоноков+
                case "TimeScheduleMenu": { actionReactMenu = new TimeScheduleMenu(this);  break; }
                //переход к сотрудникам+
                case "AllEmployeesMenu":{ actionReactMenu = new EmploeesMenu(this); break; }
                 //переход к преподавателям+
                 case "TeachersMenu":{ actionReactMenu = new TeachersMenu(this); break; }
                //переход к категориям+
                case "CategoriesMenu": { actionReactMenu = new CategoryMenu(this); break; }
                 //переход к группам+
                 case "GroupsMenu": { actionReactMenu = new GroopMenu(this); break; }
                 //переход к предметам+
                 case "SubjectMeny": { actionReactMenu = new SubjectMenu(this); break; }
                 //переход к гриду курсов+
                 case "CoursesMenu": { actionReactMenu = new CourseMenu(this); break; }
                 //клик по меню прав +
                 case "MenuRoles": { actionReactMenu = new RoleMenu(this); break; }
                 //клик по меню расписания +
                 case "ScheduleMenu": { actionReactMenu = new ScheduleMenu(this); break; }
                 //клик по меню персонала +
                 case "EmployeesMenu": { actionReactMenu = new PersonnelMenu(this); break; }
                 //клик по меню обучения +
                  case "LearningMenu": { actionReactMenu = new LearningMenu(this); break; }
                  //переход к расписанию уроков по группам +
                  case "GroopsScheduleMenu": { actionReactMenu = new ScheduleGroopMenu(this); break; }
                   //переход к слушателям+
                   case "ListenerMenu": { actionReactMenu = new ListenerMenu(this); break; }
                    //переход к гриду кабинетов+
                    case "CabinetMenu": { actionReactMenu = new CabinteMenu(this); break; }
                    //переход к расписанию уроков по преподавателям+
                    case "TeachersScheduleMenu": { actionReactMenu = new ScheduleOfLessonTeachers(this); break; }
                    //переход к расписанию уроков по кабинетам+
                    case "CabinetsScheduleMenu": { actionReactMenu = new ScheduleOfLessonCabinet(this); break; }
                    //переход к скидкам +
                    case "DiscountsMenu": { actionReactMenu = new DiscountMenu(this); break; }
                    //переход к таблице расходов +
                    case "TypeCostsMenu": { actionReactMenu = new TypeOfCostsMenu(this); break; }
                    //переход к таблице доходов +
                    case "TypeProfitMenu": { actionReactMenu = new TypeOfProfitsMenu(this); break; }
                    //пеня +
                    case "FineMenu": { actionReactMenu = new FineMenu(this); break; }
                    //переход к работе с коефициентом выслуги лет+
                     case "WorkCoeffMenu": { actionReactMenu = new WorkCoeffMenu(this); break; }
                    //переход к работам обслуживания+
                     case "ServiceWorkMenu": { actionReactMenu = new ServiceWorkMenu(this); break; }
                    //переход к должностям+
                    case "PositionsMenu": { actionReactMenu = new PositionMenu(this); break; }
                    //переход к штату +
                    case "StaffMenu": { actionReactMenu = new StaffMenu(this); break; }
                    //переход к штатному расписанию+
                    case "StaffScheduleMenu": { actionReactMenu = new ScheduleOfStaff(this); break; }
                    //переход к гриду отчета кассы+
                    case "CashboxReportMenu": { actionReactMenu = new CashboxReportMenu(this); break; }
                    //Списки выплат+
                    case "PaymentListReportMenu": { actionReactMenu = new PaymentListMenu(this); break; }
                    //Статистика 
                    case "StatisticReportMenu": { actionReactMenu = new StatisticMenu(this); break; }
                    //клилк по меню отчётов
                    case "ReportsMenu": { actionReactMenu = new ReportMenu(this); break; }
                    //выход из пользователя+
                    case "Leave": { actionReactMenu = new UnLogin(this); break; }
                  

            }
            actionReactMenu.MenuClick();
        }
        //клик на лейбл в расписании по группам +
        public void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IMousDown actionReact = new LabelClickFromGroopsSchedule(this, sender);
            actionReact.MousDown();

        }
        //изменение кнопок контроля для DataGrid+
        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ISelectedChanged actionReact = new ControlButtonState(this);
            actionReact.SelectedChanged();
        }
        //выбор в combobox
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           ComboBox comboBox = sender as ComboBox;
            switch (comboBox.Name)
            {
                //изменение группы в расписании по кабинетам/преподам при добавлении+
                case "TeacherScheduleSelectGroop":{ actionReactComboBox = new ChangeGroopFromAddTeacherSchedule(this);  break; }
                //изменение группы в расписании по кабинетам/преподам при добавлении+
                case "CabinetScheduleGroop":{ actionReactComboBox = new ChangeGroopFromAddCabinetSchedule (this);  break; }
                    //изменение группы в изменении в расписании по кабинетам/преподам+
                     case "CabinetScheduleChangeGroop": { actionReactComboBox = new ChangeGroopFromChangeCabinetSchedule(this);  break; }
                    //изменение группы в изменении в расписании по кабинетам/преподам+
                     case "TeacherScheduleChangeGroop":{ actionReactComboBox = new ChangeGroopFromChangeTeacherSchedule(this);  break; }
                    //выбор варианта фильтрации штата+
                    case "StaffFilterCMBX":{ actionReactComboBox = new ChangeFilterTypeForStaff(this);  break; }
                    

            }
            actionReactComboBox.SelectionChanged();
        }
        //ввод только чисел с запятой +
        public void DigitWithDot_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
               e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            { MessageBox.Show("Test");e.Handled = true;  }
            
        }
        //чекбокс в штате +
        public void Staff_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name.Split('_')[3] == "state")
            {
                textBoxArrRate[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
            if (cb.Name.Split('_')[3] == "obsl")
            {
                textBoxArrVolumeWork[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
        }
        public void Staff_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Name.Split('_')[3] == "state")
            {
                textBoxArrRate[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = false;
            }
            if (cb.Name.Split('_')[3] == "obsl")
            {
                textBoxArrVolumeWork[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = false;
            }
        }
        //подсчёт общей оплаты за год для группы +
        private void PaymentSumForGroop_TextChanged(object sender, TextChangedEventArgs e)
        {
            double rez = 0;
            if (GroopAddPaymentFor1Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor1Month.Text);
            if (GroopAddPaymentFor2Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor2Month.Text);
            if (GroopAddPaymentFor3Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor3Month.Text);
            if (GroopAddPaymentFor4Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor4Month.Text);
            if (GroopAddPaymentFor5Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor5Month.Text);
            if (GroopAddPaymentFor6Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor6Month.Text);
            if (GroopAddPaymentFor7Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor7Month.Text);
            if (GroopAddPaymentFor8Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor8Month.Text);
            if (GroopAddPaymentFor9Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor9Month.Text);
            if (GroopAddPaymentFor10Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor10Month.Text);
            if (GroopAddPaymentFor11Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor11Month.Text);
            if (GroopAddPaymentFor12Month.Text != "") rez += Convert.ToDouble(GroopAddPaymentFor12Month.Text);
            GroopAddPayForYear.Content = Math.Round(rez,2);

            double rez2 = 0;
            if (GroopChangePaymentFor1Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor1Month.Text);
            if (GroopChangePaymentFor2Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor2Month.Text);
            if (GroopChangePaymentFor3Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor3Month.Text);
            if (GroopChangePaymentFor4Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor4Month.Text);
            if (GroopChangePaymentFor5Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor5Month.Text);
            if (GroopChangePaymentFor6Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor6Month.Text);
            if (GroopChangePaymentFor7Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor7Month.Text);
            if (GroopChangePaymentFor8Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor8Month.Text);
            if (GroopChangePaymentFor9Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor9Month.Text);
            if (GroopChangePaymentFor10Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor10Month.Text);
            if (GroopChangePaymentFor11Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor11Month.Text);
            if (GroopChangePaymentFor12Month.Text != "") rez2 += Convert.ToDouble(GroopChangePaymentFor12Month.Text);
            GroopChangePayForYear.Content = Math.Round(rez2, 2);
        }
        //чекбокс (чек) для выбора групп +
        public void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            textBoxArrForListeners[indexText].IsEnabled = true;

        }
        //чекбокс (анчек) для выбора групп +
        public void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox chb = sender as CheckBox;
            int indexText = Convert.ToInt32(chb.Name.Split('_')[1]);
            textBoxArrForListeners[indexText].IsEnabled = false;
            textBoxArrForListeners[indexText].Text = "";

        }
        //цифры без запятой +
        private void Digit_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }
        //клик по лейблу штатного расписания +
        public void Label_StaffSchedule_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IMousDown actionReact = new LabelClickFromStaffSchedule(this, sender);
            actionReact.MousDown();
        }
        //применение фильтров+
        private void FilterGroupsButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltersButtonClick.ApplyForManager(this, sender);
        }
        //выбор варианта фильтрации всех сотрудников+
        private void SelectedFilter_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cbx = sender as CheckBox;
            if (cbx.Name == "FirstMethodFilter") SecondMethodFilter.IsChecked = false;
            else FirstMethodFilter.IsChecked = false;
        }
        
      
      
    }
}
