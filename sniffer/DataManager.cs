/************************************************************************************
 *源码来自(CSkin论坛)  bbs.CSkin.net
 *如果对该源码有问题可以直接点击下方的提问按钮进行提问哦
 *站长将亲自帮你解决问题
 *CSkin论坛-找到你需要的C#源码，交流和学习
************************************************************************************/
namespace Sniffer
{
    using System;
    using System.Collections;
    using System.Net;

    public class DataManager
    {
        private Hashtable m_IPv4Table = null;

        public DataManager()
        {
            this.m_IPv4Table = new Hashtable();
        }

        public void AddIPv4Datagram(IPv4Datagram datagram)
        {
            this.m_IPv4Table.Add(datagram.GetHashString(), datagram);
        }

        public IPv4Datagram GetIPv4Datagram(int identification, IPAddress source, IPAddress dest)
        {
            string format = "{0}:{1}:{2}";
            //string key = string.Format(format, identification,source.Address.ToString(), dest.Address.ToString());原来的
            string key = string.Format(format, identification,source.ToString(), dest.ToString());
            if (this.m_IPv4Table.Contains(key))
            {
                return (IPv4Datagram) this.m_IPv4Table[key];
            }
            return null;
        }

        public void RemoveIPv4Datagram(IPv4Datagram datagram)
        {
            this.m_IPv4Table.Remove(datagram.GetHashString());
        }
    }
}

