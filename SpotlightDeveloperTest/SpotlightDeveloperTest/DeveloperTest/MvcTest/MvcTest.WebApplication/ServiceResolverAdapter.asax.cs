using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MvcTest.WebApplication {
	public class ServiceResolverAdapter : IDependencyResolver {
		private readonly System.Web.Mvc.IDependencyResolver dependencyResolver;

		public ServiceResolverAdapter(System.Web.Mvc.IDependencyResolver dependencyResolver) {
			if (dependencyResolver == null) throw new ArgumentNullException("dependencyResolver");
			this.dependencyResolver = dependencyResolver;
		}

		public object GetService(Type serviceType) {
			return dependencyResolver.GetService(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType) {
			return dependencyResolver.GetServices(serviceType);
		}
	}
}