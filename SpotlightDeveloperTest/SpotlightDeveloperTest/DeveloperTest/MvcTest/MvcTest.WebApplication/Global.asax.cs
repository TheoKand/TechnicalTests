using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using MvcTest.WebApplication.Services;

namespace MvcTest.WebApplication {
	public class MvcApplication : System.Web.HttpApplication {

		/* This application uses the Castle Windsor IoC container */
		private static IWindsorContainer container;

		protected void Application_Start() {
			AreaRegistration.RegisterAllAreas();
			InitializeWindsor();
			RouteConfig.RegisterRoutes(RouteTable.Routes);
		}

		private static void InitializeWindsor() {
			container = new WindsorContainer();
			container.Register(Component.For<IClock>().ImplementedBy<SystemClock>());
			container.Register(Types.FromThisAssembly().Where(t => typeof(IController).IsAssignableFrom(t)).LifestylePerWebRequest());
			DependencyResolver.SetResolver(new ServiceResolverAdapter(new WindsorDependencyResolver(container)));
		}
	}
}
