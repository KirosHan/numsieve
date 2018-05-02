namespace Sniffer
{
    using System;
    using System.Net.Sockets;

    public class SocketPair
    {
        private byte[] m_buffer = null;
        private Socket m_socket = null;

        public SocketPair(Socket socket, byte[] buffer)
        {
            this.IPSocket = socket;
            this.Buffer = buffer;
        }

        public byte[] Buffer
        {
            get
            {
                return this.m_buffer;
            }
            set
            {
                this.m_buffer = value;
            }
        }

        public Socket IPSocket
        {
            get
            {
                return this.m_socket;
            }
            set
            {
                this.m_socket = value;
            }
        }
    }
}

