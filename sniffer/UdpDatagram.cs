namespace Sniffer
{
    using System;
    using System.Net;

    public class UdpDatagram
    {
        private byte[] m_Data = null;
        private IPEndPoint m_Destination = null;
        private IPEndPoint m_Source = null;

        public string GetHashString()
        {
            string format = "Udp:{0}:{1}:{2}:{3}";
            string.Format(format, new object[] { this.SourceIP, this.SourcePort, this.DestinationIP, this.DestinationPort });
            return format;
        }

        public void SetData(byte[] data, int offset, int length)
        {
            this.m_Data = new byte[length];
            Array.Copy(data, offset, this.m_Data, 0, length);
        }

        public byte[] Data
        {
            get
            {
                return this.m_Data;
            }
        }

        public IPEndPoint Destination
        {
            get
            {
                return this.m_Destination;
            }
            set
            {
                this.m_Destination = value;
            }
        }

        public string DestinationIP
        {
            get
            {
                return this.Destination.Address.ToString();
            }
        }

        public int DestinationPort
        {
            get
            {
                return this.m_Destination.Port;
            }
        }

        public IPEndPoint Source
        {
            get
            {
                return this.m_Source;
            }
            set
            {
                this.m_Source = value;
            }
        }

        public string SourceIP
        {
            get
            {
                return this.Source.Address.ToString();
            }
        }

        public int SourcePort
        {
            get
            {
                return this.m_Source.Port;
            }
        }
    }
}

