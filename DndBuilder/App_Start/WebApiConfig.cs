﻿using System.Web.Http;

namespace DndBuilder
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // TODO: Fix this so it has my own comments
            // Web API configuration and services

            // Web API routes
            //config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //////////////////////////////////////////////////////

            // DO NOT REMOVE THIS LINE
            config.MapHttpAttributeRoutes();

            // avoid convention - based routing , hence commented out
            // config.Routes.MapHttpRoute (
            // name: "DefaultApi",
            // routeTemplate: "api/{controller}/{id}",
            // defaults: new {id = RouteParameter.Optional}
            // );

            // TODO: was uncommented
            //GlobalConfiguration.Configuration.Formatters.XmlFormatter.
            //SupportedMediaTypes.Clear();
            //config.Formatters.Insert(0, new System.Net.Http.Formatting.
            //JsonMediaTypeFormatter());

            // Apply this GLOBALLY so that we don 't have to be bothered
            // with it during other JSON operations
            // TODO: was uncommented
            //Newtonsoft.Json.JsonConvert.DefaultSettings = () =>
            //new Newtonsoft.Json.JsonSerializerSettings
            //{
            //    Formatting = Newtonsoft.Json.Formatting.Indented,
            //    ReferenceLoopHandling = Newtonsoft.Json.
            //    ReferenceLoopHandling.Ignore
            //};

            /* If we don't do this then JSON will send dates in a local - time
            format that is not consistently interpreted by Chrome and IE
            ( the default format misses defining the timezone , so it is USELESS .
            This one will always be UTC )*/
            // TODO: was uncommented
            //config.Formatters.JsonFormatter.SerializerSettings.
            //DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
        }
    }
}
