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
            if (Convert.ToDouble(windowObj.DiscountFor1Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor2Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor3Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor4Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor5Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor6Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor7Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor8Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor9Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor10Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor11Month.Text) > 100 || Convert.ToDouble(windowObj.DiscountFor12Month.Text) > 100) {MessageBox.Show("Процент не может быть больше 100"); return; }
            NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
            NpgsqlCommand com;
            string sql = "";
            try
            {
                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor1Month.Text + " where kol_month=1 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor2Month.Text + " where kol_month=2 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor3Month.Text + " where kol_month=3 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor4Month.Text + " where kol_month=4 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor5Month.Text + " where kol_month=5 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor6Month.Text + " where kol_month=6 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor7Month.Text + " where kol_month=7 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor8Month.Text + " where kol_month=8 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor9Month.Text + " where kol_month=9";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor10Month.Text + " where kol_month=10 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor11Month.Text + " where kol_month=11 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();

                con.Open();
                sql = "update skidki set skidka=" + windowObj.DiscountFor12Month.Text + " where kol_month=12 ";
                com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();



            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }

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
                            case 0: { windowObj.DiscountFor1Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 1: { windowObj.DiscountFor2Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 2: { windowObj.DiscountFor3Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 3: { windowObj.DiscountFor4Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 4: { windowObj.DiscountFor5Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 5: { windowObj.DiscountFor6Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 6: { windowObj.DiscountFor7Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 7: { windowObj.DiscountFor8Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 8: { windowObj.DiscountFor9Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 9: { windowObj.DiscountFor10Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 10: { windowObj.DiscountFor11Month.Text = reader1.GetDouble(0).ToString(); break; }
                            case 11: { windowObj.DiscountFor12Month.Text = reader1.GetDouble(0).ToString(); break; }

                        }
                        i++;
                    }

                }
                con1.Close();
            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
