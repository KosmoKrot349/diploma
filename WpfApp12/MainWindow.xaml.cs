using System;
using System.Collections;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfApp12.strategiesForMainWind.ButtonClick;
using WpfApp12.strategiesForMainWind.OtherMethods;

namespace WpfApp12
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //строка подключения
        public string connectionString = "";
        IButtonClick actionReact;
        public MainWindow()
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
            FillingStaffSchedule.Fill(this);
            Recalculation.Recalc(this);
        }
        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Name)
            {
                //авторизация пользователя
                case "Authorize": { actionReact = new Authorize(this);  break; }
                //проверка подключения
                case "ConnectionCheck": { actionReact = new ConnectionCheck(this);  break; }
                //принудительное сохранение
                case "EnforcementSeatings": { actionReact = new EnforcementSeatings(this);  break; }    
            }
            actionReact.ButtonClick();
        }
       
        //воод только цифр
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)))
            {
                { e.Handled = true; }
            }
        }


    }

    }

