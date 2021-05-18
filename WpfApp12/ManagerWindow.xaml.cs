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
        
        public CheckBox[] chbxMas;
        public string FIO = "";
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


        public filtr filtr = new filtr();

        public filtr PeopleFromCashboxFilter = new filtr();
        public filtr ProfitTypesFromCashboxFilter = new filtr();

        public filtr StaffFromCashboxFiltr = new filtr();
        public filtr CostsTypesFromCashboxFilter = new filtr();


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
            filtr.connectionString = connectionString;
            PeopleFromCashboxFilter.connectionString = connectionString;
            StaffFromCashboxFiltr.connectionString = connectionString;
            ProfitTypesFromCashboxFilter.connectionString = connectionString;
            CostsTypesFromCashboxFilter.connectionString = connectionString;
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
            strategiesForManager.OtherMethods.HideAll.Hide(this);

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
            IMenuClick actionReact = new RoleMenu(this);
            actionReact.MenuClick();
        }
        //клик по меню расписания +
        private void raspMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ScheduleMenu(this);
            actionReact.MenuClick();
        }
        //клик по меню персонала +
        private void sotrMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new PersonnelMenu(this);
            actionReact.MenuClick();
        }
        //клик по меню обучения +
        private void obuchMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new LearningMenu(this);
            actionReact.MenuClick();
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
                textBoxArrRate[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
            if (cb.Name.Split('_')[3] == "obsl")
            {
                textBoxArrVolumeWork[Convert.ToInt32(cb.Name.Split('_')[1])].IsEnabled = true;
            }
        }
        public void Shtat_UnChecked(object sender, RoutedEventArgs e)
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
        //добавление сотрудника в таблицу преподавателей +
        private void addToPrerB_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new EmployeeToTeacher(this);
            actionReact.ButtonClick();
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
            IMousDown actionReact = new LabelClickFromGroopsSchedule(this, sender);
            actionReact.MousDown();
           
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
            ISelectedChanged actionReact = new ControlButtonState(this);
            actionReact.SelectedChanged();
        }
        //переход к гриду кабинетов+
        private void kabMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CabinteMenu(this);
            actionReact.MenuClick();
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
        
        //переход к расписанию уроков по преподавателям+
        private void yrokiPMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ScheduleOfLessonTeachers(this);
            actionReact.MenuClick();
        }
        
        //переход к расписанию уроков по кабинетам+
        private void yrokiCMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ScheduleOfLessonCabinet(this);
            actionReact.MenuClick();
        }
        //переход к форме добавления в расписание занятий по кабинетам +
        private void AddRaspButС_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddScheduleCabinet(this);
            actionReact.ButtonClick();

        }
        //изменение группы в расписании по кабинетам/преподам при добавлении+
        private void raspAddGroupK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box.Name == "raspAddGroupK")
            {
                ISelectionChaged actionReact = new ChangeGroopFromAddCabinetSchedule(this);
                actionReact.SelectionChanged();
            }

            if (box.Name == "raspAddGroupP")
            {
                ISelectionChaged actionReact = new ChangeGroopFromAddTeacherSchedule(this);
                actionReact.SelectionChanged();
            }

        }
        //переход к форме изменения в расписании занятий по кабинетам+
        private void ChangeRaspButС_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeScheduleCabinet(this);
            actionReact.ButtonClick();
        }
        //изменение группы в изменении в расписании по кабинетам/преподам+
        private void raspChangeGroupK_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox box = sender as ComboBox;
            if (box.Name == "raspChangeGroupK")
            {
                ISelectionChaged actionReact = new ChangeGroopFromChangeCabinetSchedule(this);
                actionReact.SelectionChanged();
            }

            if (box.Name == "raspChangeGroupP")
            {
                ISelectionChaged actionReact = new ChangeGroopFromChangeTeacherSchedule(this);
                actionReact.SelectionChanged();
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
            IMenuClick actionReact = new TypeOfCostsMenu(this);
            actionReact.MenuClick();
        }
        //переход к таблице доходов +
        private void doh_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new TypeOfProfitsMenu(this);
            actionReact.MenuClick();
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
            IMenuClick actionReact = new FineMenu(this);
            actionReact.MenuClick();
        }
        //переход к работе с коефициентом выслуги лет+
        private void koefVisl_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new WorkCoeffMenu(this);
            actionReact.MenuClick();
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
            IMenuClick actionReact = new ServiceWorkMenu(this);
            actionReact.MenuClick();
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
            IMenuClick actionReact = new PositionMenu(this);
            actionReact.MenuClick();
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
            IMenuClick actionReact = new StaffMenu(this);
            actionReact.MenuClick();
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
            IMenuClick actionReact = new ScheduleOfStaff(this);
            actionReact.MenuClick();
        }
        //клик по лейблу штатного расписания +
        public void Label_shtatRasp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            IMousDown actionReact = new LabelClickFromStaffSchedule(this, sender);
            actionReact.MousDown();
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
            ApplyFiltersButtonClick.ApplyForManager(this, sender);
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
        //переход к гриду отчета кассы+
        private void kassaMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CashboxReportMenu(this);
            actionReact.MenuClick();
        }
        //+
        private void SpiskiVyplatMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new StatisticMenu(this);
            actionReact.MenuClick();
        }
        //+
        private void StatistikaMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actiomReact = new PaymentListMenu(this);
            actiomReact.MenuClick();
        }
        //клилк по меню отчётов
        private void MenuOtchety_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actiomReact = new ReportMenu(this);
            actiomReact.MenuClick();
        }
        //применение фильтров для кассы
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button bt = sender as Button;
            if (bt.Name == "PrimFKD") 
            {
                PeopleFromCashboxFilter.ApplyDohFiltr(ProfitTypesFromCashboxFilter);
                DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel, PeopleFromCashboxFilter.sql, StaffFromCashboxFiltr.sql);
            }
            if (bt.Name == "PrimFKR") 
            {

                StaffFromCashboxFiltr.ApplyRashFiltr(CostsTypesFromCashboxFilter);
                DataGridUpdater.updateGridKassa(connectionString, KassaDodohGrid, KassaRashodGrid, kassaTitleLabel, KassaItogoDohod, KassaItogoRashod, kassaAllDohodLabel, PeopleFromCashboxFilter.sql, StaffFromCashboxFiltr.sql);
            }
        }
    }
}
