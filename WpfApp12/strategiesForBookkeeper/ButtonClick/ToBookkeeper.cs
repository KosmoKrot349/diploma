using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.ButtonClick
{
    class ToBookkeeper:IButtonClick
    {
        public void ButtonClick()
        {
            MessageBox.Show("Вы уже выбрали роль бухгалтера");
        }
    }
}
