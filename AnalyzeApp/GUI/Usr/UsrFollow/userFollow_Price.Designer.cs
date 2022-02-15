
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_Price
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
            this.nmValue = new System.Windows.Forms.NumericUpDown();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOption = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbMode = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // nmValue
            // 
            this.nmValue.Location = new System.Drawing.Point(286, 9);
            this.nmValue.Name = "nmValue";
            this.nmValue.Size = new System.Drawing.Size(70, 20);
            this.nmValue.TabIndex = 5;
            this.nmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(23, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(27, 19);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "Giá";
            // 
            // cmbOption
            // 
            this.cmbOption.Location = new System.Drawing.Point(84, 10);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOption.Properties.Items.AddRange(new object[] {
            "Cắt lên",
            "Cắt xuống"});
            this.cmbOption.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbOption.Size = new System.Drawing.Size(100, 20);
            this.cmbOption.TabIndex = 15;
            // 
            // cmbMode
            // 
            this.cmbMode.Location = new System.Drawing.Point(216, 10);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbMode.Properties.Items.AddRange(new object[] {
            "MA",
            "EMA"});
            this.cmbMode.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbMode.Size = new System.Drawing.Size(70, 20);
            this.cmbMode.TabIndex = 16;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = global::AnalyzeApp.Properties.Resources.cancel;
            this.btnDelete.Location = new System.Drawing.Point(426, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 23);
            this.btnDelete.TabIndex = 17;
            // 
            // userFollow_Price
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cmbMode);
            this.Controls.Add(this.cmbOption);
            this.Controls.Add(this.nmValue);
            this.Controls.Add(this.labelControl1);
            this.Name = "userFollow_Price";
            this.Size = new System.Drawing.Size(460, 42);
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbMode.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmValue;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOption;
        private DevExpress.XtraEditors.ComboBoxEdit cmbMode;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}
