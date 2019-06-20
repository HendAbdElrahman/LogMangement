[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LogManagement.App_Start.NinjectWebCommon222), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LogManagement.App_Start.NinjectWebCommon222), "Stop")]

namespace LogManagement.App_Start
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Http;
    using System.Web.Http.Validation;
    using Business;
    using IBusiness;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.WebApi;
    using Ninject.Web.WebApi.Filter;

    public static class NinjectWebCommon222 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
               // kernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(config.Services.GetServices(typeof(ModelValidatorProvider)).Cast<ModelValidatorProvider>()))
                //kernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(new[] { new NinjectFilterProvider(kernel) }.AsEnumerable()));

                //  GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind(typeof(IParser<>)).To(typeof(JSONParser<>)).InTransientScope();
            kernel.Bind(typeof(IParserFactory<>)).To(typeof(ParserFactory<>)).InTransientScope();
            kernel.Bind<ILogger>().To(typeof(FileLogger<>)).InTransientScope();
        }        
    }
}
