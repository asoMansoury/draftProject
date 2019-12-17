using DraftProject.Common;
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
        private int page = 0;
        StiReport stiReportResearcher;
        List<DraftModel> resultDrafts = null;
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
            page = 0;
            btnPrev.Enabled = false;
            btnNext.Enabled = true;
            searchGrid();
            


        }

        private void searchGrid(int page = 0)
        {
            var Name = txtNumber.Text;
            var Family = txtManagement.Text;
            var UserName = txtFromDate.Text;
            var userService = new DraftCrud();
            var model = new DraftModel();
            model.Number = txtNumber.Text;
            model.Management = txtManagement.Text;
            model.Date = txtFromDate.Text;
            model.CarTag = txtCarTag.Text;
            model.Driver = txtDriver.Text;
            model.Origin = txtOrigin.Text;
            model.Destination = txtDestination.Text;
            
            resultDrafts = userService.findDrafts(model,page,txtFromDate.Text,txtToDate.Text);
            if (resultDrafts == null)
                btnNext.Enabled = false;
            grdDrafts.DataSource = resultDrafts;
            if (resultDrafts != null)
            {

                grdDrafts.Columns[0].HeaderText = "شناسه حواله";
                grdDrafts.Columns[1].HeaderText = "شماره حواله";
                grdDrafts.Columns[2].HeaderText = "سریال";
                grdDrafts.Columns[3].HeaderText = "مدیریت";
                grdDrafts.Columns[4].HeaderText = "کامیون";
                grdDrafts.Columns[5].Visible = false;
                grdDrafts.Columns[6].HeaderText = "پلاک";
                grdDrafts.Columns[7].HeaderText = "راننده";
                grdDrafts.Columns[8].HeaderText = "گواهینامه";
                grdDrafts.Columns[9].HeaderText = "نوع";
                grdDrafts.Columns[10].Visible = false;
                grdDrafts.Columns[11].HeaderText = "مقدار";
                grdDrafts.Columns[12].HeaderText = "مبدا";
                grdDrafts.Columns[13].HeaderText = "مقصد";
                grdDrafts.Columns[14].HeaderText = "شناسه کاربر";
                grdDrafts.Columns[15].HeaderText = "تاریخ";
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
                stiReportResearcher = CommonUtils.ShowReport(ID);
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

        private void btnNext_Click(object sender, EventArgs e)
        {
            page++;
            searchGrid(page);
            if (page > 0)
                btnPrev.Enabled = Enabled;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if(page>0)
            {
                btnNext.Enabled = true;
                page--;
                searchGrid(page);
            }
            if (page == 0)
                btnPrev.Enabled = false;
        }

        private void btnPrintAll_Click(object sender, EventArgs e)
        {
            var Name = txtNumber.Text;
            var Family = txtManagement.Text;
            var UserName = txtFromDate.Text;
            var userService = new DraftCrud();
            var model = new DraftModel();
            model.Serial = txtNumber.Text;
            model.Management = txtManagement.Text;
            model.Date = txtFromDate.Text;
            model.CarTag = txtCarTag.Text;
            model.Driver = txtDriver.Text;
            model.Origin = txtOrigin.Text;
            model.Destination = txtDestination.Text;

            var data = userService.findAllDrafts(model, page, txtFromDate.Text, txtToDate.Text);
            var stiReport = CommonUtils.ShowReportList(data);
            stiReport.Show();
        }

        private void btnRows_Click(object sender, EventArgs e)
        {
            if (resultDrafts == null)
            {
                MessageBox.Show("رکوردی برای تهیه گزارش انتخاب نشده است", "خطا", MessageBoxButtons.OK);
                return;
            }
            var stiReport = CommonUtils.ShowReportList(resultDrafts);
            stiReport.Show();
        }
    }
}
