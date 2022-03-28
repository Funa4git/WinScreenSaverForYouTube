using System;
using System.Windows.Forms;

namespace WinScreenSaverForYouTube
{
    public partial class Form1 : Form
    {
        
        static int sleepCounter = 0;
        static bool isCountNumber = false;
        string url = "https://www.youtube.com/";
        private System.Windows.Forms.Timer viewTimer;
        private System.Windows.Forms.Timer loopTimer;


        // YouTubeを開く関数
        public void ViewYouTube(string youtube_url)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo(youtube_url);
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);

            viewTimer = new System.Windows.Forms.Timer();
            viewTimer.Tick += new EventHandler(MyKeyEvent); // TimerEvent
            viewTimer.Interval = 10000;  // 10秒後に実行
            viewTimer.Start();
        }

        private void MyKeyEvent(object sender, EventArgs e)
        {
            SendKeys.Send("F");
            viewTimer.Stop();
        }



        // Main関数
        public Form1()
        {
            InitializeComponent();
            //  拡大ボタンを無効化
            this.MaximizeBox = false;
            numericUpDown1.Value = 5;
            label2.Text = "タイマーを設定して開始ボタンを押してください。\r\n";
            label3.Text = "YouTubeのURLを設定してください。";

            loopTimer = new System.Windows.Forms.Timer();
            loopTimer.Tick += new EventHandler(MyLoopEvent); // TimerEvent
            loopTimer.Interval = 1000; // 1秒毎に確認
            loopTimer.Start();
        }

        private void MyLoopEvent(object sender, EventArgs e)
        {
            if (isCountNumber)
            {
                sleepCounter++;
                if(sleepCounter <= progressBar1.Maximum)
                {
                    progressBar1.Value = sleepCounter;
                }
                if (numericUpDown1.Value * 60 - sleepCounter < 0)
                {
                    System.Media.SystemSounds.Asterisk.Play();  // 警告音
                    ViewYouTube(url);
                    isCountNumber = false;
                    sleepCounter = 0;
                    progressBar1.Value = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "タイマーが開始されました。\r\n";
            label2.Text += "設定時間：" + numericUpDown1.Value + "分\r\n";

            progressBar1.Minimum = 0;
            progressBar1.Maximum = (int)numericUpDown1.Value * 60;

            isCountNumber = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "タイマーは停止中です。\r\n";
            label2.Text += "タイマーを設定して開始ボタンを押してください。\r\n";
            isCountNumber = false;
            sleepCounter = 0;
            progressBar1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // YouTubeのときのみTextを保存
            if (textBox1.Text.Contains("https://www.youtube.com/watch?v="))
            {
                url = textBox1.Text;

                label3.Text = url + "\r\nが再生されます\r\n";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (url.Contains("https://www.youtube.com/"))
            {
                ViewYouTube(url);
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 30;
        }

        private void launchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;        //フォームの表示
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //アイコンをトレイから取り除く
            notifyIcon1.Visible = false;
            //アプリケーションの終了
            Application.Exit();
        }

        // 閉じるボタンでタスクトレイに最小化
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (e.CloseReason != System.Windows.Forms.CloseReason.ApplicationExitCall)
            {
                e.Cancel = true;
                this.Visible = false;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            this.Visible = true;
            // 最小化を解除
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            // 最前面に表示
            this.Activate();
        }
    }
}