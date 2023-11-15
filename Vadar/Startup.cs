using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Vadar.Models;
using System.Linq;


[assembly: OwinStartupAttribute(typeof(Vadar.Startup))]
namespace Vadar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndUsers();
        }

        private void CreateRolesAndUsers()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // Crear roles si no existen
                if (!roleManager.RoleExists("Administrador"))
                {
                    var role = new IdentityRole { Name = "Administrador" };
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Jefa"))
                {
                    var role = new IdentityRole { Name = "Jefa" };
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Empleado"))
                {
                    var role = new IdentityRole { Name = "Empleado" };
                    roleManager.Create(role);
                }

                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                var usuario = userManager.FindByEmail("gabo05gabriel@gmail.com");
                if (usuario != null && !userManager.IsInRole(usuario.Id, "Administrador"))
                {
                    userManager.AddToRole(usuario.Id, "Administrador");
                }

                var userManager2 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var usuario2 = userManager2.FindByEmail("deidyvaca06@gmail.com");
                if (usuario2 != null && !userManager2.IsInRole(usuario2.Id, "Jefa"))
                {
                    userManager2.AddToRole(usuario2.Id, "Jefa");
                }

                var userManager3 = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var usuarios = userManager3.Users.Where(user => user.Email != "gabo05gabriel@gmail.com" && user.Email != "deidyvaca06@gmail.com").ToList();

                foreach (var usuarioEmpleado in usuarios)
                {
                    if (!userManager3.IsInRole(usuarioEmpleado.Id, "Empleado"))
                    {
                        userManager3.AddToRole(usuarioEmpleado.Id, "Empleado");
                    }
                }

            }
        }
    }
}
