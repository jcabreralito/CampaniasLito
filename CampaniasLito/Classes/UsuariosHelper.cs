using CampaniasLito.Models;
using CampaniasLito.Properties;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CampaniasLito.Classes
{
    public class UsuariosHelper : IDisposable
    {
        private static ApplicationDbContext userContext = new ApplicationDbContext();
        private static CampaniasLitoContext db = new CampaniasLitoContext();

        public static bool DeleteUser(string userName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(userName);
            if (userASP == null)
            {
                return false;
            }
            var response = userManager.Delete(userASP);
            return response.Succeeded;
        }

        public static bool UpdateUserName(string currentUserName, string newUserName, string currentRoleName, string newRoleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(currentUserName);
            if (userASP == null)
            {
                return false;
            }
            userASP.Email = newUserName;
            userASP.UserName = newUserName;
            //userManager.RemovePassword(userASP.Id);
            //userManager.AddPassword(userASP.Id, newUserName);
            userManager.RemoveFromRole(userASP.Id, currentRoleName);
            userManager.AddToRole(userASP.Id, newRoleName);
            var response = userManager.Update(userASP);
            return response.Succeeded;
        }

        public static void CrearRoles(string rolName)
        {
            var rolExist = db.Roles.Where(r => r.Nombre == rolName).FirstOrDefault();

            if (rolExist != null)
            {
                if (rolExist.Nombre != rolName)
                {
                    Rol rol = new Rol();
                    rol.Nombre = rolName;
                    db.Roles.Add(rol);
                    db.SaveChanges();
                }
            }
            else
            {
                Rol rol = new Rol();
                rol.Nombre = rolName;
                db.Roles.Add(rol);
                db.SaveChanges();
            }
        }

        public static void CheckRole(string roleName)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // Check to see if Role Exists, if not create it
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }
        }

        public static void AddRole(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = userManager.FindByName(email);

            if (userASP == null)
            {
                CreateUserASP(email, roleName, password);
                return;
            }

            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CheckSuperUser()
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var email = Resources.AdminUser.ToString();
            var password = Resources.AdminPassWord.ToString();
            var userASP = userManager.FindByName(email);
            if (userASP == null)
            {
                CreateUserASP(email, "SuperAdmin", password);
                return;
            }

            userManager.AddToRole(userASP.Id, "SuperAdmin");
        }

        public static void CreateUserASP(string email, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, email);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static void CreateUserASP(string email, string roleName, string password)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));

            var userASP = new ApplicationUser
            {
                Email = email,
                UserName = email,
            };

            userManager.Create(userASP, password);
            userManager.AddToRole(userASP.Id, roleName);
        }

        public static async Task PasswordRecovery(string email)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            var user = db.Usuarios.Where(tp => tp.NombreUsuario == email).FirstOrDefault();
            if (user == null)
            {
                return;
            }


            var random = new Random();
            var newPassword = string.Format("{0}{1}{2:04}*",
                user.Nombres.Trim().ToUpper().Substring(0, 1),
                user.Apellidos.Trim().ToLower().Substring(0, 1) + "Lt",
                random.Next(10000));

            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Nuevo Password";
            var body = string.Format(@"
                <h1>Nuevo Password</h1>
                <p>Tu nuevo password es: <strong>{0}</strong></p>",
                newPassword);

            //await MailHelper.SendMail(email, "jesuscabrerag@yahoo.com.mx", "jesuscabrerag@yahoo.com.mx", subject, body);
            await MailHelper.SendMail(email, "jesuscabrerag@yahoo.com.mx", subject, body);
        }

        public static async Task ChangePassword(string email, string newPassword)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var userASP = userManager.FindByEmail(email);
            if (userASP == null)
            {
                return;
            }

            var user = db.Usuarios.Where(tp => tp.NombreUsuario == email).FirstOrDefault();
            if (user == null)
            {
                return;
            }


            userManager.RemovePassword(userASP.Id);
            userManager.AddPassword(userASP.Id, newPassword);

            var subject = "Nuevo Password";
            var body = string.Format(@"
                <h1>Nuevo Password</h1>
                <p>El usuario: <strong>{1}</strong>, ha cambiado su password: <strong>{0}</strong></p>",
                newPassword, email);

            await MailHelper.SendMail(email, "jesuscabrerag@yahoo.com.mx", subject, body);
        }

        public void Dispose()
        {
            userContext.Dispose();
            db.Dispose();
        }
    }
}