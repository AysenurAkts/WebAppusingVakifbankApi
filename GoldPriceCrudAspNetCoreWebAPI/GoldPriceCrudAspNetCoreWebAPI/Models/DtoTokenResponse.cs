using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPriceCrudAspNetCoreWebAPI.Models
{
   //token alınması icin yapılan request sonucunu tutmak için tanımlanan sınıf
    public class DtoTokenResponse
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public string expires_in { get; set; }
        public string scope { get; set; }
    }
}