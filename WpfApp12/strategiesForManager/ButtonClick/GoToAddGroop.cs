using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToAddGroop:IButtonClick
    {
        ManagerWindow windowObj;

        public GoToAddGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.GroopAddTitle.Text = "";
            windowObj.GroopAddComment.Text = "";
            windowObj.GroopAddCourse.SelectedIndex = 0;
            windowObj.GroopAddPaymentFor1Month.Text = "";
            windowObj.GroopAddPaymentFor2Month.Text = "";
            windowObj.GroopAddPaymentFor3Month.Text = "";
            windowObj.GroopAddPaymentFor4Month.Text = "";
            windowObj.GroopAddPaymentFor5Month.Text = "";
            windowObj.GroopAddPaymentFor6Month.Text = "";
            windowObj.GroopAddPaymentFor7Month.Text = "";
            windowObj.GroopAddPaymentFor8Month.Text = "";
            windowObj.GroopAddPaymentFor9Month.Text = "";
            windowObj.GroopAddPaymentFor10Month.Text = "";
            windowObj.GroopAddPaymentFor11Month.Text = "";
            windowObj.GroopAddPaymentFor12Month.Text = "";
            windowObj.GroopAddPayForYear.Content = "";
            windowObj.HideAll();
            windowObj.GroopAddGrid.Visibility = Visibility.Visible;
            windowObj.GroopAddDateStartLearn.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
            windowObj.GroopAddDateEndLearn.Text = DateTime.Now.ToShortDateString();
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from courses";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.GroopAddCourse.Items.Add(reader.GetString(0));

                    }
                    windowObj.GroopAddCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
