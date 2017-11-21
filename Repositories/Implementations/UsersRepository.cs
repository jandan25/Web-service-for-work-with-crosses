using CrossEntities;
using CrossEntities.Interfaces;
using GenericRepository.Implementation;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class UsersRepository : GenericRepository<Users>, IUsersRepository
    {
        IGoodWillEntitiesContext _context;
        IDbSet<Users> _dbSet;
        public UsersRepository(IGoodWillEntitiesContext context) : base (context)
        {
            _context = context;
            _dbSet = context.Set<Users>();
        }

        public Users GetByName(string name)
        {
            return _dbSet.AsNoTracking().Where(x => x.Login == name.Trim()).FirstOrDefault();
        }

        public bool ValidateUser(string name, string password, out Users user)
        {
            Guid pass;
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.Default.GetBytes(password));
                pass = new Guid(hash);
                user = _dbSet.AsNoTracking().Where(x => x.Login == name & x.Password == pass).FirstOrDefault();
                if (user != null)
                    return true;
                else return false;
            }
        }
    }
}
