using TestPEPRN.DAL.Models;
using TestPEPRN.DAL.Repositories;

namespace TestPEPRN.BLL.Services
{
    public class MemberService
    {
        private readonly MemberRepository _repository = new();

        public StaffMember? Authenticate(string email, string password)
        {
            return _repository.GetByEmailAndPassword(email, password);
        }
    }
}
