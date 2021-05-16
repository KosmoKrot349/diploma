using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForAdmin
{
    class ToAdmin:IButtonClick
    {
        public void buttonClick()
        {
            MessageBox.Show("Вы уже выбрали роль администратора");

        }
    }
}
