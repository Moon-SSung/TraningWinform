﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LoginApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = $"ID :  {textBox1.Text}  \r\nPASSWORD : {textBox2.Text}";

            //ToUppper을 사용해서 문자들을 대문자로 인식시켜서 대소문자 구분없이 로그인이 가능하다
            if ((textBox1.Text.ToUpper() == "ADMIN") && (textBox2.Text.ToUpper() =="P@SSW0RD!"))
            {
                MessageBox.Show("관리자 로그인!!");
            }
        }
    }
}
