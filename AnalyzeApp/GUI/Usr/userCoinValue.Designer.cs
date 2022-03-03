
namespace AnalyzeApp.GUI.Usr
{
    partial class userCoinValue
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
            this.label1 = new System.Windows.Forms.Label();
            this.picOption = new System.Windows.Forms.PictureBox();
            this.lblClose = new System.Windows.Forms.LinkLabel();
            this.cmbOption = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Name1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // nmValue
            // 
            this.nmValue.DecimalPlaces = 8;
            this.nmValue.Location = new System.Drawing.Point(297, 12);
            this.nmValue.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nmValue.Name = "nmValue";
            this.nmValue.Size = new System.Drawing.Size(95, 20);
            this.nmValue.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(257, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Giá trị";
            // 
            // picOption
            // 
            this.picOption.Image = global::AnalyzeApp.Properties.Resources.up_24x24;
            this.picOption.Location = new System.Drawing.Point(15, 11);
            this.picOption.Name = "picOption";
            this.picOption.Size = new System.Drawing.Size(21, 21);
            this.picOption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picOption.TabIndex = 12;
            this.picOption.TabStop = false;
            // 
            // lblClose
            // 
            this.lblClose.AutoSize = true;
            this.lblClose.Location = new System.Drawing.Point(408, 14);
            this.lblClose.Name = "lblClose";
            this.lblClose.Size = new System.Drawing.Size(33, 13);
            this.lblClose.TabIndex = 17;
            this.lblClose.TabStop = true;
            this.lblClose.Text = "Close";
            this.lblClose.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblClose_LinkClicked);
            // 
            // cmbOption
            // 
            this.cmbOption.EditValue = "";
            this.cmbOption.Location = new System.Drawing.Point(42, 12);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOption.Properties.DisplayMember = "Name";
            this.cmbOption.Properties.NullText = "";
            this.cmbOption.Properties.PopupView = this.gridLookUpEdit2View;
            this.cmbOption.Properties.ValueMember = "Id";
            this.cmbOption.Size = new System.Drawing.Size(100, 20);
            this.cmbOption.TabIndex = 28;
            this.cmbOption.EditValueChanged += new System.EventHandler(this.cmbOption_EditValueChanged);
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.Id,
            this.Name1});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            // 
            // Id
            // 
            this.Id.Caption = "Id";
            this.Id.FieldName = "Id";
            this.Id.Name = "Id";
            this.Id.UnboundType = DevExpress.Data.UnboundColumnType.Integer;
            // 
            // Name1
            // 
            this.Name1.Caption = "Name";
            this.Name1.FieldName = "Name";
            this.Name1.Name = "Name1";
            this.Name1.OptionsColumn.ShowCaption = false;
            this.Name1.UnboundType = DevExpress.Data.UnboundColumnType.String;
            this.Name1.Visible = true;
            this.Name1.VisibleIndex = 0;
            // 
            // userCoinValue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbOption);
            this.Controls.Add(this.lblClose);
            this.Controls.Add(this.picOption);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nmValue);
            this.Name = "userCoinValue";
            this.Size = new System.Drawing.Size(450, 42);
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.NumericUpDown nmValue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picOption;
        private System.Windows.Forms.LinkLabel lblClose;
        private DevExpress.XtraEditors.GridLookUpEdit cmbOption;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private DevExpress.XtraGrid.Columns.GridColumn Name1;
    }
}
