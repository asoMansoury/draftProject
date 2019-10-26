using DraftProject.DataBase.CRUDSqliLite;
using DraftProject.DataBase.Models;
using DraftProject.users;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
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

    
    public partial class UpdateDraft : Form
    {
        StiReport stiReportResearcher;
        bool isForClosing = false;
        public UpdateDraft()
        {
            InitializeComponent();
            isForClosing = true;
        }

        private void UpdateDraft_Load(object sender, EventArgs e)
        {
            grdDrafts.CellMouseDoubleClick += btnUpdate_DoubleClick;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            searchGrid();
            


        }

        private void searchGrid()
        {
            var Name = txtSerial.Text;
            var Family = txtManagement.Text;
            var UserName = txtDate.Text;
            var userService = new DraftCrud();
            var model = new DraftModel();
            model.Serial = txtSerial.Text;
            model.Management = txtManagement.Text;
            model.Date = txtDate.Text;
            model.CarTag = txtCarTag.Text;
            model.Driver = txtDriver.Text;
            model.Type = txtType.Text;
            model.Origin = txtOrigin.Text;
            model.Destination = txtDestination.Text;
            var data = userService.findDrafts(model);
            grdDrafts.DataSource = data;
            if (data != null)
            {

                grdDrafts.Columns[0].HeaderText = "شناسه حواله";
                grdDrafts.Columns[1].HeaderText = "شماره حواله";
                grdDrafts.Columns[2].HeaderText = "سریال";
                grdDrafts.Columns[3].HeaderText = "مدیریت";
                grdDrafts.Columns[4].HeaderText = "کامیون";
                grdDrafts.Columns[5].HeaderText = "پلاک";
                grdDrafts.Columns[6].HeaderText = "راننده";
                grdDrafts.Columns[7].HeaderText = "گواهینامه";
                grdDrafts.Columns[8].HeaderText = "نوع";
                grdDrafts.Columns[9].HeaderText = "مقدار";
                grdDrafts.Columns[10].HeaderText = "مبدا";
                grdDrafts.Columns[11].HeaderText = "مقصد";
                grdDrafts.Columns[12].HeaderText = "شناسه کاربر";
                grdDrafts.Columns[13].HeaderText = "تاریخ";
            }
        }


        private void btnUpdate_DoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var row = grdDrafts.Rows[e.RowIndex];
            Int32 ID = Int32.Parse(row.Cells["ID"].Value.ToString());
            DraftRegister registerUserForm = new DraftRegister(true, ID);
            registerUserForm.ShowDialog(this);
            searchGrid();
        }


        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void عملیاتپایگاهدادهToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isForClosing = false;
            BackupDatabase frm = new BackupDatabase();
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var currentRow = grdDrafts.CurrentRow;
            if (currentRow != null)
            {
                int ID = Int32.Parse(currentRow.Cells[0].Value.ToString());
                stiReportResearcher = new StiReport();
                stiReportResearcher.Load(Application.StartupPath + "\\Report.mrt");



                StiText txt_user = new StiText();
                txt_user = (StiText)stiReportResearcher.GetComponentByName("txtUserName");
                txt_user.Text = "کاربر سیستم : منصوری";

                StiText txt_nemberReport = new StiText();
                txt_nemberReport = (StiText)stiReportResearcher.GetComponentByName("txtNumber");
                txt_nemberReport.Text = "شماره گزارش : 1122";

                System.Globalization.PersianCalendar pc = new System.Globalization.PersianCalendar();
                string Date_shamsi = pc.GetYear(DateTime.Now) + "/" + pc.GetMonth(DateTime.Now) + "/" + pc.GetDayOfMonth(DateTime.Now);
                StiText txt_date = new StiText();
                txt_date = (StiText)stiReportResearcher.GetComponentByName("txtDate");
                txt_date.Text = " تاریخ : " + Date_shamsi;
                stiReportResearcher.Show();
            }
            else
            {
                MessageBox.Show("لطفا رکورد مورد نظر خود را انتخاب نمایید.");
            }
        }

        private void grdDrafts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdateDraft_FormClosed(object sender, FormClosedEventArgs e)
        {
            if ( isForClosing == true)
                Application.Exit();
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
    }
}
