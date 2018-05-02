namespace Sniffer
{
    using System;

    public class IPv4Fragment
    {
        private byte[] m_Data = null;
        private int m_Length = 0;
        private bool m_MoreFlag = false;
        private IPv4Fragment m_Next = null;
        private int m_Offset = 0;
        private int m_TTL = 0;

        public void SetData(byte[] Data, int offset, int length)
        {
            this.m_Data = new byte[length];
            this.m_Length = length;
            Array.Copy(Data, offset, this.m_Data, 0, length);
        }

        public byte[] Data
        {
            get
            {
                return this.m_Data;
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

        public bool MoreFlag
        {
            get
            {
                return this.m_MoreFlag;
            }
            set
            {
                this.m_MoreFlag = value;
            }
        }

        public IPv4Fragment Next
        {
            get
            {
                return this.m_Next;
            }
            set
            {
                this.m_Next = value;
            }
        }

        public int Offset
        {
            get
            {
                return this.m_Offset;
            }
            set
            {
                this.m_Offset = value;
            }
        }

        public int TTL
        {
            get
            {
                return this.m_TTL;
            }
            set
            {
                this.m_TTL = value;
            }
        }
    }
}

