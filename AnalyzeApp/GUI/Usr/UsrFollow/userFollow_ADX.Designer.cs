
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_ADX
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
            this.cmbOption = new DevExpress.XtraEditors.ComboBoxEdit();
            this.nmValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(23, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(36, 19);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "ADX";
            // 
            // cmbOption
            // 
            this.cmbOption.Location = new System.Drawing.Point(84, 10);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOption.Properties.Items.AddRange(new object[] {
            "Vượt trên",
            "Giảm dưới"});
            this.cmbOption.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbOption.Size = new System.Drawing.Size(100, 20);
            this.cmbOption.TabIndex = 16;
            // 
            // nmValue
            // 
            this.nmValue.Location = new System.Drawing.Point(200, 9);
            this.nmValue.Name = "nmValue";
            this.nmValue.Size = new System.Drawing.Size(70, 20);
            this.nmValue.TabIndex = 17;
            this.nmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // userFollow_ADX
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.nmValue);
            this.Controls.Add(this.cmbOption);
            this.Controls.Add(this.labelControl1);
            this.Name = "userFollow_ADX";
            this.Size = new System.Drawing.Size(460, 42);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOption;
        private System.Windows.Forms.NumericUpDown nmValue;
    }
}
