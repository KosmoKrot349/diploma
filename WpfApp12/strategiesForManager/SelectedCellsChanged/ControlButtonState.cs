using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.SelectedChanged
{
    class ControlButtonState:ISelectedChanged
    {
        ManagerWindow window;

        public ControlButtonState(ManagerWindow window)
        {
            this.window = window;
        }

        public void SelectedChanged()
        {
            //слушатели
            window.DeleteListener.IsEnabled = true;
            window.GoToChangeListener.IsEnabled = true;

            //курсы
            window.DeleteCourse.IsEnabled = true;
            window.GoToChangeCourse.IsEnabled = true;

            //предметы
            window.DeleteSubject.IsEnabled = true;

            //группы
            window.DeleteGroop.IsEnabled = true;
            window.GoToChangeGroop.IsEnabled = true;

            //преподаватели
            window.TeacherDelete.IsEnabled = true;
            window.GoToChangeTeacher.IsEnabled = true;

            //категории
            window.DeleteCategory.IsEnabled = true;

            //все сотрудники
            window.EmployeeDelet.IsEnabled = true;
            window.GoToEmployeeToTeacher.IsEnabled = true;
            window.GoToEmployeeToStuff.IsEnabled = true;

            //расписание звонков
            window.TimeScheduleDelete.IsEnabled = true;

            //кабинеты
            window.DeleteCabinet.IsEnabled = true;

            //типы дохода 
            window.DeleteProfit.IsEnabled = true;

            //типы расходов
            window.DeleteCost.IsEnabled = true;

            //коефициент за выслугу лет
            window.DeleteWorkCoeff.IsEnabled = true;

            //работы обслуживания
            window.DeleteServiceWork.IsEnabled = true;

            //должности 
            window.GoToAddPosition.IsEnabled = true;
            window.DeletePosition.IsEnabled = true;

            //штат
            window.DeleteFromStaff.IsEnabled = true;
            window.GoToChangeStaffEmployee.IsEnabled = true;
        }
    }
}
