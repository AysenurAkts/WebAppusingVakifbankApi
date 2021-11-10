using GoldPriceCrudAspNetCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

namespace GoldPriceCrudAspNetCoreWebAPI
{
    public class DatabaseEntity
    {
        private readonly IConfiguration configuration;

        public DatabaseEntity(IConfiguration config)
        {
            this.configuration = config;
        }

        //Yapılan isteklerin cevabı veritabanına loglanır
        public int InsertGoldPricesLog(List<GoldRateType> goldPriceList)
        {
            string conn = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(conn);
            connection.Open();
            int count = 0;

            foreach (var item in goldPriceList)
            {
                SqlCommand com = new SqlCommand("INSERT INTO [dbo].[goldRate] ([RateDate],[CurrencyCode],[ProductName],[SaleRate],[ISIN],[PurchaseRate]) " +
                    "VALUES " + "('2020-08-14T10:54:15','TL','Altın',10.20,'ısın',10.40)", connection);
                string sql = "INSERT INTO [dbo].[goldRate] ([RateDate],[CurrencyCode],[ProductName],[SaleRate],[ISIN],[PurchaseRate],[LogDate]) " +
                    "VALUES(@RateDateParam,@CurrencyCodeParam,@ProductNameParam,@SaleRateParam,@ISINParam,@PurchaseRateParam,@LogDateParam)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@RateDateParam", SqlDbType.NVarChar, 50).Value = item.RateDate;
                    cmd.Parameters.Add("@CurrencyCodeParam", SqlDbType.NVarChar, 5).Value = item.CurrencyCode;
                    cmd.Parameters.Add("@ProductNameParam", SqlDbType.NVarChar, 50).Value = item.ProductName;
                    SqlParameter param = cmd.Parameters.Add("@SaleRateParam", SqlDbType.Decimal, 0);
                    param.Precision = 18;
                    param.Scale = 2;
                    param.Value = item.SaleRate;
                    cmd.Parameters.Add("@ISINParam", SqlDbType.NVarChar, 50).Value = item.ISIN;
                    SqlParameter param2 = cmd.Parameters.Add("@PurchaseRateParam", SqlDbType.Decimal, 0);
                    param.Precision = 18;
                    param.Scale = 2;
                    param2.Value = item.PurchaseRate;
                    cmd.Parameters.Add("@LogDateParam", SqlDbType.NVarChar,50).Value = DateTime.Now;
                    cmd.CommandType = CommandType.Text;
                    count += cmd.ExecuteNonQuery();
                }
            }
            connection.Close();
            return count;
        }
    }
}
