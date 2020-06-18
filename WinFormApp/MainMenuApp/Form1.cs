using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainMenuApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void 파일FToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void MnuNewFile_Click(object sender, EventArgs e)
        {
            textBox1.Text = MnuNewFile.Text + Environment.NewLine;
            toolStripStatusLabel1.Text = MnuNewFile.Text;   // toolstrip bar에 설명을 나타냄
            // 실제 새 파일 로직 집어 넣어야 함
        }

        private void 열기OToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = 열기OToolStripMenuItem.Text + Environment.NewLine;
        }

        private void 저장SToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Text = 저장SToolStripMenuItem.Text + Environment.NewLine;
            MessageBox.Show("Save ON");
        }

        private void 종료XToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void 프로그램정보AToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox aboutBox = new AboutBox();
            aboutBox.ShowDialog();
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Right)
            {
                contextMenuStrip1.Show();
            }
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            LblMouseLocation.Text = $"X, Y = ({e.X},{e.Y})";
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            button1.Focus();

        }
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            MnuNewFile_Click(sender, e); 
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // 폼로드의 경우는 값을 "초기화"할 때 주로 사용
        }
    }
}
