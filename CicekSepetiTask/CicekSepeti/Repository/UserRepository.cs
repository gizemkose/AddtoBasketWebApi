using CicekSepeti.Entities;
using CicekSepeti.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CicekSepeti.Repository
{
    public class UserRepository:IUserRepository
    {
        private readonly ApiContext _context;
        public UserRepository(ApiContext context)
        {
            _context = context;
        }
        public virtual bool UserExist(int? userId)
        {
            var user = _context.User.Any(s => s.Id == userId);
            if (user)
                return user;
            else
                throw new AppException("User not found");

        }
    }
}
