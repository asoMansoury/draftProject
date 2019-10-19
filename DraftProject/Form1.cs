using DraftProject.DataBase;
using DraftProject.users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txtUser.Text;
            var password =txtPassword.Text;
            if (userName == ""||
                    password == "") {
                MessageBox.Show("نام کاربر یا کلمه عبور وارد نشده است","ورود",MessageBoxButtons.OK);
                return;
            }
            DraftContentEntities db = new DraftContentEntities();
            if (db.Users.Any(r => r.UserName == userName && password == r.Password))
            {
                RegisterUserForm frmUser = new RegisterUserForm();
                frmUser.Show();
                this.Hide();
                return;
            }
            else {
                MessageBox.Show("نام کاربر یا کلمه عبور اشتباه است", "ورود", MessageBoxButtons.OK);
                return;
            }


        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
