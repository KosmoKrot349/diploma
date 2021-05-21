using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.OtherMethods
{
    class HideAll
    {
        public static void Hide(BookkeeperWindow window)
        {
            window.AccrualsDataGrid.SelectedItem = null;
            window.CostsDataGrid.SelectedItem = null;
            window.ProfitDataGrid.SelectedItem = null;

            window.helloGrdi.Visibility = Visibility.Collapsed;
            window.PaymentGrid.Visibility = Visibility.Collapsed;
            window.ProfitGrid.Visibility = Visibility.Collapsed;
            window.ProfitAddGrid.Visibility = Visibility.Collapsed;
            window.ProfitChangeGrid.Visibility = Visibility.Collapsed;
            window.CostsGrid.Visibility = Visibility.Collapsed;
            window.CostsAddGrid.Visibility = Visibility.Collapsed;
            window.CostsChangeGrid.Visibility = Visibility.Collapsed;
            window.TaxesGrid.Visibility = Visibility.Collapsed;
            window.AccrualsGrid.Visibility = Visibility.Collapsed;
            window.DebtPaymentGrid.Visibility = Visibility.Collapsed;
            window.LearningAccrualsPaymentGrdi.Visibility = Visibility.Collapsed;
            window.CashboxGrid.Visibility = Visibility.Collapsed;
            window.StatisticGrid.Visibility = Visibility.Collapsed;
            window.PaymentListGrid.Visibility = Visibility.Collapsed;

            //начисления
            window.AccrualOfSalaryForMonth.IsEnabled = false;

            //расходы
            window.DeleteCosts.IsEnabled = false;
            window.GoToChangeCosts.IsEnabled = false;

            //доходы
            window.DeleteProfit.IsEnabled = false;
            window.GoToChangeProfit.IsEnabled = false;

        }
    }
}
