using DAL;
using DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class SystemUserService
    {
        private UserRepo _repo = new();

        public SystemUser? GetByEmailAndPassword(string email, string password)
        {
            return _repo.GetByEmailAndPassword(email, password);
        }   
    }
}
