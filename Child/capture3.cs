using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numsieve
{
    public partial class capture3 : Form
    {
        private UrlCaptureConfiguration CaptureConfiguration { get; set; }
        bool tbIgnoreResources = false;//过滤css js image资源
        private const string Separator = "------------------------------------------------------------------";
        private int listenport = 8888;
        public capture3()
        {
            InitializeComponent();
            CaptureConfiguration = new UrlCaptureConfiguration();
        }
        private ContextMenuStrip strip = new ContextMenuStrip();//1

        private void capture3_Load(object sender, EventArgs e)
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
            this.listView1.Columns.Add("版本", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("Request", 120, HorizontalAlignment.Left);
            /*
            this.listView1.Columns.Add("耗时", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("方式", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("结果", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("收到长度", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("类型", 60, HorizontalAlignment.Left);
            */

        }
        private void FiddlerApplication_AfterSessionComplete(Session sess)
        {
            // Ignore HTTPS connect requests
            if (sess.RequestMethod == "CONNECT")
                return;

            if (CaptureConfiguration.ProcessId > 0)
            {
                if (sess.LocalProcessID != 0 && sess.LocalProcessID != CaptureConfiguration.ProcessId)
                    return;
            }

            if (!string.IsNullOrEmpty(CaptureConfiguration.CaptureDomain))
            {
                if (sess.hostname.ToLower() != CaptureConfiguration.CaptureDomain.Trim().ToLower())
                    return;
            }

            if (CaptureConfiguration.IgnoreResources)
            {
                string url = sess.fullUrl.ToLower();

                var extensions = CaptureConfiguration.ExtensionFilterExclusions;
                foreach (var ext in extensions)
                {
                    if (url.Contains(ext))
                        return;
                }

                var filters = CaptureConfiguration.UrlFilterExclusions;
                foreach (var urlFilter in filters)
                {
                    if (url.Contains(urlFilter))
                        return;
                }
            }

            if (sess == null || sess.oRequest == null || sess.oRequest.headers == null)
                return;

            string headers = sess.oRequest.headers.ToString();
            var reqBody = "";
            reqBody = Encoding.UTF8.GetString(sess.RequestBody);

            // if you wanted to capture the response
            //string respHeaders = session.oResponse.headers.ToString();
            //var respBody = Encoding.UTF8.GetString(session.ResponseBody);

            // replace the HTTP line to inject full URL
            string firstLine = sess.RequestMethod + " " + sess.fullUrl + " " + sess.oRequest.headers.HTTPVersion;
            int at = headers.IndexOf("\r\n");
            if (at < 0)
                return;
            headers = firstLine + "\r\n" + headers.Substring(at + 1);

            string output = headers + "\r\n" +
                            (!string.IsNullOrEmpty(reqBody) ? reqBody + "\r\n" : string.Empty) +
                            Separator + "\r\n\r\n";

            AddList(sess.fullUrl, sess.RequestMethod, sess.oRequest.headers.HTTPVersion, reqBody);
            // must marshal to UI thread
            

        }

        void Start()
        {
            if (tbIgnoreResources)
                CaptureConfiguration.IgnoreResources = true;
            else
                CaptureConfiguration.IgnoreResources = false;

            string strProcId = "";

            int procId = 0;
            if (!string.IsNullOrEmpty(strProcId))
            {
                if (!int.TryParse(strProcId, out procId))
                    procId = 0;
            }
            CaptureConfiguration.ProcessId = procId;
            CaptureConfiguration.CaptureDomain = "";//domain


            FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;
            FiddlerApplication.Startup(listenport, true, true, true);
        }


        void Stop()
        {
            FiddlerApplication.AfterSessionComplete -= FiddlerApplication_AfterSessionComplete;

            if (FiddlerApplication.IsStarted())
                FiddlerApplication.Shutdown();
        }
        private void AddList(string url, string mode,string version, string body)
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
                    lvi.SubItems.Add(version);
                    lvi.SubItems.Add(body);
                    checkurl(url);
                    this.listView1.Items.Add(lvi);

                    this.listView1.EndUpdate();
                });
            }
            catch { }
        }

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
            catch (Exception e)
            { throw e; }
        }
        private void Item_Click(object sender, EventArgs e)
        {
          
            Clipboard.SetText(listView1.FocusedItem.SubItems[1].Text.ToString());
            MessageBox.Show("URL已复制：" + listView1.FocusedItem.SubItems[1].Text.ToString());
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
            if (button1.Text == "开始抓取")
            {
                if (int.Parse(textBox1.Text.Trim()) < 1 || int.Parse(textBox1.Text.Trim()) > 65535)
                {
                    MessageBox.Show("端口号必须在区间 0-65536 内");
                    textBox1.Text = "8888";
                }
                else 
                {
                    
                    if (!CertMaker.rootCertExists())
                    {
                        try
                        {
                            CertMaker.createRootCert();
                            CertMaker.trustRootCert();
                        }
                        catch
                        {
                            MessageBox.Show("证书无法被创建，可能无法正常抓包，尝试清理证书，如还是出现此问题请联系作者。");
                        }
                    }
                   
                    listenport = int.Parse(textBox1.Text.Trim());
                    Start();
                    UpdateButtonStatus();
                    System.Diagnostics.Process.Start("http://www.10010.com");
                }
            }
            else
            {
                Stop();
                UpdateButtonStatus();

            }
        }

        public void UpdateButtonStatus()
        {


            if(FiddlerApplication.IsStarted())
            {
                button1.Text = "停止抓取";
            }
            else
            {

                button1.Text = "开始抓取";
            }

        }

        private void capture3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (CertMaker.rootCertExists())
            {
                try { CertMaker.removeFiddlerGeneratedCerts(true); }
                catch { MessageBox.Show("清理失败，请联系作者。"); }
                

                
            }
        }
    }
}
