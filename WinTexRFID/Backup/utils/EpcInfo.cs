using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFAPP.utils
{
    public class EpcInfo
    {
        public EpcInfo(string epc, int count, byte[] epcBytes, byte[] tidBytes)
        {
            this.epc = epc;
            this.count = count;
            this.epcBytes=epcBytes;
            this.epcBytes = epcBytes;
            if (epcBytes != null && epcBytes.Length > 0 && tidBytes != null && tidBytes.Length > 0)
            {
                epcAndTidBytes = new byte[epcBytes.Length + tidBytes.Length];
                for (int k = 0; k < epcBytes.Length;k++ )
                {
                    epcAndTidBytes[k]=epcBytes[k];
                }
                for (int k = 0; k < tidBytes.Length; k++)
                {
                    epcAndTidBytes[k + epcBytes.Length] = tidBytes[k];
                }
            }
            else if (epcBytes != null && epcBytes.Length > 0)
            {
                epcAndTidBytes = epcBytes;
            }
            else if (tidBytes != null && tidBytes.Length > 0)
            {
                epcAndTidBytes = tidBytes;
            }
            else
            {
              epcAndTidBytes=new byte[0];
            }
        }
        string epc;

        public string Epc
        {
            get { return epc; }
            set { epc = value; }
        }

        int count;

        public int Count
        {
            get { return count; }
            set { count = value; }
        }

        byte[] epcBytes;

        public byte[] EpcBytes
        {
            get { return epcBytes; }
            set { epcBytes = value; }
        }

        byte[] tidBytes;

        public byte[] TidBytes
        {
            get { return tidBytes; }
            set { tidBytes = value; }
        }


        byte[] epcAndTidBytes;

        public byte[] EpcAndTidBytes
        {
            get { return epcAndTidBytes; }
            set { epcAndTidBytes = value; }
        }
       
    }
}
