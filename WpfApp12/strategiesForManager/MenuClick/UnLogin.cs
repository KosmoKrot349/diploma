using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.MenuClick
{
    class UnLogin:IMenuClick
    {
        ManagerWindow windowObj;

        public UnLogin(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.logUser = -2;
            MainWindow wind = new MainWindow();
            wind.Login.Text = "";
            wind.Password.Password = "";
            wind.Width = windowObj.Width;
            wind.Height = windowObj.Height;
            wind.Left = windowObj.Left;
            wind.Top = windowObj.Top;
            wind.Show();
            windowObj.Close();
        }
    }
}
