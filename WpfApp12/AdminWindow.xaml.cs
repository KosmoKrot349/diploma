
using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections;
using System.IO;
using WpfApp12.strategiesForAdmin;
using WpfApp12.strategiesForAdmin.MenuClick;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public int adminRole = 0;
        public int managerRole = 0;
        public int bookkeeperRole = 0;
        public int userID = 0;
        public int logUser;
        public string UserName = "";
        //строка подключения
        public string connectionString = "";
        public filter filter = new filter();

        IButtonClick actionReactButton;
        IMenuClick actionReactMenu;
        public AdminWindow()
        {
            InitializeComponent();
            StreamReader reader = new StreamReader(@"setting.txt");
            ArrayList ls = new ArrayList();
            while (!reader.EndOfStream)
            {
                ls.Add(reader.ReadLine());
            }
            object[] arr = ls.ToArray();
            connectionString = "Server=" + arr[0].ToString().Split(':')[1] + ";Port=" + arr[2].ToString().Split(':')[1] + ";User Id=postgres;Password=" + arr[1].ToString().Split(':')[1] + ";Database=db";
            filter.connectionString = connectionString;
            MenuRoles.BorderBrush = null;
            usersMenu.BorderBrush = null;
            archiveMenu.BorderBrush = null;
            settingMenu.BorderBrush = null;
            GoToNextYear.BorderBrush = null;
        }
        //+
        public void hideAll()
        {
            strategiesForAdmin.OtherMethods.HideAll.Hide(this);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                //переход к созданию пользователя
                case "GoToRegister": { actionReactButton = new GoToRegister(this); break; }
                //регистрация+
                case "Register": { actionReactButton = new Register(this); break; }
                //удаление пользователя+
                case "DelUser": { actionReactButton = new DelUser(this); break; }
                //переход к изменению пользователя+
                case "GoToChangeUser": { actionReactButton = new GoToChangeUser(this); break; }
                //изменение  пользоателя+
                case "ChangeUser": { actionReactButton = new ChangeUser(this); break; }
                //выбор папки СУБД+
                case "SelectBinForNextYear": { actionReactButton = new SelectBin(this,sender); break; }
                //Выбор папки для бэкапа+
                case "SelectBackUpFolderForNextYear": { actionReactButton = new SelectBackUpFolder(this,sender); break; }
                //выбор папки СУБД+
                case "SelectBin": { actionReactButton = new SelectBin(this, sender); break; }
                //Выбор папки для бэкапа+
                case "SelectBackUpFolder": { actionReactButton = new SelectBackUpFolder(this, sender); break; }
                //Создание бэкапа+
                case "BackUpCreate": { actionReactButton = new BackUpCreate(this); break; }
                //выбор бэкапа+
                case "SelectBackUp": { actionReactButton = new SelectBackUp(this); break; }
                //выбор папки СУБД+
                case "SelectFolderBackUpRestore": { actionReactButton = new SelectFolderBackUpRestore(this); break; }
                //восстановление бэкапа+
                case "Restore": { actionReactButton = new Restore(this); break; }
                //проверка подключения+
                case "ConnectionCheck": { actionReactButton = new ConnectionCheck(this); break; }
                //принудительное сохранение+
                case "EnforcementSeatings":{ actionReactButton = new EnforcementSeatings(this); break; }
                //сохранение пароля рута+
                case "SaveRootPassword":{ actionReactButton = new SaveRootPassword(this); break; }
                //применение фильтра по ролям
                case "FilterApp":{ actionReactButton = new FilterApp(this); break; }
                //переход на новый год
                case "ToNextYear":{ actionReactButton = new ToNextYear(this); break; }
                  

            }

        actionReactButton.buttonClick();
        }
        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            switch (item.Name)
            {
                //переход к следующему году+
                case "GoToNextYear": { actionReactMenu = new GoToNextYear(this); break; }
                //переход из меню админа в меню админа+
                case "GoToAdminMenu": { actionReactMenu = new ToAdmin(); break; }
                //переход из меню админа в меню бухгалтера+
                case "GoToBookkeerMenu": { actionReactMenu = new ToBookkeeper(this); break; }
                //переход из меню админа в меню директора+
                case "GoToManagerMenu": { actionReactMenu = new ToManager(this); break; }
                //переход к окну бэкапа+
                case "CreateBackUpMenu": { actionReactMenu = new GoToCreateBackUp(this); break; }
                //переход к окну восстановления+
                case "RestoreBackUpMenu": { actionReactMenu = new GoToRestore(this); break; }
                //выход из пользователя+
                case "Leave": { actionReactMenu = new UnLogin(this); break; }
                // переход к настройкам+
                case "settingMenu": { actionReactMenu = new UnLogin(this); break; }
                //переход к пользователям+
                case "usersMenu": { actionReactMenu = new UsersMenuClick(this); break; }
                //клик по меню права+
                 case "MenuRoles": { actionReactMenu = new AccessMenuClick(this); break; }
                 //клик по меню архив+
                 case "archiveMenu": { actionReactMenu = new ArchiveMenuClick(this); break; }   
            }

            actionReactMenu.MenuClick();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            switch (checkBox.Name)
            {
                case "RoleAdminCreateUser": { adminRole = 1; break; }
                case "RoleBookkeeperCreateUser": { bookkeeperRole = 1; break; }
                case "RoleManagerCreateUser": { managerRole = 1; break; }
            }
        
        }
        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            switch (checkBox.Name)
            {
                case "RoleAdminCreateUser": { adminRole = 0; break; }
                case "RoleBookkeeperCreateUser": { bookkeeperRole = 0; break; }
                case "RoleManagerCreateUser": { managerRole = 0; break; }
            }

        }
        //изменение кнопок контроля для DataGrid
        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            GoToChangeUser.IsEnabled = true;
            DelUser.IsEnabled = true;
        }

       
    }
}
