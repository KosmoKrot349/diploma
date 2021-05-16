﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class StaffMenu:IMenuClick
    {
        DirectorWindow window;

        public StaffMenu(DirectorWindow window)
        {
            this.window = window;
        }

        public void MenuClick()
        {
            window.HideAll();
            window.ShtatGrid.Visibility = Visibility.Visible;
            window.ShtatFiltrCmbx.SelectedIndex = 0;
            window.filtr.sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid)";
            DataGridUpdater.updateDataGridShtat(window.connectionString, window.filtr.sql, window.ShtatDataGrid);
        }
    }
}
