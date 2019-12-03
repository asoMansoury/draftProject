using DraftProject.Common;
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
        bool isForClosing = false;
        private UniqueModel uniqueModel = null;
        public DraftRegister(bool isForUpdate = false, int DraftID = 0)
        {
            InitializeComponent();
            this.isForUpdate = isForUpdate;
            this.DraftID = DraftID;
            isForClosing = true;
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


            if (ChekDateIsValid() == false)
                return;
            if (CheckIsCarTagValid() == false)
                return;

            var paramValues = bindFields();
            if (MessageBox.Show("آیا اطلاعات ذخیره گردد. ", "ثبت اطلاعات", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                DbContext context = new DbContext();
                context.InsertData(DatabaseConstantData.DraftTable, paramValues);
                MessageBox.Show("اطلاعات با موفقیت ثبت گردید", "ثبت اطلاعات", MessageBoxButtons.OK);
                DraftCrud draftCrud = new DraftCrud();
                var draft = draftCrud.findDraftByNumber(txtNumber.Text);
                var stiReport =CommonUtils.ShowReport(draft.ID);
                stiReport.Show(); 
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
                textBoxes.Add(txtValue);
                setNullToTextBox(textBoxes);

                if ( DateTime.Now.ToShortDateString() != CommonUtils.ConvertPersianToMiladiDate(uniqueModel.Date).ToShortDateString())
                    uniqueModel.UniquID = 1;
                
                else
                    uniqueModel.UniquID++;
                var paramsGenerate = bindFieldsGenerate(uniqueModel.UniquID);
                context.UpdateGenerate(DatabaseConstantData.GenerateTable, uniqueModel.ID, paramsGenerate);
                UniqueCrud uniqueCrud = new UniqueCrud();
                uniqueModel = uniqueCrud.GetLastUnique();
                fillForRegisterLoad();
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

            index = 0;
            foreach (var item in txtTruck.Items)
            {
                var ItemModel = item as ItemModel;
                if (ItemModel.ID.ToString() == model.TruckID)
                {
                    break;
                }
                index++;
            }
            txtTruck.SelectedIndex = index;

            index = 0;
            foreach (var item in txtType.Items)
            {
                var ItemModel = item as ItemModel;
                if (ItemModel.ID.ToString() == model.TypeID)
                {
                    break;
                }
                index++;
            }
            txtType.SelectedIndex = index;
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

            if (ChekDateIsValid() == false)
                return;
            if (CheckIsCarTagValid() == false)
                return;
            
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
            UniqueCrud uniqueCrud = new UniqueCrud();

            uniqueModel = uniqueCrud.GetLastUnique();
            var users = usersCrud.findUsers();
            txtUserID.DisplayMember = "Name";
            txtUserID.ValueMember = "ID";

            txtDate.Text = CommonUtils.ConvertMiladiToPersianDate(DateTime.Now.ToShortDateString());
            foreach (var item in users)
            {
                var model = new ItemModel();
                model.Name = item.name + " " + item.family;
                model.ID = item.ID;
                txtUserID.Items.Add(model);
            }

            txtTruck.DisplayMember = "Name";
            txtTruck.ValueMember = "ID";
            foreach (var item in CommonUtils.getTrucksType())
            {
                txtTruck.Items.Add(item);
            }

            txtType.DisplayMember = "Name";
            txtType.ValueMember = "ID";
            foreach (var item in CommonUtils.getTypes())
            {
                txtType.Items.Add(item);
            }


            if (isForUpdate == true)
            {
                btnUpdate.Enabled = true;
                btnRegister.Enabled = false;
                DraftCrud draftCrud = new DraftCrud();
                var draft = draftCrud.findDraftByID(DraftID);
                setElementsValue(draft);
            }


            if (isForUpdate == false)
                fillForRegisterLoad();
            
                
        }

        private void fillForRegisterLoad()
        {
            txtNumber.Text = uniqueModel.UniquID.ToString();
            txtCarTag.Text = "33ع333-55";
            txtOrigin.Text = "زرند";
            txtDestination.Text = "صبا فولاد";
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private Dictionary<string, string> bindFields()
        {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(DraftConstantData.CarTag, txtCarTag.Text);
            paramValues.Add(DraftConstantData.CertificateDriver, txtCertificateDriver.Text);
            paramValues.Add(DraftConstantData.Date,CommonUtils.ConvertPersianToMiladiDate( txtDate.Text).ToShortDateString());
            paramValues.Add(DraftConstantData.Destination, txtDestination.Text);
            paramValues.Add(DraftConstantData.Driver, txtDriver.Text);
            paramValues.Add(DraftConstantData.IsBackup, false.ToString());
            paramValues.Add(DraftConstantData.Management, txtManagement.Text);
            paramValues.Add(DraftConstantData.Number, txtNumber.Text);
            paramValues.Add(DraftConstantData.Origin, txtOrigin.Text);
            paramValues.Add(DraftConstantData.Serial, txtSerial.Text);

            var selectedTruck = txtTruck.SelectedItem as ItemModel;
            paramValues.Add(DraftConstantData.Truck, selectedTruck.ID.ToString());
            var selectedType = txtType.SelectedItem as ItemModel;
            paramValues.Add(DraftConstantData.Type, selectedType.ID.ToString());
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


        private Dictionary<string, string> bindFieldsGenerate(int UniqueID)
        {
            var paramValues = new Dictionary<string, string>();
            paramValues.Add(GenerateUniqueConst.Date, CommonUtils.ConvertMiladiToPersianDate(DateTime.Now.ToShortDateString()));
            paramValues.Add(GenerateUniqueConst.UniquID, UniqueID.ToString());
            return paramValues;
        }

        private void ایجادحوالهجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
        }

        private void ویرایشحوالهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            UpdateDraft frm = new UpdateDraft();
            frm.Show();
            this.Close();
        }

        private void ایجادکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            RegisterUserForm frm = new RegisterUserForm();
            frm.Show();
            this.Close();
        }

        private void ویرایشکاربرجدیدToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            UpdateUsers frm = new UpdateUsers();
            frm.Show();
            this.Close();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void عملیاتپایگاهدادهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            BackupDatabase frm = new BackupDatabase();
            frm.ShowDialog(this);
        }

        private void DraftRegister_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isForUpdate == false&&isForClosing==true)
                Application.Exit();
        }

        private void txtDate_LostFocus(object sender, EventArgs e)
        {
            if (ChekDateIsValid() == false)
                return;

            
        }

        private bool ChekDateIsValid()
        {
            string Date = txtDate.Text;
            try
            {
                DateTime dt = DateTime.Parse(Date);
            }
            catch (Exception)
            {

                MessageBox.Show("فرمت تاریخ باید به صورت روبرو و شمسی باشد." + " YYYY/MM/DD");
                return false;   
            }
            return true;
        }

        private void txtCarTag_LostFocus(object sender, EventArgs e)
        {
            if(CheckIsCarTagValid() == false){
                return;
            }
        }

        private bool CheckIsCarTagValid()
        {
            if (CommonUtils.CheckRegex(txtCarTag.Text, @"\d{2}.{1}\d{3}-\d{2}") == false)
            {
                MessageBox.Show("فرمت پلاک وارد شده میباشد به صورت روبرو باشد. : NNWNNN-NN");
                return false;
            }
            return true;
        }

        private void txtUserID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtValue_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }

}
