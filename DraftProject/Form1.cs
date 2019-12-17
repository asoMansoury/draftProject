using DraftProject.Common;
using DraftProject.DataBase;
using DraftProject.DataBase.CRUDSqliLite;
using DraftProject.users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace DraftProject
{

    public partial class Form1 : Form
    {
        private SQLiteConnection sql_con;
        private SQLiteCommand sql_cmd;
        private SQLiteDataAdapter DB;
        private DataSet DS = new DataSet();
        private DataTable DT = new DataTable();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userContext = new UsersCrud();
            if (userContext.CheckProgramIsActive()==false){
                MessageBox.Show("برنامه قفل می باشد، لطفا برای فعال سازی برنامه کد خریداری شده را وارد نمایید.");
                button1.Enabled = true;
                return;
            }
            var userName = txtUser.Text;
            var password =txtPassword.Text;
            if (userName == ""||
                    password == "") {
                MessageBox.Show("نام کاربر یا کلمه عبور وارد نشده است","ورود",MessageBoxButtons.OK);
                return;
            }
            
            var user = userContext.findUser(userName,CommonUtils.HashingPassword(password));
            if (user!=null)
            {
                if (user.IsActive == 1)
                {
                    UserLogged.UserID = user.ID;
                    UserLogged.UserName = user.userName;
                    CommonUtils.uniqeUserID = user.ID.ToString();
                    RegisterUserForm frmUser = new RegisterUserForm();
                    frmUser.Show();
                    this.Hide();
                    return;
                }
                else
                {
                    MessageBox.Show("نام کاربر یا کلمه عبور اشتباه است", "ورود", MessageBoxButtons.OK);
                    return;
                }

            }
            else {
                MessageBox.Show("نام کاربر یا کلمه عبور اشتباه است", "ورود", MessageBoxButtons.OK);
                return;
            }


        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ActiveProgram activeProgram = new ActiveProgram();
            activeProgram.ShowDialog(this);
            if (activeProgram.isActive == true) {
                button1.Enabled = false;
            }
        }
    }
}
