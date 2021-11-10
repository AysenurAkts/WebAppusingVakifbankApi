using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldPriceCrudAspNetCoreWebAPI.Constants
{
    //Token alınması için gerekli parametlerin tanımlandığı sınıf
    [Serializable]
    public class ConstVariables 
    {
        public const string grant_type = "client_credentials";
        public const string client_id = "**";
        public const string client_secret = "**";
        public const string scope = "public";
    }
}
