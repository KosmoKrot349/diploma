using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForBookkeeper.SelectionChanged
{
    class SelectingPersonAddProfit : ISelectionChanged
    {
        BookkeeperWindow window;

        public SelectingPersonAddProfit(BookkeeperWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            if (window.ProfitAddPerson.Items.Count == 0) { return; }
            if (window.ProfitAddPerson.SelectedItem.ToString() == "Нет в списке") { window.ProfitAddPersonNmae.Text = ""; window.ProfitAddPersonNmae.IsEnabled = true; }
            else { window.ProfitAddPersonNmae.Text = window.ProfitAddPerson.SelectedItem.ToString(); window.ProfitAddPersonNmae.IsEnabled = false; }
        }
    }
}
