/************************************************************************************
 *Դ������(CSkin��̳)  bbs.CSkin.net
 *����Ը�Դ�����������ֱ�ӵ���·������ʰ�ť��������Ŷ
 *վ�������԰���������
 *CSkin��̳-�ҵ�����Ҫ��C#Դ�룬������ѧϰ
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
            //string key = string.Format(format, identification,source.Address.ToString(), dest.Address.ToString());ԭ����
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

