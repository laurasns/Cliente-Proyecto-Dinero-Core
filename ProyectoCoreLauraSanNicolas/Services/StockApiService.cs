using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace ProyectoCoreLauraSanNicolas.Services
{
    public class StockApiService
    {
        public Dictionary<TimeSerie, string> TimeSeries = new Dictionary<TimeSerie, string>()
        {
            { TimeSerie.INTRADAY,"TIME_SERIES_INTRADAY" },
            { TimeSerie.DAILY,"TIME_SERIES_DAILY" },
            { TimeSerie.WEEKLY,"TIME_SERIES_WEEKLY" },
            { TimeSerie.MONTHLY,"TIME_SERIES_MONTHLY" }
        };

        public Dictionary<string, double> ConvertStockDataToHighChartJson(TimeSerie timeSerie, string symbolName, int resultsCount)
        {
            Dictionary<string, double> jsonForStockComponent = new Dictionary<string, double>();

            string timeSerieValue = TimeSeries.FirstOrDefault(o => o.Key == timeSerie).Value;
            string stockJson;
            if (symbolName == "USD" || symbolName == "GBP")
            {
                using (WebClient wc = new WebClient())
                {
                    stockJson = wc.DownloadString($"https://www.alphavantage.co/query?function=FX_DAILY&from_symbol=EUR&to_symbol={symbolName}&apikey=YFBDJX9GUMLZZ2YP");
                }
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    stockJson = wc.DownloadString($"https://www.alphavantage.co/query?function={timeSerieValue}&symbol={symbolName}&apikey=YFBDJX9GUMLZZ2YP");
                }
            }

            dynamic jsonObj = JObject.Parse(stockJson);
            dynamic stockData = null;

            foreach (JProperty prop in jsonObj)
            {
                if (prop.Name.Contains("Time Series"))
                {
                    stockData = prop.Value;
                }
            }

            if (stockData == null)
            {
                throw new Exception("error requesting time series");
            }

            int count = 0;
            foreach (JProperty serie in stockData)
            {
                if (count >= resultsCount) break;
                
                string date = serie.Name;
                double stockValue = 0;
                
                foreach (JProperty value in serie.Value)
                {
                    if (value.Name.Contains("close"))
                    {
                        stockValue = Math.Round((double)value.Value, 2);
                    }
                }
                jsonForStockComponent.Add(date, stockValue);

                count++;
            }

            return jsonForStockComponent;
        }

        public enum TimeSerie
        {
            INTRADAY,
            DAILY,
            WEEKLY,
            MONTHLY
        }
       
    }
}