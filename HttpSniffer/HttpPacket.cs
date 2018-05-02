/************************************************************************************
 *源码来自(CSkin论坛)  bbs.CSkin.net
 *如果对该源码有问题可以直接点击下方的提问按钮进行提问哦
 *站长将亲自帮你解决问题
 *CSkin论坛-找到你需要的C#源码，交流和学习
************************************************************************************/
namespace HttpSniffer
{
    using System;

    public class HttpPacket
    {
        private string m_Host = "";
        private string m_Method = "";
        private string m_UserAgent = "";
        private string m_Cookies = "";
        private string m_Referer = "";
        private string m_Connection = "keep-alive";
        private string m_Protocol = "HTTP";
        private string m_Url = "";
        private bool m_myself = false;

        public string GetLineWith(string BeginLine, string[] strArray)
        {
            for (int i = 0; i < strArray.Length; i++)
            {
                if (strArray[i].StartsWith(BeginLine))
                {
                    return strArray[i].Remove(0, BeginLine.Length).TrimStart();
                }
            }
            return "";
        }

        public string[] GetLineArray(string Buffer)
        {
            char[] separator = new char[] { '\r', '\n' };
            string[] strArray = Buffer.Split(separator);
            return strArray;
        }

        public static bool IsHttpPacket(string RawData)
        {
            return ((RawData.StartsWith("HTTP/") || RawData.StartsWith("GET ")) || RawData.StartsWith("POST"));
        }

        public void ParseRequest(string Data)
        {
            string[] lineArray = GetLineArray(Data);
            if (lineArray.Length > 0)
            {

                string[] firstArray = lineArray[0].Split(' ');
                if (firstArray.Length == 3)
                {
                    this.m_Method = firstArray[0];
                    this.m_Protocol = firstArray[2].Substring(0, firstArray[2].IndexOf('/'));
                    this.m_Url = firstArray[1];
                }

                this.m_Host = GetLineWith("Host:", lineArray);
                this.m_Referer = GetLineWith("Referer:", lineArray);
                this.m_UserAgent = GetLineWith("User-Agent:", lineArray);
                this.m_Cookies = GetLineWith("Cookie:", lineArray);
                this.m_Connection = GetLineWith("Connection:", lineArray);
                string myself = GetLineWith("Author:", lineArray);
                if (!string.IsNullOrEmpty(myself)) this.m_myself = true;
                string contentlenght = GetLineWith("Content-Length:", lineArray);

                //post数据
                if (contentlenght != null)
                {

                }
            }
        }

        public string Host
        {
            get
            {
                return this.m_Host;
            }
        }

        public string Method
        {
            get
            {
                return this.m_Method;
            }
        }

        public string URL
        {
            get
            {
                return this.m_Protocol.ToLower() + "://" + this.m_Host + this.m_Url;
            }
        }

        public string Cookie
        {
            get
            {
                return this.m_Cookies;
            }
        }

        public string UserAgent
        {
            get { return this.m_UserAgent; }
        }

        public string Connection
        {
            get { return m_Connection; }
        }

        public bool Myself
        {
            get { return m_myself; }
        }

        public string Refer
        {
            get { return this.m_Referer; }
        }
    }
}

