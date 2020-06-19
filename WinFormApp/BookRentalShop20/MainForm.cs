using System;
using System.Linq.Expressions;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace BookRentalShop20
{
    public partial class MainForm : MetroForm
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            //loginForm.Focus();
        }

        private void MainForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            if (MetroMessageBox.Show(this, "정말 종료하시겠습니까?", "종료",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (Form item in this.MdiChildren)
                {
                    item.Close();
                }
                e.Cancel = false;   //false는 취소가 되지 않았다는 뜻
            }
            else
            {
                e.Cancel = true;    //true가 취소 됬다는 뜻
            }

        }

        private void InitChildForm(Form form, string strFormTitle)
        {
            form.Text = strFormTitle;
            form.Dock = DockStyle.Fill; //화면을 꽉 채움
            form.MdiParent = this;  //
            form.Show();
            form.WindowState = FormWindowState.Maximized;
        }
        
        private void MnuItemDivMng_Click(object sender, EventArgs e)    //구분코드관리
        {
            DivForm form = new DivForm();
            InitChildForm(form, "구분코드 관리");      
        }

        private void 사용자관리UToolStripMenuItem_Click(object sender, EventArgs e)  //사용자폼 관리
        {
            UserForm form = new UserForm();
            InitChildForm(form, "사용자 관리");

        }


    }
}
