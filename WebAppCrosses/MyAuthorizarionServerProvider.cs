using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using CrossEntities;
using Microsoft.Owin.Security.OAuth;
using WebAppCrosses.Attributes;
using Repositories;

namespace WebAppCrosses
{
    public class MyAuthorizarionServerProvider : OAuthAuthorizationServerProvider
    {
        private IUnitOfWorkFactory _factory;

        public MyAuthorizarionServerProvider()
        {
            _factory = new UnitOfWorkFactory();
        }
        public MyAuthorizarionServerProvider(IUnitOfWorkFactory factory)
        {
            _factory = factory;
        }

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
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<Users>();
                var user = repo.GetByParam(u => u.Login == context.UserName && u.Password.ToString() == context.Password);
                return user;
            }
        }

        public string GetUserRole(Users user)
        {
            using (IUnitOfWork unitOfWork = _factory.Create())
            {
                var repo = unitOfWork.GetStandardRepo<UserRoles>();
                var request = repo.GetByParam(u => u.UserRoleID == user.RoleID);
                string result = request.Name;
                return result;
            }
        }
    }
}