using Newtonsoft.Json;
using System.Collections.Generic;

namespace AnalyzeApp.Model.ENTITY
{
    public class CoinDeptModel
    {
		[JsonProperty(PropertyName = "bids")]
		public dynamic bidsJson { get; set; }

		private List<(double, double)> _bids;
		public List<(double, double)> Bids
		{
			get
			{
				if (_bids == null) _bids = new List<(double, double)>();
				_bids.Clear();
				if (asksJson is Newtonsoft.Json.Linq.JArray)
				{
					foreach (var price in bidsJson)
					{
						_bids.Add(((double)price[0], (double)price[1]));
					}
				}
				return _bids;
			}
		}

		[JsonProperty(PropertyName = "asks")]
		public dynamic asksJson { get; set; }

		private List<(double, double)> _asks;
		public List<(double, double)> Asks
		{
			get
			{
				if (_asks == null) _asks = new List<(double, double)>();
				_asks.Clear();
				if (asksJson is Newtonsoft.Json.Linq.JArray)
				{
					foreach (var price in asksJson)
					{
						_asks.Add(((double)price[0], (double)price[1]));
					}
				}
				return _asks;
			}
		}
	}
}
