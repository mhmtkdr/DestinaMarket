﻿using DestinaMarket.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DestinaMarket.Web.Startup))]
namespace DestinaMarket.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            //createRoles();
        }
    }
    //private void createRoles()
    //{
    //    ApplicationDbContext context = new ApplicationDbContext();
    //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
    //    if (!roleManager.RoleExists("User"))
    //    {
    //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
    //        role.Name = "User";
    //        roleManager.Create(role);
    //    }
    //    if (!roleManager.RoleExists("Admin"))
    //    {
    //        var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
    //        role.Name = "Admin";
    //        roleManager.Create(role);
    //    }
    //}
}
