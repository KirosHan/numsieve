﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading;
using System.Text.RegularExpressions;

namespace Numsieve
{
    public partial class Form1 : Form
    {
   

        private int adplay = 0;//全局播放控制

        public Form1()
        {
            InitializeComponent();
        }
        public bool isrun = false; //全局变量 描述运行状态
        public int delay = 1000; //全局变量 延迟时间（毫秒）
        private void button1_Click(object sender, EventArgs e)
        {

            if (isrun == false)
            {//开始
                delay = Int32.Parse(numericUpDown1.Value.ToString()) * 1000;
                isrun = true;
                Thread thread = new Thread(new ParameterizedThreadStart(delegate { Request(); }));
                thread.Start();
                setstatus(true);
            }
            else {
                //停止
                isrun = false;
                setstatus(false);
            }

        }
        public delegate void RequestDelegate();


        private void Request()  //线程函数  扫号逻辑在这里完成
        {

            int count_neterror = 1;  //统计连接错误次数
            int count = 1;//统计扫描次数

            // ServicePointManager.DefaultConnectionLimit = 1000;
            addTostaBox(string.Format("Info: 线程已启动"));
            while (isrun == true)
            {
                string url = urltxt.Text.Trim();
                if (url == "")
                {
                    isrun = false;
                    MessageBox.Show("url不能为空");
                }

                string GetStr = jsonhandle(HttpGet(url));
                if (GetStr != "")
                {
                    try
                    {
                        string jsonText = GetStr;
                        JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray array = (JArray)json1["numArray"];
                        int length = Int32.Parse(json1["splitLen"].ToString());

                        for (int i = 0; i <= 1188; i += length)  //每次拉取json都是100个号码，过滤掉无用信息
                        {

                            addToBox(array[i].ToString() + "\r\n", ResultBox);
                            match(array[i].ToString());
                        }
                        addTostaBox(string.Format("Info: 扫描完成 " + count.ToString() + " 次"));
                        count++;
                    }
                    catch
                    {
                        addTostaBox(string.Format("Error: 线程错误，重试中..."));
                    }


                }
                else
                {
                    addTostaBox(string.Format("Error: 获取数据失败 （" + count_neterror.ToString()+")"));
                    count_neterror++;
                }
                System.Threading.Thread.Sleep(delay);//单线程循环延迟
            }
            addTostaBox(string.Format("Info: 线程已结束"));
            isrun = false;
            setstatus(false);
            Thread.CurrentThread.Abort();



        }


        public delegate void setstatusDelegate(bool sta);
        public void setstatus(bool sta)
        {
            if (sta == true)
            {
                if (button1.InvokeRequired)
                {
                    setstatusDelegate d = setstatus;
                    button1.Invoke(d, sta);

                }
                else
                {
                    button1.Text = "STOP ! ! !";
                }
            
                开始ToolStripMenuItem.Enabled = false;
                停止ToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (button1.InvokeRequired)
                {
                    setstatusDelegate d = setstatus;
                    button1.Invoke(d, sta);

                }
                else
                {
                    button1.Text = "START ! ! !";
                }
                
                开始ToolStripMenuItem.Enabled = true;
                停止ToolStripMenuItem.Enabled = false;
            }

        }

