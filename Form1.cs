using System;
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
        public Form1()
        {
            InitializeComponent();
        }
        public bool isrun = false; //全局变量 描述运行状态
        private void button1_Click(object sender, EventArgs e)
        {

            if (isrun == false)
            {//开始
                isrun = true;
                Thread thread = new Thread(new ParameterizedThreadStart(delegate { Request(); }));
                thread.Start();
                button1.Text = "STOP ! ! !";
            }
            else {
                //停止
                isrun = false;
                button1.Text = "START ! ! !";
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
                   
                string GetStr = HttpGet(url);

               


                if (GetStr != "")
                {
                    try
                    {
                        string jsonText = GetStr;
                        JObject json1 = (JObject)JsonConvert.DeserializeObject(jsonText);
                        JArray array = (JArray)json1["numArray"];

                        for (int i = 0; i <= 1188; i += 12)  //每次拉取json都是100个号码，过滤掉无用信息
                        {
                            
                            addToBox(array[i].ToString() + "\r\n",ResultBox);
                            match(array[i].ToString());
                        }
                        addTostaBox(string.Format("Info: 扫描完成 "+count.ToString()+" 次"));
                        count++;
                    }
                    catch
                    {
                        addTostaBox(string.Format("Error: 线程错误，重试中..."));
                    }


                }
                else
                {
                    addTostaBox(string.Format("Error: 获取数据失败 -"+count_neterror.ToString()));
                    count_neterror++;
                }

            }
            addTostaBox(string.Format("Info: 线程已结束"));
            Thread.CurrentThread.Abort();

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
            //AABB
            //ABAB
            //ABCABC
            //ABCD
            //ABCBA
            //ABCCBA


            if (AAAAAArg.IsMatch(getstr))  // AAAAAA
            {
                addToBox(getstr+"\r\n", AAAAAAbox);
            }
            else if (AAAAArg.IsMatch(getstr)) //AAAAA
            {
                addToBox(getstr + "\r\n", AAAAAbox);
            }
            else if (endArg.IsMatch(getstr))  //AAA结尾
            {
                addToBox(getstr + "\r\n", endAAAbox);
            }
            else if (AAAArg.IsMatch(getstr)) //AAAA
            {
                addToBox(getstr + "\r\n", AAAAbox);
            }
            else if (AAArg.IsMatch(getstr)) //AAA
            {
                addToBox(getstr + "\r\n", AAAbox);
            }



        }
 
        public string HttpGet(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html;charset=UTF-8";

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                //处理下字符串：queryMoreNums -> json
                int index = retString.IndexOf('(');
                retString = retString.Substring(index + 1);
                index = retString.IndexOf(')');
                retString = retString.Substring(0, index);
           

                return retString;
            }
            catch
            {
               // MessageBox.Show("Something is wrong !");
                return "";
            }
        }

       





        delegate void addToBoxDelegate(string str, RichTextBox richtextbox);

        private void addToBox(string str ,RichTextBox richtextbox)
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
    }
}
