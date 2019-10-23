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
            Draft.DraftRegister frmUser1 = new Draft.DraftRegister();
            frmUser1.Show();
            this.Hide();
            return;
            var userName = txtUser.Text;
            var password =txtPassword.Text;
            if (userName == ""||
                    password == "") {
                MessageBox.Show("نام کاربر یا کلمه عبور وارد نشده است","ورود",MessageBoxButtons.OK);
                return;
            }
            var userContext = new UsersCrud();
            var user = userContext.findUser(userName,password);
            if (user!=null)
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
