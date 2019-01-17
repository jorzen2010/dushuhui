using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkyWechatService
{
    public class WechatAccessToken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }

    public class WechatError
    {
        public string errcode { get; set; }
        public string errmsg { get; set; }
    }


    public class WechatResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }

    }

    
}