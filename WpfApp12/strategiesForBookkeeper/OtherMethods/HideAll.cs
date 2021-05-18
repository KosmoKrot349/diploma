using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBookkeeper.OtherMethods
{
    class HideAll
    {
        public static void Hide(BookkeeperWindow window)
        {
            window.NachDataGrid.SelectedItem = null;
            window.RoshodyDataGrid.SelectedItem = null;
            window.DohodyDataGrid.SelectedItem = null;

            window.helloGrdi.Visibility = Visibility.Collapsed;
            window.OplataGrid.Visibility = Visibility.Collapsed;
            window.DohodyrGrid.Visibility = Visibility.Collapsed;
            window.DohodyrAddGrid.Visibility = Visibility.Collapsed;
            window.DohodyChangeGrid.Visibility = Visibility.Collapsed;
            window.RoshodyGrid.Visibility = Visibility.Collapsed;
            window.RashodyAddGrid.Visibility = Visibility.Collapsed;
            window.RashodyChangeGrid.Visibility = Visibility.Collapsed;
            window.NalogiGrid.Visibility = Visibility.Collapsed;
            window.GlNachGrid.Visibility = Visibility.Collapsed;
            window.DolgGrid.Visibility = Visibility.Collapsed;
            window.NoDolgGrdi.Visibility = Visibility.Collapsed;
            window.kassaGrid.Visibility = Visibility.Collapsed;
            window.StatisticaGrid.Visibility = Visibility.Collapsed;
            window.ZpOthcetGrid.Visibility = Visibility.Collapsed;

            //начисления
            window.ViplataBut.IsEnabled = false;

            //расходы
            window.RashDeleteButton.IsEnabled = false;
            window.RashChangeButton.IsEnabled = false;

            //доходы
            window.DohDeleteButton.IsEnabled = false;
            window.DohChangeButton.IsEnabled = false;

        }
    }
}
