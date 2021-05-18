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
            window.listenerDeleteButton.IsEnabled = true;
            window.listenerChangeButton.IsEnabled = true;

            //курсы
            window.coursDeleteButton.IsEnabled = true;
            window.coursChangeButton.IsEnabled = true;

            //предметы
            window.subDeleteButton.IsEnabled = true;

            //группы
            window.groupDeleteButton.IsEnabled = true;
            window.groupChangeButton.IsEnabled = true;

            //преподаватели
            window.prepDeleteButton.IsEnabled = true;
            window.prepChangeButton.IsEnabled = true;

            //категории
            window.kategDeleteButton.IsEnabled = true;

            //все сотрудники
            window.allSotrDeleteButton.IsEnabled = true;
            window.allSotrToPrepBtton.IsEnabled = true;
            window.allSotrToShtatBtton.IsEnabled = true;

            //расписание звонков
            window.zvonkiDeleteButton.IsEnabled = true;

            //кабинеты
            window.cabDeleteButton.IsEnabled = true;

            //типы дохода 
            window.TypeDohDeleteButton.IsEnabled = true;

            //типы расходов
            window.TypeRashDeleteButton.IsEnabled = true;

            //коефициент за выслугу лет
            window.KoefDeleteButton.IsEnabled = true;

            //работы обслуживания
            window.ObslWorkDeleteButton.IsEnabled = true;

            //должности 
            window.StateChangeButton.IsEnabled = true;
            window.StateDeleteButton.IsEnabled = true;

            //штат
            window.shtatDeleteButton.IsEnabled = true;
            window.shtatChangeButton.IsEnabled = true;
        }
    }
}
