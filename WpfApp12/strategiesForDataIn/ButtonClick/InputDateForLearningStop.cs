using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForDataIn.ButtonClick
{
    class InputDateForLearningStop:IButtonClick
    {
        DateIn window;

        public InputDateForLearningStop(DateIn window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (window.SelectDateToPay.Text == "") { MessageBox.Show("Дата не выбрана"); return; }
            window.dateMonday = Convert.ToDateTime(window.SelectDateToPay.Text);
            window.Close();
        }
    }
}
