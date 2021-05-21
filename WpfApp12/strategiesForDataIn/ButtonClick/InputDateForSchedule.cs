using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForDataIn.ButtonClick
{
    class InputDateForSchedule:IButtonClick
    {
        DateIn window;

        public InputDateForSchedule(DateIn window)
        {
            this.window = window;
        }

        public void ButtonClick()
        {
            if (window.datePick.Text == "") { MessageBox.Show("Дата не выбрана"); return; }
            if (Convert.ToDateTime(window.datePick.Text).DayOfWeek.ToString() != "Monday") { MessageBox.Show("Дата не понедельник"); return; }
            window.dateMonday = Convert.ToDateTime(window.datePick.Text);
            window.Close();
        }
    }
}
