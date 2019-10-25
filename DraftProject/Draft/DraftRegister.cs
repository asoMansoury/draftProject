using DraftProject.DataBase.CRUDSqliLite;
using DraftProject.DataBase.Models;
using DraftProject.users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject.Draft
{

    public partial class DraftRegister : Form
    {
        private bool isForUpdate = false;
        private int DraftID = 0;
        public DraftRegister(bool isForUpdate = false, int DraftID = 0)
        {
            InitializeComponent();
            this.isForUpdate = isForUpdate;
            this.DraftID = DraftID;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtCarTag.Text == "" || txtCertificateDriver.Text == "" || txtDate.Text == "" || txtDestination.Text == "" || txtDriver.Text == "" ||
                txtManagement.Text == "" || txtNumber.Text == "" || txtOrigin.Text == "" || txtSerial.Text == "" || txtTruck.Text == "" || txtType.Text == "" ||
                txtUserID.SelectedItem == null || txtValue.Text == "")
            {
                MessageBox.Show("اطلاعات را به صورت کامل وارد نمایید.", "خطا در ورود اطلاعات", MessageBoxButtons.OK);
                return;
            }

            var paramValues = bindFields();
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbContext context = new DbContext();
                context.InsertData(DatabaseConstantData.DraftTable, paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                List<TextBox> textBoxes = new List<TextBox>();
                textBoxes.Add(txtCarTag);
                textBoxes.Add(txtCertificateDriver);
                textBoxes.Add(txtDate);
                textBoxes.Add(txtDestination);
                textBoxes.Add(txtDriver);
                textBoxes.Add(txtManagement);
                textBoxes.Add(txtNumber);
                textBoxes.Add(txtOrigin);
                textBoxes.Add(txtSerial);
                textBoxes.Add(txtTruck);
                textBoxes.Add(txtType);
                textBoxes.Add(txtValue);
                setNullToTextBox(textBoxes);
            }
            else
            {
                return;
            }

        }

        private void setNullToTextBox(List<TextBox> button)
        {
            foreach (var item in button)
            {
                item.Text = "";
            }
        }

        private void setElementsValue(DraftModel model)
        {
            txtCarTag.Text = model.CarTag;
            txtCertificateDriver.Text = model.CertificateDriver;
            txtDate.Text = model.Date;
            txtDestination.Text = model.Destination;
            txtDriver.Text = model.Driver;
            txtManagement.Text = model.Management;
            txtNumber.Text = model.Number.ToString();
            txtOrigin.Text = model.Origin;
            txtSerial.Text = model.Serial;
            txtTruck.Text = model.Truck;
            txtType.Text = model.Type;
            txtValue.Text = model.Value.ToString();
            int index = 0;
            foreach (var item in txtUserID.Items)
            {
                var ItemModel = item as ItemModel;
                if (ItemModel.ID == model.UserID)
                {
                    break;
                }
                index++;
            }
            txtUserID.SelectedIndex = index;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCarTag.Text == "" || txtCertificateDriver.Text == "" || txtDate.Text == "" || txtDestination.Text == "" || txtDriver.Text == "" ||
                txtManagement.Text == "" || txtNumber.Text == "" || txtOrigin.Text == "" || txtSerial.Text == "" || txtTruck.Text == "" || txtType.Text == "" ||
                txtUserID.SelectedItem == null || txtValue.Text == "")
            {
                MessageBox.Show("اطلاعات را به صورت کامل وارد نمایید.", "خطا در ورود اطلاعات", MessageBoxButtons.OK);
                return;
            }


            var paramValues = bindFields();
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbContext context = new DbContext();
                context.UpdateUser(DatabaseConstantData.DraftTable, DraftID, paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void DraftRegister_Load(object sender, EventArgs e)
        {
            UsersCrud usersCrud = new UsersCrud();
            var users = usersCrud.findUsers();
            txtUserID.DisplayMember = "Name";
            txtUserID.ValueMember = "ID";
            foreach (var item in users)
            {
                var model = new ItemModel();
                model.Name = item.name + " " + item.family;
                model.ID = item.ID;
                txtUserID.Items.Add(model);
            }


            if (isForUpdate == true)
            {
                btnUpdate.Enabled = true;
                btnRegister.Enabled = false;
                DraftCrud draftCrud = new DraftCrud();
                var draft = draftCrud.findDraftByID(DraftID);
                setElementsValue(draft);
            }


        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private Dictionary<string, string> bindFields()
        {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(DraftConstantData.CarTag, txtCarTag.Text);
            paramValues.Add(DraftConstantData.CertificateDriver, txtCertificateDriver.Text);
            paramValues.Add(DraftConstantData.Date, txtDate.Text);
            paramValues.Add(DraftConstantData.Destination, txtDestination.Text);
            paramValues.Add(DraftConstantData.Driver, txtDriver.Text);
            paramValues.Add(DraftConstantData.IsBackup, false.ToString());
            paramValues.Add(DraftConstantData.Management, txtManagement.Text);
            paramValues.Add(DraftConstantData.Number, txtNumber.Text);
            paramValues.Add(DraftConstantData.Origin, txtOrigin.Text);
            paramValues.Add(DraftConstantData.Serial, txtSerial.Text);
            paramValues.Add(DraftConstantData.Truck, txtTruck.Text);
            paramValues.Add(DraftConstantData.Type, txtType.Text);
            var selectedUser = txtUserID.SelectedItem as ItemModel;
            paramValues.Add(DraftConstantData.UserID, selectedUser.ID.ToString());
            paramValues.Add(DraftConstantData.Value, txtValue.Text);


            if (isForUpdate == false)
            {
                paramValues.Add(DraftConstantData.InsertDate, DateTime.Now.Date.ToString());
                paramValues.Add(DraftConstantData.InsertBy, UserLogged.UserID.ToString());
            }else
            {
                paramValues.Add(DraftConstantData.UpdateDate, DateTime.Now.Date.ToString());
                paramValues.Add(DraftConstantData.UpdateBy, UserLogged.UserID.ToString());
            }
            return paramValues;
        }

        private void ایجادحوالهجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ویرایشحوالهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateDraft frm = new UpdateDraft();
            frm.Show();
            this.Close();
        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RegisterUserForm frm = new RegisterUserForm();
            frm.Show();
            this.Close();
        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateUsers frm = new UpdateUsers();
            frm.Show();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void عملیاتپایگاهدادهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupDatabase frm = new BackupDatabase();
            frm.ShowDialog(this);
        }
    }

}
