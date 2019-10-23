using DraftProject.DataBase.CRUDSqliLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject.users
{
    public partial class BackupDatabase : Form
    {
        public BackupDatabase()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dbFilePath = Application.StartupPath + "\\sqlLiteDbDraft.db";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = @"C:\";
            saveFileDialog.Title = "ایجاد پشتیبان از دیتابیس";
            saveFileDialog.Filter = "Database File (*.db)|*.db";
            var dialogResult =  saveFileDialog.ShowDialog();
            if(dialogResult == DialogResult.OK)
            {
                string Path = saveFileDialog.FileName;
                File.Copy(dbFilePath, Path);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"C:\";
            openFileDialog.Title = "بارگزاری نسخه دیتابیس";
            openFileDialog.Filter = "Database File (*.db)|*.db";
            if (openFileDialog.ShowDialog()== DialogResult.OK)
            {
                string path = openFileDialog.FileName;
                var resultMsg = MessageBox.Show("آیا میخواهید عملیات بارگزاری انجام شود?", "بارگزارش", MessageBoxButtons.YesNo);
                if(resultMsg == DialogResult.Yes)
                {
                    DraftCrud draftCrud = new DraftCrud();
                    var draftRecords = draftCrud.GetDrafts(path);
                    draftCrud.saveIntoDraftTable(draftRecords);

                    UsersCrud usersCrud = new UsersCrud();
                    var userRecords = usersCrud.GetUsers(path);
                    usersCrud.saveIntoDraftTable(userRecords);
                    MessageBox.Show("عملیات بارگذاری با موفقیت تکمیل گردید.");
                }
            }
        }

        private void BackupDatabase_Load(object sender, EventArgs e)
        {

        }
    }
}
