using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.SelectionChanged
{
    class ChangeGroopFromAddTeacherSchedule:ISelectionChaged
    {
        ManagerWindow window;

        public ChangeGroopFromAddTeacherSchedule(ManagerWindow window)
        {
            this.window = window;
        }

        public void SelectionChanged()
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(window.connectionString);
                con.Open();
                string sql = "SELECT title FROM subject where(select courses.subs from courses inner join groups using(courseid) where nazvanie ='" + window.TeacherScheduleSelectGroop.SelectedItem + "' )  @> ARRAY[subid]";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                   window.TeacherScheduleSelectSubject.Items.Clear();
                    while (reader.Read())
                    {
                        window.TeacherScheduleSelectSubject.Items.Add(reader.GetString(0));
                    }
                    window.TeacherScheduleSelectSubject.SelectedIndex = 0;
                }

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
