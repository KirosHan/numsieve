namespace Sniffer.Filters
{
    using Sniffer;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;

    internal class ConfigFilter : IAllowFilter, IDenyFilter
    {
        private string m_DestinationIP;
        private int m_DestinationPort;
        private ProtocolType m_Protocol;
        private string m_SourceIP;
        private int m_SourcePort;
        private FilterType m_Type;

        internal ConfigFilter(FilterType type, ProtocolType protocol)
        {
            this.m_SourceIP = null;
            this.m_DestinationIP = null;
            this.m_SourcePort = -1;
            this.m_DestinationPort = -1;
            this.Type = type;
            this.Protocol = protocol;
        }

        internal ConfigFilter(FilterType type, ProtocolType protocol, IPAddress source, IPAddress dest)
        {
            this.m_SourceIP = null;
            this.m_DestinationIP = null;
            this.m_SourcePort = -1;
            this.m_DestinationPort = -1;
            if (source != null)
            {
                this.SourceIP = source.ToString();
            }
            if (dest != null)
            {
                this.DestinationIP = dest.ToString();
            }
            this.Type = type;
            this.Protocol = protocol;
        }

        internal ConfigFilter(FilterType type, ProtocolType protocol, IPEndPoint source, IPEndPoint dest)
        {
            this.m_SourceIP = null;
            this.m_DestinationIP = null;
            this.m_SourcePort = -1;
            this.m_DestinationPort = -1;
            if (source != null)
            {
                this.SourceIP = source.Address.ToString();
                this.SourcePort = source.Port;
            }
            if (dest != null)
            {
                this.DestinationIP = dest.Address.ToString();
                this.DestinationPort = dest.Port;
            }
            this.Type = type;
            this.Protocol = protocol;
        }

        internal ConfigFilter(FilterType type, ProtocolType protocol, string source_addr, int source_port, string dest_addr, int dest_port)
        {
            this.m_SourceIP = null;
            this.m_DestinationIP = null;
            this.m_SourcePort = -1;
            this.m_DestinationPort = -1;
            try
            {
                if (source_addr != null)
                {
                    IPAddress.Parse(source_addr);
                    this.SourceIP = source_addr;
                }
                if (source_port >= 0)
                {
                    this.SourcePort = source_port;
                }
                if (dest_addr != null)
                {
                    IPAddress.Parse(dest_addr);
                    this.DestinationIP = dest_addr;
                }
                if (dest_port >= 0)
                {
                    this.DestinationPort = dest_port;
                }
            }
            catch (Exception exception)
            {
                throw new SnifferException("Could not create filter.  Had a inavlid value", exception);
            }
            this.Type = type;
            this.Protocol = protocol;
        }

        public bool AllowIPv4Datagram(IPv4Datagram datagram)
        {
            return (((this.Type == FilterType.Allow) && (this.Protocol == ProtocolType.IP)) && this.PacketMatch(datagram.SourceIP, -1, datagram.DestinationIP, -1));
        }

        public bool AllowIPv4Fragment(IPv4Fragment fragment)
        {
            return false;
        }

        public bool AllowTcpPacket(TcpPacket packet)
        {
            return ((this.Type == FilterType.Allow) && ((this.Protocol == ProtocolType.Tcp) && this.PacketMatch(packet.SourceIP, packet.SourcePort, packet.DestinationIP, packet.DestinationPort)));
        }

        public bool AllowUdpPacket(UdpDatagram packet)
        {
            return ((this.Type == FilterType.Allow) && ((this.Protocol == ProtocolType.Udp) && this.PacketMatch(packet.SourceIP, packet.SourcePort, packet.DestinationIP, packet.DestinationPort)));
        }

        public bool DenyIPv4Datagram(IPv4Datagram datagram)
        {
            return (((this.Type == FilterType.Deny) && (this.Protocol == ProtocolType.IP)) && this.PacketMatch(datagram.SourceIP, -1, datagram.DestinationIP, -1));
        }

        public bool DenyIPv4Fragment(IPv4Fragment fragment)
        {
            return false;
        }

        public bool DenyTcpPacket(TcpPacket packet)
        {
            return ((this.Type == FilterType.Deny) && ((this.Protocol == ProtocolType.Tcp) && this.PacketMatch(packet.SourceIP, packet.SourcePort, packet.DestinationIP, packet.DestinationPort)));
        }

        public bool DenyUdpPacket(UdpDatagram packet)
        {
            return ((this.Type == FilterType.Deny) && ((this.Protocol == ProtocolType.Udp) && this.PacketMatch(packet.SourceIP, packet.SourcePort, packet.DestinationIP, packet.DestinationPort)));
        }

        private bool PacketMatch(string source_ip, int source_port, string dest_ip, int dest_port)
        {
            bool flag = true;
            if ((this.SourceIP != null) && (this.SourceIP != source_ip))
            {
                flag = false;
            }
            if ((this.SourcePort >= 0) && (this.SourcePort != source_port))
            {
                flag = false;
            }
            if ((this.DestinationIP != null) && (this.DestinationIP != dest_ip))
            {
                flag = false;
            }
            if ((this.DestinationPort >= 0) && (this.DestinationPort != dest_port))
            {
                flag = false;
            }
            return flag;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder(this.Protocol.ToString());
            builder.Append(" Source IP: ");
            if (this.SourceIP != null)
            {
                builder.Append(this.SourceIP);
            }
            else
            {
                builder.Append("*");
            }
            if ((this.Protocol == ProtocolType.Tcp) || (ProtocolType.Udp == this.Protocol))
            {
                builder.Append(" Port: ");
                if (this.SourcePort >= 0)
                {
                    builder.Append(this.SourcePort);
                }
                else
                {
                    builder.Append("*");
                }
            }
            builder.Append(" Destination IP: ");
            if (this.DestinationIP != null)
            {
                builder.Append(this.DestinationIP);
            }
            else
            {
                builder.Append("*");
            }
            if ((this.Protocol == ProtocolType.Tcp) || (ProtocolType.Udp == this.Protocol))
            {
                builder.Append(" Port: ");
                if (this.DestinationPort >= 0)
                {
                    builder.Append(this.DestinationPort);
                }
                else
                {
                    builder.Append("*");
                }
            }
            builder.Append(" ");
            builder.Append(this.Type);
            return builder.ToString();
        }

        public string DestinationIP
        {
            get
            {
                return this.m_DestinationIP;
            }
            set
            {
                this.m_DestinationIP = value;
            }
        }

        public int DestinationPort
        {
            get
            {
                return this.m_DestinationPort;
            }
            set
            {
                this.m_DestinationPort = value;
            }
        }

        internal ProtocolType Protocol
        {
            get
            {
                return this.m_Protocol;
            }
            set
            {
                this.m_Protocol = value;
            }
        }

        public string SourceIP
        {
            get
            {
                return this.m_SourceIP;
            }
            set
            {
                this.m_SourceIP = value;
            }
        }

        public int SourcePort
        {
            get
            {
                return this.m_SourcePort;
            }
            set
            {
                this.m_SourcePort = value;
            }
        }

        internal FilterType Type
        {
            get
            {
                return this.m_Type;
            }
            set
            {
                this.m_Type = value;
            }
        }
    }
}

