namespace Numsieve
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.stabox = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.ResultBox = new System.Windows.Forms.RichTextBox();
            this.urltxt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.AAAAAAbox = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.AAAAAbox = new System.Windows.Forms.RichTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.AAAAbox = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.AAAbox = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.endAAAbox = new System.Windows.Forms.RichTextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // stabox
            // 
            this.stabox.FormattingEnabled = true;
            this.stabox.ItemHeight = 15;
            this.stabox.Location = new System.Drawing.Point(12, 545);
            this.stabox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.stabox.Name = "stabox";
            this.stabox.Size = new System.Drawing.Size(1083, 64);
            this.stabox.TabIndex = 31;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(496, 141);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(131, 35);
            this.button4.TabIndex = 30;
            this.button4.Text = "Export";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(349, 141);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(131, 35);
            this.button3.TabIndex = 29;
            this.button3.Text = "Empty";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ResultBox
            // 
            this.ResultBox.Location = new System.Drawing.Point(25, 66);
            this.ResultBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ResultBox.Name = "ResultBox";
            this.ResultBox.Size = new System.Drawing.Size(165, 174);
            this.ResultBox.TabIndex = 26;
            this.ResultBox.Text = "";
            // 
            // urltxt
            // 
            this.urltxt.Location = new System.Drawing.Point(109, 22);
            this.urltxt.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.urltxt.Name = "urltxt";
            this.urltxt.Size = new System.Drawing.Size(517, 25);
            this.urltxt.TabIndex = 25;
            this.urltxt.Text = "http://num.10010.com/NumApp/NumberCenter/qryNum?callback=jsonp_queryMoreNums&prov" +
    "inceCode=31&cityCode=310&advancePayLower=0&sortType=1&goodsNet=4&searchCategory=" +
    "3&qryType=01&numNet=130";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 24;
            this.label1.Text = "json URL";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(349, 66);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(276, 51);
            this.button1.TabIndex = 23;
            this.button1.Text = "START ! ! !";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 620);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 13, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1310, 25);
            this.statusStrip1.TabIndex = 32;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(97, 20);
            this.toolStripStatusLabel1.Text = "onbeta.com";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(107, 20);
            this.toolStripStatusLabel2.Text = "onsigma.com";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(96, 20);
            this.toolStripStatusLabel3.Text = "fatefox.com";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(80, 20);
            this.toolStripStatusLabel4.Text = "foxplus.io";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(125, 20);
            this.toolStripStatusLabel5.Text = "iceagedata.com";
            // 
            // AAAAAAbox
            // 
            this.AAAAAAbox.Location = new System.Drawing.Point(25, 308);
            this.AAAAAAbox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AAAAAAbox.Name = "AAAAAAbox";
            this.AAAAAAbox.Size = new System.Drawing.Size(136, 176);
            this.AAAAAAbox.TabIndex = 33;
            this.AAAAAAbox.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Location = new System.Drawing.Point(671, 36);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(367, 119);
            this.richTextBox2.TabIndex = 34;
            this.richTextBox2.Text = resources.GetString("richTextBox2.Text");
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(695, 204);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 29);
            this.button2.TabIndex = 35;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 15);
            this.label2.TabIndex = 36;
            this.label2.Text = "AAAAAA";
            // 
            // AAAAAbox
            // 
            this.AAAAAbox.Location = new System.Drawing.Point(179, 308);
            this.AAAAAbox.Name = "AAAAAbox";
            this.AAAAAbox.Size = new System.Drawing.Size(142, 176);
            this.AAAAAbox.TabIndex = 37;
            this.AAAAAbox.Text = "";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(176, 286);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 36;
            this.label3.Text = "AAAAA";
            // 
            // AAAAbox
            // 
            this.AAAAbox.Location = new System.Drawing.Point(511, 309);
            this.AAAAbox.Name = "AAAAbox";
            this.AAAAbox.Size = new System.Drawing.Size(131, 176);
            this.AAAAbox.TabIndex = 38;
            this.AAAAbox.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(508, 287);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 15);
            this.label4.TabIndex = 36;
            this.label4.Text = "AAAA";
            // 
            // AAAbox
            // 
            this.AAAbox.Location = new System.Drawing.Point(659, 309);
            this.AAAbox.Name = "AAAbox";
            this.AAAbox.Size = new System.Drawing.Size(140, 176);
            this.AAAbox.TabIndex = 39;
            this.AAAbox.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(659, 288);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(39, 15);
            this.label5.TabIndex = 40;
            this.label5.Text = "AAA ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(346, 287);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 15);
            this.label6.TabIndex = 36;
            this.label6.Text = "AAAA结尾";
            // 
            // endAAAbox
            // 
            this.endAAAbox.Location = new System.Drawing.Point(349, 309);
            this.endAAAbox.Name = "endAAAbox";
            this.endAAAbox.Size = new System.Drawing.Size(131, 176);
            this.endAAAbox.TabIndex = 38;
            this.endAAAbox.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1310, 645);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.AAAbox);
            this.Controls.Add(this.endAAAbox);
            this.Controls.Add(this.AAAAbox);
            this.Controls.Add(this.AAAAAbox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.AAAAAAbox);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.stabox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.ResultBox);
            this.Controls.Add(this.urltxt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NumSieve     By Kiros ";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox stabox;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.RichTextBox ResultBox;
        private System.Windows.Forms.TextBox urltxt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.RichTextBox AAAAAAbox;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox AAAAAbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox AAAAbox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox AAAbox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox endAAAbox;
    }
}

