[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NewsSite.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NewsSite.Web.App_Start.NinjectWebCommon), "Stop")]

namespace NewsSite.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    using NewsSite.Data;
    using NewsSite.Data.UnitOfWork;
    using NewsSite.Web.Infrastructure.Interfaces;
    using NewsSite.Web.Infrastructure.Services;

    public static class NinjectWebCommon 
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
            kernel.Bind<INewsSiteDbContext>().To<NewsSiteDbContext>();
            kernel.Bind<INewsSiteData>().To<NewsSiteData>();

            kernel.Bind<IArticleService>().To<ArticleService>();
            kernel.Bind<IPhotoService>().To<PhotoService>();
            kernel.Bind<ICommentService>().To<CommentService>();
            kernel.Bind<ICategoryService>().To<CategoryService>();
            kernel.Bind<IAdvertismentService>().To<AdvertismentService>();

            kernel.Bind<IDropDownListService>().To<DropDownListService>();
        }        
    }
}
