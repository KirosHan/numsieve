namespace Sniffer
{
    using Sniffer.Filters;
    using System;
    using System.Net;
    using System.Net.Sockets;

    public class FilterFactory
    {
        private static AllowFilter m_Allow = null;
        private static DenyFilter m_Deny = null;

        public static IAllowFilter CreateAllowFilter()
        {
            lock (typeof(FilterFactory))
            {
                if (m_Allow == null)
                {
                    m_Allow = new AllowFilter();
                }
            }
            return m_Allow;
        }

        public static IDenyFilter CreateDenyFilter()
        {
            lock (typeof(FilterFactory))
            {
                if (m_Deny == null)
                {
                    m_Deny = new DenyFilter();
                }
            }
            return m_Deny;
        }

        public static IAllowFilter CreateIPv4AllowFilter()
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.IP);
        }

        public static IAllowFilter CreateIPv4AllowFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.IP, source, dest);
        }

        public static IAllowFilter CreateIPv4AllowFilter(string source_addr, string dest_addr)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.IP, source_addr, -1, dest_addr, -1);
        }

        public static IDenyFilter CreateIPv4DenyFilter()
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.IP);
        }

        public static IDenyFilter CreateIPv4DenyFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.IP, source, dest);
        }

        public static IDenyFilter CreateIPv4DenyFilter(string source_addr, string dest_addr)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.IP, source_addr, -1, dest_addr, -1);
        }

        public static IAllowFilter CreateTcpAllowFilter()
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Tcp);
        }

        public static IAllowFilter CreateTcpAllowFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Tcp, source, dest);
        }

        public static IAllowFilter CreateTcpAllowFilter(IPEndPoint source, IPEndPoint dest)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Tcp, source, dest);
        }

        public static IAllowFilter CreateTcpAllowFilter(string source_addr, int source_port, string dest_addr, int dest_port)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Tcp, source_addr, source_port, dest_addr, dest_port);
        }

        public static IDenyFilter CreateTcpDenyFilter()
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Tcp);
        }

        public static IDenyFilter CreateTcpDenyFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Tcp, source, dest);
        }

        public static IDenyFilter CreateTcpDenyFilter(IPEndPoint source, IPEndPoint dest)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Tcp, source, dest);
        }

        public static IDenyFilter CreateTcpDenyFilter(string source_addr, int source_port, string dest_addr, int dest_port)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Tcp, source_addr, source_port, dest_addr, dest_port);
        }

        public static IAllowFilter CreateUdpAllowFilter()
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Udp);
        }

        public static IAllowFilter CreateUdpAllowFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Udp, source, dest);
        }

        public static IAllowFilter CreateUdpAllowFilter(IPEndPoint source, IPEndPoint dest)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Udp, source, dest);
        }

        public static IAllowFilter CreateUdpAllowFilter(string source_addr, int source_port, string dest_addr, int dest_port)
        {
            return new ConfigFilter(FilterType.Allow, ProtocolType.Udp, source_addr, source_port, dest_addr, dest_port);
        }

        public static IDenyFilter CreateUdpDenyFilter()
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Udp);
        }

        public static IDenyFilter CreateUdpDenyFilter(IPAddress source, IPAddress dest)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Udp, source, dest);
        }

        public static IDenyFilter CreateUdpDenyFilter(IPEndPoint source, IPEndPoint dest)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Udp, source, dest);
        }

        public static IDenyFilter CreateUdpDenyFilter(string source_addr, int source_port, string dest_addr, int dest_port)
        {
            return new ConfigFilter(FilterType.Deny, ProtocolType.Udp, source_addr, source_port, dest_addr, dest_port);
        }
    }
}

