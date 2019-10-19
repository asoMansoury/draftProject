using DraftProject.DataBase;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject.users
{
    public partial class RegisterUserForm : Form
    {
        public RegisterUserForm()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            DraftContentEntities db = new DraftContentEntities();
            var userInformation = new User();
            if (txtName.Text == "" ||
                txtFamily.Text == "" ||
                txtUserCode.Text == "" ||
                txtPassword.Text == "" ||
                txtUserName.Text == "") {
                MessageBox.Show("اطلاعات را به صورت کامل وارد نمایید.","خطا در ورود اطلاعات",MessageBoxButtons.OK);
                return;
            }
            userInformation.Name = txtName.Text;
            userInformation.Family = txtFamily.Text;
            userInformation.UserCode = txtUserCode.Text;
            userInformation.Password = txtPassword.Text;
            userInformation.UserName = txtUserName.Text;
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                db.AddToUsers(userInformation);
                db.SaveChanges();
                txtName.Text = "";
                txtFamily.Text = "";
                txtUserCode.Text = "";
                txtPassword.Text = "";
                txtUserName.Text = "";
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید","ثبت اطلاعات",MessageBoxButtons.OK);
            }
            else {
                return;
            }
        }
    }
}
