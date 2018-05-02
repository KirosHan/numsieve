namespace Sniffer.Filters
{
    using Sniffer;
    using System;

    internal class AllowFilter : IAllowFilter
    {
        internal AllowFilter()
        {
        }

        public bool AllowIPv4Datagram(IPv4Datagram datagram)
        {
            return true;
        }

        public bool AllowIPv4Fragment(IPv4Fragment fragment)
        {
            return true;
        }

        public bool AllowTcpPacket(TcpPacket packet)
        {
            return true;
        }

        public bool AllowUdpPacket(UdpDatagram packet)
        {
            return true;
        }

        public override string ToString()
        {
            return "Allow all data";
        }
    }
}

