
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollowSetting
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
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tp1 = new DevExpress.XtraTab.XtraTabPage();
            this.tp2 = new DevExpress.XtraTab.XtraTabPage();
            this.tp3 = new DevExpress.XtraTab.XtraTabPage();
            this.tp4 = new DevExpress.XtraTab.XtraTabPage();
            this.tp5 = new DevExpress.XtraTab.XtraTabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cmbFrequency = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Name1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.chkState = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFrequency.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkState.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.HeaderLocation = DevExpress.XtraTab.TabHeaderLocation.Left;
            this.tabControl.Location = new System.Drawing.Point(0, 43);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tp1;
            this.tabControl.Size = new System.Drawing.Size(650, 355);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tp1,
            this.tp2,
            this.tp3,
            this.tp4,
            this.tp5});
            // 
            // tp1
            // 
            this.tp1.ImageOptions.Image = global::AnalyzeApp.Properties.Resources._1;
            this.tp1.Name = "tp1";
            this.tp1.Size = new System.Drawing.Size(613, 347);
            // 
            // tp2
            // 
            this.tp2.ImageOptions.Image = global::AnalyzeApp.Properties.Resources._2;
            this.tp2.Name = "tp2";
            this.tp2.Size = new System.Drawing.Size(613, 347);
            // 
            // tp3
            // 
            this.tp3.Appearance.PageClient.BackColor = System.Drawing.Color.Turquoise;
            this.tp3.Appearance.PageClient.Options.UseBackColor = true;
            this.tp3.ImageOptions.Image = global::AnalyzeApp.Properties.Resources._3;
            this.tp3.Name = "tp3";
            this.tp3.Size = new System.Drawing.Size(613, 347);
            // 
            // tp4
            // 
            this.tp4.ImageOptions.Image = global::AnalyzeApp.Properties.Resources._4;
            this.tp4.Name = "tp4";
            this.tp4.Size = new System.Drawing.Size(613, 347);
            // 
            // tp5
            // 
            this.tp5.ImageOptions.Image = global::AnalyzeApp.Properties.Resources._5;
            this.tp5.Name = "tp5";
            this.tp5.Size = new System.Drawing.Size(613, 347);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.cmbFrequency);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.chkState);
            this.groupBox2.Location = new System.Drawing.Point(0, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(650, 40);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // cmbFrequency
            // 
            this.cmbFrequency.EditValue = "";
            this.cmbFrequency.Location = new System.Drawing.Point(67, 13);
            this.cmbFrequency.Name = "cmbFrequency";
            this.cmbFrequency.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbFrequency.Properties.DisplayMember = "Name";
            this.cmbFrequency.Properties.NullText = "";
            this.cmbFrequency.Properties.PopupView = this.gridLookUpEdit2View;
            this.cmbFrequency.Properties.ValueMember = "Id";
            this.cmbFrequency.Size = new System.Drawing.Size(116, 20);
            this.cmbFrequency.TabIndex = 30;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Tần suất";
            // 
            // chkState
            // 
            this.chkState.Location = new System.Drawing.Point(206, 11);
            this.chkState.Name = "chkState";
            this.chkState.Properties.OffText = "Tắt";
            this.chkState.Properties.OnText = "Bật thông báo";
            this.chkState.Size = new System.Drawing.Size(139, 24);
            this.chkState.TabIndex = 9;
            // 
            // userFollowSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.tabControl);
            this.Name = "userFollowSetting";
            this.Size = new System.Drawing.Size(650, 400);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbFrequency.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkState.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tp1;
        private DevExpress.XtraTab.XtraTabPage tp2;
        private DevExpress.XtraTab.XtraTabPage tp3;
        private DevExpress.XtraTab.XtraTabPage tp4;
        private DevExpress.XtraTab.XtraTabPage tp5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        public DevExpress.XtraEditors.ToggleSwitch chkState;
        private DevExpress.XtraEditors.GridLookUpEdit cmbFrequency;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private DevExpress.XtraGrid.Columns.GridColumn Name1;
    }
}
