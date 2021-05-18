using Npgsql;
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
        //авторизация пользователя
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new Authorize(this);
            actionReact.ButtonClick();
        }
        //проверка подключения
        private void Button_Click11(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new ConnectionCheck(this);
            actionReact.ButtonClick();
        }
  
        //принудительное сохранение
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IButtonClick actionReact = new EnforcementSeatings(this);
            actionReact.ButtonClick();
        }
        //воод только цифр
        private void ip1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tbne = sender as TextBox;
            if ((!Char.IsDigit(e.Text, 0)))
            {
                { e.Handled = true; }
            }
        }


    }

    }

