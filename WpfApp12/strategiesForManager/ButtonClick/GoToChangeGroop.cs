using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class GoToChangeGroop:IButtonClick
    {
        DirectorWindow windowObj;

        public GoToChangeGroop(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.grTitleCh.Text = "";
            windowObj.grCourseCh.Items.Clear();
            windowObj.grPayment1Ch.Text = "";
            windowObj.grPayment2Ch.Text = "";
            windowObj.grPayment3Ch.Text = "";
            windowObj.grPayment4Ch.Text = "";
            windowObj.grPayment5Ch.Text = "";
            windowObj.grPayment6Ch.Text = "";
            windowObj.grPayment7Ch.Text = "";
            windowObj.grPayment8Ch.Text = "";
            windowObj.grPayment9Ch.Text = "";
            windowObj.grPayment10Ch.Text = "";
            windowObj.grPayment11Ch.Text = "";
            windowObj.grPayment12Ch.Text = "";
            windowObj.payToYearCh.Content = "";
            windowObj.grCommCh.Text = "";
            DataRowView DRV = windowObj.groupsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            windowObj.HideAll();
            windowObj.groupChangeGrid.Visibility = Visibility.Visible;
            object[] arr = DR.ItemArray;
            windowObj.grID = Convert.ToInt32(arr[0]);
            windowObj.grTitleCh.Text = arr[1].ToString();
            windowObj.dontChGName = arr[1].ToString();
            windowObj.payToYearCh.Content = arr[3].ToString();
            windowObj.grCommCh.Text = arr[6].ToString();
            bool ListHasGr = false;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select listenerid from listeners where ARRAY[" + windowObj.grID + "] <@ grid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ListHasGr = true;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12],date_start,date_end from groups where grid =" + windowObj.grID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.grPayment1Ch.Text = reader.GetDouble(0).ToString();
                        if (reader.GetDouble(0) == 0 && ListHasGr == true) { windowObj.grPayment1Ch.IsEnabled = false; }
                        windowObj.grPayment2Ch.Text = reader.GetDouble(1).ToString();
                        if (reader.GetDouble(1) == 0 && ListHasGr == true) { windowObj.grPayment2Ch.IsEnabled = false; }
                        windowObj.grPayment3Ch.Text = reader.GetDouble(2).ToString();
                        if (reader.GetDouble(2) == 0 && ListHasGr == true) { windowObj.grPayment3Ch.IsEnabled = false; }
                        windowObj.grPayment4Ch.Text = reader.GetDouble(3).ToString();
                        if (reader.GetDouble(3) == 0 && ListHasGr == true) { windowObj.grPayment4Ch.IsEnabled = false; }
                        windowObj.grPayment5Ch.Text = reader.GetDouble(4).ToString();
                        if (reader.GetDouble(4) == 0 && ListHasGr == true) { windowObj.grPayment5Ch.IsEnabled = false; }
                        windowObj.grPayment6Ch.Text = reader.GetDouble(5).ToString();
                        if (reader.GetDouble(5) == 0 && ListHasGr == true) { windowObj.grPayment6Ch.IsEnabled = false; }
                        windowObj.grPayment7Ch.Text = reader.GetDouble(6).ToString();
                        if (reader.GetDouble(6) == 0 && ListHasGr == true) { windowObj.grPayment7Ch.IsEnabled = false; }
                        windowObj.grPayment8Ch.Text = reader.GetDouble(7).ToString();
                        if (reader.GetDouble(7) == 0 && ListHasGr == true) { windowObj.grPayment8Ch.IsEnabled = false; }
                        windowObj.grPayment9Ch.Text = reader.GetDouble(8).ToString();
                        if (reader.GetDouble(8) == 0 && ListHasGr == true) { windowObj.grPayment9Ch.IsEnabled = false; }
                        windowObj.grPayment10Ch.Text = reader.GetDouble(9).ToString();
                        if (reader.GetDouble(9) == 0 && ListHasGr == true) { windowObj.grPayment10Ch.IsEnabled = false; }
                        windowObj.grPayment11Ch.Text = reader.GetDouble(10).ToString();
                        if (reader.GetDouble(10) == 0 && ListHasGr == true) { windowObj.grPayment11Ch.IsEnabled = false; }
                        windowObj.grPayment12Ch.Text = reader.GetDouble(11).ToString();
                        if (reader.GetDouble(11) == 0 && ListHasGr == true) { windowObj.grPayment12Ch.IsEnabled = false; }
                        windowObj.DateStartCh.Text = reader.GetDateTime(12).ToShortDateString();
                        windowObj.DateEndCh.Text = reader.GetDateTime(13).ToShortDateString();
                        if (ListHasGr == true) { windowObj.DateStartCh.IsEnabled = false; windowObj.DateEndCh.IsEnabled = false; }
                    }
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from courses";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                bool b = false;
                int i = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        windowObj.grCourseCh.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { windowObj.grCourseCh.SelectedIndex = i; b = true; }
                        i++;
                    }
                    if (b == false)
                        windowObj.grCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
