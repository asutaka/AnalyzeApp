using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr.UsrFollow
{
    public partial class userFollowSettingDetail : UserControl
    {
        private BarManager barManager1 = null;
        private PopupMenu popupMenu1 = null;
        private BarButtonItem btnMACD = null;
        private BarButtonItem btnMA = null;
        private BarButtonItem btnPrice = null;
        private BarButtonItem btnMCDX = null;
        private BarButtonItem btnRSI = null;
        private BarButtonItem btnADX = null;
        public userFollowSettingDetail()
        {
            InitializeComponent();
            InitControls();
        }

        private void InitControls()
        {
            barManager1 = new BarManager();
            barManager1.Form = this;

            popupMenu1 = new PopupMenu(barManager1);
            btnMACD = new BarButtonItem(barManager1, "MACD");
            btnMA = new BarButtonItem(barManager1, "MA / EMA");
            btnPrice = new BarButtonItem(barManager1, "Giá");
            btnMCDX = new BarButtonItem(barManager1, "MCDX");
            btnRSI = new BarButtonItem(barManager1, "RSI");
            btnADX = new BarButtonItem(barManager1, "ADX");
            popupMenu1.AddItem(btnMACD);
            popupMenu1.AddItem(btnMA);
            popupMenu1.AddItem(btnPrice);
            popupMenu1.AddItem(btnMCDX);
            popupMenu1.AddItem(btnRSI);
            popupMenu1.AddItem(btnADX);
            // 
            // dropDownButton1
            // 
            dropDownButton1.DropDownControl = popupMenu1;
            dropDownButton1.Click += new EventHandler(this.dropDownButton1_Click);

            // 
            // btnMACD
            // 
            btnMACD.ImageOptions.Image = Properties.Resources.green;
            btnMACD.Tag = "MACD";
            btnMACD.ItemClick += new ItemClickEventHandler(this.btnMACD_ItemClick);
            // 
            // btnMA
            // 
            btnMA.ImageOptions.Image = Properties.Resources.green;
            btnMA.Tag = "MA";
            btnMA.ItemClick += new ItemClickEventHandler(this.btnMA_ItemClick);
            // 
            // btnPrice
            // 
            btnPrice.ImageOptions.Image = Properties.Resources.green;
            btnPrice.Tag = "Price";
            btnPrice.ItemClick += new ItemClickEventHandler(this.btnPrice_ItemClick);
            // 
            // btnMCDX
            // 
            btnMCDX.ImageOptions.Image = Properties.Resources.green;
            btnMCDX.Tag = "MCDX";
            btnMCDX.ItemClick += new ItemClickEventHandler(this.btnMCDX_ItemClick);
            // 
            // btnRSI
            // 
            btnRSI.ImageOptions.Image = Properties.Resources.green;
            btnRSI.Tag = "RSI";
            btnRSI.ItemClick += new ItemClickEventHandler(this.btnRSI_ItemClick);
            // 
            // btnADX
            // 
            btnADX.ImageOptions.Image = Properties.Resources.green;
            btnADX.Tag = "ADX";
            btnADX.ItemClick += new ItemClickEventHandler(this.btnADX_ItemClick);
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
            if (tag == "MACD")
            {
                MACD();
            }
            else if (tag == "MA")
            {
                MA();
            }
            else if (tag == "Price")
            {
                Price();
            }
            else if (tag == "MCDX")
            {
                MCDX();
            }
            else if (tag == "RSI")
            {
                RSI();
            }
            else if (tag == "ADX")
            {
                ADX();
            }
        }

        private void MACD()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MACD());
        }

        private void MA()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MA());
        }

        private void Price()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_Price());
        }
        private void MCDX()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_MCDX());
        }

        private void RSI()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_RSI());
        }

        private void ADX()
        {
            ((FlowLayoutPanel)tabMain.SelectedPage.Controls[0]).Controls.Add(new userFollow_ADX());
        }
    }
}
