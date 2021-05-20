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

            window.ListenersDataGrid.SelectedItem = null;
            window.CourcesDataGrid.SelectedItem = null;
            window.SubjectsDataGrid.SelectedItem = null;
            window.GroupsDataGrid.SelectedItem = null;
            window.TeachersDataGrid.SelectedItem = null;
            window.CategoriesDataGrid.SelectedItem = null;
            window.EmployeesDataGrid.SelectedItem = null;
            window.TimeScheduleDataGrid.SelectedItem = null;
            window.CabinetsDataGrid.SelectedItem = null;
            window.PositionsDataGrid.SelectedItem = null;
            window.ServiceWorkDataGrid.SelectedItem = null;
            window.StaffDataGrid.SelectedItem = null;
            window.WorkCoeffDataGrid.SelectedItem = null;
            window.CostsTypeDataGrid.SelectedItem = null;
            window.ProfitTypesDataGrid.SelectedItem = null;



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
            window.TimeScheduleGrid.Visibility = Visibility.Collapsed;
            window.TeachersGrid.Visibility = Visibility.Collapsed;
            window.CategoriesGrid.Visibility = Visibility.Collapsed;
            window.EmployeesGrid.Visibility = Visibility.Collapsed;
            window.GroopsGrid.Visibility = Visibility.Collapsed;
            window.TeacherChangeGrid.Visibility = Visibility.Collapsed;
            window.SubjectsGrid.Visibility = Visibility.Collapsed;
            window.CourcesGrid.Visibility = Visibility.Collapsed;
            window.GroopAddGrid.Visibility = Visibility.Collapsed;
            window.GroupChangeGrid.Visibility = Visibility.Collapsed;
            window.CourseAddGrid.Visibility = Visibility.Collapsed;
            window.CourseChangeGrid.Visibility = Visibility.Collapsed;
            window.AddTeacherGrid.Visibility = Visibility.Collapsed;
            window.GroopScheduleGrid.Visibility = Visibility.Collapsed;
            window.TeacherScheduleGrid.Visibility = Visibility.Collapsed;
            window.CabinetScheduleGrid.Visibility = Visibility.Collapsed;
            window.AddGroopScheduleGrid.Visibility = Visibility.Collapsed;
            window.ChangeGroopSchduleGrid.Visibility = Visibility.Collapsed;
            window.ListenerGrid.Visibility = Visibility.Collapsed;
            window.ListenerChangeGrid.Visibility = Visibility.Collapsed;
            window.AddCabinetScheduleGrdi.Visibility = Visibility.Collapsed;
            window.ChangeCabinetScheduleGrid.Visibility = Visibility.Collapsed;
            window.AddTeacherScheduleGrid.Visibility = Visibility.Collapsed;
            window.ChangeTeacherScheduleGrid.Visibility = Visibility.Collapsed;
            window.CabinetsGrid.Visibility = Visibility.Collapsed;
            window.DiscountGrid.Visibility = Visibility.Collapsed;
            window.ProfiTypesGrid.Visibility = Visibility.Collapsed;
            window.CostsTypeGrid.Visibility = Visibility.Collapsed;
            window.WorkCoeffGrid.Visibility = Visibility.Collapsed;
            window.ServiceWorksGrid.Visibility = Visibility.Collapsed;
            window.PositionGrid.Visibility = Visibility.Collapsed;
            window.PositionAddGrid.Visibility = Visibility.Collapsed;
            window.PositionsChangeGrid.Visibility = Visibility.Collapsed;
            window.AddStaffGrid.Visibility = Visibility.Collapsed;
            window.StaffGrid.Visibility = Visibility.Collapsed;
            window.ChangeShtatGrid.Visibility = Visibility.Collapsed;
            window.StaffScheduleGrid.Visibility = Visibility.Collapsed;
            window.CashboxReportGrid.Visibility = Visibility.Collapsed;
            window.StatisticGrid.Visibility = Visibility.Collapsed;
            window.PaymentListReportGrid.Visibility = Visibility.Collapsed;

        }
    }
}
