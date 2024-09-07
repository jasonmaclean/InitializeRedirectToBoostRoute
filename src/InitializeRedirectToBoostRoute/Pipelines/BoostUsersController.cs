using Sitecore.Diagnostics;
using Sitecore.Security.Accounts;
using Sitecore.Text;
using Sitecore.Web;
using System.Web.Mvc;

namespace Sitecore.Support.Client.LicenseOptions.Controllers
{
    public class BoostUsersController : Controller
    {
        [HttpGet]
        public void RedirectToBoost()
        {
            if (Context.User.IsAuthenticated || !Context.User.IsAdministrator)
            {
                Log.Info($"User {Context.User.Name} is being presented a 401 response.", this);

                base.Response.StatusCode = 401;
            }
            else
            {
                var boostUrl = GetBoostUrl();

                Log.Info($"User {Context.User.Name} is being redirected to {boostUrl}.", this);

                base.Response.Redirect(boostUrl, endResponse: true);
            }
        }

        protected string GetBoostUrl()
        {
            var licenseID = SecurityModel.License.License.LicenseID;
            var licensee = SecurityModel.License.License.Licensee;

            var urlString = new UrlString(WebUtil.GetFullUrl(new StartUrlManager().GetStartUrl(Context.User)));
            urlString.Add("inv", "1");

            var urlString2 = new UrlString("http://www.sitecore.net/boost");
            urlString2.Add("url", urlString.ToString());

            urlString2.Add("lid", StringUtil.GetString(new string[]
            {
                licenseID
            }));

            urlString2.Add("host", StringUtil.GetString(new string[]
            {
                WebUtil.GetHostName()
            }));

            urlString2.Add("ip", StringUtil.GetString(new string[]
            {
                WebUtil.GetHostIPAddress()
            }));
            urlString2.Add("licensee", StringUtil.GetString(new string[]
            {
                licensee
            }));
            urlString2.Add("iisname", StringUtil.GetString(new string[]
            {
                WebUtil.GetIISName()
            }));
            return urlString2.ToString();
        }
    }
}