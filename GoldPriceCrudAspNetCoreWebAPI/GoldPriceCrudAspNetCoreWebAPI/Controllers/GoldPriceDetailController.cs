using GoldPriceCrudAspNetCoreWebAPI.Constants;
using GoldPriceCrudAspNetCoreWebAPI.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using GoldPriceCrudAspNetCoreWebAPI;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace GoldPriceCrudAspNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //Yetkilendirme icin token alma işlemleri gerçekleştirilir
    public class GoldPriceDetailController : Controller
    {
        private readonly IConfiguration configuration;

        public GoldPriceDetailController(IConfiguration config)
        {
            this.configuration = config;
        }

        [HttpPost("getTokenId")]
        public string GetToken()
        {
            //gerekli parametreler doldurulur
            string postString = string.Format("grant_type={0}&client_id={1}&client_secret={2}&scope={3}",
                                              ConstVariables.grant_type,
                                              ConstVariables.client_id,
                                              ConstVariables.client_secret,
                                              ConstVariables.scope);
            
            //parametreler eklenerek post request yapılır ve response olarak token alınır
            var httpRequest = WebRequest.Create("https://apigw.vakifbank.com.tr:8443/auth/oauth/v2/token");
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            httpRequest.ContentLength = postString.Length;
            httpRequest.Proxy = WebRequest.DefaultWebProxy;

            StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());
            streamWriter.Write(postString);
            streamWriter.Close();

            WebResponse webResponse = httpRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(stream);
            string content = streamReader.ReadToEnd();

            DtoTokenResponse dtoTokenResponse = JsonConvert.DeserializeObject<DtoTokenResponse>(content);
            //token bilgisi döndürülür
            return dtoTokenResponse.access_token;
        }

        //Tarih bilgisine göre altın fiyatları alınır
        //url: http://localhost:55176/api/GoldPriceDetail/{goldPriceDateTime}
        [HttpGet("{goldPriceDateTime}")]
        public List<GoldRateType> GetGoldRate(DateTime goldPriceDateTime)
        {
            List<GoldRateType> goldRateType = new List<GoldRateType>();
            try
            {
                //kullanıcıdan alınan tarih bilgisi formatı düzenlenir
                string body = "{ \"PriceDate\": \"" + goldPriceDateTime.Year + "-" +
                                                  goldPriceDateTime.Month.ToString().PadLeft(2, '0') + "-" +
                                                  goldPriceDateTime.Day.ToString().PadLeft(2, '0') + "T00:00:00+03:00\"}";

                //token olusturulur
                string tokenId = GetToken();

                //post request yapılır ve goldRate çıktısı response olarak alınır
                var httpRequest = WebRequest.Create("https://apigw.vakifbank.com.tr:8443/getGoldPrices");
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                httpRequest.Headers.Add("Authorization", "Bearer " + tokenId);
                httpRequest.Proxy = WebRequest.DefaultWebProxy;
                httpRequest.ContentLength = body.Length;

                StreamWriter streamWriter = new StreamWriter(httpRequest.GetRequestStream());
                streamWriter.Write(body);
                streamWriter.Close();

                WebResponse webResponse = httpRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(stream);
                string content = streamReader.ReadToEnd();

                GoldRatePrice goldRate = JsonConvert.DeserializeObject<GoldRatePrice>(content);
                goldRateType = goldRate.Data.GoldRate;

                //veritabanı loglama islemlerini yapan fonksiyonn çağırılır
                DatabaseEntity databaseEntity = new DatabaseEntity(configuration);
                databaseEntity.InsertGoldPricesLog(goldRateType);
            }
            catch (Exception ex)
            {
                //Servisten kayıt dönmediğinde Bad Request Hatası döner. Ekranda bu hata gösterilmek istenmediği için hata fırlatılmamıştır.
                //Bunun yerine sorgu sonucundan kayıt dönmediği ekrandaki grid'de gösterilmiştir.
            }
            //ilgili tarihin altın fiyat bilgilerini tutan goldRateType döndürülür
            return goldRateType;
        }
    }
}