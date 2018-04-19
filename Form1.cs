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
                            
                            addToresultBox(array[i].ToString() + "\r\n",0);
                        }
                        addTostaBox(string.Format("Info: 扫描完成 "+count.ToString()+" 次"));
                        count++;
                    }
                    catch
                    {
                        addTostaBox(string.Format("Error: 拉取错误，重试中..."));
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

        public void match(string _getstr)
        {


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

       





        delegate void addToresultBoxDelegate(string str, int type);

        private void addToresultBox(string str ,int type)
        {
            if (type == 0)
            {
                if (NameBox.InvokeRequired)
                {
                    addToresultBoxDelegate d = addToresultBox;
                    NameBox.Invoke(d, str);
                }
                else
                {
                    NameBox.AppendText(str);

                }
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
            NameBox.Text = "";
        }



    }
}
