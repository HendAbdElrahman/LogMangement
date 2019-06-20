[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LogManagement.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LogManagement.NinjectWebCommon), "Stop")]

namespace LogManagement
{
    using System;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using Business;
    using IBusiness;
    using LogManagement.App_Start;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon1
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
                GlobalConfiguration.Configuration.DependencyResolver = new App_Start.NinjectDependencyResolver(kernel);

                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(kernel));
                return kernel;

            }
            catch(Exception ex)
            {
                kernel?.Dispose();
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

            /* kernel.Bind<IDomainModels.IDownloadedFile>().To<DomainModels.DownloadedFile>().InTransientScope();

             kernel.Bind<IDomainModels.IProcessingStatus>().To<DomainModels.ProcessingStatu>().InTransientScope();

             kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InThreadScope();

             kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InTransientScope();

             kernel.Bind<ILogger>().To<FileLogger>().InSingletonScope();

             kernel.Bind<IIOHelper>().To<IOHelper>().InRequestScope();

             kernel.Bind<IParser>().To<SourcesParser>().InRequestScope();

             kernel.Bind<IDownloadStrategy>().To<FTPDownloadStrategy>().InThreadScope();

             kernel.Bind<IDownloadStrategy>().To<SFTPDownloadStrategy>().InThreadScope();

             kernel.Bind<IDownloadStrategy>().To<UriDownloadStrategy>().InThreadScope();

             kernel.Bind<IDownloadStrategyFactory>().To<DownloadStrategyFactory>().InRequestScope();

             kernel.Bind<IDownloadManager>().To<DownloadManager>().InRequestScope();*/
        }        
    }
}