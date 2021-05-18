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
            if (window.dohAddKtoVnesCm.Items.Count == 0) { return; }
            if (window.dohAddKtoVnesCm.SelectedItem.ToString() == "Нет в списке") { window.dohAddKtoVnesTb.Text = ""; window.dohAddKtoVnesTb.IsEnabled = true; }
            else { window.dohAddKtoVnesTb.Text = window.dohAddKtoVnesCm.SelectedItem.ToString(); window.dohAddKtoVnesTb.IsEnabled = false; }
        }
    }
}
