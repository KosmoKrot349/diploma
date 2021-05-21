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
            if (but.Name == "FilterGroupsButton")
            {
                window.filter.ApplyListenerFilter();
                DataGridUpdater.updateListenerDataGrid(window);
            }


            if (but.Name == "FilterCourseButton")
            {
                window.filter.ApplyGroupsFilter();
                DataGridUpdater.updateGroopsDataGrid(window);
            }

            if (but.Name == "FilterSubjectsButton")
            {
                window.filter.ApplyCourseFilter();
                DataGridUpdater.updateСoursesDataGrid(window);
            }


            if (but.Name == "FilterTeachersButton")
            {
                window.filter.ApplyTeachersFilter();
                DataGridUpdater.updateTeachersDataGrid(window);
            }

            if (but.Name == "FilterEmployeesButton")
            {
                window.sqlForAllEmployees = "SELECT * FROM sotrudniki";
                if (window.DatePickeEmployees.Text == "") {MessageBox.Show("Неоюходимо выбрать месяц"); return; }

                if (window.FirstMethodFilter.IsChecked == true)
                {
                    window.sqlForAllEmployees = "select * from sotrudniki inner join nachisl using(sotrid) where extract(Month from payday) = " + window.DatePickeEmployees.Text.Split('.')[1] + " and extract(Year from payday)=" + window.DatePickeEmployees.Text.Split('.')[2] + " and (prepzp+shtatzp+obslzp)-viplacheno!=0";

                }
                if (window.SecondMethodFilter.IsChecked == true)
                {

                    window.sqlForAllEmployees = "select * from sotrudniki where sotrid not in (select sotrid from nachisl where extract(Month from payday) = " + window.DatePickeEmployees.Text.Split('.')[1] + " and extract(Year from payday)=" + window.DatePickeEmployees.Text.Split('.')[2] + ")";

                }

                DataGridUpdater.updateEmploeesDataGrid(window);
            }

            if (but.Name == "FilterStaffButton")
            {
                if (window.StaffFilterCMBX.SelectedIndex == 0)
                {
                    window.filter.ApplyStaffFirstFilter();
                }
                else
                {

                    window.filter.ApplyStaffSecondFilter();
                }
                DataGridUpdater.updateStaffDataGrid(window);
            }

        }
        public static void ApplyForBookkeeper(BookkeeperWindow window, object sender)
        {
            Button but = sender as Button;
            if (but.Name == "FilterProfitButton")
            {
                window.filter.ApplyCostsFilter();
                DataGridUpdater.updateCostsDataGrid(window);
            }

            if (but.Name == "FilterCostsButton")
            {
                window.filter.ApplyProfitFilter();
                DataGridUpdater.updateProfitDataGrid(window);
            }
            if (but.Name == "FilterCashBoxProfit")
            {
                window.PeopleFromCashboxFilter.ApplyProfitFilterForCashboxReport(window.ProfitTypesFromCashboxFilter);
                DataGridUpdater.updateCashBoxGrid(window.connectionString, window.CashboxProfitGrid, window.CashboxCostsGrid, window.CashboxTitleLabel, window.CashboxTotalProfit, window.CashboxTotalCosts, window.CashboxProfit, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
            if (but.Name == "FilterCashBoxCosts")
            {
                window.StaffFromCashboxFiltr.ApplyCostsFilterForCashboxReport(window.CostsTypesFromCashboxFilter);
                DataGridUpdater.updateCashBoxGrid(window.connectionString, window.CashboxProfitGrid, window.CashboxCostsGrid, window.CashboxTitleLabel, window.CashboxTotalProfit, window.CashboxTotalCosts, window.CashboxProfit, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
        }
    }
}
