using AnalyzeApp.Common;
using AnalyzeApp.Model.ENTITY;
using AnalyzeApp.Model.ENUM;
using System.Windows.Forms;

namespace AnalyzeApp.GUI.Usr
{
    public partial class userConfig : UserControl
    {
        private BasicSettingModel _model = Config.BasicSetting;
        public userConfig()
        {
            InitializeComponent();
            InitData();
        }

        public void ResetConfig()
        {
            cmbCandleStick.EditValue = _model.CandleStick_Value;
            nmVolume.Value = _model.Volume_Value;
            nmMA.Value = _model.MA_Value;
            nmEMA.Value = _model.EMA_Value;
            nmRSI.Value = _model.RSI_Value;
            nmADX.Value = _model.ADX_Value;
            nmMCDX.Value = _model.MCDX_Value;
            nmHighMACD.Value = _model.MACD_Value.High;
            nmLowMACD.Value = _model.MACD_Value.Low;
            nmSignal.Value = _model.MACD_Value.Signal;
            //advance
            cmbTime.EditValue = _model.TimeCalculate;
            nmRealtime.Value = _model.RealtimeInterval;
            chkPriceAction.IsOn = _model.PriceAction;
        }

        private void InitData()
        {
            cmbTime.Properties.BeginUpdate();
            cmbTime.Properties.DataSource = typeof(enumTimeZone).EnumToData();
            cmbTime.Properties.EndUpdate();
            cmbTime.EditValue = (int)enumTimeZone.OneHour;

            cmbCandleStick.Properties.BeginUpdate();
            cmbCandleStick.Properties.DataSource = typeof(enumCandleStick).EnumToData();
            cmbCandleStick.Properties.EndUpdate();
            cmbCandleStick.EditValue = (int)enumCandleStick.C;
            ResetConfig();
        }

        public BasicSettingModel GetConfigData()
        {
            var basicModel = new BasicSettingModel
            {
                CandleStick_Value = (int)cmbCandleStick.EditValue,
                Volume_Value = (int)nmVolume.Value,
                MA_Value = (int)nmMA.Value,
                EMA_Value = (int)nmEMA.Value,
                RSI_Value = (int)nmRSI.Value,
                ADX_Value = (int)nmADX.Value,
                MCDX_Value = (int)nmMCDX.Value,
                MACD_Value = new MACD_BasicSettingModel
                {
                    High = (int)nmHighMACD.Value,
                    Low = (int)nmLowMACD.Value,
                    Signal = (int)nmSignal.Value
                },
                //advance
                TimeCalculate = (int)cmbTime.EditValue,
                RealtimeInterval = (int)nmRealtime.Value,
                PriceAction = chkPriceAction.IsOn
            };
            return basicModel;
        }
    }
}
