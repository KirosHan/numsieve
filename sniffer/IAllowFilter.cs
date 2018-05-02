namespace Sniffer
{
    using System;

    public interface IAllowFilter
    {
        bool AllowIPv4Datagram(IPv4Datagram datagram);
        bool AllowIPv4Fragment(IPv4Fragment fragment);
        bool AllowTcpPacket(TcpPacket packet);
        bool AllowUdpPacket(UdpDatagram packet);
    }
}

