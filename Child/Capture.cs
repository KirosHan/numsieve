using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Sniffer;
using System.Net;
using System.Text.RegularExpressions;

namespace Numsieve
{
    public partial class Capture : Form
    {
        public Capture()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }
        private ContextMenuStrip strip = new ContextMenuStrip();//1
        private void Capture_Load(object sender, EventArgs e)
        {
            LoadListView();
            strip.Items.Add("复制URL");//2
            strip.Items[0].Click += new System.EventHandler(this.Item_Click);
            
        }

        private void LoadListView()
        {
            this.listView1.FullRowSelect = true;
            this.listView1.Columns.Add("开始时间", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("地址", 600, HorizontalAlignment.Left);
            this.listView1.Columns.Add("方式", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Cookie", 120, HorizontalAlignment.Left);
            /*
            this.listView1.Columns.Add("耗时", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("方式", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("结果", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("收到长度", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("类型", 60, HorizontalAlignment.Left);
            */

        }
        private void Item_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.FocusedItem.SubItems[1].Text.ToString());
            Clipboard.SetText(listView1.FocusedItem.SubItems[1].Text.ToString());
            MessageBox.Show("URL已复制：" + listView1.FocusedItem.SubItems[1].Text.ToString());
        }

        private void AddList(string url, string mode, string cookie)
        {
            try
            {
                this.Invoke((EventHandler)delegate
                {

                    this.listView1.BeginUpdate();   //数据更新，UI暂时挂起，直到EndUpdate绘制控件，可以有效避免闪烁并大大提高加载速度


                        ListViewItem lvi = new ListViewItem();

                        lvi.Text = DateTime.Now.ToString("HH:mm:ss");

                        lvi.SubItems.Add(url);
                        lvi.SubItems.Add(mode);
                        lvi.SubItems.Add(cookie);
                        checkurl(url);
                    this.listView1.Items.Add(lvi);

                    this.listView1.EndUpdate();
                });
            }
            catch { }
        }

        private static SnifferSocket m_Sniffer;

        private void checkurl(string _str)
        {
            try
            {
                if (checkBox1.Checked == true)
                {
                    Regex rg = new Regex(@"num.10010.com/NumApp/NumberCenter"); //验证域名
                    Regex mobilerg = new Regex(@"m.10010.com/NumApp/NumberCenter"); //验证域名
                    if (rg.IsMatch(_str) || mobilerg.IsMatch(_str))
                    {
                        Clipboard.SetText(_str);
                        MessageBox.Show("检测到可使用的URL并已复制到剪贴板：" + _str);
                    }
                }
            }
            catch(Exception e)
            { throw e;}
        }

        void m_Sniffer_TcpPacketReceived(TcpPacket packet)
        {
            string data = Encoding.ASCII.GetString(packet.Data);
            if (data.StartsWith("GET "))
            {
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                this.AddList(sn.URL,"GET", sn.Cookie);
               // checkurl(sn.URL);
                this.SetText("GET:" + sn.Host + ",url:" + sn.URL + "cookie:" + sn.Cookie);
                
            }
            else if (data.StartsWith("POST "))
            {
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                this.AddList(sn.URL, "POST", sn.Cookie);
                //checkurl(sn.URL);
                this.SetText("POST:" + sn.Host + ",url:" + sn.URL + "cookie:" + sn.Cookie);
            }
            else if (data.StartsWith("HTTP/"))
            {
                
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                this.AddList(sn.URL, "HTTP", sn.Cookie);
                this.SetText("请求:" + sn.Host + ",url:" + sn.URL + "cookie:" + sn.Cookie);
                
            }
            else
            {
                HttpSniffer.HttpPacket sn = new HttpSniffer.HttpPacket();
                sn.ParseRequest(data);
                this.AddList(sn.ToString(), "其他", "...");
                this.SetText("请求:" + sn.Host + ",url:" + sn.URL );
            }
        }

        private void SetText(string text)
        {
            /*
            try
            {
                this.Invoke((EventHandler)delegate
                {
                    this.textBox1.AppendText(text + "\r\n");
                });
            }
            catch { }

    */
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                strip.Show(listView1, e.Location);//鼠标右键按下弹出菜单
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_Sniffer = new SnifferSocket();
            m_Sniffer.TcpPacketReceived += new TcpPacketCallback(m_Sniffer_TcpPacketReceived);
            IPAddress[] addressList = Dns.GetHostAddresses(Dns.GetHostName());

            if (addressList.Length != 0)
            {
                foreach (IPAddress ip in addressList)
                {
                    if (ip.ToString().Split('.').Length == 4) m_Sniffer.Sniff(ip.ToString());
                }
            }
            this.button1.Text = "已开始抓取URL";
            this.button1.Enabled = false;
            System.Diagnostics.Process.Start("http://www.10010.com");
        }
    }
}
