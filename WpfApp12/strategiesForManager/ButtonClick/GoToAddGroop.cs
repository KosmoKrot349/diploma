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
        DirectorWindow windowObj;

        public GoToAddGroop(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.grTitle.Text = "";
            windowObj.grComm.Text = "";
            windowObj.grCourse.SelectedIndex = 0;
            windowObj.grPayment1.Text = "";
            windowObj.grPayment2.Text = "";
            windowObj.grPayment3.Text = "";
            windowObj.grPayment4.Text = "";
            windowObj.grPayment5.Text = "";
            windowObj.grPayment6.Text = "";
            windowObj.grPayment7.Text = "";
            windowObj.grPayment8.Text = "";
            windowObj.grPayment9.Text = "";
            windowObj.grPayment10.Text = "";
            windowObj.grPayment11.Text = "";
            windowObj.grPayment12.Text = "";
            windowObj.payToYear.Content = "";
            windowObj.HideAll();
            windowObj.groupAddGrid.Visibility = Visibility.Visible;
            windowObj.DateStartGrAdd.Text = DateTime.Now.AddMonths(-11).ToShortDateString();
            windowObj.DateEndGrAdd.Text = DateTime.Now.ToShortDateString();
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
                        windowObj.grCourse.Items.Add(reader.GetString(0));

                    }
                    windowObj.grCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
