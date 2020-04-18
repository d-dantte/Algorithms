using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Codility01.StockQuery
{
    class Challenge
    {
        public static void openAndClosePrices(string firstDate, string lastDate, string weekDay)
        {
            var start = DateTime.Parse(firstDate);
            var end = DateTime.Parse(lastDate);
            var day = Helper.ParseDayOfWeek(weekDay);
            var stocks = new List<StockValue>();
            var hasPassedEndDate = false;
            var page = 1;

            using (var service = new StockAccessService())
            {
                while (!hasPassedEndDate && service.TryGetStocks(page++, out var stockData))
                {
                    foreach (var stockValue in stockData.Data)
                    {
                        if (stockValue.Date == null || stockValue.Date < start)
                            continue;

                        else if (stockValue.Date > end)
                        {
                            hasPassedEndDate = true;
                            break;
                        }

                        else if (stockValue.Date.Value.DayOfWeek == day)
                            stocks.Add(stockValue);
                    }
                }

                stocks.ForEach(stock => Console.WriteLine(stock.OpenClose()));
            }
        }

        class StockValue
        {
            public decimal? Open { get; set; }
            public decimal? Close { get; set; }
            public decimal? High { get; set; }
            public decimal? Low { get; set; }
            public DateTime? Date { get; set; }

            public string OpenClose() => $"{Helper.ToStockDate(Date)} {Open} {Close}";
        }

        class StockData
        {
            public int Page { get; set; }
            public int TotalPages { get; set; }
            public int PerPage { get; set; }
            public int Total { get; set; }
            public StockValue[] Data { get; set; }
        }

        class StockAccessService : IDisposable
        {
            private HttpClient _client = new HttpClient();

            #region Dispose
            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    _client.Dispose();
                }
            }
            #endregion

            public bool TryGetStocks(int page, out StockData data) => TryGetStocks(page, null, out data);

            public bool TryGetStocks(int page, StockValue searchParameter, out StockData data)
            {
                try
                {
                    var query = GetQuery(page, searchParameter);
                    var url = $"https://jsonmock.hackerrank.com/api/stocks/search/?{query}";

                    var json = _client.GetStringAsync(url).Result; //make it synchronious

                    data = JsonConvert.DeserializeObject<StockData>(json.Replace("_",""));

                    return data.Data.Length > 0;
                }
                catch
                {
                    data = new StockData
                    {
                        Data = new StockValue[0],
                        Page = 1,
                        PerPage = 0,
                        Total = 0,
                        TotalPages = 0
                    };

                    return false;
                }
            }

            private string GetQuery(int page, StockValue data)
            {
                var query = "";

                if (page >= 1)
                    query += ("page=" + page);

                if (data != null)
                {
                    if (data.Close != null)
                        query += ((query.Length > 0 ? "&" : "") + "close=" + data.Close);

                    if (data.Open != null)
                        query += ((query.Length > 0 ? "&" : "") + "open=" + data.Open);

                    if (data.High != null)
                        query += ((query.Length > 0 ? "&" : "") + "high=" + data.High);

                    if (data.Low != null)
                        query += ((query.Length > 0 ? "&" : "") + "low=" + data.Low);

                    if (data.Date != null)
                        query += ((query.Length > 0 ? "&" : "") + "date=" + Helper.ToStockDate(data.Date));
                }

                return query.ToString();
            }
        }

        static class Helper
        {
            public static DayOfWeek ParseDayOfWeek(string weekDay)
                => (DayOfWeek)Enum.Parse(typeof(DayOfWeek), weekDay);

            public static string ToStockDate(DateTime? date) => date?.ToString("d-MMMM-yyyy") ?? "";
        }
    }
    }
