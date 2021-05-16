using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class GoToProfitTable:IButtonClick
    {
        BuhgalterWindow windowObj;

        public GoToProfitTable(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.HideAll();
            windowObj.DohodyrGrid.Visibility = Visibility.Visible;
            windowObj.FiltrGridDohody.Children.Clear();
            windowObj.FiltrGridDohody.ColumnDefinitions.Clear();
            windowObj.filtr.CreateFiltrDohody(windowObj.FiltrGridDohody);
            windowObj.filtr.sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) ";
            DataGridUpdater.updateDataGridDohody(windowObj.connectionString, windowObj.filtr.sql, windowObj.DohodyDataGrid);
        }
    }
}
