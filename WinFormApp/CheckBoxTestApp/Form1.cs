﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CheckBoxTestApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //private string strTemp = "";
        //private void UpdateLabel(string s, bool b)
        //{
        //    if(b == true)
        //    {
        //        label1.Text = strTemp;
        //    }
        //    else
        //    {
        //        strTemp = label1.Text;
        //        int i = strTemp.IndexOf(s.Substring(0, 1));
        //        int j = i + s.Length;
        //        label1.Text = strTemp.Remove(i, j - i);
        //    }
        //}

        private void UpdateLabel()
        {
            string strchk1 = "", strchk2 = "", strchk3 = "", strchk4 = "";

            if (checkBox1.Checked) strchk1 = checkBox1.Text;    // 체크박스에 체크가 되있으면 글자를 출력
            else strchk1 = "";  // 체크박스에 체크가 해제되면 글자 없애줌

            if (checkBox2.Checked) strchk2 = checkBox2.Text;
            else strchk2 = "";

            if (checkBox3.Checked) strchk3 = checkBox3.Text;
            else strchk3 = "";

            if (checkBox4.Checked) strchk4 = checkBox4.Text;
            else strchk4 = "";

            label1.Text = strchk1+ " " + strchk2 + " " + strchk3 + " " + strchk4;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLabel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "좋아하는 과일 : ";
        }
    }
}
