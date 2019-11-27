namespace DraftProject
{
    partial class ActiveProgram
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtSecret = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(131, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "کد فعال سازی برنامه";
            // 
            // txtSecret
            // 
            this.txtSecret.Location = new System.Drawing.Point(78, 35);
            this.txtSecret.Name = "txtSecret";
            this.txtSecret.Size = new System.Drawing.Size(221, 20);
            this.txtSecret.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(145, 61);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "فعال سازی";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ActiveProgram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(357, 96);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtSecret);
            this.Controls.Add(this.label1);
            this.Name = "ActiveProgram";
            this.Text = "ActiveProgram";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSecret;
        private System.Windows.Forms.Button button1;
    }
}