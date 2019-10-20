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

namespace DraftProject.users
{
    public partial class RegisterUserForm : Form
    {
        private bool isForUpdate = false;
        private int  userID = 0;
        public RegisterUserForm(bool isForUpdate=false, int UserID=0)
        {
            InitializeComponent();
            this.isForUpdate = isForUpdate;
            this.userID = UserID;
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
                context.InsertData(UsersConstantData.UsersTable,  paramValues);
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
            paramValues.Add(UsersConstantData.name, txtName.Text);
            paramValues.Add(UsersConstantData.Password, txtPassword.Text);
            paramValues.Add(UsersConstantData.Family, txtFamily.Text);
            paramValues.Add(UsersConstantData.UserCode, txtUserCode.Text);
            paramValues.Add(UsersConstantData.UserName, txtUserName.Text);
            return paramValues;
        }
        private void ثبتکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
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
                context.UpdateUser(UsersConstantData.UsersTable,userID, paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                return;
            }
        }
    }
}
