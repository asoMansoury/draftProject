using DraftProject.Common;
using DraftProject.DataBase.CRUDSqliLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DraftProject
{
    public partial class ActiveProgram : Form
    {
        public bool isActive = false;
        public ActiveProgram()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DbContext db = new DbContext();
            UsersCrud usersCrud = new UsersCrud();
            var lstSecrets = usersCrud.GetAllSecretCodes();
            if (lstSecrets.Any(r => r.Key == txtSecret.Text))
            {
                var itemSecret = lstSecrets.Where(z => z.Key == txtSecret.Text).FirstOrDefault();
                int Year = CommonUtils.ConvertMiladiToPersianDateGetYear(DateTime.Now.ToString());

                if (Int32.Parse(itemSecret.FromDate) == Year)
                {
                    if (itemSecret.IsActive == 0)
                    {
                        if (db.ActiveSecretCode(txtSecret.Text) == true)
                        {
                            MessageBox.Show("فعال سازی برای سال جاری با موفقیت انجام گردید.");
                            isActive = true;
                            this.Close();
                        };
                    }
                    else
                    {
                        MessageBox.Show("کد وارد شده قبلا وارد شده و فعال گردیده است");
                        isActive = true;
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("کد فعال سازی صحیح نمی باشد.");
                    return;
                }


            }
            else
            {
                MessageBox.Show("کد فعال سازی صحیح نمی باشد.");
                return;
            }

        }
    }
}
