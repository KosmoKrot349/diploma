using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.SelectedCellsChanged
{
    class ControlButtonState:ISelectedCellsChanged
    {
        BookkeeperWindow window;

        public ControlButtonState(BookkeeperWindow window)
        {
            this.window = window;
        }

        public void SelectedCellsChanged()
        {
            //начисления
           window.AccrualOfSalaryForMonth.IsEnabled = true;

            //расходы
            window.DeleteCosts.IsEnabled = true;
            window.GoToChangeCosts.IsEnabled = true;

            //доходы
            window.DeleteProfit.IsEnabled = true;
            window.GoToChangeProfit.IsEnabled = true;
        }
    }
}
