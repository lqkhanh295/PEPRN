using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class UserRepo
    {
        public SystemUser? GetByEmailAndPassword(string email, string password)
        {
            using (var context = new Sp25PlantInventoryDbContext())
            {
                var user = context.SystemUsers
                    .FirstOrDefault(u => u.UserEmail == email && u.UserPassword == password);
                return user;
            }
        }
    }
}
