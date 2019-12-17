using DraftProject.DataBase;
using DraftProject.DataBase.CRUDSqliLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using System.Globalization;
using System.Threading;
using DraftProject.Draft;
using DraftProject.Common;

namespace DraftProject.users
{
    public partial class RegisterUserForm : Form
    {
        private bool isForUpdate = false;
        private int  userID = 0;
        bool isForClosing = true;
        public RegisterUserForm(bool isForUpdate=false, int UserID=0)
        {
            InitializeComponent();
            this.isForUpdate = isForUpdate;
            this.userID = UserID;
            isForClosing = true;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" ||
                txtFamily.Text == "" ||
                txtUserCode.Text == "" ||
                txtPassword.Text == "" ||
                txtUserName.Text == "") {
                MessageBox.Show("اطلاعات را به صورت کامل وارد نمایید.","خطا در ورود اطلاعات",MessageBoxButtons.OK);
                return;
            }

            var paramValues = bindFields();
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbContext context = new DbContext();
                context.InsertData(DatabaseConstantData.UsersTable,  paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                txtName.Text = "";
                txtFamily.Text = "";
                txtUserCode.Text = "";
                txtPassword.Text = "";
                txtUserName.Text = "";
            }
            else {
                return;
            }
        }


        private Dictionary<string, string> bindFields() {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(DatabaseConstantData.name, txtName.Text);
            paramValues.Add(DatabaseConstantData.Password, CommonUtils.HashingPassword(txtPassword.Text));
            paramValues.Add(DatabaseConstantData.Family, txtFamily.Text);
            paramValues.Add(DatabaseConstantData.UserCode, txtUserCode.Text);
            paramValues.Add(DatabaseConstantData.UserName, txtUserName.Text);
            paramValues.Add(DatabaseConstantData.IsBackup, false.ToString());

            if (isForUpdate == false)
            {
                paramValues.Add(DatabaseConstantData.InsertDate, DateTime.Now.Date.ToString());
                paramValues.Add(DatabaseConstantData.InsertBy, UserLogged.UserID.ToString());
                paramValues.Add(DraftConstantData.IsActive, "1");
            }
            else
            {
                paramValues.Add(DatabaseConstantData.UpdateDate, DateTime.Now.Date.ToString());
                paramValues.Add(DatabaseConstantData.UpdateBy, UserLogged.UserID.ToString());
            }
            return paramValues;
        }
        private void ثبتکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

            isForClosing = false;
            UpdateUsers updateUsers = new UpdateUsers();
            updateUsers.Show();
            this.Close();
        }

        private void RegisterUserForm_Load(object sender, EventArgs e)
        {
            if (isForUpdate == true)
            {
                btnUpdate.Enabled = true;
                btnRegister.Enabled = false;
                UsersCrud usersCrud = new UsersCrud();
                var user = usersCrud.findUserByID(userID);
                txtName.Text = user.name;
                txtFamily.Text = user.family;
                txtUserCode.Text = user.userCode;
                txtUserName.Text = user.userName;
                txtUserName.Enabled = false;
                txtPassword.Text = user.password;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" ||
                    txtFamily.Text == "" ||
                    txtUserCode.Text == "" ||
                    txtPassword.Text == "" ||
                    txtUserName.Text == "")
            {
                MessageBox.Show("اطلاعات را به صورت کامل وارد نمایید.", "خطا در ورود اطلاعات", MessageBoxButtons.OK);
                return;
            }


            var paramValues = bindFields();
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbContext context = new DbContext();
                context.UpdateUser(DatabaseConstantData.UsersTable,userID, paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                return;
            }
        }




        private void Button1_Click(object sender, EventArgs e)
        {
            DraftRegister frm = new DraftRegister();
            frm.Show();



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

        private void RegisterUserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isForUpdate == false && isForClosing == true)
                Application.Exit();
        }
    }
}
