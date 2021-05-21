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
            window.DeleteListener.IsEnabled = false;
            window.GoToChangeListener.IsEnabled = false;

            //курсы
            window.DeleteCourse.IsEnabled = false;
            window.GoToChangeCourse.IsEnabled = false;

            //предметы
            window.DeleteSubject.IsEnabled = false;

            //группы
            window.DeleteGroop.IsEnabled = false;
            window.GoToChangeGroop.IsEnabled = false;

            //преподаватели
            window.TeacherDelete.IsEnabled = false;
            window.GoToChangeTeacher.IsEnabled = false;

            //категории
            window.DeleteCategory.IsEnabled = false;

            //все сотрудники
            window.EmployeeDelet.IsEnabled = false;
            window.GoToEmployeeToTeacher.IsEnabled = false;
            window.GoToEmployeeToStuff.IsEnabled = false;

            //расписание звонков
            window.TimeScheduleDelete.IsEnabled = false;

            //кабинеты
            window.DeleteCabinet.IsEnabled = false;

            //типы дохода
            window.DeleteCost.IsEnabled = false;

            //типы расходов
            window.DeleteProfit.IsEnabled = false;

            //коефициент за выслугу лет
            window.DeleteWorkCoeff.IsEnabled = false;

            //работы обслуживания
            window.DeleteServiceWork.IsEnabled = false;

            //должности 
            window.GoToChangePosition.IsEnabled = false;
            window.DeletePosition.IsEnabled = false;

            //штат
            window.DeleteFromStaff.IsEnabled = false;
            window.GoToChangeStaffEmployee.IsEnabled = false;

            //штатное расписание
            window.SaveStaffSchedule.IsEnabled = false;

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
