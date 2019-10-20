using DraftProject.DataBase.CRUDSqliLite;
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
    public partial class UpdateUsers : Form
    {
        public UpdateUsers()
        {
            InitializeComponent();
        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterUserForm registerUserForm = new RegisterUserForm();
            registerUserForm.Show();
            this.Close();
        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void UpdateUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var Name = txtName.Text;
            var Family = txtFamily.Text;
            var UserName = txtUserName.Text;
            var userService = new UsersCrud();
            var data = userService.findUsers(Name,Family,UserName);
            grdUsers.DataSource = data;
            grdUsers.Columns[0].HeaderText = "شناسه کاربر";
            grdUsers.Columns[1].HeaderText = "نام";
            grdUsers.Columns[2].HeaderText = "نام خانوادگی";
            grdUsers.Columns[3].HeaderText = "کد کاربر";
            grdUsers.Columns[4].HeaderText = "نام کاربری";
            grdUsers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdUsers.Columns[5].Visible = false;
            grdUsers.CellMouseDoubleClick += btnLogin_Click;
        }

        private void btnLogin_Click(object sender, DataGridViewCellMouseEventArgs e) {
           var row = grdUsers.Rows[e.RowIndex];
           Int32 ID = Int32.Parse(row.Cells["ID"].Value.ToString());
            RegisterUserForm registerUserForm = new RegisterUserForm(true, ID);
            registerUserForm.ShowDialog(this);
        }

        private void UpdateUsers_Load(object sender, EventArgs e)
        {

        }
    }
}
