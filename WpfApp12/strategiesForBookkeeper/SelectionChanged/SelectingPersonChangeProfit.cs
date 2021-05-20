using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.SelectionChanged
{
    class SelectingPersonChangeProfit:ISelectionChanged
    {
        BookkeeperWindow window;

        public SelectingPersonChangeProfit(BookkeeperWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            if (window.ProfitChangePerson.Items.Count == 0) { return; }
            if (window.ProfitChangePerson.SelectedItem.ToString() == "Нет в списке") { window.ProfitChangePersonName.Text = ""; window.ProfitChangePersonName.IsEnabled = true; }
            else
            { window.ProfitChangePersonName.Text = window.ProfitChangePerson.SelectedItem.ToString(); window.ProfitChangePersonName.IsEnabled = false; }
        }
    }
}
