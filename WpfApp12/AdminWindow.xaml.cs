
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.IO;
using WpfApp12.strategiesForAdmin;
using WpfApp12.strategiesForAdmin.strategiesForAdminMenuClick;

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
        public string connectionString = "";
        public filtr filtr = new filtr();
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
            IButtonClick actReact = new ToAdmin();
            actReact.buttonClick();
        }
        //переход из меню админа в меню бухгалтера+
        private void BuhgRoleA_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new ToBuhg(this);
            actReact.buttonClick();
        }
        //переход из меню админа в меню директора+
        private void DirectorRoleA_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new ToDirector(this);
            actReact.buttonClick();
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
            IButtonClick actReact = new GoToRegister(this);
            actReact.buttonClick();
        }
        //регистрация+
        private void register_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new Register(this);
            actReact.buttonClick(); 
        }
      
        //удаление пользователя+
        private void dellUser_Click(object sender, RoutedEventArgs e)
        { 
            IButtonClick actReact = new DelUser(this);
            actReact.buttonClick();
        }
        //переход к изменению пользователя+
        private void changeUser_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new GoToChangeUser(this);
            actReact.buttonClick();
        }
        //изменение  пользоателя+
        private void change_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new ChangeUser(this);
            actReact.buttonClick();
        }
        //
        //переход к окну бэкапа+
        private void crDump_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new GoToCreateBackUp(this);
            actReact.buttonClick();
        }

        //выбор папки СУБД+
        private void selectBin_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new SelectBin(this,sender);
            actReact.buttonClick();
        }
        //Выбор папки для бэкапа+
        private void selectbckp_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new SelectBackUpFolder(this, sender);
            actReact.buttonClick();
        }
        //Создание бэкапа+
        private void crDumpButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new BackUpCreate(this);
            actReact.buttonClick();
        }
        //выбор бэкапа+
        private void rsSelectbckp_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new SelectBackUp(this);
            actReact.buttonClick();
        }
        //выбор папки СУБД+
        private void rsSelectBin_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new SelectFolderBackUpRestore(this);
            actReact.buttonClick();
        }
        //переход к окну восстановления+
        private void rstrDump_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new GoToRestore(this);
            actReact.buttonClick();
        }
        //восстановление бэкапа+
        private void rsDumpButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new Restore(this);
            actReact.buttonClick();
        }
        //выход из пользователя+
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new UnLogin(this);
            actReact.buttonClick();
        }
        // переход к настройкам+
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new GoToSeatings(this);
            actReact.buttonClick();
        }
        //проверка подключения+
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new ConnectionCheck(this);
            actReact.buttonClick();
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
            IButtonClick actReact = new EnforcementSeatings(this);
            actReact.buttonClick();
        }
        //сохранение пароля рута+
        private void rootSave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new SaveRootPassword(this);
            actReact.buttonClick();
        }
        //переход к пользователям+
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            IMenuClick actReact = new UsersMenuClick(this);
            actReact.MenuClick();
        }
        //клик по меню права+
        private void MenuRolesA_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actReact = new AccessMenuClick(this);
            actReact.MenuClick();
        }
        //клик по меню архив+
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            IMenuClick actReact = new ArchiveMenuClick(this);
            actReact.MenuClick();
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
            IButtonClick actReact = new FilterApp(this);
            actReact.buttonClick();
        }
        //переход к следующему году+
        private void ToNextYearMenu_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new GoToNextYear(this);
            actReact.buttonClick();
        }
        //переход на новый год
        private void crDumpButtonNextYear_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actReact = new ToNextYear(this);
            actReact.buttonClick();
        }
    }
}
