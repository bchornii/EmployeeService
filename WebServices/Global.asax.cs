using System.Reflection;
using System.Web;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DataAccess;
using DataAccess.Infrastructure;
using Domain.Services;

namespace WebServices
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var config = GlobalConfiguration.Configuration;

            var builder = new ContainerBuilder();
            RegisterApplicationTypes(builder);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());            

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private void RegisterApplicationTypes(ContainerBuilder builder)
        {
            RegisterServices(builder);
            RegisterCustomFactories(builder);
            RegisterDataAccess(builder);
        }

        private void RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterType<EmployeeService>().As<IEmployeeService>().InstancePerRequest();
        }

        private void RegisterCustomFactories(ContainerBuilder builder)
        {
            builder.RegisterType<NorthwindDbContextFactory>().As<INorthwindDbContextFactory>().InstancePerRequest();
        }

        private void RegisterDataAccess(ContainerBuilder builder)
        {
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
        }
    }
}
