namespace Sniffer
{
    using System;
    using System.Collections;

    public class FilterManager
    {
        private ArrayList m_AllowList = null;
        private ArrayList m_DenyList = null;

        public FilterManager()
        {
            this.m_AllowList = new ArrayList();
            this.m_DenyList = new ArrayList();
        }

        public void AddAllowFilter(IAllowFilter filter)
        {
            this.m_AllowList.Add(filter);
        }

        public void AddDenyFilter(IDenyFilter filter)
        {
            this.m_DenyList.Add(filter);
        }

        public void ClearAllowFilters()
        {
            this.m_AllowList.Clear();
        }

        public void ClearDenyFilters()
        {
            this.m_DenyList.Clear();
        }

        public bool IsWanted(IPv4Datagram datagram)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            foreach (IAllowFilter filter in this.m_AllowList)
            {
                if (filter.AllowIPv4Datagram(datagram))
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                return true;
            }
            foreach (IDenyFilter filter2 in this.m_DenyList)
            {
                if (filter2.DenyIPv4Datagram(datagram))
                {
                    flag3 = true;
                    break;
                }
            }
            if (!flag3)
            {
                flag = true;
            }
            return flag;
        }

        public bool IsWanted(IPv4Fragment fragment)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            foreach (IAllowFilter filter in this.m_AllowList)
            {
                if (filter.AllowIPv4Fragment(fragment))
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                return true;
            }
            foreach (IDenyFilter filter2 in this.m_DenyList)
            {
                if (filter2.DenyIPv4Fragment(fragment))
                {
                    flag3 = true;
                    break;
                }
            }
            if (!flag3)
            {
                flag = true;
            }
            return flag;
        }

        public bool IsWanted(TcpPacket packet)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            foreach (IAllowFilter filter in this.m_AllowList)
            {
                if (filter.AllowTcpPacket(packet))
                {
                    flag = true;
                    break;
                }
            }
            if (flag)
            {
                return true;
            }
            foreach (IDenyFilter filter2 in this.m_DenyList)
            {
                if (filter2.DenyTcpPacket(packet))
                {
                    flag2 = true;
                    break;
                }
            }
            if (!flag2)
            {
                flag3 = true;
            }
            return flag3;
        }

        public bool IsWanted(UdpDatagram packet)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            foreach (IAllowFilter filter in this.m_AllowList)
            {
                if (filter.AllowUdpPacket(packet))
                {
                    flag2 = true;
                    break;
                }
            }
            if (flag2)
            {
                return true;
            }
            foreach (IDenyFilter filter2 in this.m_DenyList)
            {
                if (filter2.DenyUdpPacket(packet))
                {
                    flag3 = true;
                    break;
                }
            }
            if (!flag3)
            {
                flag = true;
            }
            return flag;
        }

        public void RemoveAllowFilter(IAllowFilter filter)
        {
            int index = -1;
            for (int i = 0; i < this.m_AllowList.Count; i++)
            {
                IAllowFilter objA = (IAllowFilter) this.m_AllowList[i];
                if (object.ReferenceEquals(objA, filter))
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                this.m_AllowList.RemoveAt(index);
            }
        }

        public void RemoveDenyFilter(IDenyFilter filter)
        {
            int index = -1;
            for (int i = 0; i < this.m_DenyList.Count; i++)
            {
                IDenyFilter objA = (IDenyFilter) this.m_DenyList[i];
                if (object.ReferenceEquals(objA, filter))
                {
                    index = i;
                    break;
                }
            }
            if (index >= 0)
            {
                this.m_DenyList.RemoveAt(index);
            }
        }

        public ArrayList AllowFilters
        {
            get
            {
                return this.m_AllowList;
            }
        }

        public ArrayList DenyFilters
        {
            get
            {
                return this.m_DenyList;
            }
        }
    }
}

