using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp12.strategiesForBuhgalter.strategiesForBuhgalterWindButtonClick
{
    class SaveTax:IButtonClick
    {
        BuhgalterWindow windowObj;

        public SaveTax(BuhgalterWindow windowObj)
        {
            this.windowObj = windowObj;
        }

        public void ButtonClick()
        {
            if (Convert.ToDouble(windowObj.FondSotsStrah.Text.Replace('.', ',')) > 100 || Convert.ToDouble(windowObj.VoenSbor.Text.Replace('.', ',')) > 100 || Convert.ToDouble(windowObj.NDFL.Text.Replace('.', ',')) > 100) { MessageBox.Show("Процент не может быть больше 100"); return; }
            double[] mas = new double[3];
            if (windowObj.FondSotsStrah.Text != "") { mas[0] = Convert.ToDouble(windowObj.FondSotsStrah.Text.Replace('.', ',')); }
            if (windowObj.VoenSbor.Text != "") { mas[1] = Convert.ToDouble(windowObj.VoenSbor.Text.Replace('.', ',')); }
            if (windowObj.NDFL.Text != "") { mas[2] = Convert.ToDouble(windowObj.NDFL.Text.Replace('.', ',')); }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "UPDATE nalogi SET sotsstarh=" + mas[0].ToString().Replace(',', '.') + ", voensbor=" + mas[1].ToString().Replace(',', '.') + ", ndfl=" + mas[2].ToString().Replace(',', '.');
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                com.ExecuteNonQuery();
                con.Close();
            }
            catch { System.Windows.Forms.MessageBox.Show("Не удалось подключиться к базе данных"); return; }
            try
            {
                NpgsqlConnection con = new NpgsqlConnection(windowObj.connectionString);
                con.Open();
                string sql = "select * from nalogi";
                NpgsqlCommand com = new NpgsqlCommand(sql, con);
                NpgsqlDataReader reader = com.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        windowObj.FondSotsStrah.Text = reader.GetDouble(0).ToString();
                        windowObj.VoenSbor.Text = reader.GetDouble(1).ToString();
                        windowObj.NDFL.Text = reader.GetDouble(2).ToString();

                    }
                }
                con.Close();

            }
            catch { MessageBox.Show("Не удалось подключиться к базе данных"); return; }
        }
    }
}
