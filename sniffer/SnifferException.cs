namespace Sniffer
{
    using System;

    public class SnifferException : Exception
    {
        public SnifferException(string msg) : this(msg, null)
        {
        }

        public SnifferException(string msg, Exception e) : base(msg, e)
        {
        }
    }
}

