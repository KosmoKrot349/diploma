using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Npgsql;
using System.Collections;
using System.Windows.Media;

namespace WpfApp12.strategiesForAdmin.MenuClick
{
    class GoToSeatings:IMenuClick
    {
        private AdminWindow windowObj;

        public GoToSeatings(AdminWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void MenuClick()
        {
            windowObj.MenuRoles.BorderBrush = null;
            windowObj.usersMenu.BorderBrush = null;
            windowObj.archiveMenu.BorderBrush = null;
            windowObj.settingMenu.BorderBrush = Brushes.DarkRed;
            windowObj.GoToNextYear.BorderBrush = null;
            windowObj.hideAll();
            windowObj.SettingsGrid.Visibility = Visibility.Visible;

            StreamReader streamReader = new StreamReader(@"setting.txt");
            ArrayList list = new ArrayList();
            while (!streamReader.EndOfStream)
            {
                list.Add(streamReader.ReadLine());
            }
            streamReader.Close();
            object[] stringArr = list.ToArray();
            windowObj.connect.Text = stringArr[0].ToString().Split(':')[1];
            windowObj.dbPassText.Text = stringArr[1].ToString().Split(':')[1];
            windowObj.dbPortSettings.Text = stringArr[2].ToString().Split(':')[1];


            if (windowObj.logUser == -1)
            {
                try
                {
                    windowObj.rootSettings.Visibility = Visibility.Visible;
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select pas from users where uid = -1";
                    NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = comand.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            windowObj.rootPassSettings.Text = reader.GetString(0);

                        }

                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            else windowObj.rootSettings.Visibility = Visibility.Collapsed;
        }
    }
}
