namespace AnalyzeApp.Model.ENTITY
{
    public class PrivateSettingModel
    {
        public PrivateTop30Model PrivateTop30 { get; set; }
        public PrivateMCDXModel PrivateMCDX { get; set; }
    }
    public class PrivateTop30Model
    {
        //Tính toán trên khung thời gian nào
        public int Interval { get; set; }
    }
    public class PrivateMCDXModel
    {
        //Tính toán trên khung thời gian nào
        public int Interval { get; set; }
    }
}
