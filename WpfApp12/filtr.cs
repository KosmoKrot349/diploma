using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;
using Microsoft;

namespace WpfApp12
{
   public class filtr
    {
    public CheckBox[] chbxMas;
    public string sql = "";
        public string connectionString;

        public void CreatePrepFiltr(Grid FiltrGridPrep)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader1.GetString(0);
                        chbxMas[i].Name = "id_" + reader1.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridPrep.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridPrep.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con1.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateGroupFiltr(Grid FiltrGridCourse)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подклюситься к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridCourse.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridCourse.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateCourseFiltr(Grid FiltrGridSubs)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridSubs.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridSubs.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateListenersFiltr(Grid FiltrGridGroups)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridGroups.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridGroups.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateShtatFiltrFirst(Grid FiltrShtatSotr)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrShtatSotr.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrShtatSotr.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateShtatFiltrSecond(Grid FiltrShtatSotr)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrShtatSotr.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrShtatSotr.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

        }
        public void CreateFiltrDohody(Grid FiltrGridDohody)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridDohody.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridDohody.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateFiltrRashody(Grid FiltrGridRashody)
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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "id_" + reader.GetInt32(1);
                        ColumnDefinition cmd = new ColumnDefinition();
                        FiltrGridRashody.ColumnDefinitions.Add(cmd);
                        Grid.SetColumn(chbxMas[i], i);
                        FiltrGridRashody.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
        public void CreateUsersFiltr(Grid FiltrGridRoles)
        {
            FiltrGridRoles.Children.Clear();
            FiltrGridRoles.ColumnDefinitions.Clear();
            chbxMas = new CheckBox[3];
            for (int i = 0; i < 3; i++)
            {
                chbxMas[i] = new CheckBox();
                switch (i)
                {
                    case 0: { chbxMas[i].Content = "Админ"; chbxMas[i].Name = "id_admin"; break; }
                    case 1: { chbxMas[i].Content = "Директор"; chbxMas[i].Name = "id_dir"; break; }
                    case 2: { chbxMas[i].Content = "Бухгалтер"; chbxMas[i].Name = "id_buhg"; break; }
                }
                ColumnDefinition cmd = new ColumnDefinition();
                FiltrGridRoles.ColumnDefinitions.Add(cmd);
                Grid.SetColumn(chbxMas[i], i);
                FiltrGridRoles.Children.Add(chbxMas[i]);

            }
        }

