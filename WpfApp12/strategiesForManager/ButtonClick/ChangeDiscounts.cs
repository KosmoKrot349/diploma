using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForManager.ButtonClick
{
    class ChangeDiscounts:IButtonClick
    {
        ManagerWindow windowObj;

        public ChangeDiscounts(ManagerWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (Convert.ToDouble(windowObj.sk1.Text) > 100 || Convert.ToDouble(windowObj.sk2.Text) > 100 || Convert.ToDouble(windowObj.sk3.Text) > 100 || Convert.ToDouble(windowObj.sk4.Text) > 100 || Convert.ToDouble(windowObj.sk5.Text) > 100 || Convert.ToDouble(windowObj.sk6.Text) > 100 || Convert.ToDouble(windowObj.sk7.Text) > 100 || Convert.ToDouble(windowObj.sk8.Text) > 100 || Convert.ToDouble(windowObj.sk9.Text) > 100 || Convert.ToDouble(windowObj.sk10.Text) > 100 || Convert.ToDouble(windowObj.sk11.Text) > 100 || Convert.ToDouble(windowObj.sk12.Text) > 100) {MessageBox.Show("Процент не может быть больше 100"); return; }
            NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand com;
            string sql = "";
            try
            {
                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk1.Text + " where kol_month=1 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk2.Text + " where kol_month=2 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk3.Text + " where kol_month=3 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk4.Text + " where kol_month=4 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk5.Text + " where kol_month=5 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk6.Text + " where kol_month=6 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk7.Text + " where kol_month=7 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk8.Text + " where kol_month=8 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk9.Text + " where kol_month=9";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk10.Text + " where kol_month=10 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk11.Text + " where kol_month=11 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.sk12.Text + " where kol_month=12 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();



            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }

            try
            {
                NpgsqlConnection con1 = new NpgsqlConnection(windowObj.connectionString);
                con1.Open();
                string sql1 = "select skidka from skidki order by kol_month";
                NpgsqlCommand com1 = new NpgsqlCommand(sql1, con1);
                NpgsqlDataReader reader1 = com1.ExecuteReader();
                if (reader1.HasRows)
                {
                    int i = 0;
                    while (reader1.Read())
                    {
                        switch (i)
                        {
                            case 0: { windowObj.sk1.Text = reader1.GetDouble(0).ToString(); break; }
                            case 1: { windowObj.sk2.Text = reader1.GetDouble(0).ToString(); break; }
                            case 2: { windowObj.sk3.Text = reader1.GetDouble(0).ToString(); break; }
                            case 3: { windowObj.sk4.Text = reader1.GetDouble(0).ToString(); break; }
                            case 4: { windowObj.sk5.Text = reader1.GetDouble(0).ToString(); break; }
                            case 5: { windowObj.sk6.Text = reader1.GetDouble(0).ToString(); break; }
                            case 6: { windowObj.sk7.Text = reader1.GetDouble(0).ToString(); break; }
                            case 7: { windowObj.sk8.Text = reader1.GetDouble(0).ToString(); break; }
                            case 8: { windowObj.sk9.Text = reader1.GetDouble(0).ToString(); break; }
                            case 9: { windowObj.sk10.Text = reader1.GetDouble(0).ToString(); break; }
                            case 10: { windowObj.sk11.Text = reader1.GetDouble(0).ToString(); break; }
                            case 11: { windowObj.sk12.Text = reader1.GetDouble(0).ToString(); break; }

                        }
                        i++;
                    }

                }
                con1.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
