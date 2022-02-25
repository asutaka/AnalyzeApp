using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollowSettingDetail : UserControl
    {
        private BarManager barManager1 = null;
        private PopupMenu popupMenu1 = null;
        private BarButtonItem btnADX = null;
        private BarButtonItem btnMA = null;
        private BarButtonItem btnMACD = null;
        private BarButtonItem btnMCDX = null;
        private BarButtonItem btnPrice = null;
        private BarButtonItem btnRSI = null;
        private BarButtonItem btnVolume1 = null;
        private BarButtonItem btnVolume2 = null;
       
        public FollowSettingModel _model = null;
        private FollowSettingModeModel _modelMode;
        private readonly int _num;
        private readonly bool _isFollow;
        public userFollowSettingDetail(int num, bool isFollow)
        {
            InitializeComponent();
            _num = num;
            _isFollow = isFollow;
            if (_isFollow)
                _model = Config.FollowSetting;
            else
                _model = Config.AdvanceSetting;
            InitControls();
            InitData();
        }

        public void ResetConfig()
        {
            pnl1.Controls.Clear();
            pnl2.Controls.Clear();
            pnl3.Controls.Clear();
            pnl4.Controls.Clear();
            pnl5.Controls.Clear();
            pnl6.Controls.Clear();
            InitData();
        }

        public FollowSettingModeModel GetData()
        {
            FollowSettingModeModel result = new FollowSettingModeModel();
            var title = txtTitle.Text.Trim();
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
                title = $"Chỉ báo { _num }";
            result.Title = title;
            result.PointCondition = nmPointCondition.Value;
            result.lFollowSettingModeDetail = new List<FollowSettingModeDetailModel>();
            if(pnl1.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.FifteenMinute };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl1, modelDetail));
            }
            if (pnl2.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.OneHour };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl2, modelDetail));
            }
            if (pnl3.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.FourHour };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl3, modelDetail));
            }
            if (pnl4.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.OneDay };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl4, modelDetail));
            }
            if (pnl5.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.OneWeek };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl5, modelDetail));
            }
            if (pnl6.Controls.Count > 0)
            {
                var modelDetail = new FollowSettingModeDetailModel { Interval = (int)enumInterval.OneMonth };
                result.lFollowSettingModeDetail.Add(BuildSettingModeDetail(pnl6, modelDetail));
            }
            return result;
        }

        private FollowSettingModeDetailModel BuildSettingModeDetail(FlowLayoutPanel pnl, FollowSettingModeDetailModel modelDetail)
        {
            foreach (Control item in pnl.Controls)
            {
                if (item.GetType() == typeof(userFollow_ADX))
                {
                    var userModel = item as userFollow_ADX;
                    if (modelDetail.lFollowSetting_Adx == null)
                        modelDetail.lFollowSetting_Adx = new List<FollowSetting_AdxModel>();
                    modelDetail.lFollowSetting_Adx.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_MA))
                {
                    var userModel = item as userFollow_MA;
                    if (modelDetail.lFollowSetting_Ma == null)
                        modelDetail.lFollowSetting_Ma = new List<FollowSetting_MaModel>();
                    modelDetail.lFollowSetting_Ma.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_MACD))
                {
                    var userModel = item as userFollow_MACD;
                    if (modelDetail.lFollowSetting_Macd == null)
                        modelDetail.lFollowSetting_Macd = new List<FollowSetting_MacdModel>();
                    modelDetail.lFollowSetting_Macd.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_MCDX))
                {
                    var userModel = item as userFollow_MCDX;
                    if (modelDetail.lFollowSetting_Mcdx == null)
                        modelDetail.lFollowSetting_Mcdx = new List<FollowSetting_McdxModel>();
                    modelDetail.lFollowSetting_Mcdx.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_Price))
                {
                    var userModel = item as userFollow_Price;
                    if (modelDetail.lFollowSetting_Price == null)
                        modelDetail.lFollowSetting_Price = new List<FollowSetting_PriceModel>();
                    modelDetail.lFollowSetting_Price.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_RSI))
                {
                    var userModel = item as userFollow_RSI;
                    if (modelDetail.lFollowSetting_Rsi == null)
                        modelDetail.lFollowSetting_Rsi = new List<FollowSetting_RsiModel>();
                    modelDetail.lFollowSetting_Rsi.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_Volume))
                {
                    var userModel = item as userFollow_Volume;
                    if (modelDetail.lFollowSetting_Volume == null)
                        modelDetail.lFollowSetting_Volume = new List<FollowSetting_VolumeModel>();
                    modelDetail.lFollowSetting_Volume.Add(userModel.GetData());
                }
                else if (item.GetType() == typeof(userFollow_Volume2))
                {
                    var userModel = item as userFollow_Volume2;
                    if (modelDetail.lFollowSetting_Volume2 == null)
                        modelDetail.lFollowSetting_Volume2 = new List<FollowSetting_Volume2Model>();
                    modelDetail.lFollowSetting_Volume2.Add(userModel.GetData());
                }
            }
            return modelDetail;
        }

        private void InitControls()
        {
            lblPointCondition.Visible = !_isFollow;
            nmPointCondition.Visible = !_isFollow;
            txtTitle.Visible = _isFollow;

            barManager1 = new BarManager();
            barManager1.Form = this;

            popupMenu1 = new PopupMenu(barManager1);
            btnADX = new BarButtonItem(barManager1, "ADX");
            btnMA = new BarButtonItem(barManager1, "MA / EMA");
            btnMACD = new BarButtonItem(barManager1, "MACD");
            btnMCDX = new BarButtonItem(barManager1, "MCDX");
            btnPrice = new BarButtonItem(barManager1, "Giá");
            btnRSI = new BarButtonItem(barManager1, "RSI");
            btnVolume1 = new BarButtonItem(barManager1, "Volume 1");
            btnVolume2 = new BarButtonItem(barManager1, "Volume 2");
           
            popupMenu1.AddItem(btnMACD);
            popupMenu1.AddItem(btnMA);
            popupMenu1.AddItem(btnPrice);
            popupMenu1.AddItem(btnMCDX);
            popupMenu1.AddItem(btnRSI);
            popupMenu1.AddItem(btnADX);
            popupMenu1.AddItem(btnVolume1);
            popupMenu1.AddItem(btnVolume2);
            // 
            // dropDownButton1
            // 
            dropDownButton1.DropDownControl = popupMenu1;
            dropDownButton1.Click += new EventHandler(this.dropDownButton1_Click);
            // 
            // btnADX
            // 
            btnADX.ImageOptions.Image = Properties.Resources.green;
            btnADX.Tag = "ADX";
            btnADX.ItemClick += new ItemClickEventHandler(this.btnADX_ItemClick);
            // 
            // btnMA
            // 
            btnMA.ImageOptions.Image = Properties.Resources.green;
            btnMA.Tag = "MA";
            btnMA.ItemClick += new ItemClickEventHandler(this.btnMA_ItemClick);
            // 
            // btnMACD
            // 
            btnMACD.ImageOptions.Image = Properties.Resources.green;
            btnMACD.Tag = "MACD";
            btnMACD.ItemClick += new ItemClickEventHandler(this.btnMACD_ItemClick);
            // 
            // btnMCDX
            // 
            btnMCDX.ImageOptions.Image = Properties.Resources.green;
            btnMCDX.Tag = "MCDX";
            btnMCDX.ItemClick += new ItemClickEventHandler(this.btnMCDX_ItemClick);
            // 
            // btnPrice
            // 
            btnPrice.ImageOptions.Image = Properties.Resources.green;
            btnPrice.Tag = "Price";
            btnPrice.ItemClick += new ItemClickEventHandler(this.btnPrice_ItemClick);
            // 
            // btnRSI
            // 
            btnRSI.ImageOptions.Image = Properties.Resources.green;
            btnRSI.Tag = "RSI";
            btnRSI.ItemClick += new ItemClickEventHandler(this.btnRSI_ItemClick);
            // 
            // btnVolume1
            // 
            btnVolume1.ImageOptions.Image = Properties.Resources.green;
            btnVolume1.Tag = "Volume1";
            btnVolume1.ItemClick += new ItemClickEventHandler(this.btnVolume1_ItemClick);
            // 
            // btnVolume2
            // 
            btnVolume2.ImageOptions.Image = Properties.Resources.green;
            btnVolume2.Tag = "Volume2";
            btnVolume2.ItemClick += new ItemClickEventHandler(this.btnVolume2_ItemClick);
        }

        private void InitData()
        {
            switch (_num)
            {
                case 1: _modelMode = _model.FollowSettingMode1; break;
                case 2: _modelMode = _model.FollowSettingMode2; break;
                case 3: _modelMode = _model.FollowSettingMode3; break;
                case 4: _modelMode = _model.FollowSettingMode4; break;
                case 5: _modelMode = _model.FollowSettingMode5; break;
                default: _modelMode = new FollowSettingModeModel();break;
            }
            if (_modelMode == null)
                _modelMode = new FollowSettingModeModel();
            tabMain.SelectedPageIndex = 1;
            if (string.IsNullOrWhiteSpace(_modelMode.Title))
                _modelMode.Title = $"Chỉ báo { _num }";
            txtTitle.Text = _modelMode.Title;
            var lFollowSettingModel = _modelMode.lFollowSettingModeDetail;
            if (lFollowSettingModel == null || !lFollowSettingModel.Any())
                return;
            foreach (var item in lFollowSettingModel)
            {
                InitDataTab(item);
            }
        }

        private void InitDataTab(FollowSettingModeDetailModel model)
        {
            FlowLayoutPanel pnl = null;
            switch ((enumInterval)model.Interval)
            {
                case enumInterval.FifteenMinute: pnl = pnl1;break;
                case enumInterval.OneHour: pnl = pnl2;break;
                case enumInterval.FourHour: pnl = pnl3;break;
                case enumInterval.OneDay: pnl = pnl4;break;
                case enumInterval.OneWeek: pnl = pnl5;break;
                case enumInterval.OneMonth: pnl = pnl6;break;
                default: break;
            }
            if (pnl == null)
                return;
            if(model.lFollowSetting_Adx != null && model.lFollowSetting_Adx.Any())
            {
                foreach (var item in model.lFollowSetting_Adx)
                {
                    pnl.Controls.Add(new userFollow_ADX(item, _isFollow));
                }
            }
            if (model.lFollowSetting_Ma != null && model.lFollowSetting_Ma.Any())
            {
                foreach (var item in model.lFollowSetting_Ma)
                {
                    pnl.Controls.Add(new userFollow_MA(item, _isFollow));
                }
            }
            if (model.lFollowSetting_Macd != null && model.lFollowSetting_Macd.Any())
            {
                foreach (var item in model.lFollowSetting_Macd)
                {
                    pnl.Controls.Add(new userFollow_MACD(item, _isFollow));
                }
            }
            if (model.lFollowSetting_Mcdx != null && model.lFollowSetting_Mcdx.Any())
            {
                foreach (var item in model.lFollowSetting_Mcdx)
                {
                    pnl.Controls.Add(new userFollow_MCDX(item, _isFollow));
                }
            }
            if (model.lFollowSetting_Price != null && model.lFollowSetting_Price.Any())
            {
                foreach (var item in model.lFollowSetting_Price)
                {
                    pnl.Controls.Add(new userFollow_Price(item, _isFollow));
                }
            }
            if (model.lFollowSetting_Rsi != null && model.lFollowSetting_Rsi.Any())
            {
                foreach (var item in model.lFollowSetting_Rsi)
                {
                    pnl.Controls.Add(new userFollow_RSI(item, _isFollow));
                }
            }
            foreach (Control item in pnl.Controls)
            {
                switch ((enumInterval)model.Interval)
                {
                    case enumInterval.FifteenMinute: pnl1.Controls.Add(item);break;
                    case enumInterval.OneHour: pnl2.Controls.Add(item);break;
                    case enumInterval.FourHour: pnl3.Controls.Add(item);break;
                    case enumInterval.OneDay: pnl4.Controls.Add(item);break;
                    case enumInterval.OneWeek: pnl5.Controls.Add(item);break;
                    case enumInterval.OneMonth: pnl6.Controls.Add(item);break;
                }
            }
        }

        private void btnMACD_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            MACD();
        }

        private void btnMA_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            MA();
        }

        private void btnPrice_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            Price();
        }

        private void btnMCDX_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            MCDX();
        }

        private void btnRSI_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            RSI();
        }

        private void btnVolume1_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            Volume1();
        }

        private void btnVolume2_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            Volume2();
        }

        private void btnADX_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateDropDownButton(e.Item);
            //...
            ADX();
        }

        private void UpdateDropDownButton(BarItem submenuItem)
        {
            dropDownButton1.Text = submenuItem.Caption;
            dropDownButton1.ImageOptions.SvgImage = submenuItem.ImageOptions.SvgImage;
            dropDownButton1.ImageOptions.SvgImageSize = new Size(16, 16);
            dropDownButton1.Tag = submenuItem.Tag;
        }

        private void dropDownButton1_Click(object sender, EventArgs e)
        {
            var tagObj = (sender as DropDownButton).Tag;
            if (tagObj == null)
                return;
            var tag = tagObj.ToString();
            if (tag == "ADX")
            {
                ADX();
            }
            else if (tag == "MA")
            {
                MA();
            }
            else if (tag == "MACD")
            {
                MACD();
            }
            else if (tag == "MCDX")
            {
                MCDX();
            }
            else if (tag == "Price")
            {
                Price();
            }
            else if (tag == "RSI")
            {
                RSI();
            }
            else if (tag == "Volume1")
            {
                Volume1();
            }
            else if (tag == "Volume2")
            {
                Volume2();
            }
        }

        private void MACD()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MACD(null, _isFollow));
        }

        private void MA()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MA(null, _isFollow));
        }

        private void Price()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_Price(null, _isFollow));
        }
        private void MCDX()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MCDX(null, _isFollow));
        }

        private void RSI()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_RSI(null, _isFollow));
        }

        private void ADX()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_ADX(null, _isFollow));
        }

        private void Volume1()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_Volume(null, _isFollow));
        }

        private void Volume2()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_Volume2(null, _isFollow));
        }
    }
}
