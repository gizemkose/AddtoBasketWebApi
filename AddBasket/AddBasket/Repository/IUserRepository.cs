using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AddBasket.Repository
{
   public interface IUserRepository
    {
        bool UserExist(int? userId);
    }
}
