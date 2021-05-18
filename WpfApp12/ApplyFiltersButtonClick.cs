using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12
{
    class ApplyFiltersButtonClick
    {
        public static void ApplyForManager(ManagerWindow window, object sender)
        {
            Button but = sender as Button;
            if (but.Name == "FiltrGroupsButton")
            {
                window.filtr.ApplyListFiltr();
                DataGridUpdater.updateDataGridListener(window.connectionString, window.filtr.sql, window.listenerDataGrid);
            }


            if (but.Name == "FiltrCourseButton")
            {
                window.filtr.ApplyGroupsFiltr();
                DataGridUpdater.updateDataGridGroups(window.connectionString, window.filtr.sql, window.groupsDataGrid);
            }

            if (but.Name == "FiltrSubsButton")
            {
                window.filtr.ApplyCourseFiltr();
                DataGridUpdater.updateDataGridСourses(window.connectionString, window.filtr.sql, window.coursDataGrid);
            }


            if (but.Name == "FiltrPrepButton")
            {
                window.filtr.ApplyPrepFiltr();
                DataGridUpdater.updateDataGridPrep(window.connectionString, window.filtr.sql, window.prepDataGrid);
            }

            if (but.Name == "FiltrAllSotrButton")
            {
                window.sqlForAllEmployees = "SELECT * FROM sotrudniki";
                if (window.DatePickAllSotr.Text == "") {MessageBox.Show("Неоюходимо выбрать месяц"); return; }

                if (window.FirstMethodFiltr.IsChecked == true)
                {
                    window.sqlForAllEmployees = "select * from sotrudniki inner join nachisl using(sotrid) where extract(Month from payday) = " + window.DatePickAllSotr.Text.Split('.')[1] + " and extract(Year from payday)=" + window.DatePickAllSotr.Text.Split('.')[2] + " and (prepzp+shtatzp+obslzp)-viplacheno!=0";

                }
                if (window.SecondMethodFiltr.IsChecked == true)
                {

                    window.sqlForAllEmployees = "select * from sotrudniki where sotrid not in (select sotrid from nachisl where extract(Month from payday) = " + window.DatePickAllSotr.Text.Split('.')[1] + " and extract(Year from payday)=" + window.DatePickAllSotr.Text.Split('.')[2] + ")";

                }

                DataGridUpdater.updateDataGridSotr(window.connectionString, window.sqlForAllEmployees, window.allSotrDataGrid);
            }

            if (but.Name == "FiltrShtatButton")
            {
                if (window.ShtatFiltrCmbx.SelectedIndex == 0)
                {
                    window.filtr.ApplyShtatFiltrFirst();
                }
                else
                {

                    window.filtr.ApplyShtatFiltrSecond();
                }
                DataGridUpdater.updateDataGridShtat(window.connectionString, window.filtr.sql, window.ShtatDataGrid);
            }

        }
        public static void ApplyForBookkeeper(BookkeeperWindow window, object sender)
        {
            Button but = sender as Button;
            if (but.Name == "FiltrRashodyButton")
            {
                window.filter.ApplyRashodyFiltr();
                DataGridUpdater.updateDataGridRashody(window.connectionString, window.filter.sql, window.RoshodyDataGrid);
            }

            if (but.Name == "FiltrDohodyButton")
            {
                window.filter.ApplyDohodyFiltr();
                DataGridUpdater.updateDataGridDohody(window.connectionString, window.filter.sql, window.RoshodyDataGrid);
            }
            if (but.Name == "PrimFKD")
            {
                window.PeopleFromCashboxFilter.ApplyDohFiltr(window.ProfitTypesFromCashboxFilter);
                DataGridUpdater.updateGridKassa(window.connectionString, window.KassaDodohGrid, window.KassaRashodGrid, window.kassaTitleLabel, window.KassaItogoDohod, window.KassaItogoRashod, window.kassaAllDohodLabel, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
            if (but.Name == "PrimFKR")
            {
                window.StaffFromCashboxFiltr.ApplyRashFiltr(window.CostsTypesFromCashboxFilter);
                DataGridUpdater.updateGridKassa(window.connectionString, window.KassaDodohGrid, window.KassaRashodGrid, window.kassaTitleLabel, window.KassaItogoDohod, window.KassaItogoRashod, window.kassaAllDohodLabel, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
        }
    }
}
