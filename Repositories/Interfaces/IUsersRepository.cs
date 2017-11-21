using CrossEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    interface IUsersRepository
    {
        Users GetByName(string name);
        bool ValidateUser(string name, string password, out Users user);
    }
}
