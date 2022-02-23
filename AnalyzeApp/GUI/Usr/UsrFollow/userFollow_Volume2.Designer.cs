
namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    partial class userFollow_Volume2
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
            this.nmPoint = new System.Windows.Forms.NumericUpDown();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.nmValue = new System.Windows.Forms.NumericUpDown();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cmbOption = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Name1 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.nmPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            this.SuspendLayout();
            // 
            // nmPoint
            // 
            this.nmPoint.Location = new System.Drawing.Point(383, 11);
            this.nmPoint.Name = "nmPoint";
            this.nmPoint.Size = new System.Drawing.Size(55, 20);
            this.nmPoint.TabIndex = 33;
            this.nmPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = global::AnalyzeApp.Properties.Resources.cancel;
            this.btnDelete.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.btnDelete.Location = new System.Drawing.Point(439, 10);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(25, 23);
            this.btnDelete.TabIndex = 32;
            // 
            // nmValue
            // 
            this.nmValue.Location = new System.Drawing.Point(145, 11);
            this.nmValue.Name = "nmValue";
            this.nmValue.Size = new System.Drawing.Size(55, 20);
            this.nmValue.TabIndex = 31;
            this.nmValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nmValue.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(6, 13);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(28, 16);
            this.labelControl1.TabIndex = 30;
            this.labelControl1.Text = "Vol2";
            // 
            // cmbOption
            // 
            this.cmbOption.EditValue = "";
            this.cmbOption.Location = new System.Drawing.Point(46, 12);
            this.cmbOption.Name = "cmbOption";
            this.cmbOption.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbOption.Properties.DisplayMember = "Name";
            this.cmbOption.Properties.NullText = "";
            this.cmbOption.Properties.PopupView = this.gridLookUpEdit2View;
            this.cmbOption.Properties.ValueMember = "Id";
            this.cmbOption.Size = new System.Drawing.Size(100, 20);
            this.cmbOption.TabIndex = 34;
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
            // userFollow_Volume2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.Controls.Add(this.nmPoint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.nmValue);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.cmbOption);
            this.Name = "userFollow_Volume2";
            this.Size = new System.Drawing.Size(470, 42);
            ((System.ComponentModel.ISupportInitialize)(this.nmPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nmValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbOption.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nmPoint;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private System.Windows.Forms.NumericUpDown nmValue;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevExpress.XtraEditors.GridLookUpEdit cmbOption;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn Id;
        private DevExpress.XtraGrid.Columns.GridColumn Name1;
    }
}
