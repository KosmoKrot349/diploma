using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp12.strategiesForBookkeeper.SelectionChanged
{
    class SelectingPersonTypeProfitChange:ISelectionChanged
    {
        BookkeeperWindow windowObj;
        ComboBox cmb;

        public SelectingPersonTypeProfitChange(BookkeeperWindow windowObj,ComboBox cmb)
        {
            this.windowObj = windowObj;
            this.cmb = cmb;
        }

        public void SelectionChanged()
        {
            if (cmb.SelectedIndex == 1)
            {
                windowObj.dohChKtoVnesCm.Items.Clear();
                try
                {
                    NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                    con.Open();
                    string sql = "select fio from listeners";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        windowObj.dohChKtoVnesCm.SelectedIndex = 0;
                        int i = 0;
                        while (reader.Read())
                        {
                            windowObj.dohChKtoVnesCm.Items.Add(reader.GetString(0));

                            if (reader.GetString(0) == windowObj.personForProfit) {windowObj.dohChKtoVnesCm.SelectedIndex = i; }
                            i++;
                        }
                    }
                    con.Close();
                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            if (cmb.SelectedIndex == 0)
            {
                windowObj.dohChKtoVnesCm.Items.Clear();
                try
                {

                    NpgsqlConnection con3 = new NpgsqlConnection(windowObj.connectionString);
                    con3.Open();
                    string sql = "select fio from sotrudniki";
                    NpgsqlCommand com = new NpgsqlCommand(sql, con3);
                    NpgsqlDataReader reader = com.ExecuteReader();
                    if (reader.HasRows)
                    {
                        windowObj.dohChKtoVnesCm.SelectedIndex = 0;
                        int i = 0;
                        while (reader.Read())
                        {
                            windowObj.dohChKtoVnesCm.Items.Add(reader.GetString(0));
                            if (reader.GetString(0) == windowObj.personForProfit) { windowObj.dohChKtoVnesCm.SelectedIndex = i; }
                            i++;
                        }
                    }
                    con3.Close();

                }
                catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            }
            if (cmb.SelectedIndex == 2) { windowObj.dohChKtoVnesCm.Items.Clear(); windowObj.dohChKtoVnesCm.SelectedIndex = 0; windowObj.dohChKtoVnesCm.Items.Add("Нет в списке"); }

        }
    }
}
