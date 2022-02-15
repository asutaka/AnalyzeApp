
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_RSI
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOption = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(200, 9);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(70, 20);
            this.numericUpDown1.TabIndex = 11;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown1.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(23, 10);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(29, 19);
            this.labelControl1.TabIndex = 10;
            this.labelControl1.Text = "RSI";
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
            this.cmbOption.TabIndex = 17;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = global::AnalyzeApp.Properties.Resources.cancel;
            this.btnDelete.Location = new System.Drawing.Point(426, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // userFollow_RSI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.cmbOption);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.labelControl1);
            this.Name = "userFollow_RSI";
            this.Size = new System.Drawing.Size(460, 42);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOption;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
    }
}
