using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using System.Collections;
using WpfApp12.strategiesForBookkeeper.ButtonClick;
using WpfApp12.strategiesForBookkeeper.MenuClick;
using WpfApp12.strategiesForBookkeeper.SelectionChanged;
using WpfApp12.strategiesForBookkeeper.OtherMethods;
using WpfApp12.strategiesForBookkeeper.SelectedCellsChanged;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для BuhgalterWindow.xaml
    /// </summary>
    public partial class BookkeeperWindow : Window
    {
        public int logUser;
        public string UserName = "";
        public TextBox[] textBoxArrForDefreyment;
        public TextBox[] textBoxArrForArrearsDefreyment;
        //строка подключения
        public string connectionString = "";

      public  int profitID = -1;
       public int costID = 1;

        //начисления
        public DateTime dateAccrual;
        public CheckBox[] checkBoxArrStaffForAccrual;
        public bool selected = false;


        public filter filter = new filter();
        public filter PeopleFromCashboxFilter = new filter();
        public filter ProfitTypesFromCashboxFilter = new filter();

        public filter StaffFromCashboxFiltr = new filter();
        public filter CostsTypesFromCashboxFilter = new filter();
        public string personForProfit = "";
        IButtonClick actionReactButton;
        IMenuClick actionReactMenu;
        

        //+
        public BookkeeperWindow()
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
            RolesMenu.BorderBrush = null;
            ProfitMenu.BorderBrush = null;
            CostsMenu.BorderBrush = null;
            TaxesMenu.BorderBrush = null;
            ReportsMenu.BorderBrush = null;

        }
        //+
        public void HideAll()
        {

            strategiesForBookkeeper.OtherMethods.HideAll.Hide(this);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                //закрытие запими об оплате +
                case "ClosePaymentEntry": { actionReactButton = new ClosePaymentEntry(this); break; }
                //открытие записи +
                case "UnClosePaymentEntry": { actionReactButton = new UnClosePaymentEntry(this); break; }
                //приняте оплаты +
                case "AddPayment": { actionReactButton = new AddPayment(this); break; }
                //остановка оучения+
                case "LearnStop": { actionReactButton = new LearnStop(this); break; }
                //восстановление обучения+
                case "LearnRestart": { actionReactButton = new LearnRestart(this); break; }
                // переход к добавление дохода+
                case "GoToAddProfit": { actionReactButton = new GoToAddProfit(this); break; }
                //добавление дохода в базу+
                case "AddProfit": { actionReactButton = new AddProfit(this); break; }
                //удаление дохода+ 
                case "DeleteProfit": { actionReactButton = new DeleteProfit(this); break; }
                //переход к имзенению дохода+
                case "GoToChangeProfit": { actionReactButton = new GoToChangeProfit(this); break; }
                //изменение дохода+
                case "ChangeProfit": { actionReactButton = new ChangeProfit(this); break; }
                //переход к доабвлению расходов+
                case "GoToAddCosts": { actionReactButton = new GoToAddCosts(this); break; }
                //добавление расхода в базу+
                case "AddCosts": { actionReactButton = new AddCosts(this); break; }
                //удаление расхода+
                case "DeleteCosts": { actionReactButton = new DeleteCosts(this); break; }
                //переход к изменению расхода+
                case "GoToChangeCosts": { actionReactButton = new GoToChangeCosts(this); break; }
                //изменение расхода+
                case "ChangeCosts": { actionReactButton = new ChangeCosts(this); break; }
                //сохранение налогов+
                case "SaveTax": { actionReactButton = new SaveTax(this); break; }
                //выбрать всех в таблице начилсений +
                case "AccrualsSelectAll": { actionReactButton = new AccrualsSelectAll(this); break; }
                //переход к предыдущему месяцу в начислениях +
                case "GoToPreviouslyMonth": { actionReactButton = new GoToPreviouslyMonth(this); break; }
                //переход к следующему месяцу в начислениях +
                case "GoToNextMonth": { actionReactButton = new GoToNextMonth(this); break; }
                //начисление зп за месяц +
                case "AccrualOfSalaryForMonth": { actionReactButton = new AccrualOfSalaryForMonth(this); break; }
                //выплата зп +
                case "SalaryPay": { actionReactButton = new SalaryPay(this); break; }
                //оплата долга слушателем +
                case "AddPaymentForArrears": { actionReactButton = new AddPaymentForArrears(this); break; }
                //удаление долга+
                case "DeleteArrears": { actionReactButton = new DeleteArrears(this); break; }
                    
            }
            actionReactButton.ButtonClick();
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem item = sender as MenuItem;
            switch (item.Name)
            {
                //переход из меню бухаглтера в меню админа +
                case "GoToAdmin": { actionReactMenu = new ToAdmin(this); break; }
                //переход из меню бухаглтера в меню бухглатера +'
                case "GoToBookkeeper": { actionReactMenu = new ToBookkeeper(); break; }
                //переход из меню бухаглтера в меню директора +
                case "GoToManager": { actionReactMenu = new ToManager(this); break; }
                //выход из пользователя+
                case "Leave": { actionReactMenu = new UnLogin(this); break; }
                //переход к оплате слушателей+
                case "PaymentForListenerMenu": { actionReactMenu = new PaymentMenu(this); break; }
                //переход к таблице доходов+
                case "ProfitTableMenu": { actionReactMenu = new ProfitMenu(this); break; }
                //переход к расходам+
                case "CostsTableMenu": { actionReactMenu = new CostsMenu(this); break; }
                //переход к налогам+
                case "TaxesMenu":{ actionReactMenu = new TaxMenu(this); break; }
                //переход к начислениям+
                case "AccrualSalaryMenu":{ actionReactMenu = new AccrualsMenu(this); break; }
                //переход к гриду долга+
                case "PaymentDebtMenu":{ actionReactMenu = new ArrearsMenu(this); break; }
                //переход к гриду отчета кассы+
                case "CashboxReportMenu":{ actionReactMenu = new CashboxReportMenu(this); break; }
                //переход к гриду отчета статистика+
                case "StatisticReportMenu":{ actionReactMenu = new StatisticReportMenu(this); break; }
                //переход к гриду отчета списки выплат+
                case "PaymentListReportMenu":{ actionReactMenu = new ListOfPaymentReportMenu(this); break; }
                //кликл по меню прав+
                case "RolesMenu":{ actionReactMenu = new AccessMenu(this); break; }
                //кликл по меню доходов+
                case "ProfitMenu":{ actionReactMenu = new SelectProfitMenu(this); break; }
                //кликл по меню расходов+
                case "CostsMenu":{ actionReactMenu = new SelectCostsMenu(this); break; }
                //кликл по меню отчётов+
                case "ReportsMenu":{ actionReactMenu = new ReportMenu(this); break; }
        


            }
            actionReactMenu.MenuClick();
        }
        private void Combobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ISelectionChanged actionReactComboBox=null;
            ComboBox comboBox = sender as ComboBox;
            switch (comboBox.Name)
            { //выбор слушателя +
                case "Listener": { updateDefraymentTable.Update(this, 1); break; }
                //выбор слушателя долг +
                case "ListenerDebt": { updateDefraymentTable.Update(this, 2); break; }
                //выбор группы человека при добавлении доходв
                case "ProfitAddPErsonType": { actionReactComboBox= new SelectingPersonTypeProfitAdd(this, comboBox); break; }
                //выбор группы человека при изменении доходв
                case "ProfitChangePersonType": { actionReactComboBox= new SelectingPersonTypeProfitChange(this, comboBox); break; }
                //изменение группы выбор слушталей +
                case "Groups": { actionReactComboBox= new SelectingGroop(this); break; }
                //изменение группы выбор слушталей в долгах +
                case "GroupsDolg": { actionReactComboBox= new SelectingGroopArrears(this); break; }
                //выбор человека при добавленнии дохода
                case "ProfitAddPerson": { actionReactComboBox= new SelectingPersonAddProfit(this); break; }
                //выбор человека при изменении дохода
                case "ProfitChangePerson": { actionReactComboBox= new SelectingPersonChangeProfit(this); break; }
                   
            }
           if(actionReactComboBox!=null) actionReactComboBox.SelectionChanged();
            

        }

        //вод только цифер +
        public void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
  
        //применение фильтров + 
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltersButtonClick.ApplyForBookkeeper(this, sender);
        }
        //разблокировка всех кнопок +
        private void DataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ISelectedCellsChanged actionReact = new ControlButtonState(this);
            actionReact.SelectedCellsChanged();
        }
        
    }
}


