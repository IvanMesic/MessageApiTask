using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfobipRecruitmentAssingment_Mesic
{
    internal class ConfigService
    {
        public static string GetApiKey()
        {
            return ConfigurationManager.AppSettings["ApiKey"];
        }
        
        public static string GetApiUrl()
        {
            return ConfigurationManager.AppSettings["ApiUrl"];
        }
    }
}
