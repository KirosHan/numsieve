namespace Sniffer
{
    using System;
    using System.Net;

    public class TcpPacket
    {
        private bool m_Ack = false;
        private uint m_Acknowledgement = 0;
        private byte[] m_Data = null;
        private int m_DataOffset = 0;
        private IPEndPoint m_Destination = null;
        private bool m_Fin = false;
        private int m_Length = 0;
        private bool m_Push = false;
        private bool m_Reset = false;
        private uint m_Sequence = 0;
        private IPEndPoint m_Source = null;
        private bool m_Syn = false;
        private bool m_Urgent = false;

        public string GetHashString()
        {
            string format = "Tcp:{0}:{1}:{2}:{3}";
            return string.Format(format, new object[] { this.SourceIP, this.SourcePort, this.DestinationIP, this.DestinationPort });
        }

        public void SetData(byte[] data, int offset, int length)
        {
            this.m_Data = new byte[length];
            Array.Copy(data, offset, this.m_Data, 0, length);
        }

        public bool Ack
        {
            get
            {
                return this.m_Ack;
            }
            set
            {
                this.m_Ack = value;
            }
        }

        public uint Acknowledgement
        {
            get
            {
                return this.m_Acknowledgement;
            }
            set
            {
                this.m_Acknowledgement = value;
            }
        }

        public byte[] Data
        {
            get
            {
                return this.m_Data;
            }
            set
            {
                this.m_Data = value;
            }
        }

        public int DataOffset
        {
            get
            {
                return this.m_DataOffset;
            }
            set
            {
                this.m_DataOffset = value;
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

        public bool Fin
        {
            get
            {
                return this.m_Fin;
            }
            set
            {
                this.m_Fin = value;
            }
        }

        public int Length
        {
            get
            {
                return this.m_Length;
            }
            set
            {
                this.m_Length = value;
            }
        }

        public bool Push
        {
            get
            {
                return this.m_Push;
            }
            set
            {
                this.m_Push = value;
            }
        }

        public bool Reset
        {
            get
            {
                return this.m_Reset;
            }
            set
            {
                this.m_Reset = value;
            }
        }

        public uint Sequence
        {
            get
            {
                return this.m_Sequence;
            }
            set
            {
                this.m_Sequence = value;
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

        public bool Syn
        {
            get
            {
                return this.m_Syn;
            }
            set
            {
                this.m_Syn = value;
            }
        }

        public bool Urgent
        {
            get
            {
                return this.m_Urgent;
            }
            set
            {
                this.m_Urgent = value;
            }
        }
    }
}

