using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.IO;
using Npgsql;
using System.Collections;

using WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick;
using WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterMenuClick;
using WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterSelectionChanged;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для BuhgalterWindow.xaml
    /// </summary>
    public partial class BuhgalterWindow : Window
    {
        public int logUser;
        public string FIO = "";
        public TextBox[] masTbx;
        public TextBox[] masTbx2;
        //строка подключения
        public string connectionString = "";

      public  int DohodID = -1;
       public int RashodID = 1;

        //начисления
        public DateTime dateNuch;
        public CheckBox[] ChbxMas_SotrNuch;
        public bool selected = false;


        public filtr filtr = new filtr();
        public filtr fda = new filtr();
        public filtr fdb = new filtr();

        public filtr fra = new filtr();
        public filtr frb = new filtr();
        public string strrr = "";


        //+
        public BuhgalterWindow()
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
            MenuRolesB.BorderBrush = null;
            Dohody.BorderBrush = null;
            Rashody.BorderBrush = null;
            Nalogi.BorderBrush = null;
            otchetMenu.BorderBrush = null;

        }
        //+
        public void HideAll()
        {

            NachDataGrid.SelectedItem = null;
            RoshodyDataGrid.SelectedItem = null;
            DohodyDataGrid.SelectedItem = null;

            helloGrdi.Visibility = Visibility.Collapsed;
            OplataGrid.Visibility = Visibility.Collapsed;
            DohodyrGrid.Visibility = Visibility.Collapsed;
            DohodyrAddGrid.Visibility = Visibility.Collapsed;
            DohodyChangeGrid.Visibility = Visibility.Collapsed;
            RoshodyGrid.Visibility = Visibility.Collapsed;
            RashodyAddGrid.Visibility = Visibility.Collapsed;
            RashodyChangeGrid.Visibility = Visibility.Collapsed;
            NalogiGrid.Visibility = Visibility.Collapsed;
            GlNachGrid.Visibility = Visibility.Collapsed;
            DolgGrid.Visibility = Visibility.Collapsed;
            NoDolgGrdi.Visibility = Visibility.Collapsed;
            kassaGrid.Visibility = Visibility.Collapsed;
            StatisticaGrid.Visibility = Visibility.Collapsed;
            ZpOthcetGrid.Visibility = Visibility.Collapsed;

            //начисления
            ViplataBut.IsEnabled = false;

            //расходы
            RashDeleteButton.IsEnabled = false;
            RashChangeButton.IsEnabled = false;

            //доходы
            DohDeleteButton.IsEnabled = false;
            DohChangeButton.IsEnabled = false;
        }
        //+
        public void updateOplataTable(int a)
        {
            if (a == 1)
            {
                int kol_Month = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listnuch where listenerid = (select listenerid from listeners where fio='" + Listener.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + Groups.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payformonth = reader.GetString(0);
                            string[] payformonthMas = payformonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payformonthMas[i] != "0")
                                {

                                    kol_Month++;

                                }
                            }
                            masTbx = new TextBox[kol_Month];
                            for (int i = 0; i < kol_Month; i++)
                            {
                                masTbx[i] = new TextBox();
                                masTbx[i].PreviewTextInput += TextBox_PreviewTextInput;
                            }
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridOpat(connectionString, MonthOplGrid, Groups, Listener, masTbx, isClose, isStop, Closeing, Open, StopLern, RestartLern);
            }


            if (a == 2)
            {
                
                int kol_Month = 0;
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(connectionString);
                    con.Open();
                    string sql = "SELECT  array_to_string(payformonth,'_')  FROM listdolg where listenerid = (select listenerid from listeners where fio='" + ListenerDolg.SelectedItem + "') and grid = (select grid from groups where nazvanie ='" + GroupsDolg.SelectedItem + "')";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string payformonth = reader.GetString(0);
                            string[] payformonthMas = payformonth.Split('_');

                            for (int i = 0; i < 12; i++)
                            {
                                if (payformonthMas[i] != "0")
                                {

                                    kol_Month++;

                                }
                            }
                            masTbx2 = new TextBox[kol_Month];
                            for (int i = 0; i < kol_Month; i++)
                            {
                                masTbx2[i] = new TextBox();
                                masTbx2[i].PreviewTextInput += TextBox_PreviewTextInput;
                            }
                        }
                    }
                con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

                DataGridUpdater.updateDataGridDolg(connectionString, MonthOplGridDolg, GroupsDolg, ListenerDolg, masTbx2, DataPerehoda, isStopDolg);
            }
        }
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
            IButtonClick actionReact = new GoToPayment(this);
            actionReact.ButtonClick();
        }
        //изменение группы выбор слушталей +
        private void Groups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "dohAddKtoVnesCmF")
            {
                ISelectionChanged actionReact = new SelectingPersonProfitAdd(this, cmb);
                actionReact.SelectionChanged();
            }

            if (cmb.Name == "dohChKtoVnesCmF")
            {
                ISelectionChanged actionReact = new SelectingPersonProfitChange(this,cmb);
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
            if(cmb.Name== "Listener")
            updateOplataTable(1);
            if (cmb.Name == "ListenerDolg")
            updateOplataTable(2);

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
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            IButtonClick actionReact = new GoToProfitTable(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToCosts(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToTax(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToAccruals(this);
            actionReact.ButtonClick();
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
            Button but = sender as Button;
            if (but.Name == "FiltrRashodyButton")
            {
                IButtonClick actionReact = new AppCostsFiltr(this);
                actionReact.ButtonClick();
            }

            if (but.Name == "FiltrDohodyButton")
            {
                IButtonClick actionReact = new AppProfitFiltr(this);
                actionReact.ButtonClick();
            }
            if (but.Name == "PrimFKD")
            {
                IButtonClick actionReact = new AppCashierProfitFiltr(this);
                actionReact.ButtonClick();
            }
            if (but.Name == "PrimFKR")
            {
                IButtonClick actionReact = new AppCashierCostsFiltr(this);
                actionReact.ButtonClick();
            }
        }
        //переход к гриду долга+
        private void OplataDolgMenu_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToArrears(this);
            actionReact.ButtonClick();
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
            IButtonClick actionReact = new GoToCashierReport(this);
            actionReact.ButtonClick();
        }
        //переход к гриду отчета статистика+
        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToStatsReport(this);
            actionReact.ButtonClick();

        }
        //переход к гриду отчета списки выплат+
        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new GoToListsOfPaymentReport(this);
            actionReact.ButtonClick();
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
            IMenuClick actionReact = new ProfitMenu(this);
            actionReact.MenuClick();
        }
        //кликл по меню расходов+
        private void Rashody_Click(object sender, RoutedEventArgs e)
        {
            IMenuClick actionReact = new CostsMenu(this);
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
            //начисления
            ViplataBut.IsEnabled = true;

           //расходы
            RashDeleteButton.IsEnabled = true;
            RashChangeButton.IsEnabled = true;

            //доходы
            DohDeleteButton.IsEnabled = true;
            DohChangeButton.IsEnabled = true;


        }
   

        private void dohAddKtoVnesCm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            if (cmb.Name == "dohAddKtoVnesCm")
            {
                if (cmb.Items.Count == 0) { return; }
                if (cmb.SelectedItem.ToString() == "Нет в списке") { dohAddKtoVnesTb.Text = ""; dohAddKtoVnesTb.IsEnabled = true; }
                else { dohAddKtoVnesTb.Text = cmb.SelectedItem.ToString(); dohAddKtoVnesTb.IsEnabled = false; }

            }

            if (cmb.Name == "dohChKtoVnesCm")
            {

                if (cmb.Items.Count == 0) { return; }
                if (cmb.SelectedItem.ToString() == "Нет в списке") { dohChKtoVnesTb.Text = ""; dohChKtoVnesTb.IsEnabled = true; }
                else
                { dohChKtoVnesTb.Text = cmb.SelectedItem.ToString(); dohChKtoVnesTb.IsEnabled = false; }

            }
        }
    }
}


