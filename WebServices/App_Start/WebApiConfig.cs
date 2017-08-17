using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WebServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {            
            // Web API routes
            config.MapHttpAttributeRoutes();

            var jsonSerializerSettings = GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings;
            jsonSerializerSettings.Formatting = Formatting.Indented;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
