
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_MACD
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
            this.components = new System.ComponentModel.Container();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbOption = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.nmRatioMax = new System.Windows.Forms.NumericUpDown();
            this.nmPoint = new System.Windows.Forms.NumericUpDown();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRatioMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(10, 11);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(38, 16);
            this.labelControl1.TabIndex = 15;
            this.labelControl1.Text = "MACD";
            // 
            // cmbOption
            // 
            this.cmbOption.Location = new System.Drawing.Point(50, 10);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOption.Properties.Items.AddRange(new object[] {
            "Cắt lên",
            "Cắt xuống",
            "Gần cắt lên",
            "Gần cắt xuống"});
            this.cmbOption.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cmbOption.Size = new System.Drawing.Size(100, 20);
            this.cmbOption.TabIndex = 14;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = global::AnalyzeApp.Properties.Resources.cancel;
            this.btnDelete.Location = new System.Drawing.Point(443, 8);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 23);
            this.btnDelete.TabIndex = 18;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // nmRatioMax
            // 
            this.nmRatioMax.DecimalPlaces = 1;
            this.nmRatioMax.Location = new System.Drawing.Point(148, 9);
            this.nmRatioMax.Name = "nmRatioMax";
            this.nmRatioMax.Size = new System.Drawing.Size(55, 20);
            this.nmRatioMax.TabIndex = 22;
            this.nmRatioMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // nmPoint
            // 
            this.nmPoint.Location = new System.Drawing.Point(387, 9);
            this.nmPoint.Name = "nmPoint";
            this.nmPoint.Size = new System.Drawing.Size(55, 20);
            this.nmPoint.TabIndex = 23;
            this.nmPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // toolTip1
            // 
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ToolTipTitle = "Chỉ báo MACD";
            // 
            // userFollow_MACD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.nmPoint);
            this.Controls.Add(this.nmRatioMax);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbOption);
            this.Name = "userFollow_MACD";
            this.Size = new System.Drawing.Size(470, 42);
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmRatioMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmPoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbOption;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.NumericUpDown nmRatioMax;
        private System.Windows.Forms.NumericUpDown nmPoint;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
