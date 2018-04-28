using CefSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Numsieve
{
    public partial class CaptureListFrm : Form
    {

        private ChromeHelper chrome = null;

        /// <summary>
        /// 是否开始
        /// </summary>
        private bool isStart = false;

        /// <summary>
        /// 当前选中抓包条目
        /// </summary>
        private LoadInfo currentModel = null;

        /// <summary>
        /// 默认编码方式
        /// </summary>
        private Encoding defaultEncode = Encoding.GetEncoding("gbk");

        private ContextMenuStrip strip = new ContextMenuStrip();//1
        

        public CaptureListFrm()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            LoadListView();
        }

        public CaptureListFrm(ChromeHelper chrome)
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
            this.chrome = chrome;
            LoadListView();
        }

        private void Execute()
        {
            if (chrome == null)
            {
                chrome = new ChromeHelper("http://www.10010.com");
                chrome.Init();
                var browser = chrome.CreateBrowser();
                this.Invoke(new Action<Panel>(p =>
                {
                    p.Controls.Add(browser);
                    p.Update();
                }), this.panel1);
            }
        }
        private void LoadListView()
        {
            this.listView1.FullRowSelect = true;
            this.listView1.Columns.Add("开始时间", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("地址", 600, HorizontalAlignment.Left);
            this.listView1.Columns.Add("耗时", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("方式", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("结果", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("收到长度", 60, HorizontalAlignment.Left);
            this.listView1.Columns.Add("类型", 60, HorizontalAlignment.Left);
            

            this.listView2.FullRowSelect = true;
            this.listView2.Columns.Add("名称", 160, HorizontalAlignment.Left);
            this.listView2.Columns.Add("值", 260, HorizontalAlignment.Left);

            this.listView3.FullRowSelect = true;
            this.listView3.Columns.Add("名称", 160, HorizontalAlignment.Left);
            this.listView3.Columns.Add("值", 260, HorizontalAlignment.Left);
        }


        private void CaptureListFrm_Load(object sender, EventArgs e)
        {
            strip.Items.Add("复制URL");//2

            strip.Items[0].Click += new System.EventHandler(this.Item_Click);
            //Application.ApplicationExit += Application_ApplicationExit;
            Execute();
            Thread t = new Thread(ExecuteThread);
            t.IsBackground = true;
            t.Start();
        }
        private void Item_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(listView1.FocusedItem.SubItems[1].Text.ToString());
            Clipboard.SetText(listView1.FocusedItem.SubItems[1].Text.ToString());
            MessageBox.Show("URL已复制：" + listView1.FocusedItem.SubItems[1].Text.ToString());
        }

        private void ExecuteThread()
        {
            var list = LoadHelper.GetAll();
            this.AddList(list);
            int maxId = 0;
            if (list.Count > 0) { maxId = list.Max(p => p.Index); }
            while (true)
            {
                try
                {
                    while (isStart)
                    {
                        try
                        {
                            var newList = LoadHelper.GetNew(maxId);
                            if (newList.Count > 0)
                            {
                                maxId = newList.Max(p => p.Index);
                                maxId = (newList.Max(p => p.Index) > maxId ? newList.Max(p => p.Index) : maxId);
                            }

                            this.AddList(newList);
                        }
                        finally { Thread.Sleep(200); }
                    }
                }
                catch { }
                finally
                {
                    Thread.Sleep(300);
                }
            }
        }

        private void AddList(List<LoadInfo> list)
        {
            if (list.Count > 0)
            {
                this.Invoke(new Action<ListView>(p =>
                {
                    p.BeginUpdate();
                    foreach (var model in list)
                    {
                        ListViewItem item = new ListViewItem(DateTime.Now.ToString("HH:mm:ss"));
                        item.Tag = model.Key;
                        item.SubItems.AddRange(new string[] { model.Url ,"", "", "", "", ""});

                        p.Items.Add(item);

                        if (checkBox1.Checked == true)
                        {
                            Regex rg = new Regex(@"num.10010.com/NumApp/NumberCenter"); //验证域名
                            Regex mobilerg = new Regex(@"m.10010.com/NumApp/NumberCenter"); //验证域名
                            if (rg.IsMatch(model.Url)|| mobilerg.IsMatch(model.Url))
                            {
                                Clipboard.SetText(model.Url);
                                MessageBox.Show("检测到可使用的URL并已复制到剪贴板：" + model.Url);
                            }
                        }
                    }

                    p.EndUpdate();
                }), this.listView1);
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (this.btnStart.Text == "开始")
            {
                this.btnStart.Text = "停止";
                this.chrome.StartCapture();
            }
            else
            {
                this.btnStart.Text = "开始";
                this.chrome.StopCapture();
            }

            isStart = !isStart;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            /*  bug调试中
            LoadHelper.ClearAll();
            this.Invoke(new Action<ListView>(p =>
            {
                p.BeginUpdate();
                p.Items.Clear();
                p.EndUpdate();
            }), this.listView1);
            */    
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems != null && this.listView1.SelectedItems.Count > 0)
            {
                string guid = this.listView1.SelectedItems[0].Tag as string;
                currentModel = LoadHelper.GetInfo(guid);
                if (currentModel != null)
                {
                    this.listView2.Items.Clear();
                    this.listView3.Items.Clear();
                    if (currentModel.RequestHeader != null && currentModel.RequestHeader.Count > 0)
                    {
                        InitHeader(currentModel.RequestHeader, this.listView2);
                    }

                    if (currentModel.ResponseHeader != null && currentModel.ResponseHeader.Count > 0)
                    {
                        InitHeader(currentModel.ResponseHeader, this.listView3);
                    }

                    try
                    {
                        this.richTextBox1.Clear();
                        if (currentModel.ResponseHeader["Content-Type"].Contains("image"))
                        {
                            Bitmap bmp = null;
                            using (MemoryStream ms = new MemoryStream(currentModel.Data))
                            {
                                bmp = (Bitmap)Image.FromStream(ms);
                            }

                            Clipboard.SetDataObject(bmp, false);//将图片放在剪贴板中
                            if (richTextBox1.CanPaste(DataFormats.GetFormat(DataFormats.Bitmap)))
                                richTextBox1.Paste();//粘贴数据
                        }
                        else
                        {
                            this.richTextBox1.Text = defaultEncode.GetString(currentModel.Data);
                        }
                    }
                    catch { }
                }
            }
        }

        private void InitHeader(NameValueCollection collec, ListView listView)
        {
            this.Invoke(new Action<ListView>(p =>
            {
                p.BeginUpdate();
                foreach (var key in collec.AllKeys)
                {
                    ListViewItem item = new ListViewItem(key);
                    item.SubItems.Add(collec[key]);

                    p.Items.Add(item);
                }

                p.EndUpdate();
            }), listView);
        }

        


        #region 右键菜单
        private void EncodingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            try
            {
                if (currentModel != null)
                {
                    if (item.Name == "utf8ToolStripMenuItem")
                    {
                        this.richTextBox1.Text = Encoding.UTF8.GetString(currentModel.Data);
                        SetText("UTF8");
                        defaultEncode = Encoding.UTF8;
                    }
                    else if (item.Name == "gb2312ToolStripMenuItem")
                    {
                        this.richTextBox1.Text = Encoding.GetEncoding("gb2312").GetString(currentModel.Data);
                        SetText("GB2312");
                        defaultEncode = Encoding.GetEncoding("gb2312");
                    }
                    else if (item.Name == "gBKToolStripMenuItem")
                    {
                        this.richTextBox1.Text = Encoding.GetEncoding("gbk").GetString(currentModel.Data);
                        SetText("GBK");
                        defaultEncode = Encoding.GetEncoding("gbk");
                    }
                    else if (item.Name == "aNSIToolStripMenuItem1")
                    {
                        this.richTextBox1.Text = Encoding.Default.GetString(currentModel.Data);
                        SetText("ANSI");
                        defaultEncode = Encoding.Default;
                    }
                    else if (item.Name == "unicodeToolStripMenuItem")
                    {
                        this.richTextBox1.Text = Encoding.Unicode.GetString(currentModel.Data);
                        SetText("Unicode");
                        defaultEncode = Encoding.Unicode;
                    }
                }
            }
            catch { }
        }

        private void SetText(string type)
        {
            this.utf8ToolStripMenuItem.Text = (type == "UTF8" ? "√  UTF8" : "   UTF8");
            this.gb2312ToolStripMenuItem.Text = (type == "GB2312" ? "√  GB2312" : "   GB2312");
            this.gBKToolStripMenuItem.Text = (type == "GBK" ? "√  GBK" : "   GBK");
            this.aNSIToolStripMenuItem1.Text = (type == "ANSI" ? "√  ANSI" : "   ANSI");
            this.unicodeToolStripMenuItem.Text = (type == "Unicode" ? "√  Unicode" : "   Unicode");
        }
        #endregion

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnNav_Click(object sender, EventArgs e)
        {
            this.chrome.JumpUrl(this.txtUrl.Text);
        }

        private void CaptureListFrm_Leave(object sender, EventArgs e)
        {
           // Cef.Shutdown();
        }
        private void Application_ApplicationExit(object sender, EventArgs e)
        {
            Cef.Shutdown();
        }
       // listViewtcmedicineSearch.MouseClick += new MouseEventHandler(listViewtcmedicineSearchMouseClick);

        private void listViewtcmedicineSearchMouseClick(object sender, MouseEventArgs e)
        {
            
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                strip.Show(listView1, e.Location);//鼠标右键按下弹出菜单
            }
        }

        private void CaptureListFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Cef.Shutdown();
        }
        public void Close1()
        {
            this.Dispose();

        }

    }
}
