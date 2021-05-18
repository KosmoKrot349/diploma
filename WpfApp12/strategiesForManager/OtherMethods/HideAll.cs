using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.OtherMethods
{
    class HideAll
    {
        public static void Hide(ManagerWindow window) {

            window.listenerDataGrid.SelectedItem = null;
            window.coursDataGrid.SelectedItem = null;
            window.subsDataGrid.SelectedItem = null;
            window.groupsDataGrid.SelectedItem = null;
            window.prepDataGrid.SelectedItem = null;
            window.kategDataGrid.SelectedItem = null;
            window.allSotrDataGrid.SelectedItem = null;
            window.zvonkiDataGrid.SelectedItem = null;
            window.cabDataGrid.SelectedItem = null;
            window.StateDataGrid.SelectedItem = null;
            window.ObslWorkDataGrid.SelectedItem = null;
            window.ShtatDataGrid.SelectedItem = null;
            window.KoefDataGrid.SelectedItem = null;
            window.TypeRashDataGrid.SelectedItem = null;
            window.TypeDohDataGrid.SelectedItem = null;



            //слушатели
            window.listenerDeleteButton.IsEnabled = false;
            window.listenerChangeButton.IsEnabled = false;

            //курсы
            window.coursDeleteButton.IsEnabled = false;
            window.coursChangeButton.IsEnabled = false;

            //предметы
            window.subDeleteButton.IsEnabled = false;

            //группы
            window.groupDeleteButton.IsEnabled = false;
            window.groupChangeButton.IsEnabled = false;

            //преподаватели
            window.prepDeleteButton.IsEnabled = false;
            window.prepChangeButton.IsEnabled = false;

            //категории
            window.kategDeleteButton.IsEnabled = false;

            //все сотрудники
            window.allSotrDeleteButton.IsEnabled = false;
            window.allSotrToPrepBtton.IsEnabled = false;
            window.allSotrToShtatBtton.IsEnabled = false;

            //расписание звонков
            window.zvonkiDeleteButton.IsEnabled = false;

            //кабинеты
            window.cabDeleteButton.IsEnabled = false;

            //типы дохода
            window.TypeDohDeleteButton.IsEnabled = false;

            //типы расходов
            window.TypeRashDeleteButton.IsEnabled = false;

            //коефициент за выслугу лет
            window.KoefDeleteButton.IsEnabled = false;

            //работы обслуживания
            window.ObslWorkDeleteButton.IsEnabled = false;

            //должности 
            window.StateChangeButton.IsEnabled = false;
            window.StateDeleteButton.IsEnabled = false;

            //штат
            window.shtatDeleteButton.IsEnabled = false;
            window.shtatChangeButton.IsEnabled = false;

            //штатное расписание
            window.ShtatRaspSaveBut.IsEnabled = false;

            window.ListenerAddGrid.Visibility = Visibility.Collapsed;
            window.helloGrdi.Visibility = Visibility.Collapsed;
            window.zvonkiGrid.Visibility = Visibility.Collapsed;
            window.prepGrid.Visibility = Visibility.Collapsed;
            window.kategGrid.Visibility = Visibility.Collapsed;
            window.allSotrGrid.Visibility = Visibility.Collapsed;
            window.groupsGrid.Visibility = Visibility.Collapsed;
            window.prepChangeGrid.Visibility = Visibility.Collapsed;
            window.subGrid.Visibility = Visibility.Collapsed;
            window.courseGrid.Visibility = Visibility.Collapsed;
            window.groupAddGrid.Visibility = Visibility.Collapsed;
            window.groupChangeGrid.Visibility = Visibility.Collapsed;
            window.courseAddGrid.Visibility = Visibility.Collapsed;
            window.courseChangeGrid.Visibility = Visibility.Collapsed;
            window.addPrepGrid.Visibility = Visibility.Collapsed;
            window.raspGridG.Visibility = Visibility.Collapsed;
            window.raspGridP.Visibility = Visibility.Collapsed;
            window.raspGridС.Visibility = Visibility.Collapsed;
            window.addRaspGrid.Visibility = Visibility.Collapsed;
            window.changeRaspGrid.Visibility = Visibility.Collapsed;
            window.ListenerGrid.Visibility = Visibility.Collapsed;
            window.ListenerChangeGrid.Visibility = Visibility.Collapsed;
            window.addRaspGridKab.Visibility = Visibility.Collapsed;
            window.changeRaspGridKab.Visibility = Visibility.Collapsed;
            window.addRaspGridPrep.Visibility = Visibility.Collapsed;
            window.changeRaspGridPrep.Visibility = Visibility.Collapsed;
            window.cabGrid.Visibility = Visibility.Collapsed;
            window.skidkiGrid.Visibility = Visibility.Collapsed;
            window.TypeDohGrid.Visibility = Visibility.Collapsed;
            window.TypeRashGrid.Visibility = Visibility.Collapsed;
            window.KoefGrid.Visibility = Visibility.Collapsed;
            window.ObslWorkGrid.Visibility = Visibility.Collapsed;
            window.StateGrid.Visibility = Visibility.Collapsed;
            window.StateAddGrid.Visibility = Visibility.Collapsed;
            window.StateChaneGrid.Visibility = Visibility.Collapsed;
            window.addShtatGrid.Visibility = Visibility.Collapsed;
            window.ShtatGrid.Visibility = Visibility.Collapsed;
            window.ChangeShtatGrid.Visibility = Visibility.Collapsed;
            window.ShtatRaspGrid.Visibility = Visibility.Collapsed;
            window.kassaGrid.Visibility = Visibility.Collapsed;
            window.StatisticaGrid.Visibility = Visibility.Collapsed;
            window.ZpOthcetGrid.Visibility = Visibility.Collapsed;

        }
    }
}
