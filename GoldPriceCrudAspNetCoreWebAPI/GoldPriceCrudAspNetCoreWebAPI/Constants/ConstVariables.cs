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
        public const string client_id = "l7xxc086bdb62c6844068d3b2287d6309f4c";
        public const string client_secret = "5581ee3687c6487bb2ec964c4315f441";
        public const string scope = "public";
    }
}