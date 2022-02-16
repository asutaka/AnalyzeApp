
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_MCDX
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkValid = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.chkValid.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(23, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(49, 19);
            this.labelControl1.TabIndex = 17;
            this.labelControl1.Text = "MCDX";
            // 
            // chkValid
            // 
            this.chkValid.EditValue = true;
            this.chkValid.Location = new System.Drawing.Point(84, 10);
            this.chkValid.Name = "chkValid";
            this.chkValid.Properties.OffText = "Off";
            this.chkValid.Properties.OnText = "On";
            this.chkValid.Size = new System.Drawing.Size(95, 24);
            this.chkValid.TabIndex = 18;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = global::AnalyzeApp.Properties.Resources.cancel;
            this.btnDelete.Location = new System.Drawing.Point(426, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 23);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // userFollow_MCDX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.chkValid);
            this.Controls.Add(this.labelControl1);
            this.Name = "userFollow_MCDX";
            this.Size = new System.Drawing.Size(470, 42);
            ((System.ComponentModel.ISupportInitialize)(this.chkValid.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ToggleSwitch chkValid;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}
