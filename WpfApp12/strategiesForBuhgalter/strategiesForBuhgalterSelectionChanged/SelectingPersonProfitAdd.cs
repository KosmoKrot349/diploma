using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterSelectionChanged
{
    class SelectingPersonProfitAdd:ISelectionChanged
    {
        BuhgalterWindow windowObj;
        ComboBox cmb;

        public SelectingPersonProfitAdd(BuhgalterWindow windowObj, ComboBox cmb)
        {
            this.windowObj = windowObj;
            this.cmb = cmb;
        }

        public void SelectionChanged()
        {

            if (cmb.SelectedIndex == 1)
            {
                windowObj.dohAddKtoVnesCm.Items.Clear();
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select fio from listeners";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            windowObj.dohAddKtoVnesCm.Items.Add(reader.GetString(0));
                        }
                        windowObj.dohAddKtoVnesCm.SelectedIndex = 0;
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            if (cmb.SelectedIndex == 0)
            {
                windowObj.dohAddKtoVnesCm.Items.Clear();
                try
                {

                    NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                    con3.Open();
                    string sql = "select fio from sotrudniki";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con3);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            windowObj.dohAddKtoVnesCm.Items.Add(reader.GetString(0));
                        }
                        windowObj.dohAddKtoVnesCm.SelectedIndex = 0;
                    }
                    con3.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            if (cmb.SelectedIndex == 2) { windowObj.dohAddKtoVnesCm.Items.Clear(); windowObj.dohAddKtoVnesCm.Items.Add("Нет в списке"); }
            windowObj.dohAddKtoVnesCm.SelectedIndex = 0;

        }
    }
}
