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
        ManagerWindow windowObj;

        public GoToChangeGroop(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            windowObj.GroopChangeTitle.Text = "";
            windowObj.GroopChangeCourse.Items.Clear();
            windowObj.GroopChangePaymentFor1Month.Text = "";
            windowObj.GroopChangePaymentFor2Month.Text = "";
            windowObj.GroopChangePaymentFor3Month.Text = "";
            windowObj.GroopChangePaymentFor4Month.Text = "";
            windowObj.GroopChangePaymentFor5Month.Text = "";
            windowObj.GroopChangePaymentFor6Month.Text = "";
            windowObj.GroopChangePaymentFor7Month.Text = "";
            windowObj.GroopChangePaymentFor8Month.Text = "";
            windowObj.GroopChangePaymentFor9Month.Text = "";
            windowObj.GroopChangePaymentFor10Month.Text = "";
            windowObj.GroopChangePaymentFor11Month.Text = "";
            windowObj.GroopChangePaymentFor12Month.Text = "";
            windowObj.GroopChangePayForYear.Content = "";
            windowObj.GroopChangeComment.Text = "";
            DataRowView DRV = windowObj.GroupsDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            windowObj.HideAll();
            windowObj.GroupChangeGrid.Visibility = Visibility.Visible;
            object[] arr = DR.ItemArray;
            windowObj.groopID = Convert.ToInt32(arr[0]);
            windowObj.GroopChangeTitle.Text = arr[1].ToString();
            windowObj.dontChangeGroopName = arr[1].ToString();
            windowObj.GroopChangePayForYear.Content = arr[3].ToString();
            windowObj.GroopChangeComment.Text = arr[6].ToString();
            bool ListenerHasGroop = false;
            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select listenerid from listeners where ARRAY[" + windowObj.groopID + "] <@ grid";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    ListenerHasGroop = true;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


            try
            {

                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select payment[1],payment[2],payment[3],payment[4],payment[5],payment[6],payment[7],payment[8],payment[9],payment[10],payment[11],payment[12],date_start,date_end from groups where grid =" + windowObj.groopID;
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.GroopChangePaymentFor1Month.Text = reader.GetDouble(0).ToString();
                        if (reader.GetDouble(0) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor1Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor2Month.Text = reader.GetDouble(1).ToString();
                        if (reader.GetDouble(1) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor2Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor3Month.Text = reader.GetDouble(2).ToString();
                        if (reader.GetDouble(2) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor3Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor4Month.Text = reader.GetDouble(3).ToString();
                        if (reader.GetDouble(3) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor4Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor5Month.Text = reader.GetDouble(4).ToString();
                        if (reader.GetDouble(4) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor5Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor6Month.Text = reader.GetDouble(5).ToString();
                        if (reader.GetDouble(5) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor6Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor7Month.Text = reader.GetDouble(6).ToString();
                        if (reader.GetDouble(6) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor7Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor8Month.Text = reader.GetDouble(7).ToString();
                        if (reader.GetDouble(7) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor8Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor9Month.Text = reader.GetDouble(8).ToString();
                        if (reader.GetDouble(8) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor9Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor10Month.Text = reader.GetDouble(9).ToString();
                        if (reader.GetDouble(9) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor10Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor11Month.Text = reader.GetDouble(10).ToString();
                        if (reader.GetDouble(10) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor11Month.IsEnabled = false; }
                        windowObj.GroopChangePaymentFor12Month.Text = reader.GetDouble(11).ToString();
                        if (reader.GetDouble(11) == 0 && ListenerHasGroop == true) { windowObj.GroopChangePaymentFor12Month.IsEnabled = false; }
                        windowObj.GroopChangeDateStartLearn.Text = reader.GetDateTime(12).ToShortDateString();
                        windowObj.GroopChangeDateEndLearn.Text = reader.GetDateTime(13).ToShortDateString();
                        if (ListenerHasGroop == true) { windowObj.GroopChangeDateStartLearn.IsEnabled = false; windowObj.GroopChangeDateEndLearn.IsEnabled = false; }
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

                        windowObj.GroopChangeCourse.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[2].ToString()) { windowObj.GroopChangeCourse.SelectedIndex = i; b = true; }
                        i++;
                    }
                    if (b == false)
                        windowObj.GroopAddCourse.SelectedIndex = 0;
                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
