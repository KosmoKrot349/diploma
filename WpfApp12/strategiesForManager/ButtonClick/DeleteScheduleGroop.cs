using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class DeleteScheduleGroop:IButtonClick
    {
        DirectorWindow windowObj;

        public DeleteScheduleGroop(DirectorWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            int grid = Convert.ToInt32(windowObj.lbmas[0, windowObj.jRaspLebale].Name.Split('_')[1]);
            int lesNum = Convert.ToInt32(windowObj.lbmas[windowObj.iRaspLebale, 1].Content.ToString().Split('\n')[0]);
            int day = 0;
            for (int ii = 1; ii <= 7; ii++)
            {
                if (ii * windowObj.m < windowObj.iRaspLebale) { day++; } else break;
            }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "delete from raspisanie where grid =" + grid + " and lesson_number = " + lesNum + " and day=" + (day + 1) + " and date='" + windowObj.dateMonday.AddDays(day).ToShortDateString().Replace('.', '-') + "'";
              MessageBox.Show(sql);
                NpgsqlCommand comand = new NpgsqlCommand(sql, con);
                comand.ExecuteNonQuery();
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            windowObj.showRaspG(windowObj.dateMonday, windowObj.dateMonday.AddDays(6));
        }
    }
}