        public void CreateKassaDAFiltr(Grid FiltrGridPeople)
        {
            FiltrGridPeople.Children.Clear();
            FiltrGridPeople.RowDefinitions.Clear();

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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FiltrGridPeople.RowDefinitions.Add(rwd);
                        Grid.SetRow(chbxMas[i], i);
                        FiltrGridPeople.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            
        }
        public void CreateKassaDBFiltr(Grid FiltrGridType)
        {
            FiltrGridType.Children.Clear();
            FiltrGridType.RowDefinitions.Clear();

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
                        chbxMas = new CheckBox[reader1.GetInt32(0)];
                    }

                }
                con1.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "N_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FiltrGridType.RowDefinitions.Add(rwd);
                        Grid.SetRow(chbxMas[i], i);
                        FiltrGridType.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных1"); return; }

        }
        public void CreateKassaRAFiltr(Grid FiltrGridPeople)
        {
            FiltrGridPeople.Children.Clear();
            FiltrGridPeople.RowDefinitions.Clear();

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
                        chbxMas = new CheckBox[reader.GetInt32(0)];
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "N2_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FiltrGridPeople.RowDefinitions.Add(rwd);
                        Grid.SetRow(chbxMas[i], i);
                        FiltrGridPeople.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }


        }
        public void CreateKassaRBFiltr(Grid FiltrGridType)
        {
            FiltrGridType.Children.Clear();
            FiltrGridType.RowDefinitions.Clear();

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
                        chbxMas = new CheckBox[reader1.GetInt32(0)];
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
                        chbxMas[i] = new CheckBox();
                        chbxMas[i].Content = reader.GetString(0);
                        chbxMas[i].Name = "N3_" + reader.GetInt32(1);
                        RowDefinition rwd = new RowDefinition();
                        rwd.Height = new GridLength(30);
                        FiltrGridType.RowDefinitions.Add(rwd);
                        Grid.SetRow(chbxMas[i], i);
                        FiltrGridType.Children.Add(chbxMas[i]);
                        i++;
                    }

                }
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных1"); return; }

        }




        public void ApplyListFiltr() {

            sql = "SELECT listenerid,  fio,  phones, comment,array_length(grid, 1) as grid FROM listeners  where grid @> ARRAY[ ";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += chbxMas[i].Name.Split('_')[1] + ","; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "] order by listenerid"; }
            else
                sql = sql.Substring(0, sql.Length - 21) + "order by listenerid";

        }
        public void ApplyGroupsFiltr()
        {
            sql = "SELECT groups.grid as grid,  groups.nazvanie as gtitle,courses.title as ctitle, groups.comment as comment ,groups.payment[1],groups.payment[2],groups.payment[3],groups.payment[4],groups.payment[5],groups.payment[6],groups.payment[7],groups.payment[8],groups.payment[9],groups.payment[10],groups.payment[11],groups.payment[12],date_start,date_end FROM groups inner join courses using (courseid) where ";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "courseid =" + chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 3); }
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyCourseFiltr()
        {
            sql = "select courseid,title,comment FROM courses where subs @> ARRAY[";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += chbxMas[i].Name.Split('_')[1] + ","; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 20);
        }
        public void ApplyPrepFiltr()
        {
            sql = "SELECT  prep.prepid as prid,kategorii.title as nazva ,sotrudniki.fio as name ,prep.date_start as date,sotrudniki.comment as comm FROM sotrudniki inner join prep using(sotrid) inner join kategorii using(kategid) where ";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "kategid =" + chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
            { sql = sql.Substring(0, sql.Length - 3); }
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyShtatFiltrFirst()
        {
            sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid) where states @> ARRAY [";

            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += chbxMas[i].Name.Split('_')[1] + ","; }

            }

            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 23);

        }
        public void ApplyShtatFiltrSecond()
        {
            sql = "SELECT shtat.shtatid, sotrudniki.fio, array_to_string(stavky,'_') as stavky,array_to_string(obem,'_') as obem   FROM shtat inner join sotrudniki using(sotrid) where obslwork @> ARRAY [";

            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += chbxMas[i].Name.Split('_')[1] + ","; }

            }

            if (change == true)
            { sql = sql.Substring(0, sql.Length - 1) + "]"; }
            else
                sql = sql.Substring(0, sql.Length - 25);
        }
        public void ApplyRashodyFiltr()
        {
            sql = "SELECT rashody.rashid as rashid, typerash.title as title, sotrudniki.fio as fio, rashody.summ as summ , rashody.data as data, rashody.description as description FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid) where ";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "rashody.typeid = " + chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3);
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyDohodyFiltr()
        {
            sql = "SELECT dodhody.dohid as dohid, typedohod.title as title, dodhody.sum as sum, dodhody.data as data, dodhody.fio as fio FROM dodhody inner join typedohod using(idtype) where ";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "dodhody.idtype = " + chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3);
            else
                sql = sql.Substring(0, sql.Length - 7);
        }
        public void ApplyUsersFiltr()
        {
            sql = "select * from users where uid != -1 ";
            if (chbxMas[0].IsChecked == true)
            {
                sql += "and admin = 1 ";
            }
            if (chbxMas[2].IsChecked == true)
            {
                sql += "and buhgalter = 1 ";
            }
            if (chbxMas[1].IsChecked == true)
            {
                sql += "and director = 1 ";
            }

        }

        public void ApplyDohFiltr(filtr fdb)
        {
            sql = "SELECT data,title,sum,fio  FROM dodhody inner join typedohod using(idtype) where (";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "dodhody.fio = " + "'"+chbxMas[i].Content + "' or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3)+")";
            else
                sql = sql.Substring(0, sql.Length - 8);

           string sql2 = "(";

            bool change2 = false;
            for (int i = 0; i < fdb.chbxMas.Length; i++)
            {
                if (fdb.chbxMas[i].IsChecked == true) { change2 = true; sql2 += "dodhody.idtype = " + fdb.chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change2 == true)
                sql2 = sql2.Substring(0, sql2.Length - 3) + ")";
            else
                sql2 = sql2.Substring(0, sql2.Length - 1);

            if (change == true && change2==true) { sql += " and " + sql2; } if(change == false && change2 == true) { sql += " where " + sql2; }
        }

        public void ApplyRashFiltr(filtr frb)
        {
            sql = "SELECT data,title,fio,summ  FROM rashody inner join typerash using(typeid) inner join sotrudniki using(sotrid) where (";
            bool change = false;
            for (int i = 0; i < chbxMas.Length; i++)
            {
                if (chbxMas[i].IsChecked == true) { change = true; sql += "rashody.sotrid = "  + chbxMas[i].Name.Split('_')[1] + " or "; }

            }
            if (change == true)
                sql = sql.Substring(0, sql.Length - 3) + ")";
            else
                sql = sql.Substring(0, sql.Length - 8);

          string sql2 = "(";

            bool change2 = false;
            for (int i = 0; i < frb.chbxMas.Length; i++)
            {
                if (frb.chbxMas[i].IsChecked == true) { change2 = true; sql2 += "rashody.typeid = " + frb.chbxMas[i].Name.Split('_')[1] + " or "; }

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
