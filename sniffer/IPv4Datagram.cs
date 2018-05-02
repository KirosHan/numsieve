namespace Sniffer
{
    using System;
    using System.Net;

    public class IPv4Datagram
    {
        private bool m_Complete = false;
        private byte[] m_Data = null;
        private IPAddress m_Destination = null;
        private IPv4Fragment m_FragmentHead = null;
        private int m_Identification = 0;
        private int m_Length = 0;
        private int m_Protocol = 0;
        private IPAddress m_Source = null;
        private int m_TypeOfService = 0;
        private string m_UpperProtocol = null;

        public void AddFragment(IPv4Fragment fragment)
        {
            if (this.m_FragmentHead == null)
            {
                this.m_FragmentHead = fragment;
            }
            else if (fragment.Offset < this.m_FragmentHead.Offset)
            {
                fragment.Next = this.m_FragmentHead;
                this.m_FragmentHead = fragment;
            }
            else
            {
                IPv4Fragment fragmentHead = this.m_FragmentHead;
                IPv4Fragment next = fragmentHead.Next;
                while ((fragmentHead != null) && (fragment.Offset > fragmentHead.Offset))
                {
                    next = fragmentHead;
                    fragmentHead = fragmentHead.Next;
                }
                next.Next = fragment;
                fragment.Next = fragmentHead;
            }
            this.TestComplete();
        }

        private void CombineData()
        {
            IPv4Fragment fragment;
            int num = 0;
            int destinationIndex = 0;
            for (fragment = this.m_FragmentHead; fragment != null; fragment = fragment.Next)
            {
                num += fragment.Length;
            }
            this.m_Data = new byte[num];
            this.m_Length = num;
            for (fragment = this.m_FragmentHead; fragment != null; fragment = fragment.Next)
            {
                Array.Copy(fragment.Data, 0, this.m_Data, destinationIndex, fragment.Length);
                destinationIndex += fragment.Length;
            }
        }

        public string GetHashString()
        {
            string format = "IPv4:{0}:{1}";
            return string.Format(format, this.SourceIP, this.DestinationIP);
        }

        public static string GetIPString(uint addr)
        {
            uint num = addr >> 0x18;
            uint num2 = (addr >> 0x10) & 0xff;
            uint num3 = (addr >> 8) & 0xff;
            uint num4 = addr & 0xff;
            string format = "{0}.{1}.{2}.{3}";
            return string.Format(format, new object[] { num, num2, num3, num4 });
        }

        public string GetUpperProtocol()
        {
            return this.m_UpperProtocol;
        }

        private void SetUpperProtocol(int protocol)
        {
            this.m_Protocol = protocol;
            switch (this.Protocol)
            {
                case 1:
                    this.m_UpperProtocol = "ICMP";
                    return;

                case 3:
                    this.m_UpperProtocol = "GW To GW";
                    return;

                case 4:
                    this.m_UpperProtocol = "CMCC";
                    return;

                case 5:
                    this.m_UpperProtocol = "ST";
                    return;

                case 6:
                    this.m_UpperProtocol = "Tcp";
                    return;

                case 7:
                    this.m_UpperProtocol = "Ucl";
                    return;

                case 8:
                    this.m_UpperProtocol = "7";
                    return;

                case 9:
                    this.m_UpperProtocol = "Secure";
                    return;

                case 10:
                    this.m_UpperProtocol = "BBN";
                    return;

                case 11:
                    this.m_UpperProtocol = "NVP";
                    return;

                case 12:
                    this.m_UpperProtocol = "PUP";
                    return;

                case 13:
                    this.m_UpperProtocol = "Pluribus";
                    return;

                case 14:
                    this.m_UpperProtocol = "Telenet";
                    return;

                case 15:
                    this.m_UpperProtocol = "XNET";
                    return;

                case 0x10:
                    this.m_UpperProtocol = "Chaos";
                    return;

                case 0x11:
                    this.m_UpperProtocol = "Udp";
                    return;

                case 0x12:
                    this.m_UpperProtocol = "Multiplexing";
                    return;

                case 0x13:
                    this.m_UpperProtocol = "DCN";
                    return;

                case 20:
                    this.m_UpperProtocol = "TAC Monitoring";
                    return;

                case 0x3f:
                    this.m_UpperProtocol = "Any local network";
                    return;

                case 0x40:
                    this.m_UpperProtocol = "SATNET";
                    return;

                case 0x41:
                    this.m_UpperProtocol = "MIT Subnet Support";
                    return;

                case 0x45:
                    this.m_UpperProtocol = "SATNET Monitoring";
                    return;

                case 0x47:
                    this.m_UpperProtocol = "Internet packet core utility";
                    return;

                case 0x4c:
                    this.m_UpperProtocol = "Backroom SATNET";
                    return;

                case 0x4e:
                    this.m_UpperProtocol = "WIDEBAND Monitoring";
                    return;

                case 0x4f:
                    this.m_UpperProtocol = "WIDEBAND EXPAK";
                    return;
            }
            this.m_UpperProtocol = this.Protocol.ToString();
        }

        private void TestComplete()
        {
            bool flag = false;
            IPv4Fragment fragmentHead = this.m_FragmentHead;
            int num = 0;
            while (fragmentHead != null)
            {
                if (fragmentHead.Next != null)
                {
                    if (fragmentHead.Offset == num)
                    {
                        fragmentHead = fragmentHead.Next;
                        num += fragmentHead.Length;
                    }
                    else
                    {
                        fragmentHead = null;
                    }
                }
                else if ((fragmentHead.Offset == num) && !fragmentHead.MoreFlag)
                {
                    fragmentHead = null;
                    flag = true;
                }
                else
                {
                    fragmentHead = null;
                }
            }
            if (flag)
            {
                this.CombineData();
            }
            this.m_Complete = flag;
        }

        public bool WasFragmented()
        {
            return (this.FragmentList.Next != null);
        }

        public bool Complete
        {
            get
            {
                return this.m_Complete;
            }
        }

        public byte[] Data
        {
            get
            {
                return this.m_Data;
            }
        }

        public IPAddress Destination
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
                return this.Destination.ToString();
            }
        }

        public IPv4Fragment FragmentList
        {
            get
            {
                return this.m_FragmentHead;
            }
        }

        public int Identification
        {
            get
            {
                return this.m_Identification;
            }
            set
            {
                this.m_Identification = value;
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

        public int Protocol
        {
            get
            {
                return this.m_Protocol;
            }
            set
            {
                this.SetUpperProtocol(value);
            }
        }

        public IPAddress Source
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
                return this.Source.ToString();
            }
        }

        public int TypeOfService
        {
            get
            {
                return this.m_TypeOfService;
            }
            set
            {
                this.m_TypeOfService = value;
            }
        }
    }
}

