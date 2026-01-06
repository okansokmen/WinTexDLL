using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.EnterpriseServices ;
using System.Reflection;

namespace TeraziLibrary
{
 

    public interface iInterface
    {
    } 

    [EventTrackingEnabled(true)] 
    [Description("Serviced Component")] 

    public class Class1 : ServicedComponent, iInterface 
    {
        private SerialPort comport = new SerialPort();
        private string xtext;


        public string Terazi (string Comport,string Hiz,string Parity,string Databit,string Stopbit,string Gonderi)
        {
           
            xtext = "";
          
        comport.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
            comport.BaudRate = Convert.ToInt32(Hiz);
            comport.DataBits = Convert.ToInt16(Databit);

         if (Stopbit == "1") comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
         if (Stopbit == "0") comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "None");
         if (Stopbit == "2") comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "Two");
         if (Stopbit == "1.5") comport.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "OnePointFive");
        
        
         if (Parity == "n")  comport.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
         if (Parity == "o") comport.Parity = (Parity)Enum.Parse(typeof(Parity), "Odd");
         if (Parity == "e") comport.Parity = (Parity)Enum.Parse(typeof(Parity), "Even");
         if (Parity == "m") comport.Parity = (Parity)Enum.Parse(typeof(Parity), "Mark");
         if (Parity == "s") comport.Parity = (Parity)Enum.Parse(typeof(Parity), "Space");
          comport.PortName = Comport;
        comport.ReadBufferSize = 4096;
        if (!comport.IsOpen) comport.Open();
        byte[] data = HexStringToByteArray(Gonderi);
            //"4B4559303136800D0A");
        comport.Write(data, 0, data.Length);
        while (comport.IsOpen)
        {
            
        }

        return xtext;
	
        }
        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (!comport.IsOpen) return;

               string bak = comport.ReadExisting();
             
                
                xtext += bak;
                // close the stream
                if (bak.Contains("PRINT"))
                {
                comport.Close();
                } 

            
        }
        private string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
        private byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }


    }
}
