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


        // YouTube���J���֐�
        public void ViewYouTube(string youtube_url)
        {
            var startInfo = new System.Diagnostics.ProcessStartInfo(youtube_url);
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);

            viewTimer = new System.Windows.Forms.Timer();
            viewTimer.Tick += new EventHandler(MyKeyEvent); // TimerEvent
            viewTimer.Interval = 10000;  // 10�b��Ɏ��s
            viewTimer.Start();
        }

        private void MyKeyEvent(object sender, EventArgs e)
        {
            SendKeys.Send("F");
            viewTimer.Stop();
        }



        // Main�֐�
        public Form1()
        {
            InitializeComponent();
            //  �g��{�^���𖳌���
            this.MaximizeBox = false;
            numericUpDown1.Value = 5;
            label2.Text = "�^�C�}�[��ݒ肵�ĊJ�n�{�^���������Ă��������B\r\n";
            label3.Text = "YouTube��URL��ݒ肵�Ă��������B";

            loopTimer = new System.Windows.Forms.Timer();
            loopTimer.Tick += new EventHandler(MyLoopEvent); // TimerEvent
            loopTimer.Interval = 1000; // 1�b���Ɋm�F
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
                    System.Media.SystemSounds.Asterisk.Play();  // �x����
                    ViewYouTube(url);
                    isCountNumber = false;
                    sleepCounter = 0;
                    progressBar1.Value = 0;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "�^�C�}�[���J�n����܂����B\r\n";
            label2.Text += "�ݒ莞�ԁF" + numericUpDown1.Value + "��\r\n";

            progressBar1.Minimum = 0;
            progressBar1.Maximum = (int)numericUpDown1.Value * 60;

            isCountNumber = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label2.Text = "�^�C�}�[�͒�~���ł��B\r\n";
            label2.Text += "�^�C�}�[��ݒ肵�ĊJ�n�{�^���������Ă��������B\r\n";
            isCountNumber = false;
            sleepCounter = 0;
            progressBar1.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // YouTube�̂Ƃ��̂�Text��ۑ�
            if (textBox1.Text.Contains("https://www.youtube.com/watch?v="))
            {
                url = textBox1.Text;

                label3.Text = url + "\r\n���Đ�����܂�\r\n";
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
            this.Visible = true;        //�t�H�[���̕\��
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //�A�C�R�����g���C�����菜��
            notifyIcon1.Visible = false;
            //�A�v���P�[�V�����̏I��
            Application.Exit();
        }

        // ����{�^���Ń^�X�N�g���C�ɍŏ���
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
            // �ŏ���������
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            // �őO�ʂɕ\��
            this.Activate();
        }
    }
}