namespace Sniffer
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.CompilerServices;

    public class SnifferSocket
    {
        private DataManager m_Data = null;
        private FilterManager m_Filter = null;
        private Hashtable m_SocketMap = null;

        public event IPv4DatagramCallback IPv4DatagramReceived;

        public event IPv4FragmentCallback IPv4FragmentReceived;

        public event SnifferErrorCallback SnifferError;

        public event ConnectionCallback TcpClose;

        public event ConnectionCallback TcpConnect;

        public event TcpPacketCallback TcpPacketReceived;

        public event UdpDatagramCallback UdpDatagramReceived;

        public SnifferSocket()
        {
            this.m_Data = new DataManager();
            this.m_Filter = new FilterManager();
            this.m_SocketMap = new Hashtable();
        }

        private void FireIPv4DatagramReceived(IPv4Datagram datagram)
        {
            if (this.FilterSet.IsWanted(datagram) && (this.IPv4DatagramReceived != null))
            {
                this.IPv4DatagramReceived(datagram);
            }
        }

        private void FireIPv4FragmentReceived(IPv4Fragment fragment)
        {
            if (this.FilterSet.IsWanted(fragment) && (this.IPv4FragmentReceived != null))
            {
                this.IPv4FragmentReceived(fragment);
            }
        }

        private void FireSnifferError(SnifferException e)
        {
            if (this.SnifferError != null)
            {
                this.SnifferError(e);
            }
        }

        private void FireTcpPacketReceived(TcpPacket packet)
        {
            if (this.FilterSet.IsWanted(packet))
            {
                if (this.TcpPacketReceived != null)
                {
                    this.TcpPacketReceived(packet);
                }
                if (packet.Syn && (this.TcpConnect != null))
                {
                    this.TcpConnect(packet.Source, packet.Destination);
                }
                if (packet.Fin && (this.TcpConnect != null))
                {
                    this.TcpClose(packet.Source, packet.Destination);
                }
            }
        }

        private void FireUdpDatagramReceived(UdpDatagram packet)
        {
            if (this.FilterSet.IsWanted(packet) && (this.UdpDatagramReceived != null))
            {
                this.UdpDatagramReceived(packet);
            }
        }

        private void HandleIPv4Datagram(byte[] buffer)
        {
            int identification = 0;
            int num2 = 0;
            uint addr = 0;
            uint num4 = 0;
            int offset = 0;
            IPv4Datagram datagram = null;
            IPv4Fragment fragment = new IPv4Fragment();
            fragment.MoreFlag = Sniffer.HeaderParser.ToByte(buffer, 50, 1) > 0;
            fragment.Offset = Sniffer.HeaderParser.ToInt(buffer, 0x33, 13) * 8;
            fragment.TTL = Sniffer.HeaderParser.ToInt(buffer, 0x40, 8);
            fragment.Length = Sniffer.HeaderParser.ToUShort(buffer, 0x10, 0x10);
            offset = Sniffer.HeaderParser.ToInt(buffer, 4, 4) * 4;
            fragment.SetData(buffer, offset, fragment.Length - offset);
            identification = Sniffer.HeaderParser.ToByte(buffer, 0x20, 0x10);
            num2 = Sniffer.HeaderParser.ToByte(buffer, 0x48, 8);
            addr = Sniffer.HeaderParser.ToUInt(buffer, 0x60, 0x20);
            num4 = Sniffer.HeaderParser.ToUInt(buffer, 0x80, 0x20);
            IPAddress source = IPAddress.Parse(IPv4Datagram.GetIPString(addr));
            IPAddress dest = IPAddress.Parse(IPv4Datagram.GetIPString(num4));
            datagram = this.m_Data.GetIPv4Datagram(identification, source, dest);
            if (datagram == null)
            {
                datagram = new IPv4Datagram();
                datagram.Identification = identification;
                datagram.Source = source;
                datagram.Destination = dest;
                datagram.Protocol = num2;
            }
            datagram.AddFragment(fragment);
            if (!datagram.Complete)
            {
                this.m_Data.AddIPv4Datagram(datagram);
                this.FireIPv4FragmentReceived(fragment);
            }
            else
            {
                this.FireIPv4DatagramReceived(datagram);
                switch (datagram.Protocol)
                {
                    case 6:
                        this.HandleTcpPacket(datagram.Data, datagram.Source, datagram.Destination);
                        break;

                    case 0x11:
                        this.HandleUdpDatagram(datagram.Data, datagram.Source, datagram.Destination);
                        break;
                }
                if (datagram.WasFragmented())
                {
                    this.m_Data.RemoveIPv4Datagram(datagram);
                }
            }
        }

        private void HandleTcpPacket(byte[] data, IPAddress source, IPAddress destination)
        {
            TcpPacket packet = new TcpPacket();
            int port = Sniffer.HeaderParser.ToInt(data, 0, 0x10);
            int num2 = Sniffer.HeaderParser.ToInt(data, 0x10, 0x10);
            int offset = Sniffer.HeaderParser.ToInt(data, 0x60, 4) * 4;
            packet.Source = new IPEndPoint(source, port);
            packet.Destination = new IPEndPoint(destination, num2);
            packet.Sequence = Sniffer.HeaderParser.ToUInt(data, 0x20, 0x20);
            packet.Acknowledgement = Sniffer.HeaderParser.ToUInt(data, 0x40, 0x20);
            packet.Urgent = Sniffer.HeaderParser.ToByte(data, 0x6a, 1) != 0;
            packet.Ack = Sniffer.HeaderParser.ToByte(data, 0x6b, 1) != 0;
            packet.Push = Sniffer.HeaderParser.ToByte(data, 0x6c, 1) != 0;
            packet.Reset = Sniffer.HeaderParser.ToByte(data, 0x6d, 1) != 0;
            packet.Syn = Sniffer.HeaderParser.ToByte(data, 110, 1) != 0;
            packet.Fin = Sniffer.HeaderParser.ToByte(data, 0x6f, 1) != 0;
            packet.SetData(data, offset, data.Length - offset);
            this.FireTcpPacketReceived(packet);
        }

        private void HandleUdpDatagram(byte[] data, IPAddress source, IPAddress destination)
        {
            UdpDatagram packet = new UdpDatagram();
            int port = Sniffer.HeaderParser.ToInt(data, 0, 0x10);
            int num2 = Sniffer.HeaderParser.ToInt(data, 0x10, 0x10);
            int length = Sniffer.HeaderParser.ToInt(data, 0x20, 0x10) - 8;
            packet.Source = new IPEndPoint(source, port);
            packet.Destination = new IPEndPoint(destination, num2);
            packet.SetData(data, 8, length);
            this.FireUdpDatagramReceived(packet);
        }

        private void ReceivePacket(IAsyncResult ar)
        {
            int num = 0;
            SocketPair asyncState = ar.AsyncState as SocketPair;
            Socket iPSocket = asyncState.IPSocket;
            int num2 = 0;
            try
            {
                num = iPSocket.EndReceive(ar);
            }
            catch (SocketException exception)
            {
                this.FireSnifferError(new SnifferException("Error Receiving Packet", exception));
            }
            num2 = Sniffer.HeaderParser.ToInt(asyncState.Buffer, 0, 4);
            try
            {
                switch (num2)
                {
                    case 4:
                    {
                        this.HandleIPv4Datagram(asyncState.Buffer);
                    }
                    break;
                }
            }
            catch (Exception exception2)
            {
                this.FireSnifferError(new SnifferException("Unknown Exception", exception2));
            }
            iPSocket.BeginReceive(asyncState.Buffer, 0, asyncState.Buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceivePacket), asyncState);
        }

        private void SetupSocket(Socket socket)
        {
            bool flag = true;
            SocketException e = null;
            try
            {
                socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AcceptConnection, 1);
                byte[] buffer3 = new byte[4];
                buffer3[0] = 1;
                byte[] optionInValue = buffer3;
                byte[] optionOutValue = new byte[4];
                int ioControlCode = -1744830463;
                int num2 = socket.IOControl(ioControlCode, optionInValue, optionOutValue);
                if ((((optionOutValue[0] + optionOutValue[1]) + optionOutValue[2]) + optionOutValue[3]) != 0)
                {
                    flag = false;
                }
            }
            catch (SocketException exception2)
            {
                e = exception2;
                flag = false;
            }
            finally
            {
                if (!flag)
                {
                    throw new SnifferException("Could not set the socket to receive all", e);
                }
            }
        }

        public void Sniff(string ip)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);
            byte[] buffer = new byte[0x800];
            SocketPair pair = new SocketPair(socket, buffer);
            socket.Blocking = false;
            socket.Bind(new IPEndPoint(IPAddress.Parse(ip), 0));
            this.SetupSocket(socket);
            if (this.m_SocketMap.Contains(ip))
            {
                throw new SnifferException("Socket already bound on that IP");
            }
            this.m_SocketMap.Add(ip, pair);
            try
            {
                socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(this.ReceivePacket), pair);
            }
            catch (Exception exception)
            {
                throw new SnifferException("Could not start the Receive", exception);
            }
        }

        public FilterManager FilterSet
        {
            get
            {
                return this.m_Filter;
            }
        }
    }
}

