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
    public partial class UpdateDraft : Form
    {
        public UpdateDraft()
        {
            InitializeComponent();
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
            BackupDatabase frm = new BackupDatabase();
            frm.ShowDialog(this);
        }
    }
}
