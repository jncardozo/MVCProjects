[assembly: WebActivator.PostApplicationStartMethod(typeof(DBFMvcMovies.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace DBFMvcMovies.App_Start
{
    using System.Reflection;
    using System.Web.Mvc;
    using Aplication.Services.Movie.Classes;
    using Aplication.Services.Movie.Interfaces;
    using Aplication.Services.UserAccount.Classes;
    using Aplication.Services.UserAccount.Interfaces;    
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<IUserAccountAppService, UserAccountsAppService> (Lifestyle.Scoped);
            container.Register<IMoviesAppService, MoviesAppService>(Lifestyle.Scoped);

            // For instance:
            // container.Register<IUserRepository, SqlUserRepository>(Lifestyle.Scoped);
        }
    }
}