        public void match(string getstr)
        {
            //  string pat = 
            Regex AAAAAArg = new Regex(@"([\d])\1{5}"); //包含AAAAAA号码
            Regex AAAAArg = new Regex(@"([\d])\1{4}"); //包含AAAAA号码
            //AAABBB
            Regex endArg = new Regex(@"([\d])\1{2}\b"); //AAA结尾号码
            Regex AAAArg = new Regex(@"([\d])\1{3}"); //包含AAAA号码
            Regex AAArg = new Regex(@"([\d])\1{2}"); //包含AAA号码
            //---------------------------
            Regex AABBrg = new Regex(@"([\d])\1{1}([\d])\2{1}");  //AABB

            Regex ABABrg = new Regex(@"([\d])([\d])\1\2");  //ABAB
            Regex ABCABCrg = new Regex(@"(012|123|234|345|456|567|678|789|987|876|765|654|543|432|321|210)\1{1}");  //ABCABC
            Regex ABCDrg = new Regex(@"0123|1234|2345|3456|4567|5678|6789|7890|0987|9876|8765|7654|6543|5432|4321|3210");  //ABCD
            Regex ABCBArg = new Regex(@"01210|12321|23432|34543|45654|56765|67876|78987|89098");//ABCBA
            Regex ABCCBArg = new Regex(@"012210|123321|234432|345543|456654|567765|678876|789987|890098");//ABCCBA


            if (AAAAAArg.IsMatch(getstr))  // AAAAAA
            {
                addToBox(getstr + "\r\n", AAAAAAbox);
            }
            else if (AAAAArg.IsMatch(getstr)) //AAAAA
            {
                addToBox(getstr + "\r\n", AAAAAbox);
            }
            else if (AAAArg.IsMatch(getstr)) //AAAA
            {
                addToBox(getstr + "\r\n", AAAAbox);
            }
            else if (AAArg.IsMatch(getstr)) //AAA
            {
                addToBox(getstr + "\r\n", AAAbox);
            }
            if (endArg.IsMatch(getstr))  //AAA结尾
            {
                addToBox(getstr + "\r\n", endAAAbox);
            }
            if (AABBrg.IsMatch(getstr)) //AABB
            {
                addToBox(getstr + "\r\n", AABBbox);
            }
            if (ABABrg.IsMatch(getstr)) //ABAB
            {
                addToBox(getstr + "\r\n", ABABbox);
            }
            if (ABCABCrg.IsMatch(getstr)) //ABCABC
            {
                addToBox(getstr + "\r\n", ABCABCbox);
            }
            if (ABCDrg.IsMatch(getstr)) //ABCD
            {
                addToBox(getstr + "\r\n", ABCDbox);
            }
            if (ABCBArg.IsMatch(getstr)) //ABCD
            {
                addToBox(getstr + "\r\n", ABCBAbox);
            }
            if (ABCCBArg.IsMatch(getstr)) //ABCD
            {
                addToBox(getstr + "\r\n", ABCCBAbox);
            }

        }

        public string HttpGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";
                //对于https://m.10010.com 不添加useragent无法拉取数据
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Safari/537.36 Edge/13.10586";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch
            {
                // MessageBox.Show("Something is wrong !");
                return "";
            }
        }

        public string jsonhandle(string _str)
        {
            try
            {
                //处理下字符串：queryMoreNums -> json
                int index = _str.IndexOf('(');
                _str = _str.Substring(index + 1);
                index = _str.IndexOf(')');
                _str = _str.Substring(0, index);
                return _str;

            }
            catch
            {
                return "";

            }

        }





        delegate void addToBoxDelegate(string str, RichTextBox richtextbox);

        private void addToBox(string str, RichTextBox richtextbox)
        {
            /*/分类存放
            if (type == 0)
            {
                if (richTextBox2.InvokeRequired)
                {
                    addToresultBoxDelegate d = addToresultBox;
                    richTextBox2.Invoke(d, str, type);
                }
                else
                {
                    richTextBox2.AppendText(str);

                }
            }
            else if (type == 1) { }
            else if (type == 2) { }
            else if (type == 3) { }
            else if (type == 4) { }
            else if (type == 5) { }
            else if (type == 6) { }
            */
            if (richtextbox.InvokeRequired)
            {
                addToBoxDelegate d = addToBox;
                richtextbox.Invoke(d, str, richtextbox);
            }
            else
            {
                richtextbox.AppendText(str);
                richtextbox.SelectionStart = richtextbox.Text.Length; //Set the current caret position at the end
                richtextbox.ScrollToCaret(); //Now scroll it automatically

            }


        }
        delegate void addtostaBoxDelegate(string str);
        private void addTostaBox(string str)
        {
            if (stabox.InvokeRequired)
            {
                addtostaBoxDelegate d = addTostaBox;
                stabox.Invoke(d, str);

            }
            else
            {
                //  Stabox.AppendText(str);
                stabox.Items.Add(str);
                stabox.SetSelected(stabox.Items.Count - 1, true);
            }
        }



        private void button3_Click(object sender, EventArgs e)
        {
            ResultBox.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string GetStr = richTextBox2.Text;
            try
            {
                string jsonText = GetStr;
                JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                JArray array = (JArray)json1["numArray"];

                for (int i = 0; i <= 1188; i += 12)  //每次拉取json都是100个号码，过滤掉无用信息
                {

                    addToBox(array[i].ToString() + "\r\n", ResultBox);
                    match(array[i].ToString());
                }
                addTostaBox(string.Format("Info: 扫描完成 ... 次"));

            }
            catch
            {
                addTostaBox(string.Format("Error: 线程错误，重试中..."));
            }
        }

