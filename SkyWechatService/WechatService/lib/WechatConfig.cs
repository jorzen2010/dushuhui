using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyWechatService
{
    public class WechatConfig
    {
        public string Appid { get; set; }
        public string AppSecret { get; set; }
        public string AccessToken { get; set; }
        public string AccessTokenExpiredTime { get; set; }

    }
}
