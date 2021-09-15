using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using v1._0.Models;

namespace v1._0.security
{
    public class PersonelRoleProvider : RoleProvider
    {
        public override string ApplicationName 
        { get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            FileArchivingEntities db = new FileArchivingEntities();
            var kullanici = db.users.FirstOrDefault(x => x.mail == username);
            var kullanıcı2=db.adnnin.FirstOrDefault(x => x.mail == username);
            if (kullanici == null)
            {
                return new string[] { kullanıcı2.role };
            }
            return new string[] { kullanici.role };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}