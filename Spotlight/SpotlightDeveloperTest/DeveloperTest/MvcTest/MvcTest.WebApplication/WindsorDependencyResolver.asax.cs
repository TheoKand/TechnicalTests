using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Dependencies;
using Castle.Windsor;
using IDependencyResolver = System.Web.Mvc.IDependencyResolver;

namespace MvcTest.WebApplication {
	public class WindsorDependencyResolver : IDependencyResolver, System.Web.Http.Dependencies.IDependencyResolver {
		private readonly IWindsorContainer container;

		public WindsorDependencyResolver(IWindsorContainer container) {
			this.container = container;
		}

		public object GetService(Type serviceType) {
			return (container.Kernel.HasComponent(serviceType) ? container.Resolve(serviceType) : null);
		}

		public IEnumerable<object> GetServices(Type serviceType) {
			return (container.Kernel.HasComponent(serviceType) ? container.ResolveAll(serviceType).Cast<object>() : new List<object>());
		}

		public IDependencyScope BeginScope() {
			return (this);
		}

		public void Dispose() {
			// Must be a no-op since BeginScope returns this.
		}
	}
}