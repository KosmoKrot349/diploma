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
                window.filter.ApplyListenerFilter();
                DataGridUpdater.updateListenerDataGrid(window);
            }


            if (but.Name == "FiltrCourseButton")
            {
                window.filter.ApplyGroupsFilter();
                DataGridUpdater.updateGroopsDataGrid(window);
            }

            if (but.Name == "FiltrSubsButton")
            {
                window.filter.ApplyCourseFilter();
                DataGridUpdater.updateСoursesDataGrid(window);
            }


            if (but.Name == "FiltrPrepButton")
            {
                window.filter.ApplyTeachersFilter();
                DataGridUpdater.updateTeachersDataGrid(window);
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

                DataGridUpdater.updateEmploeesDataGrid(window);
            }

            if (but.Name == "FiltrShtatButton")
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
            if (but.Name == "FiltrRashodyButton")
            {
                window.filter.ApplyCostsFilter();
                DataGridUpdater.updateCostsDataGrid(window);
            }

            if (but.Name == "FiltrDohodyButton")
            {
                window.filter.ApplyProfitFilter();
                DataGridUpdater.updateProfitDataGrid(window);
            }
            if (but.Name == "PrimFKD")
            {
                window.PeopleFromCashboxFilter.ApplyProfitFilterForCashboxReport(window.ProfitTypesFromCashboxFilter);
                DataGridUpdater.updateCashBoxGrid(window.connectionString, window.CashboxProfitGrid, window.CashboxCostsGrid, window.CashboxTitleLabel, window.CashboxTotalProfit, window.CashboxTotalCosts, window.CashboxProfit, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
            if (but.Name == "PrimFKR")
            {
                window.StaffFromCashboxFiltr.ApplyCostsFilterForCashboxReport(window.CostsTypesFromCashboxFilter);
                DataGridUpdater.updateCashBoxGrid(window.connectionString, window.CashboxProfitGrid, window.CashboxCostsGrid, window.CashboxTitleLabel, window.CashboxTotalProfit, window.CashboxTotalCosts, window.CashboxProfit, window.PeopleFromCashboxFilter.sql, window.StaffFromCashboxFiltr.sql);
            }
        }
    }
}
