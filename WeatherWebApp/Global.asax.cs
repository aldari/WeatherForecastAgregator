﻿using System;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Castle.Windsor;
using Castle.Windsor.Installer;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using WeatherWebApp.Plumbing;

namespace WeatherWebApp
{
	public class MvcApplication : HttpApplication
	{
		private static IWindsorContainer container;

		protected void Application_AuthenticateRequest(object sender, EventArgs e)
		{
			var cookie = Request.Cookies[FormsAuthentication.FormsCookieName];
			var ticket = TryDecryptCookie(cookie);

			if (ticket != null) //Already signed-in
			{
				//TODO: running in Cassini, you can not use custom IIdentity impelentations
				//      so you can directly use identity below if using IIS/IISExpress

				var identity = new AppIdentity(ticket);
				var genericIdentity = new GenericIdentity(identity.Name);
				var principal = new GenericPrincipal(genericIdentity, new string[0]);

				Context.User = principal;
			}
		}

		protected void Application_End()
		{
#if DEBUG
			NHibernateProfiler.Stop();
#endif
			container.Dispose();
		}

		protected void Application_Start()
		{
#if DEBUG
			NHibernateProfiler.Initialize();
#endif
			AreaRegistration.RegisterAllAreas();
			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);
			BootstrapContainer();
            AutoMapperConfiguration.Configure();
		}

		private FormsAuthenticationTicket TryDecryptCookie(HttpCookie cookie)
		{
			if (cookie == null || cookie.Value == null)
			{
				return null;
			}

			return FormsAuthentication.Decrypt(cookie.Value);
		}

		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
				);
		}

		private static void BootstrapContainer()
		{
			container = new WindsorContainer()
				.Install(FromAssembly.This(),
                FromAssembly.Named("Data.NHibernate"),
                FromAssembly.Named("Services"));
			var controllerFactory = new WindsorControllerFactory(container.Kernel);
			ControllerBuilder.Current.SetControllerFactory(controllerFactory);
		}
	}
}