﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class TeachersMenu:IMenuClick 
    {
        DirectorWindow windowObj;

        public TeachersMenu(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.HideAll();
            windowObj.prepGrid.Visibility = Visibility.Visible;

            windowObj.FiltrGridPrep.Children.Clear();
            windowObj.FiltrGridPrep.ColumnDefinitions.Clear();

            windowObj.filtr.CreatePrepFiltr(windowObj.FiltrGridPrep);
            windowObj.filtr.sql = "SELECT prep.prepid as prid,kategorii.title as nazva ,sotrudniki.fio as name ,prep.date_start as date,sotrudniki.comment as comm FROM sotrudniki inner join prep using(sotrid) inner join kategorii using(kategid)";

            DataGridUpdater.updateDataGridPrep(windowObj.connectionString, windowObj.filtr.sql, windowObj.prepDataGrid);
        }
    }
}