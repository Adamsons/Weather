using RestSharp;
using WeatherApp.Configuration;
using WeatherApp.Service.Services.Abstract;
using WeatherApp.Service.Services.Concrete;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeatherApp.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WeatherApp.App_Start.NinjectWebCommon), "Stop")]

namespace WeatherApp.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IRestClient>().To<RestClient>();
            kernel.Bind<IWeatherService>().To<AccuWeatherService>().WithConstructorArgument("apiUrl", ApiConfig.AccuWeatherApiUrl);
            kernel.Bind<IWeatherService>().To<BbcWeatherService>().WithConstructorArgument("apiUrl", ApiConfig.BbcWeatherApiUrl);
        }        
    }
}
