using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class GoToManager : IMenuClick
    {
        public void MenuClick()
        {
            MessageBox.Show("Вы уже выбрали роль директора");
        }
    }
}