        private void 功能ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }



        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isrun = false;

        }

        private void 开始ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isrun == false)
            {//开始
                delay = Int32.Parse(numericUpDown1.Value.ToString()) * 1000;
                isrun = true;
                Thread thread = new Thread(new ParameterizedThreadStart(delegate { Request(); }));
                thread.Start();
                button1.Text = "STOP ! ! !";
                开始ToolStripMenuItem.Enabled = false;
                停止ToolStripMenuItem.Enabled = true;
            }
        }

        private void 停止ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (isrun != false)
                //停止
                isrun = false;
            button1.Text = "START ! ! !";
            开始ToolStripMenuItem.Enabled = true;
            停止ToolStripMenuItem.Enabled = false;
        }

        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About aboutfrm = new About();
            aboutfrm.ShowDialog();
        }

        private void 防封代理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("该功能将在后续版本中开放，敬请期待！");
        }


       
        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            spec specfrm = new spec();
            specfrm.ShowDialog();
        }

 

        private void 地址获取器20ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Capture cap = new Capture();
            cap.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Capture cap = new Capture();
            cap.ShowDialog();

        }


        private void checkversion(string s_version,string s_date,bool isshow)  //根据版本号查找更新
        {
            String[] arr_s_version = s_version.Split('.');
            String[] arr_now_version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            
            for (int i =0;i<=2;i++)
            {
                if (int.Parse(arr_now_version[i]) < int.Parse(arr_s_version[i]))
                {
                    toolStripStatusLabel6.Text = "新版本已发布: v" + s_version;
                    if (isshow == true)
                    {
                        download downloadfrm = new download(s_version, s_date);
                        downloadfrm.ShowDialog();
                       // MessageBox.Show("有新版本!" + s_version);
                    }
                    break;
                }else if(int.Parse(arr_now_version[i]) > int.Parse(arr_s_version[i]))
                { break; }
                
            }

        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                string jsonText = HttpGet("http://www.onsigma.com/numsieve.php");
                JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                string s_version = json1["version"].ToString();
                string s_date = json1["date"].ToString();
                string s_notice = json1["notice"].ToString();
                string s_ad = json1["ad"].ToString();
                adplay = int.Parse(json1["isplay"].ToString());

                checkversion(s_version,s_date,true);
                toolStripStatusLabel7.Text = s_notice;
                toolStripStatusLabel1.Text = "Release Date:" + s_date;
                if (adplay == 1)
                { 关于ToolStripMenuItem.Visible = true; }

            }
            catch
            {
                
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel6.Text = "Version " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            try
            {
                string jsonText = HttpGet("http://www.onsigma.com/numsieve.php");
                JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                string s_version = json1["version"].ToString();
                string s_date = json1["date"].ToString();
                string s_notice = json1["notice"].ToString();
                string s_ad = json1["ad"].ToString();
                adplay = int.Parse(json1["isplay"].ToString());

                checkversion(s_version, s_date, true);
                toolStripStatusLabel7.Text = s_notice;
                toolStripStatusLabel1.Text = "Release Date:" + s_date;
                if (adplay == 1)
                { 关于ToolStripMenuItem.Visible = true; }

            }
            catch
            {

            }
        }

        private void VersionChecktimer_Tick(object sender, EventArgs e)
        {

            try
            {
                string jsonText = HttpGet("http://www.onsigma.com/numsieve.php");
                JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                string s_version = json1["version"].ToString();
                string s_date = json1["date"].ToString();
                string s_notice = json1["notice"].ToString();
                string s_ad = json1["ad"].ToString();
                adplay = int.Parse(json1["isplay"].ToString());

                checkversion(s_version, s_date, false);
                toolStripStatusLabel7.Text = s_notice;
                toolStripStatusLabel1.Text = "Release Date:" + s_date;
                if (adplay == 1)
                { 关于ToolStripMenuItem.Visible = true; }

            }
            catch
            {

            }

        }

        private void 地址获取器30ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            capture3 cap3 = new capture3();
            cap3.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            capture3 cap3 = new capture3();
            cap3.ShowDialog();
        }
    }
}
