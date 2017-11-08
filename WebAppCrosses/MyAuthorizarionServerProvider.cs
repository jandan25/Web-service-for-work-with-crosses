using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CrossEntities;
using Microsoft.Owin.Security.OAuth;
using WebAppCrosses.Attributes;

namespace WebAppCrosses
{
    public class MyAuthorizarionServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated(); // 
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var user = GetUser(context);
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if (context.UserName == user.Login && context.Password == user.Password.ToString())
            {
                identity.AddClaim(new Claim(ClaimTypes.Role,GetUserRole(user)));
                identity.AddClaim(new Claim("username", user.Login));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid grant","provided username and password is incorrect");
                return;
            }
        }

        public Users GetUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            using (var unitOfWork = new Repositories.UnitOfWork())
            {
                var repo = unitOfWork.GetStandardRepo<Users>();
                var user = repo.GetByParam(u => u.Login == context.UserName && u.Password.ToString() == context.Password);
                return user;
            }
        }

        public string GetUserRole(Users user)
        {
            using (var unitOfWork = new Repositories.UnitOfWork())
            {
                var repo = unitOfWork.GetStandardRepo<UserRoles>();
                var request = repo.GetByParam(u => u.UserRoleID == user.RoleID);
                string result = request.Name;
                return result;
            }
        }
    }
}