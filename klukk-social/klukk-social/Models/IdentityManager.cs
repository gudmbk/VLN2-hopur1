using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;

namespace klukk_social.Models
{
	public class IdentityManager
	{
		public bool RoleExists(string name)
		{
			var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
			return rm.RoleExists(name);
		}

		public bool CreateRole(string name)
		{
			var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
			var idResult = rm.Create(new IdentityRole(name));
			return idResult.Succeeded;
		}

		public bool UserExists(string name)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			return um.FindByName(name) != null;
		}

		public User GetUser(string name)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			return um.FindByName(name);
		}

		public bool CreateUser(User user, string password)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var idResult = um.Create(user, password);
			return idResult.Succeeded;
		}

		public bool AddUserToRole(string userId, string roleName)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var idResult = um.AddToRole(userId, roleName);
			return idResult.Succeeded;
		}

		public bool UserIsInRole(string userId, string roleName)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var result = um.IsInRole(userId, roleName);
			return result;
		}

		public void ClearUserRoles(string userId)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
			var user = um.FindById(userId);
			var currentRoles = new List<IdentityUserRole>();
			currentRoles.AddRange(user.Roles);
			foreach (var role in currentRoles)
			{
				var r = rm.FindById(role.RoleId);
				um.RemoveFromRole(userId, r.Name);
			}
		}
        /*
		public void RemoverUser(string userId, string roleName)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var result = um.RemoveFromRole<User>(userId, roleName);
		}
        */

		public IList<string> GetUserRoles(string userId)
		{
			var um = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
			var result = um.GetRoles(userId);
			return result;
		}
	}
}
