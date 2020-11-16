using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using System.Linq;

namespace ClubAdministration.Persistence {
    public class MemberRepository : IMemberRepository {
        private readonly ApplicationDbContext _dbContext;

        public MemberRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckDuplicateMember(Member member)
        {
            var duplicateMember = _dbContext.Members
                .Where(m => m.LastName.Equals(member.LastName) && m.FirstName.Equals(member.FirstName) && m.Id != member.Id);

            if (duplicateMember == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}