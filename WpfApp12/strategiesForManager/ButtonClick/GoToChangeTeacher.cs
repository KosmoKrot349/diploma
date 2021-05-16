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
    class GoToChangeTeacher : IButtonClick
    {
        DirectorWindow windowObj;

        public GoToChangeTeacher(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            DataRowView DRV = windowObj.prepDataGrid.SelectedItem as DataRowView;
            if (DRV == null) { MessageBox.Show("Вы не можете перейти к изменению не выбрав запись."); return; }
            DataRow DR = DRV.Row;
            object[] arr = DR.ItemArray;
            windowObj.prepID = Convert.ToInt32(arr[0]);
            windowObj.prepFio.Text = arr[2].ToString();
            windowObj.prepCom.Text = arr[4].ToString();
            string[] date1 = arr[3].ToString().Split(' ');
            string date2 = date1[0];
            windowObj.dateStartAdd.Text = date2;
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select title from kategorii";
                NpgsqlCommand command = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = command.ExecuteReader();
                int itmeIndex = 0;
                windowObj.kategCMBX.SelectedIndex = 0;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.kategCMBX.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == arr[1].ToString()) { windowObj.kategCMBX.SelectedIndex = itmeIndex; }
                        itmeIndex++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.HideAll();
            windowObj.prepChangeGrid.Visibility = Visibility.Visible;
        }
    }
}
