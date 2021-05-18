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
           window.ViplataBut.IsEnabled = true;

            //расходы
            window.RashDeleteButton.IsEnabled = true;
            window.RashChangeButton.IsEnabled = true;

            //доходы
            window.DohDeleteButton.IsEnabled = true;
            window.DohChangeButton.IsEnabled = true;
        }
    }
}
