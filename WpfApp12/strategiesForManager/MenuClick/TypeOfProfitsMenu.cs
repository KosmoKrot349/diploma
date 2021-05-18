﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class TypeOfProfitsMenu:IMenuClick
    {
        ManagerWindow window;

        public TypeOfProfitsMenu(ManagerWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.TypeDohGrid.Visibility = Visibility.Visible;
            DataGridUpdater.updateDataGriTypeDoh(window.connectionString, window.TypeDohDataGrid);
        }
    }
}
