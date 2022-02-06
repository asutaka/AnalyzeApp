using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraEditors;
using System;
using System.Data;

namespace AnalyzeApp.GUI.Usr
{
    public partial class frmCoinFollowInfo : XtraForm
    {
        private FollowModel _model = new FollowModel();
        public frmCoinFollowInfo(FollowModel model)
        {
            InitializeComponent();
            _model = model;
            InitData();
        }

        private void InitData()
        {
            LoadInternalNotify();
            SetupData();
        }

        private void LoadInternalNotify()
        {
            cmbFrequency.Properties.BeginUpdate();
            foreach (DataRow row in typeof(enumIntervalNotify).EnumToData().Rows)
            {
                cmbFrequency.Properties.Items.Add(row["Name"]);
            }
            cmbFrequency.SelectedIndex = 0;
            cmbFrequency.Properties.EndUpdate();
        }

        private void SetupData()
        {
            cmbFrequency.SelectedIndex = _model.Interval;
            chkState.IsOn = _model.IsNotify;
            foreach (var item in _model.Follows)
            {
                pnlMain.Controls.Add(new userCoinFollow(item));
            }
        }

        public FollowModel GetModel()
        {
            return _model;
        }

        private void btnAddSignal_Click(object sender, EventArgs e)
        {
            pnlMain.Controls.Add(new userCoinFollow());
        }

        private void btnOkAndSave_Click(object sender, EventArgs e)
        {
            _model.Interval = cmbFrequency.SelectedIndex;
            _model.IsNotify = chkState.IsOn;
            _model.Follows.Clear();
            if (pnlMain.Controls.Count > 0)
            {
                foreach (var item in pnlMain.Controls)
                {
                    var user = item as userCoinFollow;
                    if (user.CheckValid())
                    {
                        _model.Follows.Add(user.GetModel());
                    }
                }
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}