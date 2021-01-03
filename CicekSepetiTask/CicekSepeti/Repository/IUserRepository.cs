using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
   public interface IUserRepository
    {
        bool UserExist(int? userId);
    }
}
