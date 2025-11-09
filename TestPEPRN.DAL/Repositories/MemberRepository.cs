using System.Linq;
using TestPEPRN.DAL.Models;

namespace TestPEPRN.DAL.Repositories
{
    public class MemberRepository
    {
        public StaffMember? GetByEmailAndPassword(string email, string password)
        {
            using var context = new AirConditionerShop2024DbContext();
            return context.StaffMembers
                .FirstOrDefault(m => m.EmailAddress.ToLower() == email.ToLower() 
                                  && m.Password.ToLower() == password.ToLower());
        }
    }
}
