using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp12
{
    class SaveDataBaseSettings
    {
        public static void Save(string connect,string password,string port)
        {
            object[] arr = new object[3];

            arr[0] = "conn:" + connect;
            arr[1] = "pass:" + password;
            arr[2] = "port:" + port;

            StreamWriter writer = new StreamWriter(@"setting.txt");
            writer.WriteLine(arr[0]); writer.WriteLine(arr[1]); writer.WriteLine(arr[2]);
            writer.Close();
        }
    }
}
