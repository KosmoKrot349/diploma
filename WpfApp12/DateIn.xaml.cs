using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Npgsql;
using WpfApp12.strategiesForDataIn.ButtonClick;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для DateIn.xaml
    /// </summary>
    public partial class DateIn : Window
    {
       public DateTime dateMonday = new DateTime();

        public int AccrualRecordId = -1;
        public double toPay=0;

        public string connectionString;

        IButtonClick actionReact;
        public DateIn()
        {
            InitializeComponent();
        }
        public DateTime getDm()
        {
            return dateMonday;
        }

        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {   //дата для расписания
                case "InputDateForSchedule": { actionReact = new InputDateForSchedule(this);break; }
                //дата для остановки расчёта 
                case "InputDateForLearningStop": { actionReact = new InputDateForLearningStop(this); break; }
                //размер пени 
                case "InputFine": { actionReact = new InputFine(this); break; }
                //выплата зп
                case "InputSalary": { actionReact = new InputSalary(this); break; }
            }
            actionReact.ButtonClick();

        }

        //ввод только цифр
        private void PenyaProc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)) && (e.Text != ","))
            {
                e.Handled = true;
            }
            else
                if ((e.Text == ",") && ((tbne.Text.IndexOf(",") != -1) || (tbne.Text == "")))
            {  e.Handled = true; }
        }
       
    }
}
