namespace Sniffer.Filters
{
    using Sniffer;
    using System;

    internal class DenyFilter : IDenyFilter
    {
        internal DenyFilter()
        {
        }

        public bool DenyIPv4Datagram(IPv4Datagram datagram)
        {
            return true;
        }

        public bool DenyIPv4Fragment(IPv4Fragment fragment)
        {
            return true;
        }

        public bool DenyTcpPacket(TcpPacket packet)
        {
            return true;
        }

        public bool DenyUdpPacket(UdpDatagram packet)
        {
            return true;
        }

        public override string ToString()
        {
            return "Deny all data";
        }
    }
}

