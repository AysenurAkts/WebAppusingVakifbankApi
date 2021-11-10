using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPriceCrudAspNetCoreWebAPI.Models
{
    //altın fiyatları servisine yapılan istek sonucu alanları için sınıflar oluşturulur
    public class GoldRatePrice
    {
        public GoldData Data { get; set; }
        public GoldHeader Header { get; set; }
    }
    public class GoldData
    {
        public List<GoldRateType> GoldRate { get; set; }
    }

    public class GoldHeader
    {
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string StatusDescriptionEn { get; set; }
        public string ObjectID { get; set; }
    }

    public class GoldRateType
    {
        public DateTime RateDate { get; set; }
        public string CurrencyCode { get; set; }
        public string ProductName { get; set; }
        public decimal SaleRate { get; set; }
        public string ISIN { get; set; }
        public decimal PurchaseRate { get; set; }
    }
}