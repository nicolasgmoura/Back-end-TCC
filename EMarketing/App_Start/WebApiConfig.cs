using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace EMarketing
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            
            var formatters = config.Formatters;
            formatters.Remove(formatters.XmlFormatter);

            var jsonSettings = formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.None;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
            formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
