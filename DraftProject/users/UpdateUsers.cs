﻿using DraftProject.DataBase.CRUDSqliLite;
using DraftProject.Draft;
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
        private bool isForClosing  =false;
        public UpdateUsers()
        {
            InitializeComponent();
            isForClosing = true;
        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            RegisterUserForm registerUserForm = new RegisterUserForm();
            registerUserForm.Show();
            this.Close();
        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
        }

        private void UpdateUsers_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ( isForClosing == true)
                Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            searchGrid();
            
        }

        private void searchGrid()
        {
            var Name = txtName.Text;
            var Family = txtFamily.Text;
            var UserName = txtUserName.Text;
            var userService = new UsersCrud();
            var data = userService.findUsers(Name, Family, UserName);
            grdUsers.DataSource = data;
            grdUsers.Columns[0].HeaderText = "شناسه کاربر";
            grdUsers.Columns[1].HeaderText = "نام";
            grdUsers.Columns[2].HeaderText = "نام خانوادگی";
            grdUsers.Columns[3].HeaderText = "کد کاربر";
            grdUsers.Columns[4].HeaderText = "نام کاربری";
            grdUsers.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            grdUsers.Columns[5].Visible = false;
        }

        private void btnLogin_Click(object sender, DataGridViewCellMouseEventArgs e) {
           var row = grdUsers.Rows[e.RowIndex];
           Int32 ID = Int32.Parse(row.Cells["ID"].Value.ToString());
            RegisterUserForm registerUserForm = new RegisterUserForm(true, ID);
            registerUserForm.ShowDialog(this);
            searchGrid();
        }

        private void UpdateUsers_Load(object sender, EventArgs e)
        {
            grdUsers.CellMouseDoubleClick += btnLogin_Click;
        }

        private void ایجادحوالهجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            DraftRegister frm = new DraftRegister();
            frm.Show();
            this.Close();
        }

        private void ویرایشحوالهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            UpdateDraft frm = new UpdateDraft();
            frm.Show();
            this.Close();
        }

        private void عملیاتپایگاهدادهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            BackupDatabase frm = new BackupDatabase();
            frm.ShowDialog(this);
        }

        private void grdUsers_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            var currentRow = grdUsers.CurrentRow;
            if (currentRow != null)
            {
                int ID = Int32.Parse(currentRow.Cells[0].Value.ToString());
                var userService = new UsersCrud();
                userService.DisableUser(ID);
                MessageBox.Show("عملیات با موفقیت انجام شد");
            }
            else
            {
                MessageBox.Show("لطفا رکورد مورد نظر خود را انتخاب نمایید.");
            }
        }
    }
}
