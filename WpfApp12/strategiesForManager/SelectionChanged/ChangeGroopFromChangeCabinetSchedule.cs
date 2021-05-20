using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.SelectionChanged
{
    class ChangeGroopFromChangeCabinetSchedule:ISelectionChaged
    {
        ManagerWindow window;

        public ChangeGroopFromChangeCabinetSchedule(ManagerWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + window.CabinetScheduleChangeGroop.SelectedItem + "' )  @> ARRAY[subid]";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    window.CabinetScheduleChangeSubject.Items.Clear();
                    window.CabinetScheduleChangeSubject.SelectedIndex = 0;
                    int i = 0;
                    while (reader.Read())
                    {
                        window.CabinetScheduleChangeSubject.Items.Add(reader.GetString(0));
                        if (reader.GetString(0) == window.labelArr[window.iCoordScheduleLabel, window.jCoordScheduleLabel].Content.ToString().Split('\n')[0]) { window.CabinetScheduleChangeSubject.SelectedIndex = i; }
                        i++;
                    }

                }

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
