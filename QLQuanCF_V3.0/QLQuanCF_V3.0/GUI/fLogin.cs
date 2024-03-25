using QLQuanCF_V1._0.DAO;
using QLQuanCF_V1._0.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLQuanCF_V3._0
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Xử lý sự kiện event form
            if (MessageBox.Show("Bạn có thực sự muốn thoát chương tình", "Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            txbPassWord.Text = "";
            txbUserName.Text = "";
        }

        private void btnDangNhap_Click_1(object sender, EventArgs e)
        {
            string userName = txbUserName.Text;
            string passWord = txbPassWord.Text;
            if (this.txbUserName.TextLength == 0 && this.txbPassWord.TextLength == 0)
                MessageBox.Show("Bạn chưa nhập tên đăng nhập và mật khẩu", "Thông báo");
            else
                if (this.txbUserName.TextLength == 0)
                    MessageBox.Show("Tên đăng nhập không được bỏ trống", "Thông báo");
                else
                    if (this.txbPassWord.TextLength == 0)
                        MessageBox.Show("Mật khẩu không được bỏ trống", "Thông báo");
                    else
                        //if (Login(userName, passWord))
                        {
                            Account loginAccount = AccountDAO.Instance.GetAccountByUserName(userName);
                            fTableManager f = new fTableManager(loginAccount);
                            this.Hide();
                            f.ShowDialog();//fTableManager trở thành form chính
                            this.Show();
                        }
                        //else
                     //   {
                        //    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu. Vui lòng thử lại", "Thông báo");
                       // }
        }
        bool Login(string userName, string passWord)
        {
            return AccountDAO.Instance.Login(userName, passWord);
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkMatKhau_CheckedChanged_1(object sender, EventArgs e)
        {
            if (chkMatKhau.Checked)
            {
                txbPassWord.UseSystemPasswordChar = false;
            }
            else
            {
                txbPassWord.UseSystemPasswordChar = true;
            }
        }

        private void txbUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
