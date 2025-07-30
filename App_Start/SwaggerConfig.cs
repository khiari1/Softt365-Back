using System.Web.Http;
using Swashbuckle.Application;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SimpleFrameworkApp.App_Start.SwaggerConfig), "Register")]

namespace SimpleFrameworkApp.App_Start
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SimpleFrameworkApp API");
                    // c.IncludeXmlComments(GetXmlCommentsPath()); // Active si tu veux la doc des commentaires XML
                })
                .EnableSwaggerUi();
        }
    }
}