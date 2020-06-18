using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ListControlTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                listBox1.Items.Add(textBox1.Text);
                comboBox1.Items.Add(textBox1.Text);
            }
            textBox1.Text = ""; // 글자 추가를 누르면 초기화 됨
            textBox1.Focus();   // 커서가 texbox1으로 이동함
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex > -1) // 리스트의 첫번째 값이 0이기 때문에 아무것도 선택하지 않은 값이 -1임
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = comboBox1.SelectedItem.ToString();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar == 13) // Enter값이 13이다.
            {
                button1_Click(sender, new EventArgs());
            }
        }
    }
}
