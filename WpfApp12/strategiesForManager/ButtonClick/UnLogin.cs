﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class UnLogin:IButtonClick
    {
        DirectorWindow windowObj;

        public UnLogin(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.logUser = -2;
            MainWindow wind = new MainWindow();
            wind.log.Text = "";
            wind.pas.Password = "";
            wind.Width = windowObj.Width;
            wind.Height = windowObj.Height;
            wind.Left = windowObj.Left;
            wind.Top = windowObj.Top;
            wind.Show();
            windowObj.Close();
        }
    }
}