using System.Windows;
using System.Windows.Controls;
using Npgsql;

namespace WpfApp12
{
   public class filter
    {
    public CheckBox[] checkBoxVariantsArr;
    public string sql = "";
        public string connectionString;

        public void CreateTeacherFilter(Grid FilterGridTeacher)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(kategid) from kategorii";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select title,kategid from kategorii";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    int i = 0;
                    while (reader1.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader1.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader1.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterGridTeacher.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterGridTeacher.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateGroupFilter(Grid FilterGridCourse)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(courseid) from courses";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,courseid from courses";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterGridCourse.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterGridCourse.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateCourseFilter(Grid FilterGridSubjects)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(subid) from subject";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,subid from subject";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterGridSubjects.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterGridSubjects.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateListenersFilter(Grid FilterGridGroups)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(grid) from groups";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select nazvanie,grid from groups";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterGridGroups.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterGridGroups.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch {MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateStaffFirstFilter(Grid FilterStaffGrid)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(statesid) from  states";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,statesid from states";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterStaffGrid.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterStaffGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateStaffSecondFilter(Grid FilterStaffGrid)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(rabotyid) from  raboty_obsl";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,rabotyid from raboty_obsl";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterStaffGrid.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterStaffGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateProfitFilter(Grid FilterProfitGrid)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(idtype) from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,idtype from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterProfitGrid.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterProfitGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateCostsFilter(Grid FilterCostsGrid)
        {
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(typeid) from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,typeid from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FilterCostsGrid.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(checkBoxVariantsArr[i], i);
                        FilterCostsGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateUsersFilter(Grid FilterRolesGrid)
        {
            FilterRolesGrid.Children.Clear();
            FilterRolesGrid.ColumnDefinitions.Clear();
            checkBoxVariantsArr = new CheckBox[3];
            for (int i = 0; i < 3; i++)
            {
                checkBoxVariantsArr[i] = new CheckBox();
                switch (i)
                {
                    case 0: { checkBoxVariantsArr[i].Content = "Админ"; checkBoxVariantsArr[i].Name = "isAdmin"; break; }
                    case 1: { checkBoxVariantsArr[i].Content = "Директор"; checkBoxVariantsArr[i].Name = "isManager"; break; }
                    case 2: { checkBoxVariantsArr[i].Content = "Бухгалтер"; checkBoxVariantsArr[i].Name = "isBookkeeper"; break; }
                }
                ColumnDefinition cmd = new ColumnDefinition();
                FilterRolesGrid.ColumnDefinitions.Add(cmd);
                Grid.SetColumn(checkBoxVariantsArr[i], i);
                FilterRolesGrid.Children.Add(checkBoxVariantsArr[i]);

            }
        }
        public void CreateCashboxProfitPersonFilter(Grid FilterPeopleGrid)
        {
            FilterPeopleGrid.Children.Clear();
            FilterPeopleGrid.RowDefinitions.Clear();

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count(distinct fio) from dodhody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select distinct fio from dodhody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FilterPeopleGrid.RowDefinitions.Add(rwd);
                        Grid.SetRow(checkBoxVariantsArr[i], i);
                        FilterPeopleGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            
        }
        public void CreateCashboxProfitTypesFilter(Grid FilterTypeGrid)
        {
            FilterTypeGrid.Children.Clear();
            FilterTypeGrid.RowDefinitions.Clear();

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(title) from typedohod";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader1.GetInt32(0)];
                    }

                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,idtype from typedohod";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "N_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FilterTypeGrid.RowDefinitions.Add(rwd);
                        Grid.SetRow(checkBoxVariantsArr[i], i);
                        FilterTypeGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных1"); return; }

        }
        public void CreateCashboxCostsPersonFilter(Grid FilterPeopleGrid)
        {
            FilterPeopleGrid.Children.Clear();
            FilterPeopleGrid.RowDefinitions.Clear();

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select count( sotrid) from rashody";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select  distinct fio,sotrid from rashody inner join sotrudniki using(sotrid)";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "N2_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FilterPeopleGrid.RowDefinitions.Add(rwd);
                        Grid.SetRow(checkBoxVariantsArr[i], i);
                        FilterPeopleGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }


        }
        public void CreateCashboxCostsTypesFilter(Grid FilterTypeGrid)
        {
            FilterTypeGrid.Children.Clear();
            FilterTypeGrid.RowDefinitions.Clear();

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(connectionString);
                con1.Open();
                string sql1 = "select count(title) from typerash";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    while (reader1.Read())
                    {
                        checkBoxVariantsArr = new CheckBox[reader1.GetInt32(0)];
                    }

                }
                con1.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con = new NpgsqlConnection(connectionString);
                con.Open();
                string sql = "select title,typeid from typerash";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        checkBoxVariantsArr[i] = new CheckBox();
                        checkBoxVariantsArr[i].Content = reader.GetString(0);
                        checkBoxVariantsArr[i].Name = "N3_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FilterTypeGrid.RowDefinitions.Add(rwd);
                        Grid.SetRow(checkBoxVariantsArr[i], i);
                        FilterTypeGrid.Children.Add(checkBoxVariantsArr[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных1"); return; }

        }


        public void ApplyListenerFilter() {

            sql = "SELECT listenerid,  fio,  phones, comment,array_length(grid, 1) as grid FROM listeners  where grid @> ARRAY[ ";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += checkBoxVariantsArr[i].Name.Split('_')[1] + ","; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "] order by listenerid"; }
            else
                sql = sql.Substring(0, sql.Length - 21) + "order by listenerid";

        }
        public void ApplyGroupsFilter()
        {
            sql = "SELECT groups.grid as grid,  groups.nazvanie as gtitle,courses.title as ctitle, groups.comment as comment ,groups.payment[1],groups.payment[2],groups.payment[3],groups.payment[4],groups.payment[5],groups.payment[6],groups.payment[7],groups.payment[8],groups.payment[9],groups.payment[10],groups.payment[11],groups.payment[12],date_start,date_end FROM groups inner join courses using (courseid) where ";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "courseid =" + checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 3); }
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyCourseFilter()
        {
            sql = "select courseid,title,comment FROM courses where subs @> ARRAY[";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += checkBoxVariantsArr[i].Name.Split('_')[1] + ","; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 20);
        }
        public void ApplyTeachersFilter()
        {
            sql = "SELECT  prep.prepid as prid,kategorii.title as nazva ,sotrudniki.fio as name ,prep.date_start as date,sotrudniki.comment as comm FROM sotrudniki inner join prep using(sotrid) inner join kategorii using(kategid) where ";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "kategid =" + checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 3); }
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyStaffFirstFilter()
        {
            sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid) where states @> ARRAY [";

            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += checkBoxVariantsArr[i].Name.Split('_')[1] + ","; }

            }

            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 23);

        }
        public void ApplyStaffSecondFilter()
        {
            sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid) where obslwork @> ARRAY [";

            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += checkBoxVariantsArr[i].Name.Split('_')[1] + ","; }

            }

            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 25);
        }
        public void ApplyCostsFilter()
        {
            sql = "SELECT rashody.rashid as rashid, typerash.title as title, sotrudniki.fio as fio, rashody.summ as summ , rashody.data as data, rashody.description as description FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid) where ";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "rashody.typeid = " + checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3);
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyProfitFilter()
        {
            sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) where ";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "dodhody.idtype = " + checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3);
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyUsersFilter()
        {
            sql = "select * from users where uid != -1 ";
            if (checkBoxVariantsArr[0].IsChecked == true)
            {
                sql += "and admin = 1 ";
            }
            if (checkBoxVariantsArr[2].IsChecked == true)
            {
                sql += "and buhgalter = 1 ";
            }
            if (checkBoxVariantsArr[1].IsChecked == true)
            {
                sql += "and director = 1 ";
            }

        }
        public void ApplyProfitFilterForCashboxReport(filter filterProfitTypes) { 
            sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype) where (";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "dodhody.fio = " + "'"+checkBoxVariantsArr[i].Content + "' or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3)+")";
            else
                sql = sql.Substring(0, sql.Length - 8);

           string sql2 = "(";

            bool change2 = false;
            for (int i = 0; i < filterProfitTypes.checkBoxVariantsArr.Length; i++)
            {
                if (filterProfitTypes.checkBoxVariantsArr[i].IsChecked == true) { change2 = true; sql2 += "dodhody.idtype = " + filterProfitTypes.checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change2 == true)
                sql2 = sql2.Substring(0, sql2.Length - 3) + ")";
            else
                sql2 = sql2.Substring(0, sql2.Length - 1);

            if (change == true && change2==true) { sql += " and " + sql2; } if(change == false && change2 == true) { sql += " where " + sql2; }
        }
        public void ApplyCostsFilterForCashboxReport(filter filterCostsTypes)
        {
            sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid) where (";
            bool change = false;
            for (int i = 0; i < checkBoxVariantsArr.Length; i++)
            {
                if (checkBoxVariantsArr[i].IsChecked == true) { change = true; sql += "rashody.sotrid = "  + checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3) + ")";
            else
                sql = sql.Substring(0, sql.Length - 8);

          string sql2 = "(";

            bool change2 = false;
            for (int i = 0; i < filterCostsTypes.checkBoxVariantsArr.Length; i++)
            {
                if (filterCostsTypes.checkBoxVariantsArr[i].IsChecked == true) { change2 = true; sql2 += "rashody.typeid = " + filterCostsTypes.checkBoxVariantsArr[i].Name.Split('_')[1] + " or "; }

            }
            if (change2 == true)
                sql2 = sql2.Substring(0, sql2.Length - 3) + ")";
            else
                sql2 = sql2.Substring(0, sql2.Length - 1);

            if (change == true && change2 == true) { sql += " and " + sql2; }
            if (change == false && change2 == true) { sql += " where " + sql2; }


        }
    }
}
