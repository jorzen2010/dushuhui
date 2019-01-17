using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using SkyCommon;


namespace SkyWechatService
{
    public class AccessTokenService
    {

        public static string GetAccessToken()
        {

            string access_token = "";

            WechatConfig wechatConfig = AccessTokenService.GetWechatConfig();

            if (DateTime.Now < DateTime.Parse(wechatConfig.AccessTokenExpiredTime))
            {
                access_token = wechatConfig.AccessToken;
            }
            else
            {
                access_token = AccessTokenService.RefrenshToken(wechatConfig.Appid, wechatConfig.AppSecret);

            }
            return access_token;
        }
        public static string  RefrenshToken(string appid, string appsecret)
        {
            string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appid, appsecret);

            HttpWebResponse res = HttpWebResponseUtility.CreateGetHttpResponse(url, null, userAgent, null);
            Stream stream = res.GetResponseStream();
            StreamReader sr = new StreamReader(stream);
            string result = sr.ReadToEnd();

            WechatAccessToken token = new WechatAccessToken();
            token = JsonConvert.DeserializeObject<WechatAccessToken>(result);
            string access_token = token.access_token;

            if (string.IsNullOrEmpty(token.access_token))
            {
                WechatError err = new WechatError();
                err = JsonConvert.DeserializeObject<WechatError>(result);
                access_token = "appid或者appsecret错误，无法获取access_token 微信错误代码：" + err.errcode + "微信错误信息：" + err.errmsg;
                LogHelper.Error(access_token);
            }
            else
            {
                AccessTokenService.WriteToken(token.access_token, System.DateTime.Now.AddMinutes(115).ToString("yyyy-MM-dd HH:mm:ss"));

            }
            return access_token;
        }


        //以下两个部分获取的是web项目中的web.config的配置信息：如下
  //      <appSettings>
  //  <add key="WechatId" value="" />
  //  <add key="WechatToken" value="" />
  //  <add key="WechatAppID" value="" />
  //  <add key="WechatAppSecret" value="" />
  //  <add key="WechatAccessToken" value="--" />
  //  <add key="WechatAccessTokenExpiredTime" value="2017-07-03 17:39:28" />
  //  <add key="WechatJsUrl" value="http://zbz.zuyanquxian.cn/wxapi.ashx" />
  //  <add key="WechatJsToken" value="" />
  //  <add key="WechatJsDomain" value="" />
  //  <add key="WechatJsReturnDomain" value="" />
  //</appSettings>

        public static void WriteToken(string token,string tokenExpiredTime)
        {
            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appsection = config.GetSection("appSettings") as AppSettingsSection;

            appsection.Settings["WechatAccessToken"].Value = token;
            appsection.Settings["WechatAccessTokenExpiredTime"].Value = tokenExpiredTime;
            config.Save();
        
        }

        public static WechatConfig GetWechatConfig()
        {

            Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            AppSettingsSection appsection = config.GetSection("appSettings") as AppSettingsSection;
            WechatConfig wechatConfig = new WechatConfig();

            wechatConfig.Appid = appsection.Settings["WechatAppID"].Value;
            wechatConfig.AppSecret = appsection.Settings["WechatAppSecret"].Value;
            wechatConfig.AccessToken = appsection.Settings["WechatAccessToken"].Value;
            wechatConfig.AccessTokenExpiredTime = appsection.Settings["WechatAccessTokenExpiredTime"].Value;

            return wechatConfig;
        }
        
    }
}