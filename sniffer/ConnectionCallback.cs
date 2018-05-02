namespace Sniffer
{
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;

    public delegate void ConnectionCallback(IPEndPoint source, IPEndPoint destination);
}

