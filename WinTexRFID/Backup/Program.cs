using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
 
 
 

namespace UHFAPP
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
              // Test();
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public static void Test()
        {
            int len;
            int iRes;
            byte[] sendMsg = new byte[1024];
            byte[] recvMsg = new byte[1024 * 8];
           

            Program.SDK_UHF.OnTagReceivedCallDelegate callback = new SDK_UHF.OnTagReceivedCallDelegate(OnTagReceivedCall);
            SDK_UHF.ZHX_OCX_Init(callback);

            iRes = SDK_UHF.ZHX_OCX_GetConnect(recvMsg);
            Console.WriteLine("recv message:" + recvMsg);

            //open
             sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{\"tranTypeId\":\"SDVRFIDCO001\"}\"");
            iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg);
            Console.WriteLine("open: recvMsg:" + System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100));

            //get version
            sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{\"tranTypeId\":\"SDVRFIDCO003\"}\"");
            iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg);
            Console.WriteLine("get: version:" + System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100));

            //inventory
            sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{\"tranTypeId\":\"SDVRFIDCO014\"}\"");
            iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg);
            Console.WriteLine("inventory:" + System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100));

            int start = Environment.TickCount;
            do
            {
                Thread.Sleep(50);
            } while (Environment.TickCount - start < 5000);


            sendMsg = System.Text.ASCIIEncoding.ASCII.GetBytes("{\"tranTypeId\":\"SDVRFIDCO015\"}\"");
            iRes = SDK_UHF.ZHX_OCX_ExeMessage(sendMsg, recvMsg);
            Console.WriteLine("stop inventory:" + System.Text.ASCIIEncoding.ASCII.GetString(recvMsg, 0, 100));
            SDK_UHF.ZHX_OCX_Free();

        }
        public static void OnTagReceivedCall(byte[] epc)
        {
            Console.WriteLine("epc:回调数据");
        }

        public class SDK_UHF
        {
    
           [DllImport("UHFAPI_back.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int CryptoTransmit(byte[] pin, int inLen, byte[] pout, int[] outLen, int wait_recv_ms);
 
            [DllImport("UHFAPI_back.dll", CallingConvention = CallingConvention.Cdecl)]
           public static extern void ZHX_OCX_Init(OnTagReceivedCallDelegate call);

            [DllImport("UHFAPI_back.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern void ZHX_OCX_Free();

            [DllImport("UHFAPI_back.dll", CallingConvention = CallingConvention.Cdecl)]
             public static extern int ZHX_OCX_GetConnect(byte[] msgOut);

            [DllImport("UHFAPI_back.dll", CallingConvention = CallingConvention.Cdecl)]
            public static extern int  ZHX_OCX_ExeMessage(byte[] msgIn, byte[] msgOut);

            public delegate void OnTagReceivedCallDelegate(byte[] epc);
    
 

        }
    }
}
