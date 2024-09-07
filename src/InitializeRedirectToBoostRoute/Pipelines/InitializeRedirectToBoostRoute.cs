using Sitecore.Diagnostics;
using Sitecore.Pipelines;
using System.Web.Mvc;
using System.Web.Routing;

namespace SitecoreFundamentals.InitializeRedirectToBoostRoute.Pipelines
{
    public class InitializeRedirectToBoostRoute
    {
    }
}
namespace Sitecore.Support.Mvc.Pipelines.Initialize
{
    internal class InitializeRedirectToBoostRoute
    {
        public virtual void Process(PipelineArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            this.RegisterRoutes(RouteTable.Routes, args);
        }

        protected virtual void RegisterRoutes(RouteCollection routes, PipelineArgs args)
        {
            string[] namespaces = new string[] { "Sitecore.Support.Client.LicenseOptions.Controllers" };
            routes.MapRoute("RouteName", "api/sitecore/BoostUsers/{action}", new
            {
                controller = "BoostUsers",
                action = "RedirectToBoost",
                id = UrlParameter.Optional
            }, namespaces);
        }
    }
}
