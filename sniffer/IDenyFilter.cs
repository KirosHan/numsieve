namespace Sniffer
{
    using System;

    public interface IDenyFilter
    {
        bool DenyIPv4Datagram(IPv4Datagram datagram);
        bool DenyIPv4Fragment(IPv4Fragment fragment);
        bool DenyTcpPacket(TcpPacket packet);
        bool DenyUdpPacket(UdpDatagram packet);
    }
}

