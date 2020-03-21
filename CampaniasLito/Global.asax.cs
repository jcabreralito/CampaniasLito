using CampaniasLito.Classes;
using CampaniasLito.Migrations;
using CampaniasLito.Models;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace CampaniasLito
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CampaniasLitoContext, Configuration>());
            CheckRolesAndSuperUser();
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CheckRolesAndSuperUser()
        {
            UsuariosHelper.CheckRole("SuperAdmin");
            UsuariosHelper.CheckRole("Admin");
            UsuariosHelper.CheckRole("User");
            UsuariosHelper.CheckRole("Servicio");
            UsuariosHelper.CheckRole("Cliente");
            UsuariosHelper.CheckRole("CONSULTAS");
            UsuariosHelper.CheckSuperUser();
            UsuariosHelper.CrearRoles("SuperAdmin");
            UsuariosHelper.CrearRoles("Admin");
            UsuariosHelper.CrearRoles("User");
            UsuariosHelper.CrearRoles("Servicio");
            UsuariosHelper.CrearRoles("Cliente");
            UsuariosHelper.CrearRoles("CONSULTAS");
        }
    }
}
