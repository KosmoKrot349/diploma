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
        public string FIO = "";
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


        public filtr filter = new filtr();
        public filtr PeopleFromCashboxFilter = new filtr();
        public filtr ProfitTypesFromCashboxFilter = new filtr();

        public filtr StaffFromCashboxFiltr = new filtr();
        public filtr CostsTypesFromCashboxFilter = new filtr();
        public string personForProfit = "";


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
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;

        }
        //+
        public void HideAll()
        {

            strategiesForBookkeeper.OtherMethods.HideAll.Hide(this);
        }
        //+
      
        //переход из меню бухаглтера в меню админа +
        private void AdminRoleB_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ToAdmin(this);
            actionReact.ButtonClick();
        }
        //переход из меню бухаглтера в меню бухглатера +
        private void BuhgRoleB_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ToBuhg();
            actionReact.ButtonClick();
        }
        //переход из меню бухаглтера в меню директора + 
        private void DirectorRoleB_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ToManager(this);
            actionReact.ButtonClick();
        }
        //выход из пользователя+
        private void Leave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new UnLogin(this);
            actionReact.ButtonClick();
        }
        
        //переход к оплате слушателей+
        private void OplataSlysh_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new PaymentMenu(this);
            actionReact.MenuClick();
        }
        //изменение группы выбор слушталей +
        private void Groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "dohAddKtoVnesCmF")
            {
                ISelectionChanged actionReact = new SelectingPersonTypeProfitAdd(this, cmb);
                actionReact.SelectionChanged();
            }

            if (cmb.Name == "dohChKtoVnesCmF")
            {
                ISelectionChanged actionReact = new SelectingPersonTypeProfitChange(this,cmb);
                actionReact.SelectionChanged();
            }

            if (cmb.Name== "Groups")
            {
                ISelectionChanged actionReact = new SelectingGroop(this);
                actionReact.SelectionChanged();
            }

            if (cmb.Name == "GroupsDolg")
            {
                ISelectionChanged actionReact = new SelectingGroopArrears(this);
                actionReact.SelectionChanged();
            }

        }
        //выбор слушателя +
        private void Listener_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "Listener")
                updateDefraymentTable.Update(this, 1);
            if (cmb.Name == "ListenerDolg")
                updateDefraymentTable.Update(this, 2);

        }
        //закрытие запими об оплате +
        private void Closeing_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ClosePaymentEntry(this);
            actionReact.ButtonClick();
        }
        //открытие записи +
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new UnClosePaymentEntry(this);
            actionReact.ButtonClick();
        }
        //приняте оплаты +
        private void AddPAy_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddPayment(this);
            actionReact.ButtonClick();
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
      //остановка оучения+
        private void StopLern_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new LearnStop(this);
            actionReact.ButtonClick();
        }
        //восстановление обучения+
        private void RestartLern_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new LearnRestart(this);
            actionReact.ButtonClick();
        }
        //переход к таблице доходов+
        private void DodhodyTable_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ProfitMenu(this);
            actionReact.MenuClick();
        }
        // переход к добавление дохода+
        private void DohAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddProfit(this);
            actionReact.ButtonClick();
        }
        //добавление дохода в базу+
        private void DohodyAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddProfit(this);
            actionReact.ButtonClick();
        }
        //удаление дохода+ 
        private void DohDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteProfit(this);
            actionReact.ButtonClick();
        }
        //переход к имзенению дохода+
        private void DohChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeProfit(this);
            actionReact.ButtonClick();

        }
        //изменение дохода+
        private void DohodyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeProfit(this);
            actionReact.ButtonClick();
        }
        //переход к расходам+
        private void RashodyTable_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CostsMenu(this);
            actionReact.MenuClick();
        }
        //переход к доабвлению расходов+
        private void RashAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToAddCosts(this);
            actionReact.ButtonClick();
        }
        //добавление расхода в базу+
        private void RashodyAddButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddCosts(this);
            actionReact.ButtonClick();
        }
        //удаление расхода+
        private void RashDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteCosts(this);
            actionReact.ButtonClick();
        }
        //переход к изменению расхода+
        private void RashChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToChangeCosts(this);
            actionReact.ButtonClick();
        }
        //изменение расхода+
        private void RashodyChangeButton_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ChangeCosts(this);
            actionReact.ButtonClick();
        }
        //переход к налогам+
        private void Nalogi_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new TaxMenu(this);
            actionReact.MenuClick();
        }
        //сохранение налогов+
        private void NalogiSave_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new SaveTax(this);
            actionReact.ButtonClick();
        }
        //переход к начислениям+
        private void ZP_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new AccrualsMenu(this);
            actionReact.MenuClick();
        }
        //выбрать всех в таблице начилсений +
        private void NuchSelectAllSotrBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AccrualsSelectAll(this);
            actionReact.ButtonClick();
        }
        //переход к предыдущему месяцу в начислениях +
        private void NachMonthPrev_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToPreviouslyMonth(this);
            actionReact.ButtonClick();
        }
        //переход к следующему месяцу в начислениях +
        private void NachMonthNext_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToNextMonth(this);
            actionReact.ButtonClick();
        }
        //начисление зп за месяц +
        private void NachAddBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AccrualOfSalaryForMonth(this);
            actionReact.ButtonClick();
        }
        //выплата зп +
        private void ViplataBut_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new SalaryPay(this);
            actionReact.ButtonClick();
        }
        //применение фильтров + 
        private void FiltrButton_Click(object sender, RoutedEventArgs e)
        {
            ApplyFiltersButtonClick.ApplyForBookkeeper(this, sender);
        }
        //переход к гриду долга+
        private void OplataDolgMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ArrearsMenu(this);
            actionReact.MenuClick();
        }
        //оплата долга слушателем +
        private void AddPAyDolg_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new AddPaymentForArrears(this);
            actionReact.ButtonClick();
        }
        //удаление долга+
        private void deleteDolg_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new DeleteArrears(this);
            actionReact.ButtonClick();
        }
        //переход к гриду отчета кассы+
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CashboxReportMenu(this);
            actionReact.MenuClick();
        }
        //переход к гриду отчета статистика+
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new StatisticReportMenu(this);
            actionReact.MenuClick();

        }
        //переход к гриду отчета списки выплат+
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ListOfPaymentReportMenu(this);
            actionReact.MenuClick();
        }
        //кликл по меню прав+
        private void MenuRolesB_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new AccessMenu(this);
            actionReact.MenuClick();
        }
        //кликл по меню доходов+
        private void Dohody_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new SelectProfitMenu(this);
            actionReact.MenuClick();
        }
        //кликл по меню расходов+
        private void Rashody_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new SelectCostsMenu(this);
            actionReact.MenuClick();
        }
        //кликл по меню отчётов+
        private void otchetMenu_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new ReportMenu(this);
            actionReact.MenuClick();
        }
        //разблокировка всех кнопок +
        private void NachDataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            ISelectedCellsChanged actionReact = new ControlButtonState(this);
            actionReact.SelectedCellsChanged();
        }
   

        private void dohAddKtoVnesCm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "dohAddKtoVnesCm")
            {
                ISelectionChanged actionReact = new SelectingPersonAddProfit(this);
                actionReact.SelectionChanged();
            }

            if (cmb.Name == "dohChKtoVnesCm")
            {
                ISelectionChanged actionReact = new SelectingPersonChangeProfit(this);
                actionReact.SelectionChanged();
            }
        }
    }
}


