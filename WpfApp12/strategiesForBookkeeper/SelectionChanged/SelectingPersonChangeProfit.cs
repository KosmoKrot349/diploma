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
            if (window.dohChKtoVnesCm.Items.Count == 0) { return; }
            if (window.dohChKtoVnesCm.SelectedItem.ToString() == "Нет в списке") { window.dohChKtoVnesTb.Text = ""; window.dohChKtoVnesTb.IsEnabled = true; }
            else
            { window.dohChKtoVnesTb.Text = window.dohChKtoVnesCm.SelectedItem.ToString(); window.dohChKtoVnesTb.IsEnabled = false; }
        }
    }
}
