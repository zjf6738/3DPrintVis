using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharp7;

namespace Sharp7Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //-------------- Create and connect the client
            //string ip = "192.168.0.2";
            //int rack = 0;
            //int slot = 1;

            string ip = ConfigurationManager.AppSettings["PLC_IP"];
            int rack = Convert.ToInt32(ConfigurationManager.AppSettings["PLC_RACK"]);
            int slot = Convert.ToInt32(ConfigurationManager.AppSettings["PLC_SLOT"]);


            //Console.Write("ip:"); 
            //ip = Console.ReadLine();
            //Console.Write("rack:");
            //rack = Convert.ToInt32(Console.ReadLine());
            //Console.Write("slot:");
            //slot = Convert.ToInt32(Console.ReadLine());


            var client = new S7Client();
            int result = client.ConnectTo(ip, rack, slot);
            if (result == 0)
            {
                Console.WriteLine("Connected to " + ip);
            }
            else
            {
                Console.WriteLine("ErrCode-" + result.ToString() + ":" + client.ErrorText(result));
                Console.ReadKey();
                return;
            }

            //// ----------------Write db3
            //byte [] db3 = new byte[9];
            //db3[0] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB0"]);
            //db3[1] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB1"]);
            //db3[2] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB2"]);
            //db3[3] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB3"]);
            //db3[4] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB4"]);
            //db3[5] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB5"]);
            //db3[6] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB6"]);
            //db3[7] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB7"]);
            //db3[8] = (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB8"]);

            //var s7MultiVar = new S7MultiVar(client);
            //const int DB3_START_INDEX = 0;
            ////S7.SetByteAt(db3, 0, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB0"]));
            ////S7.SetByteAt(db3, 1, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB1"]));
            ////S7.SetByteAt(db3, 2, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB2"]));
            ////S7.SetByteAt(db3, 3, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB3"]));
            ////S7.SetByteAt(db3, 4, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB4"]));
            ////S7.SetByteAt(db3, 5, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB5"]));
            ////S7.SetByteAt(db3, 6, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB6"]));
            ////S7.SetByteAt(db3, 7, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB7"]));
            ////S7.SetByteAt(db3, 8, (byte)Convert.ToByte(ConfigurationManager.AppSettings["DB3.DBB8"]));


            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 3, DB3_START_INDEX, db3.Length, ref db3);

            //result = s7MultiVar.Write();
            //if (result != 0)
            //{
            //    Console.WriteLine("Error on s7MultiVar.Write()" + client.ErrorText(result));
            //}

            //byte[] db3Read = new byte[18];
            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 3, 0, db3.Length, ref db3Read);
            //result = s7MultiVar.Read();
            //if (result != 0)
            //{
            //    Console.WriteLine("Error on s7MultiVar.Read()" + client.ErrorText(result));
            //}
            //else
            //{
            //    for (int i = 0; i < 9; i++) Console.WriteLine(db3Read[i]);
            //}

            const int DB1_SIZE = 10;
            const int DB3_SIZE = 10;

            //-------------- Read db1
            Console.WriteLine("\n---- Read DB 1");

            byte[] db1Buffer = new byte[DB1_SIZE];
            result = client.DBRead(1, 0, DB1_SIZE, db1Buffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }
            int db1dbw2 = S7.GetIntAt(db1Buffer, 2);
            Console.WriteLine("DB1.DBW2: " + db1dbw2);

            double db1dbd4 = S7.GetRealAt(db1Buffer, 4);
            Console.WriteLine("DB1.DBD4: " + db1dbd4);

            //double db1dbd8 = S7.GetDIntAt(db1Buffer, 8);
            //Console.WriteLine("DB1.DBD8: " + db1dbd8);

            //double db1dbd12 = S7.GetDWordAt(db1Buffer, 12);
            //Console.WriteLine("DB1.DBD12: " + db1dbd12);

            //double db1dbw16 = S7.GetWordAt(db1Buffer, 16);
            //Console.WriteLine("DB1.DBD16: " + db1dbw16);

            //-------------- Read DB3
            Console.WriteLine("\n---- Read DB 3");

            byte[] db3Buffer = new byte[DB3_SIZE];
            result = client.DBRead(3, 0, DB3_SIZE, db3Buffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }
            int db3dbw2 = S7.GetIntAt(db3Buffer, 2);
            Console.WriteLine("DB3.DBW2: " + db3dbw2);

            double db3dbd4 = S7.GetRealAt(db3Buffer, 4);
            Console.WriteLine("DB3.DBD4: " + db3dbd4);

            //double db3dbd8 = S7.GetDIntAt(db3Buffer, 8);
            //Console.WriteLine("DB3.DBD8: " + db3dbd8);

            //uint db3dbd12 = S7.GetDWordAt(db3Buffer, 12);
            //Console.WriteLine("DB3.DBD12: " + db3dbd12);

            //ushort db3dbd16 = S7.GetWordAt(db3Buffer, 16);
            //Console.WriteLine("DB3.DBD16: " + db3dbd16);

            //-------------- Write Db1
            Console.WriteLine("\n---- Write BD 1");

            db1Buffer = new byte[DB3_SIZE];
            const int START_INDEX = 4;
            S7.SetRealAt(db1Buffer, 4 - START_INDEX, (float)54.36);
            //S7.SetDIntAt(db1Buffer, 8 - START_INDEX, 555666);
            //S7.SetDWordAt(db1Buffer, 12 - START_INDEX, 123456);
            result = client.DBWrite(1, 0, db1Buffer.Length, db1Buffer);
            if (result != 0)
            {
                Console.WriteLine("Error: " + client.ErrorText(result));
            }

            ////-------------- Read multi vars
            //var s7MultiVar = new S7MultiVar(client);
            //byte[] db1 = new byte[18];
            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 1, 0, db1.Length, ref db1);
            //byte[] db3 = new byte[18];
            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 3, 0, db3.Length, ref db3);
            //result = s7MultiVar.Read();
            //if (result != 0)
            //{
            //    Console.WriteLine("Error on s7MultiVar.Read()" + client.ErrorText(result));
            //}

            //db1dbw2 = S7.GetIntAt(db1, 2);
            //Console.WriteLine("DB1.DBW2.0 = {0}", db1dbw2);

            //db1dbd4 = S7.GetRealAt(db1, 4);
            //Console.WriteLine("DB1.DBW4.0 = {0}", db1dbd4);

            //db1dbd8 = S7.GetDIntAt(db1, 8);
            //Console.WriteLine("DB1.DBW8.0 = {0}", db1dbd8);

            //db3dbw2 = S7.GetIntAt(db3, 2);
            //Console.WriteLine("DB3.DBW2.0 = {0}", db3dbw2);

            //db3dbd4 = S7.GetRealAt(db3, 4);
            //Console.WriteLine("DB3.DBW4.0 = {0}", db3dbd4);

            //db3dbd8 = S7.GetDIntAt(db3, 8);
            //Console.WriteLine("DB3.DBW8.0 = {0}", db3dbd8);

            ////-------------- Write multi vars
            //s7MultiVar = new S7MultiVar(client);
            //const int DB1_START_INDEX = 2;
            //db1 = new byte[10];
            //S7.SetIntAt(db1, 2 - DB1_START_INDEX, 50);
            //S7.SetRealAt(db1, 4 - DB1_START_INDEX, (float)36.5);
            //S7.SetDIntAt(db1, 8 - DB1_START_INDEX, 123456);
            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 1, DB1_START_INDEX, db1.Length, ref db1);

            //const int DB3_START_INDEX = 2;
            //db3 = new byte[10];
            //S7.SetIntAt(db3, 2 - DB3_START_INDEX, -50);
            //S7.SetRealAt(db3, 4 - DB3_START_INDEX, (float)-25.36);
            //S7.SetDIntAt(db3, 8 - DB3_START_INDEX, -123456);
            //s7MultiVar.Add(S7Consts.S7AreaDB, S7Consts.S7WLByte, 3, DB3_START_INDEX, db3.Length, ref db3);
            //result = s7MultiVar.Write();
            //if (result != 0)
            //{
            //    Console.WriteLine("Error on s7MultiVar.Read()" + client.ErrorText(result));
            //}

            //-------------- Disconnect the client
            client.Disconnect();
            Console.ReadKey();
        }
    }
}
